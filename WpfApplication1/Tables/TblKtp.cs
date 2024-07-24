// Decompiled with JetBrains decompiler
// Type: KSEIApp.TblKtp
// Assembly: KSEIApp_V1.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A39806AA-01AF-452E-B86C-A187933A1DF3
// Assembly location: D:\Magang\Support\KSEIApp_V1.1\KSEIApp_V1.1.exe

using System;

namespace WpfApplication1.Tables
{
    public class TblKtp
    {
        public long Id { get; set; }

        public string Nik { get; set; }

        public string Nama { get; set; }

        public string Tempatlhr { get; set; }

        public DateTime? Tgllahir { get; set; }

        public string Jnskelamin { get; set; }

        public string Goldarah { get; set; }

        public string Alamat { get; set; }

        public string Rtrw { get; set; }

        public string Kel { get; set; }

        public string Kec { get; set; }

        public string Agama { get; set; }

        public string Statuskawin { get; set; }

        public string Pekerjaan { get; set; }

        public string Kewarganegaraan { get; set; }

        public DateTime? Berlaku { get; set; }

        public string Provinsi { get; set; }

        public string Foto { get; set; }

        public string Tandatangan { get; set; }

        public DateTime? Tglinput { get; set; }
        
        public string Email { get; set; }
        
        public virtual TblPerusahaanEfek IdEfekNavigation { get; set; }
    }
}