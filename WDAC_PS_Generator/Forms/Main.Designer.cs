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
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            ReplaceButton = new Button();
            SmartCardCheck = new CheckBox();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            tableLayoutPanel1.SetColumnSpan(label1, 3);
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(774, 44);
            label1.TabIndex = 0;
            label1.Text = resources.GetString("label1.Text");
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // NewButton
            // 
            NewButton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            NewButton.Location = new Point(3, 63);
            NewButton.Name = "NewButton";
            NewButton.Size = new Size(253, 73);
            NewButton.TabIndex = 1;
            NewButton.Text = "New WDAC File";
            NewButton.UseVisualStyleBackColor = true;
            NewButton.Click += NewButton_Click;
            // 
            // AddButton
            // 
            AddButton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            AddButton.Location = new Point(522, 63);
            AddButton.Name = "AddButton";
            AddButton.Size = new Size(255, 73);
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
            tableLayoutPanel1.Controls.Add(label4, 2, 2);
            tableLayoutPanel1.Controls.Add(label3, 1, 2);
            tableLayoutPanel1.Controls.Add(label2, 0, 2);
            tableLayoutPanel1.Controls.Add(ReplaceButton, 1, 1);
            tableLayoutPanel1.Controls.Add(AddButton, 2, 1);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(NewButton, 0, 1);
            tableLayoutPanel1.Controls.Add(SmartCardCheck, 0, 3);
            tableLayoutPanel1.Location = new Point(12, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.Size = new Size(780, 295);
            tableLayoutPanel1.TabIndex = 4;
            // 
            // label4
            // 
            label4.Location = new Point(522, 139);
            label4.Name = "label4";
            label4.Size = new Size(253, 125);
            label4.TabIndex = 6;
            label4.Text = "Use this to add a new CA to an existing WDAC file";
            label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            label3.Location = new Point(262, 139);
            label3.Name = "label3";
            label3.Size = new Size(253, 125);
            label3.TabIndex = 6;
            label3.Text = "Use this to replace a single CA in an existing WDAC file with a new one.";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            label2.Location = new Point(3, 139);
            label2.Name = "label2";
            label2.Size = new Size(253, 125);
            label2.TabIndex = 5;
            label2.Text = "Use this to generate a new WDAC file with selected trusted CAs.";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ReplaceButton
            // 
            ReplaceButton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ReplaceButton.Location = new Point(262, 63);
            ReplaceButton.Name = "ReplaceButton";
            ReplaceButton.Size = new Size(254, 73);
            ReplaceButton.TabIndex = 5;
            ReplaceButton.Text = "Replace CA In Current WDAC File";
            ReplaceButton.UseVisualStyleBackColor = true;
            // 
            // SmartCardCheck
            // 
            SmartCardCheck.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            SmartCardCheck.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(SmartCardCheck, 3);
            SmartCardCheck.Location = new Point(3, 267);
            SmartCardCheck.Name = "SmartCardCheck";
            SmartCardCheck.Size = new Size(774, 34);
            SmartCardCheck.TabIndex = 7;
            SmartCardCheck.Text = "Use Smart Card Crypto Provider";
            SmartCardCheck.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(804, 319);
            Controls.Add(tableLayoutPanel1);
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
    }
}
