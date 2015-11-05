using System;
using System.IO;
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
                    "\\HaarCascades\\" + this.HaarCascadeFileNale).LocalPath;
            }
        }

        protected abstract double ScanFactor { get; }

        protected abstract int Neighbours { get; }

        protected abstract string HaarCascadeFileNale { get; }

        protected string GetTemplatePath(string templateName)
        {
            return new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase) +
                      "\\Templates\\" + templateName).LocalPath;
        }
    }
}