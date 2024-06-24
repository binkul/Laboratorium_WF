﻿using Laboratorium.ADO.DTO;
using Laboratorium.ADO.Repository;
using Laboratorium.ClpData.Repository;
using Laboratorium.Commons;
using Laboratorium.Material.Dto;
using Laboratorium.Material.Forms;
using Laboratorium.Material.Repository;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Laboratorium.Material.Service
{
    public class MaterialClpService
    {
        private const string ID = "Id";
        private const string NAME_PL = "NamePl";
        private const string FORM_TOP = "Form_Top";
        private const string FORM_LEFT = "Form_Left";
        private const string FORM_WIDTH = "Form_Width";
        private const string FORM_HEIGHT = "Form_Height";
        private const string FORM_DATA = "MaterialClpForm";
        private const int STD_WIDTH = 100;

        private readonly SqlConnection _connection;
        private readonly MaterialDto _material;
        private readonly MaterialClpForm _form;
        private readonly IBasicCRUD<MaterialClpGhsDto> _materialGhsRepository;
        private readonly IBasicCRUD<MaterialClpHCodeDto> _materialHcodeRepository;
        private readonly IBasicCRUD<MaterialClpPCodeDto> _materialPcodeRepository;
        private readonly IBasicCRUD<MaterialClpSignalDto> _materialSignalRepository;
        private IDictionary<string, double> _formData = CommonFunction.LoadWindowsDataAsDictionary(FORM_DATA);

        private IList<CmbClpSignalDto> _cmbSignalList;
        private IList<CmbClpCombineDto> _cmbCodeList;
        private IList<MaterialClpGhsDto> _ghsList;
        private IList<ClpHPcombineDto> _codeList;
        private BindingSource _sourceBinding;
        private BindingSource _codeBinding;

        public byte SignalWordId { get; set; } = 0;
        private bool _signalWordChanged = false;
        private bool _gHScodeChanged = false;
        public bool _codeChanged = false;

        public MaterialClpService(SqlConnection connection, MaterialDto material, MaterialClpForm form)
        {
            _connection = connection;
            _material = material;
            _form = form;
            _materialGhsRepository = new MaterialGHSRepository(_connection);
            _materialHcodeRepository = new MaterialHcodeRepository(_connection);
            _materialPcodeRepository = new MaterialPcodeRepository(_connection);
            _materialSignalRepository = new MaterialSignalRepository(_connection);
        }

        #region Change status to save button

        public bool SignalWordChanged
        {
            get => _signalWordChanged;
            set
            {
                _signalWordChanged = value;
                ChangeStatus();
            }
        }

        public bool GHScodeChanged
        {
            get => _gHScodeChanged;
            set
            {
                _gHScodeChanged = value;
                ChangeStatus();
            }
        }

        public bool CodeChanged
        {
            get => _codeChanged;
            set
            {
                _codeChanged = value;
                ChangeStatus();
            }
        }

        private void ChangeStatus()
        {
            _form.EnableSave(SignalWordChanged | GHScodeChanged | CodeChanged);
        }

        #endregion


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

            foreach (DataGridViewColumn col in _form.GetDgvSourceClp.Columns)
            {
                if (col.Visible)
                {
                    string name = col.Name;
                    double width = col.Width;
                    list.Add(name, width);
                }
            }

            foreach (DataGridViewColumn col in _form.GetDgvMaterialClp.Columns)
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


        #region Prepare data

        public void PrepareAllData()
        {
            #region Tables/Views/Bindings

            IBasicCRUD<CmbClpSignalDto> signalRepo = new CmbClpSignalRepository(_connection);
            _cmbSignalList = signalRepo.GetAll();

            _ghsList = _materialGhsRepository.GetAllByLaboId(_material.Id);

            IBasicCRUD<CmbClpCombineDto> cmbCodeRepo = new CmbClpCombineRepository(_connection);
            _cmbCodeList = cmbCodeRepo.GetAll();
            _sourceBinding = new BindingSource();
            _sourceBinding.DataSource = _cmbCodeList;

            IBasicCRUD<ClpHPcombineDto> codeRepo = new ClpHPcombineRepository(_connection);
            _codeList = codeRepo.GetAllByLaboId(_material.Id);
            _codeBinding = new BindingSource();
            _codeBinding.DataSource = _codeList;
            
            #endregion

            PrepareGHSdata();
            PrepareDgvSourceClp();
            PrepareDgvMaterialClp();

            #region Prepare ComoBox

            _form.GetCmbSignal.DataSource = _cmbSignalList;
            _form.GetCmbSignal.ValueMember = ID;
            _form.GetCmbSignal.DisplayMember = NAME_PL;
            _form.GetCmbSignal.SelectedIndexChanged += SignalWord_SelectedIndexChanged;
            ReadSignalWord();

            #endregion

        }

        private void PrepareDgvSourceClp()
        {
            DataGridView view = _form.GetDgvSourceClp;
            view.DataSource = _sourceBinding;
            view.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.RowsDefaultCellStyle.Font = new Font(view.DefaultCellStyle.Font.Name, 10, FontStyle.Regular);
            view.ColumnHeadersDefaultCellStyle.Font = new Font(view.DefaultCellStyle.Font.Name, 10, FontStyle.Bold);
            view.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            view.RowHeadersVisible = false;
            view.DefaultCellStyle.ForeColor = Color.Black;
            view.MultiSelect = false;
            view.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            view.ReadOnly = true;
            view.AutoGenerateColumns = false;

            
            view.Columns.Remove("Descritption");
            view.Columns.Remove("Ordering");
            view.Columns.Remove("Type");

            view.Columns[ID].Visible = false;

            view.Columns["Code"].HeaderText = "Kod";
            view.Columns["Code"].DisplayIndex = 0;
            view.Columns["Code"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns["Code"].Width = _formData.ContainsKey("Code") ? (int)_formData["Code"] : STD_WIDTH;
            view.Columns["Code"].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns["ClassName"].HeaderText = "Klasa";
            view.Columns["ClassName"].DisplayIndex = 1;
            view.Columns["ClassName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns["ClassName"].Width = _formData.ContainsKey("ClassName") ? (int)_formData["ClassName"] : STD_WIDTH;
            view.Columns["ClassName"].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns["SignalWord"].HeaderText = "Hasło";
            view.Columns["SignalWord"].DisplayIndex = 2;
            view.Columns["SignalWord"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns["SignalWord"].Width = _formData.ContainsKey("SignalWord") ? (int)_formData["SignalWord"] : STD_WIDTH;
            view.Columns["SignalWord"].SortMode = DataGridViewColumnSortMode.NotSortable;

            if (view.Rows.Count > 0)
            {
                view.CurrentCell = view.Rows[_sourceBinding.Position].Cells["Code"];
            }

        }

        private void PrepareDgvMaterialClp()
        {
            DataGridView view = _form.GetDgvMaterialClp;
            view.DataSource = _codeBinding;
            view.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.RowsDefaultCellStyle.Font = new Font(view.DefaultCellStyle.Font.Name, 9, FontStyle.Regular);
            view.ColumnHeadersDefaultCellStyle.Font = new Font(view.DefaultCellStyle.Font.Name, 10, FontStyle.Bold);
            view.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            view.RowHeadersWidth = 30;
            view.DefaultCellStyle.ForeColor = Color.Black;
            view.MultiSelect = false;
            view.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            view.ReadOnly = true;
            view.AutoGenerateColumns = false;

            view.Columns.Remove("MaterialId");
            view.Columns.Remove("Ordering");
            view.Columns.Remove("Type");

            view.Columns["CodeId"].Visible = false;

            view.Columns["ClassClp"].HeaderText = "Klasa";
            view.Columns["ClassClp"].DisplayIndex = 0;
            view.Columns["ClassClp"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns["ClassClp"].Width = _formData.ContainsKey("ClassClp") ? (int)_formData["ClassClp"] : STD_WIDTH;
            view.Columns["ClassClp"].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns["CodeClp"].HeaderText = "Kod";
            view.Columns["CodeClp"].DisplayIndex = 1;
            view.Columns["CodeClp"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns["CodeClp"].Width = _formData.ContainsKey("CodeClp") ? (int)_formData["CodeClp"] : STD_WIDTH;
            view.Columns["CodeClp"].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns["DescriptionClp"].HeaderText = "Opis";
            view.Columns["DescriptionClp"].DisplayIndex = 2;
            view.Columns["DescriptionClp"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            view.Columns["DescriptionClp"].Width = _formData.ContainsKey("DescriptionClp") ? (int)_formData["DescriptionClp"] : STD_WIDTH;
            view.Columns["DescriptionClp"].SortMode = DataGridViewColumnSortMode.NotSortable;

            if (view.Rows.Count > 0)
            {
                view.CurrentCell = view.Rows[_sourceBinding.Position].Cells["ClassClp"];
            }
        }

        private void PrepareGHSdata()
        {
            foreach (var pic in _form.GhsList)
            {
                pic.Visible = true;
            }
            foreach (var pic in _form.GhsOkList)
            {
                pic.Visible = false;
            }
            foreach (var ghs in _ghsList)
            {
                _form.GhsList[ghs.CodeId - 1].Visible = false;
                _form.GhsOkList[ghs.CodeId - 1].Visible = true;
            }

            GHScodeChanged = false;
        }

        #endregion


        #region Add/remove buttons

        public void AddOne()
        {
            if (_sourceBinding.Count == 0 || _sourceBinding.Current == null || _material == null)
                return;

            CmbClpCombineDto current = (CmbClpCombineDto)_sourceBinding.Current;
            bool exist = _codeList.Any(i => i.CodeId == current.Id && i.Type == current.Type);
            
            if (exist)
                return;

            ClpHPcombineDto newItem = new ClpHPcombineDto(_material.Id, current.ClassName, current.Id, current.Code, current.Descritption, current.Ordering, current.Type);
            _codeList.Add(newItem);
            _codeList = _codeList.OrderBy(i => i.Ordering).ToList();
            _codeBinding.DataSource = _codeList;

            CodeChanged = true;
        }

        public void AddAll()
        {
            if (_material == null)
                return;

            RemoveAll();
            for (int i = 0; i < _sourceBinding.Count; i++)
            {
                CmbClpCombineDto current = (CmbClpCombineDto)_sourceBinding[i];
                ClpHPcombineDto newItem = new ClpHPcombineDto(_material.Id, current.ClassName, current.Id, current.Code, current.Descritption, current.Ordering, current.Type);
                _codeList.Add(newItem);
            }
            _codeList = _codeList.OrderBy(i => i.Ordering).ToList();
            _codeBinding.DataSource = _codeList;

            CodeChanged = true;
        }

        public void RemoveOne()
        {
            if (_codeBinding.Count == 0 || _codeBinding.Current == null)
                return;
            _codeBinding.EndEdit();

            ClpHPcombineDto code = (ClpHPcombineDto)_codeBinding.Current;
            _codeBinding.Remove(code);

            CodeChanged = true;
        }

        public void RemoveAll()
        {
            if (_codeBinding.Count == 0)
                return;
            _codeBinding.EndEdit();

            do
            {
                _codeBinding.RemoveAt(0);
            } while (_codeBinding.Count > 0);

            CodeChanged = true;
        }

        #endregion


        #region DataGridView Events

        public void DgvSourceClpFormat(DataGridViewCellFormattingEventArgs e)
        {
            DataGridView view = _form.GetDgvSourceClp;

            if (view.Columns[e.ColumnIndex].Name == "ClassName")
            {
                e.CellStyle.Font = new Font(e.CellStyle.Font.Name, 10, FontStyle.Bold);
                e.CellStyle.ForeColor = Color.Red;
            }
            else if (view.Columns[e.ColumnIndex].Name == "Code")
            {
                e.CellStyle.Font = new Font(e.CellStyle.Font.Name, 9, FontStyle.Bold);
                if (e.Value.ToString().Contains("EUH"))
                {
                    e.CellStyle.ForeColor = Color.DarkGreen;
                }
                else if (e.Value.ToString().Contains("P"))
                {
                    e.CellStyle.ForeColor = Color.Magenta;
                }
                else
                {
                    e.CellStyle.ForeColor = Color.Blue;
                }
            }
            else
            {
                e.CellStyle.Font = new Font(e.CellStyle.Font.Name, 9, FontStyle.Bold);
                e.CellStyle.ForeColor = Color.Black;
            }
        }
            
        public void DgvMaterialClpFormat(DataGridViewCellFormattingEventArgs e)
        {
            DataGridView view = _form.GetDgvMaterialClp;

            if (view.Columns[e.ColumnIndex].Name == "ClassClp")
            {
                e.CellStyle.Font = new Font(e.CellStyle.Font.Name, 10, FontStyle.Bold);
                e.CellStyle.ForeColor = Color.Red;
            }
            else if (view.Columns[e.ColumnIndex].Name == "CodeClp")
            {
                e.CellStyle.Font = new Font(e.CellStyle.Font.Name, 9, FontStyle.Bold);
                if (e.Value.ToString().Contains("EUH"))
                {
                    e.CellStyle.ForeColor = Color.DarkGreen;
                }
                else if (e.Value.ToString().Contains("P"))
                {
                    e.CellStyle.ForeColor = Color.Magenta;
                }
                else
                {
                    e.CellStyle.ForeColor = Color.Blue;
                }
            }
            else
            {
                e.CellStyle.Font = new Font(e.CellStyle.Font.Name, 8, FontStyle.Regular);
                e.CellStyle.ForeColor = Color.Black;
            }
        }

        #endregion


        #region Signal Word

        private void SignalWord_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            SignalWordChanged = true;
        }

        private void ReadSignalWord()
        {
            var signal = _materialSignalRepository.GetAllByLaboId(_material.Id);

            if (signal.Count >= 1)
                SignalWordId = signal[0].CodeId;
            else
                SignalWordId = 1;

            _form.GetCmbSignal.SelectedValue = SignalWordId;
            SignalWordChanged = false;
        }

        #endregion


        #region CRUD

        public void Save()
        {

        }

        #endregion
    }
}
