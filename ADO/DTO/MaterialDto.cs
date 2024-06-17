using Laboratorium.ADO.Service;
using Laboratorium.Material.Dto;
using Laboratorium.Material.Service;
using System;
using System.Collections.Generic;

namespace Laboratorium.ADO.DTO
{
    public class MaterialDto
    {
        #region Fields

        public int Id { get; set; }
        private string _name;
        private string _index;
        private byte _supplierId;
        private short _functionId;
        private bool _isIntermediate;
        private bool _isDanger;
        private bool _isProduction;
        private bool _isObserved;
        private bool _isActive;
        private bool _isPackage;
        private double? _price;
        private double? _pricePerQuality;
        private double? _priceTransport;
        private double? _quantity;
        private byte _currencyId;
        private byte _unitId;
        private string _priceUnit;
        private double? _density;
        private double? _solids;
        private double? _ash450;
        private double? _voc;
        private string _vocPercent;
        private string _remarks;
        private DateTime _dateCreated = DateTime.Today;
        private DateTime _dateUpdated = DateTime.Today;
        private RowState _rowState = RowState.ADDED;
        private IService _service;
        public CrudState CrudState { get; set; } = CrudState.OK;

        public IList<MaterialClpGhsDto> GhsCodeList { get; set; } = new List<MaterialClpGhsDto>();
        public IList<ClpHPcombineDto> HPcodeList { get; set; } = new List<ClpHPcombineDto>();
        public MaterialClpSignalDto SignalWord { get; set; }


        #endregion

        private MaterialDto(Builder builder)
        {
            Id = builder._id;
            _name = builder._name;
            _index = builder._index;
            _supplierId = builder._supplierId;
            _functionId = builder._functionId;
            _isIntermediate = builder._isIntermediate;
            _isDanger = builder._isDanger;
            _isProduction = builder._isProduction;
            _isObserved = builder._isObserved;
            _isActive = builder._isActive;
            _isPackage = builder._isPackage;
            _price = builder._price;
            _pricePerQuality = builder._pricePerQuantity;
            _priceTransport = builder._priceTransport;
            _quantity = builder._quantity;
            _currencyId = builder._currencyId;
            _unitId = builder._unitId;
            _priceUnit = builder._priceUnit;
            _density = builder._density;
            _solids = builder._solids;
            _ash450 = builder._ash450;
            _voc = builder._voc;
            _vocPercent = builder._vocPercent;
            _remarks = builder._remarks;
            _dateCreated = builder._dateCreated;
            _dateUpdated = builder._dateUpdated;
            _service = builder._service;
            SignalWord = new MaterialClpSignalDto(Id, 1, "-- Brak --", DateTime.Today);
        }

