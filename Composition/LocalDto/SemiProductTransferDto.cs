using Laboratorium.ADO.DTO;
using System.Collections.Generic;

namespace Laboratorium.Composition.LocalDto
{
    public class SemiProductTransferDto
    {
        public double? Price { get; set; }
        public double? VOC { get; set; }
        public IList<CompositionDto> SubProductComposition { get; set; } = null;

        public SemiProductTransferDto()
        { }

        public SemiProductTransferDto(double? price, double? voc, IList<CompositionDto> subProductComposition)
        {
            Price = price;
            VOC = voc;
            SubProductComposition = subProductComposition;
        }
    }
}
