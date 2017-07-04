using System.ComponentModel;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.Pipeline.Editor.Controls
{
    public partial class FieldsListBox : ListBox
    {
        private IFields _fields;

        public FieldsListBox()
        {
            InitializeComponent();
        }

        public FieldsListBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public IFields Fields
        {
            set
            {
                _fields = value;
                InitControl();
            }
        }

        private void InitControl()
        {
            int count = _fields.FieldCount;
            for (int i = 0; i < count; i++)
            {
                IField pField = _fields.Field[i];
                if (pField.Type == esriFieldType.esriFieldTypeGeometry)
                    continue;
                this.Items.Add(pField.Name);
            }
        }
    }
}