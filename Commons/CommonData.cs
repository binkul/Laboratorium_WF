using System.Collections.Generic;
using System.Drawing;

namespace Laboratorium.Commons
{
    public static class CommonData
    {
        public static int ERROR_CODE = -1;
        public static int HEADER_WIDTH_ADMIN = 35;
        public static int HEADER_WIDTH_USER = 40;
        public static string ALL_DATA_PL = "-- Wszystko --";
        public static string LENETA = "Leneta cz/b";
        public static int STD_APPLICATOR = -1;
        public static int NONE_APPLIKATOR = -2;

        public static IList<string> Aplikators = new List<string>()
        {
            "Aplikator zwykły 75um",
            "Aplikator zwykły 100um",
            "Aplikator zwykły 125um",
            "Aplikator zwykły 150um",
            "Aplikator zwykły 200um",
            "Aplikator zwykły 240um",
            "Aplikator zwykły 250um",
            "Aplikator zwykły 300um",
            "Aplikator okrgły 50um",
            "Aplikator okrgły 75um",
            "Aplikator okrgły 100um",
            "Aplikator okrgły 125um",
            "Aplikator okrgły 150um",
            "Aplikator okrgły 175um",
            "Aplikator okrgły 200um",
            "Aplikator okrgły 250um",
            "Aplikator okrgły 300um",
            "Aplikator drutowy 50um",
            "Aplikator drutowy 75um",
            "Aplikator drutowy 100um",
            "Aplikator drutowy 125um",
            "Aplikator drutowy 150um",
            "Aplikator drutowy 175um",
            "Aplikator drutowy 200um",
            "Aplikator drutowy 250um",
            "Aplikator drutowy 300um"
        };

        public static IList<string> AplikatorsStd = new List<string>()
        {
            "Aplikator zwykły 75um",
            "Aplikator zwykły 100um",
            "Aplikator zwykły 150um",
            "Aplikator zwykły 240um"
        };

        public static IList<Image> GhsImages = new List<Image>
        {
            new Bitmap(Properties.Resources.Eksplozja, new Size(100, 100)),
            new Bitmap(Properties.Resources.Plomien, new Size(100, 100)),
            new Bitmap(Properties.Resources.Plomien_nad_okregiem, new Size(100, 100)),
            new Bitmap(Properties.Resources.Butla, new Size(100, 100)),
            new Bitmap(Properties.Resources.Zrace, new Size(100, 100)),
            new Bitmap(Properties.Resources.Czaszka, new Size(100, 100)),
            new Bitmap(Properties.Resources.Wykrzyknik, new Size(100, 100)),
            new Bitmap(Properties.Resources.Meduza, new Size(100, 100)),
            new Bitmap(Properties.Resources.Ryba, new Size(100, 100)),
        };

        public static IList<Point> GhsPoints = new List<Point>
        {
            new Point(50, 0),
            new Point(0, 50),
            new Point(100, 50),
            new Point(50, 100),
            new Point(150, 0),
            new Point(150, 100),
            new Point(200, 50)
        };

    }
}
