using System.Windows.Forms;

namespace Laboratorium.ADO.Service
{
    public interface ILoadService
    {
        DialogResult FormClose(FormClosingEventArgs e);
        void PrepareAllData();
        bool Save();
    }
}
