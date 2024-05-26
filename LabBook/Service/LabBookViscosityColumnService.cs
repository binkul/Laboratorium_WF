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
        STD_ICI_SOL,
        SPECIAL
    }

    static class LabBookViscosityColumnService
    {
        public static readonly IDictionary<Profile, IList<string>> Profiles = new Dictionary<Profile, IList<string>>
        {
            { Profile.STD, new List<string>() { "pH", "Brook1", "Brook5", "Brook20", "BrookDisc", "BrookComment" } },
            { Profile.STD_SOL, new List<string>() { "Brook1", "Brook5", "Brook20", "BrookDisc", "BrookComment" } },
            { Profile.STD_X, new List<string>() { "pH", "Brook1", "Brook5", "Brook20", "BrookDisc", "BrookComment", "BrookXvisc", "BrookXrpm", "BrookXdisc" } },
            { Profile.STD_X_SOL, new List<string>() { "Brook1", "Brook5", "Brook20", "BrookDisc", "BrookComment", "BrookXvisc", "BrookXrpm", "BrookXdisc" } },
            { Profile.KREBS, new List<string>() { "pH", "Krebs", "KrebsComment" } },
            { Profile.KREBS_SOL, new List<string>() { "Krebs", "KrebsComment" } },
            { Profile.STD_KREBS, new List<string>() { "pH", "Brook1", "Brook5", "Brook20", "BrookDisc", "BrookComment", "Krebs", "KrebsComment" } },
            { Profile.STD_KREBS_SOL, new List<string>() { "Brook1", "Brook5", "Brook20", "BrookDisc", "BrookComment", "Krebs", "KrebsComment" } },
            { Profile.ICI, new List<string>() { "pH", "ICI", "IciDisc", "IciComment" } },
            { Profile.ICI_SOL, new List<string>() { "ICI", "IciDisc", "IciComment" } },
            { Profile.STD_ICI, new List<string>() { "pH", "Brook1", "Brook5", "Brook20", "BrookDisc", "BrookComment", "ICI", "IciDisc", "IciComment" } },
            { Profile.STD_ICI_SOL, new List<string>() { "Brook1", "Brook5", "Brook20", "BrookDisc", "BrookComment", "ICI", "IciDisc", "IciComment" } },
            { Profile.PRB, new List<string>() { "pH", "Brook1", "Brook5", "Brook10", "Brook20", "Brook50", "Brook100", "BrookDisc", "BrookComment" } }
        };
    }
}
