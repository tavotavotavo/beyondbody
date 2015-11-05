using Emgu.CV;
using Emgu.CV.Structure;

namespace Detection.PriorityAlgorithm
{
    public class ProfileFaceItem
    {
        public ProfileFaceItem(bool shouldFlip)
        {
            this.ShouldFlip = shouldFlip;
        }

        public bool ShouldFlip { get; set; }
    }
}