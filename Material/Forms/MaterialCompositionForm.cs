using Laboratorium.ADO.DTO;
using Laboratorium.Material.Service;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Laboratorium.Material.Forms
{
    public partial class MaterialCompositionForm : Form
    {
        private readonly MaterialCompositionService _service;
        private bool _init = true;
        public DataGridView GetDgvCompound => DgvCompound;
        public DataGridView GetDgvComposition => DgvComposition;
        public TextBox GetTxtFilteringName => TxtFilerName;
        public TextBox GetTxtFilteringCas => TxtFilterCas;

        public MaterialCompositionForm(SqlConnection connection, MaterialDto material)
        {
            InitializeComponent();
            _service = new MaterialCompositionService(connection, this, material);
            LblMaterial.Text = material.Name;
        }

        public bool Init => _init;
    
        public void EnableSave(bool enable)
        {
            BtnSave.Enabled = enable;
        }

        #region Data for outside request

        public bool IsChanged => _service.IsSaved;

        public IList<MaterialCompositionDto> CompositionList => _service.CompositionList;

        #endregion

        #region FormLoad/Closing

        private void MaterialCompositionForm_Load(object sender, EventArgs e)
        {
            _service.PrepareAllData();
            _service.LoadFormData();
            _service.LoadFormData();

            ToolTip toolTip_2 = new ToolTip();
            toolTip_2.SetToolTip(BtnSave, "Zapisz");
            ToolTip toolTip_3 = new ToolTip();
            toolTip_2.SetToolTip(BtnDown, "Przesuń w dół");
            ToolTip toolTip_4 = new ToolTip();
            toolTip_2.SetToolTip(BtnUp, "Przesuń w górę");
            ToolTip toolTip_5 = new ToolTip();
            toolTip_2.SetToolTip(BtnAddOne, "Dodaj");
            ToolTip toolTip_6 = new ToolTip();
            toolTip_2.SetToolTip(BtnRemoveOne, "Usuń bierzący");
            ToolTip toolTip_7 = new ToolTip();
            toolTip_2.SetToolTip(BtnRemoveAll, "Usuń wszystko");

            _init = false;
        }

        private void MaterialCompositionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _service.FormClose(e);
        }

        #endregion

        #region TextBox events

        private void TxtFilerName_TextChanged(object sender, EventArgs e)
        {
            _service.Filtering();
        }

        private void TxtFilterCas_TextChanged(object sender, EventArgs e)
        {
            _service.Filtering();
        }

        private void TxtFilerName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
                SendKeys.Send("{Tab}");
            }

            else
            {
                base.OnKeyPress(e);
            }
        }

        #endregion

        #region DataGridView events

        private void DgvCompound_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            _service.DgvCompoundColumnWidthChanged();
        }

        private void DgvComposition_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            _service.CellValueChanged();
        }

        #endregion

        #region Buttons

        private void BtnAddOne_Click(object sender, EventArgs e)
        {
            _service.AddOne();
        }

        private void BtnRemoveOne_Click(object sender, EventArgs e)
        {
            _service.RemoveOne();
        }

        private void BtnRemoveAll_Click(object sender, EventArgs e)
        {
            _service.RemoveAll();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            _service.Save();
        }

        private void BtnUp_Click(object sender, EventArgs e)
        {
            _service.MoveUp();
        }

        private void BtnDown_Click(object sender, EventArgs e)
        {
            _service.MoveDown();
        }

        #endregion
    }
}
