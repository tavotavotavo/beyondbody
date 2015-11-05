using DetectorsResult;
using Domain;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detectors
{
    public class FrontalFaceDetector : BaseFaceDetector
    {
        protected override string HaarCascadeFileNale
        {
            get
            {
                return "haarcascade_frontalface_default.xml";
            }
        }

        protected override double ScanFactor { get { return 1.5; } }

        protected override int Neighbours { get { return 3; } }

        public FrontalFaceDetector()
            : base()
        {
        }

        public override IEnumerable<Face> DetectFaces(Image<Bgr, byte> image)
        {
            var result = base.DetectFaces(image);

            result.ToList().ForEach(x => x.IsFrontal = true);

            return result;
        }
    }
}