using Detectors;
using Domain;
using Emgu.CV.CvEnum;
using Emgu.CV.UI;
using ProcessingInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsUI
{
    public partial class ConfigForm : Form
    {
        private IMainProcessor processor;
        private List<Gesture> gestures;
        private GesturesService gesturesService;

        public ConfigForm(IMainProcessor processor, GesturesService gesturesService)
        {
            this.processor = processor;
            this.gesturesService = gesturesService;
            this.gestures = new List<Gesture>();

            InitializeComponent();
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void HasGlasses_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                this.processor.WithGlasses();
            }
            else
            {
                this.processor.WithoutGlasses();
            }
        }

        private void TakeMentePhoto(object sender, EventArgs e)
        {
        }

        private void TakeGesture1Photo(object sender, EventArgs e)
        {
            this.GetMouthPhotoFor(1);
        }

        private void TakeGesture2Photo(object sender, EventArgs e)
        {
            this.GetMouthPhotoFor(2);
        }

        private void TakeGesture3Photo(object sender, EventArgs e)
        {
            this.GetMouthPhotoFor(3);
        }

        private void TakeGesture4Photo(object sender, EventArgs e)
        {
            this.GetMouthPhotoFor(4);
        }

        private void TakeGesture5Photo(object sender, EventArgs e)
        {
            this.GetMouthPhotoFor(5);
        }

        private void TakeGesture6Photo(object sender, EventArgs e)
        {
            this.GetMouthPhotoFor(6);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GetMouthPhotoFor(int number)
        {
            var image = this.processor.GetCurrentMouthImage();

            var imageBox = (ImageBox)this.Controls.Find("imageBox" + number.ToString(), true).FirstOrDefault();
            var textBox = (TextBox)this.Controls.Find("textBox" + number.ToString(), true).FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(textBox.Text))
            {
                if (image != null)
                {
                    imageBox.Image = image.Resize(imageBox.Width, imageBox.Height, INTER.CV_INTER_LINEAR);

                    textBox.Text = textBox.Text.Trim();

                    this.gestures.Add(new Gesture()
                    {
                        Image = image,
                        Word = new Word(textBox.Text)
                    });
                }
                else
                {
                    MessageBox.Show("Tome otra vez la foto por favor");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Debe especificar la terminación");
                return;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                this.processor.WaitForActionAndExecute(() =>
                {
                    this.processor.RegisterGestures(this.textBoxUser.Text.Trim(), this.gestures);
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            MessageBox.Show("Configuración guardada correctamente");

            this.Close();
        }

        private void DeleteGestures_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.textBoxUser.Text))
            {
                var result = MessageBox.Show("Esta seguro que desea eliminar todos los gestos del usuario " + this.textBoxUser.Text,
                    "Eliminación de Gestos",
                    MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        this.processor.WaitForActionAndExecute(() =>
                        {
                            this.gesturesService.DeleteGestures(this.textBoxUser.Text);
                        });
                        this.gestures.Clear();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe introducir un usuario");
            }
        }
    }
}