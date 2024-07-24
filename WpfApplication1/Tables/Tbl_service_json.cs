using System.ComponentModel;
using System.Data.Linq.Mapping;

namespace WpfApplication1.Tables
{
    [Table(Name = "tbl_service_json")]
    public class Tbl_service_json : INotifyPropertyChanging, INotifyPropertyChanged
    {
        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(string.Empty);
        private string _Nik;
        private string _Json;

        [Column(Name = "nik", Storage = "_Nik", DbType = "NChar(100) NOT NULL", CanBeNull = false, IsPrimaryKey = true)]
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

        [Column(Name = "json", Storage = "_Json", DbType = "NText NOT NULL", CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
        public string Json
        {
            get => this._Json;
            set
            {
                if (!(this._Json != value))
                    return;
                this.SendPropertyChanging();
                this._Json = value;
                this.SendPropertyChanged(nameof (Json));
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
            if (this.PropertyChanging == null)
                return;
            this.PropertyChanging((object) this, Tbl_service_json.emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged == null)
                return;
            this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
        }
    }
}