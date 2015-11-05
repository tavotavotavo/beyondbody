using Domain;
using Processing.Actions;

namespace Processing.States
{
    internal class AboutToLeftDoubleClickState : AboutToLeftClickState
    {
        internal AboutToLeftDoubleClickState(CursorAction<FaceState> cursorAction)
            : base(cursorAction)
        {
        }

        protected override void BothEyesOpenState()
        {
            this.action.SetState<ShouldLeftDoubleClickState>();
        }

        protected override void IsBlinkingState()
        {
            this.action.SetState<AboutToLeftDoubleClickState>();
        }

        protected override AboutToAbortClickState AboutToAbort(int milliseconds)
        {
            this.action.SetState<AboutToAbortLeftDoubleClickState>();
            var newState = this.action.GetState<AboutToAbortLeftDoubleClickState>();

            newState.StartTimer();
            newState.MaxErrorMilliseconds = milliseconds;

            return newState;
        }
    }
}