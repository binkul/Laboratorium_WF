using System.Collections.Generic;

namespace Laboratorium.ADO.SqlDataConstant
{
    public static class SqlSave
    {
        public static readonly Dictionary<SqlIndex, string> Save = new Dictionary<SqlIndex, string>
        {
            {SqlIndex.LaboIndex, "" },
            {SqlIndex.UserIndex, "Insert Into Konkurencja.dbo.LaboUsers(name, surname, e_mail, login, password, permission, identifier, active, date_created) " +
                                            "Output INSERTED.id Values(@name, @surname, @e_mail, @login, @password, @permission, @identifier, @active, @date_created"},
        };
    }
}
