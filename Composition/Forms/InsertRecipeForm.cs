using Laboratorium.ADO.DTO;
using Laboratorium.Composition.Service;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Laboratorium.Composition.Forms
{
    public partial class InsertRecipeForm : Form
    {
        private readonly IList<LaboDto> _laboList;
        private InsertRecipeService _service;

        public InsertRecipeForm(IList<LaboDto> laboList)
        {
            _laboList = laboList;
            InitializeComponent();
        }

        public DataGridView GetDgvLabo => DgvLabo;
        public TextBox GetTxtFilterNumber => TxtFindNumber;
        public TextBox GetTxtFilterName => TxtFindName;

        private void InsertRecipeForm_Load(object sender, System.EventArgs e)
        {
            _service = new InsertRecipeService(this, _laboList);
            
            _service.PrepareAllData();
            _service.LoadFormData();
        }

        private void InsertRecipeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _service.FormClose(e);
        }

        private void DgvLabo_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            _service.ColumnWidthChanged();
        }
    }
}
