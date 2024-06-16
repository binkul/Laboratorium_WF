using Laboratorium.ADO.Service;
using System;

namespace Laboratorium.ADO.DTO
{
    public class MaterialClpGhsDto
    {
        public int MaterialId { get; set; }
        public byte CodeId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Today;

        public MaterialClpGhsDto(int materialId, byte codeId, DateTime dateCreated)
        {
            MaterialId = materialId;
            CodeId = codeId;
            DateCreated = dateCreated;
        }
    }
}
