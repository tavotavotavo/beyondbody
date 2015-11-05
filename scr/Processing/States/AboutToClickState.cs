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

        protected abstract void BothEyesOpenState();

        protected abstract void IsBlinkingState();

        protected abstract AboutToAbortClickState AboutToAbort(int milliseconds);

        protected abstract bool IsBlinking(Face face);

        protected abstract void AbortClick();

        protected virtual int MinSeconds { get { return 500; } }

        protected virtual int MaxSeconds { get { return 2000; } }

        internal override void Next(Face face)
        {
            if (timer.ElapsedMilliseconds > this.MaxSeconds)
            {
                this.AbortClick();
                this.ResetTimer();
            }
            else if (face.HasBothEyesOpen)
            {
                if (timer.ElapsedMilliseconds < this.MinSeconds)
                {
                    this.AboutToAbort(300);
                }
                else
                {
                    ((ClickAction)this.action).Clicks++;
                    this.ResetTimer();
                    this.BothEyesOpenState();
                }
            }
            else if (this.IsBlinking(face))
            {
                this.IsBlinkingState();
            }
            else if (face.HasBothEyesClosed)
            {
                this.AboutToAbort(200);
            }
            else
            {
                this.AboutToAbort(100);
            }
        }
    }
}