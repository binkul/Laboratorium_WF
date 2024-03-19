using System.Collections.Generic;

namespace Laboratorium.ADO.SqlDataConstant
{
    public static class SqlRead
    {
        public static readonly Dictionary<SqlIndex, string> Read = new Dictionary<SqlIndex, string>
        {
            {SqlIndex.LaboIndex, "Select Numer_d, Tytuł, Data, DateUpdated, Cykl, Cel, UwagiWnioski, CAST(ROUND(Gestosc, 3) as numeric(7, 4)) as density, Observation, IsDeleted From Konkurencja.dbo.DoswTytul Order By Numer_d"},
        };
    }
}
