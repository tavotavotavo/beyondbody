using Processing.Processors;
using ProcessingInterfaces;
using Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeyondBody
{
    static class Program
    {
        private static IMainProcessor mainProcessor;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mainProcessor = new MainProcessor();
        }
    }
}
