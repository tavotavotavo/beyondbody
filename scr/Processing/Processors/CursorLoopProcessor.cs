using MouseSimulation.Simulators;
using System;
using System.Diagnostics;

namespace Processing.Processors
{
    public class CursorLoopProcessor
    {
        private CursorSimulator mouseSimulator;
        private bool isStarted;
        private uint generalCounter;
        private uint rightCounter;
        private uint leftCounter;
        private uint bottomCounter;
        private uint pixelsToAdd;
        private uint topCounter;
        protected Stopwatch timer;

        public CursorLoopProcessor(CursorSimulator mouseSimulator)
        {
            this.mouseSimulator = mouseSimulator;
            this.timer = new Stopwatch();
            this.pixelsToAdd = 1;
        }

        public bool ShouldIncrementLeftCounter { get; set; }

        public bool ShouldIncrementRightCounter { get; set; }

        public bool ShouldIncrementBottomCounter { get; set; }

        public bool ShouldIncrementTopCounter { get; set; }

        internal void Start()
        {
            this.timer.Start();
            this.isStarted = true;
        }

        internal void Finish()
        {
            this.timer.Reset();
            this.isStarted = false;
            this.ResetCounters();
        }

        public void Pool(object sender, EventArgs e)
        {
            if (this.isStarted)
            {
                uint limit = 15;

                this.generalCounter++;

                if (timer.ElapsedMilliseconds > 1000)
                {
                    if (this.generalCounter < limit)
                    {
                        pixelsToAdd = (uint)Math.Round((double)limit / this.generalCounter);
                    }
                }

                if (this.ShouldIncrementLeftCounter)
                {
                    this.leftCounter += pixelsToAdd;
                    this.mouseSimulator.MoveCursorToLeft(leftCounter);

                    this.rightCounter = this.VerifyNoUsingCounter((int)this.rightCounter, pixelsToAdd, this.mouseSimulator.MoveCursorToRight);
                    this.bottomCounter = this.VerifyNoUsingCounter((int)this.bottomCounter, pixelsToAdd, this.mouseSimulator.MoveCursorToBottom);
                    this.topCounter = this.VerifyNoUsingCounter((int)this.topCounter, pixelsToAdd, this.mouseSimulator.MoveCursorToTop);
                }
                if (this.ShouldIncrementRightCounter)
                {
                    this.rightCounter += pixelsToAdd;
                    this.mouseSimulator.MoveCursorToRight(rightCounter);

                    this.leftCounter = this.VerifyNoUsingCounter((int)this.leftCounter, pixelsToAdd, this.mouseSimulator.MoveCursorToLeft);
                    this.bottomCounter = this.VerifyNoUsingCounter((int)this.bottomCounter, pixelsToAdd, this.mouseSimulator.MoveCursorToBottom);
                    this.topCounter = this.VerifyNoUsingCounter((int)this.topCounter, pixelsToAdd, this.mouseSimulator.MoveCursorToTop);
                 
                }
                if (this.ShouldIncrementBottomCounter)
                {
                    this.bottomCounter += pixelsToAdd;
                    this.mouseSimulator.MoveCursorToBottom(bottomCounter);

                    this.leftCounter = this.VerifyNoUsingCounter((int)this.leftCounter, pixelsToAdd, this.mouseSimulator.MoveCursorToLeft);
                    this.rightCounter = this.VerifyNoUsingCounter((int)this.rightCounter, pixelsToAdd, this.mouseSimulator.MoveCursorToRight);
                    this.topCounter = this.VerifyNoUsingCounter((int)this.topCounter, pixelsToAdd, this.mouseSimulator.MoveCursorToTop);
        
                }
                if (this.ShouldIncrementTopCounter)
                {
                    this.topCounter += pixelsToAdd;
                    this.mouseSimulator.MoveCursorToTop(topCounter);

                    this.leftCounter = this.VerifyNoUsingCounter((int)this.leftCounter, pixelsToAdd, this.mouseSimulator.MoveCursorToLeft);
                    this.rightCounter = this.VerifyNoUsingCounter((int)this.rightCounter, pixelsToAdd, this.mouseSimulator.MoveCursorToRight);
                    this.bottomCounter = this.VerifyNoUsingCounter((int)this.bottomCounter, pixelsToAdd, this.mouseSimulator.MoveCursorToBottom);
        
                }
            }
        }

        private uint VerifyNoUsingCounter(int counter, uint pixelsToAdd, Action<uint> action)
        {
            if (counter > 0)
            {
                counter -= (int)pixelsToAdd * 2;
                //this.rightCounter -= pixelsToAdd;
                if (counter < 0)
                    counter = 0;
                else
                    action((uint)counter);
            }

            return (uint)counter;
        }

        internal bool IsStarted()
        {
            return this.isStarted;
        }

        internal void MoveCursorToRight()
        {
            this.ResetFlags();
            this.ShouldIncrementRightCounter = true;
        }

        internal void MoveCursorToLeft()
        {
            this.ResetFlags();
            this.ShouldIncrementLeftCounter = true;
        }

        internal void MoveCursorToTop()
        {
            this.ResetFlags();
            this.ShouldIncrementTopCounter = true;
        }

        internal void MoveCursorToBottom()
        {
            this.ResetFlags();
            this.ShouldIncrementBottomCounter = true;
        }

        private void ResetCounters()
        {
            this.leftCounter = 0;
            this.rightCounter = 0;
            this.topCounter = 0;
            this.bottomCounter = 0;
            this.generalCounter = 0;
            this.timer.Restart();

            this.ResetFlags();
        }

        private void ResetFlags()
        {
            this.ShouldIncrementBottomCounter = false;
            this.ShouldIncrementTopCounter = false;
            this.ShouldIncrementRightCounter = false;
            this.ShouldIncrementLeftCounter = false;
        }
    }
}