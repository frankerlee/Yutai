using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.Pipeline.Editor.Forms
{
    public partial class FieldCalculator : Form
    {
        private IFields _fields;

        public FieldCalculator(IFields fields)
        {
            InitializeComponent();
            _fields = fields;
            fieldListBox.Fields = _fields;
        }

        public string Expression
        {
            get { return txtExpression.Text; }
        }

        public int Length
        {
            get { return (int) numLength.Value; }
        }

        private void fieldListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.fieldListBox.IndexFromPoint(e.Location);
            if (index == ListBox.NoMatches)
                return;
            txtExpression.Add($"[{fieldListBox.Items[index]}]");
        }

        private void btnAddSerial_Click(object sender, EventArgs e)
        {
            txtExpression.Add($"[#####]");
        }
    }
}