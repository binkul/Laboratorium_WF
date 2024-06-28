
namespace Laboratorium.ADO.DTO
{
    public class CmbCurrencyDto
    {
        public byte Id { get; set; }
        private string _name;
        private string _currency;
        private double _rate;
        private RowState _rowState = RowState.ADDED;

        public CmbCurrencyDto(byte id, string name, string currency, double rate)
        {
            Id = id;
            _name = name;
            _currency = currency;
            _rate = rate;
        }

        private void ChangeState(RowState state)
        {
            _rowState = _rowState == RowState.UNCHANGED ? state : _rowState;
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string Currency
        {
            get => _currency;
            set
            {
                _currency = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public double Rate
        {
            get => _rate;
            set
            {
                _rate = value;
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
