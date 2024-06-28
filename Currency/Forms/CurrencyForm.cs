using Laboratorium.Currency.Service;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Laboratorium.Currency.Forms
{
    public partial class CurrencyForm : Form
    {
        private readonly CurrencyService _service;
        private bool _init = true;
        public DataGridView GetDgvCurrency => DgvCurrency;

        public CurrencyForm(SqlConnection connection)
        {
            InitializeComponent();
            _service = new CurrencyService(connection, this);
        }

        public void ActivateSave(bool activate) => BtnSave.Enabled = activate;
        
        public bool Init => _init;

        private void CurrencyForm_Load(object sender, EventArgs e)
        {
            _service.PrepareAllData();
            _service.LoadFormData();

            _init = false;
        }

        private void CurrencyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _service.FormClose(e);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            _service.Delete();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            _service.Save();
        }

        private void DgvCurrency_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (Init)
                return;

            ActivateSave(true);
        }

        private void DgvCurrency_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Nieprawidłowa wartość dla kursu waluty. Wprowadzona wartość musi być liczbą dodatnią niezerową.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void BtnAddNew_Click(object sender, EventArgs e)
        {
            _service.AddNew();
        }
    }
}
