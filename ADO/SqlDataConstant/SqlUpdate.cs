using System.Collections.Generic;

namespace Laboratorium.ADO.SqlDataConstant
{
    public abstract class SqlUpdate
    {
        public static readonly Dictionary<SqlIndex, string> Update = new Dictionary<SqlIndex, string>
        {
            {SqlIndex.LaboIndex, ""},
            {SqlIndex.UserIndex, "Update Konkurencja.dbo.LaboUsers Set name=@name, surname=@surname, e_mail=@e_mail, [login]=@login, [password]=@password, permission=@permission, " +
                "identifier=@identifier, active=@active, date_created=@date_created) Where id=@id" }
        };
    }
}
