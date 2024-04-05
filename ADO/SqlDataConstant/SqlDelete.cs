using System.Collections.Generic;

namespace Laboratorium.ADO.SqlDataConstant
{
    public static class SqlDelete
    {
        public static readonly Dictionary<SqlIndex, string> Delete = new Dictionary<SqlIndex, string>
        {
            {SqlIndex.LaboIndex, ""},
            {SqlIndex.LaboBasicIndex, "Delete From Konkurencja.dbo.LaboDataBasic Where labo_id=" },
            {SqlIndex.LaboViscosityIndex, "Delete From Konkurencja.dbo.LaboDataViscosity Where labo_id=" },
            {SqlIndex.LaboViscosityColIndex, "Delete From Konkurencja.dbo.LaboDataViscosityCol Where labo_id=" }
        };
    }
}
