using Laboratorium.ADO.Service;
using System;

namespace Laboratorium.ADO.DTO
{
    public class LaboDataNormTestDto
    {
        public int Id { get; set; } = 0;
        public int LaboId { get; set; }
        public int Days { get; set; } = 0;
        public DateTime DateCreated { get; set; } = DateTime.Today;

        private short _position;
        private string _norm;
        private string _description;
        private string _requirement;
        private string _result;
        private string _substrate;
        private string _comment;
        public DateTime DateUpdated { get; private set; }
        private RowState _rowState = RowState.ADDED;
        private readonly IService _service;
        public CrudState CrudState { get; set; } = CrudState.OK;

        public LaboDataNormTestDto(int days, int id, int laboId, short position, string norm, string description, string requirement, 
            string result, string substrate, string comment, DateTime dateCreated, DateTime dateUpdated, IService service)
        {
            Days = days;
            Id = id;
            LaboId = laboId;
            DateCreated = dateCreated;
            _position = position;
            _norm = norm;
            _description = description;
            _requirement = requirement;
            _result = result;
            _substrate = substrate;
            _comment = comment;
            DateUpdated = dateUpdated;
            _service = service;
        }

        public LaboDataNormTestDto(int laboId, short position, string norm, string description, string requirement, string result, 
            string substrate, string comment, DateTime dateUpdated, IService service)
        {
            LaboId = laboId;
            _position = position;
            _norm = norm;
            _description = description;
            _requirement = requirement;
            _result = result;
            _substrate = substrate;
            _comment = comment;
            _service = service;
            DateUpdated = dateUpdated;
        }

        public LaboDataNormTestDto(int laboId, short position, DateTime dateUpdated, IService service)
        {
            LaboId = laboId;
            _position = position;
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

        public short Position
        {
            get => _position;
            set
            {
                _position = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string Norm
        {
            get => _norm;
            set
            {
                _norm = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string Requirement
        {
            get => _requirement;
            set
            {
                _requirement = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string Result
        {
            get => _result;
            set
            {
                _result = value;
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
