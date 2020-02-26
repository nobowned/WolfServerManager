using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;
using Renci.SshNet.Common;
using System.Linq;

namespace Wolf_Server_Manager
{
    public partial class MainForm : Form
    {
        List<WolfServer> ServerList;
        BindingSource dgvServerListBindingSource;
        WolfServer SelectedServer;
        WolfServer PreviousServer; // Used to go back to previous server using XButton2;
        string ServerListDirectory = Path.Combine(Directory.GetCurrentDirectory(), "ServerLists");

        bool MouseClickingStartButtonCell;
        bool MouseClickingEditButtonCell;

        bool ControlDown;

        const int ColumnIndexStartServer = 0;
        const int ColumnIndexEditServer = 1;

        const string ButtonStartServerName = "Start";
        const string ButtonStopServerName = "Stop";

        DataGridViewDisableButtonColumn StartServerButtonColumn = new DataGridViewDisableButtonColumn();
        DataGridViewDisableButtonColumn EditServerButtonColumn = new DataGridViewDisableButtonColumn()
        {
            Text = "Edit",
            UseColumnTextForButtonValue = true
        };

        Properties.Settings Settings;

        private void DataGridViewAddButtonColumns()
        {
            try
            {
                dgvServerList.Columns.Add(StartServerButtonColumn);
                dgvServerList.Columns.Add(EditServerButtonColumn);
            }
            catch (InvalidOperationException)
            {
                StartServerButtonColumn.Visible = true;
                EditServerButtonColumn.Visible = true;
            }
        }

        private void DataGridViewRemoveButtonColumns()
        {
            StartServerButtonColumn.Visible = false;
            EditServerButtonColumn.Visible = false;
        }

        private void InitializeServerList()
        {
            ServerList = new List<WolfServer>();
            int FilesParsed = 0;

            Directory.CreateDirectory(ServerListDirectory);

            if (Directory.Exists(ServerListDirectory))
            {
                foreach (var ServerListFilePath in Directory.GetFiles(ServerListDirectory, "*.json"))
                {
                    LoadServerListFromFile(ServerListFilePath);
                    FilesParsed++;
                }
            }
            
            Log.Debug($"Parsed {ServerList.Count} servers from {FilesParsed} .json files");
        }

        private void LoadSettings()
        {
            Settings = Properties.Settings.Default;

            var fontSize = Settings.FontSize == 0.0f ?
                dgvServerList.Font.Size : Settings.FontSize;
            dgvServerList.Font = new System.Drawing.Font(dgvServerList.Font.Name, fontSize);

            var totalScreen = SystemInformation.VirtualScreen;

            if (Settings.WindowSizeSaved)
            {
                Size = Settings.WindowSize;

                if (Size.Width > totalScreen.Width || Size.Width < 300 || Size.Height > totalScreen.Height || Size.Height < 300)
                {
                    Size = totalScreen.Size;
                }
            }

            if (Settings.WindowLocationSaved)
            {
                Location = Settings.WindowLocation;

                if (Location.X > totalScreen.Width || Location.X < totalScreen.X ||
                    Location.Y > totalScreen.Height || Location.Y < totalScreen.Y)
                {
                    Location = totalScreen.Location;
                }
            }

            WindowState = Settings.WindowMaximized ? FormWindowState.Maximized : FormWindowState.Normal;
        }

        private void SaveSettings()
        {
            Settings.FontSize = dgvServerList.Font.Size;
            Settings.WindowMaximized = WindowState == FormWindowState.Maximized;
            Settings.WindowSize = Size;
            Settings.WindowSizeSaved = true;
            Settings.WindowLocation = Location;
            Settings.WindowLocationSaved = true;
            Settings.Save();
        }

        private void BindDataSources<T>(List<T> list)
        {
            dgvServerListBindingSource = new BindingSource();
            dgvServerListBindingSource.DataSource = list;
            dgvServerList.DataSource = dgvServerListBindingSource;
        }

        private void DataGridViewForceColumnDisplayIndexes()
        {
            for (int i = 2; i < dgvServerList.ColumnCount; ++i)
            {
                dgvServerList.Columns[i].DisplayIndex = i - 2;
            }

            dgvServerList.Columns[0].DisplayIndex = dgvServerList.ColumnCount - 2;
            dgvServerList.Columns[1].DisplayIndex = dgvServerList.ColumnCount - 1;
        }

