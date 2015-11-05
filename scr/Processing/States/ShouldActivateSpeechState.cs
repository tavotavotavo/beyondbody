using Domain;
using Processing.Actions;

namespace Processing.States
{
    internal class ShouldActivateSpeechState : FaceState
    {
        internal ShouldActivateSpeechState(KeyboardAction<FaceState> cursorAction)
            : base(cursorAction)
        {
        }

        internal override void Next(Face item)
        {
            this.action.SetState<NotAboutToActivateSpeechState>();
        }
    }
}