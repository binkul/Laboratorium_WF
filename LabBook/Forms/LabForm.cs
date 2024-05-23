using Laboratorium.ADO.DTO;
using Laboratorium.LabBook.Service;
using Laboratorium.Login.Forms;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
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
        private LabBookService _service;
        private UserDto _user;
        private bool _loginOk = false;
        private bool _init = true;

        public MenuStrip GetMainMenu => LabMainMenuStrip;
        public ToolStripMenuItem GetNormMenu => NormsToolStripMenuItem;
        public DataGridView GetDgvLabo => DgvLabo;
        public DataGridView GetDgvViscosity => DgvViscosity;
        public DataGridView GetDgvContrast => DgvContrast;
        public DataGridView GetDgvNormTest => DgvNormTest;
        public BindingNavigator GetNavigatorLabo => BindingNavigatorLabo;
        public ComboBox GetCmbGlossClass => CmbGlossClass;
        public ComboBox GetCmbContrastClass => CmbContrastClass;
        public ComboBox GetCmbScrubClass => CmbScrubClass;
        public ComboBox GetCmbVocClass => CmbVocClass;
        public Button GetBtnFilterCancel => BtnFilterCancel;
        public Button GetBtnFilterProject => BtnFilterByProject;
        public Button GetBtnProjectChange => BtnChangeProject;
        public Button GetBtnDelete => BtnDelete;
        public Button GetBtnUp => BtnUp;
        public Button GetBtnDown => BtnDown;
        public TextBox GetTxtTitle => TxtTitle;
        public TextBox GetTxtObservation => TxtObservation;
        public TextBox GetTxtConclusion => TxtConclusion;
        public TextBox GetTxtFilterNumD => TxtFilterNumD;
        public TextBox GetTxtFilterTitle => TxtFilterTitle;
        public TextBox GetTxtFilterUser => TxtFilterUser;
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
        public TextBox GetTxtYieldFormula => TxtYieldFormula;
        public TextBox GetTxtAdhesion => TxtAdhesion;
        public TextBox GetTxtFlow => TxtFlow;
        public TextBox GetTxtSpill => TxtSpill;
        public TextBox GetTxtDryI => TxtDryI;
        public TextBox GetTxtDryII => TxtDryII;
        public TextBox GetTxtDryIII => TxtDryIII;
        public TextBox GetTxtDryIV => TxtDryIV;
        public TextBox GetTxtDryV => TxtDryV;
        public TabPage GetPageBasic => TbBasicData;
        public Label GetLblDateCreated => LblDateCreated;
        public Label GetLblDateModified => LblDateModified;
        public Label GetLblNrD => LblNrD;
        public Label GetLblProject => LblProject;

        public LabForm()
        {
            InitializeComponent();
            _connection = new SqlConnection(connectionString);
        }

        public bool Init => _init;

        public void ActivateSave(bool state)
        {
            if (_init)
                return;
            BtnSave.Enabled = state;   
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
            else
            {
                _service = new LabBookService(_connection, _user, this);
            }

            ToolTip toolTip_1 = new ToolTip();
            toolTip_1.SetToolTip(BtnAdd, "Dodaj pojedynczy");
            ToolTip toolTip_2 = new ToolTip();
            toolTip_2.SetToolTip(BtnAddMany, "Dodaj wiele");

            _service.PrepareAllData();
            _service.LoadFormData();

            _init = false;
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

        private void TxtGloss20_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextBox box = (TextBox)sender;
            string text = box.Text;
            string separator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            string wzor = (separator == ".") ? @"^[0-9]*\" + separator + "?[0-9]*$"
                                             : "^[0-9]*" + separator + "?[0-9]*$";
            Regex wzorzec = new Regex(wzor);

            if (!wzorzec.IsMatch(text) && text.Length > 0)
            {
                MessageBox.Show("Wprowadzona wartość nie jest liczbą '" + text + "'",
                    "Błąd wartości", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        private void TxtFilterNumD_TextChanged(object sender, EventArgs e)
        {
            _service.SetFilter(0);
        }

        private void BtnFilterCancel_Click(object sender, EventArgs e)
        {
            _service.ClearFilter();
        }

        private void BtnFilterByProject_Click(object sender, EventArgs e)
        {
            _service.FilterByProject();
        }

        private void BtnChangeProject_Click(object sender, EventArgs e)
        {
            _service.ChangeProject();
        }

        private void StdXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            int nr = Convert.ToInt32(item.Tag);
            _service.ChangeViscosityProfile(nr);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            _service.Save();
        }

        private void TabLabo_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl control = (TabControl)sender;
            if (control.SelectedTab.Name == TbLabBook.Name)
            {
                BtnAdd.Enabled = true;
                BtnAddMany.Enabled = true;
            }
            else
            {
                BtnAdd.Enabled = false;
                BtnAddMany.Enabled = false;
            }

            if (control.SelectedTab.Name == TbContrast.Name)
            {
                ApplicatorToolStripMenuItem.Enabled = true;
            }
            else
            {
                ApplicatorToolStripMenuItem.Enabled= false;
            }

            if (control.SelectedTab.Name == TbTests.Name)
            {
                NormsToolStripMenuItem.Enabled = true;
            }
            else
            {
                NormsToolStripMenuItem.Enabled = false;
            }

            if (control.SelectedTab.Name == TbContrast.Name || control.SelectedTab.Name == TbViscosity.Name || control.SelectedTab.Name == TbTests.Name)
            {
                BtnUp.Enabled = true;
                BtnDown.Enabled = true;
            }
            else
            {
                BtnUp.Enabled = false;
                BtnDown.Enabled = false;
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            _service.AddOneLabBook();
        }

        private void BtnAddMany_Click(object sender, EventArgs e)
        {
            _service.AddSeriesLabBooks();
        }

        private void ApplicatorInsertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = (ToolStripMenuItem)sender;
            int tag = menu.Tag != null ? Convert.ToInt32(menu.Tag) : -1;
            _service.ApplicatorInsert(tag);
        }

        private void ApplicatorStdInsertstandardoweToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _service.StandardApplicatorInsert();
        }


        #region DataGridView Events

        private void DgvContrast_SizeChanged(object sender, EventArgs e)
        {
            if (_service != null)
                _service.ResizeContrastColumn();
        }

        private void DgvLabo_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            _service.ResizeLaboColumn(e);
        }

        private void DgvLabo_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            _service.BrightForeColorInDeleted(e);
        }

        private void DgvLabo_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            _service.IconInCellPainting(e);
        }

        private void DgvViscosity_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            _service.DefaultValuesForViscosity(e);
        }

        private void DgvNormTest_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            _service.PaintHeaderForNormTest(e);
        }

        private void DgvDeleteButton_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            _service.DeleteRow(sender, e);
        }

        #endregion

    }
}
