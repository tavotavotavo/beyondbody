using Common.PriorityAlgorithm;
using Domain.Exceptions;
using Emgu.CV;
using Emgu.CV.Structure;
using Login.PriorityItems;
using ProcessingInterfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Login
{
    public class LoginService
    {
        private bool isLoggedIn;
        private bool shouldReloadImages;
        private Stopwatch timer;
        private string usersPath = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase) + "\\Users").LocalPath;
        private string loginPattern = "Login.bmp";
        private IMainProcessor processor;
        private LifoAlgorithm<UserImageItem> priority;
        private List<Image<Bgr, byte>> currentImages;

        public LoginService(IMainProcessor processor)
        {
            this.processor = processor;
            this.isLoggedIn = false;
            this.timer = new Stopwatch();
            this.timer.Start();
            this.priority = new LifoAlgorithm<UserImageItem>();
            this.currentImages = new List<Image<Bgr, byte>>();
        }

        public string CurrentUser { get; private set; }

        public bool IsLoggedIn()
        {
            if (isLoggedIn && timer.ElapsedMilliseconds > 5000)
            {
                isLoggedIn = false;
                this.processor.NotifyLogoff();
            }

            return isLoggedIn;
        }

        public void RestartTimer()
        {
            this.timer.Restart();
        }

        public void Login(Image<Bgr, byte> source)
        {
            var loginImagesPaths = new List<string>();

            if (Directory.Exists(this.usersPath))
                loginImagesPaths = Directory.GetFiles(this.usersPath, "*" + this.loginPattern, SearchOption.AllDirectories).ToList();

            if (!loginImagesPaths.Any())
            {
                this.isLoggedIn = true;
                this.timer.Restart();
                this.CurrentUser = "default";
                processor.NotifyLoginSuccess(this.CurrentUser);
            }
            else
            {
                if (this.shouldReloadImages || loginImagesPaths.Count != currentImages.Count)
                {
                    this.shouldReloadImages = false;
                    this.priority.Clear();
                    this.currentImages.Clear();

                    foreach (var loginPath in loginImagesPaths)
                    {
                        var image = new Image<Bgr, byte>(loginPath);
                        var path = new String(loginPath.ToArray());

                        this.currentImages.Add(image);

                        this.priority.AddAlgorithmItem(new UserImagePriorityItem(new UserImageItem(image, 1, path)));
                        this.priority.AddAlgorithmItem(new UserImagePriorityItem(new UserImageItem(image, 0.9, path)));
                        this.priority.AddAlgorithmItem(new UserImagePriorityItem(new UserImageItem(image, 0.8, path)));
                        this.priority.AddAlgorithmItem(new UserImagePriorityItem(new UserImageItem(image, 0.7, path)));
                        this.priority.AddAlgorithmItem(new UserImagePriorityItem(new UserImageItem(image, 0.6, path)));
                        this.priority.AddAlgorithmItem(new UserImagePriorityItem(new UserImageItem(image, 0.5, path)));
                        this.priority.AddAlgorithmItem(new UserImagePriorityItem(new UserImageItem(image, 0.4, path)));
                    }
                }

                var imageItem = this.priority.Next();

                var template = imageItem.Image.Copy();

                Image<Gray, float> result = source.Convert<Gray, byte>().SmoothGaussian(5)
                    .MatchTemplate(template.Convert<Gray, byte>()
                        .Resize(imageItem.ResizeScale, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR).SmoothGaussian(5), 
                            Emgu.CV.CvEnum.TM_TYPE.CV_TM_CCOEFF_NORMED);

                double[] minValues, maxValues;
                Point[] minLocations, maxLocations;
                result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);

                if (maxValues[0] > 0.9)
                {
                    this.isLoggedIn = true;
                    this.timer.Restart();
                    var newPath = imageItem.Path.Replace("\\", "|");
                    var splittedPath = newPath.Split('|');
                    this.CurrentUser = splittedPath[splittedPath.Length - 4];
                    processor.NotifyLoginSuccess(this.CurrentUser);
                    return;
                }
            }
        }

        public void Register(string userName, IEnumerable<IImage> images)
        {
            try
            {
                this.shouldReloadImages = true;

                var basePath = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
                var imagesPath = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase) +
                           "\\Users\\" + userName + "\\Login\\Images").LocalPath;

                if (!Directory.Exists(imagesPath))
                    Directory.CreateDirectory(imagesPath);

                for (int i = 0; i < images.Count(); i++)
                {
                    var fileName = "image-" + i.ToString() +  DateTime.Now.ToString("-HH-mm-ss-ff-") + this.loginPattern;

                    images.ToList()[i].Save(fileName);

                    File.Move(basePath + "\\" + fileName, imagesPath + "\\" + fileName);
                }
            }
            catch (Exception ex)
            {
                throw new RegisterException("Error al registrar usuario", ex);
            }
        }

        public void DeleteUsers()
        {
            var imagesPath = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase) +
                       "\\Users").LocalPath;

            if (Directory.Exists(imagesPath))
                Directory.Delete(imagesPath, true);
        }
    }
}