using System;

namespace Laboratorium.ADO.DTO
{
    public class CmbClpHcodeDto
    {
        public int Id { get; }
        public string ClassClp { get; }
        public string Code { get; }
        public string Description { get; }
        public int Ordering { get; }
        public byte GhsId { get; }
        public string GhsName { get; }
        public byte SignalId { get; }
        public string Signal { get; }
        public DateTime DateCreated { get; }

        public CmbClpHcodeDto(byte id, string classClp, string code, string description, int ordering, byte ghsId, string ghsName, 
            byte signalId, string signal, DateTime dateCreated)
        {
            Id = id;
            ClassClp = classClp;
            Code = code;
            Description = description;
            Ordering = ordering;
            GhsId = ghsId;
            GhsName = ghsName;
            SignalId = signalId;
            Signal = signal;
            DateCreated = dateCreated;
        }
    }
}
