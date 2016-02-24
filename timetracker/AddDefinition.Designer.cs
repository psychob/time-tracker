namespace timetracker
{
	partial class AddDefinition
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddDefinition));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.tbUniqueName = new System.Windows.Forms.TextBox();
			this.tbAppName = new System.Windows.Forms.TextBox();
			this.cmsNaming = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.intelligentNamingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.guidToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exactToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.userDefinedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.button3 = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.cbGroupPriority = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.cbGroupType = new System.Windows.Forms.ComboBox();
			this.button2 = new System.Windows.Forms.Button();
			this.tbGroupName = new System.Windows.Forms.TextBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.button4 = new System.Windows.Forms.Button();
			this.label9 = new System.Windows.Forms.Label();
			this.cbMatchAlgo = new System.Windows.Forms.ComboBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.cbRuleMatchTo = new System.Windows.Forms.ComboBox();
			this.tbMatchTest = new System.Windows.Forms.TextBox();
			this.tbPattern = new System.Windows.Forms.TextBox();
			this.button5 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.cmsGroup = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.changeTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.anyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.changePriorityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.lowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mediumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.highToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cmsRule = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.changeAlgorithmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exactToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.exactCaseInsensitiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.nearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.nearCaseInsensitiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.regularExpressionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.removeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.groupBox1.SuspendLayout();
			this.cmsNaming.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.cmsGroup.SuspendLayout();
			this.cmsRule.SuspendLayout();
			this.SuspendLayout();
			//
			// groupBox1
			//
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.button1);
			this.groupBox1.Controls.Add(this.tbUniqueName);
			this.groupBox1.Controls.Add(this.tbAppName);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(454, 82);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Base Info";
			//
			// label2
			//
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Unique Name";
			//
			// label1
			//
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(90, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Application Name";
			//
			// button1
			//
			this.button1.Location = new System.Drawing.Point(405, 45);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(43, 20);
			this.button1.TabIndex = 2;
			this.button1.Text = "...";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			//
			// tbUniqueName
			//
			this.tbUniqueName.Location = new System.Drawing.Point(140, 45);
			this.tbUniqueName.Name = "tbUniqueName";
			this.tbUniqueName.ReadOnly = true;
			this.tbUniqueName.Size = new System.Drawing.Size(259, 20);
			this.tbUniqueName.TabIndex = 1;
			this.tbUniqueName.TextChanged += new System.EventHandler(this.tbUniqueName_TextChanged);
			//
			// tbAppName
			//
			this.tbAppName.Location = new System.Drawing.Point(140, 19);
			this.tbAppName.Name = "tbAppName";
			this.tbAppName.Size = new System.Drawing.Size(308, 20);
			this.tbAppName.TabIndex = 0;
			this.tbAppName.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			//
			// cmsNaming
			//
			this.cmsNaming.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.intelligentNamingToolStripMenuItem,
			this.guidToolStripMenuItem,
			this.exactToolStripMenuItem,
			this.userDefinedToolStripMenuItem});
			this.cmsNaming.Name = "cmsNaming";
			this.cmsNaming.Size = new System.Drawing.Size(161, 92);
			//
			// intelligentNamingToolStripMenuItem
			//
			this.intelligentNamingToolStripMenuItem.Checked = true;
			this.intelligentNamingToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.intelligentNamingToolStripMenuItem.Name = "intelligentNamingToolStripMenuItem";
			this.intelligentNamingToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.intelligentNamingToolStripMenuItem.Text = "Intelligent Naming";
			this.intelligentNamingToolStripMenuItem.Click += new System.EventHandler(this.intelligentNamingToolStripMenuItem_Click);
			//
			// guidToolStripMenuItem
			//
			this.guidToolStripMenuItem.Name = "guidToolStripMenuItem";
			this.guidToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.guidToolStripMenuItem.Text = "Guid";
			this.guidToolStripMenuItem.Click += new System.EventHandler(this.guidToolStripMenuItem_Click);
			//
			// exactToolStripMenuItem
			//
			this.exactToolStripMenuItem.Name = "exactToolStripMenuItem";
			this.exactToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.exactToolStripMenuItem.Text = "Exact";
			this.exactToolStripMenuItem.Click += new System.EventHandler(this.exactToolStripMenuItem_Click);
			//
			// userDefinedToolStripMenuItem
			//
			this.userDefinedToolStripMenuItem.Name = "userDefinedToolStripMenuItem";
			this.userDefinedToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.userDefinedToolStripMenuItem.Text = "User Defined";
			this.userDefinedToolStripMenuItem.Click += new System.EventHandler(this.userDefinedToolStripMenuItem_Click);
			//
			// groupBox2
			//
			this.groupBox2.Controls.Add(this.button3);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.cbGroupPriority);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.cbGroupType);
			this.groupBox2.Controls.Add(this.button2);
			this.groupBox2.Controls.Add(this.tbGroupName);
			this.groupBox2.Location = new System.Drawing.Point(12, 100);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(454, 130);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Group Info";
			//
			// button3
			//
			this.button3.Location = new System.Drawing.Point(373, 99);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(75, 23);
			this.button3.TabIndex = 7;
			this.button3.Text = "Add New Group";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			//
			// label5
			//
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 75);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(70, 13);
			this.label5.TabIndex = 6;
			this.label5.Text = "Group Priority";
			//
			// cbGroupPriority
			//
			this.cbGroupPriority.FormattingEnabled = true;
			this.cbGroupPriority.Items.AddRange(new object[] {
			"Low",
			"Medium",
			"High"});
			this.cbGroupPriority.Location = new System.Drawing.Point(140, 72);
			this.cbGroupPriority.Name = "cbGroupPriority";
			this.cbGroupPriority.Size = new System.Drawing.Size(308, 21);
			this.cbGroupPriority.TabIndex = 5;
			//
			// label4
			//
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 48);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(63, 13);
			this.label4.TabIndex = 4;
			this.label4.Text = "Group Type";
			//
			// label3
			//
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 22);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(67, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "Group Name";
			//
			// cbGroupType
			//
			this.cbGroupType.FormattingEnabled = true;
			this.cbGroupType.Items.AddRange(new object[] {
			"All",
			"Any"});
			this.cbGroupType.Location = new System.Drawing.Point(140, 45);
			this.cbGroupType.Name = "cbGroupType";
			this.cbGroupType.Size = new System.Drawing.Size(308, 21);
			this.cbGroupType.TabIndex = 2;
			//
			// button2
			//
			this.button2.Location = new System.Drawing.Point(405, 19);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(43, 20);
			this.button2.TabIndex = 1;
			this.button2.Text = "...";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			//
			// tbGroupName
			//
			this.tbGroupName.Location = new System.Drawing.Point(140, 19);
			this.tbGroupName.Name = "tbGroupName";
			this.tbGroupName.Size = new System.Drawing.Size(259, 20);
			this.tbGroupName.TabIndex = 0;
			//
			// groupBox3
			//
			this.groupBox3.Controls.Add(this.button4);
			this.groupBox3.Controls.Add(this.label9);
			this.groupBox3.Controls.Add(this.cbMatchAlgo);
			this.groupBox3.Controls.Add(this.label8);
			this.groupBox3.Controls.Add(this.label7);
			this.groupBox3.Controls.Add(this.label6);
			this.groupBox3.Controls.Add(this.cbRuleMatchTo);
			this.groupBox3.Controls.Add(this.tbMatchTest);
			this.groupBox3.Controls.Add(this.tbPattern);
			this.groupBox3.Location = new System.Drawing.Point(12, 236);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(454, 159);
			this.groupBox3.TabIndex = 2;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Rules";
			//
			// button4
			//
			this.button4.Location = new System.Drawing.Point(373, 125);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(75, 23);
			this.button4.TabIndex = 8;
			this.button4.Text = "Add New";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.button4_Click);
			//
			// label9
			//
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(6, 102);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(61, 13);
			this.label9.TabIndex = 7;
			this.label9.Text = "Test Match";
			//
			// cbMatchAlgo
			//
			this.cbMatchAlgo.FormattingEnabled = true;
			this.cbMatchAlgo.Items.AddRange(new object[] {
			"Exact Match",
			"Exact Match (Case Insensitive)",
			"Near Match",
			"Near Match (Case Insensitive)",
			"Regular Expression",
			"Regular Expression (Case Insensitive)",
			"Starts With",
			"Starts With (Case Insensitive)",
			"Ends With",
			"Ends With (Case Insensitive)"});
			this.cbMatchAlgo.Location = new System.Drawing.Point(140, 72);
			this.cbMatchAlgo.Name = "cbMatchAlgo";
			this.cbMatchAlgo.Size = new System.Drawing.Size(308, 21);
			this.cbMatchAlgo.TabIndex = 6;
			this.cbMatchAlgo.SelectedIndexChanged += new System.EventHandler(this.cbMatchAlgo_SelectedIndexChanged);
			//
			// label8
			//
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(6, 75);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(83, 13);
			this.label8.TabIndex = 5;
			this.label8.Text = "Match Algorithm";
			//
			// label7
			//
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(6, 48);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(53, 13);
			this.label7.TabIndex = 4;
			this.label7.Text = "Match To";
			//
			// label6
			//
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(6, 22);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(41, 13);
			this.label6.TabIndex = 3;
			this.label6.Text = "Pattern";
			//
			// cbRuleMatchTo
			//
			this.cbRuleMatchTo.FormattingEnabled = true;
			this.cbRuleMatchTo.Items.AddRange(new object[] {
			"Process Name",
			"Process Name & Path",
			"Process Path",
			"Version - Product Name",
			"Version - Description",
			"Version - Company Name",
			"Version - Product Version",
			"Version - File Version",
			"MD5 Hash"});
			this.cbRuleMatchTo.Location = new System.Drawing.Point(140, 45);
			this.cbRuleMatchTo.Name = "cbRuleMatchTo";
			this.cbRuleMatchTo.Size = new System.Drawing.Size(308, 21);
			this.cbRuleMatchTo.TabIndex = 2;
			//
			// tbMatchTest
			//
			this.tbMatchTest.Location = new System.Drawing.Point(140, 99);
			this.tbMatchTest.Name = "tbMatchTest";
			this.tbMatchTest.Size = new System.Drawing.Size(308, 20);
			this.tbMatchTest.TabIndex = 1;
			this.tbMatchTest.TextChanged += new System.EventHandler(this.tbMatchTest_TextChanged);
			//
			// tbPattern
			//
			this.tbPattern.Location = new System.Drawing.Point(140, 19);
			this.tbPattern.Name = "tbPattern";
			this.tbPattern.Size = new System.Drawing.Size(308, 20);
			this.tbPattern.TabIndex = 0;
			this.tbPattern.TextChanged += new System.EventHandler(this.tbPattern_TextChanged);
			//
			// button5
			//
			this.button5.Location = new System.Drawing.Point(391, 646);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(75, 23);
			this.button5.TabIndex = 4;
			this.button5.Text = "Add";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += new System.EventHandler(this.button5_Click);
			//
			// button6
			//
			this.button6.Location = new System.Drawing.Point(310, 647);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(75, 23);
			this.button6.TabIndex = 5;
			this.button6.Text = "Cancel";
			this.button6.UseVisualStyleBackColor = true;
			this.button6.Click += new System.EventHandler(this.button6_Click);
			//
			// treeView1
			//
			this.treeView1.ImageIndex = 0;
			this.treeView1.ImageList = this.imageList1;
			this.treeView1.Location = new System.Drawing.Point(12, 401);
			this.treeView1.Name = "treeView1";
			this.treeView1.SelectedImageIndex = 0;
			this.treeView1.Size = new System.Drawing.Size(454, 228);
			this.treeView1.TabIndex = 6;
			this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
			//
			// imageList1
			//
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "folder.png");
			this.imageList1.Images.SetKeyName(1, "folder-open.png");
			this.imageList1.Images.SetKeyName(2, "file.ico");
			//
			// cmsGroup
			//
			this.cmsGroup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.changeTypeToolStripMenuItem,
			this.changePriorityToolStripMenuItem,
			this.toolStripMenuItem2,
			this.removeToolStripMenuItem});
			this.cmsGroup.Name = "cmsGroup";
			this.cmsGroup.Size = new System.Drawing.Size(149, 76);
			//
			// changeTypeToolStripMenuItem
			//
			this.changeTypeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.allToolStripMenuItem,
			this.anyToolStripMenuItem});
			this.changeTypeToolStripMenuItem.Name = "changeTypeToolStripMenuItem";
			this.changeTypeToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.changeTypeToolStripMenuItem.Text = "Change Type";
			//
			// allToolStripMenuItem
			//
			this.allToolStripMenuItem.Name = "allToolStripMenuItem";
			this.allToolStripMenuItem.Size = new System.Drawing.Size(93, 22);
			this.allToolStripMenuItem.Text = "All";
			this.allToolStripMenuItem.Click += new System.EventHandler(this.allToolStripMenuItem_Click);
			//
			// anyToolStripMenuItem
			//
			this.anyToolStripMenuItem.Name = "anyToolStripMenuItem";
			this.anyToolStripMenuItem.Size = new System.Drawing.Size(93, 22);
			this.anyToolStripMenuItem.Text = "Any";
			this.anyToolStripMenuItem.Click += new System.EventHandler(this.anyToolStripMenuItem_Click);
			//
			// changePriorityToolStripMenuItem
			//
			this.changePriorityToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.lowToolStripMenuItem,
			this.mediumToolStripMenuItem,
			this.highToolStripMenuItem});
			this.changePriorityToolStripMenuItem.Name = "changePriorityToolStripMenuItem";
			this.changePriorityToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.changePriorityToolStripMenuItem.Text = "Change Priority";
			//
			// lowToolStripMenuItem
			//
			this.lowToolStripMenuItem.Name = "lowToolStripMenuItem";
			this.lowToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
			this.lowToolStripMenuItem.Text = "Low";
			this.lowToolStripMenuItem.Click += new System.EventHandler(this.lowToolStripMenuItem_Click);
			//
			// mediumToolStripMenuItem
			//
			this.mediumToolStripMenuItem.Name = "mediumToolStripMenuItem";
			this.mediumToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
			this.mediumToolStripMenuItem.Text = "Medium";
			this.mediumToolStripMenuItem.Click += new System.EventHandler(this.mediumToolStripMenuItem_Click);
			//
			// highToolStripMenuItem
			//
			this.highToolStripMenuItem.Name = "highToolStripMenuItem";
			this.highToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
			this.highToolStripMenuItem.Text = "High";
			this.highToolStripMenuItem.Click += new System.EventHandler(this.highToolStripMenuItem_Click);
			//
			// toolStripMenuItem2
			//
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(145, 6);
			//
			// removeToolStripMenuItem
			//
			this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
			this.removeToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.removeToolStripMenuItem.Text = "Remove";
			this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
			//
			// cmsRule
			//
			this.cmsRule.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.changeAlgorithmToolStripMenuItem,
			this.toolStripMenuItem1,
			this.removeToolStripMenuItem1});
			this.cmsRule.Name = "cmsRule";
			this.cmsRule.Size = new System.Drawing.Size(160, 54);
			//
			// changeAlgorithmToolStripMenuItem
			//
			this.changeAlgorithmToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.exactToolStripMenuItem1,
			this.exactCaseInsensitiveToolStripMenuItem,
			this.nearToolStripMenuItem,
			this.nearCaseInsensitiveToolStripMenuItem,
			this.regularExpressionToolStripMenuItem});
			this.changeAlgorithmToolStripMenuItem.Name = "changeAlgorithmToolStripMenuItem";
			this.changeAlgorithmToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
			this.changeAlgorithmToolStripMenuItem.Text = "Change Algorithm";
			//
			// exactToolStripMenuItem1
			//
			this.exactToolStripMenuItem1.Name = "exactToolStripMenuItem1";
			this.exactToolStripMenuItem1.Size = new System.Drawing.Size(189, 22);
			this.exactToolStripMenuItem1.Text = "Exact";
			this.exactToolStripMenuItem1.Click += new System.EventHandler(this.exactToolStripMenuItem1_Click);
			//
			// exactCaseInsensitiveToolStripMenuItem
			//
			this.exactCaseInsensitiveToolStripMenuItem.Name = "exactCaseInsensitiveToolStripMenuItem";
			this.exactCaseInsensitiveToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.exactCaseInsensitiveToolStripMenuItem.Text = "Exact (Case insensitive)";
			this.exactCaseInsensitiveToolStripMenuItem.Click += new System.EventHandler(this.exactCaseInsensitiveToolStripMenuItem_Click);
			//
			// nearToolStripMenuItem
			//
			this.nearToolStripMenuItem.Name = "nearToolStripMenuItem";
			this.nearToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.nearToolStripMenuItem.Text = "Near";
			this.nearToolStripMenuItem.Click += new System.EventHandler(this.nearToolStripMenuItem_Click);
			//
			// nearCaseInsensitiveToolStripMenuItem
			//
			this.nearCaseInsensitiveToolStripMenuItem.Name = "nearCaseInsensitiveToolStripMenuItem";
			this.nearCaseInsensitiveToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.nearCaseInsensitiveToolStripMenuItem.Text = "Near (Case insensitive)";
			this.nearCaseInsensitiveToolStripMenuItem.Click += new System.EventHandler(this.nearCaseInsensitiveToolStripMenuItem_Click);
			//
			// regularExpressionToolStripMenuItem
			//
			this.regularExpressionToolStripMenuItem.Name = "regularExpressionToolStripMenuItem";
			this.regularExpressionToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.regularExpressionToolStripMenuItem.Text = "Regular Expression";
			this.regularExpressionToolStripMenuItem.Click += new System.EventHandler(this.regularExpressionToolStripMenuItem_Click);
			//
			// toolStripMenuItem1
			//
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(156, 6);
			//
			// removeToolStripMenuItem1
			//
			this.removeToolStripMenuItem1.Name = "removeToolStripMenuItem1";
			this.removeToolStripMenuItem1.Size = new System.Drawing.Size(159, 22);
			this.removeToolStripMenuItem1.Text = "Remove";
			this.removeToolStripMenuItem1.Click += new System.EventHandler(this.removeToolStripMenuItem1_Click);
			//
			// AddDefinition
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(478, 682);
			this.Controls.Add(this.treeView1);
			this.Controls.Add(this.button6);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "AddDefinition";
			this.Text = "AddDefinition";
			this.Load += new System.EventHandler(this.AddDefinition_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.cmsNaming.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.cmsGroup.ResumeLayout(false);
			this.cmsRule.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox tbUniqueName;
		private System.Windows.Forms.TextBox tbAppName;
		private System.Windows.Forms.ContextMenuStrip cmsNaming;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ToolStripMenuItem intelligentNamingToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem guidToolStripMenuItem;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.TextBox tbGroupName;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox cbGroupType;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.ComboBox cbRuleMatchTo;
		private System.Windows.Forms.TextBox tbMatchTest;
		private System.Windows.Forms.TextBox tbPattern;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox cbGroupPriority;
		private System.Windows.Forms.ComboBox cbMatchAlgo;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ToolStripMenuItem exactToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem userDefinedToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip cmsGroup;
		private System.Windows.Forms.ToolStripMenuItem changeTypeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem allToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem anyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem changePriorityToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem lowToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem mediumToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem highToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip cmsRule;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem changeAlgorithmToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem exactToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem exactCaseInsensitiveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem nearToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem nearCaseInsensitiveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem regularExpressionToolStripMenuItem;
	}
}