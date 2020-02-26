namespace Wolf_Server_Manager
{
    partial class AddServerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddServerForm));
            this.buttonAddServer = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.tbServerName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbSshPassword = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbSshUsername = new System.Windows.Forms.TextBox();
            this.nudSshPort = new System.Windows.Forms.NumericUpDown();
            this.nudWolfPort = new System.Windows.Forms.NumericUpDown();
            this.mtbServerIp = new System.Windows.Forms.MaskedTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbGameDirectory = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbConfigName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.nudMaxClients = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.tbExecutableName = new System.Windows.Forms.TextBox();
            this.tbPrivatePassword = new System.Windows.Forms.TextBox();
            this.nudPrivateSlots = new System.Windows.Forms.NumericUpDown();
            this.tbRconPassword = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.nudPureServer = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudSshPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWolfPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxClients)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrivateSlots)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPureServer)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonAddServer
            // 
            this.buttonAddServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddServer.AutoSize = true;
            this.buttonAddServer.Location = new System.Drawing.Point(12, 352);
            this.buttonAddServer.Name = "buttonAddServer";
            this.buttonAddServer.Size = new System.Drawing.Size(134, 23);
            this.buttonAddServer.TabIndex = 14;
            this.buttonAddServer.Text = "Add Server";
            this.buttonAddServer.UseVisualStyleBackColor = true;
            this.buttonAddServer.Click += new System.EventHandler(this.buttonAddServer_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.AutoSize = true;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(160, 352);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(131, 23);
            this.buttonCancel.TabIndex = 15;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // tbServerName
            // 
            this.tbServerName.Location = new System.Drawing.Point(12, 29);
            this.tbServerName.Name = "tbServerName";
            this.tbServerName.Size = new System.Drawing.Size(134, 20);
            this.tbServerName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Server Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Server IP";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Wolf Port";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 202);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "SSH Port";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 250);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "SSH Password";
            // 
            // tbSshPassword
            // 
            this.tbSshPassword.Location = new System.Drawing.Point(12, 266);
            this.tbSshPassword.Name = "tbSshPassword";
            this.tbSshPassword.Size = new System.Drawing.Size(134, 20);
            this.tbSshPassword.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 156);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "SSH Username";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tbSshUsername
            // 
            this.tbSshUsername.Location = new System.Drawing.Point(12, 172);
            this.tbSshUsername.Name = "tbSshUsername";
            this.tbSshUsername.Size = new System.Drawing.Size(134, 20);
            this.tbSshUsername.TabIndex = 3;
            // 
            // nudSshPort
            // 
            this.nudSshPort.Location = new System.Drawing.Point(12, 218);
            this.nudSshPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudSshPort.Name = "nudSshPort";
            this.nudSshPort.Size = new System.Drawing.Size(134, 20);
            this.nudSshPort.TabIndex = 4;
            // 
            // nudWolfPort
            // 
            this.nudWolfPort.Location = new System.Drawing.Point(12, 124);
            this.nudWolfPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudWolfPort.Name = "nudWolfPort";
            this.nudWolfPort.Size = new System.Drawing.Size(134, 20);
            this.nudWolfPort.TabIndex = 2;
            // 
            // mtbServerIp
            // 
            this.mtbServerIp.AsciiOnly = true;
            this.mtbServerIp.Location = new System.Drawing.Point(12, 75);
            this.mtbServerIp.Name = "mtbServerIp";
            this.mtbServerIp.Size = new System.Drawing.Size(134, 20);
            this.mtbServerIp.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 298);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "Game Directory";
            // 
            // tbGameDirectory
            // 
            this.tbGameDirectory.Location = new System.Drawing.Point(12, 314);
            this.tbGameDirectory.Name = "tbGameDirectory";
            this.tbGameDirectory.Size = new System.Drawing.Size(134, 20);
            this.tbGameDirectory.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(157, 59);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "Config Name";
            // 
            // tbConfigName
            // 
            this.tbConfigName.Location = new System.Drawing.Point(160, 75);
            this.tbConfigName.Name = "tbConfigName";
            this.tbConfigName.Size = new System.Drawing.Size(134, 20);
            this.tbConfigName.TabIndex = 8;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(157, 108);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 13);
            this.label10.TabIndex = 25;
            this.label10.Text = "Max Client Slots";
            // 
            // nudMaxClients
            // 
            this.nudMaxClients.Location = new System.Drawing.Point(160, 124);
            this.nudMaxClients.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.nudMaxClients.Name = "nudMaxClients";
            this.nudMaxClients.Size = new System.Drawing.Size(134, 20);
            this.nudMaxClients.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(157, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "Executable Name";
            // 
            // tbExecutableName
            // 
            this.tbExecutableName.Location = new System.Drawing.Point(160, 29);
            this.tbExecutableName.Name = "tbExecutableName";
            this.tbExecutableName.Size = new System.Drawing.Size(134, 20);
            this.tbExecutableName.TabIndex = 7;
            // 
            // tbPrivatePassword
            // 
            this.tbPrivatePassword.Location = new System.Drawing.Point(160, 172);
            this.tbPrivatePassword.Name = "tbPrivatePassword";
            this.tbPrivatePassword.Size = new System.Drawing.Size(134, 20);
            this.tbPrivatePassword.TabIndex = 10;
            // 
            // nudPrivateSlots
            // 
            this.nudPrivateSlots.Location = new System.Drawing.Point(160, 218);
            this.nudPrivateSlots.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.nudPrivateSlots.Name = "nudPrivateSlots";
            this.nudPrivateSlots.Size = new System.Drawing.Size(134, 20);
            this.nudPrivateSlots.TabIndex = 11;
            // 
            // tbRconPassword
            // 
            this.tbRconPassword.Location = new System.Drawing.Point(160, 266);
            this.tbRconPassword.Name = "tbRconPassword";
            this.tbRconPassword.Size = new System.Drawing.Size(134, 20);
            this.tbRconPassword.TabIndex = 12;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(157, 156);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 13);
            this.label11.TabIndex = 26;
            this.label11.Text = "Private Password";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(157, 202);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(95, 13);
            this.label12.TabIndex = 27;
            this.label12.Text = "Private Client Slots";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(157, 250);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(82, 13);
            this.label13.TabIndex = 28;
            this.label13.Text = "Rcon Password";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(157, 298);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(63, 13);
            this.label14.TabIndex = 29;
            this.label14.Text = "Pure Server";
            // 
            // nudPureServer
            // 
            this.nudPureServer.Location = new System.Drawing.Point(160, 314);
            this.nudPureServer.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPureServer.Name = "nudPureServer";
            this.nudPureServer.Size = new System.Drawing.Size(134, 20);
            this.nudPureServer.TabIndex = 13;
            // 
            // AddServerForm
            // 
            this.AcceptButton = this.buttonAddServer;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(303, 387);
            this.Controls.Add(this.nudPureServer);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.tbRconPassword);
            this.Controls.Add(this.nudPrivateSlots);
            this.Controls.Add(this.tbPrivatePassword);
            this.Controls.Add(this.tbExecutableName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.nudMaxClients);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tbConfigName);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tbGameDirectory);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.mtbServerIp);
            this.Controls.Add(this.nudWolfPort);
            this.Controls.Add(this.nudSshPort);
            this.Controls.Add(this.tbSshUsername);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbSshPassword);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbServerName);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonAddServer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AddServerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Server";
            ((System.ComponentModel.ISupportInitialize)(this.nudSshPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWolfPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxClients)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrivateSlots)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPureServer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAddServer;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox tbServerName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbSshPassword;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbSshUsername;
        private System.Windows.Forms.NumericUpDown nudSshPort;
        private System.Windows.Forms.NumericUpDown nudWolfPort;
        private System.Windows.Forms.MaskedTextBox mtbServerIp;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbGameDirectory;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbConfigName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown nudMaxClients;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbExecutableName;
        private System.Windows.Forms.TextBox tbPrivatePassword;
        private System.Windows.Forms.NumericUpDown nudPrivateSlots;
        private System.Windows.Forms.TextBox tbRconPassword;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown nudPureServer;
    }
}