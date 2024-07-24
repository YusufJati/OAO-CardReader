using System.Collections.Generic;

namespace WpfApplication1.Tables
{
    public class TblPerusahaanEfek
    {
        public TblPerusahaanEfek() => this.TblKtp = (ICollection<WpfApplication1.Tables.TblKtp>) new HashSet<WpfApplication1.Tables.TblKtp>();
        
        public int Id { get; set; }
        
        public string KodePerusahaanEfek { get; set; }
        
        public string Nama { get; set; }
        
        public string Alamat { get; set; }
        
        public string NomorTelepon { get; set; }
        public virtual ICollection<WpfApplication1.Tables.TblKtp> TblKtp { get; set; }

    }
}