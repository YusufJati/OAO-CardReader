namespace KSEIApp.Properties
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Configuration;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    [CompilerGenerated, GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    internal sealed class Settings : ApplicationSettingsBase
    {
        private static Settings defaultInstance = ((Settings) Synchronized(new Settings()));

        private void SettingChangingEventHandler(object sender, SettingChangingEventArgs e)
        {
        }

        private void SettingsSavingEventHandler(object sender, CancelEventArgs e)
        {
        }

        public static Settings Default =>
            defaultInstance;

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("True")]
        public bool excel
        {
            get => 
                (bool) this["excel"];
            set => 
                this["excel"] = value;
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("True")]
        public bool pdf
        {
            get => 
                (bool) this["pdf"];
            set => 
                this["pdf"] = value;
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("True")]
        public bool txt
        {
            get => 
                (bool) this["txt"];
            set => 
                this["txt"] = value;
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue(@"C:\Export")]
        public string singleexportpath
        {
            get => 
                (string) this["singleexportpath"];
            set => 
                this["singleexportpath"] = value;
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("False")]
        public bool xml
        {
            get => 
                (bool) this["xml"];
            set => 
                this["xml"] = value;
        }

        [ApplicationScopedSetting, DebuggerNonUserCode, SpecialSetting(SpecialSetting.ConnectionString), DefaultSettingValue("Data Source=DBKTP.sdf;")]
        public string DBKTPLocalConn =>
            (string) this["DBKTPLocalConn"];

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue(@"C:\Export")]
        public string serviceexportpath
        {
            get => 
                (string) this["serviceexportpath"];
            set => 
                this["serviceexportpath"] = value;
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("")]
        public string useridservice
        {
            get => 
                (string) this["useridservice"];
            set => 
                this["useridservice"] = value;
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("")]
        public string pwdservice
        {
            get => 
                (string) this["pwdservice"];
            set => 
                this["pwdservice"] = value;
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("COM9")]
        public string port
        {
            get => 
                (string) this["port"];
            set => 
                this["port"] = value;
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("")]
        public string ipaddressservice
        {
            get => 
                (string) this["ipaddressservice"];
            set => 
                this["ipaddressservice"] = value;
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("False")]
        public bool webservice
        {
            get => 
                (bool) this["webservice"];
            set => 
                this["webservice"] = value;
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("True")]
        public bool excel2013
        {
            get => 
                (bool) this["excel2013"];
            set => 
                this["excel2013"] = value;
        }
    }
}

