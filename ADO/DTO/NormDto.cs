using System.Collections.Generic;

namespace Laboratorium.ADO.DTO
{
    public class NormDto
    {
        public short Id { get; }
        public string NamePl { get; }
        public string NameEn { get; }
        public string Group { get; }
        public string Description { get; }
        public byte Position { get; }
        public IList<NormDetailDto> Details { get; set; } = new List<NormDetailDto>();


        public NormDto(short id, string namePl, string nameEn, string group, string description, byte position)
        {
            Id = id;
            NamePl = namePl;
            NameEn = nameEn;
            Group = group;
            Description = description;
            Position = position;
        }
    }
}
