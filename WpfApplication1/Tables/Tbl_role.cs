
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace WpfApplication1
{
  [Table(Name = "tbl_role")]
  public class Tbl_role : INotifyPropertyChanging, INotifyPropertyChanged
  {
    private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(string.Empty);
    private int _Id_role;
    private string _Role;
    private string _Ket;
    private EntitySet<WpfApplication1.Tables.Tbl_login> _Tbl_login;

    public Tbl_role()
    {
      this._Tbl_login = new EntitySet<WpfApplication1.Tables.Tbl_login>(new Action<WpfApplication1.Tables.Tbl_login>(this.attach_Tbl_login), new Action<WpfApplication1.Tables.Tbl_login>(this.detach_Tbl_login));
    }

    [Column(Name = "id_role", Storage = "_Id_role", AutoSync = AutoSync.OnInsert, DbType = "BigInt NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
    public int Id_role
    {
      get => this._Id_role;
      set
      {
        if (this._Id_role == value)
          return;
        this.SendPropertyChanging();
        this._Id_role = value;
        this.SendPropertyChanged(nameof (Id_role));
      }
    }

    [Column(Name = "role", Storage = "_Role", DbType = "NVarChar(50)")]
    public string Role
    {
      get => this._Role;
      set
      {
        if (!(this._Role != value))
          return;
        this.SendPropertyChanging();
        this._Role = value;
        this.SendPropertyChanged(nameof (Role));
      }
    }

    [Column(Name = "ket", Storage = "_Ket", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
    public string Ket
    {
      get => this._Ket;
      set
      {
        if (!(this._Ket != value))
          return;
        this.SendPropertyChanging();
        this._Ket = value;
        this.SendPropertyChanged(nameof (Ket));
      }
    }

    [Association(Name = "FK_tbl_login_tbl_role", Storage = "_Tbl_login", ThisKey = "Id_role", OtherKey = "Id_role", DeleteRule = "NO ACTION")]
    public EntitySet<WpfApplication1.Tables.Tbl_login> Tbl_login
    {
      get => this._Tbl_login;
      set => this._Tbl_login.Assign((IEnumerable<WpfApplication1.Tables.Tbl_login>) value);
    }

    public event PropertyChangingEventHandler PropertyChanging;

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void SendPropertyChanging()
    {
      if (this.PropertyChanging == null)
        return;
      this.PropertyChanging((object) this, Tbl_role.emptyChangingEventArgs);
    }

    protected virtual void SendPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }

    private void attach_Tbl_login(WpfApplication1.Tables.Tbl_login entity)
    {
      this.SendPropertyChanging();
      entity.Tbl_role = this;
    }

    private void detach_Tbl_login(WpfApplication1.Tables.Tbl_login entity)
    {
      this.SendPropertyChanging();
      entity.Tbl_role = (Tbl_role) null;
    }
  }
}
