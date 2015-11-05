using BeyondBody;
using Detectors;
using Domain;
using Emgu.CV;
using Emgu.CV.Structure;
using Login;
using MouseSimulation.Simulators;
using Processing.Actions;
using Processing.Processors;
using Processing.States;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Processors
{
    public class ImageProcessor
    {
        private Capture webCam;
        private FormMain mainForm;
        private EyeDetector eyeDetector;
        private FaceDetector faceDetector;
        private SpeechProcessor speechProcessor;
        private CursorLoopProcessor cursorLoopProcessor;
        private CursorActionProcessor cursorActionProcessor;
        private LoginService loginService;

        private KeyboardAction<FaceState> activateSpeechAction;

        public ImageProcessor(FormMain mainForm)
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
                this.speechProcessor = new SpeechProcessor();
                this.faceDetector = new FaceDetector();
                this.eyeDetector = new EyeDetector();

                this.loginService = new LoginService();

                this.cursorLoopProcessor = new CursorLoopProcessor();
                this.cursorActionProcessor = new CursorActionProcessor();

                this.activateSpeechAction = new ActivateSpeechAction();

                Application.Idle += new EventHandler(this.cursorLoopProcessor.Pool);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal void ProcessImages(object sender, EventArgs e)
        {
            var image = webCam.QueryFrame();

            var detectedFaces = this.faceDetector.DetectFaces(image);
            var detectedFace = this.GetCentered(detectedFaces);

            if (!detectedFace.IsEmpty)
            {
                if (this.loginService.IsLoggedIn())
                {
                    this.loginService.RestartTimer();

                    if (detectedFace.IsFrontal)
                    {
                        //Dejamos de mover el puntero del mouse
                        this.cursorLoopProcessor.Finish();

                        //Dibujamos en la imagen la cara detectada
                        this.DrawZone(detectedFace, Color.Red);

                        this.eyeDetector.DetectRightEye(detectedFace);
                        this.DrawZone(detectedFace.RightEye, Color.Blue);

                        this.eyeDetector.DetectLeftEye(detectedFace);
                        this.DrawZone(detectedFace.LeftEye, Color.Green);

                        //Procesamos las acciones
                        //Basicamente son dos acciones:
                        //- pasar al siguiente estado
                        //- ejecutar la accion si corresponde
                        this.cursorActionProcessor.Process(detectedFace);

                        this.activateSpeechAction.NextState(detectedFace);

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
                    }
                    else
                    {
                        //Comienza el movimiento del cursor 
                        this.cursorLoopProcessor.Start();
                        this.activateSpeechAction.Reset();

                        if (detectedFace.IsProfile)
                        {
                            if (detectedFace.IsLeftProfile)
                            {
                                this.cursorLoopProcessor.MoveCursorToLeft();

                                this.DrawZone(detectedFace, Color.SkyBlue);
                            }
                            else if (detectedFace.IsRightProfile)
                            {
                                this.cursorLoopProcessor.MoveCursorToRight();

                                this.DrawZone(detectedFace, Color.Yellow);
                            }
                        }
                        else
                        {
                            if (detectedFace.IsRotated)
                            {
                                if (detectedFace.IsLeftRotated)
                                {
                                    this.cursorLoopProcessor.MoveCursorToBottom();

                                    this.DrawZone(detectedFace, Color.Violet);
                                }
                                else if (detectedFace.IsRightRotated)
                                {
                                    this.cursorLoopProcessor.MoveCursorToTop();

                                    this.DrawZone(detectedFace, Color.YellowGreen);
                                }
                            }
                        }
                    }
                }
                else
                {
                    this.loginService.Login(image);
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
            }

            this.Show(image);
        }

        private void DrawZone(ZoneEntity entity, Color color)
        {
            entity.Image.Draw(entity.Zone, new Bgr(color), 2);
        }

        private void Show(Image<Bgr, byte> image)
        {
            this.mainForm.ShowOriginalImage(image);
        }

        private Face GetCentered(IEnumerable<Face> detectedFaces)
        {
            var face = detectedFaces.FirstOrDefault();

            return face ?? new Face();
        }

        internal void CloseAllProcesses()
        {
            this.speechProcessor.CloseAllProcesses();
        }
    }
}