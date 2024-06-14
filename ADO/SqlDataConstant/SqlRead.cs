using System.Collections.Generic;

namespace Laboratorium.ADO.SqlDataConstant
{
    public static class SqlRead
    {
        public static readonly Dictionary<SqlIndex, string> Read = new Dictionary<SqlIndex, string>
        {
            {SqlIndex.LaboIndex, "Select Numer_d, Tytuł, Data, DateUpdated, ProjectId, Cel, UwagiWnioski, CAST(ROUND(Gestosc, 3) as numeric(7, 4)) as density, Observation, IsDeleted, UserId From Konkurencja.dbo.DoswTytul Order By Numer_d"},
            {SqlIndex.LaboBasicIndex, "Select id, labo_id, gloss_20, gloss_60, gloss_85, gloss_class, gloss_comment, scrub_brush, scrub_sponge, scrub_class, scrub_comment, contrast_class, " +
                "contrast_comment, voc, voc_class, yield, yield_formula, adhesion, flow, spill, drying_I, drying_II, drying_III, drying_IV, drying_V, date_updated From Konkurencja.dbo.LaboDataBasic Order By labo_id" },
            {SqlIndex.LaboNormTestIndex, "Select DATEDIFF(DAY, date_created, date_updated) as [day], id, labo_id, position, norm, [description], requirement, result, substarte, comment, group_id, date_created, date_updated " +
                "From Konkurencja.dbo.LaboDataNorm Order By labo_id, position" },
            {SqlIndex.LaboContrastIndex, "Select id, labo_id, is_deleted, applicator_name, position, substrate, contrast, tw, sp, comments, date_created, date_updated From Konkurencja.dbo.LaboDataContrast Order By labo_id, position" },
            {SqlIndex.ContrastClassIndex, "Select id, name_pl, name_en From Konkurencja.dbo.CmbClassContrast order by id" },
            {SqlIndex.LaboViscosityIndex, "Select id, labo_id, to_compare, ROUND(pH, 2) as pH, temp, brook_1, brook_5, brook_10, brook_20, brook_30, " +
                "brook_40, brook_50, brook_60, brook_70, brook_80, brook_90, brook_100, brook_disc, brook_comment, brook_x_vis, brook_x_rpm, brook_x_disc, krebs, krebs_comment, ici, ici_disc, " +
                "ici_comment, date_created, date_updated From Konkurencja.dbo.LaboDataViscosity Order By date_created, id" },
            {SqlIndex.LaboViscosityColIndex, "Select labo_id, [type], [columns] From Konkurencja.dbo.LaboDataViscosityCol" },
            {SqlIndex.GlossClassIndex, "Select id, name_pl, name_en From Konkurencja.dbo.CmbClassGloss order by id" },
            {SqlIndex.ScrubClassIndex, "Select id, name_pl, name_en From Konkurencja.dbo.CmbClassScrub order by id" },
            {SqlIndex.VocClassIndex, "Select id, name_pl From Konkurencja.dbo.CmbClassVoc order by id" },
            {SqlIndex.NormIndex, "Select n.id, n.name_pl, n.name_en, n.[description], n.position, g.name_pl as group_pl, n.group_id from Konkurencja.dbo.CmbNorm n Left Join Konkurencja.dbo.CmbNormGroup g on n.group_id=g.id Order by g.name_pl, n.position" },
            {SqlIndex.NormDetailIndex, "Select id, norm_id, substrate, detail From Konkurencja.dbo.CmbNormDetail Order By norm_id" },
            {SqlIndex.UserIndex, "Select id, name, surname, e_mail, [login], permission, identifier, active, date_created From Konkurencja.dbo.LaboUsers" },
            {SqlIndex.ProjectIndex, "Select id, name, comments, is_archive, is_labo, is_auction, local_disc, [date], ovner From Konkurencja.dbo.Project Order By Id" },
            {SqlIndex.ProjectSubCatIndex, "Select id, project_id, name, date_created From Konkurencja.dbo.ProjectSubCategory Order By project_id, name" },
            {SqlIndex.MaterialIndex, "Select mat.id, name, index_db, supplier_id, function_id, is_intermediate, is_danger, is_production, is_observed, is_active, is_package, price, price_per_quantity, price_transport, quantity, currency_id, " +
                "unit_id, CASE WHEN mat.currency_id = 1 " +
                "THEN ('-- / ' + uni.name_pl) " +
                "ELSE (cur.currency + ' / ' + uni.name_pl) " +
                "END As price_unit, " +
                "density, solids, ash_450, VOC, (CAST (VOC as varchar) + '%') As voc_per, remarks, date_created, date_updated From Konkurencja.dbo.Material mat left join Konkurencja.dbo.CmbCurrency cur on mat.currency_id=cur.id " +
                "left join Konkurencja.dbo.CmbUnits uni on mat.unit_id=uni.id Order by mat.id" },
            {SqlIndex.MaterialFunctionIndex, "Select id, name_pl From Konkurencja.dbo.CmbMaterialFunction Order By name_pl" },
            {SqlIndex.ClpHcodeIndex, "Select hc.id, hc.[class], hc.code, hc.[description], hc.ordering, hc.ghs_id, ghs.[description], hc.signal_word_id, sig.name_pl, " +
                "hc.date_created from Konkurencja.dbo.CmbClpHcode hc left join Konkurencja.dbo.CmbClpGHScode ghs on hc.ghs_id=ghs.id left join Konkurencja.dbo.CmbClpSignalWord " +
                "sig on hc.signal_word_id=sig.id Order By ordering" },
            {SqlIndex.ClpPcodeIndex, "Select hc.id, hc.code, hc.[description], hc.ordering, hc.date_created from Konkurencja.dbo.CmbClpPcode hc Order By ordering" },
            {SqlIndex.CurrencyIndex, "Select id, currency, rate from Konkurencja.dbo.CmbCurrency Order By id" },
            {SqlIndex.UnitIndex, "Select id, name_pl, [description] From Konkurencja.dbo.CmbUnits Order By id" },
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
        };
    }
}
