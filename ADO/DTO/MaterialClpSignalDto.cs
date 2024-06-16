using System;

namespace Laboratorium.ADO.DTO
{
    public class MaterialClpSignalDto
    {
        public int MaterialId { get; set; }
        public byte CodeId { get; set; }
        public string NamePl { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Today;

        public MaterialClpSignalDto(int materialId, byte codeId, string namePl, DateTime dateCreated)
        {
            MaterialId = materialId;
            CodeId = codeId;
            NamePl = namePl;
            DateCreated = dateCreated;
        }
    }
}
