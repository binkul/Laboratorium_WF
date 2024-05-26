﻿using System.Collections.Generic;

namespace Laboratorium.ADO.SqlDataConstant
{
    public static class SqlDelete
    {
        public static readonly Dictionary<SqlIndex, string> Delete = new Dictionary<SqlIndex, string>
        {
            {SqlIndex.LaboIndex, ""},
            {SqlIndex.LaboBasicIndex, "Delete From Konkurencja.dbo.LaboDataBasic Where id=" },
            {SqlIndex.LaboNormTestIndex, "Delete From Konkurencja.dbo.LaboDataNorm Where id=" },
            {SqlIndex.LaboViscosityIndex, "Delete From Konkurencja.dbo.LaboDataViscosity Where id=" },
            {SqlIndex.LaboViscosityColIndex, "Delete From Konkurencja.dbo.LaboDataViscosityCol Where labo_id=" },
            {SqlIndex.LaboContrastIndex, "Delete From Konkurencja.dbo.LaboDataContrast Where id=" }
        };
    }
}
