using System;

namespace Laboratorium.ADO.DTO
{
    public class LaboDataBasicDto
    {
        public int Id { get; set; }
        public int LaboId { get; set; }
        private double? _gloss20;
        private double? _gloss60;
        private double? _gloss85;
        private byte _glossClassId;
        public GlossClassDto GlossClass { get; set; }
        private string _glossComment;
        private string _scrubBrush;
        private string _scrubSponge;
        private byte _scrubClassId;
        public ScrubClassDto ScrubClass { get; set; }
        private string _scrubComment;
        private byte _contrastClassId;
        public ContrastClassDto ContrastClass { get; set; }
        private string _contrastComment;
        private string _voc;
        private byte _vocClassId;
        public VocClassDto VocClass { get; set; }
        private string _yield;
        private string _adhesion;
        private string _flow;
        private string _spill;
        private string _dryingI;
        private string _dryingII;
        private string _dryingIII;
        private string _dryingIV;
        private string _dryingV;
        public DateTime DateUpdated { get; private set; }
        private RowState _rowState = RowState.ADDED;

        public LaboDataBasicDto(int id, int laboId, double? gloss20, double? gloss60, double? gloss85, byte glossClass, string glossComment, string scrubBrush, string scrubSponge, 
            byte scrubClass, string scrubComment, byte contrastClass, string contrastComment, string voc, byte vocClass, string yield, string adhesion, string flow, string spill, 
            string dryingI, string dryingII, string dryingIII, string dryingIV, string dryingV, DateTime dateUpdated)
        {
            Id = id;
            LaboId = laboId;
            _gloss20 = gloss20;
            _gloss60 = gloss60;
            _gloss85 = gloss85;
            _glossClassId = glossClass;
            _glossComment = glossComment;
            _scrubBrush = scrubBrush;
            _scrubSponge = scrubSponge;
            _scrubClassId = scrubClass;
            _scrubComment = scrubComment;
            _contrastClassId = contrastClass;
            _contrastComment = contrastComment;
            _voc = voc;
            _vocClassId = vocClass;
            _yield = yield;
            _adhesion = adhesion;
            _flow = flow;
            _spill = spill;
            _dryingI = dryingI;
            _dryingII = dryingII;
            _dryingIII = dryingIII;
            _dryingIV = dryingIV;
            _dryingV = dryingV;
            DateUpdated = dateUpdated;
        }

        public LaboDataBasicDto(int laboId, byte glossClass, byte scrubClass, byte contrastClass, byte vocClass, DateTime dateUpdated)
        {
            LaboId = laboId;
            _glossClassId = glossClass;
            _scrubClassId = scrubClass;
            _contrastClassId = contrastClass;
            _vocClassId = vocClass;
            DateUpdated = dateUpdated;
        }

        public double? Gloss20
        {
            get => _gloss20;
            set
            {
                _gloss20 = value;
                _rowState = _rowState == RowState.UNCHANGED ? RowState.MODIFIED : _rowState;
                DateUpdated = DateTime.Today;
            }
        }

        public double? Gloss60
        {
            get => _gloss60;
            set
            {
                _gloss60 = value;
                _rowState = _rowState == RowState.UNCHANGED ? RowState.MODIFIED : _rowState;
                DateUpdated = DateTime.Today;
            }
        }

        public double? Gloss85
        {
            get => _gloss85;
            set
            {
                _gloss85 = value;
                _rowState = _rowState == RowState.UNCHANGED ? RowState.MODIFIED : _rowState;
                DateUpdated = DateTime.Today;
            }
        }

        public byte GlossClassId
        {
            get => _glossClassId;
            set
            {
                _glossClassId = value;
                _rowState = _rowState == RowState.UNCHANGED ? RowState.MODIFIED : _rowState;
                DateUpdated = DateTime.Today;
            }
        }

        public string GlossComment
        {
            get => _glossComment;
            set
            {
                _glossComment = value;
                _rowState = _rowState == RowState.UNCHANGED ? RowState.MODIFIED : _rowState;
                DateUpdated = DateTime.Today;
            }
        }

        public string ScrubBrusch
        {
            get => _scrubBrush;
            set
            {
                _scrubBrush = value;
                _rowState = _rowState == RowState.UNCHANGED ? RowState.MODIFIED : _rowState;
                DateUpdated = DateTime.Today;
            }
        }

