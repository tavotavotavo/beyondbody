using Domain;
using Processing.Actions;

namespace Processing.States
{
    internal class NotAboutToActivateSpeechState : FaceState
    {
        internal NotAboutToActivateSpeechState(KeyboardAction<FaceState> action)
            : base(action)
        {
        }

        internal override void Next(Face face)
        {
            if (!face.HasBothEyesClosed)
            {
                this.action.SetState<InitialActivateSpeechState>();
            }
            else
            {
                this.action.SetState<NotAboutToActivateSpeechState>();
            }
        }
    }
}