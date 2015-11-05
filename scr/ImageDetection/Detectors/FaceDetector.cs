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
        private Face lastFrontalDetected;
        private Face leftProfileFakeFace;
        private Face rightProfileFakeFace;
        private FaceDetectorPriority frontalFacePriorityItem;
        private FaceDetectorPriority profileFacePriorityItem;
        private FaceDetectorPriority rotatedFacePriorityItem;
        private EyeDetector eyeDetector;

        //Implement priority

        public FaceDetector(EyeDetector eyeDetector)
        {
            this.eyeDetector = eyeDetector;
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
                detectedFace.IsOuttaControl = false;
                this.notDetectedTimer.Reset();

                if (detectedFace.IsFrontal && (this.lastFrontalDetected == null || this.lastFrontalDetected.IsIntoControlZone))
                {
                    this.eyeDetector.DetectRightEye(detectedFace);
                    this.eyeDetector.DetectLeftEye(detectedFace);
                }

                detectedFace.Convert();

                if (detectedFace.IsFrontal)
                {
                    this.lastFrontalDetected = detectedFace;

                    if (detectedFace.IsIntoControlZone)
                    {
                        this.lastDetected = detectedFace;
                        this.facePriority.SetFirst(this.frontalFacePriorityItem);

                        if (this.lastNotFrontalDetected != null)
                        {
                            this.lastNotFrontalDetected.IsRightProfile = detectedFace.Center.X < detectedFace.Image.Center().X;
                            this.lastNotFrontalDetected.IsLeftProfile = !this.lastDetected.IsRightProfile;
                            this.lastNotFrontalDetected.IsRightRotated = false;
                            this.lastNotFrontalDetected.IsLeftRotated = false;
                        }
                    }
                    else
                    {
                        this.facePriority.SetFirst(this.profileFacePriorityItem);
                        this.facePriority.SetSecond(this.rotatedFacePriorityItem);

                        var oldDetectedFace = detectedFace;
                        this.lastDetected = detectedFace = this.lastNotFrontalDetected ?? detectedFace;

                        if (this.lastDetected.IsProfile)
                        {
                            this.lastDetected.IsRightProfile = oldDetectedFace.Center.X < oldDetectedFace.Image.Center().X;
                            this.lastDetected.IsLeftProfile = !this.lastDetected.IsRightProfile;
                        }

                        if (this.lastDetected.IsRotated)
                        {
                            this.lastDetected.IsRightRotated = !(oldDetectedFace.Center.X < oldDetectedFace.Image.Center().X);
                            this.lastDetected.IsLeftRotated = !this.lastDetected.IsRightProfile;
                        }

                        detectedFace.IsOuttaControl = oldDetectedFace.IsZoneOutOfControl;
                        detectedFace.ReplacedZone = oldDetectedFace.Zone;
                    }
                }
                else
                {
                    this.lastDetected = this.lastNotFrontalDetected = detectedFace;
                }

                if (detectedFace.IsProfile)
                {
                    this.lastDetected = this.lastNotFrontalDetected = detectedFace;

                    if (detectedFace.IsIntoControlZone)
                    {
                        this.facePriority.SetFirst(this.frontalFacePriorityItem);
                        this.facePriority.SetSecond(this.profileFacePriorityItem);
                        //this.facePriority.SetSecond(this.rotatedFacePriorityItem);
                        //detectedFace.IsOuttaControl = false;
                    }
                    else
                    {
                        this.lastDetected.IsOuttaControl = this.lastDetected.IsZoneOutOfControl;
                        this.lastDetected.ReplacedZone = this.lastDetected.Zone;
                        this.facePriority.SetFirst(this.profileFacePriorityItem);
                        this.facePriority.SetSecond(this.rotatedFacePriorityItem);
                    }
                }

                if (detectedFace.IsRotated)
                {
                    this.lastDetected = this.lastNotFrontalDetected = detectedFace;

                    if (detectedFace.IsIntoControlZone)
                    {
                        //this.facePriority.SetFirst(this.rotatedFacePriorityItem);
                        //this.facePriority.SetSecond(this.profileFacePriorityItem);
                        this.facePriority.SetFirst(this.frontalFacePriorityItem);
                        this.facePriority.SetSecond(this.rotatedFacePriorityItem);
                        //detectedFace.IsOuttaControl = false;
                    }
                    else
                    {
                        this.lastDetected.IsOuttaControl = this.lastDetected.IsZoneOutOfControl;
                        this.lastDetected.ReplacedZone = this.lastDetected.Zone;
                        this.facePriority.SetFirst(this.rotatedFacePriorityItem);
                        this.facePriority.SetSecond(this.profileFacePriorityItem);
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