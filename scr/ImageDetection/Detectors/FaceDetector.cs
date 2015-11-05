using Common.PriorityAlgorithm;
using Detection.PriorityAlgorithm;
using Domain;
using Domain.Extensions;
using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Detectors
{
    public class FaceDetector
    {
        private LifoAlgorithm<BaseFaceDetector> facePriority;
        private Stopwatch notDetectedTimer;
        private Face lastDetected;
        private Face lastNotFrontalDetected;
        private FaceDetectorPriority frontalFacePriorityItem;
        private FaceDetectorPriority profileFacePriorityItem;
        private FaceDetectorPriority rotatedFacePriorityItem;
        //Implement priority

        public FaceDetector()
        {
            this.lastDetected = new Face();
            this.notDetectedTimer = new Stopwatch();

            this.facePriority = new LifoAlgorithm<BaseFaceDetector>();
            this.frontalFacePriorityItem = new FaceDetectorPriority(new FrontalFaceDetector());
            this.profileFacePriorityItem = new FaceDetectorPriority(new ProfileFaceDetector());
            this.rotatedFacePriorityItem = new FaceDetectorPriority(new RotatedFaceDetector());
            this.facePriority.AddAlgorithmItem(this.frontalFacePriorityItem);
            this.facePriority.AddAlgorithmItem(this.profileFacePriorityItem);
            this.facePriority.AddAlgorithmItem(this.rotatedFacePriorityItem);
        }

        public Face DetectFace(Image<Bgr, byte> image)
        {
            Face detectedFace = new Face();

            BaseFaceDetector faceDetector = this.facePriority.Next();

            detectedFace = this.GetCentered(image, faceDetector.DetectFaces(image));

            if (!detectedFace.IsEmpty)
            {
                this.notDetectedTimer.Reset();

                if (detectedFace.IsFrontal)
                {
                    if (detectedFace.IsIntoControlZone)
                    {
                        this.lastDetected = detectedFace;
                        this.facePriority.SetFirst();
                    }
                    else
                    {
                        this.facePriority.SetFirst(this.rotatedFacePriorityItem);
                        this.facePriority.SetSecond(this.profileFacePriorityItem);
                        detectedFace = this.lastNotFrontalDetected ?? detectedFace;
                    }
                }
                else
                {
                    this.lastDetected = this.lastNotFrontalDetected = detectedFace;
                    this.facePriority.SetFirst(this.frontalFacePriorityItem);
                }

                if (detectedFace.IsProfile)
                {
                    if (detectedFace.IsIntoControlZone)
                    {
                        this.facePriority.SetFirst(this.frontalFacePriorityItem);
                        this.facePriority.SetSecond(this.profileFacePriorityItem);
                    }
                    else
                    {
                        this.facePriority.SetFirst(this.profileFacePriorityItem);
                        this.facePriority.SetSecond(this.rotatedFacePriorityItem);
                    }
                }

                if (detectedFace.IsRotated)
                {
                    if (detectedFace.IsIntoRotatedControlZone)
                    {
                        this.facePriority.SetFirst(this.frontalFacePriorityItem);
                        this.facePriority.SetSecond(this.rotatedFacePriorityItem);
                    }
                    else
                    {
                        this.facePriority.SetFirst(this.rotatedFacePriorityItem);
                        this.facePriority.SetSecond(this.frontalFacePriorityItem);
                    }
                }
            }
            else
            {
                if (!this.notDetectedTimer.IsRunning)
                {
                    if (!this.lastDetected.IsEmpty)
                    {
                        this.lastDetected.IsFake = true;
                    }

                    this.notDetectedTimer.Start();
                }

                if (this.notDetectedTimer.ElapsedMilliseconds < 5000)
                {
                    return this.lastDetected;
                }
                else
                {
                    this.notDetectedTimer.Stop();
                    return new Face();
                }
            }

            return detectedFace;
        }

        private Face GetCentered(Image<Bgr, byte> image, IEnumerable<Face> detectedFaces)
        {
            Face returningFace = null;
            var distance = double.MaxValue;

            foreach (var face in detectedFaces)
            {
                var imageCenter = image.ROI.Center();
                var faceDistance = Math.Pow(face.Zone.X + face.Zone.Width / 2 - imageCenter.X, 2) +
                    Math.Pow(face.Zone.Y + face.Zone.Height / 2 - imageCenter.Y, 2);

                if (faceDistance < distance)
                {
                    returningFace = face;
                    distance = faceDistance;
                }
            }

            return returningFace ?? new Face();
        }
    }
}