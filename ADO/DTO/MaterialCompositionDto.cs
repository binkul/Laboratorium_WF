using System;

namespace Laboratorium.ADO.DTO
{
    public class MaterialCompositionDto
    {
        public int Id { get; set; }
        public int MaterialId { get; set; }
        private double _amountMin;
        private double _amountMax;
        private byte _ordering;
        private string _remarks;
        public DateTime _dateCreated { get; set; } = DateTime.Today;
        private RowState _rowState = RowState.ADDED;

        public MaterialCompositionDto(int id, int materialId, double amountMin, double amountMax, byte ordering, 
            string remarks, DateTime dateCreated)
        {
            Id = id;
            MaterialId = materialId;
            _amountMin = amountMin;
            _amountMax = amountMax;
            _ordering = ordering;
            _remarks = remarks;
            _dateCreated = dateCreated;
        }

        public MaterialCompositionDto(int materialId, double amountMin, double amountMax, byte ordering)
        {
            MaterialId = materialId;
            _amountMin = amountMin;
            _amountMax = amountMax;
            _ordering = ordering;
        }

        private void ChangeState(RowState state)
        {
            _rowState = _rowState == RowState.UNCHANGED ? state : _rowState;
        }

        public double AmountMin
        {
            get => _amountMin;
            set
            {
                _amountMin = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public double AmountMax
        {
            get => _amountMax;
            set
            {
                _amountMax = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public byte Ordering
        {
            get => _ordering;
            set
            {
                _ordering = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string Remarks
        {
            get => _remarks;
            set
            {
                _remarks = value;
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
