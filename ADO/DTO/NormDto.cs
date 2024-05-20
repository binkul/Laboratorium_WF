using System.Collections.Generic;

namespace Laboratorium.ADO.DTO
{
    public class NormDto
    {
        public short Id { get; }
        public string NamePl { get; }
        public string NameEn { get; }
        public string Description { get; }
        public byte Position { get; }
        public string Group { get; }
        public byte GroupId { get; }
        public IList<NormDetailDto> Details { get; set; } = new List<NormDetailDto>();

        public NormDto(short id, string namePl, string nameEn, string description, byte position, string group, byte groupId)
        {
            Id = id;
            NamePl = namePl;
            NameEn = nameEn;
            Description = description;
            Position = position;
            Group = group;
            GroupId = groupId;
        }
    }
}
