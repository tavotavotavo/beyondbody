using BeyondBody;
using ProcessingInterfaces;
using Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Processing.Processors
{
    public class MainProcessor : IMainProcessor
    {
        private FormMain mainForm;
        private ImageProcessor imageProcessor;

        public MainProcessor() {
            this.mainForm = new FormMain(this);
            this.imageProcessor = new ImageProcessor(this.mainForm);
            this.ProcessImages();
            this.ShowForm();
        }

        private void ShowForm()
        {
            Application.Run(this.mainForm);
        }

        private void ProcessImages()
        {
            Application.Idle += new EventHandler(this.imageProcessor.ProcessImages);
        }

        public void CloseAllProcesses()
        {
            this.imageProcessor.CloseAllProcesses();
        }
    }
}
