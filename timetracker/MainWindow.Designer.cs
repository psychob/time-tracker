namespace timetracker
{
 partial class MainWindow
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
   System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
   this.lvTrackApp = new System.Windows.Forms.ListView();
   this.chInternalId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
   this.chApplicationName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
   this.chTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
   this.chFullTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
   this.menuStrip1 = new System.Windows.Forms.MenuStrip();
   this.applicationDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
   this.trackedApplicationDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
   this.validateProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
   this.refreshRateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
   this.listRefreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
   this.sToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
   this.sToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
   this.sToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
   this.sToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
   this.mToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
   this.mToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
   this.mToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
   this.mToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
   this.mToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
   this.validateRefreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
   this.mToolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
   this.mToolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
   this.mToolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
   this.mToolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
   this.mToolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
   this.mToolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
   this.hToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
   this.timer_RefreshCurrentApplications = new System.Windows.Forms.Timer(this.components);
   this.niMainApp = new System.Windows.Forms.NotifyIcon(this.components);
   this.cmsNotifyStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
   this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
   this.timer_validate = new System.Windows.Forms.Timer(this.components);
   this.statusStrip1 = new System.Windows.Forms.StatusStrip();
   this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
   this.tsslRefreshCount = new System.Windows.Forms.ToolStripStatusLabel();
   this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
   this.tsslValidateRefresh = new System.Windows.Forms.ToolStripStatusLabel();
   this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
   this.tsslInvalidProcessQueue = new System.Windows.Forms.ToolStripStatusLabel();
   this.timer_updateTimers = new System.Windows.Forms.Timer(this.components);
   this.timer_InvalidProcessorRefresh = new System.Windows.Forms.Timer(this.components);
   this.sToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
   this.menuStrip1.SuspendLayout();
   this.cmsNotifyStrip.SuspendLayout();
   this.statusStrip1.SuspendLayout();
   this.SuspendLayout();
   // 
   // lvTrackApp
   // 
   this.lvTrackApp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
   this.lvTrackApp.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chInternalId,
            this.chApplicationName,
            this.chTime,
            this.chFullTime});
   this.lvTrackApp.FullRowSelect = true;
   this.lvTrackApp.GridLines = true;
   this.lvTrackApp.Location = new System.Drawing.Point(12, 27);
   this.lvTrackApp.Name = "lvTrackApp";
   this.lvTrackApp.Size = new System.Drawing.Size(513, 221);
   this.lvTrackApp.TabIndex = 0;
   this.lvTrackApp.UseCompatibleStateImageBehavior = false;
   this.lvTrackApp.View = System.Windows.Forms.View.Details;
   // 
   // chInternalId
   // 
   this.chInternalId.Text = "ID";
   this.chInternalId.Width = 35;
   // 
   // chApplicationName
   // 
   this.chApplicationName.Text = "Application";
   this.chApplicationName.Width = 250;
   // 
   // chTime
   // 
   this.chTime.Text = "Time Running";
   this.chTime.Width = 100;
   // 
   // chFullTime
   // 
   this.chFullTime.Text = "All Time";
   this.chFullTime.Width = 120;
   // 
   // menuStrip1
   // 
   this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.applicationDatabaseToolStripMenuItem,
            this.trackedApplicationDatabaseToolStripMenuItem,
            this.validateProcessToolStripMenuItem,
            this.refreshRateToolStripMenuItem});
   this.menuStrip1.Location = new System.Drawing.Point(0, 0);
   this.menuStrip1.Name = "menuStrip1";
   this.menuStrip1.Size = new System.Drawing.Size(537, 24);
   this.menuStrip1.TabIndex = 1;
   this.menuStrip1.Text = "menuStrip1";
   // 
   // applicationDatabaseToolStripMenuItem
   // 
   this.applicationDatabaseToolStripMenuItem.Name = "applicationDatabaseToolStripMenuItem";
   this.applicationDatabaseToolStripMenuItem.Size = new System.Drawing.Size(120, 20);
   this.applicationDatabaseToolStripMenuItem.Text = "Application Database";
   this.applicationDatabaseToolStripMenuItem.Click += new System.EventHandler(this.applicationDatabaseToolStripMenuItem_Click);
   // 
   // trackedApplicationDatabaseToolStripMenuItem
   // 
   this.trackedApplicationDatabaseToolStripMenuItem.Name = "trackedApplicationDatabaseToolStripMenuItem";
   this.trackedApplicationDatabaseToolStripMenuItem.Size = new System.Drawing.Size(161, 20);
   this.trackedApplicationDatabaseToolStripMenuItem.Text = "Tracked Application Database";
   this.trackedApplicationDatabaseToolStripMenuItem.Click += new System.EventHandler(this.trackedApplicationDatabaseToolStripMenuItem_Click);
   // 
   // validateProcessToolStripMenuItem
   // 
   this.validateProcessToolStripMenuItem.Name = "validateProcessToolStripMenuItem";
   this.validateProcessToolStripMenuItem.Size = new System.Drawing.Size(97, 20);
   this.validateProcessToolStripMenuItem.Text = "Validate Process";
   this.validateProcessToolStripMenuItem.Click += new System.EventHandler(this.validateProcessToolStripMenuItem_Click);
   // 
   // refreshRateToolStripMenuItem
   // 
   this.refreshRateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.listRefreshToolStripMenuItem,
            this.validateRefreshToolStripMenuItem});
   this.refreshRateToolStripMenuItem.Name = "refreshRateToolStripMenuItem";
   this.refreshRateToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
   this.refreshRateToolStripMenuItem.Text = "Refresh Rate";
   // 
   // listRefreshToolStripMenuItem
   // 
   this.listRefreshToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sToolStripMenuItem4,
            this.sToolStripMenuItem,
            this.sToolStripMenuItem1,
            this.sToolStripMenuItem2,
            this.sToolStripMenuItem3,
            this.mToolStripMenuItem,
            this.mToolStripMenuItem1,
            this.mToolStripMenuItem2,
            this.mToolStripMenuItem3,
            this.mToolStripMenuItem4});
   this.listRefreshToolStripMenuItem.Name = "listRefreshToolStripMenuItem";
   this.listRefreshToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
   this.listRefreshToolStripMenuItem.Text = "List Refresh";
   // 
   // sToolStripMenuItem
   // 
   this.sToolStripMenuItem.Checked = true;
   this.sToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
   this.sToolStripMenuItem.Name = "sToolStripMenuItem";
   this.sToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
   this.sToolStripMenuItem.Text = "5s";
   this.sToolStripMenuItem.Click += new System.EventHandler(this.sToolStripMenuItem_Click);
   // 
   // sToolStripMenuItem1
   // 
   this.sToolStripMenuItem1.Name = "sToolStripMenuItem1";
   this.sToolStripMenuItem1.Size = new System.Drawing.Size(94, 22);
   this.sToolStripMenuItem1.Text = "15s";
   this.sToolStripMenuItem1.Click += new System.EventHandler(this.sToolStripMenuItem1_Click);
   // 
   // sToolStripMenuItem2
   // 
   this.sToolStripMenuItem2.Name = "sToolStripMenuItem2";
   this.sToolStripMenuItem2.Size = new System.Drawing.Size(94, 22);
   this.sToolStripMenuItem2.Text = "30s";
   this.sToolStripMenuItem2.Click += new System.EventHandler(this.sToolStripMenuItem2_Click);
   // 
   // sToolStripMenuItem3
   // 
   this.sToolStripMenuItem3.Name = "sToolStripMenuItem3";
   this.sToolStripMenuItem3.Size = new System.Drawing.Size(94, 22);
   this.sToolStripMenuItem3.Text = "45s";
   this.sToolStripMenuItem3.Click += new System.EventHandler(this.sToolStripMenuItem3_Click);
   // 
   // mToolStripMenuItem
   // 
   this.mToolStripMenuItem.Name = "mToolStripMenuItem";
   this.mToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
   this.mToolStripMenuItem.Text = "1m";
   this.mToolStripMenuItem.Click += new System.EventHandler(this.mToolStripMenuItem_Click);
   // 
   // mToolStripMenuItem1
   // 
   this.mToolStripMenuItem1.Name = "mToolStripMenuItem1";
   this.mToolStripMenuItem1.Size = new System.Drawing.Size(94, 22);
   this.mToolStripMenuItem1.Text = "2m";
   this.mToolStripMenuItem1.Click += new System.EventHandler(this.mToolStripMenuItem1_Click);
   // 
   // mToolStripMenuItem2
   // 
   this.mToolStripMenuItem2.Name = "mToolStripMenuItem2";
   this.mToolStripMenuItem2.Size = new System.Drawing.Size(94, 22);
   this.mToolStripMenuItem2.Text = "5m";
   this.mToolStripMenuItem2.Click += new System.EventHandler(this.mToolStripMenuItem2_Click);
   // 
   // mToolStripMenuItem3
   // 
   this.mToolStripMenuItem3.Name = "mToolStripMenuItem3";
   this.mToolStripMenuItem3.Size = new System.Drawing.Size(94, 22);
   this.mToolStripMenuItem3.Text = "10m";
   this.mToolStripMenuItem3.Click += new System.EventHandler(this.mToolStripMenuItem3_Click);
   // 
   // mToolStripMenuItem4
   // 
   this.mToolStripMenuItem4.Name = "mToolStripMenuItem4";
   this.mToolStripMenuItem4.Size = new System.Drawing.Size(94, 22);
   this.mToolStripMenuItem4.Text = "15m";
   this.mToolStripMenuItem4.Click += new System.EventHandler(this.mToolStripMenuItem4_Click);
   // 
   // validateRefreshToolStripMenuItem
   // 
   this.validateRefreshToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mToolStripMenuItem5,
            this.mToolStripMenuItem6,
            this.mToolStripMenuItem7,
            this.mToolStripMenuItem8,
            this.mToolStripMenuItem9,
            this.mToolStripMenuItem10,
            this.hToolStripMenuItem});
   this.validateRefreshToolStripMenuItem.Name = "validateRefreshToolStripMenuItem";
   this.validateRefreshToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
   this.validateRefreshToolStripMenuItem.Text = "Validate Refresh";
   // 
   // mToolStripMenuItem5
   // 
   this.mToolStripMenuItem5.Name = "mToolStripMenuItem5";
   this.mToolStripMenuItem5.Size = new System.Drawing.Size(94, 22);
   this.mToolStripMenuItem5.Text = "1m";
   this.mToolStripMenuItem5.Click += new System.EventHandler(this.mToolStripMenuItem5_Click);
   // 
   // mToolStripMenuItem6
   // 
   this.mToolStripMenuItem6.Name = "mToolStripMenuItem6";
   this.mToolStripMenuItem6.Size = new System.Drawing.Size(94, 22);
   this.mToolStripMenuItem6.Text = "2m";
   this.mToolStripMenuItem6.Click += new System.EventHandler(this.mToolStripMenuItem6_Click);
   // 
   // mToolStripMenuItem7
   // 
   this.mToolStripMenuItem7.Checked = true;
   this.mToolStripMenuItem7.CheckState = System.Windows.Forms.CheckState.Checked;
   this.mToolStripMenuItem7.Name = "mToolStripMenuItem7";
   this.mToolStripMenuItem7.Size = new System.Drawing.Size(94, 22);
   this.mToolStripMenuItem7.Text = "5m";
   this.mToolStripMenuItem7.Click += new System.EventHandler(this.mToolStripMenuItem7_Click);
   // 
   // mToolStripMenuItem8
   // 
   this.mToolStripMenuItem8.Name = "mToolStripMenuItem8";
   this.mToolStripMenuItem8.Size = new System.Drawing.Size(94, 22);
   this.mToolStripMenuItem8.Text = "10m";
   this.mToolStripMenuItem8.Click += new System.EventHandler(this.mToolStripMenuItem8_Click);
   // 
   // mToolStripMenuItem9
   // 
   this.mToolStripMenuItem9.Name = "mToolStripMenuItem9";
   this.mToolStripMenuItem9.Size = new System.Drawing.Size(94, 22);
   this.mToolStripMenuItem9.Text = "15m";
   this.mToolStripMenuItem9.Click += new System.EventHandler(this.mToolStripMenuItem9_Click);
   // 
   // mToolStripMenuItem10
   // 
   this.mToolStripMenuItem10.Name = "mToolStripMenuItem10";
   this.mToolStripMenuItem10.Size = new System.Drawing.Size(94, 22);
   this.mToolStripMenuItem10.Text = "30m";
   this.mToolStripMenuItem10.Click += new System.EventHandler(this.mToolStripMenuItem10_Click);
   // 
   // hToolStripMenuItem
   // 
   this.hToolStripMenuItem.Name = "hToolStripMenuItem";
   this.hToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
   this.hToolStripMenuItem.Text = "1h";
   this.hToolStripMenuItem.Click += new System.EventHandler(this.hToolStripMenuItem_Click);
   // 
   // timer_RefreshCurrentApplications
   // 
   this.timer_RefreshCurrentApplications.Enabled = true;
   this.timer_RefreshCurrentApplications.Interval = 5125;
   this.timer_RefreshCurrentApplications.Tick += new System.EventHandler(this.timer_RefreshCurrentApplications_Tick);
   // 
   // niMainApp
   // 
   this.niMainApp.ContextMenuStrip = this.cmsNotifyStrip;
   this.niMainApp.Icon = ((System.Drawing.Icon)(resources.GetObject("niMainApp.Icon")));
   this.niMainApp.Text = "Time Tracker";
   this.niMainApp.Visible = true;
   this.niMainApp.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.niMainApp_MouseDoubleClick);
   // 
   // cmsNotifyStrip
   // 
   this.cmsNotifyStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem});
   this.cmsNotifyStrip.Name = "cmsNotifyStrip";
   this.cmsNotifyStrip.ShowImageMargin = false;
   this.cmsNotifyStrip.Size = new System.Drawing.Size(76, 26);
   // 
   // closeToolStripMenuItem
   // 
   this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
   this.closeToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
   this.closeToolStripMenuItem.Text = "Close";
   this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
   // 
   // timer_validate
   // 
   this.timer_validate.Enabled = true;
   this.timer_validate.Interval = 300000;
   this.timer_validate.Tick += new System.EventHandler(this.timer_validate_Tick);
   // 
   // statusStrip1
   // 
   this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.tsslRefreshCount,
            this.toolStripStatusLabel2,
            this.tsslValidateRefresh,
            this.toolStripStatusLabel3,
            this.tsslInvalidProcessQueue});
   this.statusStrip1.Location = new System.Drawing.Point(0, 251);
   this.statusStrip1.Name = "statusStrip1";
   this.statusStrip1.Size = new System.Drawing.Size(537, 22);
   this.statusStrip1.TabIndex = 2;
   this.statusStrip1.Text = "statusStrip1";
   // 
   // toolStripStatusLabel1
   // 
   this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
   this.toolStripStatusLabel1.Size = new System.Drawing.Size(49, 17);
   this.toolStripStatusLabel1.Text = "Refresh:";
   // 
   // tsslRefreshCount
   // 
   this.tsslRefreshCount.Name = "tsslRefreshCount";
   this.tsslRefreshCount.Size = new System.Drawing.Size(70, 17);
   this.tsslRefreshCount.Text = "Refresh Time";
   // 
   // toolStripStatusLabel2
   // 
   this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
   this.toolStripStatusLabel2.Size = new System.Drawing.Size(49, 17);
   this.toolStripStatusLabel2.Text = "Validate:";
   // 
   // tsslValidateRefresh
   // 
   this.tsslValidateRefresh.Name = "tsslValidateRefresh";
   this.tsslValidateRefresh.Size = new System.Drawing.Size(70, 17);
   this.tsslValidateRefresh.Text = "Validate Time";
   // 
   // toolStripStatusLabel3
   // 
   this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
   this.toolStripStatusLabel3.Size = new System.Drawing.Size(118, 17);
   this.toolStripStatusLabel3.Text = "Invalid Process Queue:";
   // 
   // tsslInvalidProcessQueue
   // 
   this.tsslInvalidProcessQueue.Name = "tsslInvalidProcessQueue";
   this.tsslInvalidProcessQueue.Size = new System.Drawing.Size(13, 17);
   this.tsslInvalidProcessQueue.Text = "0";
   // 
   // timer_updateTimers
   // 
   this.timer_updateTimers.Enabled = true;
   this.timer_updateTimers.Interval = 1000;
   this.timer_updateTimers.Tick += new System.EventHandler(this.timer_updateTimers_Tick);
   // 
   // timer_InvalidProcessorRefresh
   // 
   this.timer_InvalidProcessorRefresh.Enabled = true;
   this.timer_InvalidProcessorRefresh.Interval = 5000;
   this.timer_InvalidProcessorRefresh.Tick += new System.EventHandler(this.timer_InvalidProcessorRefresh_Tick);
   // 
   // sToolStripMenuItem4
   // 
   this.sToolStripMenuItem4.Name = "sToolStripMenuItem4";
   this.sToolStripMenuItem4.Size = new System.Drawing.Size(152, 22);
   this.sToolStripMenuItem4.Text = "1s";
   this.sToolStripMenuItem4.Click += new System.EventHandler(this.sToolStripMenuItem4_Click);
   // 
   // MainWindow
   // 
   this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
   this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
   this.ClientSize = new System.Drawing.Size(537, 273);
   this.Controls.Add(this.statusStrip1);
   this.Controls.Add(this.lvTrackApp);
   this.Controls.Add(this.menuStrip1);
   this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
   this.MainMenuStrip = this.menuStrip1;
   this.Name = "MainWindow";
   this.Text = "Time Tracker";
   this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
   this.Load += new System.EventHandler(this.MainWindow_Load);
   this.menuStrip1.ResumeLayout(false);
   this.menuStrip1.PerformLayout();
   this.cmsNotifyStrip.ResumeLayout(false);
   this.statusStrip1.ResumeLayout(false);
   this.statusStrip1.PerformLayout();
   this.ResumeLayout(false);
   this.PerformLayout();

  }

  #endregion

  private System.Windows.Forms.ListView lvTrackApp;
  private System.Windows.Forms.MenuStrip menuStrip1;
  private System.Windows.Forms.ToolStripMenuItem applicationDatabaseToolStripMenuItem;
  private System.Windows.Forms.ColumnHeader chInternalId;
  private System.Windows.Forms.ColumnHeader chApplicationName;
  private System.Windows.Forms.ColumnHeader chTime;
  private System.Windows.Forms.ColumnHeader chFullTime;
  private System.Windows.Forms.Timer timer_RefreshCurrentApplications;
  private System.Windows.Forms.NotifyIcon niMainApp;
  private System.Windows.Forms.ContextMenuStrip cmsNotifyStrip;
  private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
  private System.Windows.Forms.ToolStripMenuItem trackedApplicationDatabaseToolStripMenuItem;
  private System.Windows.Forms.ToolStripMenuItem validateProcessToolStripMenuItem;
  private System.Windows.Forms.Timer timer_validate;
  private System.Windows.Forms.StatusStrip statusStrip1;
  private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
  private System.Windows.Forms.ToolStripStatusLabel tsslRefreshCount;
  private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
  private System.Windows.Forms.ToolStripStatusLabel tsslValidateRefresh;
  private System.Windows.Forms.Timer timer_updateTimers;
  private System.Windows.Forms.ToolStripMenuItem refreshRateToolStripMenuItem;
  private System.Windows.Forms.ToolStripMenuItem listRefreshToolStripMenuItem;
  private System.Windows.Forms.ToolStripMenuItem validateRefreshToolStripMenuItem;
  private System.Windows.Forms.ToolStripMenuItem sToolStripMenuItem;
  private System.Windows.Forms.ToolStripMenuItem sToolStripMenuItem1;
  private System.Windows.Forms.ToolStripMenuItem sToolStripMenuItem2;
  private System.Windows.Forms.ToolStripMenuItem sToolStripMenuItem3;
  private System.Windows.Forms.ToolStripMenuItem mToolStripMenuItem;
  private System.Windows.Forms.ToolStripMenuItem mToolStripMenuItem1;
  private System.Windows.Forms.ToolStripMenuItem mToolStripMenuItem2;
  private System.Windows.Forms.ToolStripMenuItem mToolStripMenuItem3;
  private System.Windows.Forms.ToolStripMenuItem mToolStripMenuItem4;
  private System.Windows.Forms.ToolStripMenuItem mToolStripMenuItem5;
  private System.Windows.Forms.ToolStripMenuItem mToolStripMenuItem6;
  private System.Windows.Forms.ToolStripMenuItem mToolStripMenuItem7;
  private System.Windows.Forms.ToolStripMenuItem mToolStripMenuItem8;
  private System.Windows.Forms.ToolStripMenuItem mToolStripMenuItem9;
  private System.Windows.Forms.ToolStripMenuItem mToolStripMenuItem10;
  private System.Windows.Forms.ToolStripMenuItem hToolStripMenuItem;
  private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
  private System.Windows.Forms.ToolStripStatusLabel tsslInvalidProcessQueue;
  private System.Windows.Forms.Timer timer_InvalidProcessorRefresh;
  private System.Windows.Forms.ToolStripMenuItem sToolStripMenuItem4;
 }
}

