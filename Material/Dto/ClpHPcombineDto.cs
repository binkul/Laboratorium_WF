namespace Laboratorium.Material.Dto
{
    public class ClpHPcombineDto
    {
        public int MaterialId { get; set; }
        public string ClassClp { get; set; }
        public string CodeClp { get; set; }
        public string DescriptionClp { get; set; }

        public ClpHPcombineDto(int materialId, string classClp, string codeClp, string descriptionClp)
        {
            MaterialId = materialId;
            ClassClp = classClp;
            CodeClp = codeClp;
            DescriptionClp = descriptionClp;
        }
    }
}
