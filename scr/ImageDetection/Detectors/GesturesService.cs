using Domain;
using Emgu.CV;
using Emgu.CV.Structure;
using Login;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Detectors
{
    public class GesturesService
    {
        private string gesturesPattern = "Gestures.bmp";
        private string usersPath = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase) + "\\Users").LocalPath;
        private LoginService loginService;
        private bool shouldReloadImages;

        public GesturesService(LoginService loginService)
        {
            this.loginService = loginService;
        }

        public Word Detect(Face face, Image<Bgr, byte> cleanImage)
        {
            var loginImagesPaths = new List<string>();

            if (Directory.Exists(this.usersPath))
                loginImagesPaths = Directory.GetFiles(this.usersPath, "*" + this.gesturesPattern, SearchOption.AllDirectories).ToList();

            loginImagesPaths = loginImagesPaths.Where(x => x.Contains(this.loginService.CurrentUser)).ToList();

            if (!loginImagesPaths.Any())
            {
                return new Word(string.Empty);
            }
            else
            {
                if (this.shouldReloadImages)
                {
                    this.shouldReloadImages = false;
                }
                var source = cleanImage.Copy();
                source.ROI = new Rectangle
                {
                    X = face.Zone.X,
                    Y = face.Zone.Y,
                    Width = face.Zone.Width + face.Zone.Width / 3,
                    Height = face.Zone.Height + face.Zone.Height / 3
                };

                for (double scale = 1.2; scale > 0.4; scale = scale - 0.1)
                {
                    foreach (var loginPath in loginImagesPaths)
                    {
                        var template = new Image<Bgr, byte>(loginPath);
                        var path = new String(loginPath.ToArray());

                        Image<Gray, float> result = source.Convert<Gray, byte>().SmoothGaussian(5)
                            .MatchTemplate(template.Convert<Gray, byte>()
                                .Resize(scale, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR).SmoothGaussian(5),
                                    Emgu.CV.CvEnum.TM_TYPE.CV_TM_CCOEFF_NORMED);

                        double[] minValues, maxValues;
                        Point[] minLocations, maxLocations;
                        result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);

                        if (maxValues[0] > 0.9)
                        {
                            var newPath = path.Replace("\\", "|");
                            var splittedPath = newPath.Split('|');
                            if (face.Mouth.IsEmpty)
                                face.Mouth = new Mouth();
                            face.Mouth.Word = new Word(splittedPath[splittedPath.Length - 2]);
                            return face.Mouth.Word;
                        }
                    }
                }
            }

            return new Word(string.Empty);
        }

        public void Register(string userName, IEnumerable<Gesture> gestures)
        {
            try
            {
                this.shouldReloadImages = true;

                if (string.IsNullOrWhiteSpace(userName))
                {
                    userName = "default";
                }

                var basePath = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
                var userPath = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase) +
                           "\\Users\\" + userName).LocalPath;
                var imagesPath = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase) +
                           "\\Users\\" + userName + "\\Gestures").LocalPath;

                if (!Directory.Exists(userPath) && userName != "default")
                    throw new Exception("El usuario no esta registrado");

                if (!Directory.Exists(imagesPath))
                    Directory.CreateDirectory(imagesPath);

                var index = 0;

                foreach (var gesture in gestures)
                {
                    var fileName = "image-" + index.ToString() + "-" + gesture.Word.Value + DateTime.Now.ToString("-HH-mm-ss-ff-") + this.gesturesPattern;

                    gesture.Image.Save(fileName);

                    if (!Directory.Exists(imagesPath + "\\" + gesture.Word.Value))
                        Directory.CreateDirectory(imagesPath + "\\" + gesture.Word.Value);

                    File.Move(basePath + "\\" + fileName, imagesPath + "\\" + gesture.Word.Value + "\\" + fileName);

                    index++;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar gestos", ex);
            }
        }

        public void DeleteGestures(string user)
        {
            var imagesPath = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase) +
                       "\\Users\\" + user + "\\Gestures").LocalPath;

            if (Directory.Exists(imagesPath))
                Directory.Delete(imagesPath, true);
        }
    }
}