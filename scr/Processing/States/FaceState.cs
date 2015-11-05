using Domain;
using Processing.Actions;
using System.Diagnostics;

namespace Processing.States
{
    internal abstract class FaceState : State<Face>
    {
        protected Stopwatch timer;

        protected BaseAction<FaceState, Face> action;

        internal FaceState(BaseAction<FaceState, Face> cursorAction)
        {
            this.timer = new Stopwatch();
            this.action = cursorAction;
        }

        internal void StartTimer()
        {
            timer.Reset();
            timer.Start();
        }

        internal void ResetTimer()
        {
            timer.Reset();
        }
    }
}