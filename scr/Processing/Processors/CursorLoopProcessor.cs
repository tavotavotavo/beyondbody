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

        public CursorLoopProcessor()
        {
            this.mouseSimulator = new CursorSimulator();
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

                    if (this.rightCounter > 0)
                    {
                        this.rightCounter -= pixelsToAdd;
                        //this.rightCounter -= pixelsToAdd;
                        if (this.rightCounter < 0)
                            this.rightCounter = 0;
                        else
                            this.mouseSimulator.MoveCursorToRight(rightCounter);
                    }

                    if (this.bottomCounter > 0)
                    {
                        this.bottomCounter -= pixelsToAdd;
                        //this.bottomCounter -= pixelsToAdd;
                        if (this.bottomCounter < 0)
                            this.bottomCounter = 0;
                        else
                            this.mouseSimulator.MoveCursorToBottom(bottomCounter);
                    }

                    if (this.topCounter > 0)
                    {
                        this.topCounter -= pixelsToAdd;
                        //this.topCounter -= pixelsToAdd;
                        if (this.topCounter < 0)
                            this.topCounter = 0;
                        else
                            this.mouseSimulator.MoveCursorToTop(topCounter);
                    }
                }
                if (this.ShouldIncrementRightCounter)
                {
                    this.rightCounter += pixelsToAdd;
                    this.mouseSimulator.MoveCursorToRight(rightCounter);

                    if (this.leftCounter > 0)
                    {
                        this.leftCounter -= pixelsToAdd;
                        //this.leftCounter -= pixelsToAdd;
                        if (this.leftCounter < 0)
                            this.leftCounter = 0;
                        else
                            this.mouseSimulator.MoveCursorToLeft(leftCounter);
                    }

                    if (this.bottomCounter > 0)
                    {
                        this.bottomCounter -= pixelsToAdd;
                        //this.bottomCounter -= pixelsToAdd;
                        if (this.bottomCounter < 0)
                            this.bottomCounter = 0;
                        else
                            this.mouseSimulator.MoveCursorToBottom(bottomCounter);
                    }

                    if (this.topCounter > 0)
                    {
                        this.topCounter -= pixelsToAdd;
                        //this.topCounter -= pixelsToAdd;
                        if (this.topCounter < 0)
                            this.topCounter = 0;
                        else
                            this.mouseSimulator.MoveCursorToTop(topCounter);
                    }
                }
                if (this.ShouldIncrementBottomCounter)
                {
                    this.bottomCounter += pixelsToAdd;
                    this.mouseSimulator.MoveCursorToBottom(bottomCounter);

                    if (this.leftCounter > 0)
                    {
                        this.leftCounter -= pixelsToAdd;
                        //this.leftCounter -= pixelsToAdd;
                        if (this.leftCounter < 0)
                            this.leftCounter = 0;
                        else
                            this.mouseSimulator.MoveCursorToLeft(leftCounter);
                    }

                    if (this.rightCounter > 0)
                    {
                        this.rightCounter -= pixelsToAdd;
                        //this.rightCounter -= pixelsToAdd;
                        if (this.rightCounter < 0)
                            this.rightCounter = 0;
                        else
                            this.mouseSimulator.MoveCursorToRight(rightCounter);
                    }

                    if (this.topCounter > 0)
                    {
                        this.topCounter -= pixelsToAdd;
                        //this.topCounter -= pixelsToAdd;
                        if (this.topCounter < 0)
                            this.topCounter = 0;
                        else
                            this.mouseSimulator.MoveCursorToTop(topCounter);
                    }
                }
                if (this.ShouldIncrementTopCounter)
                {
                    this.topCounter += pixelsToAdd;
                    this.mouseSimulator.MoveCursorToTop(topCounter);

                    if (this.leftCounter > 0)
                    {
                        this.leftCounter -= pixelsToAdd;
                        //this.leftCounter -= pixelsToAdd;
                        if (this.leftCounter < 0)
                            this.leftCounter = 0;
                        else
                            this.mouseSimulator.MoveCursorToLeft(leftCounter);
                    }

                    if (this.rightCounter > 0)
                    {
                        this.rightCounter -= pixelsToAdd;
                        //this.rightCounter -= pixelsToAdd;
                        if (this.rightCounter < 0)
                            this.rightCounter = 0;
                        else
                            this.mouseSimulator.MoveCursorToRight(rightCounter);
                    }

                    if (this.bottomCounter > 0)
                    {
                        this.bottomCounter -= pixelsToAdd;
                        //this.bottomCounter -= pixelsToAdd;
                        if (this.bottomCounter < 0)
                            this.bottomCounter = 0;
                        else
                            this.mouseSimulator.MoveCursorToBottom(bottomCounter);
                    }
                }
            }
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