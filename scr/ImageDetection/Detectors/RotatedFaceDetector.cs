using Common.PriorityAlgorithm;
using Detection.PriorityAlgorithm;
using Domain;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Detectors
{
    public class RotatedFaceDetector : BaseFaceDetector
    {
        private LifoAlgorithm<RotatedFaceItem> priorityAlgorithm;
        private double currentAngle;

        public RotatedFaceDetector()
            : base()
        {
            this.priorityAlgorithm = new LifoAlgorithm<RotatedFaceItem>();

            this.priorityAlgorithm.AddAlgorithmItem(new RotatedFacePriorityItem(new RotatedFaceItem(19)));
            this.priorityAlgorithm.AddAlgorithmItem(new RotatedFacePriorityItem(new RotatedFaceItem(-19)));
            this.priorityAlgorithm.AddAlgorithmItem(new RotatedFacePriorityItem(new RotatedFaceItem(18)));
            this.priorityAlgorithm.AddAlgorithmItem(new RotatedFacePriorityItem(new RotatedFaceItem(-18)));
            this.priorityAlgorithm.AddAlgorithmItem(new RotatedFacePriorityItem(new RotatedFaceItem(20)));
            this.priorityAlgorithm.AddAlgorithmItem(new RotatedFacePriorityItem(new RotatedFaceItem(-20)));
            this.priorityAlgorithm.AddAlgorithmItem(new RotatedFacePriorityItem(new RotatedFaceItem(22)));
            this.priorityAlgorithm.AddAlgorithmItem(new RotatedFacePriorityItem(new RotatedFaceItem(-22)));
            this.priorityAlgorithm.AddAlgorithmItem(new RotatedFacePriorityItem(new RotatedFaceItem(24)));
            this.priorityAlgorithm.AddAlgorithmItem(new RotatedFacePriorityItem(new RotatedFaceItem(-24)));
            this.priorityAlgorithm.AddAlgorithmItem(new RotatedFacePriorityItem(new RotatedFaceItem(26)));
            this.priorityAlgorithm.AddAlgorithmItem(new RotatedFacePriorityItem(new RotatedFaceItem(-26)));
        }

        protected override int Neighbours { get { return 2; } set { } }

        protected override string HaarCascadeFileName
        {
            get
            {
                return "haarcascade_frontalface_alt.xml";
            }
        }

        protected override double ScanFactor
        {
            get
            {
                return 1.3;
            }
            set { } 
        }

        protected override Image<Bgr, byte> AfterReducingImage(Image<Bgr, byte> reducedImage)
        {
            reducedImage = reducedImage.Rotate(
                    this.GetAngle(),
                    new Bgr(Color.Black), true);

            return base.AfterReducingImage(reducedImage);
        }

        public override IEnumerable<Face> DetectFaces(Image<Bgr, byte> image)
        {
            IEnumerable<Face> rotatedFaces = new List<Face>();

            var nextItem = this.priorityAlgorithm.Next();

            this.SetAngle(nextItem.Angle);
            rotatedFaces = base.DetectFaces(image);

            if (rotatedFaces.Any())
            {
                foreach (var face in rotatedFaces)
                {
                    face.Image = image;
                    face.IsRightRotated = nextItem.IsRight;
                    face.IsLeftRotated = nextItem.IsLeft;
                }

                this.priorityAlgorithm.SetFirst();

            }

            return rotatedFaces;
        }

        private double GetAngle()
        {
            return this.currentAngle;
        }

        private void SetAngle(int angle)
        {
            this.currentAngle = angle;
        }
    }
}