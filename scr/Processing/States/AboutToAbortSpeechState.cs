using Domain;
using Processing.Actions;

namespace Processing.States
{
    internal class AboutToAbortSpeechState : FaceState
    {
        internal AboutToAbortSpeechState(KeyboardAction<FaceState> cursorAction)
            : base(cursorAction)
        {
        }

        internal override void Next(Face face)
        {
            if (this.timer.ElapsedMilliseconds < 500)
            {
                if (face.HasBothEyesClosed)
                {
                    this.action.SetState<AboutToActivateSpeechState>();
                    this.ResetTimer();
                }
            }
            else
            {
                this.action.SetState<NotAboutToActivateSpeechState>();
            }
        }
    }
}