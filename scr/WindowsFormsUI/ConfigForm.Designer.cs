namespace WindowsFormsUI
{
    partial class ConfigForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.imageBox1 = new Emgu.CV.UI.ImageBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.imageBox2 = new Emgu.CV.UI.ImageBox();
            this.imageBox3 = new Emgu.CV.UI.ImageBox();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.imageBox4 = new Emgu.CV.UI.ImageBox();
            this.imageBox6 = new Emgu.CV.UI.ImageBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.imageBox5 = new Emgu.CV.UI.ImageBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.textBoxUser = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DeleteGestures = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox3)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox5)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Tiene Lentes";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(10, 19);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.HasGlasses_CheckedChanged);
            // 
            // imageBox1
            // 
            this.imageBox1.Location = new System.Drawing.Point(10, 75);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(77, 100);
            this.imageBox1.TabIndex = 3;
            this.imageBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(10, 181);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(77, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Tomar Foto";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.TakeGesture1Photo);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(93, 181);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(77, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Tomar Foto";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.TakeGesture2Photo);
            // 
            // imageBox2
            // 
            this.imageBox2.Location = new System.Drawing.Point(93, 75);
            this.imageBox2.Name = "imageBox2";
            this.imageBox2.Size = new System.Drawing.Size(77, 100);
            this.imageBox2.TabIndex = 6;
            this.imageBox2.TabStop = false;
            // 
            // imageBox3
            // 
            this.imageBox3.Location = new System.Drawing.Point(176, 75);
            this.imageBox3.Name = "imageBox3";
            this.imageBox3.Size = new System.Drawing.Size(77, 100);
            this.imageBox3.TabIndex = 6;
            this.imageBox3.TabStop = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(176, 181);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(77, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "Tomar Foto";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.TakeGesture3Photo);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxUser);
            this.groupBox1.Controls.Add(this.textBox6);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.textBox5);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.button6);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.textBox4);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.imageBox4);
            this.groupBox1.Controls.Add(this.imageBox6);
            this.groupBox1.Controls.Add(this.imageBox1);
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.imageBox3);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.imageBox5);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.imageBox2);
            this.groupBox1.Location = new System.Drawing.Point(12, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(513, 217);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Terminaciones";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(425, 49);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(77, 20);
            this.textBox6.TabIndex = 9;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(176, 49);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(77, 20);
            this.textBox3.TabIndex = 9;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(342, 49);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(77, 20);
            this.textBox5.TabIndex = 9;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(93, 49);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(77, 20);
            this.textBox2.TabIndex = 9;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(425, 181);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(77, 23);
            this.button6.TabIndex = 7;
            this.button6.Text = "Tomar Foto";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.TakeGesture6Photo);
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(259, 49);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(77, 20);
            this.textBox4.TabIndex = 9;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(10, 49);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(77, 20);
            this.textBox1.TabIndex = 9;
            // 
            // imageBox4
            // 
            this.imageBox4.Location = new System.Drawing.Point(259, 75);
            this.imageBox4.Name = "imageBox4";
            this.imageBox4.Size = new System.Drawing.Size(77, 100);
            this.imageBox4.TabIndex = 3;
            this.imageBox4.TabStop = false;
            // 
            // imageBox6
            // 
            this.imageBox6.Location = new System.Drawing.Point(425, 75);
            this.imageBox6.Name = "imageBox6";
            this.imageBox6.Size = new System.Drawing.Size(77, 100);
            this.imageBox6.TabIndex = 6;
            this.imageBox6.TabStop = false;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(342, 181);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(77, 23);
            this.button5.TabIndex = 7;
            this.button5.Text = "Tomar Foto";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.TakeGesture5Photo);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(259, 181);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(77, 23);
            this.button4.TabIndex = 4;
            this.button4.Text = "Tomar Foto";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.TakeGesture4Photo);
            // 
            // imageBox5
            // 
            this.imageBox5.Location = new System.Drawing.Point(342, 75);
            this.imageBox5.Name = "imageBox5";
            this.imageBox5.Size = new System.Drawing.Size(77, 100);
            this.imageBox5.TabIndex = 6;
            this.imageBox5.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(513, 45);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Configuración General";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(450, 295);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 10;
            this.button7.Text = "Guardar";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(107, 295);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(89, 23);
            this.button8.TabIndex = 10;
            this.button8.Text = "Cancelar";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // textBoxUser
            // 
            this.textBoxUser.Location = new System.Drawing.Point(59, 19);
            this.textBoxUser.Name = "textBoxUser";
            this.textBoxUser.Size = new System.Drawing.Size(77, 20);
            this.textBoxUser.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Usuario:";
            // 
            // DeleteGestures
            // 
            this.DeleteGestures.Location = new System.Drawing.Point(12, 295);
            this.DeleteGestures.Name = "DeleteGestures";
            this.DeleteGestures.Size = new System.Drawing.Size(89, 23);
            this.DeleteGestures.TabIndex = 10;
            this.DeleteGestures.Text = "Borrar Gestos";
            this.DeleteGestures.UseVisualStyleBackColor = true;
            this.DeleteGestures.Click += new System.EventHandler(this.DeleteGestures_Click);
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 330);
            this.ControlBox = false;
            this.Controls.Add(this.DeleteGestures);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigForm";
            this.Text = "Configuración";
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox3)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox5)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox1;
        private Emgu.CV.UI.ImageBox imageBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private Emgu.CV.UI.ImageBox imageBox2;
        private Emgu.CV.UI.ImageBox imageBox3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox1;
        private Emgu.CV.UI.ImageBox imageBox4;
        private Emgu.CV.UI.ImageBox imageBox6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private Emgu.CV.UI.ImageBox imageBox5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxUser;
        private System.Windows.Forms.Button DeleteGestures;
    }
}