using Laboratorium.ADO.Service;
using System;

namespace Laboratorium.ADO.DTO
{
    public class MaterialClpGhsDto
    {
        public int MaterialId { get; set; }
        public byte CodeId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Today;
        public CrudState CrudState { get; set; } = CrudState.OK;


        public MaterialClpGhsDto(int materialId, byte codeId, DateTime dateCreated)
        {
            MaterialId = materialId;
            CodeId = codeId;
            DateCreated = dateCreated;
        }
    }
}
