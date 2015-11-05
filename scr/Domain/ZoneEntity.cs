using Domain.Extensions;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace Domain
{
    public abstract class ZoneEntity : BaseEntity
    {
        public Image<Bgr, byte> Image { get; set; }

        public Rectangle Zone { get; set; }

        public Point Center { get { return this.Zone.Center(); } }

        public override bool IsEmpty
        {
            get { return this.Zone.IsEmpty; }
        }
    }
}