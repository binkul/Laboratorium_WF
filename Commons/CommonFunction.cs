using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Laboratorium.Commons
{
    public enum States
    {
        None,
        Added,
        Modified,
        Deletec
    }

    public static class CommonFunction
    {
        private static string CreatePath(string fileName)
        {
            string path = Directory.GetCurrentDirectory();
            path = string.Concat(path, @"\Dane\", fileName, ".xml");

            return path;
        }

        private static string FindPath(string fileName)
        {
            string target = @"\Dane";
            string path = Directory.GetCurrentDirectory() + target;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = CreatePath(fileName);
            File.Delete(path);

            return path;
        }

        public static void WriteWindowsData(this List<double> list, string fileName)
        {
            var serializer = new XmlSerializer(typeof(List<double>));
            using (var stream = File.OpenWrite(FindPath(fileName)))
            {
                serializer.Serialize(stream, list);
                stream.Flush();
            }
        }

        public static void WriteWindowsData(IList<SerializeClass> list, string fileName)
        {
            var serializer = new XmlSerializer(typeof(List<SerializeClass>));
            using (var stream = File.OpenWrite(FindPath(fileName)))
            {
                serializer.Serialize(stream, list);
                stream.Flush();
            }
        }

        public static void WriteWindowsData(IDictionary<string, double> list, string fileName)
        {
            List<SerializeClass> serializeClasses = new List<SerializeClass>();
            foreach (var entry in list)
            {
                serializeClasses.Add(new SerializeClass(entry.Key, entry.Value));
            }

            WriteWindowsData(serializeClasses, fileName);
        }

        public static List<double> LoadWindowsDataAsList(string fileName)
        {
            List<double> windowsData = new List<double>();
            var serializer = new XmlSerializer(typeof(List<double>));

            string path = CreatePath(fileName);
            if (!File.Exists(path))
                return windowsData;

            using (var stream = File.OpenRead(path))
            {
                try
                {
                    var list = (List<double>)(serializer.Deserialize(stream));
                    windowsData.Clear();
                    windowsData.AddRange(list);
                    stream.Close();
                }
                catch
                {
                    windowsData = new List<double>();
                }
            }

            return windowsData;
        }

        public static IDictionary<string, double> LoadWindowsDataAsDictionary(string fileName)
        {
            IDictionary<string, double> windowsData = new Dictionary<string, double>();

            string path = CreatePath(fileName);
            if (!File.Exists(path))
                return windowsData;

            var serializer = new XmlSerializer(typeof(List<SerializeClass>));
            using (var stream = File.OpenRead(path))
            {
                try
                {
                    List<SerializeClass> list = (List<SerializeClass>)(serializer.Deserialize(stream));
                    stream.Close();
                    foreach (var item in list)
                    {
                        windowsData.Add(item.Key, item.Value);
                    }
                }
                catch
                {
                    windowsData = new Dictionary<string, double>();
                }
            }

            return windowsData;
        }

        public static double? DBNullToDoubleConv(object number)
        {
            return number.Equals(DBNull.Value) ? null : (double?)Convert.ToDouble(number);
        }

        public static string DBNullToStringConv(object text)
        {
            return text.Equals(DBNull.Value) ? null : text.ToString();
        }

        public static object NullStringToDBNullConv(string text)
        {
            return String.IsNullOrEmpty(text) ? DBNull.Value : (object)text;
        }

        public static object NullDoubleToDBNullConv(double? input)
        {
            return input == null ? DBNull.Value : (object)input;
        }

        public static DataGridViewButtonColumn GetDgvDeleteButtonColumn()
        {
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.Name = "Del";
            buttonColumn.HeaderText = "Del";
            buttonColumn.Text = "X";
            buttonColumn.FlatStyle = FlatStyle.Popup;
            buttonColumn.DefaultCellStyle.ForeColor = Color.Red;
            buttonColumn.DefaultCellStyle.BackColor = Color.LightGray;
            buttonColumn.UseColumnTextForButtonValue = true;
            buttonColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            buttonColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            buttonColumn.Resizable = DataGridViewTriState.False;
            buttonColumn.Width = 45;
            buttonColumn.DisplayIndex = 0;
            buttonColumn.ToolTipText = "Usuń";

            return buttonColumn;
        }

        public static double Percent (double value, double percent)
        {
            return value * percent / 100d;
        }
    }
}
