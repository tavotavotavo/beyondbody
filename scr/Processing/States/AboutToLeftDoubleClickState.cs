using Domain;
using Processing.Actions;
using System.Diagnostics;

namespace Processing.States
{
    internal class AboutToLeftDoubleClickState : FaceState
    {
        internal AboutToLeftDoubleClickState(CursorAction<FaceState> cursorAction)
            : base(cursorAction)
        {
        }

        internal override void Next(Face face)
        {
            if (this.timer.ElapsedMilliseconds > 500)
            {
                this.timer.Reset();
                this.action.SetState<NotAboutToLeftClickState>();
            }
            else
            {
                if (face.HasBothEyesOpen)
                {
                    this.timer.Reset();
                    this.action.SetState<ShouldLeftDoubleClickState>();
                }
                else if (face.IsBlinkingLeftEye)
                {
                    this.action.SetState<AboutToLeftDoubleClickState>();
                }
                else
                {
                    this.timer.Reset();
                    this.action.SetState<NotAboutToLeftClickState>();
                }
            }
        }
    }
}