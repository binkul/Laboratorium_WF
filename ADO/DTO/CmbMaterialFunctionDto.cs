namespace Laboratorium.ADO.DTO
{
    public class CmbMaterialFunctionDto
    {
        public short Id { get; set; }
        private string _namePl;
        private RowState _rowState = RowState.ADDED;

        public CmbMaterialFunctionDto(short id, string namePl)
        {
            Id = id;
            _namePl = namePl;
        }

        private void ChangeState(RowState state)
        {
            _rowState = _rowState == RowState.UNCHANGED ? state : _rowState;
        }

        public string NamePl
        {
            get => _namePl;
            set
            {
                _namePl = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public RowState GetRowState => _rowState;

        public void AcceptChanges()
        {
            _rowState = RowState.UNCHANGED;
        }

    }
}
