using System.Collections.Generic;

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
            {SqlIndex.LaboContrastIndex, "Delete From Konkurencja.dbo.LaboDataContrast Where id=" },
            {SqlIndex.MaterialIndex, "Delete From Konkurencja.dbo.Material Where id=" },
            {SqlIndex.MaterialClpGhsIndex, "Delete From Konkurencja.dbo.MaterialClpCodeGHS Where material_id=" },
            {SqlIndex.MaterialClpHcodeIndex, "Delete From Konkurencja.dbo.MaterialClpCodeH Where material_id=" },
            {SqlIndex.MaterialClpPcodeIndex, "Delete From Konkurencja.dbo.MaterialClpCodeP Where material_id=" },
            {SqlIndex.MaterialClpSignalIndex, "Delete From Konkurencja.dbo.MaterialClpSignal Where material_id=" },
            {SqlIndex.CmbCurrencyIndex, "Delete From Konkurencja.dbo.CmbCurrency Where id=" }
        };
    }
}
