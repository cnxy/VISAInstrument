using System;
using System.Threading;
using System.Windows.Forms;
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
            Application.Run(new FrmMain());
        }
    }
}
