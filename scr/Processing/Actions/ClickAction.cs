using Domain;
using Processing.States;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Processing.Actions
{
    internal abstract class ClickAction : CursorAction<FaceState>
    {
        public ClickAction(Action action)
            : base(action)
        {
        }

        internal override void NextState(Face face)
        {
            if (!face.HasEyesCentered)
            {
                this.Reset();
            }
            else 
            {
                base.NextState(face);
            }
        }

        public long FakeClicks { get; set; }

        public long Clicks { get; set; }
    }
}