        public string ScrubSponge
        {
            get => _scrubSponge;
            set
            {
                _scrubSponge = value;
                _rowState = _rowState == RowState.UNCHANGED ? RowState.MODIFIED : _rowState;
                DateUpdated = DateTime.Today;
            }
        }

        public byte ScrubClassId
        {
            get => _scrubClassId;
            set
            {
                _scrubClassId = value;
                _rowState = _rowState == RowState.UNCHANGED ? RowState.MODIFIED : _rowState;
                DateUpdated = DateTime.Today;
            }
        }

        public string ScrubComment
        {
            get => _scrubComment;
            set
            {
                _scrubComment = value;
                _rowState = _rowState == RowState.UNCHANGED ? RowState.MODIFIED : _rowState;
                DateUpdated = DateTime.Today;
            }
        }

        public byte ContrastClassId
        {
            get => _contrastClassId;
            set
            {
                _contrastClassId = value;
                _rowState = _rowState == RowState.UNCHANGED ? RowState.MODIFIED : _rowState;
                DateUpdated = DateTime.Today;
            }
        }

        public string ContrastComment
        {
            get => _contrastComment;
            set
            {
                _contrastComment = value;
                _rowState = _rowState == RowState.UNCHANGED ? RowState.MODIFIED : _rowState;
                DateUpdated = DateTime.Today;
            }
        }

        public byte VocClassId
        {
            get => _vocClassId;
            set
            {
                _vocClassId = value;
                _rowState = _rowState == RowState.UNCHANGED ? RowState.MODIFIED : _rowState;
                DateUpdated = DateTime.Today;
            }
        }

        public string VOC
        {
            get => _voc;
            set
            {
                _voc = value;
                _rowState = _rowState == RowState.UNCHANGED ? RowState.MODIFIED : _rowState;
                DateUpdated = DateTime.Today;
            }
        }

        public string Yield
        {
            get => _yield;
            set
            {
                _yield = value;
                _rowState = _rowState == RowState.UNCHANGED ? RowState.MODIFIED : _rowState;
                DateUpdated = DateTime.Today;
            }
        }

        public string Adhesion
        {
            get => _adhesion;
            set
            {
                _adhesion = value;
                _rowState = _rowState == RowState.UNCHANGED ? RowState.MODIFIED : _rowState;
                DateUpdated = DateTime.Today;
            }
        }

        public string Flow
        {
            get => _flow;
            set
            {
                _flow = value;
                _rowState = _rowState == RowState.UNCHANGED ? RowState.MODIFIED : _rowState;
                DateUpdated = DateTime.Today;
            }
        }

        public string Spill
        {
            get => _spill;
            set
            {
                _spill = value;
                _rowState = _rowState == RowState.UNCHANGED ? RowState.MODIFIED : _rowState;
                DateUpdated = DateTime.Today;
            }
        }

        public string DryingI
        {
            get => _dryingI;
            set
            {
                _dryingI = value;
                _rowState = _rowState == RowState.UNCHANGED ? RowState.MODIFIED : _rowState;
                DateUpdated = DateTime.Today;
            }
        }

        public string DryingII
        {
            get => _dryingII;
            set
            {
                _dryingII = value;
                _rowState = _rowState == RowState.UNCHANGED ? RowState.MODIFIED : _rowState;
                DateUpdated = DateTime.Today;
            }
        }

        public string DryingIII
        {
            get => _dryingIII;
            set
            {
                _dryingIII = value;
                _rowState = _rowState == RowState.UNCHANGED ? RowState.MODIFIED : _rowState;
                DateUpdated = DateTime.Today;
            }
        }

        public string DryingIV
        {
            get => _dryingIV;
            set
            {
                _dryingIV = value;
                _rowState = _rowState == RowState.UNCHANGED ? RowState.MODIFIED : _rowState;
                DateUpdated = DateTime.Today;
            }
        }

        public string DryingV
        {
            get => _dryingV;
            set
            {
                _dryingV = value;
                _rowState = _rowState == RowState.UNCHANGED ? RowState.MODIFIED : _rowState;
                DateUpdated = DateTime.Today;
            }
        }

        public RowState GetRowState => _rowState;

        public void AcceptChanged()
        {
            _rowState = RowState.UNCHANGED;
        }

    }
}
