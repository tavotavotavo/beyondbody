using Domain;
using Processing.States;
using System;
using System.Collections.Generic;

namespace Processing.Actions
{
    internal class LeftClickAction : CursorAction<FaceState>
    {
        internal LeftClickAction(Action action)
            : base(action)
        {
            this.states = new List<FaceState>
            {
                new NotAboutToLeftClickState(this),
                new InitialLeftClickState(this),
                new AboutToLeftClickState(this),
                new ShouldLeftClickState(this)
            };

            this.currentState = this.GetState<NotAboutToLeftClickState>();
        }

        internal override bool ShouldBeExecuted()
        {
            return this.currentState is ShouldLeftClickState;
        }

        internal override void Reset()
        {
            this.SetState<NotAboutToLeftClickState>();
        }
    }
}