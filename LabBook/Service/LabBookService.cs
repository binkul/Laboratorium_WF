using Laboratorium.ADO;
using Laboratorium.ADO.DTO;
using Laboratorium.ADO.Repository;
using Laboratorium.ADO.Service;
using Laboratorium.Commons;
using Laboratorium.LabBook.Forms;
using Laboratorium.LabBook.Repository;
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
        private const int HEADER_WIDTH = 35;
        private const string FORM_TOP = "Form_Top";
        private const string FORM_LEFT = "Form_Left";
        private const string FORM_WIDTH = "Form_Width";
        private const string FORM_HEIGHT = "Form_Height";
        private const string FORM_DATA = "LabBookForm";

        private const string ID = "Id";
        private const string NAME_PL = "NamePl";

        bool _cmbBlock = false;
        private readonly IExtendedCRUD<LaboDto> _repositoryLabo;
        private readonly IBasicCRUD<LaboDataBasicDto> _repositoryLaboBasic;
        private readonly IExtendedCRUD<UserDto> _repositoryUser;
        private readonly IBasicCRUD<ProjectDto> _repositoryProject;
        private readonly IBasicCRUD<LaboDataViscosityDto> _repositoryViscosity;
        private readonly IBasicCRUD<LaboDataViscosityColDto> _repositoryViscosityCol;
        private readonly SqlConnection _connection;
        private readonly UserDto _user;
        private readonly LabForm _form;

        private IList<LaboDto> _laboList;
        private BindingSource _laboBinding;
        private IList<LaboDataBasicDto> _laboBasicList;
        private BindingSource _laboBasicBinding;
        private IList<LaboDataViscosityDto> _laboViscosityList;
        private BindingSource _laboViscosityBinding;
        private IList<LaboDataViscosityColDto> _laboViscosityColList;
        private IList<UserDto> _userList;
        private IList<ProjectDto> _projectList;
        private IList<ContrastClassDto> _contrastClassList;
        private IList<GlossClassDto> _glossClassList;
        private IList<ScrubClassDto> _scrubClassList;
        private IList<VocClassDto> _vocClassList;

        private IDictionary<string, double> _formData = CommonFunction.LoadWindowsDataAsDictionary(FORM_DATA);
        private LaboDto _currentLabBook => _laboBinding != null && _laboBinding.Count > 0 ? (LaboDto)_laboBinding.Current : null;
        private LaboDataBasicDto _currentLabBookBasic => _laboBasicBinding != null ? (LaboDataBasicDto)_laboBasicBinding.Current : null;
        public LabBookService(SqlConnection connection, UserDto user, LabForm form)
        {
            _connection = connection;
            _user = user;
            _form = form;
            _repositoryLabo = new LabBookRepository(_connection, this);
            _repositoryLaboBasic = new LabBookBasicDataRepository(_connection, this);
            _repositoryViscosity = new LabBookViscosityRepository(_connection, this);
            _repositoryViscosityCol = new LabBookViscosityColRepository(_connection);
            _repositoryUser = new UserRepository(_connection);
            _repositoryProject = new ProjectRepository(_connection);
        }

        public void Modify(RowState state)
        {
            if (_form.Init)
                return;

            bool laboModify = _laboList.Where(i => i.GetRowState != RowState.UNCHANGED).Any();
            bool basicModify = _laboBasicList.Where(i => i.GetRowState != RowState.UNCHANGED).Any();
            bool visModify = _laboViscosityList.Where(i => i.GetRowState != RowState.UNCHANGED).Any();

            _form.ActivateSave(laboModify | basicModify | visModify);
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

            list.Add(FORM_TOP, _form.Left);
            list.Add(FORM_LEFT, _form.Top);
            list.Add(FORM_WIDTH, _form.Width);
            list.Add(FORM_HEIGHT, _form.Height);

            AddLaboColumnData(list);
            AddViscosityColumnData(list);
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

        #endregion


        #region Prepare Data

        public void PrepareAllData()
        {
            #region Tables/Views/Bindings for Main Labo

            LoadLaboData();
            _userList = _repositoryUser.GetAll();
            _projectList = _repositoryProject.GetAll();


            _laboBasicList = _repositoryLaboBasic.GetAll();
            _laboBasicBinding = new BindingSource();

            _laboViscosityList = _repositoryViscosity.GetAllByLaboId(-1);
            _laboViscosityBinding = new BindingSource();
            _laboViscosityBinding.DataSource = _laboViscosityList;
            _laboViscosityColList = _repositoryViscosityCol.GetAll();

            IBasicCRUD<ContrastClassDto> contrast = new ContrastClassRepository(_connection);
            _contrastClassList = contrast.GetAll();

            IBasicCRUD<GlossClassDto> gloss = new GlossClassRepository(_connection);
            _glossClassList = gloss.GetAll();

            IBasicCRUD<ScrubClassDto> scrub = new ScrubClassRepository(_connection);
            _scrubClassList = scrub.GetAll();

            IBasicCRUD<VocClassDto> voc = new VocClassRepository(_connection);
            _vocClassList = voc.GetAll();


            FillDependece();

            #endregion

            #region Prepare DgvLabBook

            PrepareDgvLabo();

            #endregion

            #region Prepare DgvLabViscosity

            PrepareDgvViscosity();

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

                LaboDataBasicDto basicData = _laboBasicList
                    .Where(i => i.LaboId == labo.Id)
                    .FirstOrDefault();
                if (basicData != null)
                    labo.LaboBasicData = basicData;

                LaboDataViscosityColDto profile = _laboViscosityColList
                    .Where(i => i.LaboId == labo.Id)
                    .FirstOrDefault();
                if (profile != null)
                {
                    labo.ViscosityProfile = profile;
                    labo.AcceptChanged();
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
            view.RowHeadersWidth = HEADER_WIDTH;
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

            int width = view.Width - HEADER_WIDTH - view.Columns["Id"].Width - view.Columns["Density"].Width - view.Columns["UserShortcut"].Width;
            if (view.ScrollBars == ScrollBars.Vertical || view.ScrollBars == ScrollBars.Both)
            {
                width -= SystemInformation.VerticalScrollBarWidth;
            }

            view.Columns["Title"].Width = _formData.ContainsKey("Title") ? (int)_formData["Title"] : width - 2;
        }

        private void PrepareDgvViscosity()
        {
            DataGridView view = _form.GetDgvViscosity;
            view.DataSource = _laboViscosityBinding;
            view.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.RowsDefaultCellStyle.Font = new Font(view.DefaultCellStyle.Font.Name, 9, FontStyle.Regular);
            view.ColumnHeadersDefaultCellStyle.Font = new Font(view.DefaultCellStyle.Font.Name, 10, FontStyle.Bold);
            view.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            view.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            view.RowHeadersWidth = HEADER_WIDTH;
            view.DefaultCellStyle.ForeColor = Color.Black;
            view.MultiSelect = false;
            view.SelectionMode = DataGridViewSelectionMode.CellSelect;
            view.ReadOnly = false;
            view.AllowUserToAddRows = true;
            view.AllowUserToDeleteRows = false;
            view.AutoGenerateColumns = false;

            view.Columns.Remove("GetRowState");
            view.Columns.Remove("CrudState");
            view.Columns["Id"].Visible = false;
            view.Columns["LaboId"].Visible = false;
            view.Columns["Service"].Visible = false;

            view.Columns["ToCompare"].HeaderText = "X";
            view.Columns["ToCompare"].DisplayIndex = 0;
            view.Columns["ToCompare"].Width = _formData.ContainsKey("ToCompare") ? (int)_formData["ToCompare"] : 30;
            view.Columns["ToCompare"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["ToCompare"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["DateCreated"].HeaderText = "Start";
            view.Columns["DateCreated"].DisplayIndex = 1;
            view.Columns["DateCreated"].Width = _formData.ContainsKey("DateCreated") ? (int)_formData["DateCreated"] : 100;
            view.Columns["DateCreated"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["DateCreated"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["DateUpdated"].HeaderText = "Koniec";
            view.Columns["DateUpdated"].DisplayIndex = 2;
            view.Columns["DateUpdated"].Width = _formData.ContainsKey("DateUpdated") ? (int)_formData["DateUpdated"] : 100;
            view.Columns["DateUpdated"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["DateUpdated"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["Days"].HeaderText = "Doba";
            view.Columns["Days"].DisplayIndex = 3;
            view.Columns["Days"].Width = _formData.ContainsKey("Days") ? (int)_formData["Days"] : 100;
            view.Columns["Days"].ReadOnly = true;
            view.Columns["Days"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Days"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["Temp"].HeaderText = "Temp";
            view.Columns["Temp"].DisplayIndex = 4;
            view.Columns["Temp"].Width = _formData.ContainsKey("Temp") ? (int)_formData["Temp"] : 100;
            view.Columns["Temp"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Temp"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["pH"].HeaderText = "pH";
            view.Columns["pH"].DisplayIndex = 5;
            view.Columns["pH"].Width = _formData.ContainsKey("pH") ? (int)_formData["pH"] : 100;
            view.Columns["pH"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["pH"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["Brook1"].HeaderText = "Lep 1";
            view.Columns["Brook1"].DisplayIndex = 6;
            view.Columns["Brook1"].Width = _formData.ContainsKey("Brook1") ? (int)_formData["Brook1"] : 100;
            view.Columns["Brook1"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Brook1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["Brook5"].HeaderText = "Lep 5";
            view.Columns["Brook5"].DisplayIndex = 7;
            view.Columns["Brook5"].Width = _formData.ContainsKey("Brook5") ? (int)_formData["Brook5"] : 100;
            view.Columns["Brook5"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Brook5"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["Brook10"].HeaderText = "Lep 10";
            view.Columns["Brook10"].DisplayIndex = 8;
            view.Columns["Brook10"].Width = _formData.ContainsKey("Brook10") ? (int)_formData["Brook10"] : 100;
            view.Columns["Brook10"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Brook10"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["Brook20"].HeaderText = "Lep 20";
            view.Columns["Brook20"].DisplayIndex = 9;
            view.Columns["Brook20"].Width = _formData.ContainsKey("Brook20") ? (int)_formData["Brook20"] : 100;
            view.Columns["Brook20"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Brook20"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["Brook30"].HeaderText = "Lep 30";
            view.Columns["Brook30"].DisplayIndex = 10;
            view.Columns["Brook30"].Width = _formData.ContainsKey("Brook30") ? (int)_formData["Brook30"] : 100;
            view.Columns["Brook30"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Brook30"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["Brook40"].HeaderText = "Lep 40";
            view.Columns["Brook40"].DisplayIndex = 11;
            view.Columns["Brook40"].Width = _formData.ContainsKey("Brook40") ? (int)_formData["Brook40"] : 100;
            view.Columns["Brook40"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Brook40"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["Brook50"].HeaderText = "Lep 50";
            view.Columns["Brook50"].DisplayIndex = 12;
            view.Columns["Brook50"].Width = _formData.ContainsKey("Brook50") ? (int)_formData["Brook50"] : 100;
            view.Columns["Brook50"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Brook50"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["Brook60"].HeaderText = "Lep 60";
            view.Columns["Brook60"].DisplayIndex = 13;
            view.Columns["Brook60"].Width = _formData.ContainsKey("Brook60") ? (int)_formData["Brook60"] : 100;
            view.Columns["Brook60"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Brook60"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["Brook70"].HeaderText = "Lep 70";
            view.Columns["Brook70"].DisplayIndex = 14;
            view.Columns["Brook70"].Width = _formData.ContainsKey("Brook70") ? (int)_formData["Brook70"] : 100;
            view.Columns["Brook70"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Brook70"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["Brook80"].HeaderText = "Lep 80";
            view.Columns["Brook80"].DisplayIndex = 15;
            view.Columns["Brook80"].Width = _formData.ContainsKey("Brook80") ? (int)_formData["Brook80"] : 100;
            view.Columns["Brook80"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Brook80"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["Brook90"].HeaderText = "Lep 90";
            view.Columns["Brook90"].DisplayIndex = 16;
            view.Columns["Brook90"].Width = _formData.ContainsKey("Brook90") ? (int)_formData["Brook90"] : 100;
            view.Columns["Brook90"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Brook90"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["Brook100"].HeaderText = "Lep 100";
            view.Columns["Brook100"].DisplayIndex = 17;
            view.Columns["Brook100"].Width = _formData.ContainsKey("Brook100") ? (int)_formData["Brook100"] : 100;
            view.Columns["Brook100"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Brook100"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["BrookDisc"].HeaderText = "Dysk";
            view.Columns["BrookDisc"].DisplayIndex = 18;
            view.Columns["BrookDisc"].Width = _formData.ContainsKey("BrookDisc") ? (int)_formData["BrookDisc"] : 70;
            view.Columns["BrookDisc"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["BrookDisc"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["BrookComment"].HeaderText = "Brook uwagi";
            view.Columns["BrookComment"].DisplayIndex = 19;
            view.Columns["BrookComment"].Width = _formData.ContainsKey("BrookComment") ? (int)_formData["BrookComment"] : 200;
            view.Columns["BrookComment"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["BrookComment"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            view.Columns["BrookXvisc"].HeaderText = "Lep X";
            view.Columns["BrookXvisc"].DisplayIndex = 20;
            view.Columns["BrookXvisc"].Width = _formData.ContainsKey("BrookXvisc") ? (int)_formData["BrookXvisc"] : 100;
            view.Columns["BrookXvisc"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["BrookXvisc"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["BrookXrpm"].HeaderText = "Obr. X";
            view.Columns["BrookXrpm"].DisplayIndex = 21;
            view.Columns["BrookXrpm"].Width = _formData.ContainsKey("BrookXrpm") ? (int)_formData["BrookXrpm"] : 100;
            view.Columns["BrookXrpm"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["BrookXrpm"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["BrookXdisc"].HeaderText = "Dysk X";
            view.Columns["BrookXdisc"].DisplayIndex = 22;
            view.Columns["BrookXdisc"].Width = _formData.ContainsKey("BrookXdisc") ? (int)_formData["BrookXdisc"] : 100;
            view.Columns["BrookXdisc"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["BrookXdisc"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            view.Columns["Krebs"].HeaderText = "Krebs";
            view.Columns["Krebs"].DisplayIndex = 23;
            view.Columns["Krebs"].Width = 100;
            view.Columns["Krebs"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Krebs"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["KrebsComment"].HeaderText = "Krebs uwagi";
            view.Columns["KrebsComment"].DisplayIndex = 24;
            view.Columns["KrebsComment"].Width = 200;
            view.Columns["KrebsComment"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["KrebsComment"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            view.Columns["ICI"].HeaderText = "ICI";
            view.Columns["ICI"].DisplayIndex = 25;
            view.Columns["ICI"].Width = 100;
            view.Columns["ICI"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["ICI"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["IciDisc"].HeaderText = "ICI dysk";
            view.Columns["IciDisc"].DisplayIndex = 26;
            view.Columns["IciDisc"].Width = 100;
            view.Columns["IciDisc"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["IciDisc"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["IciComment"].HeaderText = "ICI uwagi";
            view.Columns["IciComment"].DisplayIndex = 27;
            view.Columns["IciComment"].Width = 200;
            view.Columns["IciComment"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["IciComment"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

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


        #region Current/Binkding/Navigation/DataTable

        private void LaboBinding_PositionChanged(object sender, EventArgs e)
        {
            _cmbBlock = true;

            #region Set Current Controls

            if (_currentLabBook != null)
            {
                DateTime date = Convert.ToDateTime(_currentLabBook.DateCreated);
                string show = date.ToString("dd-MM-yyyy");
                _form.GetLblDateCreated.Text = "Utworzenie: " + show;
                _form.GetLblDateCreated.Left = _form.ClientSize.Width - _form.GetLblDateCreated.Width - 10;

                date = Convert.ToDateTime(_currentLabBook.DateUpdated);
                show = date.ToString("dd-MM-yyyy");
                _form.GetLblDateModified.Text = "Modyfikacja: " + show;
                _form.GetLblDateModified.Left = _form.ClientSize.Width - _form.GetLblDateModified.Width - 10;

                string nr = "D-" + _currentLabBook.Id.ToString();
                _form.GetLblNrD.Text = nr;

                string projectName = _currentLabBook.Project != null ? _currentLabBook.Project.Title : "-- Brak --";
                string project = "Projekt #" + _currentLabBook.ProjectId + " - '" + projectName + "'";
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
            LaboDataBasicDto basicCurrent = _currentLabBook != null ? _currentLabBook.LaboBasicData : null; // _laboBasicList.Where(i => i.LaboId == _currentLabBook.Id).FirstOrDefault() : null;
            if (_currentLabBook != null && basicCurrent == null)
            {
                basicCurrent = CreateEmptyLabodataBasic(); 
                _currentLabBook.LaboBasicData = basicCurrent;
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

            #region Set Viscosity data and fields

            if (_currentLabBook != null)
            {
                _laboViscosityList = _repositoryViscosity.GetAllByLaboId(_currentLabBook.Id);
                _laboViscosityBinding.DataSource = _laboViscosityList;

                SetViscosityVisbility(_currentLabBook.ViscosityProfile);
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
                col = new LaboDataViscosityColDto(_currentLabBook.Id, Profile.STD_X, "");
            }

            DataGridView view = _form.GetDgvViscosity;
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

            IList<string> profiles = LabBookViscosityService.Profiles[col.Profile];
            foreach (string column in profiles)
            {
                view.Columns[column].Visible = true;
            }
        }

        private LaboDataBasicDto CreateEmptyLabodataBasic()
        {
            return new LaboDataBasicDto.Builder()
                    .LaboId(_currentLabBook.Id)
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
                GlossClassDto cmb = (GlossClassDto)_form.GetCmbGlossClass.SelectedItem;
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
                ContrastClassDto cmb = (ContrastClassDto)_form.GetCmbContrastClass.SelectedItem;
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
                ScrubClassDto cmb = (ScrubClassDto)_form.GetCmbScrubClass.SelectedItem;
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
                VocClassDto cmb = (VocClassDto)_form.GetCmbVocClass.SelectedItem;
                byte cmbId = cmb.Id;
                if (cmb != null && (_currentLabBookBasic.VocClassId != cmbId))
                {
                    _currentLabBookBasic.VocClassId = cmbId;
                    _laboBasicBinding.EndEdit();
                }
            }
        }

        #endregion


        #region DataGridView Events

        public void ResizeLaboColumn(DataGridViewColumnEventArgs e)
        {
            _form.GetBtnFilterCancel.Size = new Size(_form.GetTxtFilterTitle.Height, _form.GetTxtFilterTitle.Height);
            _form.GetBtnFilterCancel.Left = _form.GetDgvLabo.Left + (HEADER_WIDTH / 2) - (_form.GetBtnFilterCancel.Size.Width / 2);

            _form.GetTxtFilterNumD.Width = _form.GetDgvLabo.Columns["Id"].Width - 1;
            _form.GetTxtFilterNumD.Left = _form.GetDgvLabo.Left + HEADER_WIDTH;

            _form.GetTxtFilterTitle.Width = _form.GetDgvLabo.Columns["Title"].Width - 2;
            _form.GetTxtFilterTitle.Left = _form.GetDgvLabo.Left + _form.GetDgvLabo.Columns["Id"].Width + HEADER_WIDTH;

            _form.GetBtnFilterProject.Width = _form.GetDgvLabo.Columns["ProjectName"].Width;
            _form.GetBtnFilterProject.Left = _form.GetDgvLabo.Left + _form.GetDgvLabo.Columns["Id"].Width + _form.GetDgvLabo.Columns["Title"].Width + HEADER_WIDTH;

            _form.GetTxtFilterUser.Width = _form.GetDgvLabo.Columns["UserShortcut"].Width;
            _form.GetTxtFilterUser.Left = _form.GetDgvLabo.Left + _form.GetDgvLabo.Columns["Id"].Width + _form.GetDgvLabo.Columns["Title"].Width
                + _form.GetDgvLabo.Columns["ProjectName"].Width + _form.GetDgvLabo.Columns["Density"].Width + HEADER_WIDTH;
        }

        public void DefaultValuesForViscosity(DataGridViewRowEventArgs e)
        {
            if (_currentLabBook != null)
            {
                e.Row.Cells["LaboId"].Value = _currentLabBook.Id;
            }
            else
            {
                e.Row.Cells["LaboId"].Value = 1;
            }

            e.Row.Cells["ToCompare"].Value = false;
            e.Row.Cells["Temp"].Value = "20oC";
            e.Row.Cells["DateCreated"].Value = DateTime.Today;
            e.Row.Cells["DateUpdated"].Value = DateTime.Today;
            e.Row.Cells["Service"].Value = this;
        }

        #endregion


        #region Buttons Events

        public void ChangeProject()
        {
            using (FindProjectForm form = new FindProjectForm(_projectList))
            {
                form.ShowDialog();
                if (form.Ok && _currentLabBook != null)
                {
                    _currentLabBook.ProjectId = form.Result.Id;
                    _currentLabBook.Project = form.Result;
                    _laboBinding.EndEdit();
                    LaboBinding_PositionChanged(null, null);
                    _form.GetDgvLabo.InvalidateRow(_form.GetDgvLabo.CurrentRow.Index);
                }
            }
        }

        public void AddOneLabBook()
        {
            LabBookRepository repository = (LabBookRepository)_repositoryLabo;
            LaboDto newLabo = new LaboDto(0, "PUSTY", DateTime.Today, 1, _user.Id, this);
            ProjectDto project = _projectList.Where(i => i.Id == 1).FirstOrDefault();
            LaboDataBasicDto basic = CreateEmptyLabodataBasic();
            newLabo.LaboBasicData = basic;
            newLabo.User = _user;
            newLabo.Project = project;

            newLabo = repository.AddNewLabo(newLabo);

            if (newLabo.CrudState == CrudState.OK)
            {
                newLabo.AcceptChanged();
                _laboBinding.Add(newLabo);
                _laboBinding.EndEdit();
                _form.GetDgvLabo.Refresh();
                int position = _laboList.IndexOf(newLabo);
                _laboBinding.Position = position;
            }
        }

        #endregion


        #region Menu

        public void ChangeViscosityProfile(int nr)
        {
            if (_currentLabBook == null)
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
                _currentLabBook.ViscosityProfile = null;
            }
            else
            {
                _currentLabBook.ViscosityProfile = new LaboDataViscosityColDto(_currentLabBook.Id, profile, "");
            }

            SetViscosityVisbility(_currentLabBook.ViscosityProfile);
        }

        #endregion


        #region Filtering

        public void SetFilter()
        {
            string nr = _form.GetTxtFilterNumD.Text;
            string title = _form.GetTxtFilterTitle.Text;
            string user = _form.GetTxtFilterUser.Text;
            string project = _form.GetBtnFilterProject.Text == CommonData.ALL_DATA_PL ? "" : _form.GetBtnFilterProject.Text;

            if (nr.Length > 0 && !Regex.IsMatch(nr, @"^\d+$"))
            {
                MessageBox.Show("Wprowadzona wartośc nie jest liczba całkowitą. Popraw wartość", "Błąd wartości", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!string.IsNullOrEmpty(nr) || !string.IsNullOrEmpty(title) || !string.IsNullOrEmpty(user) || !string.IsNullOrEmpty(project))
            {
                int id = nr.Length > 0 ? Convert.ToInt32(nr) : -1;

                List<LaboDto> filter = _laboList
                    .Where(i => i.Id >= id)
                    .Where(i => i.Title.ToLower().Contains(title.ToLower()))
                    .Where(i => i.ProjectName.Contains(project))
                    .Where(i => i.UserShortcut.ToLower().Contains(user.ToLower()))
                    .ToList();

                _laboBinding.DataSource = filter;
                _laboBinding.Position = 0;
            }
            else
            {
                _laboBinding.DataSource = _laboList;
                _laboBinding.Position = 0;
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
                    SetFilter();
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

        public void Save()
        {
            _laboBinding.EndEdit();
            _laboBasicBinding.EndEdit();    
            _laboViscosityBinding.EndEdit();

            if (Enum.TryParse(_user.Permission.ToUpper(), out Permission permission))
            {
                permission = Permission.USER;
            }

            UpdateLabo(permission);
            SaveBasicData(permission);
            SaveViscosity();
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
                        labo.AcceptChanged();
                    }
                    else
                        return false;
                }
                else
                {
                    labo.AcceptChanged();
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
                        labo.LaboBasicData.AcceptChanged();
                    else
                        return false;
                }
                else
                {
                    labo.LaboBasicData.AcceptChanged();
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
                        labo.LaboBasicData.AcceptChanged();
                    else
                        return false;
                }
                else
                {
                    labo.LaboBasicData.AcceptChanged();
                }
            }

            #endregion

            return true;
        }

        private bool SaveViscosity()
        {
            #region Save new

            var added = _laboViscosityList
                .Where(i => i.GetRowState == RowState.ADDED)
                .ToList();

            foreach (var vis in added)
            {
                CrudState answer = _repositoryViscosity.Save(vis).CrudState;
                if (answer == CrudState.OK)
                    vis.AcceptChanged();
                else
                    return false;
            }

            #endregion

            #region Update

            var modified = _laboViscosityList
                .Where(i => i.GetRowState == RowState.MODIFIED)
                .ToList();

            foreach (var vis in modified)
            {
                CrudState answer = _repositoryViscosity.Save(vis).CrudState;
                if (answer == CrudState.OK)
                    vis.AcceptChanged();
                else
                    return false;
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
