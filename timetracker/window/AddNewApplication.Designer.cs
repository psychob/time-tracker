namespace timetracker.window
{
 partial class AddNewApplication
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
   System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddNewApplication));
   this.tbApplicationTitle = new System.Windows.Forms.TextBox();
   this.tbInternalString = new System.Windows.Forms.TextBox();
   this.cbCompareWith = new System.Windows.Forms.ComboBox();
   this.cbCompareAlgorithm = new System.Windows.Forms.ComboBox();
   this.tbPattern = new System.Windows.Forms.TextBox();
   this.tbTestingString = new System.Windows.Forms.TextBox();
   this.btnNewRule = new System.Windows.Forms.Button();
   this.btnPollDataFromCurrentProcess = new System.Windows.Forms.Button();
   this.lbAllRules = new System.Windows.Forms.ListBox();
   this.btnAdd = new System.Windows.Forms.Button();
   this.btnDiscard = new System.Windows.Forms.Button();
   this.label1 = new System.Windows.Forms.Label();
   this.label2 = new System.Windows.Forms.Label();
   this.label3 = new System.Windows.Forms.Label();
   this.label4 = new System.Windows.Forms.Label();
   this.label5 = new System.Windows.Forms.Label();
   this.label6 = new System.Windows.Forms.Label();
   this.cbIsReqired = new System.Windows.Forms.CheckBox();
   this.SuspendLayout();
   // 
   // tbApplicationTitle
   // 
   this.tbApplicationTitle.Location = new System.Drawing.Point(199, 12);
   this.tbApplicationTitle.Name = "tbApplicationTitle";
   this.tbApplicationTitle.Size = new System.Drawing.Size(293, 20);
   this.tbApplicationTitle.TabIndex = 0;
   this.tbApplicationTitle.TextChanged += new System.EventHandler(this.tbApplicationTitle_TextChanged);
   // 
   // tbInternalString
   // 
   this.tbInternalString.Location = new System.Drawing.Point(199, 39);
   this.tbInternalString.Name = "tbInternalString";
   this.tbInternalString.Size = new System.Drawing.Size(292, 20);
   this.tbInternalString.TabIndex = 1;
   // 
   // cbCompareWith
   // 
   this.cbCompareWith.FormattingEnabled = true;
   this.cbCompareWith.Items.AddRange(new object[] {
            "File Name",
            "File Path",
            "File Version - Name",
            "File Version - Description",
            "File Version - Company",
            "File Version - Product Version",
            "File Version - File Version,"});
   this.cbCompareWith.Location = new System.Drawing.Point(199, 65);
   this.cbCompareWith.Name = "cbCompareWith";
   this.cbCompareWith.Size = new System.Drawing.Size(293, 21);
   this.cbCompareWith.TabIndex = 2;
   // 
   // cbCompareAlgorithm
   // 
   this.cbCompareAlgorithm.FormattingEnabled = true;
   this.cbCompareAlgorithm.Items.AddRange(new object[] {
            "Exact Case Sensitive",
            "Exact Case Insensitive",
            "Near Case Sensitive",
            "Near Case Insensitive",
            "Regular Expression"});
   this.cbCompareAlgorithm.Location = new System.Drawing.Point(199, 92);
   this.cbCompareAlgorithm.Name = "cbCompareAlgorithm";
   this.cbCompareAlgorithm.Size = new System.Drawing.Size(293, 21);
   this.cbCompareAlgorithm.TabIndex = 3;
   this.cbCompareAlgorithm.SelectedIndexChanged += new System.EventHandler(this.cbCompareAlgorithm_SelectedIndexChanged);
   // 
   // tbPattern
   // 
   this.tbPattern.Location = new System.Drawing.Point(199, 120);
   this.tbPattern.Name = "tbPattern";
   this.tbPattern.Size = new System.Drawing.Size(293, 20);
   this.tbPattern.TabIndex = 4;
   this.tbPattern.TextChanged += new System.EventHandler(this.tbPattern_TextChanged);
   // 
   // tbTestingString
   // 
   this.tbTestingString.Location = new System.Drawing.Point(199, 147);
   this.tbTestingString.Name = "tbTestingString";
   this.tbTestingString.Size = new System.Drawing.Size(291, 20);
   this.tbTestingString.TabIndex = 5;
   this.tbTestingString.TextChanged += new System.EventHandler(this.tbTestingString_TextChanged);
   // 
   // btnNewRule
   // 
   this.btnNewRule.Location = new System.Drawing.Point(393, 174);
   this.btnNewRule.Name = "btnNewRule";
   this.btnNewRule.Size = new System.Drawing.Size(96, 23);
   this.btnNewRule.TabIndex = 6;
   this.btnNewRule.Text = "Add New Rule";
   this.btnNewRule.UseVisualStyleBackColor = true;
   this.btnNewRule.Click += new System.EventHandler(this.btnNewRule_Click);
   // 
   // btnPollDataFromCurrentProcess
   // 
   this.btnPollDataFromCurrentProcess.Location = new System.Drawing.Point(12, 174);
   this.btnPollDataFromCurrentProcess.Name = "btnPollDataFromCurrentProcess";
   this.btnPollDataFromCurrentProcess.Size = new System.Drawing.Size(205, 23);
   this.btnPollDataFromCurrentProcess.TabIndex = 7;
   this.btnPollDataFromCurrentProcess.Text = "Poll Data From Current Process";
   this.btnPollDataFromCurrentProcess.UseVisualStyleBackColor = true;
   // 
   // lbAllRules
   // 
   this.lbAllRules.FormattingEnabled = true;
   this.lbAllRules.Location = new System.Drawing.Point(12, 203);
   this.lbAllRules.Name = "lbAllRules";
   this.lbAllRules.Size = new System.Drawing.Size(477, 186);
   this.lbAllRules.TabIndex = 8;
   // 
   // btnAdd
   // 
   this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
   this.btnAdd.Location = new System.Drawing.Point(245, 395);
   this.btnAdd.Name = "btnAdd";
   this.btnAdd.Size = new System.Drawing.Size(244, 23);
   this.btnAdd.TabIndex = 9;
   this.btnAdd.Text = "Add New Program Signature";
   this.btnAdd.UseVisualStyleBackColor = true;
   this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
   // 
   // btnDiscard
   // 
   this.btnDiscard.Location = new System.Drawing.Point(12, 395);
   this.btnDiscard.Name = "btnDiscard";
   this.btnDiscard.Size = new System.Drawing.Size(75, 23);
   this.btnDiscard.TabIndex = 10;
   this.btnDiscard.Text = "Discard";
   this.btnDiscard.UseVisualStyleBackColor = true;
   this.btnDiscard.Click += new System.EventHandler(this.btnDiscard_Click);
   // 
   // label1
   // 
   this.label1.AutoSize = true;
   this.label1.Location = new System.Drawing.Point(12, 15);
   this.label1.Name = "label1";
   this.label1.Size = new System.Drawing.Size(85, 13);
   this.label1.TabIndex = 11;
   this.label1.Text = "Application Title:";
   // 
   // label2
   // 
   this.label2.AutoSize = true;
   this.label2.Location = new System.Drawing.Point(12, 42);
   this.label2.Name = "label2";
   this.label2.Size = new System.Drawing.Size(123, 13);
   this.label2.TabIndex = 12;
   this.label2.Text = "Application Internal Title:";
   // 
   // label3
   // 
   this.label3.AutoSize = true;
   this.label3.Location = new System.Drawing.Point(12, 68);
   this.label3.Name = "label3";
   this.label3.Size = new System.Drawing.Size(77, 13);
   this.label3.TabIndex = 13;
   this.label3.Text = "Compare With:";
   // 
   // label4
   // 
   this.label4.AutoSize = true;
   this.label4.Location = new System.Drawing.Point(12, 95);
   this.label4.Name = "label4";
   this.label4.Size = new System.Drawing.Size(128, 13);
   this.label4.TabIndex = 14;
   this.label4.Text = "Compare Using Algorithm:";
   // 
   // label5
   // 
   this.label5.AutoSize = true;
   this.label5.Location = new System.Drawing.Point(12, 123);
   this.label5.Name = "label5";
   this.label5.Size = new System.Drawing.Size(70, 13);
   this.label5.TabIndex = 15;
   this.label5.Text = "Text/Pattern:";
   // 
   // label6
   // 
   this.label6.AutoSize = true;
   this.label6.Location = new System.Drawing.Point(12, 150);
   this.label6.Name = "label6";
   this.label6.Size = new System.Drawing.Size(73, 13);
   this.label6.TabIndex = 16;
   this.label6.Text = "Testing string:";
   // 
   // cbIsReqired
   // 
   this.cbIsReqired.AutoSize = true;
   this.cbIsReqired.Location = new System.Drawing.Point(223, 178);
   this.cbIsReqired.Name = "cbIsReqired";
   this.cbIsReqired.Size = new System.Drawing.Size(80, 17);
   this.cbIsReqired.TabIndex = 17;
   this.cbIsReqired.Text = "Is Required";
   this.cbIsReqired.UseVisualStyleBackColor = true;
   // 
   // AddNewApplication
   // 
   this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
   this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
   this.ClientSize = new System.Drawing.Size(504, 429);
   this.Controls.Add(this.cbIsReqired);
   this.Controls.Add(this.label6);
   this.Controls.Add(this.label5);
   this.Controls.Add(this.label4);
   this.Controls.Add(this.label3);
   this.Controls.Add(this.label2);
   this.Controls.Add(this.label1);
   this.Controls.Add(this.btnDiscard);
   this.Controls.Add(this.btnAdd);
   this.Controls.Add(this.lbAllRules);
   this.Controls.Add(this.btnPollDataFromCurrentProcess);
   this.Controls.Add(this.btnNewRule);
   this.Controls.Add(this.tbTestingString);
   this.Controls.Add(this.tbPattern);
   this.Controls.Add(this.cbCompareAlgorithm);
   this.Controls.Add(this.cbCompareWith);
   this.Controls.Add(this.tbInternalString);
   this.Controls.Add(this.tbApplicationTitle);
   this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
   this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
   this.MaximizeBox = false;
   this.MinimizeBox = false;
   this.Name = "AddNewApplication";
   this.Text = "Add New Application";
   this.Load += new System.EventHandler(this.AddNewApplication_Load);
   this.ResumeLayout(false);
   this.PerformLayout();

  }

  #endregion

  private System.Windows.Forms.TextBox tbApplicationTitle;
  private System.Windows.Forms.TextBox tbInternalString;
  private System.Windows.Forms.ComboBox cbCompareWith;
  private System.Windows.Forms.ComboBox cbCompareAlgorithm;
  private System.Windows.Forms.TextBox tbPattern;
  private System.Windows.Forms.TextBox tbTestingString;
  private System.Windows.Forms.Button btnNewRule;
  private System.Windows.Forms.Button btnPollDataFromCurrentProcess;
  private System.Windows.Forms.ListBox lbAllRules;
  private System.Windows.Forms.Button btnAdd;
  private System.Windows.Forms.Button btnDiscard;
  private System.Windows.Forms.Label label1;
  private System.Windows.Forms.Label label2;
  private System.Windows.Forms.Label label3;
  private System.Windows.Forms.Label label4;
  private System.Windows.Forms.Label label5;
  private System.Windows.Forms.Label label6;
  private System.Windows.Forms.CheckBox cbIsReqired;
 }
}