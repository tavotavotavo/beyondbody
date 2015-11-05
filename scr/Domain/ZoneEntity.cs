using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public abstract class ZoneEntity : BaseEntity
    {
        public Image<Bgr, byte> Image { get; set; }

        public Rectangle Zone { get; set; }

        public override bool IsEmpty
        {
            get { return this.Zone.IsEmpty; }
        }
    }
}
