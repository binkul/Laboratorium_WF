using Laboratorium.ADO;
using Laboratorium.ADO.DTO;
using Laboratorium.ADO.Repository;
using Laboratorium.ADO.Service;
using Laboratorium.Commons;
using Laboratorium.Composition.Forms;
using Laboratorium.LabBook.Forms;
using Laboratorium.LabBook.Repository;
using Laboratorium.Material.Forms;
using Laboratorium.Project.Forms;
using Laboratorium.Project.Repository;
using Laboratorium.User.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Laboratorium.LabBook.Service
{
    public class LabBookService : IService
    {
        private static readonly Color LightGrey = Color.FromArgb(200, 210, 210, 210);
        private static readonly SolidBrush redBrush = new SolidBrush(Color.Red);
        private static readonly SolidBrush blueBrush = new SolidBrush(Color.LightBlue);

        private const string FORM_TOP = "Form_Top";
        private const string FORM_LEFT = "Form_Left";
        private const string FORM_WIDTH = "Form_Width";
        private const string FORM_HEIGHT = "Form_Height";
        private const string FORM_DATA = "LabBookForm";

        private const string ID = "Id";
        private const string NAME_PL = "NamePl";

        private bool _filterBlock = false;
        private bool _cmbBlock = false;
        private readonly IExtendedCRUD<LaboDto> _repositoryLabo;
        private readonly IBasicCRUD<LaboDataBasicDto> _repositoryLaboBasic;
        private readonly IExtendedCRUD<UserDto> _repositoryUser;
        private readonly IBasicCRUD<ProjectDto> _repositoryProject;
        private readonly IBasicCRUD<LaboDataViscosityColDto> _repositoryViscosityCol;
        private readonly SqlConnection _connection;
        private readonly UserDto _user;
        private readonly LabForm _form;

        private readonly IDgvService _normTestService;
        private readonly IDgvService _contrastService;
        private readonly IDgvService _viscosityService;

        private IList<LaboDto> _laboList;
        private BindingSource _laboBinding;
        private BindingSource _laboBasicBinding;
        private IList<UserDto> _userList;
        private IList<ProjectDto> _projectList;
        private IList<CmbContrastClassDto> _contrastClassList;
        private IList<CmbGlossClassDto> _glossClassList;
        private IList<CmbScrubClassDto> _scrubClassList;
        private IList<CmbVocClassDto> _vocClassList;
        private IDictionary<string, double> _formData = CommonFunction.LoadWindowsDataAsDictionary(FORM_DATA);
        internal LaboDto CurrentLabBook => _laboBinding != null && _laboBinding.Count > 0 ? (LaboDto)_laboBinding.Current : null;
        private LaboDataBasicDto _currentLabBookBasic => _laboBasicBinding != null ? (LaboDataBasicDto)_laboBasicBinding.Current : null;
  
        
        public LabBookService(SqlConnection connection, UserDto user, LabForm form)
        {
            _connection = connection;
            _user = user;
            _form = form;
            _repositoryLabo = new LabBookRepository(_connection, this);
            _repositoryLaboBasic = new LabBookBasicDataRepository(_connection, this);
            _repositoryViscosityCol = new LabBookViscosityColRepository(_connection);
            _repositoryUser = new UserRepository(_connection);
            _repositoryProject = new ProjectRepository(_connection);

            _normTestService = new LabBookNormTestService(connection, form, this);
            _viscosityService = new LabBookViscosityService(connection, form, _formData, this);
            _contrastService = new LabBookContrastService(connection, form, this);
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

            bool laboModify = _laboList
                .Where(i => i.GetRowState != RowState.UNCHANGED)
                .Any();
            bool basicModify = _laboList
                .Where(i => i.LaboBasicData != null)
                .Select(i => i.LaboBasicData)
                .Where(i => i.GetRowState != RowState.UNCHANGED)
                .Any();

            bool visModify = _viscosityService.IsModified();
            bool conModify = _contrastService.IsModified();
            bool normModify = _normTestService.IsModified();

            bool result = laboModify | basicModify | visModify | conModify | normModify;

            _form.ActivateSave(result);
        }

        private bool IsAdmin => CurrentLabBook != null ? _user.Permission.ToLower() == "admin" : false;

        private bool IsValidUser => CurrentLabBook != null ? _user.Id == CurrentLabBook.UserId : false;

        public IDictionary<string, double> GetFormData => _formData;


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

            AddLaboColumnData(list);
            AddViscosityColumnData(list);
            AddNormTestData(list);
            CommonFunction.WriteWindowsData(list, FORM_DATA);
        }

        public void LoadFormData()
        {
            _form.Top = _formData.ContainsKey(FORM_TOP) ? (int)_formData[FORM_TOP] : _form.Top;
            _form.Left = _formData.ContainsKey(FORM_LEFT) ? (int)_formData[FORM_LEFT] : _form.Left;
            _form.Width = _formData.ContainsKey(FORM_WIDTH) ? (int)_formData[FORM_WIDTH] : _form.Width;
            _form.Height = _formData.ContainsKey(FORM_HEIGHT) ? (int)_formData[FORM_HEIGHT] : _form.Height;
        }

        private void AddLaboColumnData(IDictionary<string, double> list)
        {
            foreach (DataGridViewColumn column in _form.GetDgvLabo.Columns)
            {
                if (column.Visible)
                {
                    list.Add(column.Name, column.Width);
                }
            }
        }

        private void AddViscosityColumnData(IDictionary<string, double> list)
        {
            string name = _form.GetDgvViscosity.Columns["DateCreated"].Name;
            double width = _form.GetDgvViscosity.Columns["DateCreated"].Width;
            list.Add(name, width);
            name = _form.GetDgvViscosity.Columns["ToCompare"].Name;
            width = _form.GetDgvViscosity.Columns["ToCompare"].Width;
            list.Add(name, width);
            name = _form.GetDgvViscosity.Columns["DateUpdated"].Name;
            width = _form.GetDgvViscosity.Columns["DateUpdated"].Width;
            list.Add(name, width);
            name = _form.GetDgvViscosity.Columns["Temp"].Name;
            width = _form.GetDgvViscosity.Columns["Temp"].Width;
            list.Add(name, width);
            name = _form.GetDgvViscosity.Columns["Days"].Name;
            width = _form.GetDgvViscosity.Columns["Days"].Width;
            list.Add(name, width);
            name = _form.GetDgvViscosity.Columns["pH"].Name;
            width = _form.GetDgvViscosity.Columns["pH"].Width;
            list.Add(name, width);
            name = _form.GetDgvViscosity.Columns["Brook1"].Name;
            width = _form.GetDgvViscosity.Columns["Brook1"].Width;
            list.Add(name, width);
            name = _form.GetDgvViscosity.Columns["Brook5"].Name;
            width = _form.GetDgvViscosity.Columns["Brook5"].Width;
            list.Add(name, width);
            name = _form.GetDgvViscosity.Columns["Brook10"].Name;
            width = _form.GetDgvViscosity.Columns["Brook10"].Width;
            list.Add(name, width);
            name = _form.GetDgvViscosity.Columns["Brook20"].Name;
            width = _form.GetDgvViscosity.Columns["Brook20"].Width;
            list.Add(name, width);
            name = _form.GetDgvViscosity.Columns["Brook30"].Name;
            width = _form.GetDgvViscosity.Columns["Brook30"].Width;
            list.Add(name, width);
            name = _form.GetDgvViscosity.Columns["Brook40"].Name;
            width = _form.GetDgvViscosity.Columns["Brook40"].Width;
            list.Add(name, width);
            name = _form.GetDgvViscosity.Columns["Brook50"].Name;
            width = _form.GetDgvViscosity.Columns["Brook50"].Width;
            list.Add(name, width);
            name = _form.GetDgvViscosity.Columns["Brook60"].Name;
            width = _form.GetDgvViscosity.Columns["Brook60"].Width;
            list.Add(name, width);
            name = _form.GetDgvViscosity.Columns["Brook70"].Name;
            width = _form.GetDgvViscosity.Columns["Brook70"].Width;
            list.Add(name, width);
            name = _form.GetDgvViscosity.Columns["Brook80"].Name;
            width = _form.GetDgvViscosity.Columns["Brook80"].Width;
            list.Add(name, width);
            name = _form.GetDgvViscosity.Columns["Brook90"].Name;
            width = _form.GetDgvViscosity.Columns["Brook90"].Width;
            list.Add(name, width);
            name = _form.GetDgvViscosity.Columns["Brook100"].Name;
            width = _form.GetDgvViscosity.Columns["Brook100"].Width;
            list.Add(name, width);
            name = _form.GetDgvViscosity.Columns["BrookDisc"].Name;
            width = _form.GetDgvViscosity.Columns["BrookDisc"].Width;
            list.Add(name, width);
            name = _form.GetDgvViscosity.Columns["BrookComment"].Name;
            width = _form.GetDgvViscosity.Columns["BrookComment"].Width;
            list.Add(name, width);
            name = _form.GetDgvViscosity.Columns["BrookXvisc"].Name;
            width = _form.GetDgvViscosity.Columns["BrookXvisc"].Width;
            list.Add(name, width);
            name = _form.GetDgvViscosity.Columns["BrookXrpm"].Name;
            width = _form.GetDgvViscosity.Columns["BrookXrpm"].Width;
            list.Add(name, width);
            name = _form.GetDgvViscosity.Columns["BrookXdisc"].Name;
            width = _form.GetDgvViscosity.Columns["BrookXdisc"].Width;
            list.Add(name, width);
        }

        private void AddNormTestData(IDictionary<string, double> list)
        {
            string name = "DateCreated_test";
            double width = _form.GetDgvNormTest.Columns["DateCreated"].Width;
            list.Add(name, width);
            name = "DateUpdated_test";
            width = _form.GetDgvNormTest.Columns["DateUpdated"].Width;
            list.Add(name, width);
            name = "Days_test";
            width = _form.GetDgvNormTest.Columns["Days"].Width;
            list.Add(name, width);
            name = "Norm_test";
            width = _form.GetDgvNormTest.Columns["Norm"].Width;
            list.Add(name, width);
            name = "Description_test";
            width = _form.GetDgvNormTest.Columns["Description"].Width;
            list.Add(name, width);
            name = "Requirement_test";
            width = _form.GetDgvNormTest.Columns["Requirement"].Width;
            list.Add(name, width);
            name = "Result_test";
            width = _form.GetDgvNormTest.Columns["Result"].Width;
            list.Add(name, width);
            name = "Substrate_test";
            width = _form.GetDgvNormTest.Columns["Substrate"].Width;
            list.Add(name, width);
            name = "Comments_test";
            width = _form.GetDgvNormTest.Columns["Comments"].Width;
            list.Add(name, width);
        }

        #endregion


        #region Prepare Data

        public void PrepareAllData()
        {
            #region Tables/Views/Bindings for Main Labo

            LoadLaboData();
            _userList = _repositoryUser.GetAll();
            _projectList = _repositoryProject.GetAll();

            _laboBasicBinding = new BindingSource();

            _viscosityService.PrepareData();
            _contrastService.PrepareData();
            _normTestService.PrepareData();

            IBasicCRUD<CmbContrastClassDto> contrast = new ContrastClassRepository(_connection);
            _contrastClassList = contrast.GetAll();

            IBasicCRUD<CmbGlossClassDto> gloss = new GlossClassRepository(_connection);
            _glossClassList = gloss.GetAll();

            IBasicCRUD<CmbScrubClassDto> scrub = new ScrubClassRepository(_connection);
            _scrubClassList = scrub.GetAll();

            IBasicCRUD<CmbVocClassDto> voc = new VocClassRepository(_connection);
            _vocClassList = voc.GetAll();

            FillDependece();

            #endregion

            #region Prepare DgvLabBook

            PrepareDgvLabo();

            #endregion

            #region Prepare ComboBoxes

            PrepareComboBoxes();

            #endregion

            #region Prepare others control

            _form.GetTxtTitle.DataBindings.Clear();
            _form.GetTxtObservation.DataBindings.Clear();
            _form.GetTxtConclusion.DataBindings.Clear();
            _form.GetTxtScrubBrush.DataBindings.Clear();
            _form.GetTxtScrubSponge.DataBindings.Clear();
            _form.GetTxtScrubComment.DataBindings.Clear();
            _form.GetTxtContrastComment.DataBindings.Clear();
            _form.GetTxtGloss20.DataBindings.Clear();
            _form.GetTxtGloss60.DataBindings.Clear();
            _form.GetTxtGloss85.DataBindings.Clear();
            _form.GetTxtGlossComment.DataBindings.Clear();
            _form.GetTxtVoc.DataBindings.Clear();
            _form.GetTxtYield.DataBindings.Clear();
            _form.GetTxtYieldFormula.DataBindings.Clear();
            _form.GetTxtAdhesion.DataBindings.Clear();
            _form.GetTxtFlow.DataBindings.Clear();
            _form.GetTxtSpill.DataBindings.Clear();
            _form.GetTxtDryI.DataBindings.Clear();
            _form.GetTxtDryII.DataBindings.Clear();
            _form.GetTxtDryIII.DataBindings.Clear();
            _form.GetTxtDryIV.DataBindings.Clear();
            _form.GetTxtDryV.DataBindings.Clear();


            _form.GetTxtTitle.DataBindings.Add("Text", _laboBinding, "Title");
            _form.GetTxtObservation.DataBindings.Add("Text", _laboBinding, "Observation");
            _form.GetTxtConclusion.DataBindings.Add("Text", _laboBinding, "Conclusion");
            _form.GetTxtScrubBrush.DataBindings.Add("Text", _laboBasicBinding, "ScrubBrush");
            _form.GetTxtScrubSponge.DataBindings.Add("Text", _laboBasicBinding, "ScrubSponge");
            _form.GetTxtScrubComment.DataBindings.Add("Text", _laboBasicBinding, "ScrubComment");
            _form.GetTxtContrastComment.DataBindings.Add("Text", _laboBasicBinding, "ContrastComment");

            Binding gloss20 = new Binding("Text", _laboBasicBinding, "Gloss20", true);
            gloss20.Parse += TxtDouble_Parse;
            Binding gloss60 = new Binding("Text", _laboBasicBinding, "Gloss60", true);
            gloss60.Parse += TxtDouble_Parse;
            Binding gloss85 = new Binding("Text", _laboBasicBinding, "Gloss85", true);
            gloss85.Parse += TxtDouble_Parse;
            _form.GetTxtGloss20.DataBindings.Add(gloss20);
            _form.GetTxtGloss60.DataBindings.Add(gloss60);
            _form.GetTxtGloss85.DataBindings.Add(gloss85);

            _form.GetTxtGlossComment.DataBindings.Add("Text", _laboBasicBinding, "GlossComment");
            _form.GetTxtVoc.DataBindings.Add("Text", _laboBasicBinding, "VOC");
            _form.GetTxtYield.DataBindings.Add("Text", _laboBasicBinding, "Yield");
            _form.GetTxtYieldFormula.DataBindings.Add("Text", _laboBasicBinding, "YieldFormula");
            _form.GetTxtAdhesion.DataBindings.Add("Text", _laboBasicBinding, "Adhesion");
            _form.GetTxtFlow.DataBindings.Add("Text", _laboBasicBinding, "Flow");
            _form.GetTxtSpill.DataBindings.Add("Text", _laboBasicBinding, "Spill");
            _form.GetTxtDryI.DataBindings.Add("Text", _laboBasicBinding, "DryingI");
            _form.GetTxtDryII.DataBindings.Add("Text", _laboBasicBinding, "DryingII");
            _form.GetTxtDryIII.DataBindings.Add("Text", _laboBasicBinding, "DryingIII");
            _form.GetTxtDryIV.DataBindings.Add("Text", _laboBasicBinding, "DryingIV");
            _form.GetTxtDryV.DataBindings.Add("Text", _laboBasicBinding, "DryingV");

            #endregion

            LaboBinding_PositionChanged(null, null);
        }

        private void FillDependece()
        {
            IList<LaboDataBasicDto> laboBasicList = _repositoryLaboBasic.GetAll();
            IList<LaboDataViscosityColDto> laboViscosityColList = _repositoryViscosityCol.GetAll(); ;

            foreach (LaboDto labo in _laboList)
            {
                short id = labo.UserId;
                UserDto user = _userList
                    .Where(i => i.Id == id)
                    .FirstOrDefault();
                if (user != null)
                    labo.User = user;

                int projectId = labo.ProjectId;
                ProjectDto project = _projectList
                    .Where(i => i.Id == projectId)
                    .FirstOrDefault();
                if (project != null)
                    labo.Project = project;

                LaboDataBasicDto basicData = laboBasicList
                    .Where(i => i.LaboId == labo.Id)
                    .FirstOrDefault();
                if (basicData != null)
                    labo.LaboBasicData = basicData;

                LaboDataViscosityColDto profile = laboViscosityColList
                    .Where(i => i.LaboId == labo.Id)
                    .FirstOrDefault();
                if (profile != null)
                {
                    labo.ViscosityProfile = profile;
                    labo.AcceptChanges();
                }
            }
        }

        private void LoadLaboData()
        {
            _laboList = _repositoryLabo.GetAll();
            _laboBinding = new BindingSource
            {
                DataSource = _laboList
            };
            _form.GetNavigatorLabo.BindingSource = _laboBinding;
            _laboBinding.PositionChanged += LaboBinding_PositionChanged;
        }

        private void PrepareDgvLabo()
        {
            DataGridView view = _form.GetDgvLabo;
            view.DataSource = _laboBinding;
            view.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.RowsDefaultCellStyle.Font = new Font(view.DefaultCellStyle.Font.Name, 10, FontStyle.Regular);
            view.ColumnHeadersDefaultCellStyle.Font = new Font(view.DefaultCellStyle.Font.Name, 10, FontStyle.Bold);
            view.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            view.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            view.RowHeadersWidth = IsAdmin ? CommonData.HEADER_WIDTH_ADMIN : CommonData.HEADER_WIDTH_USER;
            view.DefaultCellStyle.ForeColor = Color.Black;
            view.MultiSelect = false;
            view.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            view.ReadOnly = false;
            view.AllowUserToAddRows = false;
            view.AllowUserToDeleteRows = false;
            view.AutoGenerateColumns = false;

            view.Columns.Remove("ProjectId");
            view.Columns.Remove("Goal");
            view.Columns.Remove("Conclusion");
            view.Columns.Remove("Observation");
            view.Columns.Remove("DateCreated");
            view.Columns.Remove("DateUpdated");
            view.Columns.Remove("GetRowState");
            view.Columns.Remove("User");
            view.Columns.Remove("Project");
            view.Columns.Remove("LaboBasicData");
            view.Columns.Remove("CrudState");
            view.Columns.Remove("ViscosityProfile");
            view.Columns["IsDeleted"].Visible = false;
            view.Columns["UserId"].Visible = false;

            view.Columns["Id"].HeaderText = "Nr D";
            view.Columns["Id"].ReadOnly = true;
            view.Columns["Id"].DisplayIndex = 0;
            view.Columns["Id"].Width = _formData.ContainsKey("Id") ? (int)_formData["Id"] : 100;
            view.Columns["Id"].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns["Title"].HeaderText = "Tytuł";
            view.Columns["Title"].DisplayIndex = 1;
            view.Columns["Title"].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns["ProjectName"].HeaderText = "Projekt";
            view.Columns["ProjectName"].DisplayIndex = 2;
            view.Columns["ProjectName"].ReadOnly = true;
            view.Columns["ProjectName"].Width = _formData.ContainsKey("ProjectName") ? (int)_formData["ProjectName"] : 50;
            view.Columns["ProjectName"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["ProjectName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            
            view.Columns["Density"].HeaderText = "Ges.";
            view.Columns["Density"].DisplayIndex = 3;
            view.Columns["Density"].Width = _formData.ContainsKey("Density") ? (int)_formData["Density"] : 70;
            view.Columns["Density"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Density"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["UserShortcut"].HeaderText = "User";
            view.Columns["UserShortcut"].DisplayIndex = 4;
            view.Columns["UserShortcut"].ReadOnly = true;
            view.Columns["UserShortcut"].Width = _formData.ContainsKey("UserShortcut") ? (int)_formData["UserShortcut"] : 50;
            view.Columns["UserShortcut"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["UserShortcut"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            int width = view.Width - CommonData.HEADER_WIDTH_ADMIN - view.Columns["Id"].Width - view.Columns["Density"].Width - view.Columns["UserShortcut"].Width;
            if (view.ScrollBars == ScrollBars.Vertical || view.ScrollBars == ScrollBars.Both)
            {
                width -= SystemInformation.VerticalScrollBarWidth;
            }

            view.Columns["Title"].Width = _formData.ContainsKey("Title") ? (int)_formData["Title"] : width - 2;
        }

        private void PrepareComboBoxes()
        {
            _form.GetCmbGlossClass.DataSource = _glossClassList;
            _form.GetCmbGlossClass.ValueMember = ID;
            _form.GetCmbGlossClass.DisplayMember = NAME_PL;
            _form.GetCmbGlossClass.SelectedIndexChanged += CmbGlossClass_SelectedIndexChanged;

            _form.GetCmbContrastClass.DataSource = _contrastClassList;
            _form.GetCmbContrastClass.ValueMember = ID;
            _form.GetCmbContrastClass.DisplayMember = NAME_PL;
            _form.GetCmbContrastClass.SelectedIndexChanged += CmbContrastClass_SelectedIndexChanged;

            _form.GetCmbScrubClass.DataSource = _scrubClassList;
            _form.GetCmbScrubClass.ValueMember = ID;
            _form.GetCmbScrubClass.DisplayMember = NAME_PL;
            _form.GetCmbScrubClass.SelectedIndexChanged += CmbScrubClass_SelectedIndexChanged;

            _form.GetCmbVocClass.DataSource = _vocClassList;
            _form.GetCmbVocClass.ValueMember = ID;
            _form.GetCmbVocClass.DisplayMember = NAME_PL;
            _form.GetCmbVocClass.SelectedIndexChanged += CmbVocClass_SelectedIndexChanged;
        }

        #endregion


        #region Current/Binding/Navigation

        private void LaboBinding_PositionChanged(object sender, EventArgs e)
        {
            _cmbBlock = true;
            bool admin = false;
            bool deleted = true;

            #region Set Current Controls

            if (CurrentLabBook != null)
            {
                DateTime date = Convert.ToDateTime(CurrentLabBook.DateCreated);
                string show = date.ToString("dd-MM-yyyy");
                _form.GetLblDateCreated.Text = "Utworzenie: " + show;
                _form.GetLblDateCreated.Left = _form.ClientSize.Width - _form.GetLblDateCreated.Width - 10;

                date = Convert.ToDateTime(CurrentLabBook.DateUpdated);
                show = date.ToString("dd-MM-yyyy");
                _form.GetLblDateModified.Text = "Modyfikacja: " + show;
                _form.GetLblDateModified.Left = _form.ClientSize.Width - _form.GetLblDateModified.Width - 10;

                string nr = "D-" + CurrentLabBook.Id.ToString();
                _form.GetLblNrD.Text = nr;

                string projectName = CurrentLabBook.Project != null ? CurrentLabBook.Project.Title : "-- Brak --";
                string project = "Projekt #" + CurrentLabBook.ProjectId + " - '" + projectName + "'";
                _form.GetLblProject.Text = project;                             
                _form.GetBtnProjectChange.Left = _form.GetLblProject.Left + _form.GetLblProject.Width + 10;
            }
            else
            {
                _form.GetLblNrD.Text = "D-Brak";
                _form.GetLblDateCreated.Text = "Utworzenie: Brak";
                _form.GetLblDateModified.Text = "Modyfikacja: Brak";
                _form.GetLblProject.Text = "Projekt - Brak";
            }

            #endregion

            #region Create Basic Data if not exist

            _laboBasicBinding.EndEdit();
            LaboDataBasicDto basicCurrent = CurrentLabBook != null ? CurrentLabBook.LaboBasicData : null; // _laboBasicList.Where(i => i.LaboId == _currentLabBook.Id).FirstOrDefault() : null;
            if (CurrentLabBook != null && basicCurrent == null)
            {
                basicCurrent = CreateEmptyLabodataBasic(CurrentLabBook); 
                CurrentLabBook.LaboBasicData = basicCurrent;
            }

            _laboBasicBinding.DataSource = basicCurrent;

            #endregion

            #region Synchronize Combo Gloss Class

            if (basicCurrent != null)
            {
                _form.GetCmbGlossClass.SelectedValue = basicCurrent.GlossClassId;
            }
            else
            {
                _form.GetCmbGlossClass.SelectedIndex = _form.GetCmbGlossClass.Items.Count > 0 ? 0 : -1;
            }

            #endregion

            #region Synchronize Combo Contrast Class

            if (basicCurrent != null)
            {
                _form.GetCmbContrastClass.SelectedValue = basicCurrent.ContrastClassId;
            }
            else
            {
                _form.GetCmbContrastClass.SelectedIndex = _form.GetCmbContrastClass.Items.Count > 0 ? 0 : -1;
            }

            #endregion

            #region Synchronize Combo Scrub Class

            if (basicCurrent != null)
            {
                _form.GetCmbScrubClass.SelectedValue = basicCurrent.ScrubClassId;
            }
            else
            {
                _form.GetCmbScrubClass.SelectedIndex = _form.GetCmbScrubClass.Items.Count > 0 ? 0 : -1;
            }

            #endregion

            #region Synchronize Combo VOC Class

            if (basicCurrent != null)
            {
                _form.GetCmbVocClass.SelectedValue = basicCurrent.VocClassId;
            }
            else
            {
                _form.GetCmbVocClass.SelectedIndex = _form.GetCmbVocClass.Items.Count > 0 ? 0 : -1;
            }

            #endregion

            #region Synchronize DataGridViews

            if (CurrentLabBook != null)
            {
                _viscosityService.SynchronizeData(CurrentLabBook.Id);
                _contrastService.SynchronizeData(CurrentLabBook.Id);
                _normTestService.SynchronizeData(CurrentLabBook.Id);
           }

            #endregion

            #region Block Controls

            if (CurrentLabBook != null)
            {
                admin = CurrentLabBook.UserId == _user.Id || IsAdmin ? true : false;
                deleted = CurrentLabBook.IsDeleted;
            }

            if (deleted)
            {
                FullBlockControls();
            }
            else if (!admin)
            {
                BlockControlsForNotAdmin();
            }
            else
            {
                FullUnblockControls();
            }

            #endregion

            _cmbBlock = false;
        }

        private void SetViscosityVisbility(LaboDataViscosityColDto profile)
        {
            LaboDataViscosityColDto col;

            if (profile != null)
            {
                col = profile;
            }
            else
            {
                col = new LaboDataViscosityColDto(CurrentLabBook.Id, Profile.STD_X, "");
            }

            DataGridView view = _form.GetDgvViscosity;
            view.Columns["Del"].Visible = true;
            view.Columns["pH"].Visible = false;
            view.Columns["Brook1"].Visible = false;
            view.Columns["Brook5"].Visible = false;
            view.Columns["Brook10"].Visible = false;
            view.Columns["Brook20"].Visible = false;
            view.Columns["Brook30"].Visible = false;
            view.Columns["Brook40"].Visible = false;
            view.Columns["Brook50"].Visible = false;
            view.Columns["Brook60"].Visible = false;
            view.Columns["Brook70"].Visible = false;
            view.Columns["Brook80"].Visible = false;
            view.Columns["Brook90"].Visible = false;
            view.Columns["Brook100"].Visible = false;
            view.Columns["BrookDisc"].Visible = false;
            view.Columns["BrookComment"].Visible = false;
            view.Columns["BrookXvisc"].Visible = false;
            view.Columns["BrookXrpm"].Visible = false;
            view.Columns["BrookXdisc"].Visible = false;
            view.Columns["Krebs"].Visible = false;
            view.Columns["KrebsComment"].Visible = false;
            view.Columns["ICI"].Visible = false;
            view.Columns["IciDisc"].Visible = false;
            view.Columns["IciComment"].Visible = false;

            IList<string> profiles = LabBookViscosityColumnService.Profiles[col.Profile];
            foreach (string column in profiles)
            {
                view.Columns[column].Visible = true;
            }
        }

        private LaboDataBasicDto CreateEmptyLabodataBasic(LaboDto labo)
        {
            return new LaboDataBasicDto.Builder()
                    .LaboId(labo.Id)
                    .GlossClassId(1)
                    .ContrastClassId(1)
                    .ScrubClassId(1)
                    .VocClassId(1)
                    .Date(DateTime.Today)
                    .Service(this)
                    .Build();
        }

        #endregion


        #region ComboBox Events

        private void CmbGlossClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cmbBlock)
                return;
            
            if (_currentLabBookBasic != null)
            {
                CmbGlossClassDto cmb = (CmbGlossClassDto)_form.GetCmbGlossClass.SelectedItem;
                byte cmbId = cmb.Id;
                if (cmb != null && (_currentLabBookBasic.GlossClassId != cmbId))
                {
                    _currentLabBookBasic.GlossClassId = cmbId;
                    _laboBasicBinding.EndEdit();
                }
            }
        }

        private void CmbContrastClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cmbBlock)
                return;

            if (_currentLabBookBasic != null)
            {
                CmbContrastClassDto cmb = (CmbContrastClassDto)_form.GetCmbContrastClass.SelectedItem;
                byte cmbId = cmb.Id;
                if (cmb != null && (_currentLabBookBasic.ContrastClassId != cmbId))
                {
                    _currentLabBookBasic.ContrastClassId = cmbId;
                    _laboBasicBinding.EndEdit();
                }
            }
        }

        private void CmbScrubClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cmbBlock)
                return;

            if (_currentLabBookBasic != null)
            {
                CmbScrubClassDto cmb = (CmbScrubClassDto)_form.GetCmbScrubClass.SelectedItem;
                byte cmbId = cmb.Id;
                if (cmb != null && (_currentLabBookBasic.ScrubClassId != cmbId))
                {
                    _currentLabBookBasic.ScrubClassId = cmbId;
                    _laboBasicBinding.EndEdit();
                }
            }

        }

        private void CmbVocClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cmbBlock)
                return;

            if (_currentLabBookBasic != null)
            {
                CmbVocClassDto cmb = (CmbVocClassDto)_form.GetCmbVocClass.SelectedItem;
                byte cmbId = cmb.Id;
                if (cmb != null && (_currentLabBookBasic.VocClassId != cmbId))
                {
                    _currentLabBookBasic.VocClassId = cmbId;
                    _laboBasicBinding.EndEdit();
                }
            }
        }

        #endregion


        #region Block Controls

        private void FullBlockControls()
        {
            _form.GetBtnDelete.Enabled = false;
            _form.GetBtnProjectChange.Enabled = false;
            _form.GetBtnUp.Enabled = false;
            _form.GetBtnDown.Enabled = false;
            _form.GetTxtTitle.Enabled = false;
            _form.GetTxtConclusion.Enabled = false;
            _form.GetTxtObservation.Enabled = false;

            _form.GetPageBasic.Enabled = false;
            _form.GetMainMenu.Enabled = false;

            _form.GetDgvLabo.Columns["Id"].ReadOnly = true;
            _form.GetDgvLabo.Columns["Title"].ReadOnly = true;
            _form.GetDgvLabo.Columns["ProjectName"].ReadOnly = true;
            _form.GetDgvLabo.Columns["Density"].ReadOnly = true;
            _form.GetDgvLabo.Columns["UserShortcut"].ReadOnly = true;

            _form.GetDgvViscosity.Enabled = false;
            _form.GetDgvContrast.Enabled = false;
            _form.GetDgvNormTest.Enabled = false;
        }

        private void FullUnblockControls()
        {
            _form.GetBtnDelete.Enabled = true;
            _form.GetBtnProjectChange.Enabled = true;
            _form.GetBtnUp.Enabled = true;
            _form.GetBtnDown.Enabled = true;
            _form.GetTxtTitle.Enabled = true;
            _form.GetTxtConclusion.Enabled = true;
            _form.GetTxtObservation.Enabled = true;

            _form.GetPageBasic.Enabled = true;
            _form.GetMainMenu.Enabled = true;

            _form.GetDgvLabo.Columns["Id"].ReadOnly = true;
            _form.GetDgvLabo.Columns["Title"].ReadOnly = false;
            _form.GetDgvLabo.Columns["ProjectName"].ReadOnly = true;
            _form.GetDgvLabo.Columns["Density"].ReadOnly = false;
            _form.GetDgvLabo.Columns["UserShortcut"].ReadOnly = true;

            _form.GetDgvViscosity.Enabled = true;
            _form.GetDgvContrast.Enabled = true;
            _form.GetDgvNormTest.Enabled = true;
        }

        private void BlockControlsForNotAdmin()
        {
            _form.GetBtnDelete.Enabled = false;
            _form.GetBtnProjectChange.Enabled = false;
            _form.GetBtnUp.Enabled = false;
            _form.GetBtnDown.Enabled = false;
            _form.GetTxtTitle.Enabled = false;
            _form.GetTxtConclusion.Enabled = false;
            _form.GetTxtObservation.Enabled = false;

            _form.GetPageBasic.Enabled = false;
            _form.GetMainMenu.Enabled = true;

            _form.GetDgvLabo.Columns["Id"].ReadOnly = true;
            _form.GetDgvLabo.Columns["Title"].ReadOnly = true;
            _form.GetDgvLabo.Columns["ProjectName"].ReadOnly = true;
            _form.GetDgvLabo.Columns["Density"].ReadOnly = true;
            _form.GetDgvLabo.Columns["UserShortcut"].ReadOnly = true;

            _form.GetDgvViscosity.Enabled = true;
            _form.GetDgvContrast.Enabled = true;
            _form.GetDgvNormTest.Enabled = true;
        }

        #endregion


        #region DataGridView Events

        public void ResizeLaboColumn(DataGridViewColumnEventArgs e)
        {
            _form.GetBtnFilterCancel.Size = new Size(_form.GetTxtFilterTitle.Height, _form.GetTxtFilterTitle.Height);
            _form.GetBtnFilterCancel.Left = _form.GetDgvLabo.Left + (CommonData.HEADER_WIDTH_ADMIN / 2) - (_form.GetBtnFilterCancel.Size.Width / 2);

            _form.GetTxtFilterNumD.Width = _form.GetDgvLabo.Columns["Id"].Width - 1;
            _form.GetTxtFilterNumD.Left = _form.GetDgvLabo.Left + CommonData.HEADER_WIDTH_ADMIN;

            _form.GetTxtFilterTitle.Width = _form.GetDgvLabo.Columns["Title"].Width - 2;
            _form.GetTxtFilterTitle.Left = _form.GetDgvLabo.Left + _form.GetDgvLabo.Columns["Id"].Width + CommonData.HEADER_WIDTH_ADMIN;

            _form.GetBtnFilterProject.Width = _form.GetDgvLabo.Columns["ProjectName"].Width;
            _form.GetBtnFilterProject.Left = _form.GetDgvLabo.Left + _form.GetDgvLabo.Columns["Id"].Width + _form.GetDgvLabo.Columns["Title"].Width + CommonData.HEADER_WIDTH_ADMIN;

            _form.GetTxtFilterUser.Width = _form.GetDgvLabo.Columns["UserShortcut"].Width;
            _form.GetTxtFilterUser.Left = _form.GetDgvLabo.Left + _form.GetDgvLabo.Columns["Id"].Width + _form.GetDgvLabo.Columns["Title"].Width
                + _form.GetDgvLabo.Columns["ProjectName"].Width + _form.GetDgvLabo.Columns["Density"].Width + CommonData.HEADER_WIDTH_ADMIN;
        }

        public void ResizeContrastColumn()
        {
            DataGridView view = _form.GetDgvContrast;

            view.Columns["DateCreated"].Width = 120;
            int contrastWidth = view.Width - view.RowHeadersWidth - view.Columns["DateCreated"].Width;
            view.Columns["Applicator"].Width = (int)(contrastWidth * 0.27);
            view.Columns["Substrate"].Width = (int)(contrastWidth * 0.13);
            view.Columns["Contrast"].Width = (int)(contrastWidth * 0.11);
            view.Columns["Sp"].Width = (int)(contrastWidth * 0.11);
            view.Columns["Tw"].Width = (int)(contrastWidth * 0.11);
            view.Columns["Comments"].Width = (int)(contrastWidth * 0.27);
        }

        public void DefaultValuesForViscosity(DataGridViewRowEventArgs e)
        {
            int labBookId = 1;
            if (CurrentLabBook != null)
            {
                labBookId = CurrentLabBook.Id;
            }

            LabBookViscosityService service = (LabBookViscosityService)_viscosityService;
            service.AddNew(e, labBookId);
        }

        public void BrightForeColorInDeleted(DataGridViewCellFormattingEventArgs e)
        {
            bool deleted = Convert.ToBoolean(_form.GetDgvLabo.Rows[e.RowIndex].Cells["IsDeleted"].Value);

            if (deleted)
            {
                e.CellStyle.ForeColor = LightGrey;
            }
        }

        public void IconInCellPainting(DataGridViewRowPostPaintEventArgs e)
        {
            long userId = Convert.ToInt32(_form.GetDgvLabo.Rows[e.RowIndex].Cells["UserId"].Value);
            bool deleted = Convert.ToBoolean(_form.GetDgvLabo.Rows[e.RowIndex].Cells["IsDeleted"].Value);

            if (deleted)
            {
                Font font = _form.GetDgvLabo.RowsDefaultCellStyle.Font;
                float size = font.Size - 2;

                string drawString = "Deleted ... Deleted ...";
                StringFormat drawFormat = new StringFormat();
                drawFormat.Alignment = StringAlignment.Center;
                Font drawFont = new Font("Arial", size, FontStyle.Bold);
                int x = _form.GetDgvLabo.RowHeadersWidth + 20;
                int y = e.RowBounds.Top + 4;
                int width = 300;
                int height = e.RowBounds.Height;
                Rectangle drawRect = new Rectangle(x, y, width, height);

                e.Graphics.DrawString(drawString, drawFont, redBrush, drawRect, drawFormat);
            }
            else if (!IsAdmin && !IsValidUser)
            {
                int x = e.RowBounds.Left + 25;
                int width = 4;
                Rectangle rectangleTop = new Rectangle(x, e.RowBounds.Top + 4, width, e.RowBounds.Height - 14);
                Rectangle rectangleBottom = new Rectangle(x, e.RowBounds.Top + e.RowBounds.Height - 8, width, 4);

                e.Graphics.FillRectangle(redBrush, rectangleTop);
                e.Graphics.FillRectangle(redBrush, rectangleBottom);
            }
        }

        public void PaintHeaderForNormTest(DataGridViewRowPostPaintEventArgs e)
        {
            if (_form.Init)
                return;

            bool head = Convert.ToInt32(_form.GetDgvNormTest.Rows[e.RowIndex].Cells["LaboId"].Value) == -1;

            if (head)
            {
                // drive red rectangle on all row
                DataGridView view = _form.GetDgvNormTest;
                string name = _form.GetDgvNormTest.Rows[e.RowIndex].Cells["Norm"].Value.ToString();
                int x = e.RowBounds.Left + CommonData.HEADER_WIDTH_ADMIN;
                int y = e.RowBounds.Top;
                int width = 0;
                foreach (DataGridViewColumn column in view.Columns)
                {
                    if (column.Visible)
                        width += column.Width;
                }
                int height = e.RowBounds.Height;
                Graphics graph = e.Graphics;
                graph.FillRectangle(blueBrush, new Rectangle(x, y, width, height));

                // print name of the group norm test
                Font font = view.RowsDefaultCellStyle.Font;
                StringFormat drawFormat = new StringFormat();
                drawFormat.Alignment = StringAlignment.Center;
                Font drawFont = new Font("Arial", font.Size, FontStyle.Bold);
                Rectangle drawRect = new Rectangle(x, y + 2, width, height);
                e.Graphics.DrawString(name, drawFont, redBrush, drawRect, drawFormat);
            }
        }

        //public void DeleteRow(object sender, DataGridViewCellEventArgs e)
        //{
        //    var grid = (DataGridView)sender;

        //    if (e.RowIndex < 0) return;
        //    if (grid.Columns.Count == 0 || grid.Rows.Count == 0) return;
        //    if (grid.Rows[e.RowIndex].IsNewRow) return;

        //    bool head = Convert.ToInt64(grid.Rows[e.RowIndex].Cells["TmpId"].Value) == -1;
        //    if (!IsAdmin && !IsValidUser && !head)
        //    {
        //        MessageBox.Show("Nie masz uprawnień do usuwania wierszy.", "Brak uprawnień", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //        return;
        //    }

        //    if (grid[e.ColumnIndex, e.RowIndex] is DataGridViewButtonCell)
        //    {
        //        long id = Convert.ToInt64(grid.Rows[e.RowIndex].Cells["Id"].Value);
        //        long tmpId = Convert.ToInt64(grid.Rows[e.RowIndex].Cells["TmpId"].Value);

        //        string name = grid.Name;
        //        switch (name)
        //        {
        //            case "DgvViscosity":
        //                _viscosityService.Delete(id, tmpId);
        //                break;
        //            case "DgvContrast":
        //                _contrastService.Delete(id, tmpId);
        //                break;
        //            case "DgvNormTest":
        //                _normTestService.Delete(id, tmpId);
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //}

        public void AddNewViscosityRow()
        {
            _viscosityService.AddNew(-1);
        }

        #endregion


        #region Insert procedures

        private void InsertNewEmptyLabo()
        {
            ProjectDto project = _projectList
                .Where(i => i.Id == 1)
                .FirstOrDefault();

            LaboDto newLabo = new LaboDto(0, "PUSTY", DateTime.Today, DateTime.Today, project.Id,
                "", null, "", "", false, _user.Id, this)
            {
                User = _user,
                Project = project
            };
            newLabo = SaveNewlabo(newLabo);
            newLabo.LaboBasicData = CreateEmptyLabodataBasic(newLabo);
            newLabo.ViscosityProfile = new LaboDataViscosityColDto(newLabo.Id, Profile.STD, "");

            _ = _repositoryLaboBasic.Save(newLabo.LaboBasicData);
            newLabo.LaboBasicData.AcceptChanges();

            Modify(RowState.ADDED);
        }

        private void InsertCopyLastLabo()
        {
            LaboDto lastLabo = _laboList[_laboList.Count - 1];
            LaboDto newLabo = new LaboDto(0, lastLabo.Title, DateTime.Today, DateTime.Today, lastLabo.ProjectId,
                lastLabo.Goal, null, "", "", false, _user.Id, this)
            {
                User = _user,
                Project = lastLabo.Project
            };

            newLabo = SaveNewlabo(newLabo);
            newLabo.LaboBasicData = CreateEmptyLabodataBasic(newLabo);
            newLabo.ViscosityProfile = new LaboDataViscosityColDto(newLabo.Id, Profile.STD, "");

            _ = _repositoryLaboBasic.Save(newLabo.LaboBasicData);
            newLabo.LaboBasicData.AcceptChanges();
        }

        private void InsertCopyCurrentLabo(LaboDto current)
        {
            if (CurrentLabBook == null)
                return;

            LaboDto newLabo = new LaboDto(0, current.Title, DateTime.Today, DateTime.Today, current.ProjectId,
                current.Goal, null, "", "", false, _user.Id, this)
            {
                User = _user,
                Project = current.Project
            };

            newLabo = SaveNewlabo(newLabo);
            newLabo.LaboBasicData = CreateEmptyLabodataBasic(newLabo);
            newLabo.ViscosityProfile = new LaboDataViscosityColDto(newLabo.Id, Profile.STD, "");

            _ = _repositoryLaboBasic.Save(newLabo.LaboBasicData);
            newLabo.LaboBasicData.AcceptChanges();
        }

        private void InsertCopyCurrentEmptyLabo(LaboDto current)
        {
            if (CurrentLabBook == null)
                return;

            LaboDto newLabo = new LaboDto(0, "PUSTY", DateTime.Today, DateTime.Today, current.ProjectId,
                current.Goal, null, "", "", false, _user.Id, this)
            {
                User = _user,
                Project = current.Project
            };

            newLabo = SaveNewlabo(newLabo);
            newLabo.LaboBasicData = CreateEmptyLabodataBasic(newLabo);
            newLabo.ViscosityProfile = new LaboDataViscosityColDto(newLabo.Id, Profile.STD, "");

            _ = _repositoryLaboBasic.Save(newLabo.LaboBasicData);
            newLabo.LaboBasicData.AcceptChanges();
        }

        #endregion


        #region Buttons Events

        public void ChangeProject()
        {
            using (FindProjectForm form = new FindProjectForm(_projectList))
            {
                form.ShowDialog();
                if (form.Ok && CurrentLabBook != null)
                {
                    CurrentLabBook.ProjectId = form.Result.Id;
                    CurrentLabBook.Project = form.Result;
                    _laboBinding.EndEdit();
                    LaboBinding_PositionChanged(null, null);
                    _form.GetDgvLabo.InvalidateRow(_form.GetDgvLabo.CurrentRow.Index);
                }
            }
        }

        public void AddOneLabBook()
        {
            InsertNewEmptyLabo();
            ClearFiltrationByNewAdd(_laboBinding.Count - 1);
        }

        public void AddSeriesLabBooks()
        {
            if (CurrentLabBook == null)
                return;

            int nr = CurrentLabBook.Id;
            string title = CurrentLabBook.Title;
            int amount = 1;
            int type = 1;
            bool ok = false;

            using (AddSeriesLaboForm form = new AddSeriesLaboForm(nr, title))
            {
                form.ShowDialog();
                amount = form.Amount;
                type = form.Type;
                ok = form.Ok;
            }

            if (!ok)
                return;

            LaboDto current = CurrentLabBook;
            int position = _laboBinding.Count;

            if (type == 1)
            {
                InsertCopyLastLabo();
                position = _laboList.Count - 1;
            }
            else if (type == 2)
            {
                InsertCopyCurrentLabo(current);
                position = _laboList.Count - 1;
            }
            else if (type == 3 && amount > 0)
            {
                for (int i = 0; i < amount; i++)
                {
                    InsertCopyCurrentLabo(current);
                }
                position = _laboList.Count - amount;
            }
            else if (type == 4 && amount > 0)
            {
                InsertCopyCurrentLabo(current);
                for (int i = 0; i < amount - 1; i++)
                {
                    InsertCopyCurrentEmptyLabo(current);
                }
                position = _laboList.Count - amount;

            }
            else if (type == 5 && amount > 0)
            {
                for (int i = 0; i < amount; i++)
                {
                    InsertCopyCurrentEmptyLabo(current);
                }
                position = _laboList.Count - amount;
            }
            else if (type == 6 && amount > 0)
            {
                for (int i = 0; i < amount; i++)
                {
                    InsertNewEmptyLabo();
                }
                position = _laboList.Count - amount;
            }

            ClearFiltrationByNewAdd(position);
        }

        public void DeleteItem(int type)
        {
            switch (type)
            {
                case 0:

                    break;
                case 1:
                    _viscosityService.Delete();
                    break;
                case 2:
                    _contrastService.Delete();
                    break;
                case 3:
                    _normTestService.Delete();
                    break;
                default:

                    break;
            }
        }

        #endregion


        #region Menu

        public void ChangeViscosityProfile(int nr)
        {
            if (CurrentLabBook == null)
                return;

            Profile profile;

            switch (nr)
            {
                case 0:
                    profile = Profile.STD_X;
                    break;
                case 1:
                    profile = Profile.STD_X_SOL;
                    break;
                case 2:
                    profile = Profile.STD;
                    break;
                case 3:
                    profile = Profile.STD_SOL;
                    break;
                case 4:
                    profile = Profile.PRB;
                    break;
                case 5:
                    profile = Profile.KREBS;
                    break;
                case 6:
                    profile = Profile.KREBS_SOL;
                    break;
                case 7:
                    profile = Profile.STD_KREBS;
                    break;
                case 8:
                    profile = Profile.STD_KREBS_SOL;
                    break;
                case 9:
                    profile = Profile.ICI;
                    break;
                case 10:
                    profile = Profile.ICI_SOL;
                    break;
                case 11:
                    profile = Profile.STD_ICI;
                    break;
                case 12:
                    profile = Profile.STD_ICI_SOL;
                    break;
                case 13:
                    profile = Profile.STD_X;
                    break;
                default:
                    profile = Profile.STD_X;
                    break;
            }

            if (profile == Profile.STD_X)
            {
                CurrentLabBook.ViscosityProfile = null;
            }
            else
            {
                CurrentLabBook.ViscosityProfile = new LaboDataViscosityColDto(CurrentLabBook.Id, profile, "");
            }

            SetViscosityVisbility(CurrentLabBook.ViscosityProfile);
        }

        public void ApplicatorInsert(int type)
        {
            _contrastService.AddNew(type);
        }

        public void OpenMaterialForm()
        {
            using (MaterialForm form = new MaterialForm(_connection, _user))
            {
                form.ShowDialog();
            }
        }

        public void OpenCompositionForm()
        {
            if (CurrentLabBook == null)
                return;

            using (CompositionForm form = new CompositionForm(_connection, _user, CurrentLabBook))
            {
                form.ShowDialog();
            }
        }

        #endregion


        #region Filtering

        public bool IsFiltrationSet()
        {
            string number = _form.GetTxtFilterNumD.Text;
            string title = _form.GetTxtFilterTitle.Text;
            string user = _form.GetTxtFilterUser.Text;

            return !string.IsNullOrEmpty(number) | !string.IsNullOrEmpty(title) | !string.IsNullOrEmpty(user) | _form.GetBtnFilterProject.Text != CommonData.ALL_DATA_PL;
        }

        public void ClearFiltrationByButton()
        {
            if (!IsFiltrationSet())
                return;

            _filterBlock = true;

            _form.GetTxtFilterNumD.Text = "";
            _form.GetTxtFilterTitle.Text = "";
            _form.GetTxtFilterUser.Text = "";
            _form.GetBtnFilterProject.Text = CommonData.ALL_DATA_PL;

            int position = -1;
            if (CurrentLabBook != null)
            {
                int id = CurrentLabBook.Id;

                var current = _laboList
                    .Where(i => i.Id == id)
                    .FirstOrDefault();
                position = current != null ? _laboList.IndexOf(current) : -1;
            }

            _laboBinding.DataSource = _laboList;
            _laboBinding.Position = position;

            _filterBlock = false;
        }

        public void ClearFiltrationByNewAdd(int position)
        {
            if (!IsFiltrationSet())
            {
                _laboBinding.ResetBindings(false);
                _laboBinding.Position = position;
            }
            else
            {

                _filterBlock = true;

                _form.GetTxtFilterNumD.Text = string.Empty;
                _form.GetTxtFilterTitle.Text = string.Empty;
                _form.GetTxtFilterUser.Text = string.Empty;
                _form.GetBtnFilterProject.Text = CommonData.ALL_DATA_PL;

                _laboBinding.DataSource = _laboList;
                _laboBinding.Position = position;

                _filterBlock = false;
            }
        }

        public void SetFiltration()
        {
            if (_filterBlock)
                return;

            string nr = _form.GetTxtFilterNumD.Text;
            string title = _form.GetTxtFilterTitle.Text.ToLower();
            string user = _form.GetTxtFilterUser.Text.ToLower();
            string project = _form.GetBtnFilterProject.Text == CommonData.ALL_DATA_PL ? "" : _form.GetBtnFilterProject.Text;

            if (nr.Length > 0 && !Regex.IsMatch(nr, @"^\d+$"))
            {
                MessageBox.Show("Wprowadzona wartośc nie jest liczba całkowitą. Popraw wartość", "Błąd wartości", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (IsFiltrationSet())
            {
                int id = nr.Length > 0 ? Convert.ToInt32(nr) : -1;

                List<LaboDto> filter = _laboList
                    .Where(i => i.Id >= id)
                    .Where(i => string.IsNullOrEmpty(title) || i.Title.ToLower().Contains(title))
                    .Where(i => string.IsNullOrEmpty(project) || i.ProjectName.Contains(project))
                    .Where(i => string.IsNullOrEmpty(user) || i.UserShortcut.ToLower().Contains(user))
                    .ToList();

                _laboBinding.DataSource = filter;
            }
            else
            {
                _laboBinding.DataSource = _laboList;
            }

            LaboBinding_PositionChanged(null, null);
        }

        public void FilterByProject()
        {
            using (FindProjectForm form = new FindProjectForm(_projectList))
            {
                form.ShowDialog();
                if (form.Ok)
                {
                    _form.GetBtnFilterProject.Text = form.Result.Title;
                    SetFiltration();
                }
            }
        }

        #endregion


        #region Parse and Validating

        private void TxtDouble_Parse(object sender, ConvertEventArgs e)
        {
            if (e.Value.Equals(""))
                e.Value = null;
        }

        #endregion


        #region CRUD

        private LaboDto SaveNewlabo(LaboDto labo)
        {
            LabBookRepository repository = (LabBookRepository)_repositoryLabo;
            LaboDto newLabo = repository.AddNewLabo(labo);

            if (newLabo.CrudState == CrudState.OK)
            {
                newLabo.AcceptChanges();
                _laboList.Add(newLabo);
            }

            return newLabo;
        }

        public void Save()
        {
            _laboBinding.EndEdit();
            _laboBasicBinding.EndEdit();    

            if (Enum.TryParse(_user.Permission.ToUpper(), out Permission permission))
            {
                permission = Permission.USER;
            }

            UpdateLabo(permission);
            SaveBasicData(permission);

            _viscosityService.Save();
            _contrastService.Save();
            _normTestService.Save();

            Modify(RowState.UNCHANGED);
        }

        private bool UpdateLabo(Permission permission)
        {
            IList<LaboDto> updated = _laboList
                .Where(i => i.GetRowState == RowState.MODIFIED)
                .ToList();

            foreach (LaboDto labo in updated) 
            {
                if (permission == Permission.ADMIN || (permission == Permission.USER && _user.Id == labo.UserId))
                {
                    CrudState answer = _repositoryLabo.Update(labo).CrudState;
                    if (answer == CrudState.OK)
                    {
                        SaveViscosityColumn(labo);
                        labo.AcceptChanges();
                    }
                    else
                        return false;
                }
                else
                {
                    labo.AcceptChanges();
                }
            }

            return true;
        }

        private bool SaveBasicData(Permission permission)
        {
            #region Save new

            var added = _laboList
                .Where(i => i.LaboBasicData != null)
                .Select(i => new { i.UserId, i.LaboBasicData})
                .Where(i => i.LaboBasicData.GetRowState == RowState.ADDED)
                .ToList();

            foreach (var labo in added)
            {
                if (permission == Permission.ADMIN || (permission == Permission.USER && _user.Id == labo.UserId))
                {
                    CrudState answer = _repositoryLaboBasic.Save(labo.LaboBasicData).CrudState;
                    if (answer == CrudState.OK)
                        labo.LaboBasicData.AcceptChanges();
                    else
                        return false;
                }
                else
                {
                    labo.LaboBasicData.AcceptChanges();
                }
            }

            #endregion

            #region Update

            var modified = _laboList
                .Where(i => i.LaboBasicData != null)
                .Select(i => new { i.UserId, i.LaboBasicData })
                .Where(i => i.LaboBasicData.GetRowState == RowState.MODIFIED)
                .ToList();

            foreach (var labo in modified)
            {
                if (permission == Permission.ADMIN || (permission == Permission.USER && _user.Id == labo.UserId))
                {
                    CrudState answer = _repositoryLaboBasic.Update(labo.LaboBasicData).CrudState;
                    if (answer == CrudState.OK)
                        labo.LaboBasicData.AcceptChanges();
                    else
                        return false;
                }
                else
                {
                    labo.LaboBasicData.AcceptChanges();
                }
            }

            #endregion

            return true;
        }

        private void SaveViscosityColumn(LaboDto labo)
        {
            _repositoryViscosityCol.DeleteById(labo.Id);
            if (labo.ViscosityProfile != null)
            {
                _repositoryViscosityCol.Save(labo.ViscosityProfile);
            }
        }

        #endregion
    }
}
