using Laboratorium.ADO.Service;
using System;

namespace Laboratorium.ADO.DTO
{
    public class LaboDataViscosityDto
    {
        #region Fields

        public int Id { get; set; }
        public int LaboId { get; set; }
        private bool _toCompare;
        private double? _pH;
        private string _temp;
        private double? _brook1;
        private double? _brook5;
        private double? _brook10;
        private double? _brook20;
        private double? _brook30;
        private double? _brook40;
        private double? _brook50;
        private double? _brook60;
        private double? _brook70;
        private double? _brook80;
        private double? _brook90;
        private double? _brook100;
        private string _brookDisc;
        private string _brookComment;
        private double? _brookXvisc;
        private string _brookXrpm;
        private string _brookXdisc;
        private double? _krebs;
        private string _krebsComment;
        private double? _ici;
        private string _iciDisc;
        private string _iciComment;
        private DateTime _dateCreated = DateTime.Today;
        private DateTime _dateUpdated = DateTime.Today;
        private RowState _rowState = RowState.ADDED;
        private readonly IService _service;
        public CrudState CrudState { get; set; } = CrudState.OK;

        #endregion

        public LaboDataViscosityDto()
        { }

        private LaboDataViscosityDto(Builder builder)
        {
            Id = builder._id;
            LaboId = builder._laboId;
            _toCompare = builder._toCompare;
            _pH = builder._pH;
            _temp = builder._temp;
            _brook1 = builder._brook1;
            _brook5 = builder._brook5;
            _brook10 = builder._brook10;
            _brook20 = builder._brook20;
            _brook30 = builder._brook30;
            _brook40 = builder._brook40;
            _brook50 = builder._brook50;
            _brook60 = builder._brook60;
            _brook70 = builder._brook70;
            _brook80 = builder._brook80;
            _brook90 = builder._brook90;
            _brook100 = builder._brook100;
            _brookDisc = builder._brookDisc;
            _brookComment = builder._brookComment;
            _brookXvisc = builder._brookXvisc;
            _brookXrpm = builder._brookXrpm;
            _brookXdisc = builder._brookXdisc;
            _krebs = builder._krebs;
            _krebsComment = builder._krebsComment;
            _ici = builder._ici;
            _iciDisc = builder._iciDisc;
            _iciComment = builder._iciComment;
            DateCreated = builder._dateCreated;
            DateUpdated = builder._dateUpdated;
            _service = builder._service;
        }

        private void ChangeState(RowState state)
        {
            _rowState = _rowState == RowState.UNCHANGED ? state : _rowState;
            if (_service != null)
                _service.Modify(state);
        }

        public int Days => (DateUpdated - DateCreated).Days;

