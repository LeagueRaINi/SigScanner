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
            this.components = new System.ComponentModel.Container();
            this.ProcNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SigPatternTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SearchButton = new System.Windows.Forms.Button();
            this.OffsetsListView = new System.Windows.Forms.ListView();
            this.label3 = new System.Windows.Forms.Label();
            this.ModuleNameTextBox = new System.Windows.Forms.TextBox();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.imSearchCheckbox = new System.Windows.Forms.CheckBox();
            this.AddSigButton = new System.Windows.Forms.Button();
            this.SigMaskTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.RemoveButton = new System.Windows.Forms.Button();
            this.ClearAllButton = new System.Windows.Forms.Button();
            this.modulesToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // ProcNameTextBox
            // 
            this.ProcNameTextBox.Location = new System.Drawing.Point(8, 235);
            this.ProcNameTextBox.Name = "ProcNameTextBox";
            this.ProcNameTextBox.Size = new System.Drawing.Size(183, 20);
            this.ProcNameTextBox.TabIndex = 0;
            this.ProcNameTextBox.TextChanged += new System.EventHandler(this.ProcNameTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 219);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Process Name:";
            // 
            // SigPatternTextBox
            // 
            this.SigPatternTextBox.Location = new System.Drawing.Point(8, 21);
            this.SigPatternTextBox.Name = "SigPatternTextBox";
            this.SigPatternTextBox.Size = new System.Drawing.Size(187, 20);
            this.SigPatternTextBox.TabIndex = 2;
            this.SigPatternTextBox.TextChanged += new System.EventHandler(this.SigTextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Signature:";
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(296, 230);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(83, 26);
            this.SearchButton.TabIndex = 4;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // OffsetsListView
            // 
            this.OffsetsListView.Location = new System.Drawing.Point(8, 83);
            this.OffsetsListView.Name = "OffsetsListView";
            this.OffsetsListView.Size = new System.Drawing.Size(374, 127);
            this.OffsetsListView.TabIndex = 6;
            this.OffsetsListView.UseCompatibleStateImageBehavior = false;
            this.OffsetsListView.DoubleClick += new System.EventHandler(this.OffsetsListView_DoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(101, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Limit to module:";
            // 
            // ModuleNameTextBox
            // 
            this.ModuleNameTextBox.Location = new System.Drawing.Point(104, 59);
            this.ModuleNameTextBox.Name = "ModuleNameTextBox";
            this.ModuleNameTextBox.Size = new System.Drawing.Size(91, 20);
            this.ModuleNameTextBox.TabIndex = 8;
            this.modulesToolTip.SetToolTip(this.ModuleNameTextBox, "Leave blank to scan all");
            this.ModuleNameTextBox.Enter += new System.EventHandler(this.ModuleNameTextBox_Enter);
            this.ModuleNameTextBox.Leave += new System.EventHandler(this.ModuleNameTextBox_Leave);
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(8, 264);
            this.ProgressBar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(372, 12);
            this.ProgressBar.TabIndex = 10;
            // 
            // imSearchCheckbox
            // 
            this.imSearchCheckbox.AutoSize = true;
            this.imSearchCheckbox.Location = new System.Drawing.Point(203, 236);
            this.imSearchCheckbox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.imSearchCheckbox.Name = "imSearchCheckbox";
            this.imSearchCheckbox.Size = new System.Drawing.Size(85, 17);
            this.imSearchCheckbox.TabIndex = 11;
            this.imSearchCheckbox.Text = "Auto Search";
            this.imSearchCheckbox.UseVisualStyleBackColor = true;
            this.imSearchCheckbox.CheckedChanged += new System.EventHandler(this.imSearchCheckbox_CheckedChanged);
            // 
            // AddSigButton
            // 
            this.AddSigButton.Location = new System.Drawing.Point(209, 21);
            this.AddSigButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.AddSigButton.Name = "AddSigButton";
            this.AddSigButton.Size = new System.Drawing.Size(83, 55);
            this.AddSigButton.TabIndex = 12;
            this.AddSigButton.Text = "Add";
            this.AddSigButton.UseVisualStyleBackColor = true;
            this.AddSigButton.Click += new System.EventHandler(this.AddSigButton_Click);
            // 
            // SigMaskTextBox
            // 
            this.SigMaskTextBox.Enabled = false;
            this.SigMaskTextBox.Location = new System.Drawing.Point(8, 59);
            this.SigMaskTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SigMaskTextBox.Name = "SigMaskTextBox";
            this.SigMaskTextBox.Size = new System.Drawing.Size(94, 20);
            this.SigMaskTextBox.TabIndex = 13;
            this.SigMaskTextBox.TextChanged += new System.EventHandler(this.SigMaskTextBox_TextChanged);
            this.SigMaskTextBox.Enter += new System.EventHandler(this.SigMaskTextBox_Enter);
            this.SigMaskTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SigMaskTextBox_KeyPress);
            this.SigMaskTextBox.Leave += new System.EventHandler(this.SigMaskTextBox_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Mask:";
            // 
            // RemoveButton
            // 
            this.RemoveButton.Location = new System.Drawing.Point(296, 21);
            this.RemoveButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(83, 26);
            this.RemoveButton.TabIndex = 15;
            this.RemoveButton.Text = "Remove";
            this.RemoveButton.UseVisualStyleBackColor = true;
            this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // ClearAllButton
            // 
            this.ClearAllButton.Location = new System.Drawing.Point(296, 49);
            this.ClearAllButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ClearAllButton.Name = "ClearAllButton";
            this.ClearAllButton.Size = new System.Drawing.Size(83, 26);
            this.ClearAllButton.TabIndex = 16;
            this.ClearAllButton.Text = "Clear all";
            this.ClearAllButton.UseVisualStyleBackColor = true;
            // 
            // modulesToolTip
            // 
            this.modulesToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 287);
            this.Controls.Add(this.ClearAllButton);
            this.Controls.Add(this.RemoveButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.SigMaskTextBox);
            this.Controls.Add(this.AddSigButton);
            this.Controls.Add(this.imSearchCheckbox);
            this.Controls.Add(this.ProgressBar);
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
        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.CheckBox imSearchCheckbox;
        private System.Windows.Forms.Button AddSigButton;
        private System.Windows.Forms.TextBox SigMaskTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button RemoveButton;
        private System.Windows.Forms.Button ClearAllButton;
        private System.Windows.Forms.ToolTip modulesToolTip;
    }
}

