﻿namespace SigScanner
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
            this.label3 = new System.Windows.Forms.Label();
            this.ModuleNameTextBox = new System.Windows.Forms.TextBox();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.InstantSearchCheckBox = new System.Windows.Forms.CheckBox();
            this.AddSigButton = new System.Windows.Forms.Button();
            this.SigMaskTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.RemoveButton = new System.Windows.Forms.Button();
            this.ClearAllButton = new System.Windows.Forms.Button();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SigsTreeView = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // ProcNameTextBox
            // 
            this.ProcNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProcNameTextBox.Location = new System.Drawing.Point(8, 235);
            this.ProcNameTextBox.Name = "ProcNameTextBox";
            this.ProcNameTextBox.Size = new System.Drawing.Size(184, 20);
            this.ProcNameTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 219);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Process Name:";
            // 
            // SigPatternTextBox
            // 
            this.SigPatternTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SigPatternTextBox.Location = new System.Drawing.Point(8, 21);
            this.SigPatternTextBox.Name = "SigPatternTextBox";
            this.SigPatternTextBox.Size = new System.Drawing.Size(196, 20);
            this.SigPatternTextBox.TabIndex = 2;
            this.SigPatternTextBox.TextChanged += new System.EventHandler(this.SigTextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Pattern:";
            // 
            // SearchButton
            // 
            this.SearchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchButton.Location = new System.Drawing.Point(297, 231);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(83, 26);
            this.SearchButton.TabIndex = 4;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(101, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Limit to Module:";
            // 
            // ModuleNameTextBox
            // 
            this.ModuleNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ModuleNameTextBox.Location = new System.Drawing.Point(104, 59);
            this.ModuleNameTextBox.Name = "ModuleNameTextBox";
            this.ModuleNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.ModuleNameTextBox.TabIndex = 8;
            this.ToolTip.SetToolTip(this.ModuleNameTextBox, "Leave blank to scan all");
            this.ModuleNameTextBox.Enter += new System.EventHandler(this.ModuleNameTextBox_Enter);
            this.ModuleNameTextBox.Leave += new System.EventHandler(this.ModuleNameTextBox_Leave);
            // 
            // ProgressBar
            // 
            this.ProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressBar.Location = new System.Drawing.Point(8, 264);
            this.ProgressBar.Margin = new System.Windows.Forms.Padding(2);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(372, 12);
            this.ProgressBar.TabIndex = 10;
            // 
            // InstantSearchCheckBox
            // 
            this.InstantSearchCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.InstantSearchCheckBox.AutoSize = true;
            this.InstantSearchCheckBox.Location = new System.Drawing.Point(206, 237);
            this.InstantSearchCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.InstantSearchCheckBox.Name = "InstantSearchCheckBox";
            this.InstantSearchCheckBox.Size = new System.Drawing.Size(85, 17);
            this.InstantSearchCheckBox.TabIndex = 11;
            this.InstantSearchCheckBox.Text = "Auto Search";
            this.InstantSearchCheckBox.UseVisualStyleBackColor = true;
            this.InstantSearchCheckBox.CheckedChanged += new System.EventHandler(this.imSearchCheckbox_CheckedChanged);
            // 
            // AddSigButton
            // 
            this.AddSigButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddSigButton.Location = new System.Drawing.Point(209, 21);
            this.AddSigButton.Margin = new System.Windows.Forms.Padding(2);
            this.AddSigButton.Name = "AddSigButton";
            this.AddSigButton.Size = new System.Drawing.Size(83, 58);
            this.AddSigButton.TabIndex = 12;
            this.AddSigButton.Text = "Add";
            this.AddSigButton.UseVisualStyleBackColor = true;
            this.AddSigButton.Click += new System.EventHandler(this.AddSigButton_Click);
            // 
            // SigMaskTextBox
            // 
            this.SigMaskTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SigMaskTextBox.Enabled = false;
            this.SigMaskTextBox.Location = new System.Drawing.Point(8, 59);
            this.SigMaskTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.SigMaskTextBox.Name = "SigMaskTextBox";
            this.SigMaskTextBox.Size = new System.Drawing.Size(91, 20);
            this.SigMaskTextBox.TabIndex = 13;
            this.SigMaskTextBox.TextChanged += new System.EventHandler(this.SigMaskTextBox_TextChanged);
            this.SigMaskTextBox.Enter += new System.EventHandler(this.SigMaskTextBox_Enter);
            this.SigMaskTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SigMaskTextBox_KeyPress);
            this.SigMaskTextBox.Leave += new System.EventHandler(this.SigMaskTextBox_Leave);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Mask:";
            // 
            // RemoveButton
            // 
            this.RemoveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RemoveButton.Location = new System.Drawing.Point(296, 21);
            this.RemoveButton.Margin = new System.Windows.Forms.Padding(2);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(83, 26);
            this.RemoveButton.TabIndex = 15;
            this.RemoveButton.Text = "Remove";
            this.RemoveButton.UseVisualStyleBackColor = true;
            this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // ClearAllButton
            // 
            this.ClearAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ClearAllButton.Location = new System.Drawing.Point(296, 53);
            this.ClearAllButton.Margin = new System.Windows.Forms.Padding(2);
            this.ClearAllButton.Name = "ClearAllButton";
            this.ClearAllButton.Size = new System.Drawing.Size(83, 26);
            this.ClearAllButton.TabIndex = 16;
            this.ClearAllButton.Text = "Clear all";
            this.ClearAllButton.UseVisualStyleBackColor = true;
            this.ClearAllButton.Click += new System.EventHandler(this.ClearAllButton_Click);
            // 
            // ToolTip
            // 
            this.ToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // SigsTreeView
            // 
            this.SigsTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SigsTreeView.Location = new System.Drawing.Point(8, 85);
            this.SigsTreeView.Name = "SigsTreeView";
            this.SigsTreeView.Size = new System.Drawing.Size(371, 127);
            this.SigsTreeView.TabIndex = 17;
            this.SigsTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.SigsTreeView_NodeMouseDoubleClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 287);
            this.Controls.Add(this.SigsTreeView);
            this.Controls.Add(this.ClearAllButton);
            this.Controls.Add(this.RemoveButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.SigMaskTextBox);
            this.Controls.Add(this.AddSigButton);
            this.Controls.Add(this.InstantSearchCheckBox);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.ModuleNameTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SigPatternTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ProcNameTextBox);
            this.MinimumSize = new System.Drawing.Size(406, 326);
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ModuleNameTextBox;
        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.CheckBox InstantSearchCheckBox;
        private System.Windows.Forms.Button AddSigButton;
        private System.Windows.Forms.TextBox SigMaskTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button RemoveButton;
        private System.Windows.Forms.Button ClearAllButton;
        private System.Windows.Forms.ToolTip ToolTip;
        private System.Windows.Forms.TreeView SigsTreeView;
    }
}

