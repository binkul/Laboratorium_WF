using Laboratorium.ADO.Service;

namespace Laboratorium.ADO.DTO
{
    public class CompositionDto
    {
        private int _laboId;
        private int _version;
        private short _ordering;
        private string _material;
        private int _materialId;
        private bool _isIntermediate;
        private double _amount;
        private double _mass;
        private byte _operation;
        private string _comment;
        private RowState _rowState = RowState.ADDED;
        private IService _service;

        public CrudState CrudState { get; set; } = CrudState.OK;

        public CompositionDto(int laboId, int version, short ordering, string material, int materialId, 
            bool isIntermediate, double amount, byte operation, string comment, IService service)
        {
            _laboId = laboId;
            _version = version;
            _ordering = ordering;
            _material = material;
            _materialId = materialId;
            _isIntermediate = isIntermediate;
            _amount = amount;
            _operation = operation;
            _comment = comment;
            _service = service;
        }

        private void ChangeState(RowState state)
        {
            _rowState = _rowState == RowState.UNCHANGED ? state : _rowState;
            if (_service != null)
                _service.Modify(state);
        }

        public int LaboId
        {
            get => _laboId;
            set
            {
                _laboId = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public int Version
        {
            get => _version;
            set
            {
                _version = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public short Ordering
        {
            get => _ordering;
            set
            {
                _ordering = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string Material
        {
            get => _material;
            set
            {
                _material = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public int MaterialId
        {
            get => _materialId;
            set
            {
                _materialId = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public bool IsIntermediate
        {
            get => _isIntermediate;
            set
            {
                _isIntermediate = value;
                ChangeState(RowState.MODIFIED);
            }
        }
   
        public double Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public double Mass
        {
            get => _mass;
            set
            {
                _mass = value;
            }
        }

        public byte Operation
        {
            get => _operation;
            set
            {
                _operation = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string Comment
        {
            get => _comment;
            set
            {
                _comment = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public RowState GetRowState => _rowState;

        public void AcceptChanges()
        {
            _rowState = RowState.UNCHANGED;
            if (_service != null)
                _service.Modify(RowState.UNCHANGED);
        }

    }
}
