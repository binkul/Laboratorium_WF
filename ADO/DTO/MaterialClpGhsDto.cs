using Laboratorium.ADO.Service;
using System;

namespace Laboratorium.ADO.DTO
{
    public class MaterialClpGhsDto
    {
        public int MaterialId { get; set; }
        public byte CodeId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Today;
        private RowState _rowState;
        private readonly IService _service;

        public MaterialClpGhsDto(int materialId, byte codeId, DateTime dateCreated, IService service)
        {
            MaterialId = materialId;
            CodeId = codeId;
            DateCreated = dateCreated;
            _service = service;
            _rowState = RowState.UNCHANGED;
        }

        public MaterialClpGhsDto(int materialId, byte codeId, IService service)
        {
            MaterialId = materialId;
            CodeId = codeId;
            _service = service;
            _rowState = RowState.ADDED;
            ChangeState(_rowState);
        }

        private void ChangeState(RowState state)
        {
            _rowState = _rowState == RowState.UNCHANGED ? state : _rowState;
            if (_service != null)
                _service.Modify(_rowState);
        }

        public RowState GetRowState => _rowState;

        public void AcceptChanges()
        {
            _rowState = RowState.UNCHANGED;
        }

    }
}
