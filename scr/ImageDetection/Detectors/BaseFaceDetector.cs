using Domain;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Detectors
{
    public abstract class BaseFaceDetector : HaarCascadeDetector
    {
        public virtual IEnumerable<Face> DetectFaces(Image<Bgr, byte> image)
        {
            var result = new List<Face>();

            var factor = image.Width / this.WidthToReduce;

            var reducedImage = image.Resize(image.Width / factor,
                    image.Height / factor,
                    INTER.CV_INTER_LINEAR);

            reducedImage = this.AfterReducingImage(reducedImage);

            Image<Gray, Byte> grayImage = reducedImage.Convert<Gray, byte>();

            var faces = this.GetFacesVector(grayImage);

            foreach (var face in faces)
            {
                var detectedFace = new Face();

                detectedFace.IsFake = false;

                detectedFace.Zone =
                    new Rectangle(
                        new Point(
                            face.rect.X * factor,
                            face.rect.Y * factor),
                        new Size(
                            face.rect.Width * factor,
                            face.rect.Height * factor));

                detectedFace.Image = image;

                result.Add(detectedFace);
            }

            return result;
        }

        protected virtual MCvAvgComp[] GetFacesVector(Image<Gray, byte> grayImage)
        {
            var haarCascade = new HaarCascade(this.HaarCascadePath);

            return haarCascade.Detect(grayImage,
                this.ScanFactor, //the image where the object are to be detected from
                this.Neighbours, //factor by witch the window is scaled in subsequent scans
                HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, //min number of neighbour rectangles
                Size.Empty,
                Size.Empty);
        }

        protected virtual Image<Bgr, byte> AfterReducingImage(Image<Bgr, byte> reducedImage)
        {
            return reducedImage;
        }
    }
}