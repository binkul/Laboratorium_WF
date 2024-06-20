using Laboratorium.ADO.DTO;
using Laboratorium.Material.Service;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Laboratorium.Material.Forms
{
    public partial class MaterialClpForm : Form
    {
        private readonly MaterialClpService _service;
        public MaterialClpForm(SqlConnection connection, MaterialDto material)
        {
            InitializeComponent();
            _service = new MaterialClpService(connection, material);
        }
    }
}
