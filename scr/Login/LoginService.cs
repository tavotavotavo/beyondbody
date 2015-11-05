using Emgu.CV;
using Emgu.CV.Structure;
using System.Diagnostics;

namespace Login
{
    public class LoginService
    {
        private bool isLoggedIn;
        private Stopwatch timer;

        public LoginService()
        {
            this.isLoggedIn = false;
            this.timer = new Stopwatch();
            this.timer.Start();
        }

        public bool IsLoggedIn()
        {
            if (isLoggedIn && timer.ElapsedMilliseconds > 10000)
                isLoggedIn = false;

            return isLoggedIn;
        }

        public void RestartTimer()
        {
            this.timer.Restart();
        }

        public void Login(Image<Bgr, byte> originalImage)
        {
            this.isLoggedIn = true; //buscar en la base de datos la imagen
            this.timer.Restart();
        }
    }
}