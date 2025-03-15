namespace WDAC_PS_Generator.Forms
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            label1 = new Label();
            NewButton = new Button();
            AddButton = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            GenerateXml = new CheckBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            ReplaceButton = new Button();
            SmartCardCheck = new CheckBox();
            CaLabel = new Label();
            CaSelection = new ComboBox();
            ExecuteReplace = new Button();
            GenerateP7b = new CheckBox();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            tableLayoutPanel1.SetColumnSpan(label1, 3);
            label1.Location = new Point(6, 0);
            label1.Margin = new Padding(6, 0, 6, 0);
            label1.Name = "label1";
            label1.Size = new Size(1437, 94);
            label1.TabIndex = 0;
            label1.Text = resources.GetString("label1.Text");
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // NewButton
            // 
            NewButton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            NewButton.Location = new Point(6, 100);
            NewButton.Margin = new Padding(6);
            NewButton.Name = "NewButton";
            NewButton.Size = new Size(470, 156);
            NewButton.TabIndex = 1;
            NewButton.Text = "New WDAC File";
            NewButton.UseVisualStyleBackColor = true;
            NewButton.Click += NewButton_Click;
            // 
            // AddButton
            // 
            AddButton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            AddButton.Location = new Point(971, 100);
            AddButton.Margin = new Padding(6);
            AddButton.Name = "AddButton";
            AddButton.Size = new Size(472, 156);
            AddButton.TabIndex = 2;
            AddButton.Text = "Add CA To Current WDAC File";
            AddButton.UseVisualStyleBackColor = true;
            AddButton.Click += AddButton_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel1.Controls.Add(GenerateXml, 1, 6);
            tableLayoutPanel1.Controls.Add(label4, 2, 5);
            tableLayoutPanel1.Controls.Add(label3, 1, 5);
            tableLayoutPanel1.Controls.Add(label2, 0, 5);
            tableLayoutPanel1.Controls.Add(ReplaceButton, 1, 1);
            tableLayoutPanel1.Controls.Add(AddButton, 2, 1);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(NewButton, 0, 1);
            tableLayoutPanel1.Controls.Add(SmartCardCheck, 0, 6);
            tableLayoutPanel1.Controls.Add(CaLabel, 1, 2);
            tableLayoutPanel1.Controls.Add(CaSelection, 1, 3);
            tableLayoutPanel1.Controls.Add(ExecuteReplace, 1, 4);
            tableLayoutPanel1.Controls.Add(GenerateP7b, 2, 6);
            tableLayoutPanel1.Location = new Point(22, 26);
            tableLayoutPanel1.Margin = new Padding(6);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 7;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(1449, 546);
            tableLayoutPanel1.TabIndex = 4;
            // 
            // GenerateXml
            // 
            GenerateXml.Anchor = AnchorStyles.None;
            GenerateXml.AutoSize = true;
            GenerateXml.Checked = true;
            GenerateXml.CheckState = CheckState.Checked;
            GenerateXml.Location = new Point(603, 495);
            GenerateXml.Name = "GenerateXml";
            GenerateXml.Size = new Size(241, 36);
            GenerateXml.TabIndex = 5;
            GenerateXml.Text = "Generate XML File";
            GenerateXml.UseVisualStyleBackColor = true;
            GenerateXml.CheckedChanged += GenerateXml_CheckedChanged;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label4.Location = new Point(971, 394);
            label4.Margin = new Padding(6, 0, 6, 0);
            label4.Name = "label4";
            label4.Size = new Size(472, 83);
            label4.TabIndex = 6;
            label4.Text = "Use this to add a new CA to an existing WDAC file";
            label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            label3.Location = new Point(488, 390);
            label3.Margin = new Padding(6, 0, 6, 0);
            label3.Name = "label3";
            label3.Size = new Size(470, 91);
            label3.TabIndex = 6;
            label3.Text = "Use this to replace a single CA in an existing WDAC file with a new one.";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            label2.Location = new Point(6, 390);
            label2.Margin = new Padding(6, 0, 6, 0);
            label2.Name = "label2";
            label2.Size = new Size(470, 91);
            label2.TabIndex = 5;
            label2.Text = "Use this to generate a new WDAC file with selected trusted CAs.";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ReplaceButton
            // 
            ReplaceButton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ReplaceButton.Location = new Point(488, 100);
            ReplaceButton.Margin = new Padding(6);
            ReplaceButton.Name = "ReplaceButton";
            ReplaceButton.Size = new Size(471, 156);
            ReplaceButton.TabIndex = 5;
            ReplaceButton.Text = "Replace CA In Current WDAC File";
            ReplaceButton.UseVisualStyleBackColor = true;
            ReplaceButton.Click += ReplaceButton_Click;
            // 
            // SmartCardCheck
            // 
            SmartCardCheck.Anchor = AnchorStyles.None;
            SmartCardCheck.AutoSize = true;
            SmartCardCheck.Checked = true;
            SmartCardCheck.CheckState = CheckState.Checked;
            SmartCardCheck.Location = new Point(49, 495);
            SmartCardCheck.Margin = new Padding(6);
            SmartCardCheck.Name = "SmartCardCheck";
            SmartCardCheck.Size = new Size(383, 36);
            SmartCardCheck.TabIndex = 7;
            SmartCardCheck.Text = "Use Smart Card Crypto Provider";
            SmartCardCheck.UseVisualStyleBackColor = true;
            // 
            // CaLabel
            // 
            CaLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            CaLabel.AutoSize = true;
            CaLabel.Location = new Point(485, 262);
            CaLabel.Name = "CaLabel";
            CaLabel.Size = new Size(236, 32);
            CaLabel.TabIndex = 5;
            CaLabel.Text = "Select CA To Replace";
            CaLabel.Visible = false;
            // 
            // CaSelection
            // 
            CaSelection.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CaSelection.FormattingEnabled = true;
            CaSelection.Location = new Point(485, 297);
            CaSelection.Name = "CaSelection";
            CaSelection.Size = new Size(477, 40);
            CaSelection.TabIndex = 6;
            CaSelection.Visible = false;
            // 
            // ExecuteReplace
            // 
            ExecuteReplace.Anchor = AnchorStyles.Top;
            ExecuteReplace.Location = new Point(648, 343);
            ExecuteReplace.Name = "ExecuteReplace";
            ExecuteReplace.Size = new Size(150, 44);
            ExecuteReplace.TabIndex = 5;
            ExecuteReplace.Text = "Replace";
            ExecuteReplace.UseVisualStyleBackColor = true;
            ExecuteReplace.Visible = false;
            ExecuteReplace.Click += ExecuteReplace_Click;
            // 
            // GenerateP7b
            // 
            GenerateP7b.Anchor = AnchorStyles.None;
            GenerateP7b.AutoSize = true;
            GenerateP7b.Checked = true;
            GenerateP7b.CheckState = CheckState.Checked;
            GenerateP7b.Location = new Point(1090, 495);
            GenerateP7b.Name = "GenerateP7b";
            GenerateP7b.Size = new Size(234, 36);
            GenerateP7b.TabIndex = 8;
            GenerateP7b.Text = "Generate P7B File";
            GenerateP7b.UseVisualStyleBackColor = true;
            GenerateP7b.CheckedChanged += GenerateP7b_CheckedChanged;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1493, 596);
            Controls.Add(tableLayoutPanel1);
            Margin = new Padding(6);
            Name = "Main";
            Text = "WSOD Generator";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Button NewButton;
        private Button AddButton;
        private TableLayoutPanel tableLayoutPanel1;
        private Button ReplaceButton;
        private Label label2;
        private Label label4;
        private Label label3;
        private CheckBox SmartCardCheck;
        private Button ExecuteReplace;
        private ComboBox CaSelection;
        private Label CaLabel;
        private CheckBox GenerateXml;
        private CheckBox GenerateP7b;
    }
}
