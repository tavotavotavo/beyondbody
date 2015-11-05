using BeyondBody;
using Detectors;
using Domain;
using Domain.Extensions;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using KeyboardSimulation.Simulators;
using Login;
using MouseSimulation.Simulators;
using Processing.Actions;
using Processing.Processors;
using Processing.States;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsUI;

namespace Processors
{
    public class ImageProcessor
    {
        private KeyboardSimulator keyboardSimulator = new KeyboardSimulator();
        private Image<Emgu.CV.Structure.Bgr, byte> currentImage;
        private Image<Emgu.CV.Structure.Bgr, byte> currentImageClean;
        private Face currentDetectedFace;
        private Face lastFrontalFace;
        private Stopwatch clickTimer;
        private Stopwatch mouthTimer;
        private Capture webCam;
        private FormMain mainForm;
        private EyeDetector eyeDetector;
        private FaceDetector faceDetector;
        private SpeechProcessor speechProcessor;
        private CursorActionProcessor cursorActionProcessor;
        private DeactivateActionProcessor deactivateActionProcessor;
        private LoginService loginService;
        private CursorLoopProcessor cursorLoopProcessor;

        private KeyboardAction<FaceState> activateSpeechAction;
        private int totalCountValidFaces;
        private int totalLeftEyeClosed;
        private int totalRightEyeClosed;
        private TrainingBox trainBox;
        private CursorSimulator cursorSimulator;
        private bool shouldBlockPrecision;
        private bool isLookingForGestures;
        private bool shouldDrawEyes;

