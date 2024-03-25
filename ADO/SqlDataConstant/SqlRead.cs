using System.Collections.Generic;

namespace Laboratorium.ADO.SqlDataConstant
{
    public static class SqlRead
    {
        public static readonly Dictionary<SqlIndex, string> Read = new Dictionary<SqlIndex, string>
        {
            {SqlIndex.LaboIndex, "Select Numer_d, Tytuł, Data, DateUpdated, Cykl, Cel, UwagiWnioski, CAST(ROUND(Gestosc, 3) as numeric(7, 4)) as density, Observation, IsDeleted, UserId From Konkurencja.dbo.DoswTytul Order By Numer_d"},
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
