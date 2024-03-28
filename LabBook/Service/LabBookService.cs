using Laboratorium.ADO.DTO;
using Laboratorium.ADO.Repository;
using Laboratorium.Commons;
using Laboratorium.LabBook.Forms;
using Laboratorium.LabBook.Repository;
using Laboratorium.Project.Forms;
using Laboratorium.Project.Repository;
using Laboratorium.User.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Laboratorium.LabBook.Service
{
    public class LabBookService
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
        private readonly SqlConnection _connection;
        private readonly UserDto _user;
        private readonly LabForm _form;

        private IList<LaboDto> _laboList;
        private BindingSource _laboBinding;
        private IList<LaboDataBasicDto> _laboBasicList;
        private BindingSource _laboBasicBinding;
        private IList<UserDto> _userList;
        private IList<ProjectDto> _projectList;
        private IList<ContrastClassDto> _contrastClassList;
        private IList<GlossClassDto> _glossClassList;
        private IList<ScrubClassDto> _scrubClassList;
        private IList<VocClassDto> _vocClassList;

        private IDictionary<string, double> _formData = CommonFunction.LoadWindowsDataAsDictionary(FORM_DATA);
        private int GetCurrentLabBookId => Convert.ToInt32(_currentLabBook.Id);
        private LaboDto _currentLabBook => _laboBinding != null && _laboBinding.Count > 0 ? (LaboDto)_laboBinding.Current : null;
        private LaboDataBasicDto _currentLabBookBasic => _laboBasicBinding != null ? (LaboDataBasicDto)_laboBasicBinding.Current : null;
        public LabBookService(SqlConnection connection, UserDto user, LabForm form)
        {
            _connection = connection;
            _user = user;
            _form = form;
            _repositoryLabo = new LabBookRepository(_connection);
            _repositoryLaboBasic = new LabBookBasicDataRepository(_connection);
            _repositoryUser = new UserRepository(_connection);
            _repositoryProject = new ProjectRepository(_connection);
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

            foreach (DataGridViewColumn column in _form.GetDgvLabo.Columns)
            {
                if (column.Visible)
                {
                    list.Add(column.Name, column.Width);
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

            LoadLaboData();
            _userList = _repositoryUser.GetAll();
            _projectList = _repositoryProject.GetAll();

            _laboBasicList = _repositoryLaboBasic.GetAll();
            _laboBasicBinding = new BindingSource();

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

            #region Prepare ComboBoxes

            PrepareComboBoxes();

            #endregion

            #region Prepare others control

            _form.GetTxtTitle.DataBindings.Clear();
            _form.GetTxtScrubBrush.DataBindings.Clear();
            _form.GetTxtScrubSponge.DataBindings.Clear();


            _form.GetTxtTitle.DataBindings.Add("Text", _laboBinding, "Title");
            _form.GetTxtScrubBrush.DataBindings.Add("Text", _laboBasicBinding, "ScrubBrush");
            _form.GetTxtScrubSponge.DataBindings.Add("Text", _laboBasicBinding, "ScrubSponge");

            #endregion

            LaboBinding_PositionChanged(null, null);
        }

        private void FillDependece()
        {
            foreach (LaboDto labo in _laboList)
            {
                short id = labo.UserId;
                UserDto user = _userList.Where(i => i.Id == id).FirstOrDefault();
                if (user != null)
                    labo.User = user;

                int projectId = labo.ProjectId;
                ProjectDto project = _projectList.Where(i => i.Id == projectId).FirstOrDefault();
                if (project != null)
                    labo.Project = project;
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
            view.Columns["ProjectName"].Width = _formData.ContainsKey("ProjectName") ? (int)_formData["ProjectName"] : 50; ;
            view.Columns["ProjectName"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["ProjectName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            
            view.Columns["Density"].HeaderText = "Ges.";
            view.Columns["Density"].DisplayIndex = 3;
            view.Columns["Density"].Width = _formData.ContainsKey("Density") ? (int)_formData["Density"] : 70; ;
            view.Columns["Density"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Density"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["UserShortcut"].HeaderText = "User";
            view.Columns["UserShortcut"].DisplayIndex = 4;
            view.Columns["UserShortcut"].ReadOnly = true;
            view.Columns["UserShortcut"].Width = _formData.ContainsKey("UserShortcut") ? (int)_formData["UserShortcut"] : 50; ;
            view.Columns["UserShortcut"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["UserShortcut"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            int width = view.Width - HEADER_WIDTH - view.Columns["Id"].Width - view.Columns["Density"].Width - view.Columns["UserShortcut"].Width;
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

                string project = "Projekt #" + _currentLabBook.ProjectId + " - '" + _currentLabBook.Project.Title + "'";
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
            LaboDataBasicDto basicCurrent = _currentLabBook != null ? _laboBasicList.Where(i => i.LaboId == _currentLabBook.Id).FirstOrDefault() : null;
            if (_currentLabBook != null && basicCurrent != null)
            {
                _laboBasicBinding.DataSource = basicCurrent;
            }
            else if (_currentLabBook != null && basicCurrent == null)
            {
                basicCurrent = new LaboDataBasicDto.Builder()
                    .LaboId(_currentLabBook.Id)
                    .GlossClassId(1)
                    .ContrastClassId(1)
                    .ScrubClassId(1)
                    .VocClassId(1)
                    .Date(DateTime.Today)
                    .Build();
                _laboBasicList.Add(basicCurrent);
            }

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

            _cmbBlock = false;
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
            _form.GetBtnFilterCancel.Size = new Size(_form.GetTxtFilerTitle.Height, _form.GetTxtFilerTitle.Height);
            _form.GetBtnFilterCancel.Left = _form.GetDgvLabo.Left + (HEADER_WIDTH / 2) - (_form.GetBtnFilterCancel.Size.Width / 2);

            _form.GetTxtFilerNumD.Width = _form.GetDgvLabo.Columns["Id"].Width - 1;
            _form.GetTxtFilerNumD.Left = _form.GetDgvLabo.Left + HEADER_WIDTH;

            _form.GetTxtFilerTitle.Width = _form.GetDgvLabo.Columns["Title"].Width - 2;
            _form.GetTxtFilerTitle.Left = _form.GetDgvLabo.Left + _form.GetDgvLabo.Columns["Id"].Width + HEADER_WIDTH;

            _form.GetBtnFilterProject.Width = _form.GetDgvLabo.Columns["ProjectName"].Width;
            _form.GetBtnFilterProject.Left = _form.GetDgvLabo.Left + _form.GetDgvLabo.Columns["Id"].Width + _form.GetDgvLabo.Columns["Title"].Width + HEADER_WIDTH;

            _form.GetTxtFilerUser.Width = _form.GetDgvLabo.Columns["UserShortcut"].Width;
            _form.GetTxtFilerUser.Left = _form.GetDgvLabo.Left + _form.GetDgvLabo.Columns["Id"].Width + _form.GetDgvLabo.Columns["Title"].Width
                + _form.GetDgvLabo.Columns["ProjectName"].Width + _form.GetDgvLabo.Columns["Density"].Width + HEADER_WIDTH;
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

        #endregion


        #region Filtering

        public void SetFilter()
        {
            string nr = _form.GetTxtFilerNumD.Text;
            string title = _form.GetTxtFilerTitle.Text;
            string user = _form.GetTxtFilerUser.Text;
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
    }
}
