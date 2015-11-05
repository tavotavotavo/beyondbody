using Domain;
using Processing.Actions;
using System.Diagnostics;

namespace Processing.States
{
    internal class AboutToActivateSpeechState : FaceState
    {
        internal AboutToActivateSpeechState(KeyboardAction<FaceState> action)
            : base(action)
        {
        }

        internal override void Next(Face face)
        {
            //Si estuvo menos de 3 segundos con los ojos cerrados
            if (face.HasBothEyesClosed)
            {
                if (timer.ElapsedMilliseconds < 3000)
                {
                    //Sigue igual
                    this.action.SetState<AboutToActivateSpeechState>();
                }
                else
                {
                    //Con este estado deberia activar la funcionalidad de dictado
                    this.action.SetState<ShouldActivateSpeechState>();
                    this.ResetTimer();
                }
            }
            else
            {
                this.ResetTimer();
                this.action.SetState<NotAboutToActivateSpeechState>();
            }
        }
    }
}