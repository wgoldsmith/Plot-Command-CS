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
            this.buttonOk = new System.Windows.Forms.Button();
            this.paperStatGroupBox = new System.Windows.Forms.GroupBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.temp = new System.Windows.Forms.Label();
            this.SuspendLayout();          
            // 
            // paperStatGroupBox
            // 
            this.paperStatGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.paperStatGroupBox.Location = new System.Drawing.Point(12, 12);
            this.paperStatGroupBox.Name = "paperStatGroupBox";
            this.paperStatGroupBox.Size = new System.Drawing.Size(320, 100);
            this.paperStatGroupBox.TabIndex = 2;
            this.paperStatGroupBox.TabStop = false;
            this.paperStatGroupBox.Text = "Paper Status";
            // 
            // buttonOk
            // 
            //                                                     upper left corner of groupBox   +    height of groupBox   =   bottom left corner of groupBox, then add 20
            this.buttonOk.Location = new System.Drawing.Point(100, this.paperStatGroupBox.Location.Y + this.paperStatGroupBox.Size.Height + 20);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(60, 23);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            //                                                         upper left corner of groupBox   +    height of groupBox   =   bottom left corner of groupBox, then add 20
            this.buttonCancel.Location = new System.Drawing.Point(185, this.paperStatGroupBox.Location.Y + this.paperStatGroupBox.Size.Height + 20);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;


            this.temp.Location = new System.Drawing.Point(250, 220);
            this.temp.Name = "temp";
            this.temp.TabIndex = 1;
            this.temp.Text = printerList[1].Text;


            // 
            // PlotForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(344, 261);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.paperStatGroupBox);

            this.Controls.Add(this.temp);
            //            this.Controls.Add(printerList[1]);


            this.Name = "PlotForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PlotForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox paperStatGroupBox;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label temp;
    }
}