using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wolf_Server_Manager
{
    public partial class AddServerForm : Form
    {
        public WolfServer NewServer;
        WolfServer ServerToEdit;

        public AddServerForm()
        {
            InitializeComponent();
        }

        public AddServerForm(WolfServer server) : this()
        {
            ServerToEdit = server;

            tbServerName.Text = ServerToEdit.Name;
            mtbServerIp.Text = ServerToEdit.Ip;
            nudWolfPort.Value = ServerToEdit.WolfPort;
            nudSshPort.Value = ServerToEdit.SshPort;
            tbSshUsername.Text = ServerToEdit.SshUsername;
            tbSshPassword.Text = ServerToEdit.SshPassword;
            tbGameDirectory.Text = ServerToEdit.GameDirectory;
            tbConfigName.Text = ServerToEdit.ConfigName;
            nudMaxClients.Value = ServerToEdit.MaxClientSlots;
            tbExecutableName.Text = ServerToEdit.ExecutableName;
            tbPrivatePassword.Text = ServerToEdit.PrivatePassword;
            nudPrivateSlots.Value = ServerToEdit.PrivateClients;
            tbRconPassword.Text = ServerToEdit.RconPassword;
            nudPureServer.Value = ServerToEdit.PureServer;

            Text = buttonAddServer.Text = "Edit Server";
        }

        private void buttonAddServer_Click(object sender, EventArgs e)
        {
            WolfServer server;

            if (ServerToEdit != null)
            {
                server = ServerToEdit;
            }
            else
            {
                server = NewServer = new WolfServer();
            }

            server.Name = tbServerName.Text;
            server.Ip = mtbServerIp.Text;
            server.WolfPort = (ushort)nudWolfPort.Value;
            server.SshPort = (ushort)nudSshPort.Value;
            server.SshUsername = tbSshUsername.Text;
            server.SshPassword = tbSshPassword.Text;
            server.GameDirectory = tbGameDirectory.Text;
            server.ConfigName = tbConfigName.Text;
            server.MaxClientSlots = (int)nudMaxClients.Value;
            server.ExecutableName = tbExecutableName.Text;
            server.PrivatePassword = tbPrivatePassword.Text;
            server.PrivateClients = (int)nudPrivateSlots.Value;
            server.RconPassword = tbRconPassword.Text;
            server.PureServer = (int)nudPureServer.Value;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
