using Common.PriorityAlgorithm;
using Detectors;

namespace Detection.PriorityAlgorithm
{
    public class FaceDetectorPriority : PriorityItem<BaseFaceDetector>
    {
        public FaceDetectorPriority(BaseFaceDetector detector)
            : base(detector, 0)
        {
        }
    }
}