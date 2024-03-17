using System;

namespace Laboratorium.ADO
{
    public class ProgramDataDto
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }
        public string ColumnTwo { get; set; }
        public string ColumnThree { get; set; }
        public string ColumnFour { get; set; }
        public double? ColumnFive { get; set; }
    }
}
