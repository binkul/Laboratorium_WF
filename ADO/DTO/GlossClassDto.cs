namespace Laboratorium.ADO.DTO
{
    public class GlossClassDto
    {
        public byte Id { get; }
        public string NamePl { get; }
        public string NameEn { get; }

        public GlossClassDto(byte id, string namePl, string nameEn)
        {
            Id = id;
            NamePl = namePl;
            NameEn = nameEn;
        }
    }
}
