using Domain;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Collections.Generic;
using System.Linq;

namespace Detectors
{
    public class FrontalFaceDetector : BaseFaceDetector
    {
        protected override string HaarCascadeFileName
        {
            get
            {
                return "haarcascade_frontalface_alt.xml";
            }
        }

        protected override double ScanFactor { get { return 1.4; } set { } }

        protected override int Neighbours { get { return 2; } set { } }

        public FrontalFaceDetector()
            : base()
        {
            this.HaarCascadeFileNames.Add(this.HaarCascadeFileName);
            //this.HaarCascadeFileNames.Add("haarcascade_frontalface_default.xml");
        }

        protected override MCvAvgComp[] GetFacesVector(Image<Gray, byte> grayImage)
        {
            return this.DetectVarious(grayImage);
        }

        public override IEnumerable<Face> DetectFaces(Image<Bgr, byte> image)
        {
            var result = base.DetectFaces(image);

            result.ToList().ForEach(x => x.IsFrontal = true);

            return result;
        }
    }
}