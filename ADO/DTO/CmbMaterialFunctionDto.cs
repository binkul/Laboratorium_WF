
namespace Laboratorium.ADO.DTO
{
    public class CmbMaterialFunctionDto
    {
        public short Id { get; set; }
        public string NamePl { get; set; }

        public CmbMaterialFunctionDto(short id, string namePl)
        {
            Id = id;
            NamePl = namePl;
        }
    }
}