        public bool ToCompare
        {
            get => _toCompare;
            set
            {
                _toCompare = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public double? pH
        {
            get => _pH;
            set
            {
                _pH = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string Temp
        {
            get => _temp;
            set
            {
                _temp = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public double? Brook1
        {
            get => _brook1;
            set
            {
                _brook1 = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public double? Brook5
        {
            get => _brook5;
            set
            {
                _brook5 = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public double? Brook10
        {
            get => _brook10;
            set
            {
                _brook10 = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public double? Brook20
        {
            get => _brook20;
            set
            {
                _brook20 = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public double? Brook30
        {
            get => _brook30;
            set
            {
                _brook30 = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public double? Brook40
        {
            get => _brook40;
            set
            {
                _brook40 = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public double? Brook50
        {
            get => _brook50;
            set
            {
                _brook50 = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public double? Brook60
        {
            get => _brook60;
            set
            {
                _brook60 = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public double? Brook70
        {
            get => _brook70;
            set
            {
                _brook70 = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public double? Brook80
        {
            get => _brook80;
            set
            {
                _brook80 = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public double? Brook90
        {
            get => _brook90;
            set
            {
                _brook90 = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public double? Brook100
        {
            get => _brook100;
            set
            {
                _brook100 = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string BrookDisc
        {
            get => _brookDisc;
            set
            {
                _brookDisc = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string BrookComment
        {
            get => _brookComment;
            set
            {
                _brookComment = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public double? BrookXvisc
        {
            get => _brookXvisc;
            set
            {
                _brookXvisc = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string BrookXrpm
        {
            get => _brookXrpm;
            set
            {
                _brookXrpm = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string BrookXdisc
        {
            get => _brookXdisc;
            set
            {
                _brookXdisc = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public double? Krebs
        {
            get => _krebs;
            set
            {
                _krebs = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string KrebsComment
        {
            get => _krebsComment;
            set
            {
                _krebsComment = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public double? ICI
        {
            get => _ici;
            set
            {
                _ici = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string IciDisc
        {
            get => _iciDisc;
            set
            {
                _iciDisc = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string IciComment
        {
            get => _iciComment;
            set
            {
                _iciComment = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public DateTime DateCreated
        {
            get => _dateCreated;
            set
            {
                _dateCreated = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public DateTime DateUpdated
        {
            get => _dateUpdated;
            set
            {
                _dateUpdated = value;
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
            internal bool _toCompare;
            internal double? _pH;
            internal string _temp;
            internal double? _brook1;
            internal double? _brook5;
            internal double? _brook10;
            internal double? _brook20;
            internal double? _brook30;
            internal double? _brook40;
            internal double? _brook50;
            internal double? _brook60;
            internal double? _brook70;
            internal double? _brook80;
            internal double? _brook90;
            internal double? _brook100;
            internal string _brookDisc;
            internal string _brookComment;
            internal double? _brookXvisc;
            internal string _brookXrpm;
            internal string _brookXdisc;
            internal double? _krebs;
            internal string _krebsComment;
            internal double? _ici;
            internal string _iciDisc;
            internal string _iciComment;
            internal DateTime _dateCreated;
            internal DateTime _dateUpdated;
            internal IService _service;
            #endregion

            public LaboDataViscosityDto Build()
            {
                return new LaboDataViscosityDto(this);
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
            public Builder ToCompare(bool val)
            {
                _toCompare = val;
                return this;
            }
            public Builder pH(double? val)
            {
                _pH = val;
                return this;
            }
            public Builder Temp(string val)
            {
                _temp = val;
                return this;
            }
            public Builder Brook1(double? val)
            {
                _brook1 = val;
                return this;
            }
            public Builder Brook5(double? val)
            {
                _brook5 = val;
                return this;
            }
            public Builder Brook10(double? val)
            {
                _brook10 = val;
                return this;
            }
            public Builder Brook20(double? val)
            {
                _brook20 = val;
                return this;
            }
            public Builder Brook30(double? val)
            {
                _brook30 = val;
                return this;
            }
            public Builder Brook40(double? val)
            {
                _brook40 = val;
                return this;
            }
            public Builder Brook50(double? val)
            {
                _brook50 = val;
                return this;
            }
            public Builder Brook60(double? val)
            {
                _brook60 = val;
                return this;
            }
            public Builder Brook70(double? val)
            {
                _brook70 = val;
                return this;
            }
            public Builder Brook80(double? val)
            {
                _brook80 = val;
                return this;
            }
            public Builder Brook90(double? val)
            {
                _brook90 = val;
                return this;
            }
            public Builder Brook100(double? val)
            {
                _brook100 = val;
                return this;
            }
            public Builder BrookDisc(string val)
            {
                _brookDisc = val;
                return this;
            }
            public Builder BrookComment(string val)
            {
                _brookComment = val;
                return this;
            }
            public Builder BrookXviscosity(double? val)
            {
                _brookXvisc = val;
                return this;
            }
            public Builder BrookXrpm(string val)
            {
                _brookXrpm = val;
                return this;
            }
            public Builder BrookXdisc(string val)
            {
                _brookXdisc = val;
                return this;
            }
            public Builder Krebs(double? val)
            {
                _krebs = val;
                return this;
            }
            public Builder KrebsComment(string val)
            {
                _krebsComment = val;
                return this;
            }
            public Builder ICI(double? val)
            {
                _ici = val;
                return this;
            }
            public Builder IciDisc(string val)
            {
                _iciDisc = val;
                return this;
            }
            public Builder IciComment(string val)
            {
                _iciComment = val;
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
