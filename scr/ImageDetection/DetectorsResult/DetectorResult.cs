using Emgu.CV;
using Emgu.CV.Structure;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace DetectorsResult
{
    public class DetectorResult
    {
        private IEnumerable<MCvAvgComp> results;
        private int factor = 1;

        public DetectorResult()
        {
        }

        public DetectorResult(MCvAvgComp[] results)
        {
            this.results = results;
        }

        public DetectorResult(MCvAvgComp[] results, int factor)
        {
            this.results = results;
            this.factor = factor;
        }

        public DetectorResult(MCvAvgComp[] results, int factor, Image<Bgr, byte> image)
        {
            this.results = results;
            this.factor = factor;
            this.Image = image;
        }

        public Image<Bgr, byte> Image { get; private set; }

        public Rectangle FirstRectangle
        {
            get
            {
                if (!this.IsEmpty)
                {
                    var fisrtResult = this.results.First();

                    return new Rectangle(
                        new Point(
                            fisrtResult.rect.X * factor,
                            fisrtResult.rect.Y * factor),
                        new Size(
                            fisrtResult.rect.Width * factor,
                            fisrtResult.rect.Height * factor));
                }

                return new Rectangle();
            }
        }

        public int FirstNeighbors
        {
            get
            {
                if (!this.IsEmpty)
                    return results.First().neighbors;

                return 0;
            }
        }

        public virtual bool IsEmpty
        {
            get
            {
                return this.results != null && !this.results.Any();
            }
        }
    }

    public class NullDetectorResult : DetectorResult
    {
        public override bool IsEmpty
        {
            get
            {
                return true;
            }
        }
    }
}