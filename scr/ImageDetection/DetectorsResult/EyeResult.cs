using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace DetectorsResult
{
    public class EyeResult
    {
        public EyeResult(Rectangle zone)
        {
            this.Zone = zone;
        }

        public Rectangle Zone { get; private set; }

        public Rectangle ClosedZone { get; set; }

        public Image<Bgr, byte> EyeImage { get; set; }

        public Rectangle RightCornerZone { get; set; }

        public Rectangle LeftCornerZone { get; set; }

        public Rectangle PupilZone { get; set; }

        public bool HasRightCorner { get { return !this.RightCornerZone.IsEmpty; } }

        public bool HasLeftCorner { get { return !this.LeftCornerZone.IsEmpty; } }

        public bool HasPupil { get { return !this.PupilZone.IsEmpty; } }

        public bool IsClosed { get { return !this.ClosedZone.IsEmpty; } }

        public bool HasZone { get { return !this.Zone.IsEmpty; } }
    }
}