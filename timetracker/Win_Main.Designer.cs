namespace timetracker
{
 partial class Win_Main
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
   System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Win_Main));
   this.menuStrip1 = new System.Windows.Forms.MenuStrip();
   this.applicationDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
   this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
   this.trackedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
   this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
   this.refreshIntervalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
   this.sToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
   this.sToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
   this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
   this.sToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
   this.sToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
   this.mToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
   this.mToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
   this.mToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
   this.mToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
   this.validateIntervalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
   this.mToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
   this.mToolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
   this.mToolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
   this.mToolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
   this.hToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
   this.exitApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
   this.lvTrackedApps = new System.Windows.Forms.ListView();
   this.chPID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
   this.chNameOfApp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
   this.chCurrentSession = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
   this.chAllTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
   this.menuStrip1.SuspendLayout();
   this.SuspendLayout();
   // 
   // menuStrip1
   // 
   this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.applicationDatabaseToolStripMenuItem,
            this.refreshToolStripMenuItem,
            this.exitApplicationToolStripMenuItem});
   this.menuStrip1.Location = new System.Drawing.Point(0, 0);
   this.menuStrip1.Name = "menuStrip1";
   this.menuStrip1.Size = new System.Drawing.Size(477, 24);
   this.menuStrip1.TabIndex = 0;
   this.menuStrip1.Text = "menuStrip1";
   // 
   // applicationDatabaseToolStripMenuItem
   // 
   this.applicationDatabaseToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
   this.applicationDatabaseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.trackedToolStripMenuItem});
   this.applicationDatabaseToolStripMenuItem.Name = "applicationDatabaseToolStripMenuItem";
   this.applicationDatabaseToolStripMenuItem.Size = new System.Drawing.Size(120, 20);
   this.applicationDatabaseToolStripMenuItem.Text = "Application Database";
   // 
   // showToolStripMenuItem
   // 
   this.showToolStripMenuItem.Name = "showToolStripMenuItem";
   this.showToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
   this.showToolStripMenuItem.Text = "Show";
   this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
   // 
   // trackedToolStripMenuItem
   // 
   this.trackedToolStripMenuItem.Name = "trackedToolStripMenuItem";
   this.trackedToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
   this.trackedToolStripMenuItem.Text = "Tracked";
   this.trackedToolStripMenuItem.Click += new System.EventHandler(this.trackedToolStripMenuItem_Click);
   // 
   // refreshToolStripMenuItem
   // 
   this.refreshToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshIntervalToolStripMenuItem,
            this.validateIntervalToolStripMenuItem});
   this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
   this.refreshToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
   this.refreshToolStripMenuItem.Text = "Refresh";
   // 
   // refreshIntervalToolStripMenuItem
   // 
   this.refreshIntervalToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sToolStripMenuItem,
            this.sToolStripMenuItem1,
            this.toolStripMenuItem2,
            this.sToolStripMenuItem2,
            this.sToolStripMenuItem3,
            this.mToolStripMenuItem,
            this.mToolStripMenuItem1,
            this.mToolStripMenuItem2,
            this.mToolStripMenuItem3});
   this.refreshIntervalToolStripMenuItem.Name = "refreshIntervalToolStripMenuItem";
   this.refreshIntervalToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
   this.refreshIntervalToolStripMenuItem.Text = "Refresh Interval";
   // 
   // sToolStripMenuItem
   // 
   this.sToolStripMenuItem.Name = "sToolStripMenuItem";
   this.sToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
   this.sToolStripMenuItem.Text = "1s";
   // 
   // sToolStripMenuItem1
   // 
   this.sToolStripMenuItem1.Name = "sToolStripMenuItem1";
   this.sToolStripMenuItem1.Size = new System.Drawing.Size(94, 22);
   this.sToolStripMenuItem1.Text = "5s";
   // 
   // toolStripMenuItem2
   // 
   this.toolStripMenuItem2.Name = "toolStripMenuItem2";
   this.toolStripMenuItem2.Size = new System.Drawing.Size(94, 22);
   this.toolStripMenuItem2.Text = "10s";
   // 
   // sToolStripMenuItem2
   // 
   this.sToolStripMenuItem2.Name = "sToolStripMenuItem2";
   this.sToolStripMenuItem2.Size = new System.Drawing.Size(94, 22);
   this.sToolStripMenuItem2.Text = "15s";
   // 
   // sToolStripMenuItem3
   // 
   this.sToolStripMenuItem3.Name = "sToolStripMenuItem3";
   this.sToolStripMenuItem3.Size = new System.Drawing.Size(94, 22);
   this.sToolStripMenuItem3.Text = "30s";
   // 
   // mToolStripMenuItem
   // 
   this.mToolStripMenuItem.Name = "mToolStripMenuItem";
   this.mToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
   this.mToolStripMenuItem.Text = "1m";
   // 
   // mToolStripMenuItem1
   // 
   this.mToolStripMenuItem1.Name = "mToolStripMenuItem1";
   this.mToolStripMenuItem1.Size = new System.Drawing.Size(94, 22);
   this.mToolStripMenuItem1.Text = "5m";
   // 
   // mToolStripMenuItem2
   // 
   this.mToolStripMenuItem2.Name = "mToolStripMenuItem2";
   this.mToolStripMenuItem2.Size = new System.Drawing.Size(94, 22);
   this.mToolStripMenuItem2.Text = "10m";
   // 
   // mToolStripMenuItem3
   // 
   this.mToolStripMenuItem3.Name = "mToolStripMenuItem3";
   this.mToolStripMenuItem3.Size = new System.Drawing.Size(94, 22);
   this.mToolStripMenuItem3.Text = "15m";
   // 
   // validateIntervalToolStripMenuItem
   // 
   this.validateIntervalToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mToolStripMenuItem4,
            this.mToolStripMenuItem5,
            this.mToolStripMenuItem6,
            this.mToolStripMenuItem7,
            this.hToolStripMenuItem});
   this.validateIntervalToolStripMenuItem.Name = "validateIntervalToolStripMenuItem";
   this.validateIntervalToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
   this.validateIntervalToolStripMenuItem.Text = "Validate Interval";
   // 
   // mToolStripMenuItem4
   // 
   this.mToolStripMenuItem4.Name = "mToolStripMenuItem4";
   this.mToolStripMenuItem4.Size = new System.Drawing.Size(94, 22);
   this.mToolStripMenuItem4.Text = "1m";
   // 
   // mToolStripMenuItem5
   // 
   this.mToolStripMenuItem5.Name = "mToolStripMenuItem5";
   this.mToolStripMenuItem5.Size = new System.Drawing.Size(94, 22);
   this.mToolStripMenuItem5.Text = "5m";
   // 
   // mToolStripMenuItem6
   // 
   this.mToolStripMenuItem6.Name = "mToolStripMenuItem6";
   this.mToolStripMenuItem6.Size = new System.Drawing.Size(94, 22);
   this.mToolStripMenuItem6.Text = "15m";
   // 
   // mToolStripMenuItem7
   // 
   this.mToolStripMenuItem7.Name = "mToolStripMenuItem7";
   this.mToolStripMenuItem7.Size = new System.Drawing.Size(94, 22);
   this.mToolStripMenuItem7.Text = "30m";
   // 
   // hToolStripMenuItem
   // 
   this.hToolStripMenuItem.Name = "hToolStripMenuItem";
   this.hToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
   this.hToolStripMenuItem.Text = "1h";
   // 
   // exitApplicationToolStripMenuItem
   // 
   this.exitApplicationToolStripMenuItem.Name = "exitApplicationToolStripMenuItem";
   this.exitApplicationToolStripMenuItem.Size = new System.Drawing.Size(92, 20);
   this.exitApplicationToolStripMenuItem.Text = "Exit Application";
   this.exitApplicationToolStripMenuItem.Click += new System.EventHandler(this.exitApplicationToolStripMenuItem_Click);
   // 
   // lvTrackedApps
   // 
   this.lvTrackedApps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
   this.lvTrackedApps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chPID,
            this.chNameOfApp,
            this.chCurrentSession,
            this.chAllTime});
   this.lvTrackedApps.FullRowSelect = true;
   this.lvTrackedApps.GridLines = true;
   this.lvTrackedApps.Location = new System.Drawing.Point(12, 27);
   this.lvTrackedApps.Name = "lvTrackedApps";
   this.lvTrackedApps.Size = new System.Drawing.Size(453, 230);
   this.lvTrackedApps.TabIndex = 1;
   this.lvTrackedApps.UseCompatibleStateImageBehavior = false;
   this.lvTrackedApps.View = System.Windows.Forms.View.Details;
   // 
   // chPID
   // 
   this.chPID.Text = "PID";
   this.chPID.Width = 42;
   // 
   // chNameOfApp
   // 
   this.chNameOfApp.Text = "Name of App";
   this.chNameOfApp.Width = 172;
   // 
   // chCurrentSession
   // 
   this.chCurrentSession.Text = "Current Session Time";
   this.chCurrentSession.Width = 119;
   // 
   // chAllTime
   // 
   this.chAllTime.Text = "All Time";
   this.chAllTime.Width = 87;
   // 
   // Win_Main
   // 
   this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
   this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
   this.ClientSize = new System.Drawing.Size(477, 269);
   this.Controls.Add(this.lvTrackedApps);
   this.Controls.Add(this.menuStrip1);
   this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
   this.MainMenuStrip = this.menuStrip1;
   this.Name = "Win_Main";
   this.Text = "Time Tracker";
   this.menuStrip1.ResumeLayout(false);
   this.menuStrip1.PerformLayout();
   this.ResumeLayout(false);
   this.PerformLayout();

  }

  #endregion

  private System.Windows.Forms.MenuStrip menuStrip1;
  private System.Windows.Forms.ToolStripMenuItem applicationDatabaseToolStripMenuItem;
  private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
  private System.Windows.Forms.ToolStripMenuItem trackedToolStripMenuItem;
  private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
  private System.Windows.Forms.ToolStripMenuItem refreshIntervalToolStripMenuItem;
  private System.Windows.Forms.ToolStripMenuItem sToolStripMenuItem;
  private System.Windows.Forms.ToolStripMenuItem sToolStripMenuItem1;
  private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
  private System.Windows.Forms.ToolStripMenuItem sToolStripMenuItem2;
  private System.Windows.Forms.ToolStripMenuItem sToolStripMenuItem3;
  private System.Windows.Forms.ToolStripMenuItem mToolStripMenuItem;
  private System.Windows.Forms.ToolStripMenuItem mToolStripMenuItem1;
  private System.Windows.Forms.ToolStripMenuItem mToolStripMenuItem2;
  private System.Windows.Forms.ToolStripMenuItem mToolStripMenuItem3;
  private System.Windows.Forms.ToolStripMenuItem validateIntervalToolStripMenuItem;
  private System.Windows.Forms.ToolStripMenuItem mToolStripMenuItem4;
  private System.Windows.Forms.ToolStripMenuItem mToolStripMenuItem5;
  private System.Windows.Forms.ToolStripMenuItem mToolStripMenuItem6;
  private System.Windows.Forms.ToolStripMenuItem mToolStripMenuItem7;
  private System.Windows.Forms.ToolStripMenuItem hToolStripMenuItem;
  private System.Windows.Forms.ToolStripMenuItem exitApplicationToolStripMenuItem;
  private System.Windows.Forms.ListView lvTrackedApps;
  private System.Windows.Forms.ColumnHeader chPID;
  private System.Windows.Forms.ColumnHeader chNameOfApp;
  private System.Windows.Forms.ColumnHeader chCurrentSession;
  private System.Windows.Forms.ColumnHeader chAllTime;

 }
}