using Laboratorium.ADO.Service;
using System;
using System.Collections.Generic;

namespace Laboratorium.ADO.DTO
{
    public enum ExpandState
    {
        None,
        Expanded,
        Collapsed
    }

    public class CompositionDto
    {
        public int Id { get; set; }
        public bool Visible { get; set; } = false;
        public byte VisibleLevel { get; set; } = 0;
        public bool LastPosition {get; set;} = false;
        public int SubLevel { get; set; } = 0;
        private int _laboId;
        private int _version;
        private short _ordering;
        private string _material;
        private int _materialId;
        private bool _isSemiproduct;
        private ExpandState _semiproductExpandState = ExpandState.None;
        private double _percent;        // for manipulation
        private double _percentOrginal; // from DataBase, not modified
        private double _mass;
        private byte _operation;
        private string _comment;
        private double? _voc;
        private string _vocAmount;
        private double? _priceOryg;
        private double? _pricePl;
        private string _priceMass;
        private string _currency;
        private double? _rate;
        private RowState _rowState = RowState.ADDED;
        private IService _service;

        public IList<CompositionDto> SubProductComposition { get; set; } = null;

        public CrudState CrudState { get; set; } = CrudState.OK;

        private CompositionDto(Builder builder)
        {
            _laboId = builder._laboId;
            _version = builder._version;
            _ordering = builder._ordering;
            _material = builder._material;
            _materialId = builder._materialId;
            _isSemiproduct = builder._isSemiproduct;
            _percent = builder._percent;
            _percentOrginal = builder._percentOryginal;
            _mass = builder._mass;
            _operation = builder._operation;
            _comment = builder._comment;
            _voc = builder._voc;
            _priceOryg = builder._priceOryg;
            _pricePl = builder._pricePl;
            _currency = builder._currency;
            _rate = builder._rate;
            _service = builder._service;
        }


        private void ChangeState(RowState state)
        {
            _rowState = _rowState == RowState.UNCHANGED ? state : _rowState;
            if (_service != null)
                _service.Modify(state);
        }

        public int LaboId
        {
            get => _laboId;
            set
            {
                _laboId = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public int Version
        {
            get => _version;
            set
            {
                _version = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public short Ordering
        {
            get => _ordering;
            set
            {
                _ordering = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string Material
        {
            get => _material;
            set
            {
                _material = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public int MaterialId
        {
            get => _materialId;
            set
            {
                _materialId = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public bool IsSemiproduct
        {
            get => _isSemiproduct;
            set
            {
                _isSemiproduct = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public ExpandState SemiProductState
        {
            get => _semiproductExpandState;
            set => _semiproductExpandState = value;
        }

        public double Percent
        {
            get => _percent;
            set
            {
                _percent = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public double PercentOryginal => _percentOrginal;

        public double Mass
        {
            get => _mass;
            set
            {
                _mass = value;
            }
        }

        public double? VOC
        {
            get => _voc;
            set
            {
                _voc = value;
            }
        }

        public string VocPercent
        {
            get => _voc != -1 ? Convert.ToDouble(_voc).ToString("0.00") : "Brak";
        }

        public string VocAmount
        {
            get => !string.IsNullOrEmpty(_vocAmount) ? _vocAmount : "# Count #";
            set => _vocAmount = value;
        }

        public double? PriceOriginal
        {
            get => _priceOryg;
            set
            {
                _priceOryg = value;
            }
        }

        public string PriceCurrency
        {
            get => IsSemiproduct ? "-" : _priceOryg != null && _priceOryg != -1 ? Convert.ToDouble(_priceOryg).ToString("0.00") + " " + Currency : "Brak";
        }

        public double? PricePlKg
        {
            get => _pricePl;
            set
            {
                _pricePl = value;
            }
        }

        public string PriceMass
        {
            get => !string.IsNullOrEmpty(_priceMass) ? _priceMass : "# Count #";
            set => _priceMass = value;
        }

        public string Currency
        {
            get => _currency;
            set
            {
                _currency = value;
            }
        }

        public double? Rate
        {
            get => _rate;
            set
            {
                _rate = value;
            }
        }

        public byte Operation
        {
            get => _operation;
            set
            {
                _operation = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string Comment
        {
            get => _comment;
            set
            {
                _comment = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public RowState GetRowState => _rowState;

        public void AcceptChanges()
        {
            _rowState = RowState.UNCHANGED;
            if (_service != null)
                _service.Modify(RowState.UNCHANGED);
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
            internal IService _service;
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
            public Builder Service(IService val)
            {
                _service = val;
                return this;
            }

        }
    }
}
