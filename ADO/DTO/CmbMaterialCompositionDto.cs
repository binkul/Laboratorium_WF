namespace Laboratorium.ADO.DTO
{
    public  class CmbMaterialCompositionDto
    {
        public int Id { get; set; }
        public string Material { get; set; }
        public int MaterialId { get; set; }
        public bool IsSemiproduct { get; set; }
        public double? VOC { get; set; }
        public double? PriceOryg { get; set; }
        public double? PricePl { get; set; }
        public string Currency { get; set; }
        public double Rate { get; set; }

        public CmbMaterialCompositionDto(int id, string material, int materialId, bool isSemiproduct, double? vOC, double? priceOryg, double? pricePl, string currency, double rate)
        {
            Id = id;
            Material = material;
            MaterialId = materialId;
            IsSemiproduct = isSemiproduct;
            VOC = vOC;
            PriceOryg = priceOryg;
            PricePl = pricePl;
            Currency = currency;
            Rate = rate;
        }
    }
}
