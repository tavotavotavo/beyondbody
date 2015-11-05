using Domain;
using Processing.Actions;

namespace Processing.States
{
    internal abstract class AboutToClickState : FaceState
    {
        internal AboutToClickState(CursorAction<FaceState> cursorAction)
            : base(cursorAction)
        {
        }

        protected abstract void SetHasBothEyesOpenState();

        protected abstract void SetIsBlinkingEyeState();

        protected abstract bool IsBlinkingEye(Face face);

        protected abstract void SetAbortClickingState();

        internal override void Next(Face face)
        {
            if (timer.ElapsedMilliseconds > 500)
            {
                this.SetAbortClickingState();
                this.ResetTimer();
            }
            else if (face.HasBothEyesOpen)
            {
                this.SetHasBothEyesOpenState();
                this.ResetTimer();
            }
            else if (this.IsBlinkingEye(face))
            {
                this.SetIsBlinkingEyeState();
            }
            else
            {
                this.SetAbortClickingState();
                this.ResetTimer();
            }
        }
    }
}