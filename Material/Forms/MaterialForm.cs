using Laboratorium.ADO.DTO;
using Laboratorium.Material.Service;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Laboratorium.Material.Forms
{
    public partial class MaterialForm : Form
    {
        private MaterialService _service;
        private readonly SqlConnection _connection;
        private UserDto _user;
        private bool _init = true;

        public BindingNavigator GetBindingNavigator => BindingNavigatorMaterial;
        public DataGridView GetDgvMaterial => DgvMaterial;
        public TextBox GetTxtName => TxtName;
        public Label GetLblDateCreated => LblDateCreated;


        public MaterialForm(SqlConnection connection, UserDto user)
        {
            InitializeComponent();
            _connection = connection;
            _user = user;
        }

        public bool Init => _init;

        private void MaterialForm_Load(object sender, EventArgs e)
        {
            _service = new MaterialService(_connection, _user, this);
            _service.PrepareAllData();
            _service.LoadFormData();

            _init = false;
        }

        private void MaterialForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _service.FormClose(e);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {

        }
    }
}
