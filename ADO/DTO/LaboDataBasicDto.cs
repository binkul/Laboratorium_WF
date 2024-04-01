using Laboratorium.ADO.Service;
using System;

namespace Laboratorium.ADO.DTO
{
    public class LaboDataBasicDto
    {
        #region Fields

        public int Id { get; set; }
        public int LaboId { get; set; }
        private double? _gloss20;
        private double? _gloss60;
        private double? _gloss85;
        private byte _glossClassId;
        private string _glossComment;
        private string _scrubBrush;
        private string _scrubSponge;
        private byte _scrubClassId;
        private string _scrubComment;
        private byte _contrastClassId;
        private string _contrastComment;
        private string _voc;
        private byte _vocClassId;
        private string _yield;
        private string _yieldFormula;
        private string _adhesion;
        private string _flow;
        private string _spill;
        private string _dryingI;
        private string _dryingII;
        private string _dryingIII;
        private string _dryingIV;
        private string _dryingV;
        public DateTime DateUpdated { get; private set; }
        private RowState _rowState = RowState.ADDED;
        private readonly IService _service;

        #endregion

        private LaboDataBasicDto(Builder builder)
        {
            Id = builder._id;
            LaboId = builder._laboId;
            _gloss20 = builder._gloss20;
            _gloss60 = builder._gloss60;
            _gloss85 = builder._gloss85;
            _glossClassId = builder._glossClassId;
            _glossComment = builder._glossComment;
            _scrubBrush = builder._scrubBrush;
            _scrubSponge = builder._scrubSponge;
            _scrubClassId = builder._scrubClassId;
            _scrubComment = builder._scrubComment;
            _contrastClassId = builder._contrastClassId;
            _contrastComment = builder._contrastComment;
            _voc = builder._voc;
            _vocClassId = builder._vocClassId;
            _yield = builder._yield;
            _yieldFormula = builder._yieldFormula;
            _adhesion = builder._adhesion;
            _flow = builder._flow;
            _spill = builder._spill;
            _dryingI = builder._dryingI;
            _dryingII = builder._dryingII;
            _dryingIII = builder._dryingIII;
            _dryingIV = builder._dryingIV;
            _dryingV = builder._dryingV;
            DateUpdated = builder._dateUpdated;
            _service = builder._service;
        }

        private void ChangeState(RowState state)
        {
            _rowState = _rowState == RowState.UNCHANGED ? state : _rowState;
            DateUpdated = DateTime.Today;
            if (_service != null)
                _service.Modify(state);
        }

