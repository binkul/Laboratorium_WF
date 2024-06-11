namespace Laboratorium.ADO.DTO
{
    public class CmbScrubClassDto
    {
        public byte Id { get; }
        public string NamePl { get; }
        public string NameEn { get; }

        public CmbScrubClassDto(byte id, string namePl, string nameEn)
        {
            Id = id;
            NamePl = namePl;
            NameEn = nameEn;
        }
    }
}
