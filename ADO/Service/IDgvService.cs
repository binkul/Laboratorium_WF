namespace Laboratorium.ADO.Service
{
    public interface IDgvService
    {
        bool IsModified();
        void PrepareData();
        bool Save();
        bool Delete(long id, long tmpId);
        void AddNew(int id);
        void SynchronizeData(int LaboId);
        void AcceptAllChanges();
    }
}
