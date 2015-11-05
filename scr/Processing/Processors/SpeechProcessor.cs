using KeyboardSimulation.Simulators;
using SpeechDetection.Detectors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

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

                var winFolder = Environment.GetFolderPath(Environment.SpecialFolder.Windows);

                var path = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)) + "\\ThirdPartyPrograms\\" + "osk.exe";

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
            foreach (var process in this.processes)
            {
                if (!process.HasExited)
                    process.Kill();
            }

            this.processes.Clear();
        }
    }
}