using Domain;
using Processing.Actions;

namespace Processing.States
{
    internal class ShouldRightClickState : ShouldClickState
    {
        internal ShouldRightClickState(CursorAction<FaceState> cursorAction)
            : base(cursorAction)
        {
        }

        protected override void SetNotAboutState()
        {
            this.action.SetState<NotAboutToRightClickState>();
        }
    }

    internal class ShouldLeftClickState : ShouldClickState
    {
        internal ShouldLeftClickState(CursorAction<FaceState> cursorAction)
            : base(cursorAction)
        {
        }

        protected override void SetNotAboutState()
        {
            this.action.SetState<NotAboutToLeftClickState>();
        }
    }

    internal abstract class ShouldClickState : FaceState
    {
        internal ShouldClickState(CursorAction<FaceState> cursorAction)
            : base(cursorAction)
        {
        }

        protected abstract void SetNotAboutState();

        internal override void Next(Face face)
        {
            this.SetNotAboutState();
        }
    }
}