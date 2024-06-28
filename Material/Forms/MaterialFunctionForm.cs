using Laboratorium.Currency.Service;
using Laboratorium.Material.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Laboratorium.Material.Forms
{
    public partial class MaterialFunctionForm : Form
    {
        private readonly MaterialFunctionService _service;
        private bool _init = true;
        public DataGridView GetDgvFunction => DgvFunction;

        public MaterialFunctionForm(SqlConnection connection)
        {
            InitializeComponent();
            _service = new MaterialFunctionService(connection, this);
        }

        public bool IsChanged => _service.IsChanged;

        private void MaterialFunctionForm_Load(object sender, EventArgs e)
        {
            _service.PrepareAllData();
            _service.LoadFormData();

            _init = false;
        }

        public void ActivateSave(bool activate) => BtnSave.Enabled = activate;

        public bool Init => _init;

        private void MaterialFunctionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _service.FormClose(e);
        }

        private void DgvFunction_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (Init)
                return;

            ActivateSave(true);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            _service.Delete();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            _service.Save();
        }

        private void BtnAddNew_Click(object sender, EventArgs e)
        {
            _service.AddNew();
        }
    }
}
