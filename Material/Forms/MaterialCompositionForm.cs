using Laboratorium.ADO.DTO;
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
    public partial class MaterialCompositionForm : Form
    {
        private readonly MaterialCompositionService _service;
        private bool _init = true;
        public DataGridView GetDgvCompound => DgvCompound;
        public DataGridView GetDgvComposition => DgvComposition;

        public MaterialCompositionForm(SqlConnection connection, MaterialDto material)
        {
            InitializeComponent();
            _service = new MaterialCompositionService(connection, this, material);
        }

        public bool Init => _init;

        private void MaterialCompositionForm_Load(object sender, EventArgs e)
        {
            _service.PrepareAllData();
            _service.LoadFormData();

            _init = false;
        }


    }
}
