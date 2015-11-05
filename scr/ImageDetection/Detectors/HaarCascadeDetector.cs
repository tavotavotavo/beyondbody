using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Detectors
{
    public abstract class HaarCascadeDetector
    {
        protected string HaarCascadePath
        {
            get
            {
                return new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase) +
                    "\\HaarCascades\\" + this.HaarCascadeFileName).LocalPath;
            }
        }

        protected virtual int WidthToReduce { get { return 106; } }

        protected abstract double ScanFactor { get; set; }

        protected abstract int Neighbours { get; set; }

        protected abstract string HaarCascadeFileName { get; }

        protected string GetTemplatePath(string templateName)
        {
            return new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase) +
                      "\\Templates\\" + templateName).LocalPath;
        }

        protected IList<string> HaarCascadeFileNames { get; set; }

        protected IEnumerable<string> HaarCascadePaths
        {
            get
            {
                return this.HaarCascadeFileNames.Select(x => new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase) +
                  "\\HaarCascades\\" + x).LocalPath);
            }
        }

        public HaarCascadeDetector()
        {
            this.HaarCascadeFileNames = new List<string>();
        }

        protected MCvAvgComp[] DetectVarious(Image<Gray, byte> grayImage)
        {
            MCvAvgComp[] eyes = null;

            foreach (var haarcascade in this.HaarCascadePaths)
            {
                var haarCascade = new HaarCascade(haarcascade);
                eyes = haarCascade.Detect(grayImage,
                    this.ScanFactor, //the image where the object are to be detected from
                    this.Neighbours, //factor by witch the window is scaled in subsequent scans
                    HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, //min number of neighbour rectangles
                    Size.Empty,
                    Size.Empty);

                if (eyes.Any()) break;
            }

            return eyes;
        }
    }
}