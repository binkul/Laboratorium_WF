using Laboratorium.ADO;
using Laboratorium.ADO.DTO;
using Laboratorium.ADO.Service;
using Laboratorium.Commons;
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

        private RowState _rowState = RowState.ADDED;
        private readonly CompositionDto _component;
        private readonly IService _service;
        private readonly IList<int> _parents = new List<int>();

        public Component(IList<Component> list, CompositionDto component, IService service, double totalMass)
        {
            _component = component;
            _service = service;
            TotalMass = totalMass;
            Id = list != null ? list.Select(i => i.Id).DefaultIfEmpty().Max() + 1 : 1;
        }

        private void ChangeState(RowState state)
        {
            if (!IsMainComponent)
                _rowState = RowState.UNCHANGED;
            else
                _rowState = _rowState == RowState.UNCHANGED ? state : _rowState;

            if (_service != null)
                _service.Modify(state);
        }

        public RowState RowState
        {
            get => VisibleLevel > 0 ? RowState.UNCHANGED : _rowState;
            set => _rowState = value;
        }

        public bool IsMainComponent => VisibleLevel == 0 || _parents.Count == 0;

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
                return CommonData.ERROR_CODE;
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

        public byte OperationCopy
        {
            get => _component.OperationCopy;
            set => _component.OperationCopy = value;
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

        public string VocPercent => _component.VocMaterial != CommonData.ERROR_CODE ? Convert.ToDouble(_component.VocMaterial).ToString("0.00") : "Brak";

        public string VocMass
        {
            get
            {
                double? voc = (Percent * VocMaterial) / 100;
                return VocMaterial != CommonData.ERROR_CODE ? ((Convert.ToDouble(voc) * TotalMass) / 100).ToString("0.00") : "Brak";
            }
        }

        public double? PriceOriginal
        {
            get => _component.PriceOriginal;
            set => _component.PriceOriginal = value;
        }

        public string PriceCurrency
        {
            get
            {
                bool isPricePresent = _component.PriceOriginal != null && _component.PriceOriginal != CommonData.ERROR_CODE;
                string price = Convert.ToDouble(_component.PriceOriginal).ToString("0.00") + " " + _component.Currency;
                
                return IsSemiproduct ? "-" : isPricePresent ? price : "Brak";
            }
        }

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
                return PricePlKg != CommonData.ERROR_CODE ? Convert.ToDouble(price).ToString("0.00") : "Brak";
            }
        }

        public double? Rate => _component.Rate;

        public RowState GetRowState => RowState;

        public void AcceptChanges()
        {
            RowState = RowState.UNCHANGED;
            if (_service != null)
                _service.Modify(RowState.UNCHANGED);
        }

    }
}
