namespace SigScanner
{
    partial class ProcessSelectForm
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
            this.ProcessListView = new System.Windows.Forms.ListView();
            this.SelectProcessButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ProcessListView
            // 
            this.ProcessListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.ProcessListView.Location = new System.Drawing.Point(12, 12);
            this.ProcessListView.MultiSelect = false;
            this.ProcessListView.Name = "ProcessListView";
            this.ProcessListView.Size = new System.Drawing.Size(228, 199);
            this.ProcessListView.TabIndex = 0;
            this.ProcessListView.UseCompatibleStateImageBehavior = false;
            // 
            // SelectProcessButton
            // 
            this.SelectProcessButton.Location = new System.Drawing.Point(12, 215);
            this.SelectProcessButton.Name = "SelectProcessButton";
            this.SelectProcessButton.Size = new System.Drawing.Size(228, 23);
            this.SelectProcessButton.TabIndex = 2;
            this.SelectProcessButton.Text = "Select";
            this.SelectProcessButton.UseVisualStyleBackColor = true;
            this.SelectProcessButton.Click += new System.EventHandler(this.SelectProcessButton_Click);
            // 
            // ProcessSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(252, 250);
            this.Controls.Add(this.SelectProcessButton);
            this.Controls.Add(this.ProcessListView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ProcessSelectForm";
            this.Text = "Process Selection";
            this.Load += new System.EventHandler(this.ProcessSelectForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView ProcessListView;
        private System.Windows.Forms.Button SelectProcessButton;
    }
}