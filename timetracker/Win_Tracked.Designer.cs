namespace timetracker
{
 partial class Win_Tracked
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
   System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Win_Tracked));
   this.listView1 = new System.Windows.Forms.ListView();
   this.chTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
   this.chAllTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
   this.SuspendLayout();
   // 
   // listView1
   // 
   this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
   this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chTitle,
            this.chAllTime});
   this.listView1.FullRowSelect = true;
   this.listView1.GridLines = true;
   this.listView1.Location = new System.Drawing.Point(12, 12);
   this.listView1.Name = "listView1";
   this.listView1.Size = new System.Drawing.Size(425, 297);
   this.listView1.TabIndex = 0;
   this.listView1.UseCompatibleStateImageBehavior = false;
   this.listView1.View = System.Windows.Forms.View.Details;
   // 
   // chTitle
   // 
   this.chTitle.Text = "Title";
   this.chTitle.Width = 195;
   // 
   // chAllTime
   // 
   this.chAllTime.Text = "All Time";
   // 
   // Win_Tracked
   // 
   this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
   this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
   this.ClientSize = new System.Drawing.Size(449, 321);
   this.Controls.Add(this.listView1);
   this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
   this.MaximizeBox = false;
   this.MinimizeBox = false;
   this.Name = "Win_Tracked";
   this.Text = "Tracked Apps";
   this.Load += new System.EventHandler(this.Win_Tracked_Load);
   this.ResumeLayout(false);

  }

  #endregion

  private System.Windows.Forms.ListView listView1;
  private System.Windows.Forms.ColumnHeader chTitle;
  private System.Windows.Forms.ColumnHeader chAllTime;
 }
}