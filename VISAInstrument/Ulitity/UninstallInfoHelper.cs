using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Win32;

namespace VISAInstrument.Utility
{
    public class UninstallInfoHelper
    {

        public static UninstallInfo[] GetUninstallInfo(UninstallInfoOption uninstallInfoOption = UninstallInfoOption.Only32Bit)
        {
            const string registryPathOf32Bit = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            const string registryPathOf64Bit = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall";
            string[] registryPaths = uninstallInfoOption == UninstallInfoOption.Only32Bit ? new[] { registryPathOf32Bit }
                : uninstallInfoOption == UninstallInfoOption.Only64Bit ? new[] { registryPathOf64Bit }
                : new[] { registryPathOf32Bit, registryPathOf64Bit };
            List<UninstallInfo> list = new List<UninstallInfo>();
            foreach (string registryPath in registryPaths)
            {
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(registryPath);
                if (registryKey == null) continue;
                foreach (string subKeyName in registryKey.GetSubKeyNames())
                {
                    UninstallInfo uninstallInfo = null;
                    PropertyInfo[] propertyNames = typeof(UninstallInfo).GetProperties();
                    PropertyInfo is64BitPropertyInfo = propertyNames.First(x => x.Name == nameof(UninstallInfo.Is64Bit));
                    PropertyInfo[] newPropertyNames = propertyNames.Where(x => x != is64BitPropertyInfo).ToArray();
                    RegistryKey currentKey = registryKey.OpenSubKey(subKeyName);
                    if (currentKey == null) continue;
                    foreach (string valueName in currentKey.GetValueNames())
                    {
                        PropertyInfo propertyInfo = newPropertyNames.FirstOrDefault(x => x.Name == valueName);
                        if (propertyInfo == null) continue;
                        object value = currentKey.GetValue(valueName);
                        if (value == null || value is string valueString && string.IsNullOrEmpty(valueString)) continue;
                        if (uninstallInfo == null)
                        {
                            uninstallInfo = new UninstallInfo();
                            is64BitPropertyInfo.SetValue(uninstallInfo, registryPaths.First() != registryPath, null);
                        }
                        propertyInfo.SetValue(uninstallInfo, value.ToString(), null);
                    }

                    if (uninstallInfo != null && !string.IsNullOrEmpty(uninstallInfo.DisplayName))
                    {
                        list.Add(uninstallInfo);
                    }
                }
            }
            return list.Distinct(new UninstallInfoEqualityComparer()).ToArray();
        }

        class UninstallInfoEqualityComparer : IEqualityComparer<UninstallInfo>
        {
            public bool Equals(UninstallInfo x, UninstallInfo y)
            {
                if (x == null && y == null) return true;
                if (x == null || y == null) return false;

                if (string.IsNullOrEmpty(x.DisplayName) && string.IsNullOrEmpty(y.DisplayName)
                && string.IsNullOrEmpty(x.DisplayVersion) && string.IsNullOrEmpty(y.DisplayVersion))
                {
                    return true;
                }
                if (string.IsNullOrEmpty(x.DisplayName) || string.IsNullOrEmpty(y.DisplayName)
                                                        || string.IsNullOrEmpty(x.DisplayVersion) || string.IsNullOrEmpty(y.DisplayVersion))
                {
                    return false;
                }
                return x.Is64Bit.Equals(y.Is64Bit) && x.DisplayName.Equals(y.DisplayName) && x.DisplayVersion.Equals(y.DisplayVersion);
            }

            public int GetHashCode(UninstallInfo obj)
            {
                if (string.IsNullOrEmpty(obj.DisplayName) && string.IsNullOrEmpty(obj.DisplayVersion))
                {
                    return 0;
                }
                if (string.IsNullOrEmpty(obj.DisplayName))
                {
                    return obj.DisplayVersion.GetHashCode();
                }
                if (string.IsNullOrEmpty(obj.DisplayVersion))
                {
                    return obj.DisplayName.GetHashCode();
                }

                return obj.DisplayName.GetHashCode() ^ obj.DisplayVersion.GetHashCode() + 1623;
            }
        }

    }

    public class UninstallInfo
    {
        public bool Is64Bit { get; private set; }

        public string DisplayName { get; private set; }
        public string QuietUninstallString { get; private set; }
        public string DisplayVersion { get; private set; }
        public string Publisher { get; private set; }
        public string NoModify { get; private set; }
        public string NoRepair { get; private set; }
        public string EstimatedSize { get; private set; }
        public string DisplayIcon { get; private set; }
        public string SystemComponent { get; private set; }
        public string InstallLocation { get; private set; }
        public string URLInfoAbout { get; private set; }
        public string URLUpdateInfo { get; private set; }
        public string InstallDate { get; private set; }
        public string InstallSource { get; private set; }
        public string Language { get; private set; }
        public string WindowsInstaller { get; private set; }
        public string NoRemove { get; private set; }
        public string VersionMinor { get; private set; }
        public string VersionMajor { get; private set; }
        public string UninstallPath { get; private set; }
        public string ModifyPath { get; private set; }
        public string HelpTelephone { get; private set; }
        public string ClickToRunComponent { get; private set; }
        public string AuthorizedCDFPrefix { get; private set; }
        public string Comments { get; private set; }
        public string Contact { get; private set; }
        public string UninstallString { get; private set; }
        public string HelpLink { get; private set; }
        public string Readme { get; private set; }
        public string SettingsIdentifier { get; private set; }
        public string PartAssist { get; private set; }
        public string RepairPath { get; private set; }
        public string Version { get; private set; }
        public string EngineVersion { get; private set; }
        public string NoElevateOnModify { get; private set; }
        public string Resume { get; private set; }
        public string Installed { get; private set; }
        public string LogFile { get; private set; }
        public string ProductGuid { get; private set; }
        public string RegOwner { get; private set; }
        public string LogMode { get; private set; }
        public string Size { get; private set; }
        public string UninstallData { get; private set; }
        public string UninstallDataFile { get; private set; }
        public string ReleaseType { get; private set; }
        public string ParentDisplayName { get; private set; }
        public string ParentKeyName { get; private set; }
        public string RegistryLocation { get; private set; }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(DisplayName) && !string.IsNullOrEmpty(DisplayVersion)) return DisplayName + "," + DisplayVersion;
            return base.ToString();
        }
    }

    public enum UninstallInfoOption
    {
        Only32Bit,
        Only64Bit,
        Both
    }
}
