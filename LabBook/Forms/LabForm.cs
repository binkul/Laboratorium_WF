using Laboratorium.ADO.DTO;
using Laboratorium.Login.Forms;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Laboratorium.LabBook.Forms
{
    public partial class LabForm : Form
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        private readonly SqlConnection _connection;
        private UserDto _user;
        private bool _loginOk = false;


        public LabForm()
        {
            InitializeComponent();
            _connection = new SqlConnection(connectionString);
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
            
        }
    }
}
