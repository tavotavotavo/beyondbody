using Common.PriorityAlgorithm;
using Detection.PriorityAlgorithm;
using Domain;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.Collections.Generic;
using System.Linq;

namespace Detectors
{
    public class ProfileFaceDetector : BaseFaceDetector
    {
        private LifoAlgorithm<ProfileFaceItem> priorityAlgorithm;

        public ProfileFaceDetector()
            : base()
        {
            this.priorityAlgorithm = new LifoAlgorithm<ProfileFaceItem>();

            //this.priorityAlgorithm.AddAlgorithmItem(new ProfileFacePriorityItem(new ProfileFaceItem(true, 0.5)));
            //this.priorityAlgorithm.AddAlgorithmItem(new ProfileFacePriorityItem(new ProfileFaceItem(false, 0.5)));
            this.priorityAlgorithm.AddAlgorithmItem(new ProfileFacePriorityItem(new ProfileFaceItem(true, 1)));
            this.priorityAlgorithm.AddAlgorithmItem(new ProfileFacePriorityItem(new ProfileFaceItem(false, 1)));
            //this.priorityAlgorithm.AddAlgorithmItem(new ProfileFacePriorityItem(new ProfileFaceItem(true, 1.5)));
            //this.priorityAlgorithm.AddAlgorithmItem(new ProfileFacePriorityItem(new ProfileFaceItem(false, 1.5)));
        }

        protected override double ScanFactor
        {
            get
            {
                return 1.3;
            }
            set { } 
        }

        protected override int Neighbours { get { return 3; } set { } }

        public override IEnumerable<Face> DetectFaces(Image<Bgr, byte> image)
        {
            var item = this.priorityAlgorithm.Next();
            Image<Bgr, byte> flippedImage = image;

            if (item.ShouldFlip)
            {
                flippedImage = image.Flip(FLIP.HORIZONTAL);
            }

            flippedImage = flippedImage.Mul(item.BrightnessMultiplier);

            var profileFaces = base.DetectFaces(flippedImage);
            
            if (profileFaces.Any())
            {
                this.priorityAlgorithm.SetFirst();

                foreach (var face in profileFaces)
                {
                    face.Image = image;
                    face.IsRightProfile = !item.ShouldFlip;
                    face.IsLeftProfile = item.ShouldFlip;

                    if (face.IsLeftProfile) {
                        face.FlipZoneHorizontally();
                    }
                }

                return profileFaces;
            }

            return new List<Face>();
        }

        protected override string HaarCascadeFileName
        {
            get
            {
                return "haarcascade_profileface.xml";
            }
        }

        internal void Next()
        {
            this.priorityAlgorithm.Next();
        }
    }
}