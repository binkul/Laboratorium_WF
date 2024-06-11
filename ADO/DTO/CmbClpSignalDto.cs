namespace Laboratorium.ADO.DTO
{
    public class CmbClpSignalDto
    {
        public byte Id { get; }
        public string NamePl { get; }
        public string NameEn { get; }

        public CmbClpSignalDto(byte id, string namePl, string nameEn)
        {
            Id = id;
            NamePl = namePl;
            NameEn = nameEn;
        }
    }
}
