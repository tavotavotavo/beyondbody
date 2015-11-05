using Domain;
using MouseSimulation.Simulators;
using Processing.Actions;
using Processing.States;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Processing.Processors
{
    internal class CursorActionProcessor : ActionProcessor
    {
        private IEnumerable<ClickAction> cursorActions;
        private CursorSimulator cursorSimulator;

        public CursorActionProcessor()
        {
            this.cursorSimulator = new CursorSimulator();

            this.cursorActions = new List<ClickAction>()
            {
                new RigthClickAction(this.cursorSimulator.SimulateRightClick),
                new LeftClickAction(this.cursorSimulator.SimulateClick),
                new LeftDoubleClickAction(this.cursorSimulator.SimulateDoubleClick)
            };
        }

        public long FakeClicks { get { return this.cursorActions.Sum(x => x.FakeClicks); } }

        internal override void Process(Face detectedFace)
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

        internal void ResetClicksCount()
        {
            foreach (var action in this.cursorActions)
            {
                action.FakeClicks = 0;
            }
        }
    }

}