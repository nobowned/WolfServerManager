using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Timers;
using System.Diagnostics;

namespace Wolf_Server_Manager
{
    public class RconCommand
    {
        public const string Say = "say";
        public const string MapRestart = "map_restart";
        public const string Kick = "kick";
        public const string ClientKick = "clientkick";
        public const string Map = "map";
        public const string Exec = "exec";
        public const string KillServer = "killserver";

        public string Command;
        public string Usage;
        public int MinArguments;
        public int MaxArguments;

        public RconCommand(string command, string usage, int minArgs, int maxArgs)
        {
            Command = command;
            Usage = usage;
            MinArguments = minArgs;
            MaxArguments = maxArgs;
        }

        public string GetUsage()
        {
            return $"Usage: {Usage}";
        }
    }

    public class WolfClient
    {
        UdpClient Udp;
        Timer AsyncTimer;
        WolfServer Server;
        public bool SocketInUse { get; private set; }

        public const int DefaultTimeoutMilliseconds = 2000;
        public const string GetServerStatusResponse = "statusResponse";

        public static readonly Dictionary<string, RconCommand> RconCommands = new Dictionary<string, RconCommand>()
        {
            { RconCommand.Say, new RconCommand(RconCommand.Say, $"{RconCommand.Say} <argument> [arguments ...]", 1, -1) },
            { RconCommand.MapRestart, new RconCommand(RconCommand.MapRestart, RconCommand.MapRestart, 0, 0) },
            { RconCommand.Kick, new RconCommand(RconCommand.Kick, $"{RconCommand.Kick} <name_without_spaces>. Names with spaces cannot be kicked.", 1, 1) },
            { RconCommand.ClientKick, new RconCommand(RconCommand.ClientKick, $"{RconCommand.ClientKick} <client_number>", 1, 1) },
            { RconCommand.Map, new RconCommand(RconCommand.Map, $"{RconCommand.Map} <map_name>", 1, 1) },
            { RconCommand.Exec, new RconCommand(RconCommand.Exec, $"{RconCommand.Exec} <config_name>", 1, 1) },
            { RconCommand.KillServer, new RconCommand(RconCommand.KillServer, RconCommand.KillServer, 0, 0) },
        };

        public WolfClient(WolfServer remoteServer, TimeSpan? timeout = null)
        {
            Server = remoteServer;
            InitializeClient();
            AsyncTimer = new Timer() { Interval = timeout == null ? DefaultTimeoutMilliseconds : ((TimeSpan)timeout).TotalMilliseconds, AutoReset = false };
            AsyncTimer.Elapsed += (sender, e) =>
            {
                DestroyClient();
            };
        }

        public bool SocketClosed
        {
            get
            {
                return Udp.Client == null;
            }
        }

        public void DestroyClient()
        {
            Udp.Close();
            SocketInUse = false;

            Log.Debug("DestroyUdpClient");
        }

        public void InitializeClient()
        {
            Udp = new UdpClient();
            Udp.Connect(new IPEndPoint(new IPAddress(Server.RawIp), Server.WolfPort));

            Log.Debug("InitializeUdpClient");
        }

        public void SetTimeout(TimeSpan ts)
        {
            if (AsyncTimer.Enabled)
            {
                throw new Exception("SetTimeout: Timer in use.");
            }

            AsyncTimer.Interval = ts.TotalMilliseconds;
        }

        public static string PurifyInput(string input)
        {
            var Result = new StringBuilder();

            for (int i = 0; i < input.Length; ++i)
            {
                if (input[i] == '^')
                {
                    i++;
                    continue;
                }
                if (input[i] < 32 || input[i] > 126)
                {
                    continue;
                }

                Result.Append(input[i]);
            }

            return Result.ToString();
        }

        public class SendDataResult
        {
            public byte[] Buffer;
            public bool Success;
            public long ResponseTime;
        }

        async Task<SendDataResult> SendConnectionlessPacketAsync(List<byte> Data)
        {
            SendDataResult DataResult = new SendDataResult();
            Stopwatch PingWatch = new Stopwatch();

            if (SocketInUse)
            {
                return DataResult;
            }

            if (SocketClosed)
            {
                InitializeClient();
            }

            Log.Debug("SendConnectionlessPacketAsync_Begin");

            for (int i = 0; i < 4; ++i)
            {
                Data.Insert(0, 0xFF);
            }

            SocketInUse = true;

            Log.Debug("SendConnectionlessPacketAsync_SendAsync_Begin");

            AsyncTimer.Start();
            PingWatch.Start();

            int SentLength = 0;
            try
            {
                SentLength = await Udp.SendAsync(Data.ToArray(), Data.Count);
            }
            catch
            {
                Log.Debug("SendConnectionlessPacketAsync_SendAsync_Exception");

                SocketInUse = false;
                return DataResult;
            }

            AsyncTimer.Stop();

            Log.Debug("SendConnectionlessPacketAsync_SendAsync_End");

            if (SentLength == Data.Count)
            {
                Log.Debug("SendConnectionlessPacketAsync_ReceiveAsync_Begin");

                UdpReceiveResult UdpResult;
                AsyncTimer.Start();

                try
                {
                    UdpResult = await Udp.ReceiveAsync();
                }
                catch
                {
                    Log.Debug("SendConnectionlessPacketAsync_ReceiveAsync_Exception");

                    SocketInUse = false;
                    return DataResult;
                }

                PingWatch.Stop();
                AsyncTimer.Stop();

                Log.Debug("SendConnectionlessPacketAsync_ReceiveAsync_End");

                DataResult.Buffer = UdpResult.Buffer.Skip(4).ToArray();
                DataResult.ResponseTime = PingWatch.ElapsedMilliseconds > 9999 ? 9999 : PingWatch.ElapsedMilliseconds;
                DataResult.Success = true;
            }

            SocketInUse = false;

            Log.Debug("SendConnectionlessPacketAsync_End");

            return DataResult;
        }

