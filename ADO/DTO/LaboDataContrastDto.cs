using Laboratorium.ADO.Service;
using System;

namespace Laboratorium.ADO.DTO
{
    public class LaboDataContrastDto
    {
        private bool _isDeleted = false;
        private string _applicator;
        private short _position;
        private string _substrate;
        private double? _contrast;
        private double? _tw;
        private double? _sp;
        private string _comment;
        private DateTime _dateCreated;
        private DateTime _dateUpdated;
        private RowState _rowState = RowState.ADDED;
        private readonly IService _service;

        public int Id { get; set; } = 0;
        public int TmpId { get; set; } = 0;
        public int LaboId { get; set; }
        public int Days { get; private set; }
        public CrudState CrudState { get; set; } = CrudState.OK;

        public LaboDataContrastDto(int id, int laboId, DateTime dateCreated, bool isDeleted, string applicator, short position, string substrate, double? countrast, 
            double? tw, double? sp, string comment, DateTime dateUpdated, IService service)
        {
            Id = id;
            TmpId = id;
            LaboId = laboId;
            _dateCreated = dateCreated;
            _isDeleted = isDeleted;
            _applicator = applicator;
            _position = position;
            _substrate = substrate;
            _contrast = countrast;
            _tw = tw;
            _sp = sp;
            _comment = comment;
            _dateUpdated = dateUpdated;
            _service = service;
        }

        public LaboDataContrastDto(int tmpId, int laboId, bool isDeleted, string applicator, short position, string substrate, double? countrast, 
            double? tw, double? sp, string comment, DateTime dateUpdated, IService service)
        {
            TmpId = tmpId;
            LaboId = laboId;
            _isDeleted = isDeleted;
            _applicator = applicator;
            _position = position;
            _substrate = substrate;
            _contrast = countrast;
            _tw = tw;
            _sp = sp;
            _comment = comment;
            _dateUpdated = dateUpdated;
            _service = service;
        }

        public LaboDataContrastDto(int tmpId, int laboId, DateTime dateCreated, bool isDeleted, string applicator, short position, string substrate, DateTime dateUpdated, IService service)
        {
            TmpId = tmpId;
            LaboId = laboId;
            _dateCreated = dateCreated;
            _isDeleted = isDeleted;
            _applicator = applicator;
            _position = position;
            _substrate = substrate;
            _dateUpdated = dateUpdated;
            _service = service;
        }

        private void ChangeState(RowState state)
        {
            _dateUpdated = DateTime.Today;
            UpdaetRowState(state);
        }

        private void UpdateDays()
        {
            Days = (int)(DateUpdated - DateCreated).TotalDays;
        }

        private void UpdaetRowState(RowState state)
        {
            UpdateDays();
            _rowState = _rowState == RowState.UNCHANGED ? state : _rowState;
            if (_service != null)
                _service.Modify(_rowState);
        }

        public DateTime DateCreated
        {
            get => _dateCreated;
            set
            {
                _dateCreated = value;
                UpdaetRowState(RowState.MODIFIED);
            }
        }

        public DateTime DateUpdated
        {
            get => _dateUpdated;
            set
            {
                _dateUpdated = value;
                UpdaetRowState(RowState.MODIFIED);
            }
        }

        public bool IsDeleted
        {
            get => _isDeleted;
            set
            {
                _isDeleted = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string Applicator
        {
            get => _applicator;
            set
            {
                _applicator = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public short Position
        {
            get => _position;
            set
            {
                _position = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string Substrate
        {
            get => _substrate;
            set
            {
                _substrate = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public double? Contrast
        {
            get => _contrast;
            set
            {
                _contrast = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public double? Tw
        {
            get => _tw;
            set
            {
                _tw = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public double? Sp
        {
            get => _sp;
            set
            {
                _sp = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string Comments
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
        }

    }
}
