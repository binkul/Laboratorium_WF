namespace Laboratorium.ADO.DTO
{
    public class ScrubClassDto
    {
        public byte Id { get; }
        public string NamePl { get; }
        public string NameEn { get; }

        public ScrubClassDto(byte id, string namePl, string nameEn)
        {
            Id = id;
            NamePl = namePl;
            NameEn = nameEn;
        }
    }
}
