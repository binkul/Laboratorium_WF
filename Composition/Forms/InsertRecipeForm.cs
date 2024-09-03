using Laboratorium.ADO.DTO;
using Laboratorium.Composition.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Laboratorium.Composition.Forms
{
    public partial class InsertRecipeForm : Form
    {
        private readonly IList<LaboDto> _laboList;
        private InsertRecipeService _service;
        public bool Ok = false;

        public InsertRecipeForm(IList<LaboDto> laboList)
        {
            _laboList = laboList;
            InitializeComponent();
        }

        public DataGridView GetDgvLabo => DgvLabo;
        public TextBox GetTxtFilterNumber => TxtFindNumber;
        public TextBox GetTxtFilterName => TxtFindName;
        public LaboDto Result => _service.GetResult;

        private void InsertRecipeForm_Load(object sender, System.EventArgs e)
        {
            _service = new InsertRecipeService(this, _laboList);
            
            _service.PrepareAllData();
            _service.LoadFormData();
        }

        private void InsertRecipeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _service.FormClose(e);
        }

        private void DgvLabo_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            _service.ColumnWidthChanged();
        }

        private void TxtFindNumber_TextChanged(object sender, System.EventArgs e)
        {
            string wzor = "^[0-9]+$";
            Regex wzorzec = new Regex(wzor);

            if (!wzorzec.IsMatch(TxtFindNumber.Text) && TxtFindNumber.Text.Length > 0)
            {
                MessageBox.Show("Wprowadzona wartość nie jest liczbą '" + TxtFindNumber.Text + "'",
                    "Błąd wartości", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                _service.SetFiltration();
            }
        }

        private void TxtFindNumber_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TxtFindName_TextChanged(object sender, EventArgs e)
        {
            _service.SetFiltration();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            Ok = true;
            Close();
        }
    }
}
