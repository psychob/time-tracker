namespace timetracker.AppDBTrackedApp_Window
{
 partial class ApplicationTrackedDatabase
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
   System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationTrackedDatabase));
   this.lvAllAps = new System.Windows.Forms.ListView();
   this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
   this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
   this.SuspendLayout();
   // 
   // lvAllAps
   // 
   this.lvAllAps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
   this.lvAllAps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
   this.lvAllAps.Location = new System.Drawing.Point(12, 12);
   this.lvAllAps.Name = "lvAllAps";
   this.lvAllAps.Size = new System.Drawing.Size(292, 369);
   this.lvAllAps.TabIndex = 0;
   this.lvAllAps.UseCompatibleStateImageBehavior = false;
   this.lvAllAps.View = System.Windows.Forms.View.Details;
   // 
   // columnHeader1
   // 
   this.columnHeader1.Text = "Name of Application";
   this.columnHeader1.Width = 200;
   // 
   // columnHeader2
   // 
   this.columnHeader2.Text = "Time";
   this.columnHeader2.Width = 75;
   // 
   // ApplicationTrackedDatabase
   // 
   this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
   this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
   this.ClientSize = new System.Drawing.Size(316, 393);
   this.Controls.Add(this.lvAllAps);
   this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
   this.MaximizeBox = false;
   this.MinimizeBox = false;
   this.Name = "ApplicationTrackedDatabase";
   this.Text = "ApplicationTrackedDatabase";
   this.ResumeLayout(false);

  }

  #endregion

  private System.Windows.Forms.ColumnHeader columnHeader1;
  private System.Windows.Forms.ColumnHeader columnHeader2;
  public System.Windows.Forms.ListView lvAllAps;
 }
}