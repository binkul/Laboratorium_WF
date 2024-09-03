using Laboratorium.ADO.DTO;
using Laboratorium.ADO.Service;
using Laboratorium.Commons;
using Laboratorium.Composition.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Laboratorium.Composition.Service
{
    public class InsertRecipeService : LoadService
    {
        private const string FORM_DATA = "InsertRecipeForm";
        private const string NUMBER = "Id";
        private const string TITLE = "Title";

        private readonly InsertRecipeForm _form;
        private readonly IList<LaboDto> _laboList;
        private BindingSource _laboBinding;
        private readonly IList<string> _dgvLaboFields = new List<string> { NUMBER, TITLE };

        public InsertRecipeService(InsertRecipeForm form, IList<LaboDto> laboList)
                : base(FORM_DATA, form)
        {
            _form = form;
            _laboList = laboList;
        }

        public LaboDto GetResult => _laboBinding != null && _laboBinding.Count > 0 ? (LaboDto)_laboBinding.Current : null;

        protected override bool Status => false;

        public override void PrepareAllData()
        {
            #region Prepare List and Bindingsource

            _laboBinding = new BindingSource
            {
                DataSource = _laboList
            };

            #endregion

            #region Prepare DataGridView

            DataGridView view = _form.GetDgvLabo;
            view.DataSource = _laboBinding;
            view.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.RowsDefaultCellStyle.Font = new Font(view.DefaultCellStyle.Font.Name, 10, FontStyle.Regular);
            view.ColumnHeadersDefaultCellStyle.Font = new Font(view.DefaultCellStyle.Font.Name, 10, FontStyle.Bold);
            view.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            view.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            view.DefaultCellStyle.ForeColor = Color.Black;
            view.MultiSelect = false;
            view.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            view.ReadOnly = true;
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
            view.Columns.Remove("ProjectName");
            view.Columns.Remove("IsDeleted");
            view.Columns.Remove("UserId");
            view.Columns.Remove("Density");
            view.Columns.Remove("UserShortcut");

            view.Columns[NUMBER].HeaderText = "Nr D";
            view.Columns[NUMBER].DisplayIndex = 0;
            view.Columns[NUMBER].Width = _formData.ContainsKey(NUMBER) ? (int)_formData[NUMBER] : 100;
            view.Columns[NUMBER].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns[TITLE].HeaderText = "Tytuł";
            view.Columns[TITLE].DisplayIndex = 1;
            view.Columns[TITLE].Width = _formData.ContainsKey(TITLE) ? (int)_formData[TITLE] : 100;
            view.Columns[TITLE].SortMode = DataGridViewColumnSortMode.NotSortable;

            #endregion
        }

        protected override void PrepareColumns()
        {
            InsertRecipeForm form = (InsertRecipeForm)_baseForm;
            _fields = new Dictionary<DataGridView, IList<string>>
            {
                { form.GetDgvLabo,  _dgvLaboFields}
            };
        }

        public void ColumnWidthChanged()
        {
            _form.GetTxtFilterNumber.Left = _form.GetDgvLabo.Left + _form.GetDgvLabo.RowHeadersWidth;
            _form.GetTxtFilterNumber.Width = _form.GetDgvLabo.Columns[NUMBER].Width - 1;

            _form.GetTxtFilterName.Left = _form.GetTxtFilterNumber.Left + _form.GetTxtFilterNumber.Width + 1;
            _form.GetTxtFilterName.Width = _form.GetDgvLabo.Columns[TITLE].Width;
        }


        #region Filtration

        public bool IsFiltrationSet()
        {
            string number = _form.GetTxtFilterNumber.Text;
            string title = _form.GetTxtFilterName.Text;
            return !string.IsNullOrEmpty(number) | !string.IsNullOrEmpty(title);
        }

        public void SetFiltration()
        {
            string number = _form.GetTxtFilterNumber.Text;
            string title = _form.GetTxtFilterName.Text;

            bool isNumerick = int.TryParse(number, out int id);

            if (number.Length > 0 && !isNumerick)
                return;

            if (IsFiltrationSet())
            {
                id = number.Length > 0 ? id : -1;

                List<LaboDto> filter = _laboList
                    .Where(i => i.Id >= id)
                    .Where(i => string.IsNullOrEmpty(title) || i.Title.ToLower().Contains(title))
                    .ToList();

                _laboBinding.DataSource = filter;
            }
            else
            {
                _laboBinding.DataSource = _laboList;
            }
        }

        #endregion


        #region Not used

        public override bool Save()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
