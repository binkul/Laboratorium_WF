using System;
using System.Windows.Forms;

namespace Laboratorium.LabBook.Forms
{
    public partial class AddSeriesLaboForm : Form
    {
        private int _nr;
        private string _title;
        private bool _exitByButton = false;
        public int Amount { get; private set; } = 1;
        public int Type { get; private set; } = 1;
        public bool Ok { get; private set; } = false;

        public AddSeriesLaboForm(int nr, string title)
        {
            _nr = nr;
            _title = title;
            InitializeComponent();
        }

        private void AddSeriesLaboForm_Load(object sender, EventArgs e)
        {
            LblCurrentNrD.Text = "D-" + _nr;
            lblCurrent.Text = "Tytuł: " + _title;
            NumericUpDown.Value = 1;
        }

        private void RdCopyI_CheckedChanged(object sender, EventArgs e)
        {
            if (RdCopyI.Checked)
                Type = 1;
        }

        private void RdCopyII_CheckedChanged(object sender, EventArgs e)
        {
            if (RdCopyII.Checked)
                Type = 2;
        }

        private void RdCopyIII_CheckedChanged(object sender, EventArgs e)
        {
            if (RdCopyIII.Checked)
                Type = 3;
        }

        private void RdCopyIV_CheckedChanged(object sender, EventArgs e)
        {
            if (RdCopyIV.Checked)
                Type = 4;
        }

        private void RdCopyV_CheckedChanged(object sender, EventArgs e)
        {
            if (RdCopyV.Checked)
                Type = 5;
        }

        private void RdCopyVI_CheckedChanged(object sender, EventArgs e)
        {
            if (RdCopyVI.Checked)
                Type = 6;
        }

        private void NumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            Amount = Convert.ToInt32(NumericUpDown.Value);
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            Ok = true;
            _exitByButton = true;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Ok = false;
            _exitByButton = true;
            Close();
        }

        private void AddSeriesLaboForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_exitByButton)
                Ok = false;
        }

    }
}
