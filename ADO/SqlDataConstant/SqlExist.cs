using System.Collections.Generic;

namespace Laboratorium.ADO.SqlDataConstant
{
    public static class SqlExist
    {
        public static readonly Dictionary<SqlIndex, string> ExistById = new Dictionary<SqlIndex, string>
        {
            {SqlIndex.LaboIndex, ""},
            {SqlIndex.LaboBasicIndex, "Select Count(*) as amount From Konkurencja.dbo.LaboDataBasic Where labo_id="},
            {SqlIndex.UserIndex, "" },
        };

        public static readonly Dictionary<SqlIndex, string> ExistByName = new Dictionary<SqlIndex, string>
        {
            {SqlIndex.LaboIndex, "" },
            {SqlIndex.UserIndex, "Select count(*) as exist From Konkurencja.dbo.LaboUsers Where login='XXXX'"},
        };

    }
}
