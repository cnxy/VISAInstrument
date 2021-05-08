using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using System.Linq;
using Ivi.Visa.ConflictManager;
using VISAInstrument.Properties;
using VISAInstrument.Utility;

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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Mutex mutex = new Mutex(false, Resources.MutexName, out bool result);
            if(!result)
            {
                MessageBox.Show(Resources.Running);
                return;
            }
            if (!IsVisaExisted(out string message, out string[] visaSharedComponent, out string[] niVisaRuntime))
            {
                if (MessageBox.Show($"{message}\r\n\r{Resources.NeedToDownLoad}", @"错误",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) != DialogResult.Yes) return;
                try
                {
                    Process.Start(Resources.VISA32URL);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return;
            }
            Common.VisaSharedComponent = visaSharedComponent;
            Common.NiVisaRuntime = niVisaRuntime;
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

        private static bool IsVisaExisted(out string message, out string[] visaSharedComponent, out string[] niVisaRuntime)
        {
            const string visaSharedComponentsString = "VISA Shared Components";
            const string niVisaRuntimeString = "NI-VISA .NET Runtime";
            const string visaSharedComponentsVersion = "5.7.0.0";
            const string niVisaRuntimeVersion = "5.5.0.0";
            const string noneVersion = "0.0.0.0";
            UninstallInfo[] visaUninstallInfo = UninstallInfoHelper.GetUninstallInfo().Where(x => !string.IsNullOrEmpty(x.DisplayName)).ToArray();
            UninstallInfo[] iviVisaUninstallInfo = visaUninstallInfo.Where(x => x.DisplayName.StartsWith(visaSharedComponentsString)).ToArray();
            visaSharedComponent = iviVisaUninstallInfo.Select(x => x.ToString()).ToArray();
            niVisaRuntime = new string[0];
            string GetFullVersion(string version)
            {
                string newVersion = version;
                try
                {
                    Version versionTemp = new Version(version);
                    if (versionTemp.Build == -1)
                    {
                        newVersion += ".0";
                    }
                    if (versionTemp.Revision == -1)
                    {
                        newVersion += ".0";
                    }
                    return newVersion;
                }
                catch
                {
                    return noneVersion;
                }
            }
            message = string.Empty;
            if (!iviVisaUninstallInfo.Any(x =>
                !string.IsNullOrEmpty(x.DisplayVersion) &&
                new Version(GetFullVersion(x.DisplayVersion)) >= new Version(visaSharedComponentsVersion)))
            {
                string versions = string.Empty;
                if (iviVisaUninstallInfo.Any())
                {
                    versions = iviVisaUninstallInfo.Where(x => !string.IsNullOrEmpty(x.DisplayVersion)).Aggregate("", (m, n) => $"{m}/{n.DisplayVersion}").Trim('/');
                }
                message = !string.IsNullOrEmpty(versions) ? $"{visaSharedComponentsString}目前版本为{versions}，小于其最小版本({visaSharedComponentsVersion})" : $"必须安装{visaSharedComponentsString}，且版本必须大于最小版本({visaSharedComponentsVersion})";
                return false;
            }

            ConflictManager manager = new ConflictManager();
            try
            {
                List<VisaImplementation> results = manager.GetInstalledVisas(ApiType.DotNet);
                if (results.Count <= 0)
                {
                    message = "系统没有安装.NET版本的VISA";
                    return false;
                }

                VisaImplementation implementation =
                    results.FirstOrDefault(x => x.ResourceManufacturerId == 4086);
                if (implementation == null)
                {
                    message = $"系统没有安装{niVisaRuntimeString}";
                    return false;
                }

                UninstallInfo[] niVisaUninstallInfo = visaUninstallInfo.Where(x => x.DisplayName.StartsWith(niVisaRuntimeString)).ToArray();
                niVisaRuntime = niVisaUninstallInfo.Select(x => x.ToString()).ToArray();
                if (!niVisaUninstallInfo.Any(x =>
                    !string.IsNullOrEmpty(x.DisplayVersion) &&
                    new Version(GetFullVersion(x.DisplayVersion)) >= new Version(niVisaRuntimeVersion)))
                {
                    string versions = string.Empty;
                    if (niVisaUninstallInfo.Any())
                    {
                        versions = niVisaUninstallInfo.Where(x => !string.IsNullOrEmpty(x.DisplayVersion)).Aggregate("", (m, n) => $"{m}/{n.DisplayVersion}").Trim('/');
                    }

                    message = !string.IsNullOrEmpty(versions) ? $"{niVisaRuntimeString}目前版本为{versions}，小于其最小版本({niVisaRuntimeVersion})" : $"必须安装{niVisaRuntimeString}，且版本必须大于最小版本({niVisaRuntimeVersion})";
                    return false;
                }
                if (!implementation.Enabled) manager.SetPreferredVisa(implementation);
                return true;
            }
            catch (Exception e)
            {
                message = e.Message;
                return false;
            }
        }

        private  static bool IsVisaExisted(out string message)
        {
            return IsVisaExisted(out message, out _, out _);
        }
    }
}
