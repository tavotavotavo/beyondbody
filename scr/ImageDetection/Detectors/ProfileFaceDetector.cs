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

            this.priorityAlgorithm.AddAlgorithmItem(new ProfileFacePriorityItem(new ProfileFaceItem(true)));
            this.priorityAlgorithm.AddAlgorithmItem(new ProfileFacePriorityItem(new ProfileFaceItem(false)));
        }

        protected override double ScanFactor
        {
            get
            {
                return 1.5;
            }
        }

        protected override int Neighbours { get { return 3; } }

        public override IEnumerable<Face> DetectFaces(Image<Bgr, byte> image)
        {
            for (int i = 0; i < this.priorityAlgorithm.GetCount(); i++)
            {
                var item = this.priorityAlgorithm.Next();
                Image<Bgr, byte> flippedImage = image;

                if (item.ShouldFlip)
                    flippedImage = image.Flip(FLIP.HORIZONTAL);

                var profileFaces = base.DetectFaces(flippedImage);

                if (profileFaces.Any())
                {
                    this.priorityAlgorithm.SetFirst();

                    foreach (var face in profileFaces)
                    {
                        face.Image = image;
                        face.IsRightProfile = !item.ShouldFlip;
                        face.IsLeftProfile = item.ShouldFlip;
                    }

                    return profileFaces;
                }
            }

            return new List<Face>();
        }

        protected override string HaarCascadeFileNale
        {
            get
            {
                return "haarcascade_profileface.xml";
            }
        }
    }
}