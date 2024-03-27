namespace Laboratorium.ADO.DTO
{
    public class VocClassDto
    {
        public byte Id { get; }
        public string NamePl { get; }

        public VocClassDto(byte id, string namePl)
        {
            Id = id;
            NamePl = namePl;
        }
    }
}
