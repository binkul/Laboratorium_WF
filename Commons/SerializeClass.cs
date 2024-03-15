namespace Laboratorium.Commons
{
    public class SerializeClass
    {
        public string Key { get; set; }
        public double Value { get; set; }

        public SerializeClass() { }

        public SerializeClass(string key, double value)
        {
            Key = key;
            Value = value;
        }
    }
}
