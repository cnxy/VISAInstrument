using System;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using VISAInstrument.Properties;

namespace VISAInstrument
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Mutex mutex = new Mutex(false, Resources.MutexName, out bool result);
            if(!result)
            {
                MessageBox.Show(Resources.Running);
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Application.Run(new FrmMain());
        }


        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            AppDomain appDomain = sender as AppDomain;
            Exception ex = (Exception)e.ExceptionObject;
            if (ex is DllNotFoundException)
            {
                DialogResult result = MessageBox.Show($"{Resources.VISA32Error}\r\n\r{Resources.VISADownLoad}", Resources.RuntimeError, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                {
                    Process.Start(Resources.VISA32URL);
                }
            }
            else
            {
                MessageBox.Show(ex.Message);
            }

            Environment.Exit(1);
        }
    }
}
