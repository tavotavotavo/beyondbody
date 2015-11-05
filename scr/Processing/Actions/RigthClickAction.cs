using Domain;
using Processing.States;
using System;
using System.Collections.Generic;

namespace Processing.Actions
{
    internal class RigthClickAction : CursorAction<FaceState>
    {
        internal RigthClickAction(Action action) : base(action)
        {
            this.states = new List<FaceState>
            {
                new NotAboutToRightClickState(this),
                new InitialRightClickState(this),
                new AboutToRightClickState(this),
                new ShouldRightClickState(this)
            };

            this.currentState = this.GetState<NotAboutToRightClickState>();
        }

        internal override bool ShouldBeExecuted()
        {
            return this.currentState is ShouldRightClickState;
        }

        internal override void Reset()
        {
            this.SetState<NotAboutToRightClickState>();
        }
    }
}