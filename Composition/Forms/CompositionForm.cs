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
        private bool _init = true;
        private int _laboId;
        private CompositionService _service;

        public DataGridView GetDgvComposition => DgvComposition;

        public CompositionForm(SqlConnection connection, UserDto user, int laboId)
        {
            InitializeComponent();
            _connection = connection;
            _user = user;
            _laboId = laboId;
        }

        #region Form init and load

        public bool Init => _init;

        private void CompositionForm_Load(object sender, EventArgs e)
        {
            _service = new CompositionService(_connection, _user, this, _laboId);
            _service.PrepareAllData();
            _service.LoadFormData();

            _init = false;
        }

        private void CompositionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _service.FormClose(e);
        }

        #endregion

        private void DgvComposition_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            _service.RecipeCellFormat(e);
        }
    }
}
