namespace SigScanner
{
    partial class Form1
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
            this.ProcessNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SignatureTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SearchButton = new System.Windows.Forms.Button();
            this.OffsetsListView = new System.Windows.Forms.ListView();
            this.label3 = new System.Windows.Forms.Label();
            this.ModuleNameTextBox = new System.Windows.Forms.TextBox();
            this.CheckAllModuleCheckBox = new System.Windows.Forms.CheckBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ProcessNameTextBox
            // 
            this.ProcessNameTextBox.Location = new System.Drawing.Point(24, 48);
            this.ProcessNameTextBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.ProcessNameTextBox.Name = "ProcessNameTextBox";
            this.ProcessNameTextBox.Size = new System.Drawing.Size(178, 31);
            this.ProcessNameTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Process Name:";
            // 
            // SignatureTextBox
            // 
            this.SignatureTextBox.Location = new System.Drawing.Point(218, 48);
            this.SignatureTextBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.SignatureTextBox.Name = "SignatureTextBox";
            this.SignatureTextBox.Size = new System.Drawing.Size(350, 31);
            this.SignatureTextBox.TabIndex = 2;
            this.SignatureTextBox.TextChanged += new System.EventHandler(this.SignatureTextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(212, 17);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Signature:";
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(402, 426);
            this.SearchButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(166, 40);
            this.SearchButton.TabIndex = 4;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // OffsetsListView
            // 
            this.OffsetsListView.Location = new System.Drawing.Point(24, 173);
            this.OffsetsListView.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.OffsetsListView.Name = "OffsetsListView";
            this.OffsetsListView.Size = new System.Drawing.Size(544, 241);
            this.OffsetsListView.TabIndex = 6;
            this.OffsetsListView.UseCompatibleStateImageBehavior = false;
            this.OffsetsListView.DoubleClick += new System.EventHandler(this.OffsetsListView_DoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 92);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 25);
            this.label3.TabIndex = 7;
            this.label3.Text = "Module Name:";
            // 
            // ModuleNameTextBox
            // 
            this.ModuleNameTextBox.Location = new System.Drawing.Point(24, 123);
            this.ModuleNameTextBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.ModuleNameTextBox.Name = "ModuleNameTextBox";
            this.ModuleNameTextBox.Size = new System.Drawing.Size(178, 31);
            this.ModuleNameTextBox.TabIndex = 8;
            // 
            // CheckAllModuleCheckBox
            // 
            this.CheckAllModuleCheckBox.AutoSize = true;
            this.CheckAllModuleCheckBox.Location = new System.Drawing.Point(214, 123);
            this.CheckAllModuleCheckBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.CheckAllModuleCheckBox.Name = "CheckAllModuleCheckBox";
            this.CheckAllModuleCheckBox.Size = new System.Drawing.Size(182, 29);
            this.CheckAllModuleCheckBox.TabIndex = 9;
            this.CheckAllModuleCheckBox.Text = "Entire process";
            this.CheckAllModuleCheckBox.UseVisualStyleBackColor = true;
            this.CheckAllModuleCheckBox.CheckedChanged += new System.EventHandler(this.CheckAllModuleCheckBox_CheckedChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 475);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(556, 23);
            this.progressBar1.TabIndex = 10;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(161, 433);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(232, 29);
            this.checkBox1.TabIndex = 11;
            this.checkBox1.Text = "Search immediately";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(405, 118);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(166, 41);
            this.button1.TabIndex = 12;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1196, 638);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.CheckAllModuleCheckBox);
            this.Controls.Add(this.ModuleNameTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.OffsetsListView);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SignatureTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ProcessNameTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "Form1";
            this.Text = "Simple SigScanner";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ProcessNameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox SignatureTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.ListView OffsetsListView;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ModuleNameTextBox;
        private System.Windows.Forms.CheckBox CheckAllModuleCheckBox;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button1;
    }
}

