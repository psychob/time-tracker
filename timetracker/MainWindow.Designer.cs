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
			this.cmsNotify = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.cmsNotify_Open = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
			this.toolStripProgressBar2 = new System.Windows.Forms.ToolStripProgressBar();
			this.toolStripProgressBar3 = new System.Windows.Forms.ToolStripProgressBar();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.databaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.definitionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.definitionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.listToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.trackedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.lvTrackedApps = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.saveDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cmsNotify.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmsNotify
			// 
			this.cmsNotify.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsNotify_Open,
            this.toolStripMenuItem1,
            this.closeToolStripMenuItem});
			this.cmsNotify.Name = "contextMenuStrip1";
			this.cmsNotify.Size = new System.Drawing.Size(101, 54);
			this.cmsNotify.Opening += new System.ComponentModel.CancelEventHandler(this.cmsNotify_Opening);
			// 
			// cmsNotify_Open
			// 
			this.cmsNotify_Open.Name = "cmsNotify_Open";
			this.cmsNotify_Open.Size = new System.Drawing.Size(100, 22);
			this.cmsNotify_Open.Text = "Open";
			this.cmsNotify_Open.Click += new System.EventHandler(this.cmsNotify_Open_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(97, 6);
			// 
			// closeToolStripMenuItem
			// 
			this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			this.closeToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
			this.closeToolStripMenuItem.Text = "Close";
			this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripProgressBar2,
            this.toolStripProgressBar3});
			this.statusStrip1.Location = new System.Drawing.Point(0, 283);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(530, 22);
			this.statusStrip1.TabIndex = 1;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripProgressBar1
			// 
			this.toolStripProgressBar1.Name = "toolStripProgressBar1";
			this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
			// 
			// toolStripProgressBar2
			// 
			this.toolStripProgressBar2.Name = "toolStripProgressBar2";
			this.toolStripProgressBar2.Size = new System.Drawing.Size(100, 16);
			// 
			// toolStripProgressBar3
			// 
			this.toolStripProgressBar3.Name = "toolStripProgressBar3";
			this.toolStripProgressBar3.Size = new System.Drawing.Size(100, 16);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.databaseToolStripMenuItem,
            this.exitApplicationToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(530, 24);
			this.menuStrip1.TabIndex = 2;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// databaseToolStripMenuItem
			// 
			this.databaseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.definitionsToolStripMenuItem,
            this.trackedToolStripMenuItem});
			this.databaseToolStripMenuItem.Name = "databaseToolStripMenuItem";
			this.databaseToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
			this.databaseToolStripMenuItem.Text = "Database";
			// 
			// definitionsToolStripMenuItem
			// 
			this.definitionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.listToolStripMenuItem,
            this.toolStripMenuItem2,
            this.saveDatabaseToolStripMenuItem});
			this.definitionsToolStripMenuItem.Name = "definitionsToolStripMenuItem";
			this.definitionsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.definitionsToolStripMenuItem.Text = "Definitions";
			// 
			// addToolStripMenuItem
			// 
			this.addToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fromFileToolStripMenuItem,
            this.toolStripMenuItem3,
            this.definitionToolStripMenuItem});
			this.addToolStripMenuItem.Name = "addToolStripMenuItem";
			this.addToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.addToolStripMenuItem.Text = "Add";
			// 
			// fromFileToolStripMenuItem
			// 
			this.fromFileToolStripMenuItem.Name = "fromFileToolStripMenuItem";
			this.fromFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.fromFileToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			this.fromFileToolStripMenuItem.Text = "From File...";
			this.fromFileToolStripMenuItem.Click += new System.EventHandler(this.fromFileToolStripMenuItem_Click);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(166, 6);
			// 
			// definitionToolStripMenuItem
			// 
			this.definitionToolStripMenuItem.Name = "definitionToolStripMenuItem";
			this.definitionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.definitionToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			this.definitionToolStripMenuItem.Text = "Definition";
			this.definitionToolStripMenuItem.Click += new System.EventHandler(this.definitionToolStripMenuItem_Click);
			// 
			// listToolStripMenuItem
			// 
			this.listToolStripMenuItem.Name = "listToolStripMenuItem";
			this.listToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
			this.listToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.listToolStripMenuItem.Text = "List";
			this.listToolStripMenuItem.Click += new System.EventHandler(this.listToolStripMenuItem_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(149, 6);
			// 
			// trackedToolStripMenuItem
			// 
			this.trackedToolStripMenuItem.Name = "trackedToolStripMenuItem";
			this.trackedToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.trackedToolStripMenuItem.Text = "Tracked";
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
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
			this.lvTrackedApps.FullRowSelect = true;
			this.lvTrackedApps.GridLines = true;
			this.lvTrackedApps.Location = new System.Drawing.Point(12, 27);
			this.lvTrackedApps.Name = "lvTrackedApps";
			this.lvTrackedApps.Size = new System.Drawing.Size(506, 253);
			this.lvTrackedApps.TabIndex = 3;
			this.lvTrackedApps.UseCompatibleStateImageBehavior = false;
			this.lvTrackedApps.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "PID";
			this.columnHeader1.Width = 37;
			// 
			// columnHeader2
			// 
			this.columnHeader2.DisplayIndex = 2;
			this.columnHeader2.Text = "Application Name";
			this.columnHeader2.Width = 209;
			// 
			// columnHeader3
			// 
			this.columnHeader3.DisplayIndex = 3;
			this.columnHeader3.Text = "Application Time";
			this.columnHeader3.Width = 97;
			// 
			// columnHeader4
			// 
			this.columnHeader4.DisplayIndex = 4;
			this.columnHeader4.Text = "Application All Time";
			this.columnHeader4.Width = 112;
			// 
			// columnHeader5
			// 
			this.columnHeader5.DisplayIndex = 1;
			this.columnHeader5.Text = "Started";
			this.columnHeader5.Width = 46;
			// 
			// saveDatabaseToolStripMenuItem
			// 
			this.saveDatabaseToolStripMenuItem.Name = "saveDatabaseToolStripMenuItem";
			this.saveDatabaseToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.saveDatabaseToolStripMenuItem.Text = "Save database";
			this.saveDatabaseToolStripMenuItem.Click += new System.EventHandler(this.saveDatabaseToolStripMenuItem_Click);
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(530, 305);
			this.Controls.Add(this.lvTrackedApps);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainWindow";
			this.Text = "MainWindow";
			this.Load += new System.EventHandler(this.MainWindow_Load);
			this.Resize += new System.EventHandler(this.MainWindow_Resize);
			this.cmsNotify.ResumeLayout(false);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ContextMenuStrip cmsNotify;
		private System.Windows.Forms.ToolStripMenuItem cmsNotify_Open;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ListView lvTrackedApps;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ToolStripMenuItem databaseToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem definitionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem listToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem trackedToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitApplicationToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fromFileToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem definitionToolStripMenuItem;
		private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
		private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar2;
		private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ToolStripMenuItem saveDatabaseToolStripMenuItem;
	}
}