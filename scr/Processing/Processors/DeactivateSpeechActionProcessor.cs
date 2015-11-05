using Domain;
using MouseSimulation.Simulators;
using Processing.Actions;
using Processing.States;
using System;
using System.Collections.Generic;

namespace Processing.Processors
{
    internal class DeactivateActionProcessor : ActionProcessor
    {
        private KeyboardAction<FaceState> deactivateAction;
        private SpeechProcessor speechProcessor;
        private CursorLoopProcessor cursorLoopProcessor;

        public DeactivateActionProcessor(SpeechProcessor speechProcessor, CursorLoopProcessor cursorLoopProcessor)
        {
            this.cursorLoopProcessor = cursorLoopProcessor;
            this.speechProcessor = speechProcessor;
            this.deactivateAction = new DeactivateAction();
        }

        internal override void Process(Face detectedFace)
        {
            this.deactivateAction.NextState(detectedFace);

            if (this.deactivateAction.ShouldBeExecuted())
            {
                this.speechProcessor.Finish();
                this.cursorLoopProcessor.Finish();
            }
        }

        internal void ResetActions()
        {
            this.deactivateAction.Reset();
        }
    }
}