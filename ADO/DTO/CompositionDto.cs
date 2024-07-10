using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboratorium.ADO.DTO
{
    public class CompositionDto
    {
        public int LaboId { get; set; }
        public int Ordering { get; set; }
        public string Component { get; set; }
        public bool IsIntermediate { get; set; } = false;
        public double Amount { get; set; }
        public string Operation { get; set; }
        public string Comment { get; set; }
        public CrudState CrudState { get; set; } = CrudState.OK;

    }
}
