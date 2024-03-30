using Laboratorium.ADO.DTO;
using Laboratorium.Commons;
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
        private Font FONT_11 = new Font("Microsoft Sans Serif", 11, FontStyle.Bold);
        private Font FONT_12 = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
        private Font FONT_14 = new Font("Microsoft Sans Serif", 14, FontStyle.Bold);


        private readonly string connectionString = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        private readonly SqlConnection _connection;
        private readonly LabBookService _service;
        private UserDto _user;
        private bool _loginOk = false;

        public DataGridView GetDgvLabo => DgvLabo;
        public BindingNavigator GetNavigatorLabo => BindingNavigatorLabo;
        public ComboBox GetCmbGlossClass => CmbGlossClass;
        public ComboBox GetCmbContrastClass => CmbContrastClass;
        public ComboBox GetCmbScrubClass => CmbScrubClass;
        public ComboBox GetCmbVocClass => CmbVocClass;
        public Button GetBtnFilterCancel => BtnFilterCancel;
        public Button GetBtnFilterProject => BtnFilterByProject;
        public Button GetBtnProjectChange => BtnChangeProject;
        public TextBox GetTxtTitle => TxtTitle;
        public TextBox GetTxtFilerNumD => TxtFilterNumD;
        public TextBox GetTxtFilerTitle => TxtFilterTitle;
        public TextBox GetTxtFilerUser => TxtFilterUser;
        public TextBox GetTxtScrubBrush => TxtScrubBrush;
        public TextBox GetTxtScrubSponge => TxtScrubSponge;
        public TextBox GetTxtScrubComment => TxtScrubComment;
        public TextBox GetTxtContrastComment => TxtContrastComment;
        public TextBox GetTxtGloss20 => TxtGloss20;
        public TextBox GetTxtGloss60 => TxtGloss60;
        public TextBox GetTxtGloss85 => TxtGloss85;
        public TextBox GetTxtGlossComment => TxtGlossComment;
        public TextBox GetTxtVoc => TxtVoc;
        public TextBox GetTxtYield => TxtYield;
        public TextBox GetTxtAdhesion => TxtAdhesion;
        public TextBox GetTxtFlow => TxtFlow;
        public TextBox GetTxtSpill => TxtSpill;
        public TextBox GetTxtDryI => TxtDryI;
        public TextBox GetTxtDryII => TxtDryII;
        public TextBox GetTxtDryIII => TxtDryIII;
        public TextBox GetTxtDryIV => TxtDryIV;
        public TextBox GetTxtDryV => TxtDryV;
        public Label GetLblDateCreated => LblDateCreated;
        public Label GetLblDateModified => LblDateModified;
        public Label GetLblNrD => LblNrD;
        public Label GetLblProject => LblProject;

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

            //Rectangle tmp = Screen.FromControl(this).Bounds;
            //Width = tmp.Width - 100;
            //Height = tmp.Height - 200;
            //CenterToScreen();

            _service.PrepareAllData();
            _service.LoadFormData();
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
            LblDateCreated.Font = FONT_11;
            LblDateModified.Font = FONT_11;
            LblNrD.Font = FONT_14;
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
            BtnFilterByProject.Text = CommonData.ALL_DATA_PL;
            _service.SetFilter();
        }

        private void BtnFilterByProject_Click(object sender, EventArgs e)
        {
            _service.FilterByProject();
        }

        private void BtnChangeProject_Click(object sender, EventArgs e)
        {
            _service.ChangeProject();
        }
    }
}
