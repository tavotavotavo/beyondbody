using Domain;
using Processing.Actions;
using Processing.States;

namespace Processing.Processors
{
    internal class SpeechActionProcessor
    {
        private KeyboardAction<FaceState> deactivateSpeechAction;
        private KeyboardAction<FaceState> activateSpeechAction;
        private SpeechProcessor speechProcessor;

        public SpeechActionProcessor()
        {
            this.speechProcessor = new SpeechProcessor();;
            this.deactivateSpeechAction = new DeactivateAction();
            this.activateSpeechAction = new ActivateSpeechAction();
        }

        internal void Process(Face detectedFace)
        {
            this.deactivateSpeechAction.NextState(detectedFace);

            if (this.deactivateSpeechAction.ShouldBeExecuted())
            {
                this.speechProcessor.Finish();
            }

            this.activateSpeechAction.NextState(detectedFace);

            if (this.activateSpeechAction.ShouldBeExecuted())
            {
                if (!this.speechProcessor.IsStarted())
                {
                    this.speechProcessor.Start();
                }
                else
                {
                    this.speechProcessor.Finish();
                }
            }
        }

        internal void ResetActions()
        {
            this.deactivateSpeechAction.Reset();
        }

        internal void FinishActions()
        {
            this.speechProcessor.Finish();
        }

        internal void CloseAllProcesses()
        {
            this.speechProcessor.CloseAllProcesses();
        }
    }
}