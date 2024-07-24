namespace WpfApplication1.Tables
{
    public class TblLogin
    {
        public long Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public long? IdRole { get; set; }

        public string Deskripsi { get; set; }

        public virtual TblRole IdRoleNavigation { get; set; }
    }
}