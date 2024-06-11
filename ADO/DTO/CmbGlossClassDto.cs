namespace Laboratorium.ADO.DTO
{
    public class CmbGlossClassDto
    {
        public byte Id { get; }
        public string NamePl { get; }
        public string NameEn { get; }

        public CmbGlossClassDto(byte id, string namePl, string nameEn)
        {
            Id = id;
            NamePl = namePl;
            NameEn = nameEn;
        }
    }
}
