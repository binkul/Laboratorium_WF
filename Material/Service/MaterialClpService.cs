using Laboratorium.ADO.DTO;
using Laboratorium.ADO.Repository;
using Laboratorium.ClpData.Repository;
using Laboratorium.Commons;
using Laboratorium.Material.Forms;
using Laboratorium.Material.Repository;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        private readonly SqlConnection _connection;
        private readonly MaterialDto _material;
        private readonly MaterialClpForm _form;
        private readonly IBasicCRUD<CmbClpHcodeDto> _cmbHcodeRepository;
        private readonly IBasicCRUD<CmbClpPcodeDto> _cmbPcodeRepository;
        private readonly IBasicCRUD<MaterialClpGhsDto> _materialGhsRepository;
        private readonly IBasicCRUD<MaterialClpHCodeDto> _materialHcodeRepository;
        private readonly IBasicCRUD<MaterialClpPCodeDto> _materialPcodeRepository;
        private readonly IBasicCRUD<MaterialClpSignalDto> _materialSignalRepository;
        private IDictionary<string, double> _formData = CommonFunction.LoadWindowsDataAsDictionary(FORM_DATA);

        private IList<CmbClpGHScodeDto> _cmbGhsList;
        private IList<CmbClpSignalDto> _cmbSignalList;
        private IList<MaterialClpGhsDto> _ghsList;
        private IList<MaterialClpSignalDto> _signalList;
        private IList<MaterialClpHCodeDto> _codeList;

        public byte SignalWordId { get; set; } = 0;
        public bool SignalWordChanged { get; set; } = false;
        public bool GHScodeChanged { get; set; } = false;

        public MaterialClpService(SqlConnection connection, MaterialDto material, MaterialClpForm form)
        {
            _connection = connection;
            _material = material;
            _form = form;
            _cmbHcodeRepository = new CmbClpHcodeRepository(_connection);
            _cmbPcodeRepository = new CmbClpPcodeRepository(_connection);
            _materialGhsRepository = new MaterialGHSRepository(_connection);
            _materialHcodeRepository = new MaterialHcodeRepository(_connection);
            _materialPcodeRepository = new MaterialPcodeRepository(_connection);
            _materialSignalRepository = new MaterialSignalRepository(_connection);
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

            IBasicCRUD<CmbClpSignalDto> signalRepo = new CmbClpSignalRepository(_connection);
            _cmbSignalList = signalRepo.GetAll();

            _ghsList = _materialGhsRepository.GetAllByLaboId(_material.Id);

            #endregion

            #region Prepare ComoBox

            _form.GetCmbSignal.DataSource = _cmbSignalList;
            _form.GetCmbSignal.ValueMember = ID;
            _form.GetCmbSignal.DisplayMember = NAME_PL;
            _form.GetCmbSignal.SelectedIndexChanged += SignalWord_SelectedIndexChanged;
            ReadSignalWord();

            #endregion

            PrepareGHSdata();
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

    }
}
