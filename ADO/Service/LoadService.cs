using Laboratorium.Commons;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Laboratorium.ADO.Service
{
    public abstract class LoadService : ILoadService
    {
        private const string FORM_TOP = "Form_Top";
        private const string FORM_LEFT = "Form_Left";
        private const string FORM_WIDTH = "Form_Width";
        private const string FORM_HEIGHT = "Form_Height";

        private readonly string _formName;
        protected readonly Form _baseForm;
        protected IDictionary<DataGridView, IList<string>> _fields;
        protected IDictionary<string, double> _formData;

        protected LoadService(string formName, Form form)
        {
            _formName = formName;
            _baseForm = form;
            _formData = CommonFunction.LoadWindowsDataAsDictionary(_formName);
            PrepareColumns();
        }

        public DialogResult FormClose(FormClosingEventArgs e)
        {
            DialogResult result = DialogResult.No;

            if (Status)
            {
                result = MessageBox.Show("Wprowadzono zmiany w formularzu. Czy zapisać je?", "Zapis zmian", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    e.Cancel = !Save();
                }
                else if (result == DialogResult.Cancel)
                    e.Cancel = true;
            }


            if (!e.Cancel)
            {
                SaveFormData();
            }

            return result;
        }

        public void LoadFormData()
        {
            _baseForm.Top = _formData.ContainsKey(FORM_TOP) ? (int)_formData[FORM_TOP] : _baseForm.Top;
            _baseForm.Left = _formData.ContainsKey(FORM_LEFT) ? (int)_formData[FORM_LEFT] : _baseForm.Left;
            _baseForm.Width = _formData.ContainsKey(FORM_WIDTH) ? (int)_formData[FORM_WIDTH] : _baseForm.Width;
            _baseForm.Height = _formData.ContainsKey(FORM_HEIGHT) ? (int)_formData[FORM_HEIGHT] : _baseForm.Height;
        }

        protected void SaveFormData()
        {
            IDictionary<string, double> list = new Dictionary<string, double>();

            list.Add(FORM_TOP, _baseForm.Top);
            list.Add(FORM_LEFT, _baseForm.Left);
            list.Add(FORM_WIDTH, _baseForm.Width);
            list.Add(FORM_HEIGHT, _baseForm.Height);

            if (_fields != null && _fields.Count > 0)
            {
                foreach (KeyValuePair<DataGridView, IList<string>> entry in _fields)
                {
                    DataGridView view = entry.Key;
                    IList<string> columns = entry.Value;
                    foreach (string column in columns)
                    {
                        list.Add(view.Columns[column].Name, view.Columns[column].Width);
                    }
                }
            }

            CommonFunction.WriteWindowsData(list, _formName);
        }

        public abstract bool Save();

        public abstract void PrepareAllData();

        protected abstract void PrepareColumns();

        protected abstract bool Status { get; }
    }
}