        public double? Gloss20
        {
            get => _gloss20;
            set
            {
                _gloss20 = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public double? Gloss60
        {
            get => _gloss60;
            set
            {
                _gloss60 = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public double? Gloss85
        {
            get => _gloss85;
            set
            {
                _gloss85 = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public byte GlossClassId
        {
            get => _glossClassId;
            set
            {
                _glossClassId = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string GlossComment
        {
            get => _glossComment;
            set
            {
                _glossComment = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string ScrubBrush
        {
            get => _scrubBrush;
            set
            {
                _scrubBrush = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string ScrubSponge
        {
            get => _scrubSponge;
            set
            {
                _scrubSponge = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public byte ScrubClassId
        {
            get => _scrubClassId;
            set
            {
                _scrubClassId = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string ScrubComment
        {
            get => _scrubComment;
            set
            {
                _scrubComment = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public byte ContrastClassId
        {
            get => _contrastClassId;
            set
            {
                _contrastClassId = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string ContrastComment
        {
            get => _contrastComment;
            set
            {
                _contrastComment = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public byte VocClassId
        {
            get => _vocClassId;
            set
            {
                _vocClassId = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string VOC
        {
            get => _voc;
            set
            {
                _voc = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string Yield
        {
            get => _yield;
            set
            {
                _yield = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string YieldFormula
        {
            get => _yieldFormula;
            set
            {
                _yieldFormula = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string Adhesion
        {
            get => _adhesion;
            set
            {
                _adhesion = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string Flow
        {
            get => _flow;
            set
            {
                _flow = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string Spill
        {
            get => _spill;
            set
            {
                _spill = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string DryingI
        {
            get => _dryingI;
            set
            {
                _dryingI = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string DryingII
        {
            get => _dryingII;
            set
            {
                _dryingII = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string DryingIII
        {
            get => _dryingIII;
            set
            {
                _dryingIII = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string DryingIV
        {
            get => _dryingIV;
            set
            {
                _dryingIV = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string DryingV
        {
            get => _dryingV;
            set
            {
                _dryingV = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public RowState GetRowState => _rowState;

        public void AcceptChanged()
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
            internal int _laboId;
            internal double? _gloss20;
            internal double? _gloss60;
            internal double? _gloss85;
            internal byte _glossClassId;
            internal string _glossComment;
            internal string _scrubBrush;
            internal string _scrubSponge;
            internal byte _scrubClassId;
            internal string _scrubComment;
            internal byte _contrastClassId;
            internal string _contrastComment;
            internal string _voc;
            internal byte _vocClassId;
            internal string _yield;
            internal string _yieldFormula;
            internal string _adhesion;
            internal string _flow;
            internal string _spill;
            internal string _dryingI;
            internal string _dryingII;
            internal string _dryingIII;
            internal string _dryingIV;
            internal string _dryingV;
            internal DateTime _dateUpdated;
            internal IService _service;
            #endregion

            public LaboDataBasicDto Build()
            {
                return new LaboDataBasicDto(this);
            }
            public Builder Id(int id)
            {
                _id = id;
                return this;
            }
            public Builder LaboId(int laboId)
            {
                _laboId = laboId;
                return this;
            }
            public Builder Gloss20(double? val)
            {
                _gloss20 = val;
                return this;
            }
            public Builder Gloss60(double? val)
            {
                _gloss60 = val;
                return this;
            }
            public Builder Gloss85(double? val)
            {
                _gloss85 = val;
                return this;
            }
            public Builder GlossClassId(byte val)
            {
                _glossClassId = val;
                return this;
            }
            public Builder GlossComment(string val)
            {
                _glossComment = val;
                return this;
            }
            public Builder ScrubBrush(string val)
            {
                _scrubBrush = val;
                return this;
            }
            public Builder ScrubSponge(string val)
            {
                _scrubSponge = val;
                return this;
            }
            public Builder ScrubClassId(byte val)
            {
                _scrubClassId = val;
                return this;
            }
            public Builder ScrubComment(string val)
            {
                _scrubComment = val;
                return this;
            }
            public Builder ContrastClassId(byte val)
            {
                _contrastClassId = val;
                return this;
            }
            public Builder ContrastComment(string val)
            {
                _contrastComment = val;
                return this;
            }
            public Builder VOC(string val)
            {
                _voc = val;
                return this;
            }
            public Builder VocClassId(byte val)
            {
                _vocClassId = val;
                return this;
            }
            public Builder Yield(string val)
            {
                _yield = val;
                return this;
            }
            public Builder YieldFormula(string val)
            {
                _yieldFormula = val;
                return this;
            }
            public Builder Adhesion(string val)
            {
                _adhesion = val;
                return this;
            }
            public Builder Flow(string val)
            {
                _flow = val;
                return this;
            }
            public Builder Spill(string val)
            {
                _spill = val;
                return this;
            }
            public Builder DryingI(string val)
            {
                _dryingI = val;
                return this;
            }
            public Builder DryingII(string val)
            {
                _dryingII = val;
                return this;
            }
            public Builder DryingIII(string val)
            {
                _dryingIII = val;
                return this;
            }
            public Builder DryingIV(string val)
            {
                _dryingIV = val;
                return this;
            }
            public Builder DryingV(string val)
            {
                _dryingV = val;
                return this;
            }
            public Builder Date(DateTime val)
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
