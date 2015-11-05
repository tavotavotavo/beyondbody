using Domain;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsUI
{
    public partial class TrainingBox : Form
    {
        public TrainingBox()
        {
            InitializeComponent();
        }

        public void ShowOriginalImage(Image<Bgr, byte> drawedImage, Face face)
        {
            var imageToShow = drawedImage.Copy();

            //if (!face.IsEmpty)
            //{
            //    imageToShow.ROI = face.Zone;
            //}

            imageToShow = imageToShow.Flip(FLIP.HORIZONTAL);

            this.OriginalImageViewer.Image = imageToShow.Resize(this.OriginalImageViewer.Width, this.OriginalImageViewer.Height, INTER.CV_INTER_LINEAR);

            this.SetPoint(new Point()
            {
                X = Cursor.Position.X + 12,
                Y = Cursor.Position.Y + 12
            });
        }

        delegate void SetTextCallback(Point point);

        private void SetPoint(Point point)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetPoint);
                this.Invoke(d, new object[] { point });
            }
            else
            {
                this.Location = point;
            }
        }
        private void TrainingBox_Load(object sender, EventArgs e)
        {
        }
    }
}