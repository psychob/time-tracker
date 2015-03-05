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
   this.timer_RefreshCurrentApplications = new System.Windows.Forms.Timer(this.components);
   this.niMainApp = new System.Windows.Forms.NotifyIcon(this.components);
   this.cmsNotifyStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
   this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
   this.menuStrip1.SuspendLayout();
   this.cmsNotifyStrip.SuspendLayout();
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
   this.lvTrackApp.Size = new System.Drawing.Size(513, 234);
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
            this.trackedApplicationDatabaseToolStripMenuItem});
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
   // 
   // timer_RefreshCurrentApplications
   // 
   this.timer_RefreshCurrentApplications.Enabled = true;
   this.timer_RefreshCurrentApplications.Interval = 1750;
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
   this.cmsNotifyStrip.Size = new System.Drawing.Size(101, 26);
   // 
   // closeToolStripMenuItem
   // 
   this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
   this.closeToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
   this.closeToolStripMenuItem.Text = "Close";
   this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
   // 
   // MainWindow
   // 
   this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
   this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
   this.ClientSize = new System.Drawing.Size(537, 273);
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
 }
}

