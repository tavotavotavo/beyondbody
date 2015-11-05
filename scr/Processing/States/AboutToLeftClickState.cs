using Domain;
using Processing.Actions;

namespace Processing.States
{
    internal class AboutToLeftClickState : AboutToClickState
    {
        internal AboutToLeftClickState(CursorAction<FaceState> cursorAction)
            : base(cursorAction)
        {
        }

        protected override void BothEyesOpenState()
        {
            if (this.action is LeftDoubleClickAction)
            {
                this.action.SetState<InitialLeftDoubleClickState>();
                this.action.GetState<InitialLeftDoubleClickState>().StartTimer();
            }
            else
            {
                this.action.SetState<ShouldLeftClickState>();
            }
        }

        protected override void IsBlinkingState()
        {
            this.action.SetState<AboutToLeftClickState>();
        }

        protected override bool IsBlinking(Face face)
        {
            return face.IsBlinkingLeftEye;
        }

        protected override void AbortClick()
        {
            this.action.SetState<NotAboutToLeftClickState>();
        }

        protected override AboutToAbortClickState AboutToAbort(int milliseconds)
        {
            this.action.SetState<AboutToAbortLeftClickState>();
            var newState = this.action.GetState<AboutToAbortLeftClickState>();

            newState.StartTimer();
            newState.MaxErrorMilliseconds = milliseconds;

            return newState;
        }
    }
}