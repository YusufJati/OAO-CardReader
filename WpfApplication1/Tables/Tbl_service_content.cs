using System.ComponentModel;
using System.Data.Linq.Mapping;

namespace WpfApplication1.Tables
{
    [Table(Name = "tbl_service_content")]
    public class Tbl_service_content : INotifyPropertyChanging, INotifyPropertyChanged
    {
      private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(string.Empty);
      private string _NIK;
      private string _EKTP_STATUS;
      private string _DUSUN;
      private string _NAMA_LGKP;
      private string _AGAMA;
      private string _EKTP_CREATED;
      private string _JENIS_PKRJN;
      private string _PDDK_AKH;
      private string _TMPT_LHR;
      private string _STATUS_KAWIN;
      private string _GOL_DARAH;
      private string _JENIS_KLMIN;
      private string _NO_KK;
      private string _KAB_NAME;
      private string _NAMA_LGKP_AYAH;
      private string _NO_RW;
      private string _KEC_NAME;
      private string _NO_AKTA_LHR;
      private string _NO_KEL;
      private string _NO_RT;
      private string _KODE_POS;
      private string _NO_KEC;
      private string _ALAMAT;
      private string _NO_PROP;
      private string _NAMA_LGKP_IBU;
      private string _PNYDNG_CCT;
      private string _PROP_NAME;
      private string _NO_KAB;
      private string _TGL_LHR;
      private string _KEL_NAME;

      [Column(Storage = "_NIK", DbType = "NVarChar(50) NOT NULL", CanBeNull = false, IsPrimaryKey = true)]
      public string NIK
      {
        get => this._NIK;
        set
        {
          if (!(this._NIK != value))
            return;
          this.SendPropertyChanging();
          this._NIK = value;
          this.SendPropertyChanged(nameof (NIK));
        }
      }

      [Column(Storage = "_EKTP_STATUS", DbType = "NVarChar(50)")]
      public string EKTP_STATUS
      {
        get => this._EKTP_STATUS;
        set
        {
          if (!(this._EKTP_STATUS != value))
            return;
          this.SendPropertyChanging();
          this._EKTP_STATUS = value;
          this.SendPropertyChanged(nameof (EKTP_STATUS));
        }
      }

      [Column(Storage = "_DUSUN", DbType = "NVarChar(50)")]
      public string DUSUN
      {
        get => this._DUSUN;
        set
        {
          if (!(this._DUSUN != value))
            return;
          this.SendPropertyChanging();
          this._DUSUN = value;
          this.SendPropertyChanged(nameof (DUSUN));
        }
      }

      [Column(Storage = "_NAMA_LGKP", DbType = "NVarChar(100)")]
      public string NAMA_LGKP
      {
        get => this._NAMA_LGKP;
        set
        {
          if (!(this._NAMA_LGKP != value))
            return;
          this.SendPropertyChanging();
          this._NAMA_LGKP = value;
          this.SendPropertyChanged(nameof (NAMA_LGKP));
        }
      }

      [Column(Storage = "_AGAMA", DbType = "NVarChar(50)")]
      public string AGAMA
      {
        get => this._AGAMA;
        set
        {
          if (!(this._AGAMA != value))
            return;
          this.SendPropertyChanging();
          this._AGAMA = value;
          this.SendPropertyChanged(nameof (AGAMA));
        }
      }

      [Column(Storage = "_EKTP_CREATED", DbType = "NVarChar(50)")]
      public string EKTP_CREATED
      {
        get => this._EKTP_CREATED;
        set
        {
          if (!(this._EKTP_CREATED != value))
            return;
          this.SendPropertyChanging();
          this._EKTP_CREATED = value;
          this.SendPropertyChanged(nameof (EKTP_CREATED));
        }
      }

      [Column(Storage = "_JENIS_PKRJN", DbType = "NVarChar(50)")]
      public string JENIS_PKRJN
      {
        get => this._JENIS_PKRJN;
        set
        {
          if (!(this._JENIS_PKRJN != value))
            return;
          this.SendPropertyChanging();
          this._JENIS_PKRJN = value;
          this.SendPropertyChanged(nameof (JENIS_PKRJN));
        }
      }

      [Column(Storage = "_PDDK_AKH", DbType = "NVarChar(50)")]
      public string PDDK_AKH
      {
        get => this._PDDK_AKH;
        set
        {
          if (!(this._PDDK_AKH != value))
            return;
          this.SendPropertyChanging();
          this._PDDK_AKH = value;
          this.SendPropertyChanged(nameof (PDDK_AKH));
        }
      }

