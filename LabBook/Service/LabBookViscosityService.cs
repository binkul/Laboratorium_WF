using System.Collections.Generic;

namespace Laboratorium.LabBook.Service
{
    public enum Profile
    {
        STD,
        STD_SOL,
        STD_X,
        STD_X_SOL,
        PRB,
        KREBS,
        KREBS_SOL,
        STD_KREBS,
        STD_KREBS_SOL,
        ICI,
        ICI_SOL,
        STD_ICI,
        STD_ICI_SOL
    }

    static class LabBookViscosityService
    {
        public static readonly IDictionary<Profile, IList<string>> Read = new Dictionary<Profile, IList<string>>
        {
            { Profile.STD, new List<string>() { "pH", "brook_1", "brook_5", "brook_20" , "brook_disc", "brook_comment" } },
            { Profile.STD_SOL, new List<string>() { "brook_1", "brook_5", "brook_20" , "brook_disc", "brook_comment" } },
            { Profile.STD_X, new List<string>() { "pH", "brook_1", "brook_5", "brook_20" , "brook_disc", "brook_comment", "brook_x_vis", "brook_x_rpm", "brook_x_disc" } },
            { Profile.STD_X_SOL, new List<string>() { "brook_1", "brook_5", "brook_20" , "brook_disc", "brook_comment", "brook_x_vis", "brook_x_rpm", "brook_x_disc" } },
            { Profile.KREBS, new List<string>() { "pH", "krebs", "krebs_comment" } },
            { Profile.KREBS_SOL, new List<string>() { "krebs", "krebs_comment" } },
            { Profile.STD_KREBS, new List<string>() { "pH", "brook_1", "brook_5", "brook_20" , "brook_disc", "brook_comment", "krebs", "krebs_comment" } },
            { Profile.STD_KREBS_SOL, new List<string>() { "brook_1", "brook_5", "brook_20" , "brook_disc", "brook_comment", "krebs", "krebs_comment" } },
            { Profile.ICI, new List<string>() { "pH", "ici", "ici_disc", "ici_comment" } },
            { Profile.ICI_SOL, new List<string>() { "ici", "ici_disc", "ici_comment" } },
            { Profile.STD_ICI, new List<string>() { "pH", "brook_1", "brook_5", "brook_20", "brook_disc", "brook_comment", "ici", "ici_disc", "ici_comment" } },
            { Profile.STD_ICI_SOL, new List<string>() { "brook_1", "brook_5", "brook_20", "brook_disc", "brook_comment", "ici", "ici_disc", "ici_comment" } },
            { Profile.PRB, new List<string>() { "pH", "brook_1", "brook_5", "brook_10", "brook_20", "brook_50", "brook_100", "brook_disc", "brook_comment" } }
        };
    }
}
