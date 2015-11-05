using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using ProcessingInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeyondBody
{
    public partial class FormMain : Form
    {
        private IMainProcessor processor;

        public FormMain(IMainProcessor processor)
        {
            this.processor = processor;
            InitializeComponent();
        }

        private void FormMain_Closed(object sender, FormClosedEventArgs e)
        {
            this.processor.CloseAllProcesses();
        }

        public void ShowOriginalImage(Image<Bgr, byte> drawedImage)
        {
            this.OriginalImageViewer.Image = drawedImage.Resize(320, 240, INTER.CV_INTER_LINEAR);
        }

        public void ShowRightEye(Image<Bgr, byte> rightEye)
        {
            this.ShowEye(rightEye, this.rightEyeBox);
        }

        public void ShowLeftEye(Image<Bgr, byte> leftEye)
        {
            this.ShowEye(leftEye, this.leftEyeBox);
        }

        public void ShowRightBitMapEye(Image<Gray, byte> rightEye)
        {
            this.ShowBitMapEye(rightEye, this.rightBitMapEyeBox);
        }

        public void ShowLeftBitMapEye(Image<Gray, byte> leftEye)
        {
            this.ShowBitMapEye(leftEye, this.leftBitMapEyeBox);
        }

        private void ShowBitMapEye(Image<Gray, byte> eye, ImageBox imageBox)
        {
            imageBox.Width = eye.ROI.Width * 2;
            imageBox.Height = eye.ROI.Height * 2;
            imageBox.Image = eye;
        }

        private void ShowEye(Image<Bgr, byte> eye, ImageBox imageBox)
        {
            imageBox.Width = eye.ROI.Width * 2;
            imageBox.Height = eye.ROI.Height * 2;
            imageBox.Image = eye;
        }
    }
}