        async Task<SendDataResult> SendConnectionlessPacketAsync(string Str)
        {
            return await SendConnectionlessPacketAsync(Encoding.ASCII.GetBytes(Str + Char.MinValue).ToList());
        }

        public async Task UpdatedServerAsync(Action<WolfServer> updateComplete)
        {
            Log.Debug($"Sending getstatus request to {Server}");

            var Response = await SendConnectionlessPacketAsync("getstatus");

            Server.Players.Clear();
            Server.MapName = WolfServer.Unknown;
            Server.PlayersDisplayName = WolfServer.Unknown;
            Server.PingDisplayName = WolfServer.Unknown;
            Server.ConnectionEstablished = false;

            if (!Response.Success)
            {
                Log.Debug($"getstatus request for {Server} failed.");

                updateComplete(Server);
                return;
            }

            Log.Debug($"getstatus request for {Server} succeeded in {Response.ResponseTime} ms.");

            Server.PingTime = Response.ResponseTime;
            Server.PingDisplayName = Server.PingTime.ToString();

            var ResponseAsString = Encoding.ASCII.GetString(Response.Buffer);
            if (ResponseAsString.Length < GetServerStatusResponse.Length ||
                ResponseAsString.Substring(0, GetServerStatusResponse.Length) != GetServerStatusResponse)
            {
                Log.Debug($"getstatus response was deemed invalid from {Server}. Response:\n{ResponseAsString}.");

                updateComplete(Server);
                return;
            }

            Log.Debug($"getstatus response was deemed valid from {Server}. Response:\n{ResponseAsString}.");

            ResponseAsString = ResponseAsString.Remove(0, GetServerStatusResponse.Length + 2);
            var Items = ResponseAsString.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            int PlayerCount = Items.Length == 1 ? 0 : Items.Length - 1;
            var ServerInfo = Items[0];
            var ServerInfos = ServerInfo.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < (ServerInfos.Length - 1); i += 2)
            {
                Server.ServerInfo[ServerInfos[i]] = ServerInfos[i + 1];
            }

            for (int i = 1; i < (PlayerCount + 1); ++i)
            {
                var PlayerInfo = Items[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (PlayerInfo.Length < 3)
                {
                    continue;
                }

                WolfPlayer Player = new WolfPlayer();
                Player.Score = int.Parse(PlayerInfo[0]);
                Player.Ping = int.Parse(PlayerInfo[1]);
                Player.PlayerName = PlayerInfo[2];

                for (int j = 3; j < PlayerInfo.Length; ++j)
                {
                    Player.PlayerName += " " + PlayerInfo[j];
                }
                Player.PlayerName = Player.PlayerName.Substring(1, Player.PlayerName.Length - 2);
                Player.PlayerName = PurifyInput(Player.PlayerName);

                var Duplicates = Server.Players.Where(p => p.PlayerName == Player.PlayerName);
                if (Duplicates.Any())
                {
                    Player.Duplicate = Duplicates.Count();
                }

                Server.Players.Add(Player);
            }

            Server.MaxPlayers = int.Parse(Server.ServerInfo["sv_maxclients"]);
            Server.Name = Server.ServerInfo["sv_hostname"];
            Server.MapName = PurifyInput(Server.ServerInfo["mapname"]);
            Server.PlayersDisplayName = $"{Server.Players.Count}/{Server.MaxPlayers}";

            Server.ConnectionEstablished = true;

            Log.Debug($"getstatus response from {Server} parsed successfully.");

            updateComplete(Server);
        }

        public class RconCommandResult
        {
            public string Output;
            public bool Failure;
        }

        public async Task<RconCommandResult> SendRconCommandAsync(string commandText)
        {
            var SplitCommand = commandText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            
            if (SplitCommand.Length < 1)
            {
                return new RconCommandResult()
                {
                    Failure = true,
                    Output = $"Invalid command!\n\nAvailable commands are:\n{string.Join(", ", RconCommands.Keys)}"
                };
            }

            var Command = SplitCommand[0];

            if (!RconCommands.ContainsKey(Command))
            {
                return new RconCommandResult()
                {
                    Failure = true,
                    Output = $"{Command} is not a valid command!\n\nAvailable commands are:\n{string.Join(", ", RconCommands.Keys)}"
                };
            }

            var RconCmd = RconCommands[Command];
            var Args = SplitCommand.Skip(1);

            if (Args.Count() < RconCmd.MinArguments)
            {
                return new RconCommandResult()
                {
                    Failure = true,
                    Output = $"Too few arguments provided. {RconCmd.GetUsage()}"
                };
            }

            if (RconCmd.MaxArguments != -1)
            {
                if (Args.Count() > RconCmd.MaxArguments)
                {
                    return new RconCommandResult()
                    {
                        Failure = true,
                        Output = $"Too many arguments provided. {RconCmd.GetUsage()}"
                    };
                }
            }

            await SendConnectionlessPacketAsync($"rcon {Server.RconPassword} {commandText}");

            return new RconCommandResult()
            {
                Failure = false,
                Output = $"Rcon command '{commandText}' has been sent to the server."
            };
        }
    }
}
