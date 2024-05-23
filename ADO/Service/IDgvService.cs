using System.Windows.Forms;

namespace Laboratorium.ADO.Service
{
    public interface IDgvService
    {
        bool Modify();
        void PrepareData();
        bool Save();
        bool Delete(long id, long tmpId);
        void AddNew(int number);
        void SynchronizeData(int LaboId);
    }
}
