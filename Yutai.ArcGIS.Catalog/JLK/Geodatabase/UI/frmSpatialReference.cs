namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.Geometry;
    using JLK.Catalog;
    using JLK.Editors;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class frmSpatialReference : Form
    {
        private bool bool_0 = true;
        private bool bool_1 = false;
        private bool bool_2 = false;
        private bool bool_3 = false;
        private bool bool_4 = false;
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private Container container_0 = null;
        private CoordinateControl coordinateControl_0;
        private ISpatialReference ispatialReference_0;
        private MDomainControl mdomainControl_0;
        private TabControl tabControl1;
        private XYDomainControl xydomainControl_0;
        private ZDomainControl zdomainControl_0;

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

        protected override void Dispose(bool bool_5)
        {
            if (bool_5 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_5);
        }

        private void frmSpatialReference_Load(object sender, EventArgs e)
        {
            this.method_1();
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmSpatialReference));
            this.tabControl1 = new TabControl();
            this.btnCancel = new SimpleButton();
            this.btnOK = new SimpleButton();
            base.SuspendLayout();
            this.tabControl1.Location = new System.Drawing.Point(8, 0x10);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(0x138, 0x1b0);
            this.tabControl1.TabIndex = 0;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(0x100, 0x1d0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x38, 0x18);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(0xb8, 0x1d0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x38, 0x18);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(330, 0x1ed);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.tabControl1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmSpatialReference";
            base.ShowInTaskbar = false;
            this.Text = "空间参考";
            base.Load += new EventHandler(this.frmSpatialReference_Load);
            base.ResumeLayout(false);
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

