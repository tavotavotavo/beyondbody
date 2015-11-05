namespace WindowsFormsUI
{
    partial class TrainingBox
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
            this.components = new System.ComponentModel.Container();
            this.OriginalImageViewer = new Emgu.CV.UI.ImageBox();
            ((System.ComponentModel.ISupportInitialize)(this.OriginalImageViewer)).BeginInit();
            this.SuspendLayout();
            // 
            // OriginalImageViewer
            // 
            this.OriginalImageViewer.Location = new System.Drawing.Point(12, 12);
            this.OriginalImageViewer.Name = "OriginalImageViewer";
            this.OriginalImageViewer.Size = new System.Drawing.Size(76, 74);
            this.OriginalImageViewer.TabIndex = 3;
            this.OriginalImageViewer.TabStop = false;
            // 
            // TrainingBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(100, 98);
            this.Controls.Add(this.OriginalImageViewer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TrainingBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TrainingBox";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.TrainingBox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.OriginalImageViewer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Emgu.CV.UI.ImageBox OriginalImageViewer;
    }
}