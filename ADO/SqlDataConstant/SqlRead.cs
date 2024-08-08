using System.Collections.Generic;

namespace Laboratorium.ADO.SqlDataConstant
{
    public static class SqlRead
    {
        public static readonly Dictionary<SqlIndex, string> Read = new Dictionary<SqlIndex, string>
        {
            {SqlIndex.CmbGlossClassIndex, "Select id, name_pl, name_en From Konkurencja.dbo.CmbClassGloss order by id" },
            {SqlIndex.CmbScrubClassIndex, "Select id, name_pl, name_en From Konkurencja.dbo.CmbClassScrub order by id" },
            {SqlIndex.CmbVocClassIndex, "Select id, name_pl From Konkurencja.dbo.CmbClassVoc order by id" },
            {SqlIndex.CmbContrastClassIndex, "Select id, name_pl, name_en From Konkurencja.dbo.CmbClassContrast order by id" },
            {SqlIndex.CmbMaterialFunctionIndex, "Select id, name_pl From Konkurencja.dbo.CmbMaterialFunction Order By name_pl" },
            {SqlIndex.CmbClpHcodeIndex, "Select hc.id, hc.[class], hc.code, hc.[description], hc.ordering, hc.ghs_id, ghs.[description], hc.signal_word_id, sig.name_pl, " +
                "hc.date_created from Konkurencja.dbo.CmbClpHcode hc left join Konkurencja.dbo.CmbClpGHScode ghs on hc.ghs_id=ghs.id left join Konkurencja.dbo.CmbClpSignalWord " +
                "sig on hc.signal_word_id=sig.id Order By ordering" },
            {SqlIndex.CmbClpPcodeIndex, "Select hc.id, hc.code, hc.[description], hc.ordering, hc.date_created from Konkurencja.dbo.CmbClpPcode hc Order By ordering" },
            {SqlIndex.CmbClpCombineCodeIndex, "Select cod.id, ISNULL(cod.class, '') as class, cod.code, cod.[description], cod.ordering, " +
                "CASE When cod.signal_word_id = 1 " +
                "THEN '' " +
                "ELSE sig.name_pl " +
                "END as signal, 1 as Type From Konkurencja.dbo.CmbClpHcode cod Left Join Konkurencja.dbo.CmbClpSignalWord sig on cod.signal_word_id=sig.id " +
                "Union All Select id, '' as class, code, [description], ordering, null as signal, 0 as Type From Konkurencja.dbo.CmbClpPcode\r\nOrder By ordering" },
            {SqlIndex.CmbClpSignalIndex, "Select id, name_pl, name_en From Konkurencja.dbo.CmbClpSignalWord Order By id" },           
            {SqlIndex.CmbCurrencyIndex, "Select id, name, currency, rate from Konkurencja.dbo.CmbCurrency Order By id" },
            {SqlIndex.CmbUnitIndex, "Select id, name_pl, [description] From Konkurencja.dbo.CmbUnits Order By id" },
            {SqlIndex.LaboIndex, "Select Numer_d, Tytuł, Data, DateUpdated, ProjectId, Cel, UwagiWnioski, CAST(ROUND(Gestosc, 3) as numeric(7, 4)) as density, Observation, IsDeleted, UserId From Konkurencja.dbo.DoswTytul Order By Numer_d"},
            {SqlIndex.LaboBasicIndex, "Select id, labo_id, gloss_20, gloss_60, gloss_85, gloss_class, gloss_comment, scrub_brush, scrub_sponge, scrub_class, scrub_comment, contrast_class, " +
                "contrast_comment, voc, voc_class, yield, yield_formula, adhesion, flow, spill, drying_I, drying_II, drying_III, drying_IV, drying_V, date_updated From Konkurencja.dbo.LaboDataBasic Order By labo_id" },
            {SqlIndex.LaboNormTestIndex, "Select DATEDIFF(DAY, date_created, date_updated) as [day], id, labo_id, position, norm, [description], requirement, result, substarte, comment, group_id, date_created, date_updated " +
                "From Konkurencja.dbo.LaboDataNorm Order By labo_id, position" },
            {SqlIndex.LaboContrastIndex, "Select id, labo_id, is_deleted, applicator_name, position, substrate, contrast, tw, sp, comments, date_created, date_updated From Konkurencja.dbo.LaboDataContrast Order By labo_id, position" },
            {SqlIndex.LaboViscosityIndex, "Select id, labo_id, to_compare, ROUND(pH, 2) as pH, temp, brook_1, brook_5, brook_10, brook_20, brook_30, " +
                "brook_40, brook_50, brook_60, brook_70, brook_80, brook_90, brook_100, brook_disc, brook_comment, brook_x_vis, brook_x_rpm, brook_x_disc, krebs, krebs_comment, ici, ici_disc, " +
                "ici_comment, date_created, date_updated From Konkurencja.dbo.LaboDataViscosity Order By date_created, id" },
            {SqlIndex.LaboViscosityColIndex, "Select labo_id, [type], [columns] From Konkurencja.dbo.LaboDataViscosityCol" },
            {SqlIndex.NormIndex, "Select n.id, n.name_pl, n.name_en, n.[description], n.position, g.name_pl as group_pl, n.group_id from Konkurencja.dbo.CmbNorm n Left Join Konkurencja.dbo.CmbNormGroup g on n.group_id=g.id Order by g.name_pl, n.position" },
            {SqlIndex.NormDetailIndex, "Select id, norm_id, substrate, detail From Konkurencja.dbo.CmbNormDetail Order By norm_id" },
            {SqlIndex.UserIndex, "Select id, name, surname, e_mail, [login], permission, identifier, active, date_created From Konkurencja.dbo.LaboUsers" },
            {SqlIndex.ProjectIndex, "Select id, name, comments, is_archive, is_labo, is_auction, local_disc, [date], ovner From Konkurencja.dbo.Project Order By Id" },
            {SqlIndex.ProjectSubCatIndex, "Select id, project_id, name, date_created From Konkurencja.dbo.ProjectSubCategory Order By project_id, name" },
            {SqlIndex.MaterialIndex, "Select mat.id, mat.name, mat.index_db, mat.supplier_id, mat.function_id, mat.is_intermediate, mat.is_danger, mat.is_production, mat.is_observed, mat.is_active, mat.is_package, " +
                "mat.price, mat.price_per_quantity, mat.price_transport, mat.quantity, mat.currency_id, " +
                "unit_id, CASE WHEN mat.currency_id = 1 " +
                "THEN ('-- / ' + uni.name_pl) " +
                "ELSE (cur.currency + ' / ' + uni.name_pl) " +
                "END As price_unit, " +
                "mat.density, mat.solids, mat.ash_450, mat.VOC, (CAST (VOC as varchar) + '%') As voc_per, mat.remarks, mat.date_created, mat.date_updated From Konkurencja.dbo.Material mat " +
                "left join Konkurencja.dbo.CmbCurrency cur on mat.currency_id=cur.id " +
                "left join Konkurencja.dbo.CmbUnits uni on mat.unit_id=uni.id Order by mat.id" },
            {SqlIndex.MaterialClpGhsIndex, "Select material_id, code_id, date_created From Konkurencja.dbo.MaterialClpCodeGHS Order By material_id, code_id" },
            {SqlIndex.MaterialClpHcodeIndex, "Select mat.material_id, mat.code_id, mat.comments, mat.date_created, clp.class, clp.code, clp.[description] from Konkurencja.dbo.MaterialClpCodeH mat left join Konkurencja.dbo.CmbClpHcode " +
                "clp on mat.code_id=clp.id Order By mat.material_id, clp.ordering" },
            {SqlIndex.MaterialClpPcodeIndex, "Select mat.material_id, mat.code_id, mat.comments, mat.date_created, clp.code, clp.[description] from Konkurencja.dbo.MaterialClpCodeP mat " +
                "left join Konkurencja.dbo.CmbClpPcode clp on mat.code_id=clp.id Order By mat.material_id, clp.ordering" },
            {SqlIndex.MaterialClpHPcombineIndex, "Select mat.material_id, h.class, h.code, h.[description], h.ordering As ord From Konkurencja.dbo.MaterialClpCodeH mat left join Konkurencja.dbo.CmbClpHcode h " +
                "on mat.code_id=h.id Union All Select mat.material_id, '' As class, p.code, p.[description], p.ordering As ord From Konkurencja.dbo.MaterialClpCodeP mat left join Konkurencja.dbo.CmbClpPcode p " +
                "on mat.code_id=p.id Order by material_id, ord" },
            {SqlIndex.MaterialClpSignalIndex, "Select material_id, code_id, date_created, sig.name_pl from Konkurencja.dbo.MaterialClpSignal mat left join Konkurencja.dbo.CmbClpSignalWord sig " +
                "on mat.code_id=sig.id Order by material_id" },
            {SqlIndex.MaterialCompositionIndex, "Select mat.material_id, mat.compound_Id, mat.amount_min, mat.amount_max, mat.ordering, mat.remarks, mat.date_created, com.name_pl, com.name_en, com.short_pl, " +
                "com.short_en, com.index_nr, com.cas, com.we, com.formula, com.is_bio, com.date_created From Konkurencja.dbo.MaterialComposition mat Left Join Konkurencja.dbo.MaterialCompound com " +
                "on mat.compound_id=com.id Order By mat.material_id, mat.ordering" },
            {SqlIndex.CompoundIndex, "Select id, name_pl, name_en, short_pl, short_en, index_nr, cas, we, formula, is_bio, date_created from Konkurencja.dbo.MaterialCompound Order By is_bio desc, short_pl" },
        };

