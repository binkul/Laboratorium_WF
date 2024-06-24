namespace Laboratorium.Material.Dto
{
    public class ClpHPcombineDto
    {
        public int MaterialId { get; set; }
        public string ClassClp { get; set; }
        public short CodeId { get; set; }
        public string CodeClp { get; set; }
        public string DescriptionClp { get; set; }
        public int Ordering { get; set; }
        public bool Type { get; set; }

        public ClpHPcombineDto(int materialId, string classClp, short codeId, string codeClp, string descriptionClp, int ordering, bool type = false)
        {
            MaterialId = materialId;
            ClassClp = classClp;
            CodeId = codeId;
            CodeClp = codeClp;
            DescriptionClp = descriptionClp;
            Ordering = ordering;
            Type = type;
        }
    }
}
