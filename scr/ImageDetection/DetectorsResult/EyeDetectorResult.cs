using System.Drawing;

namespace DetectorsResult
{
    public class EyeDetectorResult : DetectorResult
    {
        public EyeDetectorResult(Rectangle rightEyeZone, Rectangle leftEyeZone, DetectorResult faceResult)
        {
            this.RightEye = new EyeResult(rightEyeZone);
            this.LeftEye = new EyeResult(leftEyeZone);
            this.HasValue = !faceResult.IsEmpty;
        }

        public EyeResult RightEye { get; private set; }

        public EyeResult LeftEye { get; private set; }

        public bool HasValue { get; private set; }
    }
}