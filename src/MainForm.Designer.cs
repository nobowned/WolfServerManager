namespace Wolf_Server_Manager
{
    partial class MainForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.dgvServerList = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAddServer = new System.Windows.Forms.ToolStripButton();
            this.tsbBack = new System.Windows.Forms.ToolStripButton();
            this.tsbRefreshServers = new System.Windows.Forms.ToolStripButton();
            this.tsbRefreshPlayers = new System.Windows.Forms.ToolStripButton();
            this.tbRcon = new System.Windows.Forms.TextBox();
            this.btnRcon = new System.Windows.Forms.Button();
            this.lblServerName = new Wolf_Server_Manager.CustomLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServerList)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvServerList
            // 
            this.dgvServerList.AllowUserToAddRows = false;
            this.dgvServerList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvServerList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvServerList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvServerList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvServerList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvServerList.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvServerList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvServerList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvServerList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvServerList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvServerList.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvServerList.EnableHeadersVisualStyles = false;
            this.dgvServerList.GridColor = System.Drawing.Color.Silver;
            this.dgvServerList.Location = new System.Drawing.Point(12, 30);
            this.dgvServerList.MultiSelect = false;
            this.dgvServerList.Name = "dgvServerList";
            this.dgvServerList.ReadOnly = true;
            this.dgvServerList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvServerList.RowHeadersVisible = false;
            this.dgvServerList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvServerList.Size = new System.Drawing.Size(1051, 442);
            this.dgvServerList.TabIndex = 1;
            this.dgvServerList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvServerList_CellClick);
            this.dgvServerList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvServerList_CellDoubleClick);
            this.dgvServerList.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvServerList_CellMouseDown);
            this.dgvServerList.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvServerList_CellMouseUp);
            this.dgvServerList.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvServerList_UserDeletingRow);
            this.dgvServerList.Click += new System.EventHandler(this.dgvServerList_Click);
            this.dgvServerList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvServerList_KeyDown);
            this.dgvServerList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvServerList_KeyUp);
            this.dgvServerList.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.dgvServerList_MouseWheel);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAddServer,
            this.tsbBack,
            this.tsbRefreshServers,
            this.tsbRefreshPlayers});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1075, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbAddServer
            // 
            this.tsbAddServer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddServer.Image = ((System.Drawing.Image)(resources.GetObject("tsbAddServer.Image")));
            this.tsbAddServer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddServer.Name = "tsbAddServer";
            this.tsbAddServer.Size = new System.Drawing.Size(23, 22);
            this.tsbAddServer.Text = "Add New Server";
            this.tsbAddServer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tsbAddServer.Click += new System.EventHandler(this.tsbAddServer_Click);
            // 
            // tsbBack
            // 
            this.tsbBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbBack.Image = ((System.Drawing.Image)(resources.GetObject("tsbBack.Image")));
            this.tsbBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBack.Name = "tsbBack";
            this.tsbBack.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tsbBack.Size = new System.Drawing.Size(23, 22);
            this.tsbBack.Text = "Back To Server List";
            this.tsbBack.Visible = false;
            this.tsbBack.Click += new System.EventHandler(this.tsbBack_Click);
            // 
            // tsbRefreshServers
            // 
            this.tsbRefreshServers.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRefreshServers.Image = ((System.Drawing.Image)(resources.GetObject("tsbRefreshServers.Image")));
            this.tsbRefreshServers.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefreshServers.Name = "tsbRefreshServers";
            this.tsbRefreshServers.Size = new System.Drawing.Size(23, 22);
            this.tsbRefreshServers.Text = "Refresh Server List";
            this.tsbRefreshServers.Click += new System.EventHandler(this.tsbRefreshServers_Click);
            // 
            // tsbRefreshPlayers
            // 
            this.tsbRefreshPlayers.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRefreshPlayers.Image = ((System.Drawing.Image)(resources.GetObject("tsbRefreshPlayers.Image")));
            this.tsbRefreshPlayers.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefreshPlayers.Name = "tsbRefreshPlayers";
            this.tsbRefreshPlayers.Size = new System.Drawing.Size(23, 22);
            this.tsbRefreshPlayers.Text = "Refresh Player List";
            this.tsbRefreshPlayers.Visible = false;
            this.tsbRefreshPlayers.Click += new System.EventHandler(this.tsbRefreshPlayers_Click);
            // 
            // tbRcon
            // 
            this.tbRcon.Location = new System.Drawing.Point(756, 4);
            this.tbRcon.Name = "tbRcon";
            this.tbRcon.Size = new System.Drawing.Size(226, 20);
            this.tbRcon.TabIndex = 4;
            this.tbRcon.Visible = false;
            this.tbRcon.Enter += new System.EventHandler(this.tbRcon_Enter);
            this.tbRcon.Leave += new System.EventHandler(this.tbRcon_Leave);
            // 
            // btnRcon
            // 
            this.btnRcon.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnRcon.Location = new System.Drawing.Point(988, 3);
            this.btnRcon.Name = "btnRcon";
            this.btnRcon.Size = new System.Drawing.Size(75, 22);
            this.btnRcon.TabIndex = 5;
            this.btnRcon.Text = "Send";
            this.btnRcon.UseVisualStyleBackColor = true;
            this.btnRcon.Visible = false;
            this.btnRcon.Click += new System.EventHandler(this.btnRcon_Click);
            // 
            // lblServerName
            // 
            this.lblServerName.AutoSize = true;
            this.lblServerName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblServerName.BorderColor = System.Drawing.Color.Silver;
            this.lblServerName.BorderWidth = 1;
            this.lblServerName.ConsecutiveSpacesUntilLineSeparator = 4;
            this.lblServerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServerName.LineSeparatorColor = System.Drawing.Color.Silver;
            this.lblServerName.LineSeparatorWidth = 1;
            this.lblServerName.Location = new System.Drawing.Point(504, 6);
            this.lblServerName.Name = "lblServerName";
            this.lblServerName.Padding = new System.Windows.Forms.Padding(2);
            this.lblServerName.Size = new System.Drawing.Size(45, 19);
            this.lblServerName.TabIndex = 3;
            this.lblServerName.Text = "label1";
            this.lblServerName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblServerName.Visible = false;
            this.lblServerName.SizeChanged += new System.EventHandler(this.lblServerName_SizeChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1075, 482);
            this.Controls.Add(this.btnRcon);
            this.Controls.Add(this.tbRcon);
            this.Controls.Add(this.lblServerName);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dgvServerList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Wolf Server Manager";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dgvServerList)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvServerList;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbAddServer;
        private System.Windows.Forms.ToolStripButton tsbRefreshServers;
        private System.Windows.Forms.ToolStripButton tsbBack;
        private System.Windows.Forms.ToolStripButton tsbRefreshPlayers;
        private CustomLabel lblServerName;
        private System.Windows.Forms.TextBox tbRcon;
        private System.Windows.Forms.Button btnRcon;
    }
}

