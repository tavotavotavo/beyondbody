using Domain;
using Processing.Actions;

namespace Processing.States
{
    internal class AboutToRightClickState : AboutToClickState
    {
        internal AboutToRightClickState(CursorAction<FaceState> cursorAction)
            : base(cursorAction)
        {
        }

        protected override void BothEyesOpenState()
        {
            this.action.SetState<ShouldRightClickState>();
        }

        protected override void IsBlinkingState()
        {
            this.action.SetState<AboutToRightClickState>();
        }

        protected override bool IsBlinking(Face face)
        {
            return face.IsBlinkingRightEye;
        }

        protected override void AbortClick()
        {
            this.action.SetState<NotAboutToRightClickState>();
        }

        protected override AboutToAbortClickState AboutToAbort(int milliseconds)
        {
            this.action.SetState<AboutToAbortRightClickState>();
            var newState = this.action.GetState<AboutToAbortRightClickState>();

            newState.StartTimer();
            newState.MaxErrorMilliseconds = milliseconds;

            return newState;
        }
    }
}