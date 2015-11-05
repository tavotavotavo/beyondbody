using Domain;
using Processing.Actions;

namespace Processing.States
{
    internal class InitialDeactivateState : FaceState
    {
        internal InitialDeactivateState(KeyboardAction<FaceState> action)
            : base(action)
        {
        }

        internal override void Next(Face face)
        {
            if (face.IsEmpty)
            {
                if (timer.ElapsedMilliseconds < 3000)
                {
                    this.action.SetState<InitialDeactivateState>();
                }
                else
                {
                    this.ResetTimer();
                    this.action.SetState<ShouldDeactivateState>();
                }
            }
            else
            {
                this.ResetTimer();
                this.action.SetState<NotAboutToDeactivateState>();
            }
        }
    }
}