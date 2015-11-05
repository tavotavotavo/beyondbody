using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Gesture
    {
        public Image<Bgr, byte> Image { get; set; }

        public Word Word { get; set; }
    }
}
