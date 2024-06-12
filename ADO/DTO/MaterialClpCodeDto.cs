using Laboratorium.ADO.Service;
using System;

namespace Laboratorium.ADO.DTO
{
    public class MaterialClpCodeDto
    {
        public bool ClpType { get; set; }
        public int MaterialId { get; set; }
        public short CodeId { get; set; }
        public string ClassClp { get; set; }
        public string CodeClp { get; set; }
        public string DescriptionClp { get; set; }
        public string Comment { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Today;
        private RowState _rowState;
        private readonly IService _service;

        public MaterialClpCodeDto(bool clpType, int materialId, short codeId, string classClp, string codeClp, string descriptionClp, 
            string comment, DateTime dateCreated, IService service)
        {
            ClpType = clpType;
            MaterialId = materialId;
            CodeId = codeId;
            ClassClp = classClp;
            CodeClp = codeClp;
            DescriptionClp = descriptionClp;
            Comment = comment;
            DateCreated = dateCreated;
            _service = service;
            _rowState = RowState.UNCHANGED;
        }

        public MaterialClpCodeDto(bool clpType, int materialId, short codeId, string classClp, string codeClp, string descriptionClp, 
            string comment, IService service)
        {
            ClpType = clpType;
            MaterialId = materialId;
            CodeId = codeId;
            ClassClp = classClp;
            CodeClp = codeClp;
            DescriptionClp = descriptionClp;
            Comment = comment;
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
