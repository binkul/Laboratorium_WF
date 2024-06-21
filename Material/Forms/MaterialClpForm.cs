using Laboratorium.ADO.DTO;
using Laboratorium.Material.Service;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Laboratorium.Material.Forms
{
    public partial class MaterialClpForm : Form
    {
        private readonly MaterialClpService _service;
        public readonly List<PictureBox> GhsList;
        public readonly List<PictureBox> GhsOkList;
        public ComboBox GetCmbSignal => CmbSignalWord;
        public DataGridView GetDgvClp => DgvClp;
        public DataGridView GetDgvMaterialClp => DgvMaterial;

        public MaterialClpForm(SqlConnection connection, MaterialDto material)
        {
            InitializeComponent();
            _service = new MaterialClpService(connection, material, this);
            GhsList = new List<PictureBox>() { PicGHS_01, PicGHS_02, PicGHS_03, PicGHS_04, PicGHS_05, PicGHS_06, PicGHS_07, PicGHS_08, PicGHS_09 };
            GhsOkList = new List<PictureBox>() { PicGHS_Ok_01, PicGHS_Ok_02, PicGHS_Ok_03, PicGHS_Ok_04, PicGHS_Ok_05, PicGHS_Ok_06, PicGHS_Ok_07, PicGHS_Ok_08, PicGHS_Ok_09 };
        }

        private void MaterialClpForm_Load(object sender, EventArgs e)
        {
            _service.PrepareAllData();
            _service.LoadFormData();
        }

        private void MaterialClpForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _service.FormClose(e);
        }

        private void PicGHS_Click(object sender, EventArgs e)
        {
            PictureBox picture = (PictureBox)sender;
            string nr = picture.Tag != null ? picture.Tag.ToString() : "";

            if (string.IsNullOrEmpty(nr))
                return;

            int index = Convert.ToInt32(nr);
            index -= 100;
            GhsList[index].Visible = false;
            GhsOkList[index].Visible = true;
            _service.GHScodeChanged = true;
        }

        private void PicGHS_Ok_Click(object sender, EventArgs e)
        {
            PictureBox picture = (PictureBox)sender;
            string nr = picture.Tag != null ? picture.Tag.ToString() : "";

            if (string.IsNullOrEmpty(nr))
                return;

            int index = Convert.ToInt32(nr);
            GhsList[index].Visible = true;
            GhsOkList[index].Visible = false;
            _service.GHScodeChanged = true;
        }

    }
}
