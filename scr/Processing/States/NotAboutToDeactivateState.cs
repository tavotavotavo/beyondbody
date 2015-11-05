using Domain;
using Processing.Actions;

namespace Processing.States
{
    internal class NotAboutToDeactivateState : FaceState
    {
        internal NotAboutToDeactivateState(KeyboardAction<FaceState> action)
            : base(action)
        {
        }

        internal override void Next(Face face)
        {
            if (face.IsEmpty)
            {
                this.action.GetState<InitialDeactivateState>().StartTimer();
                this.action.SetState<InitialDeactivateState>();
            }
            else
            {
                this.action.SetState<NotAboutToDeactivateState>();
            }
        }
    }
}