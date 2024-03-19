using Laboratorium.ADO.DTO;
using Laboratorium.LabBook.Service;
using Laboratorium.Login.Forms;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Laboratorium.LabBook.Forms
{
    public partial class LabForm : Form
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        private readonly SqlConnection _connection;
        private readonly LabBookService _service;
        private UserDto _user;
        private bool _loginOk = false;

        public DataGridView GetDgvLabo => DgvLabo;
        public BindingNavigator GetNavigatorLabo => BindingNavigatorLabo;

        public LabForm()
        {
            InitializeComponent();
            _connection = new SqlConnection(connectionString);
            _service = new LabBookService(_connection, _user, this);
        }

        private void LabForm_Load(object sender, EventArgs e)
        {
            using (LoginForm form = new LoginForm(_connection))
            {
                form.ShowDialog();
                _user = form.User;
                _loginOk = form.LoginOk;
            }

            if (!_loginOk)
                Close();

            Rectangle tmp = Screen.FromControl(this).Bounds;
            Width = tmp.Width - 100;
            Height = tmp.Height - 200;
            CenterToScreen();

            _service.PrepareAllData();
        }

        private void LabForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _service.FormClose(e);
        }
    }
}
