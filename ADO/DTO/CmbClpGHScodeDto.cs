using System;

namespace Laboratorium.ADO.DTO
{
    public class CmbClpGHScodeDto
    {
        public byte Id { get; }
        public byte Code { get; }
        public string Description { get; }
        public string Comment { get; }
        public string CommentEn { get; }
        public string PngFile { get; }
        public int Ordering { get; }
        public DateTime DateCreated { get; }

        public CmbClpGHScodeDto(byte id, byte code, string description, string comment, string commentEn, string pngFile, 
            int ordering, DateTime dateCreated)
        {
            Id = id;
            Code = code;
            Description = description;
            Comment = comment;
            CommentEn = commentEn;
            PngFile = pngFile;
            Ordering = ordering;
            DateCreated = dateCreated;
        }
    }
}
