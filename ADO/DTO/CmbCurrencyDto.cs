
namespace Laboratorium.ADO.DTO
{
    public class CmbCurrencyDto
    {
        public byte Id { get; set; }
        public string Currency { get; set; }
        public double Rate { get; set; }

        public CmbCurrencyDto(byte id, string currency, double rate)
        {
            Id = id;
            Currency = currency;
            Rate = rate;
        }
    }
}
