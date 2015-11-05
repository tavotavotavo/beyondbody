using Domain;
using Processing.States;
using System;
using System.Collections.Generic;

namespace Processing.Actions
{
    internal class DeactivateAction : KeyboardAction<FaceState>
    {
        internal DeactivateAction() : base()
        {
            this.states = new List<FaceState>()
            {
                new NotAboutToDeactivateState(this),
                new InitialDeactivateState(this),
                new ShouldDeactivateState(this)
            };

            this.currentState = this.GetState<NotAboutToDeactivateState>();
        }

        internal override bool ShouldBeExecuted()
        {
            return this.currentState is ShouldDeactivateState;
        }

        internal override void Reset()
        {
            this.SetState<NotAboutToDeactivateState>();
        }
    }
}