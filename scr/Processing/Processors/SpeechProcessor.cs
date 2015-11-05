using KeyboardSimulation.Simulators;
using SpeechDetection.Detectors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Processing.Processors
{
    public class SpeechProcessor
    {
        private KeyboardSimulator keyboardSimulator;
        private SpeechDetector speechDetector;
        private bool isStarted;
        private IList<Process> processes;

        public SpeechProcessor()
        {
            this.keyboardSimulator = new KeyboardSimulator();
            this.speechDetector = new SpeechDetector();
            this.processes = new List<Process>();

            this.speechDetector.Setup(this.keyboardSimulator.PressKeys);
        }

        internal void Start()
        {
            if (!this.isStarted)
            {
                this.speechDetector.StartDetecting();

                this.isStarted = true;

                Stopwatch timer = new Stopwatch();

                var winFolder = Environment.GetFolderPath(Environment.SpecialFolder.Windows);

                var path = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)) + "\\ThirdPartyPrograms\\On-ScreenKeyboardPortable\\On-ScreenKeyboardPortable.exe";

                var process = Process.Start(path);

                process.Exited += this.Finish;

                this.processes.Add(process);
            }
        }

        private void Finish(object o, EventArgs e)
        {
            this.Finish();
        }

        internal void Finish()
        {
            if (this.isStarted)
            {
                this.speechDetector.StopDetecting();

                this.isStarted = false;

                this.KillAllProcesses();
            }
        }

        internal bool IsStarted()
        {
            return this.isStarted;
        }

        internal void CloseAllProcesses()
        {
            this.KillAllProcesses();
        }

        private void KillAllProcesses()
        {
            if (this.isStarted)
                this.keyboardSimulator.PressAddSymbol();

            try
            {
                Process[] proc2 = Process.GetProcessesByName("On-ScreenKeyboardPortable");
                Process[] proc = Process.GetProcessesByName("osk");

                proc[0].CloseMainWindow();
                proc[0].Close();
                proc2[0].CloseMainWindow();
                proc2[0].Close();
            }
            catch (Exception)
            {
            }

            this.processes.Clear();
        }
    }
}