/*using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace WpfApplication1.Utils
{
    [Database(Name = "data.sqlite")]
    public class DBKTPContext : DataContext
    {
        private static MappingSource mappingSource = (MappingSource) new AttributeMappingSource();

        public DBKTPContext(string connection)
            : base(connection, DBKTPContext.mappingSource)
        {
        }

        public DBKTPContext(IDbConnection connection)
            : base(connection, DBKTPContext.mappingSource)
        {
        }

        public DBKTPContext(string connection, MappingSource mappingSource)
            : base(connection, mappingSource)
        {
        }

        public DBKTPContext(IDbConnection connection, MappingSource mappingSource)
            : base(connection, mappingSource)
        {
        }

        public Table<WpfApplication1.Tables.Tbl_ktp> Tbl_ktp => this.GetTable<WpfApplication1.Tables.Tbl_ktp>();

        public Table<WpfApplication1.Tables.Tbl_login> Tbl_login => this.GetTable<WpfApplication1.Tables.Tbl_login>();

        public Table<Tbl_role> Tbl_role => this.GetTable<Tbl_role>();

        public Table<WpfApplication1.Tables.Tbl_service_content> Tbl_service_content
        {
            get => this.GetTable<WpfApplication1.Tables.Tbl_service_content>();
        }

        public Table<WpfApplication1.Tables.Tbl_service_json> Tbl_service_json => this.GetTable<WpfApplication1.Tables.Tbl_service_json>();
    }
}*/

using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.SQLite;

namespace WpfApplication1.Utils
{
    [Database(Name = "data.sqlite")]
    public class DBKTPContext : DataContext
    {
        private static MappingSource mappingSource = new AttributeMappingSource();

        // Constructor untuk menerima string koneksi
        public DBKTPContext(string connection)
            : base(new SQLiteConnection(connection), DBKTPContext.mappingSource)
        {
        }

        // Constructor untuk menerima objek IDbConnection
        public DBKTPContext(IDbConnection connection)
            : base(connection, DBKTPContext.mappingSource)
        {
        }

        // Deklarasi tabel-tabel yang ada dalam database
        public Table<WpfApplication1.Tables.Tbl_ktp> Tbl_ktp => GetTable<WpfApplication1.Tables.Tbl_ktp>();

        public Table<WpfApplication1.Tables.Tbl_login> Tbl_login => GetTable<WpfApplication1.Tables.Tbl_login>();

        public Table<Tbl_role> Tbl_role => GetTable<Tbl_role>();
        
        public Table<WpfApplication1.Tables.Tbl_perusahaan_efek> Tbl_perusahaan_efek => GetTable<WpfApplication1.Tables.Tbl_perusahaan_efek>();
        
        public Table<WpfApplication1.Tables.Tbl_service_content> Tbl_service_content => GetTable<WpfApplication1.Tables.Tbl_service_content>();

        public Table<WpfApplication1.Tables.Tbl_service_json> Tbl_service_json => GetTable<WpfApplication1.Tables.Tbl_service_json>();
    }
}

