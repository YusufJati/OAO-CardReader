using System;
using System.Collections.Generic;

namespace WpfApplication1.Utils
{
    public class Customer
    {
        public int Id { get; set; }
        public string Nik { get; set; }
        public string Nama { get; set; }
        public string Email { get; set; }
        public string Alamat { get; set; }
        public string Kelurahan { get; set; }
        public string Kecamatan { get; set; }
        public string Provinsi { get; set; }
        public DateTime? tanggal_lahir { get; set; }
        //public string tempat_lahir { get; set; }
        public string jenis_kelamin { get; set; }
        public string golongan_darah { get; set; }
        public string status_pernikahan { get; set; }
        public string rt_rw { get; set; }
        public string Agama { get; set; }
        public string kewarganegaraan { get; set; }
        public string Pekerjaan { get; set; }
        public DateTime? tanggal_berlaku { get; set; }
        //public string Foto { get; set; }
        //public string TandaTangan { get; set; }
        public DateTime? tanggal_input { get; set; }
        public List<Broker> Broker { get; set; }
    }

    public class Broker
    {
        public int Id { get; set; }
        public string kode { get; set; }
        public string nama { get; set; }
        public string alamat { get; set; }
        public string nomor_telepon { get; set; }
        public string contact_person { get; set; }
        public DateTime? created_at { get; set; }
        public bool IsSelected { get; set; }
    }

    public class CustomerTransaction
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int? BrokerId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string Email { get; set; }
        public string KodeOtp { get; set; }
    }

    public class OpeningAccount
    {
        public int Id { get; set; }
        public string EfekSaham { get; set; }
        public string EfekUtang { get; set; }
        public string EfekReksaDana { get; set; }
        public string PendidikanTerakhir { get; set; }
        public string NamaIbuKandung { get; set; }
        public string NamaPerusahaan { get; set; }
        public string AlamatPerusahaan { get; set; }
        public string KodePos { get; set; }
        public string NomorPerusahaan { get; set; }
        public string JenisPekerjaan { get; set; }
        public string EmailPerusahaan { get; set; }
        public string Jabatan { get; set; }
        public string BidangUsaha { get; set; }
        public string LamaBekerja { get; set; }
        public string PenghasilanPertahun { get; set; }
        public string PenghasilanTambahanPertahun { get; set; }
        public string SumberPenghasilanUtama { get; set; }
        public string SumberPenghasilanTambahan { get; set; }
        public string SumberPendanaanTransaksi { get; set; }
        public string TujuanInvestasi { get; set; }
        public byte[] TandaTangan { get; set; }
    }
}
