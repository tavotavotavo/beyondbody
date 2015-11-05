using Processing.States;
using System;
using System.Linq;

namespace Processing.Actions
{
    internal class LeftDoubleClickAction : LeftClickAction
    {
        internal LeftDoubleClickAction(Action action)
            : base(action)
        {
            this.states.Add(new InitialLeftDoubleClickState(this));
            this.states.Add(new AboutToLeftDoubleClickState(this));
            this.states.Add(new ShouldLeftDoubleClickState(this));
        }

        internal override bool ShouldBeExecuted()
        {
            return this.currentState is ShouldLeftDoubleClickState; //cambiar
        }

        internal override void Reset()
        {
            this.SetState<NotAboutToLeftClickState>();
        }
    }
}