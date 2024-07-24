/*using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace WpfApplication1.Utils
{
    internal sealed class Settings1: ApplicationSettingsBase
    {
      private static Settings defaultInstance = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());

      private void SettingChangingEventHandler(object sender, SettingChangingEventArgs e)
      {
      }

      private void SettingsSavingEventHandler(object sender, CancelEventArgs e)
      {
      }

      public static Settings Default
      {
        get
        {
          Settings defaultInstance = Settings.defaultInstance;
          return defaultInstance;
        }
      }

      [UserScopedSetting]
      [DebuggerNonUserCode]
      [DefaultSettingValue("True")]
      public bool excel
      {
        get => (bool) this[nameof (excel)];
        set => this[nameof (excel)] = (object) value;
      }

      [UserScopedSetting]
      [DebuggerNonUserCode]
      [DefaultSettingValue("True")]
      public bool pdf
      {
        get => (bool) this[nameof (pdf)];
        set => this[nameof (pdf)] = (object) value;
      }

      [UserScopedSetting]
      [DebuggerNonUserCode]
      [DefaultSettingValue("True")]
      public bool txt
      {
        get => (bool) this[nameof (txt)];
        set => this[nameof (txt)] = (object) value;
      }

      [UserScopedSetting]
      [DebuggerNonUserCode]
      [DefaultSettingValue("C:\\Export")]
      public string singleexportpath
      {
        get => (string) this[nameof (singleexportpath)];
        set => this[nameof (singleexportpath)] = (object) value;
      }

      [UserScopedSetting]
      [DebuggerNonUserCode]
      [DefaultSettingValue("False")]
      public bool xml
      {
        get => (bool) this[nameof (xml)];
        set => this[nameof (xml)] = (object) value;
      }

      [ApplicationScopedSetting]
      [DebuggerNonUserCode]
      [SpecialSetting(SpecialSetting.ConnectionString)]
      [DefaultSettingValue("Data Source=DBKTP.sdf;")]
      public string DBKTPLocalConn => (string) this[nameof (DBKTPLocalConn)];

      [UserScopedSetting]
      [DebuggerNonUserCode]
      [DefaultSettingValue("C:\\Export")]
      public string serviceexportpath
      {
        get => (string) this[nameof (serviceexportpath)];
        set => this[nameof (serviceexportpath)] = (object) value;
      }

      [UserScopedSetting]
      [DebuggerNonUserCode]
      [DefaultSettingValue("")]
      public string useridservice
      {
        get => (string) this[nameof (useridservice)];
        set => this[nameof (useridservice)] = (object) value;
      }

      [UserScopedSetting]
      [DebuggerNonUserCode]
      [DefaultSettingValue("")]
      public string pwdservice
      {
        get => (string) this[nameof (pwdservice)];
        set => this[nameof (pwdservice)] = (object) value;
      }

      [UserScopedSetting]
      [DebuggerNonUserCode]
      [DefaultSettingValue("COM9")]
      public string port
      {
        get => (string) this[nameof (port)];
        set => this[nameof (port)] = (object) value;
      }

      [UserScopedSetting]
      [DebuggerNonUserCode]
      [DefaultSettingValue("")]
      public string ipaddressservice
      {
        get => (string) this[nameof (ipaddressservice)];
        set => this[nameof (ipaddressservice)] = (object) value;
      }

      [UserScopedSetting]
      [DebuggerNonUserCode]
      [DefaultSettingValue("False")]
      public bool webservice
      {
        get => (bool) this[nameof (webservice)];
        set => this[nameof (webservice)] = (object) value;
      }

      [UserScopedSetting]
      [DebuggerNonUserCode]
      [DefaultSettingValue("True")]
      public bool excel2013
      {
        get => (bool) this[nameof (excel2013)];
        set => this[nameof (excel2013)] = (object) value;
      }
    }
}*/