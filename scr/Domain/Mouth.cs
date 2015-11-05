using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Mouth : ZoneEntity
    {
        public Word Word { get; set; }

        public bool IsMakingGesture { get { return !string.IsNullOrWhiteSpace(this.Word != null ? this.Word.Value : string.Empty); } }

        public void ClearGesture()
        {
            this.Word = new Word(string.Empty);
        }
    }
}
