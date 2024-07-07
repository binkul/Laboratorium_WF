using Laboratorium.Compound.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Laboratorium.Compound.Forms
{
    public partial class CompoundForm : Form
    {
        private CompoundService _service;
        private readonly SqlConnection _connection;
        private bool _init = true;

        public BindingNavigator GetNavigator => BindingNavCompound;
        public DataGridView GetDgvCompound => DgvCompound;
        public Button GetBtnFilerClear => BtnFilterClear;
        public TextBox GetTxtNamePl => TxtNamePl;
        public TextBox GetTxtNameEn => TxtNameEn;
        public TextBox GetTxtShortPl => TxtShortPl;
        public TextBox GetTxtShortEn => TxtShortEn;
        public TextBox GetTxtIndeks => TxtIndeks;
        public TextBox GetTxtCas => TxtCAS;
        public TextBox GetTxtWE => TxtWE;
        public TextBox GetTxtFormula => TxtFormula;
        public TextBox GetTxtFilterName => TxtFilterName;
        public TextBox GetTxtFilterShort => TxtFilterShort;
        public TextBox GetTxtFilterCas => TxtFilerCas;
        public TextBox GetTxtFilterWe => TxtFilterWE;
        public Label GetLblDate => LblDateCreated;
        public CheckBox GetChbIsBio => ChbIsBio;
        public CheckBox GetChbFilterIsBio => ChbFiletrIsBio;

        public bool ChangedComposiotn => _service.ChangedComposition;
        public IList<int> DeletedCompounds => _service.DeletedCompounds;

        public CompoundForm(SqlConnection connection)
        {
            InitializeComponent();
            _connection = connection;
        }

        public void EnableSave(bool state)
        {
            if (_init)
                return;
            BtnSave.Enabled = state;
        }

        private void CompoundForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _service.FormClose(e);
        }

        private void CompoundForm_Load(object sender, EventArgs e)
        {
            _service = new CompoundService(_connection, this);
            _service.PrepareAllData();
            _service.LoadFormData();

            ToolTip toolTip_3 = new ToolTip();
            toolTip_3.SetToolTip(BtnSave, "Zapisz zmiany");

            _init = false;
        }

        private void TxtBox_KeyPress(object sender, KeyPressEventArgs e)
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

        private void DgvCompound_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            _service.ChangeFilterWidth();
        }

        #region Buttons

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            _service.AddNewCompound();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            _service.DeleteCompound();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            _service.Save();
        }

        #endregion

        #region Filtration
        private void TxtFilter_TextChanged(object sender, EventArgs e)
        {
            _service.SetFiltration();
        }

        private void ChbFiletrIsBio_CheckedChanged(object sender, EventArgs e)
        {
            _service.SetFiltration();
        }

        private void BtnFilterClear_Click(object sender, EventArgs e)
        {
            _service.ClearFiltrationByButton();
        }

        #endregion

    }
}
