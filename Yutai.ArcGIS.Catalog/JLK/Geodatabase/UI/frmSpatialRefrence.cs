namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.Geometry;
    using JLK.Catalog;
    using JLK.Editors;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class frmSpatialRefrence : Form
    {
        private bool bool_0 = true;
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private Container container_0 = null;
        private enumSpatialRefrenceType enumSpatialRefrenceType_0 = enumSpatialRefrenceType.enumProjectCoord;
        private GeoCoordSys geoCoordSys_0 = new GeoCoordSys();
        private ISpatialReference ispatialReference_0;
        private ProjCoordSys projCoordSys_0 = new ProjCoordSys();

        public frmSpatialRefrence()
        {
            if (CatalogLicenseProviderCheck.Check())
            {
                this.InitializeComponent();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                switch (this.enumSpatialRefrenceType_0)
                {
                    case enumSpatialRefrenceType.enumProjectCoord:
                        this.ispatialReference_0 = this.projCoordSys_0.GetSpatialRefrence();
                        if (this.ispatialReference_0 != null)
                        {
                            goto Label_0065;
                        }
                        return;

                    case enumSpatialRefrenceType.enumGeographicCoord:
                        this.ispatialReference_0 = this.geoCoordSys_0.GetSpatialRefrence();
                        if (this.ispatialReference_0 != null)
                        {
                            goto Label_0065;
                        }
                        return;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            return;
        Label_0065:
            base.DialogResult = DialogResult.OK;
            base.Close();
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void frmSpatialRefrence_Load(object sender, EventArgs e)
        {
            switch (this.enumSpatialRefrenceType_0)
            {
                case enumSpatialRefrenceType.enumProjectCoord:
                    if (this.ispatialReference_0 == null)
                    {
                        this.Text = "新建投影坐标系统";
                        break;
                    }
                    this.Text = "投影坐标系统属性";
                    this.projCoordSys_0.ProjectedCoordinateSystem = (IProjectedCoordinateSystem) this.ispatialReference_0;
                    break;

                case enumSpatialRefrenceType.enumGeographicCoord:
                    if (this.ispatialReference_0 == null)
                    {
                        this.Text = "新建地理坐标系统";
                    }
                    else
                    {
                        this.Text = "地理坐标系统属性";
                        this.geoCoordSys_0.GeographicCoordinateSystem = (IGeographicCoordinateSystem) this.ispatialReference_0;
                    }
                    base.Controls.Add(this.geoCoordSys_0);
                    return;

                default:
                    return;
            }
            base.Controls.Add(this.projCoordSys_0);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmSpatialRefrence));
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            base.SuspendLayout();
            this.btnOK.Location = new System.Drawing.Point(0xb0, 0x1c8);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x38, 0x18);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(0xf8, 0x1c8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x38, 0x18);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x142, 0x1e7);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmSpatialRefrence";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterParent;
            base.Load += new EventHandler(this.frmSpatialRefrence_Load);
            base.ResumeLayout(false);
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
                if (this.ispatialReference_0 is IGeographicCoordinateSystem)
                {
                    this.enumSpatialRefrenceType_0 = enumSpatialRefrenceType.enumGeographicCoord;
                }
                else if (this.ispatialReference_0 is IProjectedCoordinateSystem)
                {
                    this.enumSpatialRefrenceType_0 = enumSpatialRefrenceType.enumProjectCoord;
                }
            }
        }

        public enumSpatialRefrenceType SpatialRefrenceType
        {
            get
            {
                return this.enumSpatialRefrenceType_0;
            }
            set
            {
                this.enumSpatialRefrenceType_0 = value;
            }
        }

        public enum enumSpatialRefrenceType
        {
            enumProjectCoord,
            enumGeographicCoord
        }
    }
}

