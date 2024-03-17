using System.Collections.Generic;

namespace Laboratorium.ADO.SqlDataConstant
{
    public static class SqlRead
    {
        public static readonly Dictionary<SqlIndex, string> Read = new Dictionary<SqlIndex, string>
        {
            {SqlIndex.LaboIndex, "Select Numer_d, Data, Cykl, Tytuł, Cel, UwagiWnioski, Gestosc, Observation From Konkurencja.dbo.DoswTytul Order By Numer_d"},
        };
    }
}
