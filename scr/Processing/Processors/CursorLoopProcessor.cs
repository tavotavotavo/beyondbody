using MouseSimulation.Simulators;
using System;

namespace Processing.Processors
{
    public class CursorLoopProcessor
    {
        private CursorSimulator mouseSimulator;
        private bool isStarted;
        private uint rightCounter;
        private uint leftCounter;
        private uint bottomCounter;
        private uint topCounter;

        public CursorLoopProcessor()
        {
            this.mouseSimulator = new CursorSimulator();
        }

        public bool ShouldIncrementLeftCounter { get; set; }

        public bool ShouldIncrementRightCounter { get; set; }

        public bool ShouldIncrementBottomCounter { get; set; }

        public bool ShouldIncrementTopCounter { get; set; }

        internal void Start()
        {
            this.isStarted = true;
        }

        internal void Finish()
        {
            this.isStarted = false;
            this.ResetCounters();
        }

        public void Pool(object sender, EventArgs e)
        {
            if (this.isStarted)
            {
                if (this.ShouldIncrementLeftCounter)
                {
                    this.leftCounter++;
                    this.mouseSimulator.MoveCursorToLeft(leftCounter);
                }
                else if (this.ShouldIncrementRightCounter)
                {
                    this.rightCounter++;
                    this.mouseSimulator.MoveCursorToRight(rightCounter);
                }
                else if (this.ShouldIncrementBottomCounter)
                {
                    this.bottomCounter++;
                    this.mouseSimulator.MoveCursorToBottom(bottomCounter);
                }
                else if (this.ShouldIncrementTopCounter)
                {
                    this.topCounter++;
                    this.mouseSimulator.MoveCursorToTop(topCounter);
                }
            }
        }

        internal bool IsStarted()
        {
            return this.isStarted;
        }

        internal void MoveCursorToRight()
        {
            this.ShouldIncrementRightCounter = true;
        }

        internal void MoveCursorToLeft()
        {
            this.ShouldIncrementLeftCounter = true;
        }

        internal void MoveCursorToTop()
        {
            this.ShouldIncrementTopCounter = true;
        }

        internal void MoveCursorToBottom()
        {
            this.ShouldIncrementBottomCounter = true;
        }

        private void ResetCounters()
        {
            this.leftCounter = 0;
            this.rightCounter = 0;
            this.topCounter = 0;
            this.bottomCounter = 0;

            this.ShouldIncrementBottomCounter = false;
            this.ShouldIncrementTopCounter = false;
            this.ShouldIncrementRightCounter = false;
            this.ShouldIncrementLeftCounter = false;
        }
    }
}