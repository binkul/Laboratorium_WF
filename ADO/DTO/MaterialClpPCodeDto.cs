using System;

namespace Laboratorium.ADO.DTO
{
    public class MaterialClpPCodeDto
    {
        public int MaterialId { get; set; }
        public short CodeId { get; set; }
        public string ClassClp { get; set; } = "";
        public string CodeClp { get; set; }
        public string DescriptionClp { get; set; }
        public string Comment { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Today;
        public CrudState CrudState { get; set; } = CrudState.OK;


        public MaterialClpPCodeDto(int materialId, short codeId, string codeClp, string descriptionClp, string comment, DateTime dateCreated)
        {
            MaterialId = materialId;
            CodeId = codeId;
            CodeClp = codeClp;
            DescriptionClp = descriptionClp;
            Comment = comment;
            DateCreated = dateCreated;
        }
    }
}
