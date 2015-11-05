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

        protected override void SetHasBothEyesOpenState()
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

        protected override void SetIsBlinkingEyeState()
        {
            this.action.SetState<AboutToLeftClickState>();
        }

        protected override bool IsBlinkingEye(Face face)
        {
            return face.IsBlinkingLeftEye;
        }

        protected override void SetAbortClickingState()
        {
            this.action.SetState<NotAboutToLeftClickState>();
        }
    }
}