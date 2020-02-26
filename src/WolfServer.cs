using Newtonsoft.Json;
using Renci.SshNet;
using Renci.SshNet.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Wolf_Server_Manager
{
    [JsonObject(MemberSerialization.OptIn)]
    public class WolfServer : IEquatable<WolfServer>
    {
        string _name;
        [JsonProperty]
        [Browsable(false)]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                ServerDisplayName = WolfClient.PurifyInput(_name);
            }
        }
        [DisplayName("Server Name")]
        public string ServerDisplayName { get; set; }
        [JsonProperty]
        [DisplayName("Server IP")]
        public string Ip
        {
            get
            {
                return IpAsString();
            }
            set
            {
                IpStringToRaw(value);
            }
        }
        [JsonProperty]
        [DisplayName("Wolf Port")]
        public ushort WolfPort { get; set; }
        [DisplayName("Players")]
        public string PlayersDisplayName { get; set; }
        [DisplayName("Map Name")]
        public string MapName { get; set; }
        [DisplayName("Response Time (ms)")]
        public string PingDisplayName { get; set; }
        [JsonProperty]
        public string SshUsername;
        [JsonProperty]
        public string SshPassword;
        [JsonProperty]
        public ushort SshPort;
        [JsonProperty]
        public string GameDirectory;
        [JsonProperty]
        public string ConfigName;
        [JsonProperty]
        public int MaxClientSlots;
        [JsonProperty]
        public string ExecutableName;
        [JsonProperty]
        public string RconPassword;
        [JsonProperty]
        public string PrivatePassword;
        [JsonProperty]
        public int PrivateClients;
        [JsonProperty]
        public int PureServer;

        public Exception SshException;

        public bool IsLinux()
        {
            return !string.IsNullOrEmpty(ExecutableName) && ExecutableName.EndsWith(".x86", StringComparison.InvariantCulture);
        }

        public bool IsWindows()
        {
            return !string.IsNullOrEmpty(ExecutableName) && ExecutableName.EndsWith(".exe", StringComparison.InvariantCulture);
        }

        public int MaxPlayers;
        public bool ConnectionEstablished;
        public long PingTime;
        public byte[] RawIp = new byte[4];

        public List<WolfPlayer> Players = new List<WolfPlayer>();
        public Dictionary<string, string> ServerInfo = new Dictionary<string, string>();

        public const string Unknown = "????";

        public SshClient SshClient;
        public WolfClient WolfClient;

        public TimeSpan SshClientTimeout = TimeSpan.FromSeconds(5);

        public bool WolfClientInUse()
        {
            try
            {
                return WolfClient != null && WolfClient.SocketInUse;
            }
            catch
            {
                return false;
            }
        }

        public bool SshClientInUse()
        {
            try
            {
                return SshClientConnecting() || (SshClient != null && SshClient.IsConnected);
            }
            catch
            {
                return false;
            }
        }

        public bool InUse()
        {
            return WolfClientInUse() || SshClientInUse();
        }

        public bool _sshClientConnecting;
        public bool SshClientConnecting()
        {
            return _sshClientConnecting;
        }

        public WolfServer()
        {
            MapName = Unknown;
            PlayersDisplayName = Unknown;
            PingDisplayName = Unknown;
        }

        public static void ToJson(List<WolfServer> servers, string filePath)
        {
            using (var sr = new StreamWriter(filePath))
            {
                sr.WriteLine(JsonConvert.SerializeObject(servers, Formatting.Indented));
            }
        }

        public static List<WolfServer> FromJson(string json)
        {
            return JsonConvert.DeserializeObject<List<WolfServer>>(json);
        }

        public string IpAsString()
        {
            return RawIp[0] + "." + RawIp[1] + "." + RawIp[2] + "." + RawIp[3];
        }

        public void IpStringToRaw(string ip)
        {
            var octets = ip.Split('.');

            if (octets.Length != 4)
            {
                return;
            }

            for (int i = 0; i < octets.Length; ++i)
            {
                if (!byte.TryParse(octets[i], out RawIp[i]))
                {
                    return;
                }
            }
        }

        public override int GetHashCode()
        {
            return RawIp[0] + RawIp[1] + RawIp[2] + RawIp[3] + WolfPort;
        }

        public override bool Equals(object other)
        {
            if (other == null)
            {
                return false;
            }

            var Server = other as WolfServer;
            if (Server == null)
            {
                return false;
            }

            return Equals(Server);
        }

        public bool Equals(WolfServer other)
        {
            if (other == null)
            {
                return false;
            }

            return Enumerable.SequenceEqual<byte>(RawIp, other.RawIp) && WolfPort.Equals(other.WolfPort);
        }

        public async Task Start(AsyncCallback completedCallback, EventHandler<ExceptionEventArgs> errorCallback)
        {
            if (IsLinux())
            {
                Log.Debug($"Ssh_StartServer__LinuxBegin: {this}");

                await ExecuteSshCommand($"pkill -u {SshUsername} {ExecutableName} ; nohup ./{ExecutableName} +exec {ConfigName} +set net_ip {Ip} +set net_port {WolfPort} +set fs_game {GameDirectory} " +
                                        $"+set sv_maxclients {MaxClientSlots} +set sv_privateclients {PrivateClients} +set sv_privatepassword {PrivatePassword} " +
                                        $"+set rconpassword {RconPassword} +set sv_hostname {Name} +set sv_pure {PureServer} > /dev/null 2>&1 &",
                                        completedCallback, errorCallback);

                Log.Debug($"Ssh_StartServer_LinuxEnd: {this}");
            }
            else if (IsWindows())
            {
                SshException = new NotImplementedException("Starting windows servers is not yet supported.");

                //await ExecuteSshCommand($"START /MIN {ExecutableName} +set net_ip {Ip} +set net_port {WolfPort} " +
                //                        $"+set sv_maxclients {MaxClientSlots} +set fs_game {GameDirectory} +exec {ConfigName}", completedCallback, errorCallback);
            }
            else
            {
                if (string.IsNullOrEmpty(ExecutableName))
                {
                    SshException = new Exception("Executable Name has not been provided.");
                }
                else
                {
                    SshException = new Exception("The provided Executable Name is not supported.");
                }
            }
        }

        public async Task Stop(AsyncCallback completedCallback, EventHandler<ExceptionEventArgs> errorCallback)
        {
            if (IsLinux())
            {
                Log.Debug($"Ssh_StopServer_LinuxBegin: {this}");

                await ExecuteSshCommand($"pkill -u {SshUsername} -f {ExecutableName}", completedCallback, errorCallback);

                Log.Debug($"Ssh_StopServer_LinuxEnd: {this}");
            }
            else if (IsWindows())
            {
                SshException = new Exception("Stopping windows servers is not yet supported.");

                //await ExecuteSshCommand($"taskkill /f /fi \"USERNAME eq {SshUsername}\" /im {ExecutableName}", completedCallback, errorCallback);
            }
            else
            {
                if (string.IsNullOrEmpty(ExecutableName))
                {
                    SshException = new Exception("Executable Name has not been provided.");
                }
                else
                {
                    SshException = new Exception("The provided Executable Name is not supported.");
                }
            }
        }

        async Task ExecuteSshCommand(string commandText, AsyncCallback completedCallback, EventHandler<ExceptionEventArgs> errorCallback)
        {
            if (string.IsNullOrEmpty(SshUsername))
            {
                SshException = new Exception("SSH Username cannot be empty.");
                return;
            }
            if (string.IsNullOrEmpty(SshPassword))
            {
                SshException = new Exception("SSH Password cannot be empty.");
                return;
            }

            try
            {
                var connectionInfo = new PasswordConnectionInfo(Ip, SshPort, SshUsername, SshPassword) { Timeout = SshClientTimeout };
                SshClient = new SshClient(connectionInfo);
                SshClient.ErrorOccurred += errorCallback;
            }
            catch (Exception e)
            {
                SshException = e;
                return;
            }

            await Task.Run(() =>
            {
                _sshClientConnecting = true;

                try
                {
                    SshClient.Connect();

                    var cmd = SshClient.CreateCommand(commandText);
                    cmd.BeginExecute(completedCallback, this);
                }
                catch (Exception e)
                {
                    SshException = new Exception($"Failed to establish an SSH connection. Possible reasons:\n\n" +
                                                    $"\t1. No internet connection.\n" +
                                                    $"\t2. Outgoing port {WolfPort} is blocked by your firewall.\n" +
                                                    $"\t3. SSH Port {SshPort} is incorrect.");

                    Log.Info($"Failed to establish an SSH connection to {this}. Exceptions: {e}");

                    SshClient.Disconnect();
                    SshClient.Dispose();
                }

                _sshClientConnecting = false;
            });
        }

        public override string ToString()
        {
            return $"{ServerDisplayName} {Ip} {WolfPort}";
        }
    }
}
