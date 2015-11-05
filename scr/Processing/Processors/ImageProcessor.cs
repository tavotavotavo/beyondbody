using BeyondBody;
using Detectors;
using Domain;
using Domain.Extensions;
using Emgu.CV;
using Emgu.CV.Structure;
using KeyboardSimulation.Simulators;
using Login;
using Processing.Actions;
using Processing.Processors;
using Processing.States;
using ProcessingInterfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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
        private MiniClickAction miniClickAction;
        private IMainProcessor processor;
        private TrainingBox trainBox;

        public ImageProcessor(FormMain mainForm, LoginService loginService, GesturesService gesturesService, TrainingBox trainBox)
        {
            try
            {
                this.webCam = new Capture(); //Inicializa la camara
                // this.webCam = "USB\VID_04F2&PID_B272&REV_1156&MI_00";
                //USB\VID_04F2&PID_B272&MI_00;

                //TODO: Ver porque en la compu de Vale no anduvo,
                //quizá falta instalar Emgu en la computadora
                //hay que tenerlo en cuenta para el instalador
                this.mainForm = mainForm;
                this.loginService = loginService;
                this.gesturesService = gesturesService;
                this.trainBox = trainBox;

                this.clickTimer = new Stopwatch();
                this.mouthTimer = new Stopwatch();
                this.speechProcessor = new SpeechProcessor();
                this.faceDetector = new FaceDetector();
                this.eyeDetector = new EyeDetector();
                this.cursorLoopProcessor = new CursorLoopProcessor();
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
                    if (!currentDetectedFace.IsFake)
                        this.loginService.RestartTimer();

                    this.deactivateActionProcessor.Process(currentDetectedFace);

                    if (currentDetectedFace.IsFrontal)
                    {
                        //this.processor.HideArrows();

                        var eyesColor = Color.DarkRed;

                        //Dejamos de mover el puntero del mouse
                        this.cursorLoopProcessor.Finish();

                        this.eyeDetector.DetectRightEye(currentDetectedFace);
                        this.eyeDetector.DetectLeftEye(currentDetectedFace);

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
                                    this.eyeDetector.IncreasePrecision();
                                    this.clickTimer.Restart();
                                    this.cursorActionProcessor.ResetClicksCount();
                                };
                            }

                            this.DrawZone(currentDetectedFace.RightEye, eyesColor);

                            this.DrawZone(currentDetectedFace.LeftEye, eyesColor);
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

                        //Procesamos las acciones
                        //Basicamente son dos acciones:
                        //- pasar al siguiente estado
                        //- ejecutar la accion si corresponde
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

                        if (this.speechProcessor.IsStarted())
                        {
                            if (this.mouthTimer.ElapsedMilliseconds > 1000)
                            {
                                this.gesturesService.Detect(this.currentDetectedFace);

                                if (this.currentDetectedFace.Mouth.IsMakingGesture)
                                {
                                    this.keyboardSimulator.PressKey(new Word(this.currentDetectedFace.Mouth.Word));

                                    this.currentDetectedFace.Mouth.ClearGesture();
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
                        else
                        {
                            this.mouthTimer.Stop();
                        }

                        //Dibujamos en la imagen la cara detectada
                        this.DrawZone(currentDetectedFace, Color.Red);

                        //Dibujamos la zona de control
                        this.DrawRectangle(currentDetectedFace.Image, currentDetectedFace.ControlZone, Color.DarkRed);
                    }
                    else
                    {
                        //Comienza el movimiento del cursor
                        //if (this.lastFrontalFace.Center.X > this.lastFrontalFace.Image.Center().X)
                        //{
                        //    this.processor.ShowRightArrow();
                        //}
                        //else
                        //{
                        //    this.processor.ShowLeftArrow();
                        //}

                        this.cursorLoopProcessor.Start();
                        this.activateSpeechAction.Reset();

                        if (currentDetectedFace.IsProfile)
                        {
                            if (currentDetectedFace.IsLeftProfile)
                            {
                                this.cursorLoopProcessor.MoveCursorToLeft();
                            }
                            else if (currentDetectedFace.IsRightProfile)
                            {
                                this.cursorLoopProcessor.MoveCursorToRight();
                            }

                            this.DrawZone(currentDetectedFace, Color.SkyBlue);
                        }
                        else
                        {
                            if (currentDetectedFace.IsRotated)
                            {
                                if (currentDetectedFace.IsLeftRotated)
                                {
                                    this.cursorLoopProcessor.MoveCursorToBottom();

                                    this.DrawZone(currentDetectedFace, Color.Violet);
                                }
                                else if (currentDetectedFace.IsRightRotated)
                                {
                                    this.cursorLoopProcessor.MoveCursorToTop();

                                    this.DrawZone(currentDetectedFace, Color.Violet);
                                }

                                //Dibujamos la zona de control
                                this.DrawRectangle(currentDetectedFace.Image, currentDetectedFace.RotatedControlZone, Color.DarkViolet);
                            }
                        }
                    }

                    //Dibujamos el centro de control de la cara
                    this.DrawCircle(currentDetectedFace.Image, currentDetectedFace.Center, Color.Red);
                }
                else
                {
                    this.loginService.Login(this.currentImageClean);
                    this.cursorActionProcessor.ResetActions();
                    this.activateSpeechAction.Reset();
                    this.speechProcessor.Finish();
                    this.cursorLoopProcessor.Finish();
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
            entity.Image.Draw(entity.Zone, new Bgr(color), 2);
        }

        private void DrawRectangle(Image<Bgr, byte> image, Rectangle rectangle, Color color)
        {
            image.Draw(rectangle, new Bgr(color), 2);
        }

        private void DrawCircle(Image<Bgr, byte> image, Point point, Color color)
        {
            var circle = new CircleF();
            circle.Center = new PointF { X = point.X, Y = point.Y };
            circle.Radius = 4;
            image.Draw(circle, new Bgr(color), 5);
        }

        private void Show(Image<Bgr, byte> image, Face face)
        {
            this.mainForm.ShowOriginalImage(image, face);
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

            this.eyeDetector.DetectRightEye(currentDetectedFace);
            this.eyeDetector.DetectLeftEye(currentDetectedFace);

            if (this.currentDetectedFace.HasBothEyesOpen)
                imageCopy.ROI = this.currentDetectedFace.LoginZone;
            else
                imageCopy.ROI = this.currentDetectedFace.Zone;

            return imageCopy;
        }

        public GesturesService gesturesService { get; set; }
    }
}