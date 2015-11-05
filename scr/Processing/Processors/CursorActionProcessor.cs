using Domain;
using MouseSimulation.Simulators;
using Processing.Actions;
using Processing.States;
using System.Collections.Generic;

namespace Processing.Processors
{
    internal class CursorActionProcessor
    {
        private IEnumerable<CursorAction<FaceState>> cursorActions;
        private CursorSimulator cursorSimulator;

        public CursorActionProcessor()
        {
            this.cursorSimulator = new CursorSimulator();

            this.cursorActions = new List<CursorAction<FaceState>>()
                {
                    new RigthClickAction(this.cursorSimulator.SimulateRightClick),
                    new LeftClickAction(this.cursorSimulator.SimulateClick),
                    new LeftDoubleClickAction(this.cursorSimulator.SimulateClick)
                };
        }

        internal void Process(Face detectedFace)
        {
            foreach (var action in this.cursorActions)
            {
                action.NextState(detectedFace);
            }

            foreach (var action in this.cursorActions)
            {
                if (action.ShouldBeExecuted())
                {
                    action.Execute();
                }
            }
        }

        internal void ResetActions()
        {
            foreach (var action in this.cursorActions)
            {
                action.Reset();
            }
        }
    }
}