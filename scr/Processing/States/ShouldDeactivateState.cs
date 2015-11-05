using Domain;
using Processing.Actions;

namespace Processing.States
{
    internal class ShouldDeactivateState : FaceState
    {
        internal ShouldDeactivateState(KeyboardAction<FaceState> cursorAction)
            : base(cursorAction)
        {
        }

        internal override void Next(Face item)
        {
            this.action.SetState<NotAboutToDeactivateState>();
        }
    }
}