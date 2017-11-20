using System;
using System.Threading;
using System.Windows.Forms;

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
            Mutex mutex = new Mutex(false, "VISAInstrument", out bool result);
            if(!result)
            {
                MessageBox.Show("程序已经在运行！");
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }
    }
}
