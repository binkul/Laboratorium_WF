using System.Data;

namespace Laboratorium.ADO.Repository
{
    public interface IExtendedCRUD<T> : IBasicCRUD<T>
    {
        void UpdateRow(DataRow row);
        bool ExistById(long id);
        bool ExistByName(string name);
        int CountByName(string name);
    }
}
