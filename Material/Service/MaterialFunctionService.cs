using Laboratorium.ADO.DTO;
using Laboratorium.ADO.Service;
using Laboratorium.Currency.Forms;
using Laboratorium.Currency.Repository;
using Laboratorium.Material.Forms;
using Laboratorium.Material.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Laboratorium.Material.Service
{
    public class MaterialFunctionService : LoadService
    {
        private const string ID = "Id";
        private const string NAME_PL = "NamePl";
        private const string GET_ROW_STATE = "GetRowState";

        private readonly IList<string> _dgvFunctionFields = new List<string> { NAME_PL };
        private const string FORM_DATA = "FunctionForm";
        private const int STD_WIDTH = 100;

        private readonly MaterialFunctionForm _form;
        private readonly SqlConnection _connection;
        private readonly CmbMaterialFunctionRepository _repository;

        private IList<CmbMaterialFunctionDto> _functionList;
        private BindingSource _functionBinding;

        protected override bool Status => _functionList.Any(i => i.GetRowState != ADO.RowState.UNCHANGED);

        public MaterialFunctionService(SqlConnection connection, MaterialFunctionForm form) : base(FORM_DATA, form)
        {
            _form = form;
            _connection = connection;
            _repository = new CmbMaterialFunctionRepository(connection);
        }

        protected override void PrepareColumns()
        {
            MaterialFunctionForm form = (MaterialFunctionForm)_baseForm;
            _fields = new Dictionary<DataGridView, IList<string>>
            {
                { form.GetDgvFunction,  _dgvFunctionFields},
            };
        }

        public override void PrepareAllData()
        {
            _functionList = _repository.GetAll();
            _functionBinding = new BindingSource();
            _functionBinding.DataSource = _functionList;

            PrepareDgvFunction();
        }

        private void PrepareDgvFunction()
        {
            DataGridView view = _form.GetDgvFunction;
            view.DataSource = _functionBinding;
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

            view.Columns[NAME_PL].HeaderText = "Funkcja";
            view.Columns[NAME_PL].DisplayIndex = 0;
            view.Columns[NAME_PL].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            view.Columns[NAME_PL].Width = _formData.ContainsKey(NAME_PL) ? (int)_formData[NAME_PL] : STD_WIDTH;
            view.Columns[NAME_PL].SortMode = DataGridViewColumnSortMode.NotSortable;

            if (view.Rows.Count > 0)
            {
                view.CurrentCell = view.Rows[_functionBinding.Position].Cells[NAME_PL];
            }
        }


        public override bool Save()
        {
            _form.GetDgvFunction.EndEdit();
            _functionBinding.EndEdit();

            var save = _functionList
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
                    int position = _functionList.IndexOf(item);
                    _functionBinding.Position = position;
                    return false;
                }
            }

            var update = _functionList
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
                    int position = _functionList.IndexOf(item);
                    _functionBinding.Position = position;
                    return false;
                }
            }

            _form.ActivateSave(false);
            return true;
        }

        private bool CheckBeforeSave(CmbMaterialFunctionDto currency)
        {
            if (string.IsNullOrEmpty(currency.NamePl))
            {
                MessageBox.Show("Nie podano nazwy funkcji. Nie można zapisaC funckji bez nazwy", "Brak nazwy", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }


        public void AddNew(DataGridViewRowEventArgs e)
        {
            e.Row.Cells[ID].Value = _functionList.Count > 0 ? _functionList.Max(i => i.Id) + 1 : 1;
        }

        public void Delete()
        {
            if (_functionBinding.Current == null)
                return;

            CmbMaterialFunctionDto current = (CmbMaterialFunctionDto)_functionBinding.Current;
            if (MessageBox.Show("Czy usunąć funkcję: '" + current.NamePl + "' z bazy danych?", "Usuwanie", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _repository.DeleteById(current.Id);
                _functionBinding.RemoveCurrent();
            }
        }
    }
}
