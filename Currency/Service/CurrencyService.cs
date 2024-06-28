using Laboratorium.ADO.DTO;
using Laboratorium.ADO.Repository;
using Laboratorium.ADO.Service;
using Laboratorium.Currency.Forms;
using Laboratorium.Currency.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Laboratorium.Currency.Service
{
    public class CurrencyService : LoadService
    {
        #region DTO-s fields for DGV column

        private const string ID = "Id";
        private const string NAME = "Name";
        private const string CURRENCY = "Currency";
        private const string RATE = "Rate";
        private const string GET_ROW_STATE = "GetRowState";

        #endregion

        private readonly IList<string> _dgvCurrencyFields = new List<string> { NAME, CURRENCY, RATE };
        private const string FORM_DATA = "CurrencyForm";
        private const int STD_WIDTH = 100;

        private readonly CurrencyForm _form;
        private readonly SqlConnection _connection;
        private readonly IBasicCRUD<CmbCurrencyDto> _repository;
        private IList<CmbCurrencyDto> _currencyList;
        private BindingSource _currencyBinding;

        public CurrencyService(SqlConnection connection, CurrencyForm form) : base(FORM_DATA, form)
        {
            _form = form;
            _connection = connection;

            _repository = new CmbCurrencyRepository(_connection);
        }

        protected override bool Status => _currencyList.Any(i => i.GetRowState != ADO.RowState.UNCHANGED);

        protected override void PrepareColumns()
        {
            CurrencyForm form = (CurrencyForm)_baseForm;
            _fields = new Dictionary<DataGridView, IList<string>>
            {
                { form.GetDgvCurrency,  _dgvCurrencyFields},
            };
        }

        public override void PrepareAllData()
        {
            _currencyList = _repository.GetAll();
            _currencyBinding = new BindingSource();
            _currencyBinding.DataSource = _currencyList;

            PrepareDgvCurrency();
        }

        private void PrepareDgvCurrency()
        {
            DataGridView view = _form.GetDgvCurrency;
            view.DataSource = _currencyBinding;
            view.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.RowsDefaultCellStyle.Font = new Font(view.DefaultCellStyle.Font.Name, 10, FontStyle.Regular);
            view.ColumnHeadersDefaultCellStyle.Font = new Font(view.DefaultCellStyle.Font.Name, 10, FontStyle.Bold);
            view.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            view.RowHeadersWidth = 30;
            view.DefaultCellStyle.ForeColor = Color.Black;
            view.MultiSelect = false;
            view.SelectionMode = DataGridViewSelectionMode.CellSelect;
            view.ReadOnly = false;
            view.AutoGenerateColumns = false;
            view.AllowUserToResizeRows = false;

            view.Columns.Remove(GET_ROW_STATE);
            view.Columns[ID].Visible = false;

            view.Columns[NAME].HeaderText = "Kraj";
            view.Columns[NAME].DisplayIndex = 0;
            view.Columns[NAME].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            view.Columns[NAME].Width = _formData.ContainsKey(NAME) ? (int)_formData[NAME] : STD_WIDTH;
            view.Columns[NAME].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns[CURRENCY].HeaderText = "Waluta";
            view.Columns[CURRENCY].DisplayIndex = 1;
            view.Columns[CURRENCY].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns[CURRENCY].Width = _formData.ContainsKey(CURRENCY) ? (int)_formData[CURRENCY] : STD_WIDTH;
            view.Columns[CURRENCY].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns[RATE].HeaderText = "Kurs";
            view.Columns[RATE].DisplayIndex = 2;
            view.Columns[RATE].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns[RATE].Width = _formData.ContainsKey(RATE) ? (int)_formData[RATE] : STD_WIDTH;
            view.Columns[RATE].SortMode = DataGridViewColumnSortMode.NotSortable;

            if (view.Rows.Count > 0)
            {
                view.CurrentCell = view.Rows[_currencyBinding.Position].Cells[NAME];
            }
        }

        public override bool Save()
        {
            _form.GetDgvCurrency.EndEdit();
            _currencyBinding.EndEdit();

            var save = _currencyList
                .Where(i => i.GetRowState == ADO.RowState.ADDED)
                .ToList();

            foreach (var item in save)
            {
                if (CheckBeforeSave(item))
                {
                    _repository.Save(item);
                    item.AcceptChanges();
                }
                else
                {
                    int position = _currencyList.IndexOf(item);
                    _currencyBinding.Position = position;
                    return false;
                }
            }

            var update = _currencyList
                .Where(i => i.GetRowState == ADO.RowState.MODIFIED)
                .ToList();

            foreach (var item in update)
            {
                if (CheckBeforeSave(item))
                {
                    _repository.Update(item);
                    item.AcceptChanges();
                }
                else
                {
                    int position = _currencyList.IndexOf(item);
                    _currencyBinding.Position = position;
                    return false;
                }
            }

            _form.ActivateSave(false);
            return true;
        }

        private bool CheckBeforeSave(CmbCurrencyDto currency)
        {
            if (string.IsNullOrEmpty(currency.Currency))
            {
                MessageBox.Show("Nie podano symbolu waluty. Nie można zapisać waluty bez symbolu", "Brak symbolu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (currency.Rate < 0)
            {
                MessageBox.Show("Kurs waluty nie może być ujemny. Podaj prawidłowy kurs waluty.", "Brak przelicznika", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        public void Delete()
        {
            if (_currencyBinding.Current == null)
                return;

            CmbCurrencyDto current = (CmbCurrencyDto)_currencyBinding.Current;
            if (MessageBox.Show("Czy usunąć walutę: '" + current.Currency + "' kraj '" + current.Name + "' z bazy danych?", "Usuwanie", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes )
            {
                _repository.DeleteById(current.Id);
                _currencyBinding.RemoveCurrent();
            }
        }

        public void AddNew()
        {
            byte id = Convert.ToByte(_currencyList.Count > 0 ? _currencyList.Max(i => i.Id) + 1 : 1);
            CmbCurrencyDto currency = new CmbCurrencyDto(id, "", "", 1);
            _currencyBinding.Add(currency);
            _currencyBinding.Position = _currencyBinding.Count - 1;
        }
    }
}
