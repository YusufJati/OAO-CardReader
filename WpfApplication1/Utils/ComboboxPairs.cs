namespace WpfApplication1.Utils
{
    public class ComboboxPairs
    {
        public string _Key { get; set; }

        public string _Value { get; set; }

        public ComboboxPairs(string _key, string _value)
        {
            this._Key = _key;
            this._Value = _value;
        }
        
        public override string ToString()
        {
            return _Value;
        }
    }
}