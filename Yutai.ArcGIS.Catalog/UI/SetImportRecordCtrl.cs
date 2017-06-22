using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Query.UI;

namespace Yutai.ArcGIS.Catalog.UI
{
    public partial class SetImportRecordCtrl : UserControl
    {
        private IContainer icontainer_0 = null;
        [CompilerGenerated]

        public SetImportRecordCtrl()
        {
            this.InitializeComponent();
        }

        private void btnQueryDialog_Click(object sender, EventArgs e)
        {
            frmAttributeQueryBuilder builder = new frmAttributeQueryBuilder {
                Table = this.Table,
                WhereCaluse = this.memoEdit.Text
            };
            if (builder.ShowDialog() == DialogResult.OK)
            {
                this.memoEdit.Text = builder.WhereCaluse;
            }
        }

 private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.btnQueryDialog.Enabled = this.radioButton2.Checked;
            this.memoEdit.Visible = this.radioButton2.Checked;
        }

        public ITable Table
        {
            [CompilerGenerated]
            get
            {
                return this.itable_0;
            }
            [CompilerGenerated]
            set
            {
                this.itable_0 = value;
            }
        }

        public string Where
        {
            get
            {
                if (this.radioButton1.Checked)
                {
                    return "";
                }
                return this.memoEdit.Text;
            }
        }
    }
}

