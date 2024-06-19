using Laboratorium.ADO;
using Laboratorium.ADO.DTO;
using Laboratorium.ADO.Repository;
using Laboratorium.ADO.Service;
using Laboratorium.Commons;
using Laboratorium.Currency.Repository;
using Laboratorium.Material.Dto;
using Laboratorium.Material.Forms;
using Laboratorium.Material.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Laboratorium.Material.Service
{
    public class MaterialService : IService
    {
        private const string ID = "Id";
        private const string NAME_PL = "NamePl";
        private const string CURRENCY = "Currency";
        private const string ADMIN = "admin";

        private const string FORM_TOP = "Form_Top";
        private const string FORM_LEFT = "Form_Left";
        private const string FORM_WIDTH = "Form_Width";
        private const string FORM_HEIGHT = "Form_Height";
        private const string FORM_DATA = "MaterialForm";
        private const int STD_WIDTH = 100;

        private bool _filterBlock = false;
        private bool _cmbBlock = false;
        private readonly UserDto _user;
        private readonly MaterialForm _form;
        private readonly SqlConnection _connection;
        private readonly IBasicCRUD<MaterialClpGhsDto> _ghsRepository;
        private readonly IBasicCRUD<MaterialClpSignalDto> _signalRepository;
        private readonly IExtendedCRUD<MaterialDto> _repository;
        private IList<MaterialDto> _materialList;
        private IList<ClpHPcombineDto> _materialClpList;
        private IList<CmbUnitDto> _unitList;
        private IList<CmbCurrencyDto> _currencyList;
        private IList<CmbMaterialFunctionDto> _functionList;
        private BindingSource _materialBinding;
        private BindingSource _materialClpBinding;
        public MaterialDto CurrentMaterial;

        private IDictionary<string, double> _formData = CommonFunction.LoadWindowsDataAsDictionary(FORM_DATA);

        public MaterialService(SqlConnection connection, UserDto user, MaterialForm form)
        {
            _connection = connection;
            _user = user;
            _form = form;

            _ghsRepository = new MaterialGHSRepository(_connection);
            _signalRepository = new MaterialSignalRepository(_connection);
            _repository = new MaterialRepository(_connection, this);
        }

        public void Modify(RowState state)
        {
            if (_form.Init)
            {
                return;
            }

            if (state != RowState.UNCHANGED)
            {
                _form.ActivateSave(true);
                return;
            }

            bool laboModify = _materialList
                .Where(i => i.GetRowState != RowState.UNCHANGED)
                .Any();

            _form.ActivateSave(laboModify);
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

            foreach (DataGridViewColumn col in _form.GetDgvClp.Columns)
            {
                if (col.Visible)
                {
                    string name = col.Name;
                    double width = col.Width;
                    list.Add(name, width);
                }
            }

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


        #region Prepare Data

        public void PrepareAllData()
        {
            #region Tables/Views/Bindings

            _materialList = _repository.GetAll();
            _materialBinding = new BindingSource();
            _materialBinding.DataSource = _materialList;
            _form.GetBindingNavigator.BindingSource = _materialBinding;
            _materialBinding.PositionChanged += MaterialBinding_PositionChanged;

            _materialClpList = new List<ClpHPcombineDto>();
            _materialClpBinding = new BindingSource();
            _materialClpBinding.DataSource = _materialClpList;

            IBasicCRUD<CmbUnitDto> repoUnit = new CmbUnitRepository(_connection);
            _unitList = repoUnit.GetAll();

            IBasicCRUD<CmbCurrencyDto> repoCurr = new CmbCurrencyRepository(_connection);
            _currencyList = repoCurr.GetAll();

            IBasicCRUD<CmbMaterialFunctionDto> repoFunction = new CmbMaterialFunctionRepository(_connection);
            _functionList = repoFunction.GetAll();

            #endregion

            PrepareClp();
            PreparaeDgvMaterial();
            PrepareDgvClpMaterial();
            PrepareCombBoxes();
            PrepareOtherControls();

            _form.GetPicClpImage.Size = new Size(300,200);

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
            view.Columns.Remove("GhsCodeList");
            view.Columns.Remove("HPcodeList");
            view.Columns.Remove("SignalWord");

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

            if (view.Rows.Count > 0)
            {
                view.CurrentCell = view.Rows[_materialBinding.Position].Cells["Name"];
            }
        }

        private void PrepareDgvClpMaterial()
        {
            DataGridView view = _form.GetDgvClp;
            view.DataSource = _materialClpBinding;
            view.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.RowsDefaultCellStyle.Font = new Font(view.DefaultCellStyle.Font.Name, 10, FontStyle.Regular);
            view.ColumnHeadersDefaultCellStyle.Font = new Font(view.DefaultCellStyle.Font.Name, 10, FontStyle.Bold);
            view.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            view.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            view.RowHeadersVisible = false;
            view.DefaultCellStyle.ForeColor = Color.Black;
            view.MultiSelect = false;
            view.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            view.ReadOnly = true;
            view.AutoGenerateColumns = false;

            view.Columns.Remove("MaterialId");

            view.Columns["ClassClp"].HeaderText = "Klasa";
            view.Columns["ClassClp"].DisplayIndex = 0;
            view.Columns["ClassClp"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns["ClassClp"].Width = _formData.ContainsKey("ClassClp") ? (int)_formData["ClassClp"] : STD_WIDTH;
            view.Columns["ClassClp"].ReadOnly = true;
            view.Columns["ClassClp"].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns["CodeClp"].HeaderText = "Kod";
            view.Columns["CodeClp"].DisplayIndex = 1;
            view.Columns["CodeClp"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns["CodeClp"].Width = _formData.ContainsKey("CodeClp") ? (int)_formData["CodeClp"] : STD_WIDTH;
            view.Columns["CodeClp"].ReadOnly = true;
            view.Columns["CodeClp"].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns["DescriptionClp"].HeaderText = "Opis";
            view.Columns["DescriptionClp"].DisplayIndex = 2;
            view.Columns["DescriptionClp"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            view.Columns["DescriptionClp"].Width = _formData.ContainsKey("DescriptionClp") ? (int)_formData["DescriptionClp"] : STD_WIDTH;
            view.Columns["DescriptionClp"].ReadOnly = true;
            view.Columns["DescriptionClp"].SortMode = DataGridViewColumnSortMode.NotSortable;

        }

        private void PrepareOtherControls()
        {
            _form.GetTxtName.DataBindings.Clear();
            _form.GetTxtIndex.DataBindings.Clear();
            _form.GetTxtDensity.DataBindings.Clear();
            _form.GetTxtSolids.DataBindings.Clear();
            _form.GetTxtAsh.DataBindings.Clear();
            _form.GetTxtVoc.DataBindings.Clear();
            _form.GetTxtPrice.DataBindings.Clear();
            _form.GetTxtPriceQuantity.DataBindings.Clear();
            _form.GetTxtTransport.DataBindings.Clear();
            _form.GetTxtQuantity.DataBindings.Clear();
            _form.GetTxtRemarks.DataBindings.Clear();
            _form.GetChbDanger.DataBindings.Clear();
            _form.GetChbActive.DataBindings.Clear();
            _form.GetChbPacking.DataBindings.Clear();
            _form.GetChbProduction.DataBindings.Clear();
            _form.GetChbSample.DataBindings.Clear();
            _form.GetChbSemiproduct.DataBindings.Clear();

            _form.GetTxtName.DataBindings.Add("Text", _materialBinding, "Name");
            _form.GetTxtIndex.DataBindings.Add("Text", _materialBinding, "Index");
            _form.GetTxtRemarks.DataBindings.Add("Text", _materialBinding, "Remarks");
            _form.GetChbDanger.DataBindings.Add("Checked", _materialBinding, "IsDanger");
            _form.GetChbActive.DataBindings.Add("Checked", _materialBinding, "IsActive");
            _form.GetChbPacking.DataBindings.Add("Checked", _materialBinding, "IsPackage");
            _form.GetChbProduction.DataBindings.Add("Checked", _materialBinding, "IsProduction");
            _form.GetChbSample.DataBindings.Add("Checked", _materialBinding, "IsObserved");
            _form.GetChbSemiproduct.DataBindings.Add("Checked", _materialBinding, "IsIntermediate");

            Binding price = new Binding("Text", _materialBinding, "Price", true);
            price.Parse += Double_Parse;
            Binding priceQ = new Binding("Text", _materialBinding, "PricePerQuantity", true);
            priceQ.Parse += Double_Parse;
            Binding priceT = new Binding("Text", _materialBinding, "PriceTransport", true);
            priceT.Parse += Double_Parse;
            Binding quant = new Binding("Text", _materialBinding, "Quantity", true);
            quant.Parse += Double_Parse;
            Binding dens = new Binding("Text", _materialBinding, "Density", true);
            dens.Parse += Double_Parse;
            Binding solid = new Binding("Text", _materialBinding, "Solids", true);
            solid.Parse += Double_Parse;
            Binding ash = new Binding("Text", _materialBinding, "Ash450", true);
            ash.Parse += Double_Parse;
            Binding voc = new Binding("Text", _materialBinding, "VOC", true);
            voc.Parse += Double_Parse;

            _form.GetTxtPrice.DataBindings.Add(price);
            _form.GetTxtPriceQuantity.DataBindings.Add(priceQ);
            _form.GetTxtTransport.DataBindings.Add(priceT);
            _form.GetTxtQuantity.DataBindings.Add(quant);
            _form.GetTxtDensity.DataBindings.Add(dens);
            _form.GetTxtSolids.DataBindings.Add(solid);
            _form.GetTxtAsh.DataBindings.Add(ash);
            _form.GetTxtVoc.DataBindings.Add(voc);

        }

        private void PrepareCombBoxes()
        {
            _form.GetCmbUnit.DataSource = _unitList;
            _form.GetCmbUnit.ValueMember = ID;
            _form.GetCmbUnit.DisplayMember = NAME_PL;
            _form.GetCmbUnit.SelectedIndexChanged += CmbUnit_SelectedIndexChanged;

            _form.GetCmbCurrency.DataSource = _currencyList;
            _form.GetCmbCurrency.ValueMember = ID;
            _form.GetCmbCurrency.DisplayMember = CURRENCY;
            _form.GetCmbCurrency.SelectedIndexChanged += CmbCurrency_SelectedIndexChanged;

            _form.GetCmbFunction.DataSource = _functionList;
            _form.GetCmbFunction.ValueMember = ID;
            _form.GetCmbFunction.DisplayMember = NAME_PL;
            _form.GetCmbFunction.SelectedIndexChanged += CmbFunction_SelectedIndexChanged;

        }

        private void PrepareClp()
        {
            IBasicCRUD<ClpHPcombineDto> repo = new ClpHPcombineRepository(_connection);
            IList<ClpHPcombineDto> clpList = repo.GetAll();
            IList<MaterialClpGhsDto> ghsList = _ghsRepository.GetAll();
            IList<MaterialClpSignalDto> sigList = _signalRepository.GetAll();
            foreach (var material in _materialList)
            {
                if (material.IsDanger)
                {
                    var ghs = ghsList
                        .Where(i => i.MaterialId == material.Id)
                        .ToList();
                    material.GhsCodeList = ghs;

                    var clp = clpList
                        .Where(i => i.MaterialId == material.Id)
                        .ToList();

                    material.HPcodeList = clp;

                    var sig = sigList
                        .Where(i => i.MaterialId == material.Id)
                        .FirstOrDefault();
                    material.SignalWord = sig ?? new MaterialClpSignalDto(material.Id, 1, "-- Brak --", DateTime.Today);
                }
            }
        }

        #endregion


        #region Current/Binkding/Navigation

        private void MaterialBinding_PositionChanged(object sender, System.EventArgs e)
        {
            _cmbBlock = true;

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

            #region Synchronize CLP controls

            if (CurrentMaterial != null)
            {
                SetGhsImage(true);
                SetSignalWord(true);
                SynchronizeHPcode(true);
            }
            else
            {
                SetGhsImage(false);
                SetSignalWord(false);
                SynchronizeHPcode(false);
            }

            #endregion

            #region Synchronize Combo Gloss Units

            if (CurrentMaterial != null)
            {
                _form.GetCmbUnit.SelectedValue = CurrentMaterial.UnitId;
            }
            else
            {
                _form.GetCmbUnit.SelectedIndex = _form.GetCmbUnit.Items.Count > 0 ? 0 : -1;
            }

            #endregion

            #region Synchronize Combo Currency

            if (CurrentMaterial != null)
            {
                _form.GetCmbCurrency.SelectedValue = CurrentMaterial.CurrencyId;
            }
            else
            {
                _form.GetCmbCurrency.SelectedIndex = _form.GetCmbCurrency.Items.Count > 0 ? 0 : -1;
            }

            #endregion

            #region Synchronize Combo Function

            if (CurrentMaterial != null)
            {
                _form.GetCmbFunction.SelectedValue = CurrentMaterial.FunctionId;
            }
            else
            {
                _form.GetCmbFunction.SelectedIndex = _form.GetCmbFunction.Items.Count > 0 ? 0 : -1;
            }

            #endregion

            #region Synchronize others controls

            _form.GetBtnClpEdit.Enabled = CurrentMaterial != null && _user.Permission.ToLower().Equals(ADMIN) && CurrentMaterial.IsDanger;

            #endregion

            _cmbBlock = false;
        }

        public void ChangePriceUnit(MaterialDto material)
        {
            string unitName = _unitList
                .Where(i => i.Id == material.UnitId)
                .Select(i => i.NamePl)
                .FirstOrDefault();

            string currName = _currencyList
                .Where(i => i.Id == material.CurrencyId)
                .Select(i => i.Currency)
                .FirstOrDefault();

            if (material.CurrencyId != 1 && !string.IsNullOrEmpty(currName))
            {
                material.PriceUnit = currName + " / " + unitName;
            }
            else
            {
                material.PriceUnit = "-- / " + unitName;
            }
        }

        private void SetGhsImage(bool danger)
        {
            //       1       5
            //   2       4       7
            //       3       6

            _form.GetPicClpImage.Image = null;
            if (!danger)
                return;

            if (CurrentMaterial.IsDanger)
            {
                var ghsList = CurrentMaterial.GhsCodeList;
                Bitmap bitmap = new Bitmap(300, 200);
                Graphics graphics = Graphics.FromImage(bitmap);

                int position = 0;
                foreach (var ghs in ghsList)
                {
                    graphics.DrawImage(CommonData.GhsImages[ghs.CodeId - 1], CommonData.GhsPoints[position]);
                    position++;
                }
                
                graphics.Dispose();
                _form.GetPicClpImage.Image = bitmap;
            }
        }

        private void SetSignalWord(bool danger)
        {
            if (!danger || CurrentMaterial.SignalWord.CodeId == 1)
            {
                _form.GetLblClpSignal.Text = "Nieszkodliwy";
                _form.GetLblClpSignal.ForeColor = Color.Blue;
            }
            else
            {
                _form.GetLblClpSignal.Text = CurrentMaterial.SignalWord.NamePl;
                _form.GetLblClpSignal.ForeColor = Color.Red;
            }
        }

        private void SynchronizeHPcode(bool danger)
        {
            if (danger)
            {
                _materialClpList = CurrentMaterial.HPcodeList;
                _materialClpBinding.DataSource = _materialClpList;
            }
            else
            {
                _materialClpList = new List<ClpHPcombineDto>();
                _materialClpBinding.DataSource = _materialClpList;
            }
        }

        #endregion


        #region ComboBox Events

        private void CmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cmbBlock)
                return;

            if (CurrentMaterial != null)
            {
                CmbUnitDto cmb = (CmbUnitDto)_form.GetCmbUnit.SelectedItem;
                byte cmbId = cmb.Id;
                if (cmb != null && (CurrentMaterial.UnitId != cmbId))
                {
                    CurrentMaterial.UnitId = cmbId;
                    _materialBinding.EndEdit();
                }
            }
        }

        private void CmbCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cmbBlock)
                return;

            if (CurrentMaterial != null)
            {
                CmbCurrencyDto cmb = (CmbCurrencyDto)_form.GetCmbCurrency.SelectedItem;
                byte cmbId = cmb.Id;
                if (cmb != null && (CurrentMaterial.CurrencyId != cmbId))
                {
                    CurrentMaterial.CurrencyId = cmbId;
                    _materialBinding.EndEdit();
                }
            }
        }

        private void CmbFunction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cmbBlock)
                return;

            if (CurrentMaterial != null)
            {
                CmbMaterialFunctionDto cmb = (CmbMaterialFunctionDto)_form.GetCmbFunction.SelectedItem;
                short cmbId = cmb.Id;
                if (cmb != null && (CurrentMaterial.FunctionId != cmbId))
                {
                    CurrentMaterial.FunctionId = cmbId;
                    _materialBinding.EndEdit();
                }
            }
        }

        #endregion


        #region Parse Double

        private void Double_Parse(object sender, ConvertEventArgs e)
        {
            if (e.Value.Equals(""))
                e.Value = null;
        }

        #endregion


        #region Menu

        public void AddNewMaterial()
        {
            string permission = _user.Permission.ToLower();
            if (!permission.Equals(ADMIN))
                return;

            MaterialDto material = new MaterialDto.Builder()
                .Id(0)
                .Name("")
                .SupplierId(1)
                .FunctinoId(1)
                .IsIntermediate(false)
                .IsObserved(false)
                .IsActive(false)
                .Isdanger(false)
                .IsPackage(false)
                .IsProduction(false)
                .CurrencyId(1)
                .UnitId(1)
                .Service(this)
                .Build();

            CancelFilter(false);

            _materialBinding.Add(material);
            _materialBinding.EndEdit();
            _form.GetDgvMaterial.Refresh();

            _materialBinding.Position = _materialBinding.Count - 1;

            Modify(RowState.ADDED);
        }

        #endregion


        #region CRUD

        public void Save()
        {

            
        }

        public void Delete()
        {

        }

        private bool CheckBeforeSave(MaterialDto material)
        {
            bool result = true;

            if (string.IsNullOrEmpty(material.Name))
            {
                MessageBox.Show("Brak nazwy surowca. Nie mozna zapisac surowca bez nazwy. uzupełnij nazwę.", "Brak nazwy", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return result;
        }

        #endregion


        #region DataGridView and others events

        public void DgvClpHPdataFormat(DataGridViewCellFormattingEventArgs e)
        {
            DataGridView view = _form.GetDgvClp;

            if (view.Columns[e.ColumnIndex].Name == "ClassClp")
            {
                e.CellStyle.Font = new Font(e.CellStyle.Font.Name, 10, FontStyle.Bold);
                e.CellStyle.ForeColor = Color.Red;
            }
            else if (view.Columns[e.ColumnIndex].Name == "CodeClp")
            {
                e.CellStyle.ForeColor = Color.Blue;
            }
            else
            {
                e.CellStyle.Font = new Font(e.CellStyle.Font.Name, 9, FontStyle.Regular);
                e.CellStyle.ForeColor = Color.Black;
            }
        }

        public void DangerStateChanged()
        {
            if (_form.GetChbDanger.Checked)
                _form.GetBtnClpEdit.Enabled = CurrentMaterial != null && _user.Permission.ToLower().Equals(ADMIN);
            else
                _form.GetBtnClpEdit.Enabled = false;
        }

        public void ColumnWidthChanged()
        {
            DataGridView view = _form.GetDgvMaterial;
            int left;

            _form.GetBtnFilterCancel.Left = view.Left;
            _form.GetBtnFilterCancel.Width = view.RowHeadersWidth - 2;

            _form.GetTxtFilterName.Left = view.Left + view.RowHeadersWidth;
            _form.GetTxtFilterName.Width = view.Columns["Name"].Width;

            left = _form.GetTxtFilterName.Left + _form.GetTxtFilterName.Width 
                + (view.Columns["IsActive"].Width / 2);
            _form.GetChbFilterActive.Left = left - (_form.GetChbFilterActive.Width / 2);

            left = _form.GetTxtFilterName.Left + _form.GetTxtFilterName.Width
                + view.Columns["IsActive"].Width + (view.Columns["IsDanger"].Width / 2);
            _form.GetChbFilterClp.Left = left - (_form.GetChbFilterClp.Width / 2);

            left = _form.GetTxtFilterName.Left + _form.GetTxtFilterName.Width
                + view.Columns["IsActive"].Width + view.Columns["IsDanger"].Width + (view.Columns["IsProduction"].Width / 2);
            _form.GetChbFilterProd.Left = left - (_form.GetChbFilterProd.Width / 2);
        }

        #endregion


        #region Filtration

        public void Filtering()
        {
            if (_filterBlock)
                return;

            string name = _form.GetTxtFilterName.Text;
            bool isName = !string.IsNullOrEmpty(name);
            bool isActive = _form.GetChbFilterActive.Checked;
            bool isDanger = _form.GetChbFilterClp.Checked;
            bool isProd = _form.GetChbFilterProd.Checked;

            if (IsFilterSet())
            {
                SetFilter(_materialList);
                return;
            }

            IList<MaterialDto> filtered;

            filtered = _materialList
                .Where(i => isName ? i.Name.ToLower().Contains(name.ToLower()) : true)
                .Where(i => isActive ? i.IsActive : true)
                .Where(i => isDanger ? i.IsDanger : true)
                .Where(i => isProd ? i.IsProduction : true)
                .ToList();

            SetFilter(filtered);
        }

        public void SetFilter(IList<MaterialDto> filter)
        {
            _materialBinding.DataSource = filter;
            _materialBinding.Position = filter.Count > 0 ? 0 : -1;
        }

        public void CancelFilter(bool GoToFirst)
        {
            if (IsFilterSet())
                return;

            _filterBlock = true;
            _form.GetTxtFilterName.Text = "";
            _form.GetChbFilterActive.Checked = false;
            _form.GetChbFilterClp.Checked = false;
            _form.GetChbFilterProd.Checked = false;

            _materialBinding.DataSource = _materialList;
            if (GoToFirst)
                _materialBinding.Position = 0;

            _filterBlock = false;
        }

        private bool IsFilterSet()
        {
            string name = _form.GetTxtFilterName.Text;
            bool isActive = _form.GetChbFilterActive.Checked;
            bool isDanger = _form.GetChbFilterClp.Checked;
            bool isProd = _form.GetChbFilterProd.Checked;

            return string.IsNullOrEmpty(name) && !isActive && !isDanger && !isProd;
        }

        #endregion
    }
}
