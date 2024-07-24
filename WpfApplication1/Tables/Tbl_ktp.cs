using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace WpfApplication1.Tables
{
  [Table(Name = "tbl_ktp")]
  public class Tbl_ktp : INotifyPropertyChanging, INotifyPropertyChanged
  {
    private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(string.Empty);
    private long _ID;
    private string _Nik;
    private string _Nama;
    private string _Tempatlhr;
    private DateTime? _Tgllahir;
    private string _Jnskelamin;
    private string _Goldarah;
    private string _Alamat;
    private string _Rtrw;
    private string _Kel;
    private string _Kec;
    private string _Agama;
    private string _Statuskawin;
    private string _Pekerjaan;
    private string _Kewarganegaraan;
    private DateTime? _Berlaku;
    private string _Provinsi;
    private string _Foto;
    private string _Tandatangan;
    private DateTime? _Tglinput;
    private string _Email;
    private int _Id_broker;
    private EntityRef<Tbl_perusahaan_efek> _Tbl_perusahaan_efek;

    public Tbl_ktp() => this._Tbl_perusahaan_efek = new EntityRef<Tbl_perusahaan_efek>();

    [Column(Storage = "_ID", AutoSync = AutoSync.OnInsert, DbType = "BigInt NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
    public long ID
    {
      get => this._ID;
      set
      {
        if (this._ID == value)
          return;
        this.SendPropertyChanging();
        this._ID = value;
        this.SendPropertyChanged(nameof (ID));
      }
    }

    [Column(Name = "nik", Storage = "_Nik", DbType = "NVarChar(50)")]
    public string Nik
    {
      get => this._Nik;
      set
      {
        if (!(this._Nik != value))
          return;
        this.SendPropertyChanging();
        this._Nik = value;
        this.SendPropertyChanged(nameof (Nik));
      }
    }

    [Column(Name = "nama", Storage = "_Nama", DbType = "NVarChar(200)")]
    public string Nama
    {
      get => this._Nama;
      set
      {
        if (!(this._Nama != value))
          return;
        this.SendPropertyChanging();
        this._Nama = value;
        this.SendPropertyChanged(nameof (Nama));
      }
    }

    [Column(Name = "tempatlhr", Storage = "_Tempatlhr", DbType = "NVarChar(100)")]
    public string Tempatlhr
    {
      get => this._Tempatlhr;
      set
      {
        if (!(this._Tempatlhr != value))
          return;
        this.SendPropertyChanging();
        this._Tempatlhr = value;
        this.SendPropertyChanged(nameof (Tempatlhr));
      }
    }

    [Column(Name = "tgllahir", Storage = "_Tgllahir", DbType = "DateTime")]
    public DateTime? Tgllahir
    {
      get => this._Tgllahir;
      set
      {
        DateTime? tgllahir = this._Tgllahir;
        DateTime? nullable = value;
        if (tgllahir.HasValue == nullable.HasValue && (!tgllahir.HasValue || !(tgllahir.GetValueOrDefault() != nullable.GetValueOrDefault())))
          return;
        this.SendPropertyChanging();
        this._Tgllahir = value;
        this.SendPropertyChanged(nameof (Tgllahir));
      }
    }

    [Column(Name = "jnskelamin", Storage = "_Jnskelamin", DbType = "NVarChar(50)")]
    public string Jnskelamin
    {
      get => this._Jnskelamin;
      set
      {
        if (!(this._Jnskelamin != value))
          return;
        this.SendPropertyChanging();
        this._Jnskelamin = value;
        this.SendPropertyChanged(nameof (Jnskelamin));
      }
    }

    [Column(Name = "goldarah", Storage = "_Goldarah", DbType = "NChar(10)")]
    public string Goldarah
    {
      get => this._Goldarah;
      set
      {
        if (!(this._Goldarah != value))
          return;
        this.SendPropertyChanging();
        this._Goldarah = value;
        this.SendPropertyChanged(nameof (Goldarah));
      }
    }

    [Column(Name = "alamat", Storage = "_Alamat", DbType = "NVarChar(200)")]
    public string Alamat
    {
      get => this._Alamat;
      set
      {
        if (!(this._Alamat != value))
          return;
        this.SendPropertyChanging();
        this._Alamat = value;
        this.SendPropertyChanged(nameof (Alamat));
      }
    }

    [Column(Name = "rtrw", Storage = "_Rtrw", DbType = "NChar(10)")]
    public string Rtrw
    {
      get => this._Rtrw;
      set
      {
        if (!(this._Rtrw != value))
          return;
        this.SendPropertyChanging();
        this._Rtrw = value;
        this.SendPropertyChanged(nameof (Rtrw));
      }
    }

    [Column(Name = "kel", Storage = "_Kel", DbType = "NVarChar(50)")]
    public string Kel
    {
      get => this._Kel;
      set
      {
        if (!(this._Kel != value))
          return;
        this.SendPropertyChanging();
        this._Kel = value;
        this.SendPropertyChanged(nameof (Kel));
      }
    }

    [Column(Name = "kec", Storage = "_Kec", DbType = "NVarChar(50)")]
    public string Kec
    {
      get => this._Kec;
      set
      {
        if (!(this._Kec != value))
          return;
        this.SendPropertyChanging();
        this._Kec = value;
        this.SendPropertyChanged(nameof (Kec));
      }
    }

    [Column(Name = "agama", Storage = "_Agama", DbType = "NVarChar(50)")]
    public string Agama
    {
      get => this._Agama;
      set
      {
        if (!(this._Agama != value))
          return;
        this.SendPropertyChanging();
        this._Agama = value;
        this.SendPropertyChanged(nameof (Agama));
      }
    }

    [Column(Name = "statuskawin", Storage = "_Statuskawin", DbType = "NVarChar(50)")]
    public string Statuskawin
    {
      get => this._Statuskawin;
      set
      {
        if (!(this._Statuskawin != value))
          return;
        this.SendPropertyChanging();
        this._Statuskawin = value;
        this.SendPropertyChanged(nameof (Statuskawin));
      }
    }

    [Column(Name = "pekerjaan", Storage = "_Pekerjaan", DbType = "NVarChar(100)")]
    public string Pekerjaan
    {
      get => this._Pekerjaan;
      set
      {
        if (!(this._Pekerjaan != value))
          return;
        this.SendPropertyChanging();
        this._Pekerjaan = value;
        this.SendPropertyChanged(nameof (Pekerjaan));
      }
    }

    [Column(Name = "kewarganegaraan", Storage = "_Kewarganegaraan", DbType = "NVarChar(50)")]
    public string Kewarganegaraan
    {
      get => this._Kewarganegaraan;
      set
      {
        if (!(this._Kewarganegaraan != value))
          return;
        this.SendPropertyChanging();
        this._Kewarganegaraan = value;
        this.SendPropertyChanged(nameof (Kewarganegaraan));
      }
    }

    [Column(Name = "berlaku", Storage = "_Berlaku", DbType = "DateTime")]
    public DateTime? Berlaku
    {
      get => this._Berlaku;
      set
      {
        DateTime? berlaku = this._Berlaku;
        DateTime? nullable = value;
        if (berlaku.HasValue == nullable.HasValue && (!berlaku.HasValue || !(berlaku.GetValueOrDefault() != nullable.GetValueOrDefault())))
          return;
        this.SendPropertyChanging();
        this._Berlaku = value;
        this.SendPropertyChanged(nameof (Berlaku));
      }
    }

    [Column(Name = "provinsi", Storage = "_Provinsi", DbType = "NVarChar(50)")]
    public string Provinsi
    {
      get => this._Provinsi;
      set
      {
        if (!(this._Provinsi != value))
          return;
        this.SendPropertyChanging();
        this._Provinsi = value;
        this.SendPropertyChanged(nameof (Provinsi));
      }
    }

    [Column(Name = "foto", Storage = "_Foto", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
    public string Foto
    {
      get => this._Foto;
      set
      {
        if (!(this._Foto != value))
          return;
        this.SendPropertyChanging();
        this._Foto = value;
        this.SendPropertyChanged(nameof (Foto));
      }
    }

    [Column(Name = "tandatangan", Storage = "_Tandatangan", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
    public string Tandatangan
    {
      get => this._Tandatangan;
      set
      {
        if (!(this._Tandatangan != value))
          return;
        this.SendPropertyChanging();
        this._Tandatangan = value;
        this.SendPropertyChanged(nameof (Tandatangan));
      }
    }

    [Column(Name = "tglinput", Storage = "_Tglinput", DbType = "DateTime")]
    public DateTime? Tglinput
    {
      get => this._Tglinput;
      set
      {
        DateTime? tglinput = this._Tglinput;
        DateTime? nullable = value;
        if (tglinput.HasValue == nullable.HasValue && (!tglinput.HasValue || !(tglinput.GetValueOrDefault() != nullable.GetValueOrDefault())))
          return;
        this.SendPropertyChanging();
        this._Tglinput = value;
        this.SendPropertyChanged(nameof (Tglinput));
      }
    }
    
    [Column(Name = "email", Storage = "_Email", DbType = "NVarChar(50)")]
    public string Email
    {
      get => this._Email;
      set
      {
        if (!(this._Email != value))
          return;
        this.SendPropertyChanging();
        this._Email = value;
        this.SendPropertyChanged(nameof (Email));
      }
    }
    
    [Column(Name = "id_broker", Storage = "_Id_broker", DbType = "BigInt")]
    public int Id_broker
    {
      get => this._Id_broker;
    set
    {
        if (this._Id_broker != value)
        {
            this.SendPropertyChanging();
            this._Id_broker = value;
            this.SendPropertyChanged(nameof(Id_broker));
        }
    }
    }
    
    [Association(Name = "FK_tbl_ktp_tbl_perusahaan_efek", Storage = "_Tbl_perusahaan_efek", ThisKey = "Id_broker", OtherKey = "Id_broker", IsForeignKey = true)]
    public Tbl_perusahaan_efek Tbl_perusahaan_efek
    {
      get => this._Tbl_perusahaan_efek.Entity;
      set
      {
        Tbl_perusahaan_efek entity = this._Tbl_perusahaan_efek.Entity;
        if (entity == value && this._Tbl_perusahaan_efek.HasLoadedOrAssignedValue)
          return;
        this.SendPropertyChanging();
        if (entity != null)
        {
          this._Tbl_perusahaan_efek.Entity = (Tbl_perusahaan_efek) null;
          entity.Tbl_ktp.Remove(this);
        }
        this._Tbl_perusahaan_efek.Entity = value;
        if (value != null)
        {
          value.Tbl_ktp.Add(this);
          this._Id_broker = value.Id_broker;
        }
        else
          this._Id_broker = 0;
        this.SendPropertyChanged(nameof (Tbl_role));
      }
    }

    public event PropertyChangingEventHandler PropertyChanging;

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void SendPropertyChanging()
    {
      if (this.PropertyChanging == null)
        return;
      this.PropertyChanging((object) this, Tbl_ktp.emptyChangingEventArgs);
    }

    protected virtual void SendPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
