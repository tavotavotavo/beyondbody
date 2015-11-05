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
        public CursorAction(Action action)
            : base(action)
        {
        }
        public CursorAction()
            : base()
        {
        }
    }
}