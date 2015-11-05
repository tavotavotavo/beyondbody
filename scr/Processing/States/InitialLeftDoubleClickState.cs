using Domain;
using Processing.Actions;
using System.Diagnostics;

namespace Processing.States
{
    internal class InitialLeftDoubleClickState : FaceState
    {
        internal InitialLeftDoubleClickState(CursorAction<FaceState> cursorAction)
            : base(cursorAction)
        {
        }

        internal override void Next(Face face)
        {
            if (this.timer.ElapsedMilliseconds > 1500)
            {
                this.timer.Reset();
                this.action.SetState<NotAboutToLeftClickState>();
            }
            else
            {
                if (face.HasBothEyesOpen)
                {
                    this.action.SetState<InitialLeftDoubleClickState>();
                }
                else if (face.IsBlinkingLeftEye)
                {
                    this.timer.Reset();
                    this.action.SetState<AboutToLeftDoubleClickState>();
                    this.action.GetState<AboutToLeftDoubleClickState>().StartTimer();
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