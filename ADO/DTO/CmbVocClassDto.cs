namespace Laboratorium.ADO.DTO
{
    public class CmbVocClassDto
    {
        public byte Id { get; }
        public string NamePl { get; }

        public CmbVocClassDto(byte id, string namePl)
        {
            Id = id;
            NamePl = namePl;
        }
    }
}
