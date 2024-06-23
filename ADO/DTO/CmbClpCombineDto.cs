namespace Laboratorium.ADO.DTO
{
    public class CmbClpCombineDto
    {
        public short Id { get; set; }
        public string ClassName { get; set; }
        public string Code { get; set; }
        public string Descritption { get; set; }
        public int Ordering { get; set; }
        public string SignalWord { get; set; }

        public CmbClpCombineDto(short id, string className, string code, string descritption, int ordering, string signalWord)
        {
            Id = id;
            ClassName = className;
            Code = code;
            Descritption = descritption;
            Ordering = ordering;
            SignalWord = signalWord;
        }
    }
}