        public static readonly Dictionary<SqlIndex, string> ReadByName = new Dictionary<SqlIndex, string>
        {
            {SqlIndex.LaboViscosityIndex, "Select DATEDIFF(DAY, date_created, date_updated) as [day], id, labo_id, to_compare, ROUND(pH, 2), temp, brook_1, brook_5, brook_10, brook_20, brook_30, " +
                "brook_40, brook_50, brook_60, brook_70, brook_80, brook_90, brook_100, brook_disc, brook_comment, brook_x_vis, brook_x_rpm, brook_x_disc, krebs, krebs_comment, ici, ici_disc, " +
                "ici_comment, date_created, date_updated From Konkurencja.dbo.LaboDataViscosity Where labo_id=XXXX Order By date_created, [day], id" },
            {SqlIndex.LaboViscosityColIndex, "Select labo_id, [type], [columns] From Konkurencja.dbo.LaboDataViscosityCol Where labo_id=" },
            {SqlIndex.NormDetailIndex, "Select id, norm_id, substrate, detail From Konkurencja.dbo.CmbNormDetail Where norm_id=XXXX" },
            {SqlIndex.LaboNormTestIndex, "Select DATEDIFF(DAY, date_created, date_updated) as [day], id, labo_id, position, norm, [description], requirement, result, substarte, comment, " +
                "date_created, date_updated From Konkurencja.dbo.LaboDataNorm Where labo_id=XXXX Order By position" },
            {SqlIndex.UserIndex, "Select id, name, surname, e_mail, login, permission, identifier, active, date_created from Konkurencja.dbo.LaboUsers Where login = 'XXXX' and password = 'YYYY'"},
            {SqlIndex.MaterialIndex, "Select id, name, index_db, supplier_id, function_id, is_intermediate, is_danger, is_production, is_observed, is_active, is_package, price, " +
                "price_per_quantity, price_transport, quantity, currency_id, unit_id, density, solids, ash_450, VOC, remarks, date_created, date_updated " +
                "from Konkurencja.dbo.Material Where id=" },
            {SqlIndex.MaterialClpSignalIndex, "Select mat.material_id, mat.code_id, mat.date_created, sig.name_pl from Konkurencja.dbo.MaterialClpSignal mat left join " +
                "Konkurencja.dbo.CmbClpSignalWord sig on mat.code_id=sig.id Where material_id=" },
            {SqlIndex.MaterialClpGhsIndex, "Select material_id, code_id, date_created from Konkurencja.dbo.MaterialClpCodeGHS Where material_id=XXXX Order By code_id" },
            {SqlIndex.MaterialClpHPcombineIndex, "Select math.material_id, math.code_id, codh.code, ISNULL(codh.class, '') as class, codh.[description], codh.ordering as ord, 1 as [type] From Konkurencja.dbo.MaterialClpCodeH math " +
                "Left Join Konkurencja.dbo.CmbClpHcode codh on math.code_id=codh.id Where math.material_id=XXXX " +
                "UNION ALL " +
                "Select matp.material_id, matp.code_id, codp.code, '' as class, codp.[description], codp.ordering as ord, 0 as [type] From Konkurencja.dbo.MaterialClpCodeP matp " +
                "Left Join Konkurencja.dbo.CmbClpPcode codp on matp.code_id=codp.id Where matp.material_id=XXXX Order by ord" },
            {SqlIndex.MaterialCompositionIndex, "Select compound_Id, amount_min, amount_max, ordering, remarks, date_created From Konkurencja.dbo.MaterialComposition Where material_id=XXXX Order By ordering" },
            {SqlIndex.CompoundIndex, "Select id, name_pl, name_en, short_pl, short_en, index_nr, cas, we, formula, is_bio, date_created from Konkurencja.dbo.MaterialCompound Where id=" },
            {SqlIndex.CompositionIndex, "Select labo_id, [version], ordering, material, material_id, is_intermediate, amount, operation, comment From Konkurencja.dbo.LaboComposition " +
                "Where labo_id=XXXX Order By ordering" },
        };
    }
}