        public ImageProcessor(FormMain mainForm, LoginService loginService, GesturesService gesturesService, TrainingBox trainBox, CursorSimulator cursorSimulator)
        {
            try
            {
                this.cursorSimulator = cursorSimulator;
                this.webCam = new Capture(); //Inicializa la camara
                this.mainForm = mainForm;
                this.loginService = loginService;
                this.gesturesService = gesturesService;
                this.trainBox = trainBox;

                this.clickTimer = new Stopwatch();
                this.mouthTimer = new Stopwatch();
                this.speechProcessor = new SpeechProcessor();
                this.eyeDetector = new EyeDetector();
                this.faceDetector = new FaceDetector(this.eyeDetector);
                this.cursorLoopProcessor = new CursorLoopProcessor(this.cursorSimulator);
                this.cursorActionProcessor = new CursorActionProcessor();
                this.deactivateActionProcessor = new DeactivateActionProcessor(this.speechProcessor, this.cursorLoopProcessor);

                this.activateSpeechAction = new ActivateSpeechAction();

                Application.Idle += new EventHandler(this.cursorLoopProcessor.Pool);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsBeeingConfigurated { get; set; }

        internal void ProcessImages(object sender, EventArgs e)
        {
            this.currentImage = webCam.QueryFrame();
            this.currentImageClean = this.currentImage.Copy();

            this.currentDetectedFace = this.faceDetector.DetectFace(currentImage);

            if (!currentDetectedFace.IsEmpty)
            {
                if (this.loginService.IsLoggedIn())
                {
                    if (!currentDetectedFace.IsFake && !currentDetectedFace.IsOuttaControl)
                        this.loginService.RestartTimer();

                    this.deactivateActionProcessor.Process(currentDetectedFace);

                    this.shouldDrawEyes = false;

                    if (currentDetectedFace.IsFrontal)
                    {
                        var eyesColor = Color.DarkRed;

                        //Dejamos de mover el puntero del mouse
                        this.cursorLoopProcessor.Finish();

                        if (currentDetectedFace.HasEyesCentered && !currentDetectedFace.HasBothEyesClosed)
                        {
                            this.lastFrontalFace = this.currentDetectedFace;
                            eyesColor = Color.Green;

                            if (!this.clickTimer.IsRunning) { this.clickTimer.Start(); }

                            if (this.clickTimer.ElapsedMilliseconds > 5000)
                            {
                                long allowedFakeClicks = (long)(this.clickTimer.ElapsedMilliseconds / 3 / 1000) + 1;

                                if (this.cursorActionProcessor.FakeClicks > allowedFakeClicks)
                                {
                                    if (!this.shouldBlockPrecision)
                                        this.eyeDetector.IncreasePrecision();

                                    this.clickTimer.Restart();
                                    this.cursorActionProcessor.ResetClicksCount();
                                };
                            }

                            this.shouldDrawEyes = true;
                        }
                        else
                        {
                            if (this.clickTimer.IsRunning)
                            {
                                this.clickTimer.Stop();
                            }
                            if (this.clickTimer.ElapsedMilliseconds > 30000)
                            {
                                this.clickTimer.Restart();
                                this.cursorActionProcessor.ResetClicksCount();
                            }
                        }

                        this.cursorActionProcessor.Process(currentDetectedFace);

                        this.activateSpeechAction.NextState(currentDetectedFace);

                        if (this.activateSpeechAction.ShouldBeExecuted())
                        {
                            if (!this.speechProcessor.IsStarted())
                            {
                                this.speechProcessor.Start();
                            }
                            else
                            {
                                this.speechProcessor.Finish();
                            }
                        }

                        if (this.speechProcessor.IsStarted() && !this.isLookingForGestures)
                        {
                            this.isLookingForGestures = true;

                            Task.Run(() =>
                            {
                                while (this.speechProcessor.IsStarted() && this.isLookingForGestures)
                                {
                                    if (this.mouthTimer.ElapsedMilliseconds > 1000)
                                    {
                                        var word = this.gesturesService.Detect(this.currentDetectedFace, this.currentImageClean);

                                        if (!string.IsNullOrWhiteSpace(word.Value))
                                        {
                                            this.keyboardSimulator.PressKey(word);
                                        }

                                        this.mouthTimer.Restart();
                                    }
                                    else
                                    {
                                        if (!this.mouthTimer.IsRunning)
                                        {
                                            this.mouthTimer.Start();
                                        }
                                    }
                                }

                                this.isLookingForGestures = false;
                            });
                        }
                        else
                        {
                            this.mouthTimer.Stop();
                        }

                        //Dibujamos en la imagen la cara detectada
                        //this.DrawZone(currentDetectedFace, Color.Red);

                        //Dibujamos la zona de control
                        this.DrawRectangle(currentDetectedFace.Image, currentDetectedFace.ControlZone, Color.DarkRed);

                        //Dibujamos el centro de control de la cara
                        this.DrawCircle(currentDetectedFace.Image, currentDetectedFace.Center, Color.Red);
                    }
                    else if (!this.currentDetectedFace.IsOuttaControl)
                    {
                        this.cursorLoopProcessor.Start();
                        this.activateSpeechAction.Reset();

                        if (currentDetectedFace.IsProfile)
                        {
                            if (currentDetectedFace.IsLeftProfile)
                            {
                                this.cursorLoopProcessor.MoveCursorToLeft();
                                //this.DrawZone(currentDetectedFace, Color.AntiqueWhite);

                                //Dibujamos el centro de control de la cara
                                this.DrawCircle(currentDetectedFace.Image,
                                    new Point 
                                    { 
                                        X = currentDetectedFace.ControlZone.X + currentDetectedFace.ControlZone.Width, 
                                        Y = currentDetectedFace.Center.Y 
                                    },
                                    Color.SkyBlue);
                            }
                            else if (currentDetectedFace.IsRightProfile)
                            {
                                this.cursorLoopProcessor.MoveCursorToRight();
                                //this.DrawZone(currentDetectedFace, Color.SkyBlue);

                                //Dibujamos el centro de control de la cara
                                this.DrawCircle(currentDetectedFace.Image,
                                    new Point
                                    {
                                        X = currentDetectedFace.ControlZone.X,
                                        Y = currentDetectedFace.Center.Y
                                    },
                                    Color.SkyBlue);
                            }

                            this.DrawRectangle(currentDetectedFace.Image, currentDetectedFace.ControlZone, Color.DarkBlue);
                        }
                        else
                        {
                            if (currentDetectedFace.IsRotated)
                            {
                                if (currentDetectedFace.IsLeftRotated)
                                {
                                    this.cursorLoopProcessor.MoveCursorToBottom();

                                    //this.DrawZone(currentDetectedFace, Color.Violet);
                                }
                                else if (currentDetectedFace.IsRightRotated)
                                {
                                    this.cursorLoopProcessor.MoveCursorToTop();

                                    //this.DrawZone(currentDetectedFace, Color.Violet);
                                }

                                //Dibujamos la zona de control
                                this.DrawRectangle(currentDetectedFace.Image, currentDetectedFace.ControlZone, Color.DarkViolet);
                                //Dibujamos el centro de control de la cara
                                this.DrawCircle(currentDetectedFace.Image, currentDetectedFace.Center, Color.Violet);
                            }
                        }
                    }
                    else
                    {
                        //Dejamos de mover el puntero del mouse
                        this.cursorLoopProcessor.Finish();

                        this.DrawCircle(currentDetectedFace.Image, currentDetectedFace.ReplacedZone.Center(), Color.Black);
                        this.DrawRectangle(currentDetectedFace.Image, currentDetectedFace.ControlZone, Color.Black);
                    }

                    if (this.shouldDrawEyes)
                    {
                        this.DrawZone(currentDetectedFace.RightEye, Color.YellowGreen);

                        this.DrawZone(currentDetectedFace.LeftEye, Color.YellowGreen);
                    }
                }
                else
                {
                    if (!this.currentDetectedFace.IsFake)
                        this.loginService.Login(this.currentImageClean);

                    this.cursorActionProcessor.ResetActions();
                    this.activateSpeechAction.Reset();
                    this.speechProcessor.Finish();
                    this.cursorLoopProcessor.Finish();

                    if (!this.shouldBlockPrecision)
                        this.ResetPrecision();
                }
            }
            else
            {
                this.cursorActionProcessor.ResetActions();
                this.activateSpeechAction.Reset();
                this.deactivateActionProcessor.Process(currentDetectedFace);
            }

            this.Show(currentImage, currentDetectedFace);
        }

        private void CalculatePercetages(Face detectedFace)
        {
            if (detectedFace.HasEyesCentered)
            {
                this.totalCountValidFaces++;

                if (detectedFace.HasBothEyesClosed)
                {
                    this.totalLeftEyeClosed++;
                    this.totalRightEyeClosed++;
                }
                else if (detectedFace.IsBlinkingLeftEye)
                {
                    this.totalLeftEyeClosed++;
                }
                else if (detectedFace.IsBlinkingRightEye)
                {
                    this.totalRightEyeClosed++;
                }
            }
        }

        private void DrawZone(ZoneEntity entity, Color color)
        {
            if (entity.Image != null)
                entity.Image.Draw(entity.Zone, new Bgr(color), 10);
        }

        private void DrawRectangle(Image<Bgr, byte> image, Rectangle rectangle, Color color)
        {
            image.Draw(rectangle, new Bgr(color), 10);
        }

        private void DrawCircle(Image<Bgr, byte> image, Point point, Color color)
        {
            var circle = new CircleF();
            circle.Center = new PointF { X = point.X, Y = point.Y };
            circle.Radius = 8;
            image.Draw(circle, new Bgr(color), 15);
        }

        private void Show(Image<Bgr, byte> image, Face face)
        {
            this.mainForm.ShowOriginalImage(image.Flip(FLIP.HORIZONTAL), face);
            this.trainBox.ShowOriginalImage(image, face);
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

        internal void CloseAllProcesses()
        {
            this.webCam.Dispose();
            this.speechProcessor.CloseAllProcesses();
        }

        public void WithGlasses()
        {
            this.eyeDetector.ConfigureWithGlasses();
        }

        public void WithoutGlasses()
        {
            this.eyeDetector.ConfigureWithoutGlasses();
        }

        internal Image<Bgr, byte> GetCurrentMouthImage()
        {
            var imageCopy = this.currentImageClean.Copy();

            imageCopy.ROI = this.currentDetectedFace.MouthZone;

            return imageCopy;
        }

        internal Image<Bgr, byte> GetCurrentFaceImage()
        {
            var imageCopy = this.currentImageClean.Copy();

            imageCopy.ROI = this.lastFrontalFace.LoginZone;

            return imageCopy;
        }

        public GesturesService gesturesService { get; set; }

        internal void BlockPrecision(bool shouldBlock)
        {
            this.shouldBlockPrecision = shouldBlock;
        }

        internal void ResetPrecision()
        {
            this.eyeDetector.ResetPrecision();
        }

        internal bool GetGlassesConfiguration()
        {
            return this.eyeDetector.GetGlassesConfiguration();
        }

        internal bool GetBlockingConfiguration()
        {
            return this.shouldBlockPrecision;
        }
    }
}