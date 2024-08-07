using System;

namespace Laboratorium.ADO.DTO
{
    public class CompositionHistoryDto
    {
        public int Id { get; set; } = -1;
        public int LaboId { get; set; }
        public int Version { get; set; } = 1;
        public double Mass { get; set; } = 1000;
        public string ChangeType { get; set; }
        public string Comments { get; set; }
        public short LoginId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Today;

        public CompositionHistoryDto(int laboId, short loginId)
        {
            LaboId = laboId;
            LoginId = loginId;
        }

        public CompositionHistoryDto(int id, int laboId, int version, double mass, string changeType, string comments, short loginId, DateTime dateCreated)
        {
            Id = id;
            LaboId = laboId;
            Version = version;
            Mass = mass;
            ChangeType = changeType;
            Comments = comments;
            LoginId = loginId;
            DateCreated = dateCreated;
        }
    }
}
