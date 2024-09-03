using Laboratorium.ADO.DTO;
using Laboratorium.Composition.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Laboratorium.Composition.Forms
{
    public partial class CompositionForm : Form
    {
        private readonly SqlConnection _connection;
        private UserDto _user;
        private readonly IList<LaboDto> _laboList;
        private bool _init = true;
        private LaboDto _laboDto;
        private CompositionService _service;

        public DataGridView GetDgvComposition => DgvComposition;
        public ComboBox GetCmbMaterial => CmbMaterial;
        public TextBox GetTxtSetAmount => TxtSetAmount;
        public TextBox GetTxtSetMass => TxtSetMass;
        public TextBox GetTxtComment => TxtComment;
        public TextBox GetTxtTotalMass => TxtTotalMass;
        public Button GetBtnExchange => BtnExchange;
        public Button GetBtnDelete => BtnDelete;
        public Button GetBtnUp => BtnUp;
        public Button GetBtnDown => BtnDown;
        public Button GetBtnFrameUp => BtnFrameUp;
        public Button GetBtnFrameDown => BtnFrameDown;
        public Button GetBtnCut => BtnCut;
        public Button GetBtnUp100 => BtnUp100;
        public Button GetBtnAddFirst => BtnAddFirst;
        public Button GetBtnAddInside => BtnAddInside;
        public Button GetBtnAddLast => BtnAddLast;
        public RadioButton GetRadioAmount => RdAmount;
        public RadioButton GetRadioMass => RdMass;
        public Label GetLblDensity => LblDensity;
        public Label GetLblSumText => LblSum;
        public Label GetLblSumMass => LblSumMass;
        public Label GetLblSumPrecent => LblSumPercent;
        public Label GetLblMassText => LblMass;
        public Label GetLblCalcPricePerKg => LblPriceCalcPerKg;
        public Label GetLblCalcPricePerL => LblPriceCalcPerL;
        public Label GetLblVocKg => LblVocKg;
        public Label GetLblVocL => LblVocL;
        public Label GetLblVocPerKg => LblVocPerKg;
        public Label GetLblVocPerL => LblVocPerL;

        public CompositionForm(SqlConnection connection, UserDto user, LaboDto laboDto, IList<LaboDto> laboList)
        {
            InitializeComponent();
            _connection = connection;
            _user = user;
            _laboDto = laboDto;
            _laboList = laboList;
        }

        #region Form init and load

        public bool Init => _init;

        public void SaveEnable (bool status)
        {
            if (_init)
                return;

            BtnSave.Enabled = status;
        }

        private void CompositionForm_Load(object sender, EventArgs e)
        {
            LblDensity.Text = _laboDto.Density != null ? "Gęstość: " + _laboDto.Density.ToString() + " g/cm3" : "Gęstość: BRAK";
            LblNrD.Text = "D-" + _laboDto.Id.ToString();
            LblTitle.Text = _laboDto.Title;

            _service = new CompositionService(_connection, _user, this, _laboDto, _laboList);
            _service.PrepareAllData();
            _service.LoadFormData();

            _init = false;

            DgvComposition_ColumnWidthChanged(null, null);
        }

        private void CompositionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _service.FormClose(e);
        }

        #endregion

        private void DgvComposition_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            _service.DgvCellFormat(e);
        }

        private void DgvComposition_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            _service.DgvChangeColumnWidth();
        }

        private void RdAmount_CheckedChanged(object sender, EventArgs e)
        {
            _service.ChangeCalculationType((RadioButton)sender);
        }

        private void DgvComposition_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            _service.DgvCellMouseClick(e);
        }

        private void DgvComposition_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            _service.DgvCellPaint((DataGridView)sender, e);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            _service.Save();
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            _service.Print();
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            _service.LoadExistingRecipen();
        }

        private void BtnExchange_Click(object sender, EventArgs e)
        {
            _service.InsertExistingRecipe();
        }

        private void DgvComposition_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            _service.DgvCellToolTipTextNeeded(e);
        }
    }
}
