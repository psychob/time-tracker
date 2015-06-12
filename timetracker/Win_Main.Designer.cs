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
   this.components = new System.ComponentModel.Container();
   System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Win_Main));
   this.menuStrip1 = new System.Windows.Forms.MenuStrip();
   this.applicationDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
   this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
   this.trackedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
   this.exitApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
   this.lvTrackedApps = new System.Windows.Forms.ListView();
   this.chPID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
   this.chNameOfApp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
   this.chCurrentSession = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
   this.chAllTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
   this.timer_fetch_current_tasks = new System.Windows.Forms.Timer(this.components);
   this.menuStrip1.SuspendLayout();
   this.SuspendLayout();
   // 
   // menuStrip1
   // 
   this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.applicationDatabaseToolStripMenuItem,
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
   this.showToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
   this.showToolStripMenuItem.Text = "Show";
   this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
   // 
   // trackedToolStripMenuItem
   // 
   this.trackedToolStripMenuItem.Name = "trackedToolStripMenuItem";
   this.trackedToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
   this.trackedToolStripMenuItem.Text = "Tracked";
   this.trackedToolStripMenuItem.Click += new System.EventHandler(this.trackedToolStripMenuItem_Click);
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
   this.chAllTime.Width = 113;
   // 
   // timer_fetch_current_tasks
   // 
   this.timer_fetch_current_tasks.Interval = 5000;
   this.timer_fetch_current_tasks.Tick += new System.EventHandler(this.timer_fetch_current_tasks_Tick);
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
   this.Load += new System.EventHandler(this.Win_Main_Load);
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
  private System.Windows.Forms.ToolStripMenuItem exitApplicationToolStripMenuItem;
  private System.Windows.Forms.ListView lvTrackedApps;
  private System.Windows.Forms.ColumnHeader chPID;
  private System.Windows.Forms.ColumnHeader chNameOfApp;
  private System.Windows.Forms.ColumnHeader chCurrentSession;
  private System.Windows.Forms.ColumnHeader chAllTime;
  private System.Windows.Forms.Timer timer_fetch_current_tasks;

 }
}