using Domain;
using Processing.Actions;

namespace Processing.States
{
    internal class AboutToAbortRightClickState : AboutToAbortClickState
    {
        internal AboutToAbortRightClickState(ClickAction cursorAction)
            : base(cursorAction)
        {
        }

        protected override bool IsBlinking(Face face)
        {
            return face.IsBlinkingRightEye;
        }

        protected override void AbortClick()
        {
            this.action.SetState<NotAboutToRightClickState>();
        }

        protected override void AboutToClick()
        {
            this.action.SetState<AboutToRightClickState>();
        }
    }

    internal class AboutToAbortLeftClickState : AboutToAbortClickState
    {
        internal AboutToAbortLeftClickState(ClickAction cursorAction)
            : base(cursorAction)
        {
        }

        protected override bool IsBlinking(Face face)
        {
            return face.IsBlinkingLeftEye;
        }

        protected override void AbortClick()
        {
            this.action.SetState<NotAboutToLeftClickState>();
        }

        protected override void AboutToClick()
        {
            this.action.SetState<AboutToLeftClickState>();
        }
    }

    internal class AboutToAbortLeftDoubleClickState : AboutToAbortLeftClickState
    {
        internal AboutToAbortLeftDoubleClickState(ClickAction cursorAction)
            : base(cursorAction)
        {
        }

        protected override void AboutToClick()
        {
            this.action.SetState<AboutToLeftDoubleClickState>();
        }
    }

    internal abstract class AboutToAbortClickState : FaceState
    {
        internal int MaxErrorMilliseconds { get; set; }

        internal AboutToAbortClickState(ClickAction cursorAction)
            : base(cursorAction)
        {
            this.MaxErrorMilliseconds = 300;
        }

        internal override void Next(Face face)
        {
            if (this.timer.ElapsedMilliseconds < this.MaxErrorMilliseconds)
            {
                if (this.IsBlinking(face))
                {
                    this.AboutToClick();
                    this.ResetTimer();
                }
            }
            else
            {
                ((ClickAction)this.action).FakeClicks++;
                this.AbortClick();
            }
        }

        protected abstract bool IsBlinking(Face face);

        protected abstract void AbortClick();

        protected abstract void AboutToClick();
    }
}