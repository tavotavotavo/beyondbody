using Domain;
using Processing.States;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Processing.Actions
{
    internal abstract class CursorAction<TState> : BaseAction<TState, Face>
        where TState : FaceState
    {
        private Action action;

        public CursorAction(Action action)
        {
            this.action = action;
        }
        
        internal void Execute()
        {
            this.action.Invoke();
        }
    }
}