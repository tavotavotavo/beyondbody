using BeyondBody;
using Detectors;
using Domain;
using Emgu.CV;
using Emgu.CV.Structure;
using Login;
using ProcessingInterfaces;
using Processors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsUI;

namespace Processing.Processors
{
    public class MainProcessor : IMainProcessor
    {
        private FormMain mainForm;
        private ImageProcessor imageProcessor;
        private LoginService loginService;
        private GesturesService gesturesService;
        private bool IsEventLooperActivated = false;
        private RightArrow rightArrow;
        private LeftArrow leftArrow;
        private TrainingBox trainBox;

        public MainProcessor()
        {

            this.leftArrow = new LeftArrow();
            this.rightArrow = new RightArrow();
            this.loginService = new LoginService(this);
            this.gesturesService = new GesturesService(this.loginService);
            this.trainBox = new TrainingBox();
            this.mainForm = new FormMain(this, this.loginService, this.gesturesService, this.trainBox);
            this.imageProcessor = new ImageProcessor(this.mainForm, this.loginService, this.gesturesService, this.trainBox);
            this.ProcessImages();
            this.ShowForm();
        }

        private void ShowForm()
        {
            Application.Run(this.mainForm);
        }

        private void ProcessImages()
        {
            //Application.Idle += this.ProcessImages;

            Task.Run(() =>
            {
                while (!this.IsEventLooperActivated)
                {
                    this.ProcessImages(new object[] { }, new EventArgs());
                }
            });
        }

        internal void ProcessImages(object sender, EventArgs e)
        {
            if (!this.imageProcessor.IsBeeingConfigurated)
                this.imageProcessor.ProcessImages(sender, e);
        }

        public void CloseAllProcesses()
        {
            this.imageProcessor.CloseAllProcesses();
        }

        public void WithGlasses()
        {
            this.WaitForActionAndExecute(() => { this.imageProcessor.WithGlasses(); });
        }

        public void WithoutGlasses()
        {
            this.WaitForActionAndExecute(() => { this.imageProcessor.WithoutGlasses(); });
        }

        public Image<Bgr, byte> GetCurrentMouthImage()
        {
            return this.imageProcessor.GetCurrentMouthImage();
        }

        public Image<Bgr, byte> GetCurrentFaceImage()
        {
            return this.imageProcessor.GetCurrentFaceImage();
        }

        public void RegisterUser(string userName, IEnumerable<IImage> images)
        {
            this.WaitForActionAndExecute(() => { this.loginService.Register(userName, images); });
        }

        public void WaitForActionAndExecute(Action action)
        {
            this.imageProcessor.IsBeeingConfigurated = true;

            Stopwatch startTimer = new Stopwatch();
            startTimer.Start();

            while (startTimer.ElapsedMilliseconds < 1000)
            {
            }

            startTimer.Stop();

            action.Invoke();

            this.imageProcessor.IsBeeingConfigurated = false;
        }

        public void NotifyLoginSuccess(string user)
        {
            this.mainForm.NotifyLoginSuccess(user);
        }

        public void NotifyLogoff()
        {
            this.mainForm.NotifyLogoff();
            Cursor.Position = new Point(Screen.PrimaryScreen.Bounds.Width / 2,
                            Screen.PrimaryScreen.Bounds.Height / 2);
        }

        public void ActivateTaskLooper()
        {
            if (this.IsEventLooperActivated)
            {
                this.IsEventLooperActivated = false;

                Application.Idle -= this.ProcessImages;
                this.ProcessImages();
            }
        }

        public void ActivateEventLooper()
        {
            if (!this.IsEventLooperActivated)
            {
                this.IsEventLooperActivated = true;

                Application.Idle += this.ProcessImages;
            }
        }

        public void RegisterGestures(string userName, IEnumerable<Gesture> gestures)
        {
            this.gesturesService.Register(userName, gestures);
        }

        public void HideArrows()
        {
            //this.rightArrow.Hide();
            //this.leftArrow.Hide();
        }

        public void ShowRightArrow()
        {
            //this.leftArrow.Show();
        }

        public void ShowLeftArrow()
        {
            //this.rightArrow.Show();
        }
    }
}