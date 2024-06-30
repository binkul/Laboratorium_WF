using Laboratorium.ADO;
using Laboratorium.ADO.DTO;
using Laboratorium.ADO.Repository;
using Laboratorium.ADO.Service;
using Laboratorium.ClpData.Repository;
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
    public class MaterialClpService : LoadService
    {
        #region DTO-s fields for DGV column

        private const string ID = "Id";
        private const string NAME_PL = "NamePl";
        private const string MATERIAL_ID = "MaterialId";
        private const string DESCRIPTION = "Descritption";
        private const string DESCRIPTION_CLP = "DescriptionClp";
        private const string ORDERING = "Ordering";
        private const string TYPE = "Type";
        private const string CODE = "Code";
        private const string CODE_ID = "CodeId";
        private const string CODE_CLP = "CodeClp";
        private const string CLASS = "ClassName";
        private const string CLASS_CLP = "ClassClp";
        private const string SIGNAL = "SignalWord";

        #endregion

        private readonly IList<string> _dgvSourceFields = new List<string> { CODE, CLASS, SIGNAL };
        private readonly IList<string> _dgvMaterialFields = new List<string> { CLASS_CLP, CODE_CLP, DESCRIPTION_CLP };
        private const string FORM_DATA = "MaterialClpForm";
        private const int STD_WIDTH = 100;

        private readonly SqlConnection _connection;
        private readonly MaterialDto _material;
        private readonly MaterialClpForm _form;
        private readonly IBasicCRUD<MaterialClpGhsDto> _materialGhsRepository;
        private readonly IBasicCRUD<MaterialClpHCodeDto> _materialHcodeRepository;
        private readonly IBasicCRUD<MaterialClpPCodeDto> _materialPcodeRepository;
        private readonly IBasicCRUD<MaterialClpSignalDto> _materialSignalRepository;

        private IList<CmbClpSignalDto> _cmbSignalList;
        private IList<CmbClpCombineDto> _cmbCodeList;
        private IList<MaterialClpGhsDto> _codeGhsList;
        private IList<ClpHPcombineDto> _codeHPlist;
        private BindingSource _sourceBinding;
        private BindingSource _materialBinding;

        public byte SignalWordId { get; set; } = 0;
        private bool _signalWordChanged = false;
        private bool _gHScodeChanged = false;
        private bool _codeChanged = false;

        public bool BtnOk = false; 
        public MaterialClpSignalDto MaterialSignalWord;
        public IList<MaterialClpGhsDto> MaterialGhsList;
        public IList<ClpHPcombineDto> MaterialClpList;

        public MaterialClpService(SqlConnection connection, MaterialDto material, MaterialClpForm form) 
            : base(FORM_DATA, form)
        {
            _connection = connection;
            _material = material;
            _form = form;
            _materialGhsRepository = new MaterialGHSRepository(_connection);
            _materialHcodeRepository = new MaterialHcodeRepository(_connection);
            _materialPcodeRepository = new MaterialPcodeRepository(_connection);
            _materialSignalRepository = new MaterialSignalRepository(_connection);

            MaterialSignalWord = material.SignalWord;
            MaterialGhsList = material.GhsCodeList;
            MaterialClpList = material.HPcodeList;
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

        protected override bool Status => SignalWordChanged | GHScodeChanged | CodeChanged;

        #endregion


        #region Prepare data

        protected override void PrepareColumns()
        {
            MaterialClpForm form = (MaterialClpForm)_baseForm;
            _fields = new Dictionary<DataGridView, IList<string>>
            {
                { form.GetDgvSourceClp,  _dgvSourceFields},
                { form.GetDgvMaterialClp, _dgvMaterialFields }
            };
        }

        public override void PrepareAllData()
        {
            #region Tables/Views/Bindings

            IBasicCRUD<CmbClpSignalDto> signalRepo = new CmbClpSignalRepository(_connection);
            _cmbSignalList = signalRepo.GetAll();

            _codeGhsList = _materialGhsRepository.GetAllByLaboId(_material.Id);

            IBasicCRUD<CmbClpCombineDto> cmbCodeRepo = new CmbClpCombineRepository(_connection);
            _cmbCodeList = cmbCodeRepo.GetAll();
            _sourceBinding = new BindingSource();
            _sourceBinding.DataSource = _cmbCodeList;

            IBasicCRUD<ClpHPcombineDto> codeRepo = new ClpHPcombineRepository(_connection);
            _codeHPlist = codeRepo.GetAllByLaboId(_material.Id);
            _materialBinding = new BindingSource();
            _materialBinding.DataSource = _codeHPlist;
            
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
            view.AllowUserToResizeRows = false;
            
            view.Columns.Remove(DESCRIPTION);
            view.Columns.Remove(ORDERING);
            view.Columns.Remove(TYPE);

            view.Columns[ID].Visible = false;

            view.Columns[CODE].HeaderText = "Kod";
            view.Columns[CODE].DisplayIndex = 0;
            view.Columns[CODE].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns[CODE].Width = _formData.ContainsKey(CODE) ? (int)_formData[CODE] : STD_WIDTH;
            view.Columns[CODE].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns[CLASS].HeaderText = "Klasa";
            view.Columns[CLASS].DisplayIndex = 1;
            view.Columns[CLASS].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns[CLASS].Width = _formData.ContainsKey(CLASS) ? (int)_formData[CLASS] : STD_WIDTH;
            view.Columns[CLASS].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns[SIGNAL].HeaderText = "Hasło";
            view.Columns[SIGNAL].DisplayIndex = 2;
            view.Columns[SIGNAL].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns[SIGNAL].Width = _formData.ContainsKey(SIGNAL) ? (int)_formData[SIGNAL] : STD_WIDTH;
            view.Columns[SIGNAL].SortMode = DataGridViewColumnSortMode.NotSortable;

            if (view.Rows.Count > 0)
            {
                view.CurrentCell = view.Rows[_sourceBinding.Position].Cells[CODE];
            }

        }

        private void PrepareDgvMaterialClp()
        {
            DataGridView view = _form.GetDgvMaterialClp;
            view.DataSource = _materialBinding;
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
            view.AllowUserToResizeRows = false;

            view.Columns.Remove(MATERIAL_ID);
            view.Columns.Remove(ORDERING);
            view.Columns.Remove(TYPE);

            view.Columns[CODE_ID].Visible = false;

            view.Columns[CLASS_CLP].HeaderText = "Klasa";
            view.Columns[CLASS_CLP].DisplayIndex = 0;
            view.Columns[CLASS_CLP].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns[CLASS_CLP].Width = _formData.ContainsKey(CLASS_CLP) ? (int)_formData[CLASS_CLP] : STD_WIDTH;
            view.Columns[CLASS_CLP].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns[CODE_CLP].HeaderText = "Kod";
            view.Columns[CODE_CLP].DisplayIndex = 1;
            view.Columns[CODE_CLP].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns[CODE_CLP].Width = _formData.ContainsKey(CODE_CLP) ? (int)_formData[CODE_CLP] : STD_WIDTH;
            view.Columns[CODE_CLP].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns[DESCRIPTION_CLP].HeaderText = "Opis";
            view.Columns[DESCRIPTION_CLP].DisplayIndex = 2;
            view.Columns[DESCRIPTION_CLP].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            view.Columns[DESCRIPTION_CLP].Width = _formData.ContainsKey(DESCRIPTION_CLP) ? (int)_formData[DESCRIPTION_CLP] : STD_WIDTH;
            view.Columns[DESCRIPTION_CLP].SortMode = DataGridViewColumnSortMode.NotSortable;

            if (view.Rows.Count > 0)
            {
                view.CurrentCell = view.Rows[_sourceBinding.Position].Cells[CLASS_CLP];
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
            foreach (var ghs in _codeGhsList)
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
            bool exist = _codeHPlist.Any(i => i.CodeId == current.Id && i.Type == current.Type);
            
            if (exist)
                return;

            ClpHPcombineDto newItem = new ClpHPcombineDto(_material.Id, current.ClassName, current.Id, current.Code, current.Descritption, current.Ordering, current.Type);
            _codeHPlist.Add(newItem);
            _codeHPlist = _codeHPlist.OrderBy(i => i.Ordering).ToList();
            _materialBinding.DataSource = _codeHPlist;

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
                _codeHPlist.Add(newItem);
            }
            _codeHPlist = _codeHPlist.OrderBy(i => i.Ordering).ToList();
            _materialBinding.DataSource = _codeHPlist;

            CodeChanged = true;
        }

        public void RemoveOne()
        {
            if (_materialBinding.Count == 0 || _materialBinding.Current == null)
                return;
            _materialBinding.EndEdit();

            ClpHPcombineDto code = (ClpHPcombineDto)_materialBinding.Current;
            _materialBinding.Remove(code);

            CodeChanged = true;
        }

        public void RemoveAll()
        {
            if (_materialBinding.Count == 0)
                return;
            _materialBinding.EndEdit();

            do
            {
                _materialBinding.RemoveAt(0);
            } while (_materialBinding.Count > 0);

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
            CmbClpSignalDto signal = (CmbClpSignalDto)_form.GetCmbSignal.SelectedItem;
            SignalWordId = signal.Id;
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

        public override bool Save()
        {
            bool resultClp = true;
            bool resultGhs = true;
            bool resultSig = true;

            if (_material == null)
                return true;

            if (SignalWordChanged)
            {
                resultSig = SaveSignalWord();
                
            }

            if (GHScodeChanged)
            {
                resultGhs = SaveGhs();
            }

            if (CodeChanged)
            {
                resultClp = SaveClp();
            }

            BtnOk = resultClp | resultGhs | resultSig;

            return BtnOk;
        }

        private bool SaveSignalWord()
        {
            bool result;

            _materialSignalRepository.DeleteById(_material.Id);
            MaterialClpSignalDto newSignal = new MaterialClpSignalDto(_material.Id, SignalWordId, _form.GetCmbSignal.Text, DateTime.Today);
            result = _materialSignalRepository.Save(newSignal).CrudState == CrudState.OK;

            MaterialSignalWord = newSignal;
            SignalWordChanged = false;

            return result;
        }

        private bool SaveGhs()
        {
            _materialGhsRepository.DeleteById(_material.Id);
            MaterialGhsList.Clear();
            foreach (PictureBox pic in _form.GhsOkList)
            {
                if (pic.Visible)
                {
                    byte id = Convert.ToByte(pic.Tag);
                    id++;
                    MaterialClpGhsDto newGhs = new MaterialClpGhsDto(_material.Id, id, DateTime.Today);
                    if (_materialGhsRepository.Save(newGhs).CrudState == CrudState.ERROR)
                        return false;

                    MaterialGhsList.Add(newGhs);
                }
            }

            GHScodeChanged = false;
            return true;
        }

        private bool SaveClp()
        {
            _materialHcodeRepository.DeleteById(_material.Id);
            _materialPcodeRepository.DeleteById(_material.Id);
            MaterialClpList.Clear();

            foreach (ClpHPcombineDto clp in _codeHPlist)
            {
                if(clp.Type)
                {
                    MaterialClpHCodeDto newHcode = new MaterialClpHCodeDto(_material.Id, clp.CodeId, clp.ClassClp, clp.CodeClp, clp.DescriptionClp, "", DateTime.Today);
                    if (_materialHcodeRepository.Save(newHcode).CrudState == CrudState.ERROR)
                        return false;
                }
                else
                {
                    MaterialClpPCodeDto newPcode = new MaterialClpPCodeDto(_material.Id, clp.CodeId, clp.CodeClp, clp.DescriptionClp, "", DateTime.Today);
                    if (_materialPcodeRepository.Save(newPcode).CrudState == CrudState.ERROR)
                        return false;
                }
                MaterialClpList.Add(clp);
            }
            CodeChanged = false;

            return true;
        }

        #endregion
    }
}
