using System.Data.SQLite;

namespace WpfApplication1.Utils
{
    public class DBContextSingleton
    {
        private static DBKTPContext _context;
        private static readonly object _lock = new object();

        private DBContextSingleton() { }

        public static DBKTPContext Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_context == null)
                    {
                        var connectionstring = "Data Source=data.sqlite;Version=3;";
                        var conn = new SQLiteConnection(connectionstring);
                        conn.Open();
                        _context = new DBKTPContext(conn);
                    }
                    return _context;
                }
            }
        }
    }

}