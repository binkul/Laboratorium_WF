using System.Collections.Generic;

namespace Laboratorium.ADO.SqlDataConstant
{
    public static class SqlRead
    {
        public static readonly Dictionary<SqlIndex, string> Read = new Dictionary<SqlIndex, string>
        {
            {SqlIndex.LaboIndex, "Select Numer_d, Tytuł, Data, DateUpdated, ProjectId, Cel, UwagiWnioski, CAST(ROUND(Gestosc, 3) as numeric(7, 4)) as density, Observation, IsDeleted, UserId From Konkurencja.dbo.DoswTytul Order By Numer_d"},
            {SqlIndex.ContrastClassIndex, "Select id, name_pl, name_en From Konkurencja.dbo.CmbClassContrast order by id" },
            {SqlIndex.GlossClassIndex, "Select id, name_pl, name_en From Konkurencja.dbo.CmbClassGloss order by id" },
            {SqlIndex.ScrubClassIndex, "Select id, name_pl, name_en From Konkurencja.dbo.CmbClassScrub order by id" },
            {SqlIndex.VocClassIndex, "Select id, name_pl From Konkurencja.dbo.CmbClassVoc order by id" },
            {SqlIndex.LaboBasicIndex, "Select id, labo_id, gloss_20, gloss_60, gloss_85, gloss_class, gloss_comment, scrub_brush, scrub_sponge, scrub_class, scrub_comment, contrast_class, " +
                "contrast_comment, voc, voc_class, yield, adhesion, flow, spill, drying_I, drying_II, drying_III, drying_IV, drying_V, date_updated From Konkurencja.dbo.LaboDataBasic Order By labo_id" },
            {SqlIndex.UserIndex, "Select id, name, surname, e_mail, [login], permission, identifier, active, date_created From Konkurencja.dbo.LaboUsers" },
            {SqlIndex.ProjectIndex, "Select id, name, comments, is_archive, is_labo, is_auction, local_disc, [date], ovner From Konkurencja.dbo.Project Order By Id" },
            {SqlIndex.ProjectSubCatIndex, "Select id, project_id, name, date_created From Konkurencja.dbo.ProjectSubCategory Order By project_id, name" },
        };

        public static readonly Dictionary<SqlIndex, string> ReadByName = new Dictionary<SqlIndex, string>
        {
            {SqlIndex.UserIndex, "Select id, name, surname, e_mail, login, permission, identifier, active, date_created from Konkurencja.dbo.LaboUsers Where login = 'XXXX' and password = 'YYYY'"},
        };
    }
}
