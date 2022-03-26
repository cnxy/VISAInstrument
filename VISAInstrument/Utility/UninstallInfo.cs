namespace VISAInstrument.Utility
{
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
}
