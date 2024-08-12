using Laboratorium.ADO.Service;
using System;
using System.Text.RegularExpressions;

namespace Laboratorium.ADO.DTO
{
    public class CompositionDto
    {
        private int _laboId;
        private int _version;
        private short _ordering;
        private string _material;
        private int _materialId;
        private bool _isIntermediate;
        private double _amount;
        private double _mass;
        private byte _operation;
        private string _comment;
        private double? _voc;
        private double? _priceOryg;
        private double? _pricePl;
        private string _currency;
        private double? _rate;
        private RowState _rowState = RowState.ADDED;
        private IService _service;

        public CrudState CrudState { get; set; } = CrudState.OK;

        private CompositionDto(Builder builder)
        {
            _laboId = builder._laboId;
            _version = builder._version;
            _ordering = builder._ordering;
            _material = builder._material;
            _materialId = builder._materialId;
            _isIntermediate = builder._isIntermediate;
            _amount = builder._amount;
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

        public bool IsIntermediate
        {
            get => _isIntermediate;
            set
            {
                _isIntermediate = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public double Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                ChangeState(RowState.MODIFIED);
            }
        }

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
            get => IsIntermediate ? "-" : _priceOryg != null ? Convert.ToDouble(_priceOryg).ToString("0.##") + " " + Currency : "Brak";
        }

        public double? PricePl
        {
            get => _pricePl;
            set
            {
                _pricePl = value;
            }
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
            internal bool _isIntermediate;
            internal double _amount;
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
            public Builder IsIntermediate(bool val)
            {
                _isIntermediate = val;
                return this;
            }
            public Builder Amount(double val)
            {
                _amount = val;
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
