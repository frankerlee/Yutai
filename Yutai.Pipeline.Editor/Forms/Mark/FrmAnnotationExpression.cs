using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.Pipeline.Editor.Forms.Mark
{
    public partial class FrmAnnotationExpression : Form
    {
        public FrmAnnotationExpression(IFields fields, string expression)
        {
            InitializeComponent();
            fieldsListBox.Fields = fields;
            TxtExpression.Text = expression;
        }

        public string Expression => TxtExpression.Text;

        private void fieldsListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.fieldsListBox.IndexFromPoint(e.Location);
            if (index == ListBox.NoMatches)
                return;
            //TxtExpression.AppendText($"[{fieldsListBox.Items[index]}]");
            int startIndex = TxtExpression.SelectionStart;
            if (string.IsNullOrWhiteSpace(TxtExpression.SelectedText))
            {
                TxtExpression.Text = TxtExpression.Text.Insert(startIndex, fieldsListBox.Items[index].ToString());
            }
            else
            {
                TxtExpression.SelectedText = fieldsListBox.Items[index].ToString();
            }
        }

        private void BtnAppend_Click(object sender, EventArgs e)
        {
            if (fieldsListBox.SelectedItem != null)
                TxtExpression.AppendText($"  & \" \" & [{fieldsListBox.SelectedItem}] ");
        }
    }
}
