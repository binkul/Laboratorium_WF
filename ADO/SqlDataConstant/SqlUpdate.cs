using System.Collections.Generic;

namespace Laboratorium.ADO.SqlDataConstant
{
    public abstract class SqlUpdate
    {
        public static readonly Dictionary<SqlIndex, string> Update = new Dictionary<SqlIndex, string>
        {
            {SqlIndex.LaboIndex, ""},
        };
    }
}
