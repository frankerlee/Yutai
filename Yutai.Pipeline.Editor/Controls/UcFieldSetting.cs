using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Pipeline.Editor.Classes;
using Yutai.Pipeline.Editor.Forms.Mark;

namespace Yutai.Pipeline.Editor.Controls
{
    public partial class UcFieldSetting : UserControl, IFieldSetting
    {
        public event EventHandler<object> RemoveEvent;
        private IFields _fields;
        private int _index;

        public UcFieldSetting()
        {
            InitializeComponent();
        }

        public int Index
        {
            get { return _index; }
            set
            {
                _index = value;
                txtAttribute.Text = $@"属性{value}";
            }
        }

        public string FieldName => cmbField.Field.Name;

        public IField Field => cmbField.Field;

        public string Expression => txtExpression.Text;

        public int Length => (int)numLength.Value;

        public IFields Fields
        {
            set
            {
                _fields = value;
                if (_fields != null)
                {
                    cmbField.Enabled = true;
                    cmbField.Fields = value;

                    txtExpression.Enabled = true;
                    numLength.Enabled = true;
                }
            }
        }

        private void txtExpression_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (_fields == null)
                return;
            FrmAnnotationExpression frm = new FrmAnnotationExpression(_fields, txtExpression.Text);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                txtExpression.Text = frm.Expression;
            }
        }

        private void txtExpression_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.Show("双击控件，弹出表达式生成器！", txtExpression);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnRemoveEvent(this);
        }

        protected virtual void OnRemoveEvent(object e)
        {
            RemoveEvent?.Invoke(this, e);
        }
    }
}
