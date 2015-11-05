using Detectors;
using Domain;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Login;
using ProcessingInterfaces;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using WindowsFormsUI;

namespace BeyondBody
{
    public partial class FormMain : Form
    {
        private IMainProcessor processor;
        private LoginService loginService;
        private GesturesService gesturesService;
        private TrainingBox trainBox;

        public FormMain(IMainProcessor processor, LoginService loginService, GesturesService gesturesService, TrainingBox trainBox)
        {
            this.processor = processor;
            this.loginService = loginService;
            this.trainBox = trainBox;
            this.gesturesService = gesturesService;
            InitializeComponent();
        }

        private void FormMain_Closed(object sender, FormClosedEventArgs e)
        {
            this.processor.CloseAllProcesses();
        }

        public void ShowOriginalImage(Image<Bgr, byte> drawedImage, Face face)
        {
            this.OriginalImageViewer.Image = drawedImage.Resize(this.OriginalImageViewer.Width, this.OriginalImageViewer.Height, INTER.CV_INTER_LINEAR);
            //this.trainBox.ShowOriginalImage(drawedImage, face);
        }

        public void ShowRightEye(Image<Bgr, byte> rightEye)
        {
            //this.ShowEye(rightEye, this.rightEyeBox);
        }

        public void ShowLeftEye(Image<Bgr, byte> leftEye)
        {
            //this.ShowEye(leftEye, this.leftEyeBox);
        }

        public void ShowRightBitMapEye(Image<Gray, byte> rightEye)
        {
            //this.ShowBitMapEye(rightEye, this.rightBitMapEyeBox);
        }

        public void ShowLeftBitMapEye(Image<Gray, byte> leftEye)
        {
            //this.ShowBitMapEye(leftEye, this.leftBitMapEyeBox);
        }

        private void ShowBitMapEye(Image<Gray, byte> eye, ImageBox imageBox)
        {
            imageBox.Width = eye.ROI.Width * 2;
            imageBox.Height = eye.ROI.Height * 2;
            imageBox.Image = eye;
        }

        private void ShowEye(Image<Bgr, byte> eye, ImageBox imageBox)
        {
            imageBox.Width = eye.ROI.Width * 2;
            imageBox.Height = eye.ROI.Height * 2;
            imageBox.Image = eye;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm(this.processor);

            registerForm.ShowDialog();
        }

        private void configButton_Click(object sender, EventArgs e)
        {
            ConfigForm configForm = new ConfigForm(this.processor, this.gesturesService);
            configForm.ShowDialog();
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            //if (FormWindowState.Minimized == this.WindowState)
            //{
            //    notifyIcon.ShowBalloonTip(500);
            //    this.Hide();
            //}
            //else if (FormWindowState.Normal == this.WindowState)
            //{
            //    notifyIcon.Visible = false;
            //}
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.BringToFront();
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            var path = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)) + "\\ThirdPartyPrograms\\" + "Beyond Body.chm";

            var process = Process.Start(path);
        }

        private void FormMain_Load_1(object sender, EventArgs e)
        {
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            notifyIcon.Visible = true;
            this.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.processor.ActivateTaskLooper();
            this.Visible = false;
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.processor.ActivateEventLooper();
            this.Show();
            this.BringToFront();
        }

        public void NotifyLoginSuccess(string user)
        {
            notifyIcon.ShowBalloonTip(4000, "BeyondBody - Login", "Usted ha sido logueado como " + user, ToolTipIcon.Info);
        }

        public void NotifyLogoff()
        {
            notifyIcon.ShowBalloonTip(4000, "BeyondBody - Login", "Usted ha sido deslogueado", ToolTipIcon.Info);
        }

        private void RemoveUsersButton_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Esta seguro que desea eliminar todos los usuarios", "Eliminación de Usuarios", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                this.processor.WaitForActionAndExecute(() =>
                {
                    this.loginService.DeleteUsers();
                });
            }
        }

        private void DeleteConfigurationButton_Click(object sender, EventArgs e)
        {
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.trainBox == null)
                this.trainBox = new TrainingBox();

            if (this.checkBox1.Checked)
            {
                this.trainBox.Show();
            }
            else
            {
                this.trainBox.Hide();
            }
        }

        public bool ShouldShowTrain()
        {
            return this.checkBox1.Checked;
        }
    }
}