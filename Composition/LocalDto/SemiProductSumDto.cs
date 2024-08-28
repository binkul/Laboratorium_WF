using Laboratorium.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboratorium.Composition.LocalDto
{
    public class SemiProductSumDto
    {
        private const int ERROR_CODE = -1;

        private IList<bool> _isPriceList = new List<bool>();
        private IList<bool> _isVocList = new List<bool>();
        private double _totalPrice = 0;
        private double _totalVOC = 0;

        public void AddPriceOk(bool isPrice)
        {
            _isPriceList.Add(isPrice);
        }

        public void AddVocOk(bool isVoc)
        {
            _isVocList.Add(isVoc);
        }

        public bool PriceOk => !_isPriceList.Any(i => i == false);

        public bool VocOk => !_isVocList.Any(i => i == false);

        public void SumPrice(double? price, double percentOriginal)
        {
            _totalPrice += CommonFunction.Percent(Convert.ToDouble(price), percentOriginal);
        }

        public void SumVOC(double? voc, double percentOriginal)
        {
            _totalVOC += CommonFunction.Percent(Convert.ToDouble(voc), percentOriginal);
        }

        public double GetPrice() => PriceOk ? _totalPrice : ERROR_CODE;

        public double GetVOC() => VocOk ? _totalVOC : ERROR_CODE;
    }
}
