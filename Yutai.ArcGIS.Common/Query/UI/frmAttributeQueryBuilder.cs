using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Common.Query.UI
{
    public class frmAttributeQueryBuilder : Form
    {
        private AttributeQueryBuliderControl attributeQueryBuliderControl_0 = new AttributeQueryBuliderControl();
        private SimpleButton btnApply;
        private SimpleButton btnClear;
        private SimpleButton btnClose;
        private Container container_0 = null;
        private Panel panel1;
        private string string_0 = "";

        public frmAttributeQueryBuilder()
        {
            this.InitializeComponent();
            this.attributeQueryBuliderControl_0.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(this.attributeQueryBuliderControl_0);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.OK;
            this.string_0 = this.attributeQueryBuliderControl_0.WhereCaluse;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.attributeQueryBuliderControl_0.ClearWhereCaluse();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmAttributeQueryBuilder_Load(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAttributeQueryBuilder));
            this.btnApply = new SimpleButton();
            this.btnClose = new SimpleButton();
            this.panel1 = new Panel();
            this.btnClear = new SimpleButton();
            base.SuspendLayout();
            this.btnApply.Location = new Point(0x110, 0x180);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new Size(0x38, 0x18);
            this.btnApply.TabIndex = 5;
            this.btnApply.Text = "确定";
            this.btnApply.Click += new EventHandler(this.btnApply_Click);
            this.btnClose.Location = new Point(0x158, 0x180);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(0x38, 0x18);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x198, 0x178);
            this.panel1.TabIndex = 7;
            this.btnClear.Location = new Point(0x10, 0x180);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new Size(0x38, 0x18);
            this.btnClear.TabIndex = 0x33;
            this.btnClear.Text = "清除";
            this.btnClear.Click += new EventHandler(this.btnClear_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x198, 0x19f);
            base.Controls.Add(this.btnClear);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.btnClose);
            base.Controls.Add(this.btnApply);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) resources.GetObject("$Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmAttributeQueryBuilder";
            this.Text = "查询生成器";
            base.Load += new EventHandler(this.frmAttributeQueryBuilder_Load);
            base.ResumeLayout(false);
        }

        public ILayer CurrentLayer
        {
            set
            {
                this.attributeQueryBuliderControl_0.CurrentLayer = value;
            }
        }

        public ITable Table
        {
            set
            {
                this.attributeQueryBuliderControl_0.Table = value;
            }
        }

        public string WhereCaluse
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.attributeQueryBuliderControl_0.WhereCaluse = value;
            }
        }
    }
}

