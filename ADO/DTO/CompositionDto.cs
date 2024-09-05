namespace Laboratorium.ADO.DTO
{
    public class CompositionDto
    {
        public int LaboId { get; set; }
        public int Version { get; set; }
        public short Ordering { get; set; }
        public string Material { get; set; }
        public int MaterialId { get; set; }
        public bool IsSemiproduct { get; set; }
        public double Percent { get; set; }
        public double PercentOryginal { get; }
        public double? VocMaterial { get; set; }
        public double? PriceOriginal { get; set; }
        public double? PricePlKg { get; set; }
        public string Currency { get; set; }
        public double? Rate { get; }
        public byte Operation { get; set; }
        public string Comment { get; set; }

        private CompositionDto(Builder builder)
        {
            LaboId = builder._laboId;
            Version = builder._version;
            Ordering = builder._ordering;
            Material = builder._material;
            MaterialId = builder._materialId;
            IsSemiproduct = builder._isSemiproduct;
            Percent = builder._percent;
            PercentOryginal = builder._percentOryginal;
            Operation = builder._operation;
            Comment = builder._comment;
            VocMaterial = builder._voc;
            PriceOriginal = builder._priceOryg;
            PricePlKg = builder._pricePl;
            Currency = builder._currency;
            Rate = builder._rate;
        }

        public sealed class Builder
        {
            #region Fields

            internal int _laboId;
            internal int _version;
            internal short _ordering;
            internal string _material;
            internal int _materialId;
            internal bool _isSemiproduct;
            internal double _percent;
            internal double _percentOryginal;
            internal double _mass;
            internal byte _operation;
            internal string _comment;
            internal double? _voc;
            internal double? _priceOryg;
            internal double? _pricePl;
            internal string _currency;
            internal double? _rate;

            #endregion

            public CompositionDto Build()
            {
                return new CompositionDto(this);
            }

            public Builder LaboId(int val)
            {
                _laboId = val;
                return this;
            }
            public Builder Version(int val)
            {
                _version = val;
                return this;
            }
            public Builder Ordering(short val)
            {
                _ordering = val;
                return this;
            }
            public Builder Material(string val)
            {
                _material = val;
                return this;
            }
            public Builder MaterialId(int val)
            {
                _materialId = val;
                return this;
            }
            public Builder IsSemiproduct(bool val)
            {
                _isSemiproduct = val;
                return this;
            }
            public Builder Percent(double val)
            {
                _percent = val;
                return this;
            }
            public Builder PercentOriginal(double val)
            {
                _percentOryginal = val;
                return this;
            }
            public Builder Mass(double val)
            {
                _mass = val;
                return this;
            }
            public Builder Operation(byte val)
            {
                _operation = val;
                return this;
            }
            public Builder Comment(string val)
            {
                _comment = val;
                return this;
            }
            public Builder VOC(double? val)
            {
                _voc = val;
                return this;
            }
            public Builder PriceOriginal(double? val)
            {
                _priceOryg = val;
                return this;
            }
            public Builder PricePl(double? val)
            {
                _pricePl = val;
                return this;
            }
            public Builder Currency(string val)
            {
                _currency = val;
                return this;
            }
            public Builder Rate(double? val)
            {
                _rate = val;
                return this;
            }
        }
    }
}
