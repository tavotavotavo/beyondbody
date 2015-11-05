using Domain;
using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingInterfaces
{
    public interface IMainProcessor
    {
        void CloseAllProcesses();

        void WithGlasses();

        bool GetGlassesConfiguration();

        void WithoutGlasses();

        Image<Bgr, byte> GetCurrentMouthImage();

        Image<Bgr, byte> GetCurrentFaceImage();

        void RegisterUser(string userName, IEnumerable<IImage> images);

        void RegisterGestures(string userName, IEnumerable<Gesture> gestures);

        void NotifyLoginSuccess(string user);

        void WaitForActionAndExecute(Action action);

        void NotifyLogoff();

        void ActivateTaskLooper();

        void ActivateEventLooper();

        void ShowRightArrow();

        void ShowLeftArrow();

        void HideArrows();

        void BlockPrecision(bool shouldBlock);

        void ResetPrecision();

        bool GetBlockingConfiguration();
    }
}
