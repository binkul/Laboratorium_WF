namespace Laboratorium.ADO.DTO
{
    public  class CmbUnitDto
    {
        public byte Id { get; }
        public string NamePl { get; }
        public string Description { get; }

        public CmbUnitDto(byte id, string namePl, string description)
        {
            Id = id;
            NamePl = namePl;
            Description = description;
        }
    }
}
