namespace SigScanner
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.ProcNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SigPatternTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SearchButton = new System.Windows.Forms.Button();
            this.OffsetsListView = new System.Windows.Forms.ListView();
            this.label3 = new System.Windows.Forms.Label();
            this.ModuleNameTextBox = new System.Windows.Forms.TextBox();
            this.CheckAllModuleCheckBox = new System.Windows.Forms.CheckBox();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.imSearchCheckbox = new System.Windows.Forms.CheckBox();
            this.AddSigButton = new System.Windows.Forms.Button();
            this.SigMaskTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ProcNameTextBox
            // 
            this.ProcNameTextBox.Location = new System.Drawing.Point(12, 206);
            this.ProcNameTextBox.Name = "ProcNameTextBox";
            this.ProcNameTextBox.Size = new System.Drawing.Size(91, 20);
            this.ProcNameTextBox.TabIndex = 0;
            this.ProcNameTextBox.TextChanged += new System.EventHandler(this.ProcNameTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 190);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Process Name:";
            // 
            // SigPatternTextBox
            // 
            this.SigPatternTextBox.Location = new System.Drawing.Point(12, 25);
            this.SigPatternTextBox.Name = "SigPatternTextBox";
            this.SigPatternTextBox.Size = new System.Drawing.Size(177, 20);
            this.SigPatternTextBox.TabIndex = 2;
            this.SigPatternTextBox.TextChanged += new System.EventHandler(this.SigTextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Signature:";
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(296, 189);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(79, 39);
            this.SearchButton.TabIndex = 4;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // OffsetsListView
            // 
            this.OffsetsListView.Location = new System.Drawing.Point(12, 51);
            this.OffsetsListView.Name = "OffsetsListView";
            this.OffsetsListView.Size = new System.Drawing.Size(363, 127);
            this.OffsetsListView.TabIndex = 6;
            this.OffsetsListView.UseCompatibleStateImageBehavior = false;
            this.OffsetsListView.DoubleClick += new System.EventHandler(this.OffsetsListView_DoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(106, 190);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Module Name:";
            // 
            // ModuleNameTextBox
            // 
            this.ModuleNameTextBox.Location = new System.Drawing.Point(109, 206);
            this.ModuleNameTextBox.Name = "ModuleNameTextBox";
            this.ModuleNameTextBox.Size = new System.Drawing.Size(91, 20);
            this.ModuleNameTextBox.TabIndex = 8;
            // 
            // CheckAllModuleCheckBox
            // 
            this.CheckAllModuleCheckBox.AutoSize = true;
            this.CheckAllModuleCheckBox.Location = new System.Drawing.Point(206, 211);
            this.CheckAllModuleCheckBox.Name = "CheckAllModuleCheckBox";
            this.CheckAllModuleCheckBox.Size = new System.Drawing.Size(80, 17);
            this.CheckAllModuleCheckBox.TabIndex = 9;
            this.CheckAllModuleCheckBox.Text = "All Modules";
            this.CheckAllModuleCheckBox.UseVisualStyleBackColor = true;
            this.CheckAllModuleCheckBox.CheckedChanged += new System.EventHandler(this.CheckAllModuleCheckBox_CheckedChanged);
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(12, 233);
            this.ProgressBar.Margin = new System.Windows.Forms.Padding(2);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(363, 12);
            this.ProgressBar.TabIndex = 10;
            // 
            // imSearchCheckbox
            // 
            this.imSearchCheckbox.AutoSize = true;
            this.imSearchCheckbox.Location = new System.Drawing.Point(206, 189);
            this.imSearchCheckbox.Margin = new System.Windows.Forms.Padding(2);
            this.imSearchCheckbox.Name = "imSearchCheckbox";
            this.imSearchCheckbox.Size = new System.Drawing.Size(85, 17);
            this.imSearchCheckbox.TabIndex = 11;
            this.imSearchCheckbox.Text = "Auto Search";
            this.imSearchCheckbox.UseVisualStyleBackColor = true;
            // 
            // AddSigButton
            // 
            this.AddSigButton.Location = new System.Drawing.Point(292, 24);
            this.AddSigButton.Margin = new System.Windows.Forms.Padding(2);
            this.AddSigButton.Name = "AddSigButton";
            this.AddSigButton.Size = new System.Drawing.Size(83, 21);
            this.AddSigButton.TabIndex = 12;
            this.AddSigButton.Text = "Add";
            this.AddSigButton.UseVisualStyleBackColor = true;
            this.AddSigButton.Click += new System.EventHandler(this.AddSigButton_Click);
            // 
            // SigMaskTextBox
            // 
            this.SigMaskTextBox.Enabled = false;
            this.SigMaskTextBox.Location = new System.Drawing.Point(194, 25);
            this.SigMaskTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.SigMaskTextBox.Name = "SigMaskTextBox";
            this.SigMaskTextBox.Size = new System.Drawing.Size(94, 20);
            this.SigMaskTextBox.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(192, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Mask:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 257);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.SigMaskTextBox);
            this.Controls.Add(this.AddSigButton);
            this.Controls.Add(this.imSearchCheckbox);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.CheckAllModuleCheckBox);
            this.Controls.Add(this.ModuleNameTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.OffsetsListView);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SigPatternTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ProcNameTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainForm";
            this.Text = "Simple SigScanner";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MouseEnter += new System.EventHandler(this.MainForm_MouseEnter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ProcNameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox SigPatternTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.ListView OffsetsListView;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ModuleNameTextBox;
        private System.Windows.Forms.CheckBox CheckAllModuleCheckBox;
        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.CheckBox imSearchCheckbox;
        private System.Windows.Forms.Button AddSigButton;
        private System.Windows.Forms.TextBox SigMaskTextBox;
        private System.Windows.Forms.Label label4;
    }
}

