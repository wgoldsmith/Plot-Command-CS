namespace Plot_Command_CS
{
    partial class PlotForm
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
            this.paperStatGroupBox = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // paperStatGroupBox
            // 
            this.paperStatGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.paperStatGroupBox.Location = new System.Drawing.Point(12, 12);
            this.paperStatGroupBox.Name = "paperStatGroupBox";
            this.paperStatGroupBox.Size = new System.Drawing.Size(300, 100);
            this.paperStatGroupBox.TabIndex = 0;
            this.paperStatGroupBox.TabStop = false;
            this.paperStatGroupBox.Text = "Paper Status";
            // 
            // PlotForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(324, 261);
            this.Controls.Add(this.paperStatGroupBox);
            this.Name = "PlotForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PlotForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox paperStatGroupBox;
    }
}