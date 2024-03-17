using System.Collections.Generic;
using System.Data;

namespace Laboratorium.ADO.Repository
{
    public interface IBasicCRUD<T>
    {
        IList<T> GetAll();
        DataTable GetAllAsTable(bool createKey = true);
        bool DeleteById(long id);
        T Save(T entity);
        T Update(T entity);
    }
}
