namespace Laboratorium.ADO.DTO
{
    public class ContrastClassDto
    {
        public byte Id { get; }
        public string NamePl { get; }
        public string NameEn { get; }

        public ContrastClassDto(byte id, string namePl, string nameEn)
        {
            Id = id;
            NamePl = namePl;
            NameEn = nameEn;
        }
    }
}
