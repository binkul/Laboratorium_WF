using Laboratorium.ADO;
using Laboratorium.ADO.DTO;
using Laboratorium.ADO.Service;
using Laboratorium.ClpData.Repository;
using Laboratorium.Commons;
using Laboratorium.Material.Forms;
using Laboratorium.Material.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Laboratorium.Material.Service
{
    public class MaterialService : IService
    {
        private const string FORM_TOP = "Form_Top";
        private const string FORM_LEFT = "Form_Left";
        private const string FORM_WIDTH = "Form_Width";
        private const string FORM_HEIGHT = "Form_Height";
        private const string FORM_DATA = "MaterialForm";
        private const int STD_WIDTH = 100;

        private readonly SqlConnection _connection;
        private readonly UserDto _user;
        private readonly MaterialForm _form;
        private readonly CmbClpHcodeRepository _codeHrepository;
        private readonly CmbClpPcodeRepository _codePrepository;
        private readonly MaterialRepository _repository;
        private IList<MaterialDto> _materialList;
        private BindingSource _materialBinding;
        public MaterialDto CurrentMaterial;

        private IDictionary<string, double> _formData = CommonFunction.LoadWindowsDataAsDictionary(FORM_DATA);

        public MaterialService(SqlConnection connection, UserDto user, MaterialForm form)
        {
            _connection = connection;
            _user = user;
            _form = form;

            _codeHrepository = new CmbClpHcodeRepository(_connection);
            _codePrepository = new CmbClpPcodeRepository(_connection);
            _repository = new MaterialRepository(_connection, this);
        }

        public void Modify(RowState state)
        {
            if (_form.Init)
            {
                return;
            }

        }

        #region Open/Close form 

        public void FormClose(FormClosingEventArgs e)
        {



            if (!e.Cancel)
            {
                SaveFormData();
            }
        }

        private void SaveFormData()
        {
            IDictionary<string, double> list = new Dictionary<string, double>();

            list.Add(FORM_TOP, _form.Top);
            list.Add(FORM_LEFT, _form.Left);
            list.Add(FORM_WIDTH, _form.Width);
            list.Add(FORM_HEIGHT, _form.Height);

            foreach (DataGridViewColumn col in _form.GetDgvMaterial.Columns)
            {
                if (col.Visible)
                {
                    string name = col.Name;
                    double width = col.Width;
                    list.Add(name, width);
                }
            }

            //string name = _form.GetDgvMaterial.Columns["Name"].Name;
            //double width = _form.GetDgvMaterial.Columns["Name"].Width;
            //list.Add(name, width);
            //name = _form.GetDgvMaterial.Columns["IsActive"].Name;
            //width = _form.GetDgvMaterial.Columns["IsActive"].Width;
            //list.Add(name, width);
            //name = _form.GetDgvMaterial.Columns["IsDanger"].Name;
            //width = _form.GetDgvMaterial.Columns["IsDanger"].Width;
            //list.Add(name, width);
            //name = _form.GetDgvMaterial.Columns["IsActive"].Name;
            //width = _form.GetDgvMaterial.Columns["IsActive"].Width;
            //list.Add(name, width);
            //name = _form.GetDgvMaterial.Columns["IsProduction"].Name;
            //width = _form.GetDgvMaterial.Columns["IsProduction"].Width;
            //list.Add(name, width);
            //name = _form.GetDgvMaterial.Columns["Price"].Name;
            //width = _form.GetDgvMaterial.Columns["Price"].Width;
            //list.Add(name, width);
            //name = _form.GetDgvMaterial.Columns["PriceUnit"].Name;
            //width = _form.GetDgvMaterial.Columns["PriceUnit"].Width;
            //list.Add(name, width);
            //name = _form.GetDgvMaterial.Columns["VocPercent"].Name;
            //width = _form.GetDgvMaterial.Columns["VocPercent"].Width;
            //list.Add(name, width);
            //name = _form.GetDgvMaterial.Columns["DateUpdated"].Name;
            //width = _form.GetDgvMaterial.Columns["DateUpdated"].Width;
            //list.Add(name, width);


            CommonFunction.WriteWindowsData(list, FORM_DATA);
        }

        public void LoadFormData()
        {
            _form.Top = _formData.ContainsKey(FORM_TOP) ? (int)_formData[FORM_TOP] : _form.Top;
            _form.Left = _formData.ContainsKey(FORM_LEFT) ? (int)_formData[FORM_LEFT] : _form.Left;
            _form.Width = _formData.ContainsKey(FORM_WIDTH) ? (int)_formData[FORM_WIDTH] : _form.Width;
            _form.Height = _formData.ContainsKey(FORM_HEIGHT) ? (int)_formData[FORM_HEIGHT] : _form.Height;
        }

        #endregion

        public void PrepareAllData()
        {
            #region Tables/Views/Bindings

            _materialList = _repository.GetAll();
            _materialBinding = new BindingSource();
            _materialBinding.DataSource = _materialList;
            _form.GetBindingNavigator.BindingSource = _materialBinding;
            _materialBinding.PositionChanged += MaterialBinding_PositionChanged;

            #endregion

            PreparaeDgvMaterial();
            PrepareOtherControls();

            MaterialBinding_PositionChanged(null, null);
        }

        private void PreparaeDgvMaterial()
        {
            DataGridView view = _form.GetDgvMaterial;
            view.DataSource = _materialBinding;
            view.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.RowsDefaultCellStyle.Font = new Font(view.DefaultCellStyle.Font.Name, 10, FontStyle.Regular);
            view.ColumnHeadersDefaultCellStyle.Font = new Font(view.DefaultCellStyle.Font.Name, 10, FontStyle.Bold);
            view.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            view.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            view.DefaultCellStyle.ForeColor = Color.Black;
            view.MultiSelect = false;
            view.SelectionMode = DataGridViewSelectionMode.CellSelect;
            view.ReadOnly = false;
            view.AutoGenerateColumns = false;

            view.Columns.Remove("Index");
            view.Columns.Remove("SupplierId");
            view.Columns.Remove("FunctionId");
            view.Columns.Remove("IsIntermediate");
            view.Columns.Remove("IsObserved");
            view.Columns.Remove("IsPackage");
            view.Columns.Remove("PricePerQuantity");
            view.Columns.Remove("PriceTransport");
            view.Columns.Remove("Quantity");
            view.Columns.Remove("UnitId");
            view.Columns.Remove("Density");
            view.Columns.Remove("Solids");
            view.Columns.Remove("Ash450");
            view.Columns.Remove("VOC");
            view.Columns.Remove("Remarks");
            view.Columns.Remove("DateCreated");
            view.Columns.Remove("GetRowState");
            view.Columns.Remove("Service");
            view.Columns.Remove("CrudState");

            view.Columns["Id"].Visible = false;
            view.Columns["CurrencyId"].Visible = false;

            view.Columns["Name"].HeaderText = "Surowiec";
            view.Columns["Name"].DisplayIndex = 0;
            view.Columns["Name"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            view.Columns["Name"].Width = _formData.ContainsKey("Name") ? (int)_formData["Name"] : STD_WIDTH;
            view.Columns["Name"].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns["IsActive"].HeaderText = "Aktywny";
            view.Columns["IsActive"].DisplayIndex = 1;
            view.Columns["IsActive"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns["IsActive"].Width = _formData.ContainsKey("IsActive") ? (int)_formData["IsActive"] : STD_WIDTH;
            view.Columns["IsActive"].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns["IsDanger"].HeaderText = "CLP";
            view.Columns["IsDanger"].DisplayIndex = 2;
            view.Columns["IsDanger"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns["IsDanger"].Width = _formData.ContainsKey("IsDanger") ? (int)_formData["IsDanger"] : STD_WIDTH;
            view.Columns["IsDanger"].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns["IsProduction"].HeaderText = "Prod.";
            view.Columns["IsProduction"].DisplayIndex = 3;
            view.Columns["IsProduction"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns["IsProduction"].Width = _formData.ContainsKey("IsProduction") ? (int)_formData["IsProduction"] : STD_WIDTH;
            view.Columns["IsProduction"].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns["Price"].HeaderText = "Cena";
            view.Columns["Price"].DisplayIndex = 4;
            view.Columns["Price"].ReadOnly = true;
            view.Columns["Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns["Price"].Width = _formData.ContainsKey("Price") ? (int)_formData["Price"] : STD_WIDTH;
            view.Columns["Price"].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns["PriceUnit"].HeaderText = "Waluta";
            view.Columns["PriceUnit"].DisplayIndex = 5;
            view.Columns["PriceUnit"].ReadOnly = true;
            view.Columns["PriceUnit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns["PriceUnit"].Width = _formData.ContainsKey("PriceUnit") ? (int)_formData["PriceUnit"] : STD_WIDTH;
            view.Columns["PriceUnit"].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns["VocPercent"].HeaderText = "VOC";
            view.Columns["VocPercent"].DisplayIndex = 6;
            view.Columns["VocPercent"].ReadOnly = true;
            view.Columns["VocPercent"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns["VocPercent"].Width = _formData.ContainsKey("VocPercent") ? (int)_formData["VocPercent"] : STD_WIDTH;
            view.Columns["VocPercent"].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns["DateUpdated"].HeaderText = "Data zmian";
            view.Columns["DateUpdated"].DisplayIndex = 7;
            view.Columns["DateUpdated"].ReadOnly = true;
            view.Columns["DateUpdated"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns["DateUpdated"].Width = _formData.ContainsKey("DateUpdated") ? (int)_formData["DateUpdated"] : STD_WIDTH;
            view.Columns["DateUpdated"].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void PrepareOtherControls()
        {
            _form.GetTxtName.DataBindings.Clear();


            _form.GetTxtName.DataBindings.Add("Text", _materialBinding, "Name");
        }

        #region Current/Binkding/Navigation

        private void MaterialBinding_PositionChanged(object sender, System.EventArgs e)
        {
            #region Get Current Material

            if (_materialBinding == null || _materialBinding.Count == 0)
            {
                CurrentMaterial = null;
            }
            else
            {
                CurrentMaterial = (MaterialDto)_materialBinding.Current;
            }

            #endregion

            #region Set Current Controls

            if (CurrentMaterial != null)
            {
                DateTime date = Convert.ToDateTime(CurrentMaterial.DateCreated);
                string show = date.ToString("dd-MM-yyyy");
                _form.GetLblDateCreated.Text = "Utworzenie: " + show;
                _form.GetLblDateCreated.Left = _form.ClientSize.Width - _form.GetLblDateCreated.Width - 10;
            }
            else
            {
                _form.GetLblDateCreated.Text = "Utworzenie: Brak";
            }

            #endregion


        }

        #endregion
    }
}
