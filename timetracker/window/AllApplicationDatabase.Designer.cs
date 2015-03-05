namespace timetracker.window
{
 partial class AllApplicationDatabase
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
   System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AllApplicationDatabase));
   this.lbAllAps = new System.Windows.Forms.ListBox();
   this.btnAdd = new System.Windows.Forms.Button();
   this.btnEdit = new System.Windows.Forms.Button();
   this.btnRemoveOne = new System.Windows.Forms.Button();
   this.btnRemoveAll = new System.Windows.Forms.Button();
   this.SuspendLayout();
   // 
   // lbAllAps
   // 
   this.lbAllAps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
   this.lbAllAps.FormattingEnabled = true;
   this.lbAllAps.Location = new System.Drawing.Point(12, 12);
   this.lbAllAps.Name = "lbAllAps";
   this.lbAllAps.Size = new System.Drawing.Size(366, 420);
   this.lbAllAps.TabIndex = 0;
   // 
   // btnAdd
   // 
   this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
   this.btnAdd.Location = new System.Drawing.Point(384, 12);
   this.btnAdd.Name = "btnAdd";
   this.btnAdd.Size = new System.Drawing.Size(75, 23);
   this.btnAdd.TabIndex = 1;
   this.btnAdd.Text = "Add";
   this.btnAdd.UseVisualStyleBackColor = true;
   this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
   // 
   // btnEdit
   // 
   this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
   this.btnEdit.Location = new System.Drawing.Point(384, 41);
   this.btnEdit.Name = "btnEdit";
   this.btnEdit.Size = new System.Drawing.Size(75, 23);
   this.btnEdit.TabIndex = 2;
   this.btnEdit.Text = "Edit";
   this.btnEdit.UseVisualStyleBackColor = true;
   // 
   // btnRemoveOne
   // 
   this.btnRemoveOne.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
   this.btnRemoveOne.Location = new System.Drawing.Point(384, 71);
   this.btnRemoveOne.Name = "btnRemoveOne";
   this.btnRemoveOne.Size = new System.Drawing.Size(75, 23);
   this.btnRemoveOne.TabIndex = 3;
   this.btnRemoveOne.Text = "Remove";
   this.btnRemoveOne.UseVisualStyleBackColor = true;
   // 
   // btnRemoveAll
   // 
   this.btnRemoveAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
   this.btnRemoveAll.Location = new System.Drawing.Point(384, 101);
   this.btnRemoveAll.Name = "btnRemoveAll";
   this.btnRemoveAll.Size = new System.Drawing.Size(75, 23);
   this.btnRemoveAll.TabIndex = 4;
   this.btnRemoveAll.Text = "Remove All";
   this.btnRemoveAll.UseVisualStyleBackColor = true;
   // 
   // AllApplicationDatabase
   // 
   this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
   this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
   this.ClientSize = new System.Drawing.Size(471, 448);
   this.Controls.Add(this.btnRemoveAll);
   this.Controls.Add(this.btnRemoveOne);
   this.Controls.Add(this.btnEdit);
   this.Controls.Add(this.btnAdd);
   this.Controls.Add(this.lbAllAps);
   this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
   this.MaximizeBox = false;
   this.MinimizeBox = false;
   this.Name = "AllApplicationDatabase";
   this.Text = "Application Database";
   this.Load += new System.EventHandler(this.AllApplicationDatabase_Load);
   this.ResumeLayout(false);

  }

  #endregion

  private System.Windows.Forms.ListBox lbAllAps;
  private System.Windows.Forms.Button btnAdd;
  private System.Windows.Forms.Button btnEdit;
  private System.Windows.Forms.Button btnRemoveOne;
  private System.Windows.Forms.Button btnRemoveAll;
 }
}