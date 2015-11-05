using Domain;
using Processing.States;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Processing.Actions
{
    internal abstract class BaseAction<TState, TDomain> where TState : State<TDomain>
    {
        protected IList<TState> states;
        protected TState currentState;
        protected TDomain currentEntity;
        
        private Action action;

        public BaseAction(Action action)
        {
            this.action = action;
        }

        public BaseAction()
        {
        }
        
        internal void Execute()
        {
            this.action.Invoke();
        }

        internal T GetState<T>() where T : TState
        {
            return this.states.OfType<T>().First();
        }

        internal void SetState<T>() where T : TState
        {
            this.currentState = this.GetState<T>();
        }

        internal virtual void NextState(TDomain face)
        {
            this.currentEntity = face;
            this.currentState.Next(face);
        }

        internal abstract bool ShouldBeExecuted();

        internal abstract void Reset();
    }
}