using Laboratorium.ADO.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Laboratorium.Material.Service
{
    public class MaterialClpService
    {
        private readonly SqlConnection _connection;
        private readonly MaterialDto _material;

        public MaterialClpService(SqlConnection connection, MaterialDto material)
        {
            _connection = connection;
            _material = material;
        }
    }
}
