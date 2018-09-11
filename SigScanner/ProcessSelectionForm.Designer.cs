namespace SigScanner
{
    partial class ProcessSelectionForm
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
            this.FilterTextBox = new System.Windows.Forms.TextBox();
            this.SelectButton = new System.Windows.Forms.Button();
            this.id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.window = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // ProcessListView
            // 
            this.ProcessListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProcessListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ProcessListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.id,
            this.name,
            this.window});
            this.ProcessListView.FullRowSelect = true;
            this.ProcessListView.GridLines = true;
            this.ProcessListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.ProcessListView.Location = new System.Drawing.Point(12, 12);
            this.ProcessListView.MultiSelect = false;
            this.ProcessListView.Name = "ProcessListView";
            this.ProcessListView.Size = new System.Drawing.Size(356, 350);
            this.ProcessListView.TabIndex = 0;
            this.ProcessListView.UseCompatibleStateImageBehavior = false;
            this.ProcessListView.View = System.Windows.Forms.View.Details;
            // 
            // FilterTextBox
            // 
            this.FilterTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FilterTextBox.Location = new System.Drawing.Point(12, 368);
            this.FilterTextBox.Name = "FilterTextBox";
            this.FilterTextBox.Size = new System.Drawing.Size(279, 20);
            this.FilterTextBox.TabIndex = 1;
            this.FilterTextBox.TextChanged += new System.EventHandler(this.FilterTextBox_TextChanged);
            // 
            // SelectButton
            // 
            this.SelectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectButton.Location = new System.Drawing.Point(297, 366);
            this.SelectButton.Name = "SelectButton";
            this.SelectButton.Size = new System.Drawing.Size(71, 23);
            this.SelectButton.TabIndex = 2;
            this.SelectButton.Text = "Select";
            this.SelectButton.UseVisualStyleBackColor = true;
            this.SelectButton.Click += new System.EventHandler(this.SelectButton_Click);
            // 
            // id
            // 
            this.id.Text = "ID";
            this.id.Width = 50;
            // 
            // name
            // 
            this.name.Text = "NAME";
            this.name.Width = 140;
            // 
            // window
            // 
            this.window.Text = "WINDOW";
            this.window.Width = 140;
            // 
            // ProcessSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 398);
            this.Controls.Add(this.SelectButton);
            this.Controls.Add(this.FilterTextBox);
            this.Controls.Add(this.ProcessListView);
            this.MinimumSize = new System.Drawing.Size(259, 347);
            this.Name = "ProcessSelectionForm";
            this.Text = "Select Process";
            this.SizeChanged += new System.EventHandler(this.ProcessSelectionForm_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView ProcessListView;
        private System.Windows.Forms.TextBox FilterTextBox;
        private System.Windows.Forms.Button SelectButton;
        private System.Windows.Forms.ColumnHeader id;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.ColumnHeader window;
    }
}