using Laboratorium.ADO;
using Laboratorium.ADO.DTO;
using Laboratorium.ADO.Service;
using Laboratorium.ClpData.Repository;
using Laboratorium.Commons;
using Laboratorium.Material.Forms;
using Laboratorium.Material.Repository;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        private readonly SqlConnection _connection;
        private readonly UserDto _user;
        private readonly MaterialForm _form;
        private readonly CmbClpHcodeRepository _codeHrepository;
        private readonly CmbClpPcodeRepository _codePrepository;
        private readonly MaterialRepository _repository;
        private IList<MaterialDto> _materialList;
        private BindingSource _materialBinding;
        private MaterialDto _currentMaterial;

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
        }


        #region Current/Binkding/Navigation

        private void MaterialBinding_PositionChanged(object sender, System.EventArgs e)
        {
            #region Get Current Material

            if (_materialBinding == null || _materialBinding.Count == 0)
            {
                _currentMaterial = null;
            }
            else
            {
                _currentMaterial = (MaterialDto)_materialBinding.Current;
            }

            #endregion


        }

        #endregion
    }
}
