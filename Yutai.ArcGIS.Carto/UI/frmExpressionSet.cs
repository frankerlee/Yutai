using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class frmExpressionSet : Form
    {
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private Container container_0 = null;
        private LabelExpressionSetPropertyPage labelExpressionSetPropertyPage_0 = new LabelExpressionSetPropertyPage();
        private Panel panel1;
        private Panel panel2;

        public frmExpressionSet()
        {
            this.InitializeComponent();
            this.labelExpressionSetPropertyPage_0.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(this.labelExpressionSetPropertyPage_0);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.labelExpressionSetPropertyPage_0.Apply();
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExpressionSet));
            this.panel1 = new Panel();
            this.btnCancel = new SimpleButton();
            this.btnOK = new SimpleButton();
            this.panel2 = new Panel();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 0x15d);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x130, 0x20);
            this.panel1.TabIndex = 0;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(240, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x38, 0x18);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0xb0, 6);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x38, 0x18);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x130, 0x15d);
            this.panel2.TabIndex = 1;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x130, 0x17d);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Icon = ((System.Drawing.Icon)resources.GetObject("$Icon"));
            base.Name = "frmExpressionSet";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "表达式";
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public IAnnotationExpressionEngine AnnotationExpressionEngine
        {
            get
            {
                return this.labelExpressionSetPropertyPage_0.AnnotationExpressionEngine;
            }
            set
            {
                this.labelExpressionSetPropertyPage_0.AnnotationExpressionEngine = value;
            }
        }

        public IGeoFeatureLayer GeoFeatureLayer
        {
            set
            {
                this.labelExpressionSetPropertyPage_0.GeoFeatureLayer = value;
            }
        }

        public bool IsExpressionSimple
        {
            get
            {
                return this.labelExpressionSetPropertyPage_0.IsExpressionSimple;
            }
            set
            {
                this.labelExpressionSetPropertyPage_0.IsExpressionSimple = value;
            }
        }

        public ILabelEngineLayerProperties LabelEngineLayerProp
        {
            set
            {
                this.labelExpressionSetPropertyPage_0.LabelEngineLayerProp = value;
            }
        }

        public string LabelExpression
        {
            get
            {
                return this.labelExpressionSetPropertyPage_0.LabelExpression;
            }
            set
            {
                this.labelExpressionSetPropertyPage_0.LabelExpression = value;
            }
        }
    }
}

