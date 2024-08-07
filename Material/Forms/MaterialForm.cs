﻿using Laboratorium.ADO.DTO;
using Laboratorium.Material.Service;
using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Laboratorium.Material.Forms
{
    public partial class MaterialForm : Form
    {
        private MaterialService _service;
        private readonly SqlConnection _connection;
        private UserDto _user;
        private bool _init = true;

        #region Controls Share

        public BindingNavigator GetBindingNavigator => BindingNavigatorMaterial;
        public DataGridView GetDgvMaterial => DgvMaterial;
        public DataGridView GetDgvClp => DgvClp;
        public DataGridView GetDgvComposition => DgvComposition;
        public TextBox GetTxtName => TxtName;
        public TextBox GetTxtIndex => TxtIndex;
        public TextBox GetTxtDensity => TxtDensity;
        public TextBox GetTxtSolids => TxtSolids;
        public TextBox GetTxtAsh => TxtAsh450;
        public TextBox GetTxtPrice => TxtPrice;
        public TextBox GetTxtPriceQuantity => TxtPriceQuantity;
        public TextBox GetTxtTransport => TxtTransport;
        public TextBox GetTxtQuantity => TxtQuantity;
        public TextBox GetTxtRemarks => TxtRemarks;
        public TextBox GetTxtVoc => TxtVoc;
        public TextBox GetTxtFilterName => TxtFilterName;
        public Label GetLblClpSignal => LblClpSignal;
        public ComboBox GetCmbFunction => CmbFunction;
        public ComboBox GetCmbSupplier => CmbSupplier;
        public ComboBox GetCmbCurrency => CmbCurrency;
        public ComboBox GetCmbUnit => CmbUnit;
        public CheckBox GetChbDanger => ChbClp;
        public CheckBox GetChbActive => ChbActive;
        public CheckBox GetChbProduction => ChbProduction;
        public CheckBox GetChbPacking => ChbPacking;
        public CheckBox GetChbSample => ChbSample;
        public CheckBox GetChbSemiproduct => ChbSemiproduct;
        public CheckBox GetChbFilterActive => ChbFilterActive;
        public CheckBox GetChbFilterClp => ChbFilterClp;
        public CheckBox GetChbFilterProd => ChbFilterProduction;
        public Label GetLblDateCreated => LblDateCreated;
        public ToolStripLabel GetMessageLabel => ToolStripLblMessage;
        public PictureBox GetPicClpImage => PicBox_CLP;
        public Button GetBtnClpEdit => BtnClpEdit;
        public Button GetBtnFilterCancel => BtnFilterCancel;
        public Button GetBtnDelete => BtnDelete;
        public Button GetBtnClp => BtnClp;
        public ToolStripButton GetBtnNavigatorDelete => BindingNavigatorDeleteItem;

        #endregion

        public MaterialForm(SqlConnection connection, UserDto user)
        {
            InitializeComponent();
            _connection = connection;
            _user = user;
        }


        #region Form init and load

        public bool Init => _init;

        public void ActivateSave(bool state)
        {
            if (_init)
                return;
            BtnSave.Enabled = state;
        }

        private void MaterialForm_Load(object sender, EventArgs e)
        {
            _service = new MaterialService(_connection, _user, this);
            _service.PrepareAllData();
            _service.LoadFormData();

            ToolTip toolTip_1 = new ToolTip();
            toolTip_1.SetToolTip(BtnAdd, "Dodaj pojedynczy");
            ToolTip toolTip_2 = new ToolTip();
            toolTip_2.SetToolTip(BtnDelete, "Usuń bierzący");
            ToolTip toolTip_3 = new ToolTip();
            toolTip_3.SetToolTip(BtnSave, "Zapisz zmiany");
            ToolTip toolTip_4 = new ToolTip();
            toolTip_4.SetToolTip(BtnCurrency, "Otwórz waluty");
            ToolTip toolTip_5 = new ToolTip();
            toolTip_5.SetToolTip(BtnClp, "Otwórz dane CLP");
            ToolTip toolTip_6 = new ToolTip();
            toolTip_6.SetToolTip(BtnCompositiom, "Otwórz skład");
            ToolTip toolTip_7 = new ToolTip();
            toolTip_7.SetToolTip(BtnFunction, "Otwórz funkcje");
            ToolTip toolTip_8 = new ToolTip();
            toolTip_8.SetToolTip(BtnCompound, "Otwórz związki");

            _init = false;
        }

        private void MaterialForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _service.FormClose(e);
        }

        #endregion


        #region Buttons

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            _service.Delete();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            _service.Save();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            _service.AddNewMaterial();
        }

        private void BtnClpEdit_Click(object sender, EventArgs e)
        {
            _service.OpenCLP();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            _service.AddNewMaterial();
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            _service.Delete();
        }

        private void BtnCurrency_Click(object sender, EventArgs e)
        {
            _service.OpenCurrency();
        }

        private void BtnClp_Click(object sender, EventArgs e)
        {
            _service.OpenCLP();
        }

        private void BtnFunction_Click(object sender, EventArgs e)
        {
            _service.OpenFunction();
        }

        private void BtnCompositiom_Click(object sender, EventArgs e)
        {
            _service.OpenComposition();
        }

        private void BtnCompound_Click(object sender, EventArgs e)
        {
            _service.OpenCompound();
        }

        #endregion


        #region Controls validating

        private void TxtDensity_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextBox box = (TextBox)sender;
            string text = box.Text;
            string separator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            string wzor = (separator == ".") ? @"^[0-9]*\" + separator + "?[0-9]*$"
                                             : "^[0-9]*" + separator + "?[0-9]*$";
            Regex wzorzec = new Regex(wzor);

            if (!wzorzec.IsMatch(text) && text.Length > 0)
            {
                MessageBox.Show("Wprowadzona wartość nie jest liczbą '" + text + "'",
                    "Błąd wartości", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        private void TxtDensity_KeyPress(object sender, KeyPressEventArgs e)
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


        #region DataGridView Events

        private void DgvClp_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (Init)
                return;

            _service.DgvClpHPdataFormat(e);
        }

        private void DgvMaterial_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            _service.ColumnWidthChanged();
        }

        private void DgvMaterial_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            _service.CellvalueChanged(e);
        }

        #endregion


        #region Controls Events

        private void ChbClp_CheckedChanged(object sender, EventArgs e)
        {
            _service.DangerStateChanged();
        }

        #endregion


        #region Filtering

        private void TxtFilterName_TextChanged(object sender, EventArgs e)
        {
            _service.Filtering();
        }

        private void ChbFilterActive_CheckedChanged(object sender, EventArgs e)
        {
            _service.Filtering();
        }

        private void BtnFilterCancel_Click(object sender, EventArgs e)
        {
            _service.CancelFilter(true);
        }


        #endregion

    }
}