        public MainForm()
        {
            Log.InitDebugLog("DebugLog.txt");
            Log.InitInfoLog("Log.txt");

            InitializeComponent();
            LoadSettings();
            InitializeServerList();
            DataGridViewAddButtonColumns();
            BindDataSources(ServerList);
            DataGridViewForceColumnDisplayIndexes();
            DataGridViewSetStartButtonNames();
            MainForm_SizeChanged(this, null);
            ResetRconCommandTextBox();
        }

        private void SaveServerListToFile(string filePath)
        {
            try
            {
                WolfServer.ToJson(ServerList, filePath);
            }
            catch (Exception ex)
            {
                Log.Info($"Failed to store current server list to {filePath}. Exception: {ex}");
            }
        }

        private void LoadServerListFromFile(string filePath)
        {
            try
            {
                using (var sr = new StreamReader(filePath))
                {
                    var serversInFile = WolfServer.FromJson(sr.ReadToEnd());
                    if (serversInFile != null)
                    {
                        foreach (var server in serversInFile)
                        {
                            if (!ServerList.Contains(server))
                            {
                                ServerList.Add(server);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Info($"Failed to load current server list from {filePath}. Exception: {ex}");
            }
        }

        private DataGridViewDisableButtonCell GetStartButton(int rowIndex)
        {
            return dgvServerList.Rows[rowIndex].Cells[ColumnIndexStartServer] as DataGridViewDisableButtonCell;
        }

        private DataGridViewDisableButtonCell GetEditButton(int rowIndex)
        {
            return dgvServerList.Rows[rowIndex].Cells[ColumnIndexEditServer] as DataGridViewDisableButtonCell;
        }

        private void EnableColumnButtons(WolfServer server)
        {
            if (SelectedServer != null)
            {
                return;
            }

            Invoke(new Action(() =>
            {
                var serverIndex = ServerList.IndexOf(server);

                var startButtonCell = GetStartButton(serverIndex);
                if (startButtonCell != null)
                {
                    startButtonCell.Enabled = true;
                }
                var editButtonCell = GetEditButton(serverIndex);
                if (editButtonCell != null)
                {
                    editButtonCell.Enabled = true;
                }

                if (server.ConnectionEstablished)
                {
                    startButtonCell.Value = ButtonStopServerName;
                }
                else
                {
                    startButtonCell.Value = ButtonStartServerName;
                }

                dgvServerList.InvalidateRow(serverIndex);
            }));
        }

        private void DisableColumnButtons(WolfServer server)
        {
            if (SelectedServer != null)
            {
                return;
            }

            Invoke(new Action(() =>
            {
                var serverIndex = ServerList.IndexOf(server);

                var startButtonCell = GetStartButton(serverIndex);
                if (startButtonCell != null)
                {
                    startButtonCell.Enabled = false;
                }
                var editButtonCell = GetEditButton(serverIndex);
                if (editButtonCell != null)
                {
                    editButtonCell.Enabled = false;
                }

                if (server.ConnectionEstablished)
                {
                    startButtonCell.Value = ButtonStopServerName;
                }
                else
                {
                    startButtonCell.Value = ButtonStartServerName;
                }

                dgvServerList.InvalidateRow(serverIndex);
            }));
        }

        private void DataGridViewSetStartButtonNames()
        {
            for (int rowIndex = 0; rowIndex < dgvServerList.RowCount; ++rowIndex)
            {
                SetStartButtonName(rowIndex);
            }
        }

        private void SetStartButtonName(int rowIndex)
        {
            var buttonCell = GetStartButton(rowIndex);

            if (ServerList[rowIndex].ConnectionEstablished)
            {
                buttonCell.Value = ButtonStopServerName;
            }
            else
            {
                buttonCell.Value = ButtonStartServerName;
            }
        }

        private void ResetRconCommandTextBox()
        {
            tbRcon.Text = "Send Rcon Command";
            tbRcon.ForeColor = System.Drawing.SystemColors.GrayText;
        }

        private void UpdateServerStatusLabel()
        {
            if (SelectedServer == null)
            {
                return;
            }

            lblServerName.Text = $"  {SelectedServer.ServerDisplayName}       {SelectedServer.MapName}       {SelectedServer.PlayersDisplayName}  ";
        }

        private void ShowErrorMessageBox(string text)
        {
            MessageBox.Show(this, text, "Error", MessageBoxButtons.OK);
        }

        private void ShowMessageBox(string text, string caption)
        {
            MessageBox.Show(this, text, caption, MessageBoxButtons.OK);
        }

        private void UpdateServer_Complete(WolfServer server)
        {
            Log.Debug($"UpdateServer_Complete: {server}");

            if (server.WolfClient != null && !server.WolfClient.SocketClosed)
            {
                server.WolfClient.DestroyClient();
            }

            EnableColumnButtons(server);
        }

        private async Task RefreshServer(WolfServer server)
        {
            Log.Debug("RefreshServer_Begin");

            DisableColumnButtons(server);

            try
            {
                server.WolfClient = new WolfClient(server);
                await server.WolfClient.UpdatedServerAsync(UpdateServer_Complete);
            }
            catch (Exception ex)
            {
                UpdateServer_Complete(server);
                Log.Debug("RefreshServer:\n" + ex.Message);
            }

            Log.Debug("RefreshServer_End");
        }

        private void RefreshServerList()
        {
            Log.Debug("RefreshServerList_Begin");

            Task.Run(() =>
            {
                for (int i = 0; i < ServerList.Count; ++i)
                {
                    var Server = ServerList[i];

                    if (Server.InUse())
                    {
                        Log.Debug($"{Server} is already refreshing.");
                        continue;
                    }

#pragma warning disable 4014
                    RefreshServer(Server);
#pragma warning restore 4014
                }
            });

            Log.Debug("RefreshServerList_End");
        }

        private void ShowServerFailureMessageBox(WolfServer server, string text)
        {
            MessageBox.Show(this, server.ServerDisplayName + ": " + text, "Error", MessageBoxButtons.OK);
            server.SshException = null;
        }

        private void tsbAddServer_Click(object sender, EventArgs e)
        {
            var addServerDialog = new AddServerForm();

            if (addServerDialog.ShowDialog() == DialogResult.OK)
            {
                dgvServerListBindingSource.AddNew();
                var NewServerIndex = ServerList.Count - 1;
                ServerList[NewServerIndex] = addServerDialog.NewServer;
                SetStartButtonName(NewServerIndex);

                Log.Debug($"Added new server {addServerDialog.NewServer.ServerDisplayName}");
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Directory.Exists(ServerListDirectory))
            {
                SaveServerListToFile(Path.Combine(ServerListDirectory, "CurrentServerList.json"));
            }

            SaveSettings();
        }

        private void tsbRefreshServers_Click(object sender, EventArgs e)
        {
            RefreshServerList();
        }

        private async void tsbRefreshPlayers_Click(object sender, EventArgs e)
        {
            Log.Debug($"Refreshing selected server {SelectedServer}");

            tsbBack.Enabled = false;
            tsbRefreshPlayers.Enabled = false;

            await RefreshServer(SelectedServer);

            tsbBack.Enabled = true;
            tsbRefreshPlayers.Enabled = true;
            UpdateServerStatusLabel();

            dgvServerListBindingSource.ResetBindings(true);
        }

        private void SshCommand_Complete(IAsyncResult ar)
        {
            var server = ar.AsyncState as WolfServer;

            if (server.SshClient.IsConnected)
            {
                server.SshClient.Disconnect();
            }
            server.SshClient.Dispose();

            Log.Debug($"SshCommand_Complete for {server}");

            Invoke(new Action(async () =>
            {
                int counter = 0;
                var mapnameBefore = server.MapName;

                while (server.MapName == mapnameBefore && counter++ < 3)
                {
                    await RefreshServer(server);
                }

                Log.Debug($"SshCommand_Complete {server} refreshed");

                EnableColumnButtons(server);
            }));
        }

        private void SshClient_ErrorOccurred(object sender, ExceptionEventArgs e)
        {
            Log.Debug($"SshClient_ErrorOccurred");

            foreach (var server in ServerList)
            {
                if (sender == server.SshClient)
                {
                    Log.Debug($"SshClient_ErrorOccurred for {server}:\n{e.Exception}");

                    server.SshException = e.Exception;

                    Invoke(new Action(() =>
                    {
                        EnableColumnButtons(server);
                        ShowServerFailureMessageBox(server, server.SshException.Message);
                    }));
                    break;
                }
            }
        }

        private async void dgvServerList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            if (SelectedServer != null)
            {
                return;
            }

            var server = ServerList[e.RowIndex];

            if (e.ColumnIndex == ColumnIndexStartServer)
            {
                if (!MouseClickingStartButtonCell)
                {
                    return;
                }

                if (server.SshClientInUse())
                {
                    return;
                }

                var startButtonCell = GetStartButton(e.RowIndex);
                if (startButtonCell != null && startButtonCell.Enabled)
                {
                    DisableColumnButtons(server);

                    if (startButtonCell.Value.ToString() == ButtonStartServerName)
                    {
                        await server.Start(SshCommand_Complete, SshClient_ErrorOccurred);
                    }
                    else
                    {
                        await server.Stop(SshCommand_Complete, SshClient_ErrorOccurred);
                    }

                    if (server.SshException != null)
                    {
                        Log.Debug($"Ssh Exception for {server}: {server.SshException}");

                        EnableColumnButtons(server);
                        ShowServerFailureMessageBox(server, server.SshException.Message);
                    }
                }
            }
            else if (e.ColumnIndex == ColumnIndexEditServer)
            {
                if (!MouseClickingEditButtonCell)
                {
                    return;
                }

                var editButtonCell = GetEditButton(e.RowIndex);
                if (editButtonCell != null && editButtonCell.Enabled)
                {
                    var editServerDialog = new AddServerForm(ServerList[e.RowIndex]);
                    if (editServerDialog.ShowDialog() == DialogResult.OK)
                    {
                        dgvServerListBindingSource.ResetItem(e.RowIndex);
                    }
                }
            }
        }

        private void dgvServerList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            if (SelectedServer != null)
            {
                return;
            }

            var startButton = GetStartButton(e.RowIndex);
            if (!startButton.Enabled)
            {
                return;
            }

            var server = ServerList[e.RowIndex];
            if (server.MapName != WolfServer.Unknown)
            {
                BindDataSources(server.Players);
                DataGridViewRemoveButtonColumns();
                tsbBack.Visible = true;
                tsbRefreshPlayers.Visible = true;
                tsbRefreshServers.Visible = false;
                tsbAddServer.Visible = false;
                SelectedServer = server;
                lblServerName.Visible = true;
                tbRcon.Visible = true;
                btnRcon.Visible = true;
                UpdateServerStatusLabel();
            }
        }

        private void tsbBack_Click(object sender, EventArgs e)
        {
            PreviousServer = SelectedServer;
            lblServerName.Visible = false;
            tsbBack.Visible = false;
            tsbRefreshPlayers.Visible = false;
            tsbRefreshServers.Visible = true;
            tsbAddServer.Visible = true;
            SelectedServer = null;
            tbRcon.Visible = false;
            btnRcon.Visible = false;

            DataGridViewAddButtonColumns();
            BindDataSources(ServerList);
            DataGridViewForceColumnDisplayIndexes();
            DataGridViewSetStartButtonNames();
            // restore disabled rows.. (refreshing/ssh command executing..)
            foreach (var Server in ServerList)
            {
                if (Server.InUse())
                {
                    DisableColumnButtons(Server);
                }
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (ServerList.Count > 0)
            {
                Log.Debug("MainForm_Shown");

                RefreshServerList();
            }
        }

        private void tsbHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Made by Nobo @ www.rtcwmp.com", "About", MessageBoxButtons.OK);
        }

        private void dgvServerList_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (e.Row.Index < 0 ||
                SelectedServer != null ||
                !GetStartButton(e.Row.Index).Enabled)
            {
                e.Cancel = true;
                return;
            }

            var Server = ServerList[e.Row.Index];

            if (MessageBox.Show(this, $"Are you sure you want to delete {Server.ServerDisplayName}?", $"Delete {Server.ServerDisplayName}",
                MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            lblServerName_SizeChanged(lblServerName, null);
        }

        private void lblServerName_SizeChanged(object sender, EventArgs e)
        {
            lblServerName.Left = (ClientSize.Width / 2) - (lblServerName.Width / 2);
            tbRcon.Left = dgvServerList.Width - tbRcon.Width - btnRcon.Width + dgvServerList.Left;
            btnRcon.Left = dgvServerList.Width - btnRcon.Width + dgvServerList.Left;
        }

        private async void btnRcon_Click(object sender, EventArgs e)
        {
            string rconText;

            if (SelectedServer == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(SelectedServer.RconPassword))
            {
                ShowServerFailureMessageBox(SelectedServer, "RconPassword is not set!");
                return;
            }

            if (tbRcon.ForeColor == System.Drawing.SystemColors.GrayText)
            {
                rconText = string.Empty;
            }
            else
            {
                rconText = tbRcon.Text;
            }

            SelectedServer.WolfClient = new WolfClient(SelectedServer);

            tbRcon.Enabled = false;
            btnRcon.Enabled = false;

            var Response = await SelectedServer.WolfClient.SendRconCommandAsync(rconText);

            tbRcon.Enabled = true;
            btnRcon.Enabled = true;

            if (!SelectedServer.WolfClient.SocketClosed)
            {
                SelectedServer.WolfClient.DestroyClient();
            }

            if (!string.IsNullOrEmpty(Response.Output))
            {
                if (Response.Failure)
                {
                    ShowErrorMessageBox(Response.Output);
                }
                else
                {
                    ResetRconCommandTextBox();
                    ShowMessageBox(Response.Output, "Success");
                }
            }
        }

        private void tbRcon_Enter(object sender, EventArgs e)
        {
            if (tbRcon.ForeColor == System.Drawing.SystemColors.GrayText)
            {
                tbRcon.Text = string.Empty;
                tbRcon.ForeColor = System.Drawing.SystemColors.WindowText;
            }
        }

        private void tbRcon_Leave(object sender, EventArgs e)
        {
            if (tbRcon.Text == string.Empty)
            {
                ResetRconCommandTextBox();
            }
        }

        private void dgvServerList_Click(object sender, EventArgs e)
        {
            var mouseEventArgs = e as MouseEventArgs;

            if (mouseEventArgs == null)
            {
                return;
            }

            if (mouseEventArgs.Button == MouseButtons.XButton1)
            {
                if (SelectedServer != null)
                {
                    tsbBack_Click(tsbBack, e);
                }
            }
            else if (mouseEventArgs.Button == MouseButtons.XButton2)
            {
                if (SelectedServer == null)
                {
                    if (PreviousServer != null)
                    {
                        var ServerIndex = ServerList.IndexOf(PreviousServer);

                        if (ServerIndex == -1)
                        {
                            PreviousServer = null;
                            return;
                        }

                        dgvServerList_CellDoubleClick(dgvServerList, new DataGridViewCellEventArgs(0, ServerIndex));
                    }
                }
            }
        }

        private void dgvServerList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete)
            {
                e.SuppressKeyPress = true;
            }

            if (e.KeyCode == Keys.ControlKey)
            {
                ControlDown = true;
            }
        }

        private void dgvServerList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                ControlDown = false;
            }
        }

        void dgvServerList_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!ControlDown)
            {
                return;
            }

            var fontDelta = e.Delta > 0 ? 1.0f : -1.0f;
            dgvServerList.Font = new System.Drawing.Font(dgvServerList.Font.Name, dgvServerList.Font.Size + fontDelta);
        }

        private void dgvServerList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == ColumnIndexStartServer)
            {
                MouseClickingStartButtonCell = true;
            }
            else if (e.ColumnIndex == ColumnIndexEditServer)
            {
                MouseClickingEditButtonCell = true;
            }
        }

        private void dgvServerList_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            MouseClickingStartButtonCell = false;
            MouseClickingEditButtonCell = false;
        }
    }
}
