using Domain;
using Processing.States;
using System.Collections.Generic;
using System.Linq;

namespace Processing.Actions
{
    internal abstract class BaseAction<TState, TDomain> where TState : State<TDomain>
    {
        protected IList<TState> states;
        protected TState currentState;

        internal T GetState<T>() where T : TState
        {
            return this.states.OfType<T>().First();
        }

        internal void SetState<T>() where T : TState
        {
            this.currentState = this.GetState<T>();
        }

        internal void NextState(TDomain face)
        {
            this.currentState.Next(face);
        }

        internal abstract bool ShouldBeExecuted();

        internal abstract void Reset();
    }
}