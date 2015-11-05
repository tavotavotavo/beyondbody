using Emgu.CV;
using Emgu.CV.Structure;

namespace Detection.PriorityAlgorithm
{
    public class ProfileFaceItem
    {
        public ProfileFaceItem(bool shouldFlip, double brightnessMultiplier)
        {
            this.ShouldFlip = shouldFlip;
            this.BrightnessMultiplier = brightnessMultiplier;
        }

        public bool ShouldFlip { get; set; }

        public double BrightnessMultiplier { get; set; }
    }
}