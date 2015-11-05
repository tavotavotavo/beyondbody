using Emgu.CV;
using Emgu.CV.Structure;

namespace Detection.PriorityAlgorithm
{
    public class RotatedFaceItem
    {
        public RotatedFaceItem(int angle)
        {
            this.Angle = angle;
        }

        public int Angle { get; set; }

        public bool IsLeft { get { return this.Angle < 0; } }

        public bool IsRight { get { return this.Angle > 0; } }
    }
}