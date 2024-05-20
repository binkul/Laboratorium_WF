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
            {SqlIndex.LaboNormTestIndex, "Select DATEDIFF(DAY, date_created, date_updated) as [day], id, labo_id, position, norm, [description], requirement, result, substarte, comment, date_created, date_updated " +
                "From Konkurencja.dbo.LaboDataNorm Order By labo_id, position" },
            {SqlIndex.LaboContrastIndex, "Select id, labo_id, is_deleted, applicator_name, position, substrate, contrast, tw, sp, comments, date_created, date_updated From Konkurencja.dbo.LaboDataContrast Order By labo_id, position" },
            {SqlIndex.ContrastClassIndex, "Select id, name_pl, name_en From Konkurencja.dbo.CmbClassContrast order by id" },
            {SqlIndex.LaboViscosityIndex, "Select DATEDIFF(DAY, date_created, date_updated) as [day], id, labo_id, to_compare, ROUND(pH, 2) as pH, temp, brook_1, brook_5, brook_10, brook_20, brook_30, " +
                "brook_40, brook_50, brook_60, brook_70, brook_80, brook_90, brook_100, brook_disc, brook_comment, brook_x_vis, brook_x_rpm, brook_x_disc, krebs, krebs_comment, ici, ici_disc, " +
                "ici_comment, date_created, date_updated From Konkurencja.dbo.LaboDataViscosity Order By date_created, [day], id" },
            {SqlIndex.LaboViscosityColIndex, "Select labo_id, [type], [columns] From Konkurencja.dbo.LaboDataViscosityCol" },
            {SqlIndex.GlossClassIndex, "Select id, name_pl, name_en From Konkurencja.dbo.CmbClassGloss order by id" },
            {SqlIndex.ScrubClassIndex, "Select id, name_pl, name_en From Konkurencja.dbo.CmbClassScrub order by id" },
            {SqlIndex.VocClassIndex, "Select id, name_pl From Konkurencja.dbo.CmbClassVoc order by id" },
            {SqlIndex.NormIndex, "Select n.id, n.name_pl, n.name_en, n.[description], n.position, g.name_pl as group_pl, n.group_id from Konkurencja.dbo.CmbNorm n Left Join Konkurencja.dbo.CmbNormGroup g on n.group_id=g.id Order by g.name_pl, n.position" },
            {SqlIndex.NormDetailIndex, "Select id, norm_id, substrate, detail From Konkurencja.dbo.CmbNormDetail Order By norm_id" },
            {SqlIndex.UserIndex, "Select id, name, surname, e_mail, [login], permission, identifier, active, date_created From Konkurencja.dbo.LaboUsers" },
            {SqlIndex.ProjectIndex, "Select id, name, comments, is_archive, is_labo, is_auction, local_disc, [date], ovner From Konkurencja.dbo.Project Order By Id" },
            {SqlIndex.ProjectSubCatIndex, "Select id, project_id, name, date_created From Konkurencja.dbo.ProjectSubCategory Order By project_id, name" },
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
        };
    }
}