      [Column(Storage = "_TMPT_LHR", DbType = "NVarChar(50)")]
      public string TMPT_LHR
      {
        get => this._TMPT_LHR;
        set
        {
          if (!(this._TMPT_LHR != value))
            return;
          this.SendPropertyChanging();
          this._TMPT_LHR = value;
          this.SendPropertyChanged(nameof (TMPT_LHR));
        }
      }

      [Column(Storage = "_STATUS_KAWIN", DbType = "NVarChar(50)")]
      public string STATUS_KAWIN
      {
        get => this._STATUS_KAWIN;
        set
        {
          if (!(this._STATUS_KAWIN != value))
            return;
          this.SendPropertyChanging();
          this._STATUS_KAWIN = value;
          this.SendPropertyChanged(nameof (STATUS_KAWIN));
        }
      }

      [Column(Storage = "_GOL_DARAH", DbType = "NChar(10)")]
      public string GOL_DARAH
      {
        get => this._GOL_DARAH;
        set
        {
          if (!(this._GOL_DARAH != value))
            return;
          this.SendPropertyChanging();
          this._GOL_DARAH = value;
          this.SendPropertyChanged(nameof (GOL_DARAH));
        }
      }

      [Column(Storage = "_JENIS_KLMIN", DbType = "NVarChar(50)")]
      public string JENIS_KLMIN
      {
        get => this._JENIS_KLMIN;
        set
        {
          if (!(this._JENIS_KLMIN != value))
            return;
          this.SendPropertyChanging();
          this._JENIS_KLMIN = value;
          this.SendPropertyChanged(nameof (JENIS_KLMIN));
        }
      }

      [Column(Storage = "_NO_KK", DbType = "NVarChar(50)")]
      public string NO_KK
      {
        get => this._NO_KK;
        set
        {
          if (!(this._NO_KK != value))
            return;
          this.SendPropertyChanging();
          this._NO_KK = value;
          this.SendPropertyChanged(nameof (NO_KK));
        }
      }

      [Column(Storage = "_KAB_NAME", DbType = "NVarChar(50)")]
      public string KAB_NAME
      {
        get => this._KAB_NAME;
        set
        {
          if (!(this._KAB_NAME != value))
            return;
          this.SendPropertyChanging();
          this._KAB_NAME = value;
          this.SendPropertyChanged(nameof (KAB_NAME));
        }
      }

      [Column(Storage = "_NAMA_LGKP_AYAH", DbType = "NVarChar(100)")]
      public string NAMA_LGKP_AYAH
      {
        get => this._NAMA_LGKP_AYAH;
        set
        {
          if (!(this._NAMA_LGKP_AYAH != value))
            return;
          this.SendPropertyChanging();
          this._NAMA_LGKP_AYAH = value;
          this.SendPropertyChanged(nameof (NAMA_LGKP_AYAH));
        }
      }

      [Column(Storage = "_NO_RW", DbType = "NChar(10)")]
      public string NO_RW
      {
        get => this._NO_RW;
        set
        {
          if (!(this._NO_RW != value))
            return;
          this.SendPropertyChanging();
          this._NO_RW = value;
          this.SendPropertyChanged(nameof (NO_RW));
        }
      }

      [Column(Storage = "_KEC_NAME", DbType = "NVarChar(50)")]
      public string KEC_NAME
      {
        get => this._KEC_NAME;
        set
        {
          if (!(this._KEC_NAME != value))
            return;
          this.SendPropertyChanging();
          this._KEC_NAME = value;
          this.SendPropertyChanged(nameof (KEC_NAME));
        }
      }

      [Column(Storage = "_NO_AKTA_LHR", DbType = "NVarChar(50)")]
      public string NO_AKTA_LHR
      {
        get => this._NO_AKTA_LHR;
        set
        {
          if (!(this._NO_AKTA_LHR != value))
            return;
          this.SendPropertyChanging();
          this._NO_AKTA_LHR = value;
          this.SendPropertyChanged(nameof (NO_AKTA_LHR));
        }
      }

      [Column(Storage = "_NO_KEL", DbType = "NChar(10)")]
      public string NO_KEL
      {
        get => this._NO_KEL;
        set
        {
          if (!(this._NO_KEL != value))
            return;
          this.SendPropertyChanging();
          this._NO_KEL = value;
          this.SendPropertyChanged(nameof (NO_KEL));
        }
      }

