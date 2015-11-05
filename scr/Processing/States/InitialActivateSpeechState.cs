using Domain;
using Processing.Actions;

namespace Processing.States
{
    internal class InitialActivateSpeechState : FaceState
    {
        internal InitialActivateSpeechState(KeyboardAction<FaceState> action)
            : base(action)
        {
        }

        internal override void Next(Face face)
        {
            //Si estuvo menos de 3 segundos con los ojos cerrados
            if (!face.HasBothEyesClosed)
            {
                this.action.SetState<InitialActivateSpeechState>();
            }
            else
            {
                this.action.GetState<AboutToActivateSpeechState>().StartTimer();
                this.action.SetState<AboutToActivateSpeechState>();
            }
        }
    }
}