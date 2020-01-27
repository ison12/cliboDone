namespace CliboDone.Forms
{
    partial class FMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMain));
            this.menuBar = new System.Windows.Forms.MenuStrip();
            this.menuGroupFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemConvertScriptCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemOpenConvertScriptDir = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemConvertScriptRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemHide = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGroupConvert = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGroupHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemManual = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemVersion = new System.Windows.Forms.ToolStripMenuItem();
            this.txtTargetConvertScript = new System.Windows.Forms.TextBox();
            this.txtResultMessage = new System.Windows.Forms.TextBox();
            this.rdoConvEnabled = new System.Windows.Forms.RadioButton();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuItemWindowShow = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuItemWindowHide = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBar.SuspendLayout();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuBar
            // 
            this.menuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuGroupFile,
            this.menuGroupConvert,
            this.menuGroupHelp});
            this.menuBar.Location = new System.Drawing.Point(0, 0);
            this.menuBar.Name = "menuBar";
            this.menuBar.Size = new System.Drawing.Size(405, 24);
            this.menuBar.TabIndex = 0;
            this.menuBar.Text = "menuStrip1";
            // 
            // menuGroupFile
            // 
            this.menuGroupFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemConvertScriptCreate,
            this.menuItemOpenConvertScriptDir,
            this.toolStripSeparator2,
            this.menuItemConvertScriptRefresh,
            this.toolStripSeparator1,
            this.menuItemHide,
            this.menuItemExit});
            this.menuGroupFile.Name = "menuGroupFile";
            this.menuGroupFile.Size = new System.Drawing.Size(67, 20);
            this.menuGroupFile.Text = "ファイル(&F)";
            // 
            // menuItemConvertScriptCreate
            // 
            this.menuItemConvertScriptCreate.Name = "menuItemConvertScriptCreate";
            this.menuItemConvertScriptCreate.Size = new System.Drawing.Size(285, 22);
            this.menuItemConvertScriptCreate.Text = "変換スクリプトファイルを作成する";
            this.menuItemConvertScriptCreate.Click += new System.EventHandler(this.menuItemConvertScriptCreate_Click);
            // 
            // menuItemOpenConvertScriptDir
            // 
            this.menuItemOpenConvertScriptDir.Name = "menuItemOpenConvertScriptDir";
            this.menuItemOpenConvertScriptDir.Size = new System.Drawing.Size(285, 22);
            this.menuItemOpenConvertScriptDir.Text = "変換スクリプトフォルダをエクスプローラで開く";
            this.menuItemOpenConvertScriptDir.Click += new System.EventHandler(this.menuItemOpenConvertScriptDir_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(282, 6);
            // 
            // menuItemConvertScriptRefresh
            // 
            this.menuItemConvertScriptRefresh.Name = "menuItemConvertScriptRefresh";
            this.menuItemConvertScriptRefresh.Size = new System.Drawing.Size(285, 22);
            this.menuItemConvertScriptRefresh.Text = "変換スクリプトの最新情報を読み込む(&E)";
            this.menuItemConvertScriptRefresh.Click += new System.EventHandler(this.menuItemConvertScriptRefresh_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(282, 6);
            // 
            // menuItemHide
            // 
            this.menuItemHide.Name = "menuItemHide";
            this.menuItemHide.Size = new System.Drawing.Size(285, 22);
            this.menuItemHide.Text = "ウィンドウを隠す(&H)";
            this.menuItemHide.Click += new System.EventHandler(this.menuItemHide_Click);
            // 
            // menuItemExit
            // 
            this.menuItemExit.Name = "menuItemExit";
            this.menuItemExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.menuItemExit.Size = new System.Drawing.Size(285, 22);
            this.menuItemExit.Text = "アプリケーションを完全に終了する(&X)";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // menuGroupConvert
            // 
            this.menuGroupConvert.Name = "menuGroupConvert";
            this.menuGroupConvert.Size = new System.Drawing.Size(58, 20);
            this.menuGroupConvert.Text = "変換(&C)";
            // 
            // menuGroupHelp
            // 
            this.menuGroupHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemManual,
            this.menuItemVersion});
            this.menuGroupHelp.Name = "menuGroupHelp";
            this.menuGroupHelp.Size = new System.Drawing.Size(65, 20);
            this.menuGroupHelp.Text = "ヘルプ(&H)";
            // 
            // menuItemManual
            // 
            this.menuItemManual.Name = "menuItemManual";
            this.menuItemManual.Size = new System.Drawing.Size(138, 22);
            this.menuItemManual.Text = "マニュアル(&M)";
            this.menuItemManual.Click += new System.EventHandler(this.menuItemManual_Click);
            // 
            // menuItemVersion
            // 
            this.menuItemVersion.Name = "menuItemVersion";
            this.menuItemVersion.Size = new System.Drawing.Size(138, 22);
            this.menuItemVersion.Text = "バージョン(&A)";
            this.menuItemVersion.Click += new System.EventHandler(this.menuItemVersion_Click);
            // 
            // txtTargetConvertScript
            // 
            this.txtTargetConvertScript.Location = new System.Drawing.Point(68, 32);
            this.txtTargetConvertScript.Name = "txtTargetConvertScript";
            this.txtTargetConvertScript.ReadOnly = true;
            this.txtTargetConvertScript.Size = new System.Drawing.Size(325, 23);
            this.txtTargetConvertScript.TabIndex = 2;
            this.txtTargetConvertScript.TabStop = false;
            // 
            // txtResultMessage
            // 
            this.txtResultMessage.Location = new System.Drawing.Point(68, 61);
            this.txtResultMessage.Name = "txtResultMessage";
            this.txtResultMessage.ReadOnly = true;
            this.txtResultMessage.Size = new System.Drawing.Size(325, 23);
            this.txtResultMessage.TabIndex = 3;
            this.txtResultMessage.TabStop = false;
            // 
            // rdoConvEnabled
            // 
            this.rdoConvEnabled.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdoConvEnabled.AutoCheck = false;
            this.rdoConvEnabled.AutoSize = true;
            this.rdoConvEnabled.FlatAppearance.BorderSize = 0;
            this.rdoConvEnabled.Image = global::CliboDone.Properties.Resources.ConvDisabled;
            this.rdoConvEnabled.Location = new System.Drawing.Point(8, 32);
            this.rdoConvEnabled.Name = "rdoConvEnabled";
            this.rdoConvEnabled.Size = new System.Drawing.Size(54, 54);
            this.rdoConvEnabled.TabIndex = 1;
            this.rdoConvEnabled.UseVisualStyleBackColor = true;
            this.rdoConvEnabled.CheckedChanged += new System.EventHandler(this.rdoConvEnabled_CheckedChanged);
            this.rdoConvEnabled.Click += new System.EventHandler(this.rdoConvEnabled_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "CliboDone";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuItemWindowShow,
            this.contextMenuItemWindowHide,
            this.toolStripSeparator3,
            this.contextMenuItemExit});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(244, 76);
            // 
            // contextMenuItemWindowShow
            // 
            this.contextMenuItemWindowShow.Name = "contextMenuItemWindowShow";
            this.contextMenuItemWindowShow.Size = new System.Drawing.Size(243, 22);
            this.contextMenuItemWindowShow.Text = "ウィンドウを開く(&S)";
            this.contextMenuItemWindowShow.Click += new System.EventHandler(this.contextMenuItemWindowShow_Click);
            // 
            // contextMenuItemWindowHide
            // 
            this.contextMenuItemWindowHide.Name = "contextMenuItemWindowHide";
            this.contextMenuItemWindowHide.Size = new System.Drawing.Size(243, 22);
            this.contextMenuItemWindowHide.Text = "ウィンドウを隠す(&H)";
            this.contextMenuItemWindowHide.Click += new System.EventHandler(this.contextMenuItemWindowHide_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(240, 6);
            // 
            // contextMenuItemExit
            // 
            this.contextMenuItemExit.Name = "contextMenuItemExit";
            this.contextMenuItemExit.Size = new System.Drawing.Size(243, 22);
            this.contextMenuItemExit.Text = "アプリケーションを完全に終了する(&X)";
            this.contextMenuItemExit.Click += new System.EventHandler(this.contextMenuItemExit_Click);
            // 
            // FMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 91);
            this.Controls.Add(this.txtResultMessage);
            this.Controls.Add(this.txtTargetConvertScript);
            this.Controls.Add(this.rdoConvEnabled);
            this.Controls.Add(this.menuBar);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuBar;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FMain";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "CliboDone";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.FMain_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FMain_FormClosed);
            this.Load += new System.EventHandler(this.FMain_Load);
            this.menuBar.ResumeLayout(false);
            this.menuBar.PerformLayout();
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuBar;
        private System.Windows.Forms.ToolStripMenuItem menuGroupFile;
        private System.Windows.Forms.ToolStripMenuItem menuItemExit;
        private System.Windows.Forms.ToolStripMenuItem menuGroupConvert;
        private System.Windows.Forms.ToolStripMenuItem menuItemConvertScriptRefresh;
        private System.Windows.Forms.RadioButton rdoConvEnabled;
        private System.Windows.Forms.ToolStripMenuItem menuItemOpenConvertScriptDir;
        private System.Windows.Forms.TextBox txtTargetConvertScript;
        private System.Windows.Forms.TextBox txtResultMessage;
        private System.Windows.Forms.ToolStripMenuItem menuGroupHelp;
        private System.Windows.Forms.ToolStripMenuItem menuItemManual;
        private System.Windows.Forms.ToolStripMenuItem menuItemVersion;
        private System.Windows.Forms.ToolStripMenuItem menuItemConvertScriptCreate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ToolStripMenuItem menuItemHide;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem contextMenuItemWindowShow;
        private System.Windows.Forms.ToolStripMenuItem contextMenuItemExit;
        private System.Windows.Forms.ToolStripMenuItem contextMenuItemWindowHide;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}