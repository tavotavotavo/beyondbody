using Domain;
using Processing.States;
using System;
using System.Collections.Generic;

namespace Processing.Actions
{
    internal class ActivateSpeechAction : KeyboardAction<FaceState>
    {
        internal ActivateSpeechAction()
        {
            this.states = new List<FaceState>()
            {
                new NotAboutToActivateSpeechState(this),
                new InitialActivateSpeechState(this),
                new AboutToActivateSpeechState(this),
                new AboutToAbortSpeechState(this),
                new ShouldActivateSpeechState(this)
            };

            this.currentState = this.GetState<NotAboutToActivateSpeechState>();
        }

        internal override bool ShouldBeExecuted()
        {
            return this.currentState is ShouldActivateSpeechState;
        }

        internal override void Reset()
        {
            this.SetState<NotAboutToActivateSpeechState>();
        }
    }
}