using Domain;
using Processing.Actions;

namespace Processing.States
{
    internal class NotAboutToRightClickState : NotAboutToClickState
    {
        internal NotAboutToRightClickState(CursorAction<FaceState> cursorAction)
            : base(cursorAction)
        {
        }

        protected override void SetBothEyesOpenState()
        {
            this.action.SetState<InitialRightClickState>();
        }

        protected override void SetNotBothEyesOpenState()
        {
            this.action.SetState<NotAboutToRightClickState>();
        }
    }

    internal class NotAboutToMiniClickState : NotAboutToClickState
    {
        internal NotAboutToMiniClickState(CursorAction<FaceState> cursorAction)
            : base(cursorAction)
        {
        }

        protected override void SetBothEyesOpenState()
        {
            this.action.SetState<InitialMiniClickState>();
        }

        protected override void SetNotBothEyesOpenState()
        {
            this.action.SetState<NotAboutToMiniClickState>();
        }
    }

    internal class NotAboutToLeftClickState : NotAboutToClickState
    {
        internal NotAboutToLeftClickState(CursorAction<FaceState> cursorAction)
            : base(cursorAction)
        {
        }

        protected override void SetBothEyesOpenState()
        {
            this.action.SetState<InitialLeftClickState>();
        }

        protected override void SetNotBothEyesOpenState()
        {
            this.action.SetState<NotAboutToLeftClickState>();
        }
    }

    internal abstract class NotAboutToClickState : FaceState
    {
        internal NotAboutToClickState(CursorAction<FaceState> cursorAction)
            : base(cursorAction)
        {
        }

        protected abstract void SetBothEyesOpenState();

        protected abstract void SetNotBothEyesOpenState();

        internal override void Next(Face face)
        {
            if (face.HasBothEyesOpen)
            {
                this.SetBothEyesOpenState();
            }
            else
            {
                this.SetNotBothEyesOpenState();
            }
        }
    }
}