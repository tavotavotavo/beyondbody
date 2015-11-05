using Domain;
using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Detectors
{
    public static class AppConfiguration
    {
        public static bool HasGlasses { get { return false; } }
    }

    public class EyeDetector : HaarCascadeDetector
    {
        private IList<Action> increasePrecisionActions;
        private int actionIndex;
        private bool isConfiguredWithGlasses;

        public EyeDetector()
            : base()
        {
            this.actionIndex = 0;
            this.increasePrecisionActions = new List<Action>();

            if (AppConfiguration.HasGlasses)
            {
                this.ConfigureWithGlasses();
            }
            else
            {
                this.ConfigureWithoutGlasses();
            }
        }

        //better detection
        // scanfactor = 1.1
        // neighbours = 5

        public void ConfigureWithoutGlasses()
        {
            this.isConfiguredWithGlasses = false;
            this.actionIndex = 0;
            this.increasePrecisionActions.Clear();
            this.HaarCascadeFileNames.Clear();
            this.Neighbours = 5;
            this.ScanFactor = 1.5;

            this.HaarCascadeFileNames.Add("haarcascade eye new.xml");
            this.increasePrecisionActions.Add(() => { this.Neighbours = 4; });
            this.increasePrecisionActions.Add(() => { this.Neighbours = 3; });
            this.increasePrecisionActions.Add(() => { this.Neighbours = 2; });
            this.increasePrecisionActions.Add(() => { this.Neighbours = 1; });
            this.increasePrecisionActions.Add(() => { this.ScanFactor = 1.4; });
            this.increasePrecisionActions.Add(() => { this.ScanFactor = 1.3; });
            this.increasePrecisionActions.Add(() =>
            {
                this.HaarCascadeFileNames.Add("haarcascade_eye.www-personal.umich.edu.xml");
                this.Neighbours = 5;
                this.ScanFactor = 1.5;
            });
            this.increasePrecisionActions.Add(() => { this.Neighbours = 4; });
            this.increasePrecisionActions.Add(() => { this.Neighbours = 3; });
            this.increasePrecisionActions.Add(() => { this.Neighbours = 2; });
            this.increasePrecisionActions.Add(() => { this.Neighbours = 1; });
            this.increasePrecisionActions.Add(() => { this.ScanFactor = 1.4; });
            this.increasePrecisionActions.Add(() => { this.ScanFactor = 1.3; });
            this.increasePrecisionActions.Add(() =>
            {
                this.HaarCascadeFileNames.Add("haarcascade_eye_tree_eyeglasses.xml");
                this.Neighbours = 5;
                this.ScanFactor = 1.5;
            });
            this.increasePrecisionActions.Add(() => { this.Neighbours = 4; });
            this.increasePrecisionActions.Add(() => { this.Neighbours = 3; });
            this.increasePrecisionActions.Add(() => { this.Neighbours = 2; });
            this.increasePrecisionActions.Add(() => { this.Neighbours = 1; });
            this.increasePrecisionActions.Add(() => { this.ScanFactor = 1.4; });
            this.increasePrecisionActions.Add(() => { this.ScanFactor = 1.3; });
            this.increasePrecisionActions.Add(() => { this.ScanFactor = 1.2; });
            this.increasePrecisionActions.Add(() => { this.ScanFactor = 1.1; });
        }

        public void ConfigureWithGlasses()
        {
            this.isConfiguredWithGlasses = true;
            this.actionIndex = 0;
            this.increasePrecisionActions.Clear();
            this.HaarCascadeFileNames.Clear();
            this.Neighbours = 3;
            this.ScanFactor = 1.4;

            this.HaarCascadeFileNames.Add("haarcascade_eye_tree_eyeglasses.xml");
            this.HaarCascadeFileNames.Add("haarcascade eye new.xml");
            this.increasePrecisionActions.Add(() => { this.Neighbours = 2; });
            this.increasePrecisionActions.Add(() => { this.Neighbours = 1; });
            this.increasePrecisionActions.Add(() => { this.ScanFactor = 1.3; });
            this.increasePrecisionActions.Add(() => { this.ScanFactor = 1.2; });
            this.increasePrecisionActions.Add(() =>
            {
                this.HaarCascadeFileNames.Add("haarcascade_eye.www-personal.umich.edu.xml");
                this.Neighbours = 3;
                this.ScanFactor = 1.4;
            });
            this.increasePrecisionActions.Add(() => { this.Neighbours = 2; });
            this.increasePrecisionActions.Add(() => { this.Neighbours = 1; });
            this.increasePrecisionActions.Add(() => { this.ScanFactor = 1.3; });
            this.increasePrecisionActions.Add(() => { this.ScanFactor = 1.2; });
            this.increasePrecisionActions.Add(() => { this.ScanFactor = 1.1; });
        }

        protected override double ScanFactor { get; set; }

        protected override int Neighbours { get; set; }

        public void DetectRightEye(Face face)
        {
            var result = new List<Eye>();

            face.Image.ROI = face.Zone;

            var reducedImage = face.Image.Copy();

            Rectangle roi = new Rectangle();

            roi.Width = reducedImage.Width / 2;
            roi.Height = reducedImage.Height / 2;
            roi.Y = reducedImage.ROI.Y + reducedImage.Height / 8;

            reducedImage.ROI = roi;

            face.Image.ROI = new Rectangle();

            Image<Gray, Byte> grayImage = reducedImage.Convert<Gray, byte>();

            var eyes = this.DetectVarious(grayImage);

            foreach (var eye in eyes)
            {
                var detectedEye = new Eye();

                detectedEye.Zone =
                    new Rectangle(
                        new Point(
                            eye.rect.X + face.Zone.X,
                            eye.rect.Y + face.Zone.Y + roi.Y),
                        new Size(
                            eye.rect.Width,
                            eye.rect.Height));

                detectedEye.Image = face.Image;

                //si el ojo no esta en la frente lo agregamos
                if (!(detectedEye.Zone.Y + detectedEye.Zone.Height / 2 < face.Zone.Y + face.Zone.Height / 4))
                {
                    //si el ojo no esta en la boca lo agregamos
                    if (!(detectedEye.Zone.Y + detectedEye.Zone.Height / 2 > face.Zone.Y + face.Zone.Height / 2))
                    {
                        //si el ojo no esta fuera de la cara lo agregamos
                        if (!(detectedEye.Zone.X + detectedEye.Zone.Width / 2 < face.Zone.X + face.Zone.Width / 6))
                        {
                            result.Add(detectedEye);
                            break;
                        }
                    }
                }
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
            roi.Y = reducedImage.ROI.Y + reducedImage.Height / 8;
            roi.X = roi.Width;

            reducedImage.ROI = roi;

            face.Image.ROI = new Rectangle();

            Image<Gray, Byte> grayImage = reducedImage.Convert<Gray, byte>();

            var eyes = this.DetectVarious(grayImage);

            foreach (var eye in eyes)
            {
                var detectedEye = new Eye();

                detectedEye.Zone =
                    new Rectangle(
                        new Point(
                            eye.rect.X + face.Zone.X + roi.Width,
                            eye.rect.Y + face.Zone.Y + roi.Y),
                        new Size(
                            eye.rect.Width,
                            eye.rect.Height));

                detectedEye.Image = face.Image;

                //si el ojo no esta en la frente lo agregamos
                if (!(detectedEye.Zone.Y + detectedEye.Zone.Height / 2 < face.Zone.Y + face.Zone.Height / 4))
                {
                    //si el ojo no esta en la boca lo agregamos
                    if (!(detectedEye.Zone.Y + detectedEye.Zone.Height / 2 > face.Zone.Y + face.Zone.Height / 2))
                    {
                        //si el ojo no esta fuera de la cara lo agregamos
                        if (!(detectedEye.Zone.X + detectedEye.Zone.Width / 2 > face.Zone.X + face.Zone.Width - face.Zone.Width / 6))
                        {
                            result.Add(detectedEye);
                            break;
                        }
                    }
                }
            }

            var leftEye = result.FirstOrDefault();

            face.LeftEye = leftEye ?? new Eye();

            face.LeftEye.Image = face.Image;
        }

        public void IncreasePrecision()
        {
            if (this.actionIndex < this.increasePrecisionActions.Count())
            {
                this.increasePrecisionActions[actionIndex].Invoke();
                this.actionIndex++;
            }
        }

        protected override string HaarCascadeFileName
        {
            get { throw new NotImplementedException(); }
        }

        public void ResetPrecision()
        {
            if (this.isConfiguredWithGlasses)
                this.ConfigureWithGlasses();
            else
                this.ConfigureWithoutGlasses();
        }

        public bool GetGlassesConfiguration()
        {
            return this.isConfiguredWithGlasses;
        }
    }
}