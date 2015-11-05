using Emgu.CV.UI;

namespace BeyondBody
{
    partial class FormMain
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
            this.OriginalImageViewer = new ImageBox();
            this.rightEyeBox = new ImageBox();
            this.leftEyeBox = new ImageBox();
            this.rightBitMapEyeBox = new ImageBox();
            this.leftBitMapEyeBox = new ImageBox();
            ((System.ComponentModel.ISupportInitialize)(this.OriginalImageViewer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightEyeBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftEyeBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightBitMapEyeBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftBitMapEyeBox)).BeginInit();
            this.SuspendLayout();
            // 
            // OriginalImageViewer
            // 
            this.OriginalImageViewer.Location = new System.Drawing.Point(12, 12);
            this.OriginalImageViewer.Name = "OriginalImageViewer";
            this.OriginalImageViewer.Size = new System.Drawing.Size(320, 240);
            this.OriginalImageViewer.TabIndex = 2;
            this.OriginalImageViewer.TabStop = false;
            // 
            // rightEyeBox
            // 
            this.rightEyeBox.Location = new System.Drawing.Point(338, 12);
            this.rightEyeBox.Name = "rightEyeBox";
            this.rightEyeBox.Size = new System.Drawing.Size(219, 80);
            this.rightEyeBox.TabIndex = 2;
            this.rightEyeBox.TabStop = false;
            // 
            // leftEyeBox
            // 
            this.leftEyeBox.Location = new System.Drawing.Point(338, 184);
            this.leftEyeBox.Name = "leftEyeBox";
            this.leftEyeBox.Size = new System.Drawing.Size(219, 79);
            this.leftEyeBox.TabIndex = 2;
            this.leftEyeBox.TabStop = false;
            // 
            // rightBitMapEyeBox
            // 
            this.rightBitMapEyeBox.Location = new System.Drawing.Point(338, 98);
            this.rightBitMapEyeBox.Name = "rightBitMapEyeBox";
            this.rightBitMapEyeBox.Size = new System.Drawing.Size(219, 80);
            this.rightBitMapEyeBox.TabIndex = 2;
            this.rightBitMapEyeBox.TabStop = false;
            // 
            // leftBitMapEyeBox
            // 
            this.leftBitMapEyeBox.Location = new System.Drawing.Point(696, 247);
            this.leftBitMapEyeBox.Name = "leftBitMapEyeBox";
            this.leftBitMapEyeBox.Size = new System.Drawing.Size(219, 80);
            this.leftBitMapEyeBox.TabIndex = 2;
            this.leftBitMapEyeBox.TabStop = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(948, 361);
            this.Controls.Add(this.leftEyeBox);
            this.Controls.Add(this.leftBitMapEyeBox);
            this.Controls.Add(this.rightBitMapEyeBox);
            this.Controls.Add(this.rightEyeBox);
            this.Controls.Add(this.OriginalImageViewer);
            this.Name = "FormMain";
            this.Text = "Form1";
            this.FormClosed += this.FormMain_Closed;
            ((System.ComponentModel.ISupportInitialize)(this.OriginalImageViewer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightEyeBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftEyeBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightBitMapEyeBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftBitMapEyeBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ImageBox OriginalImageViewer;
        private ImageBox rightEyeBox;
        private ImageBox leftEyeBox;
        private ImageBox rightBitMapEyeBox;
        private ImageBox leftBitMapEyeBox;
    }
}

