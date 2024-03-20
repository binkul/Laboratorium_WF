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
        private const int BUTTON_SIZE = 50;
        private Font FONT_12 = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
        private Font FONT_14 = new Font("Microsoft Sans Serif", 14, FontStyle.Bold);


        private readonly string connectionString = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        private readonly SqlConnection _connection;
        private readonly LabBookService _service;
        private UserDto _user;
        private bool _loginOk = false;

        public DataGridView GetDgvLabo => DgvLabo;
        public BindingNavigator GetNavigatorLabo => BindingNavigatorLabo;
        public Button GetBtnFiltercancel => BtnFilterCancel;
        public TextBox GetTxtTitle => TxtTitle;
        public TextBox GetTxtFilerNumD => TxtFilterNumD;
        public TextBox GetTxtFilerTitle => TxtFilterTitle;
        public TextBox GetTxtFilerUser => TxtFilterUser;
        public Label GetLblDateCreated => LblDateCreated;
        public Label GetLblDateModified => LblDateModified;
        public Label GetLblNrD => LblNrD;

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
            {
                Application.Exit();
                return;
            }

            Rectangle tmp = Screen.FromControl(this).Bounds;
            Width = tmp.Width - 100;
            Height = tmp.Height - 200;
            CenterToScreen();

            _service.PrepareAllData();
        }

        private void LabForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_loginOk)
                _service.FormClose(e);
        }

        private void LabForm_Resize(object sender, EventArgs e)
        {           
            LblTitle.Font = FONT_12;
            TxtTitle.Font = FONT_12;
            LblDateCreated.Font = FONT_12;
            LblDateModified.Font = FONT_12;
            LblNrD.Font = FONT_14;

            Size size = new Size(BUTTON_SIZE, BUTTON_SIZE);
            BtnAdd.Size = size;
            BtnDelete.Size = size;
            BtnDelete.Left = BtnAdd.Left + BUTTON_SIZE + 5;
            BtnSave.Size = size;
            BtnSave.Left = BtnDelete.Left + BUTTON_SIZE + 5;
        }

        private void TxtTitle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
                SendKeys.Send("{Tab}");
            }

            else
            {
                base.OnKeyPress(e);
            }
        }

        private void DgvLabo_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            _service.ResizeLaboColumn(e);
        }

        private void TxtFilterNumD_TextChanged(object sender, EventArgs e)
        {
            _service.SetFilter();
        }

        private void BtnFilterCancel_Click(object sender, EventArgs e)
        {
            TxtFilterNumD.Text = string.Empty;
            TxtFilterTitle.Text = string.Empty;
            TxtFilterUser.Text = string.Empty;
            _service.SetFilter();
        }
    }
}