      [Column(Storage = "_NO_RT", DbType = "NChar(10)")]
      public string NO_RT
      {
        get => this._NO_RT;
        set
        {
          if (!(this._NO_RT != value))
            return;
          this.SendPropertyChanging();
          this._NO_RT = value;
          this.SendPropertyChanged(nameof (NO_RT));
        }
      }

      [Column(Storage = "_KODE_POS", DbType = "NChar(10)")]
      public string KODE_POS
      {
        get => this._KODE_POS;
        set
        {
          if (!(this._KODE_POS != value))
            return;
          this.SendPropertyChanging();
          this._KODE_POS = value;
          this.SendPropertyChanged(nameof (KODE_POS));
        }
      }

      [Column(Storage = "_NO_KEC", DbType = "NChar(10)")]
      public string NO_KEC
      {
        get => this._NO_KEC;
        set
        {
          if (!(this._NO_KEC != value))
            return;
          this.SendPropertyChanging();
          this._NO_KEC = value;
          this.SendPropertyChanged(nameof (NO_KEC));
        }
      }

      [Column(Storage = "_ALAMAT", DbType = "NVarChar(200)")]
      public string ALAMAT
      {
        get => this._ALAMAT;
        set
        {
          if (!(this._ALAMAT != value))
            return;
          this.SendPropertyChanging();
          this._ALAMAT = value;
          this.SendPropertyChanged(nameof (ALAMAT));
        }
      }

      [Column(Storage = "_NO_PROP", DbType = "NChar(10)")]
      public string NO_PROP
      {
        get => this._NO_PROP;
        set
        {
          if (!(this._NO_PROP != value))
            return;
          this.SendPropertyChanging();
          this._NO_PROP = value;
          this.SendPropertyChanged(nameof (NO_PROP));
        }
      }

      [Column(Storage = "_NAMA_LGKP_IBU", DbType = "NVarChar(100)")]
      public string NAMA_LGKP_IBU
      {
        get => this._NAMA_LGKP_IBU;
        set
        {
          if (!(this._NAMA_LGKP_IBU != value))
            return;
          this.SendPropertyChanging();
          this._NAMA_LGKP_IBU = value;
          this.SendPropertyChanged(nameof (NAMA_LGKP_IBU));
        }
      }

      [Column(Storage = "_PNYDNG_CCT", DbType = "NChar(10)")]
      public string PNYDNG_CCT
      {
        get => this._PNYDNG_CCT;
        set
        {
          if (!(this._PNYDNG_CCT != value))
            return;
          this.SendPropertyChanging();
          this._PNYDNG_CCT = value;
          this.SendPropertyChanged(nameof (PNYDNG_CCT));
        }
      }

      [Column(Storage = "_PROP_NAME", DbType = "NVarChar(50)")]
      public string PROP_NAME
      {
        get => this._PROP_NAME;
        set
        {
          if (!(this._PROP_NAME != value))
            return;
          this.SendPropertyChanging();
          this._PROP_NAME = value;
          this.SendPropertyChanged(nameof (PROP_NAME));
        }
      }

      [Column(Storage = "_NO_KAB", DbType = "NChar(10)")]
      public string NO_KAB
      {
        get => this._NO_KAB;
        set
        {
          if (!(this._NO_KAB != value))
            return;
          this.SendPropertyChanging();
          this._NO_KAB = value;
          this.SendPropertyChanged(nameof (NO_KAB));
        }
      }

      [Column(Storage = "_TGL_LHR", DbType = "NVarChar(50)")]
      public string TGL_LHR
      {
        get => this._TGL_LHR;
        set
        {
          if (!(this._TGL_LHR != value))
            return;
          this.SendPropertyChanging();
          this._TGL_LHR = value;
          this.SendPropertyChanged(nameof (TGL_LHR));
        }
      }

      [Column(Storage = "_KEL_NAME", DbType = "NVarChar(50)")]
      public string KEL_NAME
      {
        get => this._KEL_NAME;
        set
        {
          if (!(this._KEL_NAME != value))
            return;
          this.SendPropertyChanging();
          this._KEL_NAME = value;
          this.SendPropertyChanged(nameof (KEL_NAME));
        }
      }

      public event PropertyChangingEventHandler PropertyChanging;

      public event PropertyChangedEventHandler PropertyChanged;

      protected virtual void SendPropertyChanging()
      {
        if (this.PropertyChanging == null)
          return;
        this.PropertyChanging((object) this, Tbl_service_content.emptyChangingEventArgs);
      }

      protected virtual void SendPropertyChanged(string propertyName)
      {
        if (this.PropertyChanged == null)
          return;
        this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
      }
    }
}