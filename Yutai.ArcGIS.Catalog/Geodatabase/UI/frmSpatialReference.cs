using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmSpatialReference : Form
    {
        private bool bool_0 = true;
        private bool bool_1 = false;
        private bool bool_2 = false;
        private bool bool_3 = false;
        private bool bool_4 = false;
        private Container container_0 = null;

        public frmSpatialReference()
        {
            if (CatalogLicenseProviderCheck.Check())
            {
                this.InitializeComponent();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.method_2();
            }
        }

 private void frmSpatialReference_Load(object sender, EventArgs e)
        {
            this.method_1();
        }

 private ISpatialReference method_0(bool bool_5)
        {
            ISpatialReference reference = new UnknownCoordinateSystemClass();
            IControlPrecision2 precision = reference as IControlPrecision2;
            precision.IsHighPrecision = bool_5;
            ISpatialReferenceResolution resolution = reference as ISpatialReferenceResolution;
            resolution.ConstructFromHorizon();
            resolution.SetDefaultXYResolution();
            (reference as ISpatialReferenceTolerance).SetDefaultXYTolerance();
            return reference;
        }

        private void method_1()
        {
            if (this.ispatialReference_0 == null)
            {
                this.ispatialReference_0 = this.method_0(this.bool_4);
            }
            this.coordinateControl_0 = new CoordinateControl();
            this.coordinateControl_0.IsEdit = this.bool_0;
            this.coordinateControl_0.SpatialReferenceChanged += new CoordinateControl.SpatialReferenceChangedHandler(this.method_4);
            this.coordinateControl_0.SpatialRefrence = this.ispatialReference_0;
            TabPage page = new TabPage("坐标系统");
            page.Controls.Add(this.coordinateControl_0);
            this.tabControl1.TabPages.Add(page);
            if (this.bool_1)
            {
                this.xydomainControl_0 = new XYDomainControl();
                this.xydomainControl_0.IsEdit = this.bool_0;
                this.xydomainControl_0.SpatialRefrence = this.ispatialReference_0;
                TabPage page2 = new TabPage("X/Y域值");
                page2.Controls.Add(this.xydomainControl_0);
                this.tabControl1.TabPages.Add(page2);
            }
            if (this.bool_3)
            {
                this.zdomainControl_0 = new ZDomainControl();
                this.zdomainControl_0.IsEdit = this.bool_0;
                this.zdomainControl_0.SpatialRefrence = this.ispatialReference_0;
                TabPage page3 = new TabPage("Z域值");
                page3.Controls.Add(this.zdomainControl_0);
                this.tabControl1.TabPages.Add(page3);
            }
            if (this.bool_2)
            {
                this.mdomainControl_0 = new MDomainControl();
                this.mdomainControl_0.IsEdit = this.bool_0;
                this.mdomainControl_0.SpatialRefrence = this.ispatialReference_0;
                TabPage page4 = new TabPage("M域值");
                page4.Controls.Add(this.mdomainControl_0);
                this.tabControl1.TabPages.Add(page4);
            }
        }

        private void method_2()
        {
            double num5;
            double num6;
            this.ispatialReference_0 = this.coordinateControl_0.SpatialRefrence;
            if (this.bool_1)
            {
                double num;
                double num2;
                double num3;
                double num4;
                this.xydomainControl_0.SpatialRefrence.GetDomain(out num, out num2, out num3, out num4);
                this.ispatialReference_0.SetDomain(num, num2, num3, num4);
            }
            if (this.bool_3)
            {
                this.zdomainControl_0.SpatialRefrence.GetZDomain(out num5, out num6);
                this.ispatialReference_0.SetZDomain(num5, num6);
            }
            if (this.bool_2)
            {
                this.mdomainControl_0.SpatialRefrence.GetMDomain(out num5, out num6);
                this.ispatialReference_0.SetMDomain(num5, num6);
            }
        }

        private void method_3(object sender, EventArgs e)
        {
        }

        private void method_4(object object_0)
        {
            if (this.bool_1)
            {
                this.xydomainControl_0.SpatialRefrence = object_0 as ISpatialReference;
            }
            if (this.bool_3)
            {
                this.zdomainControl_0.SpatialRefrence = object_0 as ISpatialReference;
            }
            if (this.bool_2)
            {
                this.mdomainControl_0.SpatialRefrence = object_0 as ISpatialReference;
            }
        }

        public bool HasM
        {
            get
            {
                return this.bool_2;
            }
            set
            {
                this.bool_2 = value;
            }
        }

        public bool HasXY
        {
            get
            {
                return this.bool_1;
            }
            set
            {
                this.bool_1 = value;
            }
        }

        public bool HasZ
        {
            get
            {
                return this.bool_3;
            }
            set
            {
                this.bool_3 = value;
            }
        }

        public bool IsEdit
        {
            set
            {
                this.bool_0 = value;
            }
        }

        public bool IsHighPrecision
        {
            set
            {
                this.bool_4 = value;
            }
        }

        public ISpatialReference SpatialRefrence
        {
            get
            {
                return this.ispatialReference_0;
            }
            set
            {
                this.ispatialReference_0 = value;
                if (this.ispatialReference_0 != null)
                {
                    this.bool_1 = this.ispatialReference_0.HasXYPrecision();
                    this.bool_3 = this.ispatialReference_0.HasZPrecision();
                    this.bool_2 = this.ispatialReference_0.HasMPrecision();
                    IControlPrecision2 precision = this.ispatialReference_0 as IControlPrecision2;
                    this.bool_4 = precision.IsHighPrecision;
                }
            }
        }
    }
}

