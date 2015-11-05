using Domain.Exceptions;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using ProcessingInterfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace WindowsFormsUI
{
    public class ImageItem
    {
        public Image<Bgr, byte> OriginalImage { get; set; }

        public ImageBox ImageBox { get; set; }
    }

    public partial class RegisterForm : Form
    {
        private IMainProcessor processor;
        private List<ImageItem> images;
        private int currentIndex;

        public RegisterForm(IMainProcessor processor)
        {
            this.processor = processor;
            this.images = new List<ImageItem>();

            InitializeComponent();
        }

        private void completeRegistrationButton_Click(object sender, EventArgs e)
        {
            if (nameTextBox.TextLength == 0)
            {
                MessageBox.Show("Ingrese un nombre de usuario");
                return;
            }

            if (!this.images.Any() || this.images.All(x => x == null))
            {
                this.images.Clear();
                MessageBox.Show("Ingrese por lo menos una foto");
                return;
            }

            try
            {
                this.processor.RegisterUser(nameTextBox.Text, this.images.Select(x => x.OriginalImage));
            }
            catch (RegisterException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            MessageBox.Show("Registrado exitosamente");
            this.Close();
        }

        private void takePhotosButton_Click(object sender, EventArgs e)
        {
            this.SetCurrentIndex();

            ImageBox imageBox = (ImageBox)this.Controls.Find("imageBox" + this.currentIndex.ToString(), true).FirstOrDefault();

            var item = new ImageItem() { ImageBox = imageBox, OriginalImage = this.processor.GetCurrentFaceImage() };

            imageBox.Image = item.OriginalImage.Resize(imageBox.Width, imageBox.Height, INTER.CV_INTER_LINEAR);

            this.images.Add(item);

            this.currentIndex++;
        }

        public object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        private void helpButton2_Click(object sender, EventArgs e)
        {
            var path = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)) + "\\ThirdPartyPrograms\\" + "Beyond Body.chm";

            var process = Process.Start(path);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.textBox1.Enabled = this.checkBox1.Checked;
        }

        private void DeletePhotos_Click(object sender, EventArgs e)
        {
            if (!this.checkBox1.Checked)
            {
                foreach (var imageBox in this.images.Select(x => x.ImageBox))
                {
                    if (imageBox != null)
                        imageBox.Image = null;
                }

                this.images.Clear();
            }
            else
            {
                this.SetCurrentIndex();

                ImageBox imageBox = (ImageBox)this.Controls.Find("imageBox" + this.currentIndex.ToString(), true).FirstOrDefault();

                var item = this.images.Where(x => x.ImageBox == imageBox).FirstOrDefault();
                if (item != null)
                {
                    item.ImageBox.Image = null;
                    this.images.Remove(item);
                }
                this.currentIndex++;
            }
        }

        private void SetCurrentIndex()
        {
            int number;

            if (this.checkBox1.Checked && int.TryParse(this.textBox1.Text, out number))
            {
                this.currentIndex = number - 1;
            }

            if (this.currentIndex > 19 || this.currentIndex < 0)
            {
                this.currentIndex = 0;
            }
        }
    }
}