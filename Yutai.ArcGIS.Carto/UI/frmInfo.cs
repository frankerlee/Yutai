using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Carto;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class frmInfo : Form
    {
        private Container container_0 = null;
        private FeatureInfoControl featureInfoControl_0 = new FeatureInfoControl();

        public event IdentifyLayerChangedHandler IdentifyLayerChanged;

        public event IdentifyTypeChangedHandler IdentifyTypeChanged;

        public frmInfo()
        {
            this.InitializeComponent();
            this.featureInfoControl_0.Dock = DockStyle.Fill;
            base.Controls.Add(this.featureInfoControl_0);
            this.featureInfoControl_0.IdentifyTypeChanged += new FeatureInfoControl.IdentifyTypeChangedHandler(this.method_1);
            this.featureInfoControl_0.IdentifyLayerChanged += new FeatureInfoControl.IdentifyLayerChangedHandler(this.method_2);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInfo));
            base.SuspendLayout();
            base.ClientSize = new Size(0x124, 0x111);
            
            base.Name = "frmInfo";
            base.ResumeLayout(false);
        }

        private void method_0(object sender, EventArgs e)
        {
        }

        private void method_1(object object_0, object object_1)
        {
            if (this.IdentifyTypeChanged != null)
            {
                this.IdentifyTypeChanged(this, object_1);
            }
        }

        private void method_2(object object_0, object object_1)
        {
            if (this.IdentifyLayerChanged != null)
            {
                this.IdentifyLayerChanged(this, object_1);
            }
        }

        public void SetControl()
        {
            this.featureInfoControl_0.SetControl();
        }

        public void SetInfo(IPoint ipoint_0, IArray iarray_0)
        {
            this.featureInfoControl_0.SetInfo(ipoint_0, iarray_0);
        }

        public void SetInfo(IPoint ipoint_0, IArray iarray_0, IFeature ifeature_0)
        {
            this.featureInfoControl_0.SetInfo(ipoint_0, iarray_0, ifeature_0);
        }

        public ILayer CurrentLayer
        {
            set
            {
                this.featureInfoControl_0.CurrentLayer = value;
            }
        }

        public IBasicMap FocusMap
        {
            set
            {
                this.featureInfoControl_0.FocusMap = value;
            }
        }

        public IdentifyTypeEnum IdentifyType
        {
            set
            {
                this.featureInfoControl_0.IdentifyType = value;
            }
        }

        public bool IsIdentify
        {
            set
            {
                this.featureInfoControl_0.IsIdentify = value;
            }
        }

        public delegate void IdentifyLayerChangedHandler(object object_0, object object_1);

        public delegate void IdentifyTypeChangedHandler(object object_0, object object_1);
    }
}

