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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.registerButton = new System.Windows.Forms.Button();
            this.configButton = new System.Windows.Forms.Button();
            this.helpButton = new System.Windows.Forms.Button();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.OriginalImageViewer = new Emgu.CV.UI.ImageBox();
            ((System.ComponentModel.ISupportInitialize)(this.OriginalImageViewer)).BeginInit();
            this.SuspendLayout();
            // 
            // registerButton
            // 
            this.registerButton.Location = new System.Drawing.Point(326, 12);
            this.registerButton.Name = "registerButton";
            this.registerButton.Size = new System.Drawing.Size(121, 23);
            this.registerButton.TabIndex = 3;
            this.registerButton.Text = "Registrarse";
            this.registerButton.UseVisualStyleBackColor = true;
            this.registerButton.Click += new System.EventHandler(this.registerButton_Click);
            // 
            // configButton
            // 
            this.configButton.Location = new System.Drawing.Point(326, 41);
            this.configButton.Name = "configButton";
            this.configButton.Size = new System.Drawing.Size(121, 23);
            this.configButton.TabIndex = 4;
            this.configButton.Text = "Configurar";
            this.configButton.UseVisualStyleBackColor = true;
            this.configButton.Click += new System.EventHandler(this.configButton_Click);
            // 
            // helpButton
            // 
            this.helpButton.Location = new System.Drawing.Point(326, 99);
            this.helpButton.Name = "helpButton";
            this.helpButton.Size = new System.Drawing.Size(121, 23);
            this.helpButton.TabIndex = 5;
            this.helpButton.Text = "Ayuda";
            this.helpButton.UseVisualStyleBackColor = true;
            this.helpButton.Click += new System.EventHandler(this.helpButton_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipText = "Beyond Body se esta ejecutando en segundo plano";
            this.notifyIcon.BalloonTipTitle = "Beyond Body";
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Beyond Body";
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(326, 70);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Ocultar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(326, 205);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(121, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "Borrar Usuarios";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.RemoveUsersButton_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(326, 156);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(102, 17);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "Entrenar Gestos";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // OriginalImageViewer
            // 
            this.OriginalImageViewer.Location = new System.Drawing.Point(12, 12);
            this.OriginalImageViewer.Name = "OriginalImageViewer";
            this.OriginalImageViewer.Size = new System.Drawing.Size(308, 216);
            this.OriginalImageViewer.TabIndex = 2;
            this.OriginalImageViewer.TabStop = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 240);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.helpButton);
            this.Controls.Add(this.configButton);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.registerButton);
            this.Controls.Add(this.OriginalImageViewer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.ShowInTaskbar = false;
            this.Text = "Beyond Body";
            this.Load += new System.EventHandler(this.FormMain_Load_1);
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.OriginalImageViewer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ImageBox OriginalImageViewer;
        private System.Windows.Forms.Button registerButton;
        private System.Windows.Forms.Button configButton;
        private System.Windows.Forms.Button helpButton;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

