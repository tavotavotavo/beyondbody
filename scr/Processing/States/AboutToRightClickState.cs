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

        protected override void SetHasBothEyesOpenState()
        {
            this.action.SetState<ShouldRightClickState>();
        }

        protected override void SetIsBlinkingEyeState()
        {
            this.action.SetState<AboutToRightClickState>();
        }

        protected override bool IsBlinkingEye(Face face)
        {
            return face.IsBlinkingRightEye;
        }

        protected override void SetAbortClickingState()
        {
            this.action.SetState<NotAboutToRightClickState>();
        }
    }
}