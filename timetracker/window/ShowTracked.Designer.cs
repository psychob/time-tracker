namespace timetracker.window
{
 partial class ShowTracked
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
   this.lvTrackedApps = new System.Windows.Forms.ListView();
   this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
   this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
   this.SuspendLayout();
   // 
   // lvTrackedApps
   // 
   this.lvTrackedApps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
   this.lvTrackedApps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
   this.lvTrackedApps.FullRowSelect = true;
   this.lvTrackedApps.GridLines = true;
   this.lvTrackedApps.Location = new System.Drawing.Point(12, 12);
   this.lvTrackedApps.Name = "lvTrackedApps";
   this.lvTrackedApps.Size = new System.Drawing.Size(375, 546);
   this.lvTrackedApps.TabIndex = 0;
   this.lvTrackedApps.UseCompatibleStateImageBehavior = false;
   this.lvTrackedApps.View = System.Windows.Forms.View.Details;
   // 
   // columnHeader1
   // 
   this.columnHeader1.Text = "Name";
   this.columnHeader1.Width = 250;
   // 
   // columnHeader2
   // 
   this.columnHeader2.Text = "Time";
   this.columnHeader2.Width = 170;
   // 
   // ShowTracked
   // 
   this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
   this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
   this.ClientSize = new System.Drawing.Size(399, 570);
   this.Controls.Add(this.lvTrackedApps);
   this.Name = "ShowTracked";
   this.Text = "ShowTracked";
   this.Load += new System.EventHandler(this.ShowTracked_Load);
   this.ResumeLayout(false);

  }

  #endregion

  private System.Windows.Forms.ListView lvTrackedApps;
  private System.Windows.Forms.ColumnHeader columnHeader1;
  private System.Windows.Forms.ColumnHeader columnHeader2;
 }
}