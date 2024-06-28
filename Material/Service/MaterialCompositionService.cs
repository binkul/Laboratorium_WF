using Laboratorium.ADO.DTO;
using Laboratorium.ADO.Repository;
using Laboratorium.ADO.Service;
using Laboratorium.Material.Forms;
using Laboratorium.Material.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Laboratorium.Material.Service
{
    public class MaterialCompositionService : LoadService
    {
        private const string ID = "Id";
        private const string NAME_PL = "NamePl";
        private const string GET_ROW_STATE = "GetRowState";

        private readonly IList<string> _dgvCompundFields = new List<string> {  };
        private const string FORM_DATA = "CompositionForm";
        private const int STD_WIDTH = 100;

        private readonly MaterialCompositionForm _form;
        private readonly SqlConnection _connection;
        private readonly MaterialDto _material;
        private readonly IBasicCRUD<MaterialCompositionDto> _repository;

        private IList<MaterialCompositionDto> _compositionList;
        private IList<MaterialCompoundDto> _compoundList;
        private BindingSource _compositionBinding;

        public MaterialCompositionService(SqlConnection connection, MaterialCompositionForm form, MaterialDto material) : base(FORM_DATA, form)
        {
            _form = form;
            _connection = connection;
            _material = material;
            _repository = new MaterialCompositionRepository(connection);
        }



        protected override bool Status => throw new NotImplementedException();

        protected override void PrepareColumns()
        {
            
        }

        public override void PrepareAllData()
        {
            IBasicCRUD<MaterialCompoundDto> repo = new MaterialCompoundRepository(_connection);
            _compoundList = repo.GetAll();
            _compositionList = _repository.GetAllByLaboId(_material.Id);

            PrepareDgvCompound();
        }

        private void PrepareDgvCompound()
        {

        }

        public override bool Save()
        {
            throw new NotImplementedException();
        }

    }
}
