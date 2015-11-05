using DetectorsResult;
using Domain;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Detectors
{
    public class EyeDetector : HaarCascadeDetector
    {
        protected override double ScanFactor { get { return 1.5; } }

        protected override int Neighbours { get { return 3; } }

        public void DetectRightEye(Face face)
        {
            var result = new List<Eye>();

            face.Image.ROI = face.Zone;

            var reducedImage = face.Image.Copy();

            Rectangle roi = new Rectangle();

            roi.Width = reducedImage.Width / 2;
            roi.Height = reducedImage.Height / 2;

            reducedImage.ROI = roi;

            face.Image.ROI = new Rectangle();

            Image<Gray, Byte> grayImage = reducedImage.Convert<Gray, byte>();

            var haarCascade = new HaarCascade(this.HaarCascadePath);
            var eyes = haarCascade.Detect(grayImage,
                this.ScanFactor, //the image where the object are to be detected from
                this.Neighbours, //factor by witch the window is scaled in subsequent scans
                HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, //min number of neighbour rectangles
                Size.Empty,
                Size.Empty);

            foreach (var eye in eyes)
            {
                var detectedEye = new Eye();

                detectedEye.Zone =
                    new Rectangle(
                        new Point(
                            eye.rect.X + face.Zone.X,
                            eye.rect.Y + face.Zone.Y),
                        new Size(
                            eye.rect.Width,
                            eye.rect.Height));

                detectedEye.Image = face.Image;

                result.Add(detectedEye);

                break; //just the first one
            }

            var rightEye = result.FirstOrDefault();

            face.RightEye = rightEye ?? new Eye();

            face.RightEye.Image = face.Image;
        }

        public void DetectLeftEye(Face face)
        {
            var result = new List<Eye>();

            face.Image.ROI = face.Zone;

            var reducedImage = face.Image.Copy();

            Rectangle roi = new Rectangle();

            roi.Width = reducedImage.Width / 2;
            roi.Height = reducedImage.Height / 2;
            roi.X = roi.Width;

            reducedImage.ROI = roi;

            face.Image.ROI = new Rectangle();

            Image<Gray, Byte> grayImage = reducedImage.Convert<Gray, byte>();

            var haarCascade = new HaarCascade(this.HaarCascadePath);
            var eyes = haarCascade.Detect(grayImage,
                this.ScanFactor, //the image where the object are to be detected from
                this.Neighbours, //factor by witch the window is scaled in subsequent scans
                HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, //min number of neighbour rectangles
                Size.Empty,
                Size.Empty);

            foreach (var eye in eyes)
            {
                var detectedEye = new Eye();

                detectedEye.Zone =
                    new Rectangle(
                        new Point(
                            eye.rect.X + face.Zone.X + roi.Width,
                            eye.rect.Y + face.Zone.Y),
                        new Size(
                            eye.rect.Width,
                            eye.rect.Height));

                detectedEye.Image = face.Image;

                result.Add(detectedEye);

                break; //just the first one
            }

            var leftEye = result.FirstOrDefault();

            face.LeftEye = leftEye ?? new Eye();

            face.LeftEye.Image = face.Image;
        }

        protected override string HaarCascadeFileNale
        {
            get { return "haarcascade_eye.www-personal.umich.edu.xml"; }
        }
    }
}