using Laboratorium.LabBook.Service;

namespace Laboratorium.ADO.DTO
{
    public class LaboDataViscosityColDto
    {
        public int LaboId { get; set; }
        public Profile Profile { get; set; }
        public string Columns { get; set; }

        public LaboDataViscosityColDto(int laboId, Profile profile, string columns)
        {
            LaboId = laboId;
            Profile = profile;
            Columns = columns;
        }
    }
}
