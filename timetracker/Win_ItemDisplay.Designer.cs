namespace timetracker
{
 partial class Win_ItemDisplay
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
   this.tbName = new System.Windows.Forms.TextBox();
   this.tbGuid = new System.Windows.Forms.TextBox();
   this.button1 = new System.Windows.Forms.Button();
   this.groupBox1 = new System.Windows.Forms.GroupBox();
   this.tbGroupName = new System.Windows.Forms.TextBox();
   this.button2 = new System.Windows.Forms.Button();
   this.groupBox2 = new System.Windows.Forms.GroupBox();
   this.cbCompareTo = new System.Windows.Forms.ComboBox();
   this.cbAlgorithm = new System.Windows.Forms.ComboBox();
   this.tbMatchString = new System.Windows.Forms.TextBox();
   this.textBox5 = new System.Windows.Forms.TextBox();
   this.button3 = new System.Windows.Forms.Button();
   this.cbGroupType = new System.Windows.Forms.ComboBox();
   this.label1 = new System.Windows.Forms.Label();
   this.label2 = new System.Windows.Forms.Label();
   this.label3 = new System.Windows.Forms.Label();
   this.label4 = new System.Windows.Forms.Label();
   this.label5 = new System.Windows.Forms.Label();
   this.label6 = new System.Windows.Forms.Label();
   this.label7 = new System.Windows.Forms.Label();
   this.label8 = new System.Windows.Forms.Label();
   this.treeView1 = new System.Windows.Forms.TreeView();
   this.button4 = new System.Windows.Forms.Button();
   this.button5 = new System.Windows.Forms.Button();
   this.groupBox1.SuspendLayout();
   this.groupBox2.SuspendLayout();
   this.SuspendLayout();
   // 
   // tbName
   // 
   this.tbName.Location = new System.Drawing.Point(216, 12);
   this.tbName.Name = "tbName";
   this.tbName.Size = new System.Drawing.Size(356, 20);
   this.tbName.TabIndex = 0;
   // 
   // tbGuid
   // 
   this.tbGuid.Location = new System.Drawing.Point(216, 38);
   this.tbGuid.Name = "tbGuid";
   this.tbGuid.ReadOnly = true;
   this.tbGuid.Size = new System.Drawing.Size(320, 20);
   this.tbGuid.TabIndex = 1;
   // 
   // button1
   // 
   this.button1.Location = new System.Drawing.Point(542, 38);
   this.button1.Name = "button1";
   this.button1.Size = new System.Drawing.Size(30, 20);
   this.button1.TabIndex = 2;
   this.button1.Text = "R";
   this.button1.UseVisualStyleBackColor = true;
   this.button1.Click += new System.EventHandler(this.button1_Click);
   // 
   // groupBox1
   // 
   this.groupBox1.Controls.Add(this.label4);
   this.groupBox1.Controls.Add(this.label3);
   this.groupBox1.Controls.Add(this.cbGroupType);
   this.groupBox1.Controls.Add(this.button2);
   this.groupBox1.Controls.Add(this.tbGroupName);
   this.groupBox1.Location = new System.Drawing.Point(12, 64);
   this.groupBox1.Name = "groupBox1";
   this.groupBox1.Size = new System.Drawing.Size(560, 107);
   this.groupBox1.TabIndex = 3;
   this.groupBox1.TabStop = false;
   this.groupBox1.Text = "Group Data";
   // 
   // tbGroupName
   // 
   this.tbGroupName.Location = new System.Drawing.Point(204, 19);
   this.tbGroupName.Name = "tbGroupName";
   this.tbGroupName.Size = new System.Drawing.Size(350, 20);
   this.tbGroupName.TabIndex = 0;
   // 
   // button2
   // 
   this.button2.Location = new System.Drawing.Point(479, 78);
   this.button2.Name = "button2";
   this.button2.Size = new System.Drawing.Size(75, 23);
   this.button2.TabIndex = 1;
   this.button2.Text = "Add New Group";
   this.button2.UseVisualStyleBackColor = true;
   this.button2.Click += new System.EventHandler(this.button2_Click);
   // 
   // groupBox2
   // 
   this.groupBox2.Controls.Add(this.label8);
   this.groupBox2.Controls.Add(this.label7);
   this.groupBox2.Controls.Add(this.label6);
   this.groupBox2.Controls.Add(this.label5);
   this.groupBox2.Controls.Add(this.button3);
   this.groupBox2.Controls.Add(this.textBox5);
   this.groupBox2.Controls.Add(this.tbMatchString);
   this.groupBox2.Controls.Add(this.cbAlgorithm);
   this.groupBox2.Controls.Add(this.cbCompareTo);
   this.groupBox2.Location = new System.Drawing.Point(12, 177);
   this.groupBox2.Name = "groupBox2";
   this.groupBox2.Size = new System.Drawing.Size(560, 160);
   this.groupBox2.TabIndex = 4;
   this.groupBox2.TabStop = false;
   this.groupBox2.Text = "Item Data";
   // 
   // cbCompareTo
   // 
   this.cbCompareTo.FormattingEnabled = true;
   this.cbCompareTo.Items.AddRange(new object[] {
            "File Name",
            "Path without file name",
            "Full Path",
            "File Version - Name",
            "File Version - Description",
            "File Version - Company",
            "File Version - Produc Version",
            "File Version - File Version",
            "File MD5"});
   this.cbCompareTo.Location = new System.Drawing.Point(204, 19);
   this.cbCompareTo.Name = "cbCompareTo";
   this.cbCompareTo.Size = new System.Drawing.Size(350, 21);
   this.cbCompareTo.TabIndex = 0;
   // 
   // cbAlgorithm
   // 
   this.cbAlgorithm.FormattingEnabled = true;
   this.cbAlgorithm.Items.AddRange(new object[] {
            "Exact Sensitive",
            "Exact Insensitive",
            "Near Sensitive",
            "Near Insensitive",
            "Regular Expression"});
   this.cbAlgorithm.Location = new System.Drawing.Point(204, 72);
   this.cbAlgorithm.Name = "cbAlgorithm";
   this.cbAlgorithm.Size = new System.Drawing.Size(350, 21);
   this.cbAlgorithm.TabIndex = 1;
   // 
   // tbMatchString
   // 
   this.tbMatchString.Location = new System.Drawing.Point(204, 46);
   this.tbMatchString.Name = "tbMatchString";
   this.tbMatchString.Size = new System.Drawing.Size(350, 20);
   this.tbMatchString.TabIndex = 2;
   // 
   // textBox5
   // 
   this.textBox5.Location = new System.Drawing.Point(204, 99);
   this.textBox5.Name = "textBox5";
   this.textBox5.Size = new System.Drawing.Size(350, 20);
   this.textBox5.TabIndex = 3;
   // 
   // button3
   // 
   this.button3.Location = new System.Drawing.Point(479, 125);
   this.button3.Name = "button3";
   this.button3.Size = new System.Drawing.Size(75, 23);
   this.button3.TabIndex = 4;
   this.button3.Text = "Add New";
   this.button3.UseVisualStyleBackColor = true;
   this.button3.Click += new System.EventHandler(this.button3_Click);
   // 
   // cbGroupType
   // 
   this.cbGroupType.FormattingEnabled = true;
   this.cbGroupType.Items.AddRange(new object[] {
            "any",
            "all"});
   this.cbGroupType.Location = new System.Drawing.Point(204, 51);
   this.cbGroupType.Name = "cbGroupType";
   this.cbGroupType.Size = new System.Drawing.Size(350, 21);
   this.cbGroupType.TabIndex = 2;
   // 
   // label1
   // 
   this.label1.AutoSize = true;
   this.label1.Location = new System.Drawing.Point(12, 15);
   this.label1.Name = "label1";
   this.label1.Size = new System.Drawing.Size(104, 13);
   this.label1.TabIndex = 5;
   this.label1.Text = "Name of application:";
   // 
   // label2
   // 
   this.label2.AutoSize = true;
   this.label2.Location = new System.Drawing.Point(12, 41);
   this.label2.Name = "label2";
   this.label2.Size = new System.Drawing.Size(37, 13);
   this.label2.TabIndex = 6;
   this.label2.Text = "GUID:";
   // 
   // label3
   // 
   this.label3.AutoSize = true;
   this.label3.Location = new System.Drawing.Point(6, 22);
   this.label3.Name = "label3";
   this.label3.Size = new System.Drawing.Size(70, 13);
   this.label3.TabIndex = 3;
   this.label3.Text = "Group Name:";
   // 
   // label4
   // 
   this.label4.AutoSize = true;
   this.label4.Location = new System.Drawing.Point(6, 54);
   this.label4.Name = "label4";
   this.label4.Size = new System.Drawing.Size(66, 13);
   this.label4.TabIndex = 4;
   this.label4.Text = "Group Type:";
   // 
   // label5
   // 
   this.label5.AutoSize = true;
   this.label5.Location = new System.Drawing.Point(6, 22);
   this.label5.Name = "label5";
   this.label5.Size = new System.Drawing.Size(62, 13);
   this.label5.TabIndex = 5;
   this.label5.Text = "Match with:";
   // 
   // label6
   // 
   this.label6.AutoSize = true;
   this.label6.Location = new System.Drawing.Point(6, 49);
   this.label6.Name = "label6";
   this.label6.Size = new System.Drawing.Size(70, 13);
   this.label6.TabIndex = 6;
   this.label6.Text = "Match String:";
   // 
   // label7
   // 
   this.label7.AutoSize = true;
   this.label7.Location = new System.Drawing.Point(6, 75);
   this.label7.Name = "label7";
   this.label7.Size = new System.Drawing.Size(86, 13);
   this.label7.TabIndex = 7;
   this.label7.Text = "Match Algorithm:";
   // 
   // label8
   // 
   this.label8.AutoSize = true;
   this.label8.Location = new System.Drawing.Point(6, 102);
   this.label8.Name = "label8";
   this.label8.Size = new System.Drawing.Size(61, 13);
   this.label8.TabIndex = 8;
   this.label8.Text = "Test String:";
   // 
   // treeView1
   // 
   this.treeView1.Location = new System.Drawing.Point(12, 343);
   this.treeView1.Name = "treeView1";
   this.treeView1.Size = new System.Drawing.Size(560, 240);
   this.treeView1.TabIndex = 7;
   // 
   // button4
   // 
   this.button4.Location = new System.Drawing.Point(360, 589);
   this.button4.Name = "button4";
   this.button4.Size = new System.Drawing.Size(212, 23);
   this.button4.TabIndex = 8;
   this.button4.Text = "Add";
   this.button4.UseVisualStyleBackColor = true;
   this.button4.Click += new System.EventHandler(this.button4_Click);
   // 
   // button5
   // 
   this.button5.Location = new System.Drawing.Point(12, 589);
   this.button5.Name = "button5";
   this.button5.Size = new System.Drawing.Size(104, 23);
   this.button5.TabIndex = 9;
   this.button5.Text = "Cancel";
   this.button5.UseVisualStyleBackColor = true;
   this.button5.Click += new System.EventHandler(this.button5_Click);
   // 
   // Win_ItemDisplay
   // 
   this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
   this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
   this.ClientSize = new System.Drawing.Size(584, 624);
   this.Controls.Add(this.button5);
   this.Controls.Add(this.button4);
   this.Controls.Add(this.treeView1);
   this.Controls.Add(this.label2);
   this.Controls.Add(this.label1);
   this.Controls.Add(this.groupBox2);
   this.Controls.Add(this.groupBox1);
   this.Controls.Add(this.button1);
   this.Controls.Add(this.tbGuid);
   this.Controls.Add(this.tbName);
   this.MaximizeBox = false;
   this.MinimizeBox = false;
   this.Name = "Win_ItemDisplay";
   this.Text = "Add New Item";
   this.Load += new System.EventHandler(this.Win_ItemDisplay_Load);
   this.groupBox1.ResumeLayout(false);
   this.groupBox1.PerformLayout();
   this.groupBox2.ResumeLayout(false);
   this.groupBox2.PerformLayout();
   this.ResumeLayout(false);
   this.PerformLayout();

  }

  #endregion

  private System.Windows.Forms.TextBox tbName;
  private System.Windows.Forms.TextBox tbGuid;
  private System.Windows.Forms.Button button1;
  private System.Windows.Forms.GroupBox groupBox1;
  private System.Windows.Forms.TextBox tbGroupName;
  private System.Windows.Forms.Button button2;
  private System.Windows.Forms.GroupBox groupBox2;
  private System.Windows.Forms.TextBox textBox5;
  private System.Windows.Forms.TextBox tbMatchString;
  private System.Windows.Forms.ComboBox cbAlgorithm;
  private System.Windows.Forms.ComboBox cbCompareTo;
  private System.Windows.Forms.Button button3;
  private System.Windows.Forms.ComboBox cbGroupType;
  private System.Windows.Forms.Label label4;
  private System.Windows.Forms.Label label3;
  private System.Windows.Forms.Label label1;
  private System.Windows.Forms.Label label2;
  private System.Windows.Forms.Label label8;
  private System.Windows.Forms.Label label7;
  private System.Windows.Forms.Label label6;
  private System.Windows.Forms.Label label5;
  private System.Windows.Forms.TreeView treeView1;
  private System.Windows.Forms.Button button4;
  private System.Windows.Forms.Button button5;
 }
}