using System.Collections.Generic;

namespace WpfApplication1.Tables
{
    public class TblRole
    {
        public TblRole() => this.TblLogin = (ICollection<WpfApplication1.Tables.TblLogin>) new HashSet<WpfApplication1.Tables.TblLogin>();

        public long IdRole { get; set; }

        public string Role { get; set; }

        public string Ket { get; set; }

        public virtual ICollection<WpfApplication1.Tables.TblLogin> TblLogin { get; set; }
    }
}