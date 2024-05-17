using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboratorium.ADO.DTO
{
    public class NormDetailDto
    {
        public short Id { get; }

        public short NormId { get; }

        public string Substrate { get; }

        public string Detail { get; }

        public NormDetailDto(short id, short normId, string substrate, string detail)
        {
            Id = id;
            NormId = normId;
            Substrate = substrate;
            Detail = detail;
        }
    }
}
