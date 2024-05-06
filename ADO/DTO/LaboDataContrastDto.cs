using Laboratorium.ADO.Service;
using System;

namespace Laboratorium.ADO.DTO
{
    public class LaboDataContrastDto
    {
        public int Id { get; set; } = 0;
        public int LaboId { get; set; }
        public DateTime DateCreated { get; set; }

        private bool _isDeleted = false;
        private string _applicator;
        private short _position;
        private string _substrate;
        private double? _contrast;
        private double? _tw;
        private double? _sp;
        private string _comment;
        public DateTime DateUpdated { get; private set; }
        private RowState _rowState = RowState.ADDED;
        private readonly IService _service;
        public CrudState CrudState { get; set; } = CrudState.OK;

        public LaboDataContrastDto(int id, int laboId, DateTime dateCreated, bool isDeleted, string applicator, short position, string substrate, double? countrast, 
            double? tw, double? sp, string comment, DateTime dateUpdated, IService service)
        {
            Id = id;
            LaboId = laboId;
            DateCreated = dateCreated;
            _isDeleted = isDeleted;
            _applicator = applicator;
            _position = position;
            _substrate = substrate;
            _contrast = countrast;
            _tw = tw;
            _sp = sp;
            _comment = comment;
            DateUpdated = dateUpdated;
            _service = service;
        }

        public LaboDataContrastDto(int laboId, bool isDeleted, string applicator, short position, string substrate, double? countrast, 
            double? tw, double? sp, string comment, DateTime dateUpdated, IService service)
        {
            LaboId = laboId;
            _isDeleted = isDeleted;
            _applicator = applicator;
            _position = position;
            _substrate = substrate;
            _contrast = countrast;
            _tw = tw;
            _sp = sp;
            _comment = comment;
            DateUpdated = dateUpdated;
            _service = service;
        }

        public LaboDataContrastDto(int laboId, DateTime dateCreated, bool isDeleted, string applicator, short position, string substrate, DateTime dateUpdated, IService service)
        {
            LaboId = laboId;
            DateCreated = dateCreated;
            _isDeleted = isDeleted;
            _applicator = applicator;
            _position = position;
            _substrate = substrate;
            DateUpdated = dateUpdated;
            _service = service;
        }

        private void ChangeState(RowState state)
        {
            _rowState = _rowState == RowState.UNCHANGED ? state : _rowState;
            DateUpdated = DateTime.Today;
            if (_service != null)
                _service.Modify(state);
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
            if (_service != null)
                _service.Modify(RowState.UNCHANGED);
        }

    }
}
