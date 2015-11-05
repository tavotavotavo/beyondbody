using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Mouth : ZoneEntity
    {
        public string Word { get; set; }

        public bool IsMakingGesture { get { return !string.IsNullOrWhiteSpace(this.Word); } }

        public void ClearGesture()
        {
            this.Word = string.Empty;
        }
    }
}
