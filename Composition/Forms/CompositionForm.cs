using Laboratorium.ADO.DTO;
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

        public CompositionForm(SqlConnection connection, UserDto user)
        {
            InitializeComponent();
            _connection = connection;
            _user = user;
        }
    }
}