        private void ChangeState(RowState state)
        {
            _rowState = _rowState == RowState.UNCHANGED ? state : _rowState;
            DateUpdated = DateTime.Today;
            if (_service != null)
                _service.Modify(state);
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string Index
        {
            get => _index;
            set
            {
                _index = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public byte SupplierId
        {
            get => _supplierId;
            set
            {
                _supplierId = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public short FunctionId
        {
            get => _functionId;
            set
            {
                _functionId = value;
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

        public bool IsDanger
        {
            get => _isDanger;
            set
            {
                _isDanger = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public bool IsProduction
        {
            get => _isProduction;
            set
            {
                _isProduction = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public bool IsObserved
        {
            get => _isObserved;
            set
            {
                _isObserved = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public bool IsPackage
        {
            get => _isPackage;
            set
            {
                _isPackage = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public double? Price
        {
            get => _price;
            set
            {
                _price = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public double? PricePerQuantity
        {
            get => _pricePerQuality;
            set
            {
                _pricePerQuality = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public double? PriceTransport
        {
            get => _priceTransport;
            set
            {
                _priceTransport = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public double? Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public byte CurrencyId
        {
            get => _currencyId;
            set
            {
                _currencyId = value;
                MaterialService service = (MaterialService)_service;
                service.ChangePriceUnit(this);
                ChangeState(RowState.MODIFIED);
            }
        }

        public byte UnitId
        {
            get => _unitId;
            set
            {
                _unitId = value;
                MaterialService service = (MaterialService)_service;
                service.ChangePriceUnit(this);
                ChangeState(RowState.MODIFIED);
            }
        }

        public string PriceUnit
        {
            get => _priceUnit;
            set
            {
                _priceUnit = value;
            }
        }

        public double? Density
        {
            get => _density;
            set
            {
                _density = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public double? Solids
        {
            get => _solids;
            set
            {
                _solids = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public double? Ash450
        {
            get => _ash450;
            set
            {
                _ash450 = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public double? VOC
        {
            get => _voc;
            set
            {
                _voc = value;
                if (value != null)
                    VocPercent = _voc.ToString() + "%";
                else
                    VocPercent = "";
                ChangeState(RowState.MODIFIED);
            }
        }

        public string VocPercent
        {
            get => _vocPercent;
            set
            {
                _vocPercent = value;
            }
        }

        public string Remarks
        {
            get => _remarks;
            set
            {
                _remarks = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public DateTime DateCreated => _dateCreated;

        public DateTime DateUpdated
        {
            get => _dateUpdated;
            set
            {
                _dateUpdated = value;
            }
        }

        public RowState GetRowState => _rowState;

        public IService Service
        {
            get => _service;
            set => _service = value;
        }

        public void AcceptChanges()
        {
            _rowState = RowState.UNCHANGED;
            if (_service != null)
                _service.Modify(RowState.UNCHANGED);
        }


        #region Builder Pattern Class

        public sealed class Builder
        {
            #region Fields
            internal int _id;
            internal string _name;
            internal string _index;
            internal byte _supplierId;
            internal short _functionId;
            internal bool _isIntermediate;
            internal bool _isDanger;
            internal bool _isProduction;
            internal bool _isObserved;
            internal bool _isActive;
            internal bool _isPackage;
            internal double? _price;
            internal double? _pricePerQuantity;
            internal double? _priceTransport;
            internal double? _quantity;
            internal byte _currencyId;
            internal byte _unitId;
            internal string _priceUnit;
            internal double? _density;
            internal double? _solids;
            internal double? _ash450;
            internal double? _voc;
            internal string _vocPercent;
            internal string _remarks;
            internal DateTime _dateCreated = DateTime.Today;
            internal DateTime _dateUpdated = DateTime.Today;
            internal IService _service;
            #endregion

            public MaterialDto Build()
            {
                return new MaterialDto(this);
            }

            public Builder Id(int id)
            {
                _id = id;
                return this;
            }
            public Builder Name(string val)
            {
                _name = val;
                return this;
            }
            public Builder Index(string val)
            {
                _index = val;
                return this;
            }
            public Builder SupplierId(byte val)
            {
                _supplierId = val;
                return this;
            }
            public Builder FunctinoId(short val)
            {
                _functionId = val;
                return this;
            }
            public Builder IsIntermediate(bool val)
            {
                _isIntermediate = val;
                return this;
            }
            public Builder Isdanger(bool val)
            {
                _isDanger = val;
                return this;
            }
            public Builder IsProduction(bool val)
            {
                _isProduction = val;
                return this;
            }
            public Builder IsObserved(bool val)
            {
                _isObserved = val;
                return this;
            }
            public Builder IsActive(bool val)
            {
                _isActive = val;
                return this;
            }
            public Builder IsPackage(bool val)
            {
                _isPackage = val;
                return this;
            }
            public Builder Price(double? val)
            {
                _price = val;
                return this;
            }
            public Builder PricePerQuantity(double? val)
            {
                _pricePerQuantity = val;
                return this;
            }
            public Builder PriceTransport(double? val)
            {
                _priceTransport = val;
                return this;
            }
            public Builder Quantity(double? val)
            {
                _quantity = val;
                return this;
            }
            public Builder CurrencyId(byte val)
            {
                _currencyId = val;
                return this;
            }
            public Builder UnitId(byte val)
            {
                _unitId = val;
                return this;
            }
            public Builder PriceUnit(string val)
            {
                _priceUnit = val;
                return this;
            }
            public Builder Density(double? val)
            {
                _density = val;
                return this;
            }
            public Builder Solids(double? val)
            {
                _solids = val;
                return this;
            }
            public Builder Ash450(double? val)
            {
                _ash450 = val;
                return this;
            }
            public Builder VOC(double? val)
            {
                _voc = val;
                return this;
            }
            public Builder VocPercent(string val)
            {
                _vocPercent = val;
                return this;
            }
            public Builder Remarks(string val)
            {
                _remarks = val;
                return this;
            }
            public Builder DateCreated(DateTime val)
            {
                _dateCreated = val;
                return this;
            }
            public Builder DateUpdated(DateTime val)
            {
                _dateUpdated = val;
                return this;
            }
            public Builder Service(IService val)
            {
                _service = val;
                return this;
            }
        }

        #endregion

    }
}
