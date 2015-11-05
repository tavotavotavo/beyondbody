using Domain;
using Processing.States;
using System;
using System.Collections.Generic;

namespace Processing.Actions
{
    internal class MiniClickAction : CursorAction<FaceState>
    {
        internal MiniClickAction()
            : base()
        {
            this.states = new List<FaceState>
            {
                new NotAboutToMiniClickState(this),
                new InitialMiniClickState(this),
                //finish it
                //new AboutToMiniClickState(this),
                //new ShouldMiniClickState(this)
            };

            this.currentState = this.GetState<NotAboutToMiniClickState>();
        }

        internal override bool ShouldBeExecuted()
        {
            return this.currentState is NotAboutToMiniClickState;
        }

        internal override void Reset()
        {
            this.SetState<NotAboutToMiniClickState>();
        }
    }
}