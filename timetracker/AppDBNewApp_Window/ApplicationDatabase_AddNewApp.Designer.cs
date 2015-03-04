namespace timetracker.AppDBNewApp_Window
{
 partial class ApplicationDatabase_AddNewApp
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
   this.tbNameOfApplication = new System.Windows.Forms.TextBox();
   this.tbInternalNameOfApplication = new System.Windows.Forms.TextBox();
   this.cbCompareWith = new System.Windows.Forms.ComboBox();
   this.cbCompareUsingMethod = new System.Windows.Forms.ComboBox();
   this.tbTextToCompare = new System.Windows.Forms.TextBox();
   this.tbTestingString = new System.Windows.Forms.TextBox();
   this.lbAllRules = new System.Windows.Forms.ListBox();
   this.label1 = new System.Windows.Forms.Label();
   this.label2 = new System.Windows.Forms.Label();
   this.label3 = new System.Windows.Forms.Label();
   this.label4 = new System.Windows.Forms.Label();
   this.label5 = new System.Windows.Forms.Label();
   this.label6 = new System.Windows.Forms.Label();
   this.btnAddApplication = new System.Windows.Forms.Button();
   this.btnNewRule = new System.Windows.Forms.Button();
   this.btnRemoveRule = new System.Windows.Forms.Button();
   this.SuspendLayout();
   // 
   // tbNameOfApplication
   // 
   this.tbNameOfApplication.Location = new System.Drawing.Point(156, 12);
   this.tbNameOfApplication.Name = "tbNameOfApplication";
   this.tbNameOfApplication.Size = new System.Drawing.Size(262, 20);
   this.tbNameOfApplication.TabIndex = 0;
   this.tbNameOfApplication.TextChanged += new System.EventHandler(this.tbNameOfApplication_TextChanged);
   // 
   // tbInternalNameOfApplication
   // 
   this.tbInternalNameOfApplication.Location = new System.Drawing.Point(156, 39);
   this.tbInternalNameOfApplication.Name = "tbInternalNameOfApplication";
   this.tbInternalNameOfApplication.Size = new System.Drawing.Size(262, 20);
   this.tbInternalNameOfApplication.TabIndex = 1;
   // 
   // cbCompareWith
   // 
   this.cbCompareWith.FormattingEnabled = true;
   this.cbCompareWith.Items.AddRange(new object[] {
            "File Name",
            "File Path"});
   this.cbCompareWith.Location = new System.Drawing.Point(156, 66);
   this.cbCompareWith.Name = "cbCompareWith";
   this.cbCompareWith.Size = new System.Drawing.Size(262, 21);
   this.cbCompareWith.TabIndex = 2;
   // 
   // cbCompareUsingMethod
   // 
   this.cbCompareUsingMethod.FormattingEnabled = true;
   this.cbCompareUsingMethod.Items.AddRange(new object[] {
            "Exact Case Sensitive",
            "Exact Case Insensitive",
            "Near Case Sensitive",
            "Near Case Insensitive",
            "Regular Expression"});
   this.cbCompareUsingMethod.Location = new System.Drawing.Point(156, 94);
   this.cbCompareUsingMethod.Name = "cbCompareUsingMethod";
   this.cbCompareUsingMethod.Size = new System.Drawing.Size(262, 21);
   this.cbCompareUsingMethod.TabIndex = 3;
   // 
   // tbTextToCompare
   // 
   this.tbTextToCompare.Location = new System.Drawing.Point(156, 121);
   this.tbTextToCompare.Name = "tbTextToCompare";
   this.tbTextToCompare.Size = new System.Drawing.Size(262, 20);
   this.tbTextToCompare.TabIndex = 4;
   // 
   // tbTestingString
   // 
   this.tbTestingString.Location = new System.Drawing.Point(156, 148);
   this.tbTestingString.Name = "tbTestingString";
   this.tbTestingString.Size = new System.Drawing.Size(262, 20);
   this.tbTestingString.TabIndex = 5;
   // 
   // lbAllRules
   // 
   this.lbAllRules.FormattingEnabled = true;
   this.lbAllRules.Location = new System.Drawing.Point(12, 178);
   this.lbAllRules.Name = "lbAllRules";
   this.lbAllRules.Size = new System.Drawing.Size(406, 147);
   this.lbAllRules.TabIndex = 6;
   // 
   // label1
   // 
   this.label1.AutoSize = true;
   this.label1.Location = new System.Drawing.Point(12, 15);
   this.label1.Name = "label1";
   this.label1.Size = new System.Drawing.Size(105, 13);
   this.label1.TabIndex = 7;
   this.label1.Text = "Name of Application:";
   // 
   // label2
   // 
   this.label2.AutoSize = true;
   this.label2.Location = new System.Drawing.Point(12, 42);
   this.label2.Name = "label2";
   this.label2.Size = new System.Drawing.Size(143, 13);
   this.label2.TabIndex = 8;
   this.label2.Text = "Internal Name of Application:";
   // 
   // label3
   // 
   this.label3.AutoSize = true;
   this.label3.Location = new System.Drawing.Point(12, 69);
   this.label3.Name = "label3";
   this.label3.Size = new System.Drawing.Size(77, 13);
   this.label3.TabIndex = 9;
   this.label3.Text = "Compare With:";
   // 
   // label4
   // 
   this.label4.AutoSize = true;
   this.label4.Location = new System.Drawing.Point(12, 97);
   this.label4.Name = "label4";
   this.label4.Size = new System.Drawing.Size(121, 13);
   this.label4.TabIndex = 10;
   this.label4.Text = "Compare Using Method:";
   // 
   // label5
   // 
   this.label5.AutoSize = true;
   this.label5.Location = new System.Drawing.Point(12, 124);
   this.label5.Name = "label5";
   this.label5.Size = new System.Drawing.Size(99, 13);
   this.label5.TabIndex = 11;
   this.label5.Text = "Text to compare to:";
   // 
   // label6
   // 
   this.label6.AutoSize = true;
   this.label6.Location = new System.Drawing.Point(12, 151);
   this.label6.Name = "label6";
   this.label6.Size = new System.Drawing.Size(75, 13);
   this.label6.TabIndex = 12;
   this.label6.Text = "Testing String:";
   // 
   // btnAddApplication
   // 
   this.btnAddApplication.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
   this.btnAddApplication.Location = new System.Drawing.Point(267, 331);
   this.btnAddApplication.Name = "btnAddApplication";
   this.btnAddApplication.Size = new System.Drawing.Size(151, 52);
   this.btnAddApplication.TabIndex = 13;
   this.btnAddApplication.Text = "Add New Application";
   this.btnAddApplication.UseVisualStyleBackColor = true;
   this.btnAddApplication.Click += new System.EventHandler(this.btnAddApplication_Click);
   // 
   // btnNewRule
   // 
   this.btnNewRule.Location = new System.Drawing.Point(12, 331);
   this.btnNewRule.Name = "btnNewRule";
   this.btnNewRule.Size = new System.Drawing.Size(131, 23);
   this.btnNewRule.TabIndex = 14;
   this.btnNewRule.Text = "Add New Rule";
   this.btnNewRule.UseVisualStyleBackColor = true;
   this.btnNewRule.Click += new System.EventHandler(this.btnNewRule_Click);
   // 
   // btnRemoveRule
   // 
   this.btnRemoveRule.Location = new System.Drawing.Point(12, 360);
   this.btnRemoveRule.Name = "btnRemoveRule";
   this.btnRemoveRule.Size = new System.Drawing.Size(131, 23);
   this.btnRemoveRule.TabIndex = 15;
   this.btnRemoveRule.Text = "Remove Rule";
   this.btnRemoveRule.UseVisualStyleBackColor = true;
   // 
   // ApplicationDatabase_AddNewApp
   // 
   this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
   this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
   this.ClientSize = new System.Drawing.Size(430, 391);
   this.Controls.Add(this.btnRemoveRule);
   this.Controls.Add(this.btnNewRule);
   this.Controls.Add(this.btnAddApplication);
   this.Controls.Add(this.label6);
   this.Controls.Add(this.label5);
   this.Controls.Add(this.label4);
   this.Controls.Add(this.label3);
   this.Controls.Add(this.label2);
   this.Controls.Add(this.label1);
   this.Controls.Add(this.lbAllRules);
   this.Controls.Add(this.tbTestingString);
   this.Controls.Add(this.tbTextToCompare);
   this.Controls.Add(this.cbCompareUsingMethod);
   this.Controls.Add(this.cbCompareWith);
   this.Controls.Add(this.tbInternalNameOfApplication);
   this.Controls.Add(this.tbNameOfApplication);
   this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
   this.MaximizeBox = false;
   this.MinimizeBox = false;
   this.Name = "ApplicationDatabase_AddNewApp";
   this.Text = "ApplicationDatabase_AddNewApp";
   this.Load += new System.EventHandler(this.ApplicationDatabase_AddNewApp_Load);
   this.ResumeLayout(false);
   this.PerformLayout();

  }

  #endregion

  private System.Windows.Forms.TextBox tbNameOfApplication;
  private System.Windows.Forms.TextBox tbInternalNameOfApplication;
  private System.Windows.Forms.ComboBox cbCompareWith;
  private System.Windows.Forms.ComboBox cbCompareUsingMethod;
  private System.Windows.Forms.TextBox tbTextToCompare;
  private System.Windows.Forms.TextBox tbTestingString;
  private System.Windows.Forms.ListBox lbAllRules;
  private System.Windows.Forms.Label label1;
  private System.Windows.Forms.Label label2;
  private System.Windows.Forms.Label label3;
  private System.Windows.Forms.Label label4;
  private System.Windows.Forms.Label label5;
  private System.Windows.Forms.Label label6;
  private System.Windows.Forms.Button btnAddApplication;
  private System.Windows.Forms.Button btnNewRule;
  private System.Windows.Forms.Button btnRemoveRule;
 }
}