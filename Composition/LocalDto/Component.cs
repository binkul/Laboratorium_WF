using Laboratorium.ADO;
using Laboratorium.ADO.DTO;
using Laboratorium.ADO.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Laboratorium.Composition.LocalDto
{
    public enum ExpandState
    {
        None,
        Expanded,
        Collapsed
    }

    public class Component
    {
        public int Id { get; }
        public bool Visible { get; set; } = true;
        public byte VisibleLevel { get; set; } = 0;
        public ExpandState ExpandStatus { get; set; } = ExpandState.None;
        public bool LastPosition { get; set; } = false;
        public int SubLevel { get; set; } = 0;
        public double TotalMass { get; set; } = 1000;
        public CrudState CrudState { get; set; } = CrudState.OK;

        private readonly CompositionDto _component;
        private readonly IService _service;
        private readonly IList<int> _parents = new List<int>();
        private RowState _rowState = RowState.ADDED;

        public Component(IList<Component> list, CompositionDto component, IService service, double totalMass)
        {
            _component = component;
            _service = service;
            TotalMass = totalMass;
            Id = list != null ? list.Select(i => i.Id).DefaultIfEmpty().Max() + 1 : 1;
        }

        private void ChangeState(RowState state)
        {
            _rowState = _rowState == RowState.UNCHANGED ? state : _rowState;
            if (_service != null)
                _service.Modify(state);
        }

        public bool ParentsExist => _parents != null && _parents.Count > 0;

        public int ParentsCount => _parents.Count;

        public void AddParent(int id)
        {
            _parents.Add(id);
        }

        public int GetParent(int index)
        {
            if (_parents.Count > 0)
                return _parents[index];
            else
                return -1;
        }

        public bool ExistParent(int id)
        {
            return ParentsExist && _parents.Contains(id);
        }

        public int LaboId
        {
            get => _component.LaboId;
            set => _component.LaboId = value;
        }

        public int Version
        {
            get => _component.Version;
            set => _component.Version = value;
        }

        public short Ordering
        {
            get => _component.Ordering;
            set
            {
                _component.Ordering = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public byte Operation
        {
            get => _component.Operation;
            set
            {
                _component.Operation = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public string Material => _component.Material;

        public int MaterialId => _component.MaterialId;

        public bool IsSemiproduct => _component.IsSemiproduct;

        public double Percent
        {
            get => _component.Percent;
            set
            {
                _component.Percent = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public double PercentOryginal => _component.PercentOryginal;

        public string Comment
        {
            get => _component.Comment;
            set
            {
                _component.Comment = value;
                ChangeState(RowState.MODIFIED);
            }
        }

        public double Mass => Math.Round(TotalMass * (Percent / 100), 4);

        public double? VocMaterial
        {
            get => _component.VocMaterial;
            set => _component.VocMaterial = value;
        }

        public string VocPercent => _component.VocMaterial != -1 ? Convert.ToDouble(_component.VocMaterial).ToString("0.00") : "Brak";

        public string VocMass
        {
            get
            {
                double? voc = (Percent * VocMaterial) / 100;
                return VocMaterial != -1 ? ((Convert.ToDouble(voc) * TotalMass) / 100).ToString("0.00") : "Brak";
            }
        }

        public double? PriceOriginal
        {
            get => _component.PriceOriginal;
            set => _component.PriceOriginal = value;
        }

        public string PriceCurrency => IsSemiproduct ? "-" : _component.PriceOriginal != null && _component.PriceOriginal != -1 ? Convert.ToDouble(_component.PriceOriginal).ToString("0.00") + " " + _component.Currency : "Brak";

        public double? PricePlKg
        {
            get => _component.PricePlKg;
            set => _component.PricePlKg = value;
        }

        public string PriceMass
        {
            get
            {
                double? price = PricePlKg * Mass;
                return PricePlKg != -1 ? Convert.ToDouble(price).ToString("0.00") : "Brak";
            }
        }

        public double? Rate => _component.Rate;

        public RowState GetRowState => _rowState;

        public void AcceptChanges()
        {
            _rowState = RowState.UNCHANGED;
            if (_service != null)
                _service.Modify(RowState.UNCHANGED);
        }

    }
}
