using System;

namespace Laboratorium.ADO.DTO
{
    public class MaterialCompoundDto
    {
        public int Id { get; set; }
        private string _namePl;
        private string _nameEn;
        private string _shortPl;
        private string _shortEn;
        private string _index;
        private string _CAS;
        private string _WE;
        private string _Formula;
        private bool _IsBio = false;
        public DateTime DateCreated { get; set; } = DateTime.Today;
        private RowState _rowState = RowState.ADDED;

        public MaterialCompoundDto(int id, string namePl, string nameEn, string shortPl, string shortEn, 
            string index, string cAS, string wE, string formula, bool isBio, DateTime dateCreated)
        {
            Id = id;
            NamePl = namePl;
            NameEn = nameEn;
            ShortPl = shortPl;
            ShortEn = shortEn;
            Index = index;
            CAS = cAS;
            WE = wE;
            Formula = formula;
            IsBio = isBio;
            DateCreated = dateCreated;
        }

        public MaterialCompoundDto() { }

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

        public string NameEn
        {
            get => _nameEn;
            set
            {
                _nameEn = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string ShortPl
        {
            get => _shortPl;
            set
            {
                _shortPl = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string ShortEn
        {
            get => _shortEn;
            set
            {
                _shortEn = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string Index
        {
            get => _index;
            set
            {
                _index = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string CAS
        {
            get => _CAS;
            set
            {
                _CAS = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string WE
        {
            get => _WE;
            set
            {
                _WE = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string Formula
        {
            get => _Formula;
            set
            {
                _Formula = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public bool IsBio
        {
            get => _IsBio;
            set
            {
                _IsBio = value;
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
