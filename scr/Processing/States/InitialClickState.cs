using Domain;
using Processing.Actions;

namespace Processing.States
{
    internal class InitialRightClickState : InitialClickState
    {
        internal InitialRightClickState(CursorAction<FaceState> cursorAction)
            : base(cursorAction)
        {
        }

        protected override void SetHasBothEyesOpenState()
        {
            this.action.SetState<InitialRightClickState>();
        }

        protected override FaceState SetIsBlinkingEyeState()
        {
            this.action.SetState<AboutToRightClickState>();
            return this.action.GetState<AboutToRightClickState>();
        }

        protected override bool IsBlinkingEye(Face face)
        {
            return face.IsBlinkingRightEye;
        }

        protected override void SetElseState()
        {
            this.action.SetState<NotAboutToRightClickState>();
        }
    }

    internal class InitialLeftClickState : InitialClickState
    {
        internal InitialLeftClickState(CursorAction<FaceState> cursorAction)
            : base(cursorAction)
        {
        }

        protected override void SetHasBothEyesOpenState()
        {
            this.action.SetState<InitialLeftClickState>();
        }

        protected override FaceState SetIsBlinkingEyeState()
        {
            this.action.SetState<AboutToLeftClickState>();
            return this.action.GetState<AboutToLeftClickState>();
        }

        protected override bool IsBlinkingEye(Face face)
        {
            return face.IsBlinkingLeftEye;
        }

        protected override void SetElseState()
        {
            this.action.SetState<NotAboutToLeftClickState>();
        }
    }

    internal abstract class InitialClickState : FaceState
    {
        internal InitialClickState(CursorAction<FaceState> cursorAction)
            : base(cursorAction)
        {
        }

        protected abstract void SetHasBothEyesOpenState();

        protected abstract FaceState SetIsBlinkingEyeState();

        protected abstract bool IsBlinkingEye(Face face);

        protected abstract void SetElseState();

        internal override void Next(Face face)
        {
            if (this.IsBlinkingEye(face))
            {
                this.SetIsBlinkingEyeState().StartTimer();
            }
            else if (face.HasBothEyesOpen)
            {
                this.SetHasBothEyesOpenState();
            }
            else
            {
                this.SetElseState();
            }
        }
    }
}