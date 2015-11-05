using Emgu.CV;
using Emgu.CV.Structure;

namespace Detectors
{
    public class MouthDetector : FrontalFaceDetector
    {
        public MouthDetector()
            : base()
        {
        }

        protected override double ScanFactor
        {
            get
            {
                return 1.1;
            }
        }

        protected override int Neighbours
        {
            get
            {
                return 20;
            }
        }

        protected override string HaarCascadeFileNale
        {
            get
            {
                return "haarcascade_smile.xml";
            }
        }
    }
}