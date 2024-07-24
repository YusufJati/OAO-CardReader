// Decompiled with JetBrains decompiler
// Type: KSEIApp.Tbl_login
// Assembly: KSEIApp_V1.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A39806AA-01AF-452E-B86C-A187933A1DF3
// Assembly location: D:\Magang\Support\KSEIApp_V1.1\KSEIApp_V1.1.exe

using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace WpfApplication1.Tables
{
  [Table(Name = "tbl_login")]
  public class Tbl_login : INotifyPropertyChanging, INotifyPropertyChanged
  {
    private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(string.Empty);
    private int _Id;
    private string _Username;
    private string _Password;
    private int? _Id_role;
    private string _Deskripsi;
    private EntityRef<Tbl_role> _Tbl_role;

    public Tbl_login() => this._Tbl_role = new EntityRef<Tbl_role>();

    [Column(Name = "id", Storage = "_Id", AutoSync = AutoSync.OnInsert, DbType = "BigInt NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
    public int Id
    {
      get => this._Id;
      set
      {
        if (this._Id == value)
          return;
        this.SendPropertyChanging();
        this._Id = value;
        this.SendPropertyChanged(nameof (Id));
      }
    }

    [Column(Name = "username", Storage = "_Username", DbType = "NVarChar(50)")]
    public string Username
    {
      get => this._Username;
      set
      {
        if (!(this._Username != value))
          return;
        this.SendPropertyChanging();
        this._Username = value;
        this.SendPropertyChanged(nameof (Username));
      }
    }

    [Column(Name = "password", Storage = "_Password", DbType = "NVarChar(50)")]
    public string Password
    {
      get => this._Password;
      set
      {
        if (!(this._Password != value))
          return;
        this.SendPropertyChanging();
        this._Password = value;
        this.SendPropertyChanged(nameof (Password));
      }
    }

    [Column(Name = "id_role", Storage = "_Id_role", DbType = "BigInt")]
    public int? Id_role
    {
      get => this._Id_role;
      set
      {
        int? idRole = this._Id_role;
        int? nullable = value;
        if (idRole.GetValueOrDefault() == nullable.GetValueOrDefault() && idRole.HasValue == nullable.HasValue)
          return;
        this.SendPropertyChanging();
        this._Id_role = value;
        this.SendPropertyChanged(nameof (Id_role));
      }
    }

    [Column(Name = "deskripsi", Storage = "_Deskripsi", DbType = "NVarChar(200)")]
    public string Deskripsi
    {
      get => this._Deskripsi;
      set
      {
        if (!(this._Deskripsi != value))
          return;
        this.SendPropertyChanging();
        this._Deskripsi = value;
        this.SendPropertyChanged(nameof (Deskripsi));
      }
    }

    [Association(Name = "FK_tbl_login_tbl_role", Storage = "_Tbl_role", ThisKey = "Id_role", OtherKey = "Id_role", IsForeignKey = true)]
    public Tbl_role Tbl_role
    {
      get => this._Tbl_role.Entity;
      set
      {
        Tbl_role entity = this._Tbl_role.Entity;
        if (entity == value && this._Tbl_role.HasLoadedOrAssignedValue)
          return;
        this.SendPropertyChanging();
        if (entity != null)
        {
          this._Tbl_role.Entity = (Tbl_role) null;
          entity.Tbl_login.Remove(this);
        }
        this._Tbl_role.Entity = value;
        if (value != null)
        {
          value.Tbl_login.Add(this);
          this._Id_role = new int?(value.Id_role);
        }
        else
          this._Id_role = new int?();
        this.SendPropertyChanged(nameof (Tbl_role));
      }
    }

    public event PropertyChangingEventHandler PropertyChanging;

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void SendPropertyChanging()
    {
      if (this.PropertyChanging == null)
        return;
      this.PropertyChanging((object) this, Tbl_login.emptyChangingEventArgs);
    }

    protected virtual void SendPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
