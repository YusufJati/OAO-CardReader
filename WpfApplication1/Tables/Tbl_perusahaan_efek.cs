using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace WpfApplication1.Tables
{
    [Table(Name = "tbl_perusahaan_efek")]
    public class Tbl_perusahaan_efek : INotifyPropertyChanging, INotifyPropertyChanged
    {
        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(string.Empty);
        private int _Id_broker;
        private string _KodePerusahaanEfek;
        private string _Nama;
        private string _Alamat;
        private string _NomorTelepon;
        private EntitySet<WpfApplication1.Tables.Tbl_ktp> _Tbl_ktp;

        public Tbl_perusahaan_efek()
        {
            this._Tbl_ktp = new EntitySet<WpfApplication1.Tables.Tbl_ktp>(new Action<WpfApplication1.Tables.Tbl_ktp>(this.attach_Tbl_ktp), new Action<WpfApplication1.Tables.Tbl_ktp>(this.detach_Tbl_ktp));
        }

        [Column(Storage = "_Id_broker", AutoSync = AutoSync.OnInsert, DbType = "INT NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id_broker
        {
            get => this._Id_broker;
            set
            {
                if (this._Id_broker == value)
                    return;
                this.SendPropertyChanging();
                this._Id_broker = value;
                this.SendPropertyChanged(nameof(Id_broker));
            }
        }

        [Column(Name = "kode_perusahaan_efek", Storage = "_KodePerusahaanEfek", DbType = "NVarChar(50)")]
        public string KodePerusahaanEfek
        {
            get => this._KodePerusahaanEfek;
            set
            {
                if (this._KodePerusahaanEfek == value)
                    return;
                this.SendPropertyChanging();
                this._KodePerusahaanEfek = value;
                this.SendPropertyChanged(nameof(KodePerusahaanEfek));
            }
        }

        [Column(Name = "nama", Storage = "_Nama", DbType = "NVarChar(50)")]
        public string Nama
        {
            get => this._Nama;
            set
            {
                if (this._Nama == value)
                    return;
                this.SendPropertyChanging();
                this._Nama = value;
                this.SendPropertyChanged(nameof(Nama));
            }
        }

        [Column(Name = "alamat", Storage = "_Alamat", DbType = "NVarChar(100)")]
        public string Alamat
        {
            get => this._Alamat;
            set
            {
                if (this._Alamat == value)
                    return;
                this.SendPropertyChanging();
                this._Alamat = value;
                this.SendPropertyChanged(nameof(Alamat));
            }
        }

        [Column(Name = "nomor_telepon", Storage = "_NomorTelepon", DbType = "NVarChar(50)")]
        public string NomorTelepon
        {
            get => this._NomorTelepon;
            set
            {
                if (this._NomorTelepon == value)
                    return;
                this.SendPropertyChanging();
                this._NomorTelepon = value;
                this.SendPropertyChanged(nameof(NomorTelepon));
            }
        }
        
        [Association(Name = "FK_tbl_ktp_tbl_perusahaan_efek", Storage = "_Tbl_ktp", ThisKey = "Id_broker", OtherKey = "Id_broker", DeleteRule = "NO ACTION")]
        public EntitySet<WpfApplication1.Tables.Tbl_ktp> Tbl_ktp
        {
            get => this._Tbl_ktp;
            set => this._Tbl_ktp.Assign((IEnumerable<WpfApplication1.Tables.Tbl_ktp>) value);
        }

        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
            if (this.PropertyChanging == null)
                return;
            this.PropertyChanging(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged == null)
                return;
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        
        private void attach_Tbl_ktp(WpfApplication1.Tables.Tbl_ktp entity)
        {
            this.SendPropertyChanging();
            entity.Tbl_perusahaan_efek = this;
        }

        private void detach_Tbl_ktp(WpfApplication1.Tables.Tbl_ktp entity)
        {
            this.SendPropertyChanging();
            entity.Tbl_perusahaan_efek = (Tbl_perusahaan_efek) null;
        }
    }
}
