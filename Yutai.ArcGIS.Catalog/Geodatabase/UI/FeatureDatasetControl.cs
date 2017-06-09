using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal class FeatureDatasetControl : UserControl
    {
        private bool bool_0 = false;
        private SimpleButton btnEditSR;
        private CheckEdit chkShowDetail;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private IFeatureDataset ifeatureDataset_0;
        private IFeatureWorkspace ifeatureWorkspace_0;
        private ISpatialReference ispatialReference_0 = null;
        private Label label1;
        private Label label2;
        private MemoEdit memoEditSRDescription;
        private TextEdit txtFeatureDatasetName;

        public event ValueChangedHandler ValueChanged;

        public FeatureDatasetControl()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_0)
            {
                if ((this.ifeatureDataset_0 as IGeoDatasetSchemaEdit).CanAlterSpatialReference)
                {
                    (this.ifeatureDataset_0 as ISchemaLock).ChangeSchemaLock(esriSchemaLock.esriExclusiveSchemaLock);
                    (this.ifeatureDataset_0 as IGeoDatasetSchemaEdit).AlterSpatialReference(this.ispatialReference_0);
                    (this.ifeatureDataset_0 as ISchemaLock).ChangeSchemaLock(esriSchemaLock.esriSharedSchemaLock);
                }
            }
            else
            {
                try
                {
                    this.ifeatureWorkspace_0.CreateFeatureDataset(this.txtFeatureDatasetName.Text, this.ispatialReference_0);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private void btnEditSR_Click(object sender, EventArgs e)
        {
            frmSpatialReference reference = new frmSpatialReference {
                SpatialRefrence = this.ispatialReference_0,
                HasXY = true,
                HasZ = true,
                HasM = true
            };
            if (reference.ShowDialog() == DialogResult.OK)
            {
                this.ispatialReference_0 = reference.SpatialRefrence;
                this.memoEditSRDescription.Text = this.GetSpatialRefrenceInfo(this.ispatialReference_0, this.chkShowDetail.Checked);
                if (this.ValueChanged != null)
                {
                    this.ValueChanged(this, e);
                }
            }
        }

        private void chkShowDetail_CheckedChanged(object sender, EventArgs e)
        {
            this.memoEditSRDescription.Text = this.GetSpatialRefrenceInfo(this.ispatialReference_0, this.chkShowDetail.Checked);
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void FeatureDatasetControl_Load(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.txtFeatureDatasetName.Enabled = false;
            }
            if (this.ifeatureDataset_0 != null)
            {
                this.txtFeatureDatasetName.Text = this.ifeatureDataset_0.Name;
            }
            this.memoEditSRDescription.Text = this.GetSpatialRefrenceInfo(this.ispatialReference_0, this.chkShowDetail.Checked);
        }

        public string GetDomainInfo(ISpatialReference ispatialReference_1)
        {
            double num5;
            double num6;
            string str = "";
            if (ispatialReference_1.HasXYPrecision())
            {
                double num;
                double num2;
                double num3;
                double num4;
                ispatialReference_1.GetDomain(out num, out num2, out num3, out num4);
                str = ((((str + "X/Y Domain: \r\n") + "  Min X:" + num.ToString("#.######") + "\r\n") + "  Min Y:" + num3.ToString("#.######") + "\r\n") + "  Max X:" + num2.ToString("#.######") + "\r\n") + "  Max Y:" + num4.ToString("#.######") + "\r\n";
                num5 = ((num2 - num) > (num4 - num3)) ? (num2 - num) : (num4 - num3);
                num6 = 2147483645.0 / num5;
                str = str + "  Scale:" + num6.ToString("#.######") + "\r\n";
            }
            if (ispatialReference_1.HasZPrecision())
            {
                double num7;
                double num8;
                str = str + "\r\n";
                ispatialReference_1.GetZDomain(out num7, out num8);
                str = ((str + "Z Domain: \r\n") + "  Min :" + num7.ToString("#.######") + "\r\n") + "  Max :" + num8.ToString("#.######") + "\r\n";
                num5 = num8 - num7;
                num6 = 2147483645.0 / num5;
                str = str + "  Scale:" + num6.ToString("#.######") + "\r\n";
            }
            if (ispatialReference_1.HasMPrecision())
            {
                double num9;
                double num10;
                str = str + "\r\n";
                ispatialReference_1.GetMDomain(out num9, out num10);
                str = ((str + "M Domain: \r\n") + "  Min :" + num9.ToString("#.######") + "\r\n") + "  Max :" + num10.ToString("#.######") + "\r\n";
                num5 = num10 - num9;
                str = str + "  Scale:" + ((2147483645.0 / num5)).ToString("#.######") + "\r\n";
            }
            return str;
        }

        public string GetSpatialRefrenceInfo(IGeographicCoordinateSystem igeographicCoordinateSystem_0, bool bool_1)
        {
            string str = "";
            if (bool_1)
            {
                str = (((str + "地理坐标系统: \r\n") + "名称:" + igeographicCoordinateSystem_0.Name + "\r\n") + "  别名: " + igeographicCoordinateSystem_0.Alias + "\r\n") + "  缩略名: " + igeographicCoordinateSystem_0.Abbreviation + "\r\n";
                string[] strArray = new string[] { 
                    str, "  说明: ", igeographicCoordinateSystem_0.Remarks, "\r\n  角度单位: ", igeographicCoordinateSystem_0.CoordinateUnit.Name, " (", igeographicCoordinateSystem_0.CoordinateUnit.RadiansPerUnit.ToString(), ")\r\n  本初子午线: ", igeographicCoordinateSystem_0.PrimeMeridian.Name, " (", igeographicCoordinateSystem_0.PrimeMeridian.Longitude.ToString(), ")\r\n  数据: ", igeographicCoordinateSystem_0.Datum.Name, "\r\n    椭球体: ", igeographicCoordinateSystem_0.Datum.Spheroid.Name, "\r\n      长半轴: ", 
                    igeographicCoordinateSystem_0.Datum.Spheroid.SemiMajorAxis.ToString(), "\r\n      短半轴: ", igeographicCoordinateSystem_0.Datum.Spheroid.SemiMinorAxis.ToString(), "\r\n      扁率倒数: ", (1.0 / igeographicCoordinateSystem_0.Datum.Spheroid.Flattening).ToString()
                 };
                return string.Concat(strArray);
            }
            return ((str + "地理坐标系统: \r\n") + "  名称:" + igeographicCoordinateSystem_0.Name + "\r\n");
        }

        public string GetSpatialRefrenceInfo(IProjectedCoordinateSystem iprojectedCoordinateSystem_0, bool bool_1)
        {
            string str = "";
            if (!bool_1)
            {
                return ((((str + "投影坐标系统: \r\n") + "  名称:" + iprojectedCoordinateSystem_0.Name + "\r\n") + "\r\n" + "地理坐标系统: \r\n") + "  名称:" + iprojectedCoordinateSystem_0.GeographicCoordinateSystem.Name + "\r\n");
            }
            IParameter[] parameters = new IParameter[0x19];
            ((IProjectedCoordinateSystem4GEN) iprojectedCoordinateSystem_0).GetParameters(ref parameters);
            string str2 = "  ";
            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i] == null)
                {
                    break;
                }
                str2 = str2 + parameters[i].Name + ": " + parameters[i].Value.ToString() + "\r\n ";
            }
            str = ((((((str + "投影坐标系统: \r\n") + "名称:" + iprojectedCoordinateSystem_0.Name + "\r\n") + "  别名: " + iprojectedCoordinateSystem_0.Alias + "\r\n") + "   缩略名: " + iprojectedCoordinateSystem_0.Abbreviation + "\r\n") + "   说明: " + iprojectedCoordinateSystem_0.Remarks + "\r\n") + "投影: " + iprojectedCoordinateSystem_0.Projection.Name + "\r\n") + "参数:\r\n" + str2;
            str = ((((str + "线性单位: " + iprojectedCoordinateSystem_0.CoordinateUnit.Name + " (" + iprojectedCoordinateSystem_0.CoordinateUnit.MetersPerUnit.ToString() + ")\r\n") + "地理坐标系:\r\n") + "  名称: " + iprojectedCoordinateSystem_0.GeographicCoordinateSystem.Name + "\r\n") + "  缩略名: " + iprojectedCoordinateSystem_0.GeographicCoordinateSystem.Abbreviation + "\r\n") + "  说明: " + iprojectedCoordinateSystem_0.GeographicCoordinateSystem.Remarks + "\r\n";
            str = str + "  角度单位: " + iprojectedCoordinateSystem_0.GeographicCoordinateSystem.CoordinateUnit.Name + " (" + iprojectedCoordinateSystem_0.GeographicCoordinateSystem.CoordinateUnit.RadiansPerUnit.ToString() + ")\r\n";
            double num2 = 1.0 / iprojectedCoordinateSystem_0.GeographicCoordinateSystem.Datum.Spheroid.Flattening;
            return ((((((str + "  本初子午线: " + iprojectedCoordinateSystem_0.GeographicCoordinateSystem.PrimeMeridian.Name + " (" + iprojectedCoordinateSystem_0.GeographicCoordinateSystem.PrimeMeridian.Longitude.ToString() + ")\r\n") + "  数据: " + iprojectedCoordinateSystem_0.GeographicCoordinateSystem.Datum.Name + "\r\n") + "    椭球体: " + iprojectedCoordinateSystem_0.GeographicCoordinateSystem.Datum.Spheroid.Name + "\r\n") + "    长半轴: " + iprojectedCoordinateSystem_0.GeographicCoordinateSystem.Datum.Spheroid.SemiMajorAxis.ToString() + "\r\n") + "    短半轴: " + iprojectedCoordinateSystem_0.GeographicCoordinateSystem.Datum.Spheroid.SemiMinorAxis.ToString() + "\r\n") + "    扁率倒数: " + num2.ToString());
        }

        public string GetSpatialRefrenceInfo(ISpatialReference ispatialReference_1, bool bool_1)
        {
            if (ispatialReference_1 is IGeographicCoordinateSystem)
            {
                return (this.GetSpatialRefrenceInfo(ispatialReference_1 as IGeographicCoordinateSystem, bool_1) + "\r\n" + this.GetDomainInfo(ispatialReference_1));
            }
            if (ispatialReference_1 is IProjectedCoordinateSystem)
            {
                return (this.GetSpatialRefrenceInfo(ispatialReference_1 as IProjectedCoordinateSystem, bool_1) + "\r\n" + this.GetDomainInfo(ispatialReference_1));
            }
            string str = "未知坐标系统";
            if (bool_1)
            {
                str = ((((str + "\r\n名称:" + ispatialReference_1.Name + "\r\n") + "  别名: " + ispatialReference_1.Alias + "\r\n") + "   缩略名: " + ispatialReference_1.Abbreviation + "\r\n") + "   说明: " + ispatialReference_1.Remarks + "\r\n") + "\r\n" + this.GetDomainInfo(ispatialReference_1);
            }
            return str;
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.txtFeatureDatasetName = new TextEdit();
            this.groupBox1 = new GroupBox();
            this.btnEditSR = new SimpleButton();
            this.chkShowDetail = new CheckEdit();
            this.memoEditSRDescription = new MemoEdit();
            this.label2 = new Label();
            this.txtFeatureDatasetName.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            this.chkShowDetail.Properties.BeginInit();
            this.memoEditSRDescription.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0x10, 12);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称:";
            this.txtFeatureDatasetName.EditValue = "";
            this.txtFeatureDatasetName.Location = new System.Drawing.Point(0x40, 8);
            this.txtFeatureDatasetName.Name = "txtFeatureDatasetName";
            this.txtFeatureDatasetName.Size = new Size(0xc0, 0x17);
            this.txtFeatureDatasetName.TabIndex = 1;
            this.groupBox1.Controls.Add(this.btnEditSR);
            this.groupBox1.Controls.Add(this.chkShowDetail);
            this.groupBox1.Controls.Add(this.memoEditSRDescription);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(0x10, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x108, 0x108);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "空间参考";
            this.btnEditSR.Location = new System.Drawing.Point(160, 0xe7);
            this.btnEditSR.Name = "btnEditSR";
            this.btnEditSR.Size = new Size(0x40, 0x18);
            this.btnEditSR.TabIndex = 5;
            this.btnEditSR.Text = "编辑...";
            this.btnEditSR.Click += new EventHandler(this.btnEditSR_Click);
            this.chkShowDetail.Location = new System.Drawing.Point(0x10, 0xe7);
            this.chkShowDetail.Name = "chkShowDetail";
            this.chkShowDetail.Properties.Caption = "显示详细信息";
            this.chkShowDetail.Size = new Size(0x68, 0x13);
            this.chkShowDetail.TabIndex = 4;
            this.chkShowDetail.CheckedChanged += new EventHandler(this.chkShowDetail_CheckedChanged);
            this.memoEditSRDescription.EditValue = "";
            this.memoEditSRDescription.Location = new System.Drawing.Point(0x10, 0x30);
            this.memoEditSRDescription.Name = "memoEditSRDescription";
            this.memoEditSRDescription.Properties.Appearance.BackColor = SystemColors.InactiveBorder;
            this.memoEditSRDescription.Properties.Appearance.Options.UseBackColor = true;
            this.memoEditSRDescription.Properties.ReadOnly = true;
            this.memoEditSRDescription.Size = new Size(0xe8, 0xa8);
            this.memoEditSRDescription.TabIndex = 3;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0x10, 0x17);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x23, 0x11);
            this.label2.TabIndex = 1;
            this.label2.Text = "说明:";
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.txtFeatureDatasetName);
            base.Controls.Add(this.label1);
            base.Name = "FeatureDatasetControl";
            base.Size = new Size(0x128, 320);
            base.Load += new EventHandler(this.FeatureDatasetControl_Load);
            this.txtFeatureDatasetName.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.chkShowDetail.Properties.EndInit();
            this.memoEditSRDescription.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private ISpatialReference method_0(bool bool_1)
        {
            new SpatialReferenceEnvironmentClass();
            ISpatialReference reference = new UnknownCoordinateSystemClass();
            IGeodatabaseRelease release = this.ifeatureWorkspace_0 as IGeodatabaseRelease;
            IControlPrecision2 precision = reference as IControlPrecision2;
            bool_1 = GeodatabaseTools.GetGeoDatasetPrecision(release);
            precision.IsHighPrecision = bool_1;
            ISpatialReferenceResolution resolution = reference as ISpatialReferenceResolution;
            resolution.ConstructFromHorizon();
            resolution.SetDefaultXYResolution();
            (reference as ISpatialReferenceTolerance).SetDefaultXYTolerance();
            return reference;
        }

        public IFeatureDataset FeatureDataset
        {
            get
            {
                return this.ifeatureDataset_0;
            }
            set
            {
                this.ifeatureDataset_0 = value;
                this.ispatialReference_0 = (this.ifeatureDataset_0 as IGeoDataset).SpatialReference;
            }
        }

        public IFeatureWorkspace FeatureWorkspace
        {
            set
            {
                this.ifeatureWorkspace_0 = value;
                this.ispatialReference_0 = this.method_0(false);
            }
        }

        public bool IsEdit
        {
            set
            {
                this.bool_0 = value;
            }
        }
    }
}

