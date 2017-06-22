using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class frmExpressionSet : Form
    {
        private Container container_0 = null;
        private LabelExpressionSetPropertyPage labelExpressionSetPropertyPage_0 = new LabelExpressionSetPropertyPage();

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

