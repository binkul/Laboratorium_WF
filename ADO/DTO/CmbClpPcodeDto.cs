using System;

namespace Laboratorium.ADO.DTO
{
    public class CmbClpPcodeDto
    {
        public int Id { get; }
        public string Code { get; }
        public string Description { get; }
        public int Ordering { get; }
        public DateTime DateCreated { get; }

        public CmbClpPcodeDto(byte id, string code, string description, int ordering, DateTime dateCreated)
        {
            Id = id;
            Code = code;
            Description = description;
            Ordering = ordering;
            DateCreated = dateCreated;
        }
    }
}
