using Detection.PriorityAlgorithm;
using Domain;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Collections.Generic;
using System.Linq;

namespace Detectors
{
    public class FaceDetector
    {
        private LifoAlgorithm<BaseFaceDetector> facePriority;
        //Implement priority

        public FaceDetector()
        {
            this.facePriority = new LifoAlgorithm<BaseFaceDetector>();

            this.facePriority.AddAlgorithmItem(new FaceDetectorPriority(new FrontalFaceDetector()));
            this.facePriority.AddAlgorithmItem(new FaceDetectorPriority(new ProfileFaceDetector()));
            this.facePriority.AddAlgorithmItem(new FaceDetectorPriority(new RotatedFaceDetector()));
        }

        public IEnumerable<Face> DetectFaces(Image<Bgr, byte> image)
        {
            IEnumerable<Face> detectedFaces = new List<Face>();

            for (int i = 0; i < this.facePriority.GetCount(); i++)
            {
                BaseFaceDetector faceDetector = this.facePriority.Next();

                detectedFaces = faceDetector.DetectFaces(image);

                if (detectedFaces.Any())
                {
                    this.facePriority.SetFirst();

                    break;
                }
            }

            return detectedFaces;
        }
    }
}