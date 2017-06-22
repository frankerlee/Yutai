using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal partial class VCSCoordinateInfoPage : UserControl
    {
        private bool bool_0 = true;
        private IContainer icontainer_0 = null;
        private ISpatialReferenceFactory ispatialReferenceFactory_0 = new SpatialReferenceEnvironmentClass();

        public event SpatialReferenceChangedHandler SpatialReferenceChanged;

        public VCSCoordinateInfoPage()
        {
            this.InitializeComponent();
        }

 private string method_0(ISpatialReferenceInfo ispatialReferenceInfo_0)
        {
            IGeographicCoordinateSystem geographicCoordinateSystem;
            string str2;
            this.textBoxName.Text = ispatialReferenceInfo_0.Name;
            if (ispatialReferenceInfo_0 is IVerticalCoordinateSystem)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("线性单位:");
                builder.Append((ispatialReferenceInfo_0 as IVerticalCoordinateSystem).CoordinateUnit.Name);
                builder.Append("\r\n");
                if ((ispatialReferenceInfo_0 as IVerticalCoordinateSystem).PositiveDirection == -1)
                {
                    builder.Append("方向:正\r\n");
                }
                else
                {
                    builder.Append("方向:负\r\n");
                }
                builder.Append("垂直偏移:");
                builder.Append((ispatialReferenceInfo_0 as IVerticalCoordinateSystem).VerticalShift.ToString());
                builder.Append("\r\n");
                builder.Append("垂直坐标框架:");
                builder.Append(((ispatialReferenceInfo_0 as IVerticalCoordinateSystem).Datum as ISpatialReferenceInfo).Name);
                return builder.ToString();
            }
            if (ispatialReferenceInfo_0 is IGeographicCoordinateSystem)
            {
                geographicCoordinateSystem = (IGeographicCoordinateSystem) ispatialReferenceInfo_0;
                str2 = ("别名: " + geographicCoordinateSystem.Alias + "\r\n") + "缩略名: " + geographicCoordinateSystem.Abbreviation + "\r\n";
                string[] strArray = new string[] { 
                    str2, "说明: ", geographicCoordinateSystem.Remarks, "\r\n角度单位: ", geographicCoordinateSystem.CoordinateUnit.Name, " (", geographicCoordinateSystem.CoordinateUnit.RadiansPerUnit.ToString(), ")\r\n本初子午线: ", geographicCoordinateSystem.PrimeMeridian.Name, " (", geographicCoordinateSystem.PrimeMeridian.Longitude.ToString(), ")\r\n数据: ", geographicCoordinateSystem.Datum.Name, "\r\n  椭球体: ", geographicCoordinateSystem.Datum.Spheroid.Name, "\r\n    长半轴: ", 
                    geographicCoordinateSystem.Datum.Spheroid.SemiMajorAxis.ToString(), "\r\n    短半轴: ", geographicCoordinateSystem.Datum.Spheroid.SemiMinorAxis.ToString(), "\r\n    扁率倒数: ", (1.0 / geographicCoordinateSystem.Datum.Spheroid.Flattening).ToString()
                 };
                return string.Concat(strArray);
            }
            if (!(ispatialReferenceInfo_0 is IProjectedCoordinateSystem))
            {
                return "";
            }
            IProjectedCoordinateSystem system2 = (IProjectedCoordinateSystem) ispatialReferenceInfo_0;
            geographicCoordinateSystem = system2.GeographicCoordinateSystem;
            IProjection projection = system2.Projection;
            IParameter[] parameters = new IParameter[25];
            ((IProjectedCoordinateSystem4GEN) system2).GetParameters(ref parameters);
            string str3 = "  ";
            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i] == null)
                {
                    break;
                }
                str3 = str3 + parameters[i].Name + ": " + parameters[i].Value.ToString() + "\r\n ";
            }
            str2 = (((("别名: " + system2.Alias + "\r\n") + "缩略名: " + system2.Abbreviation + "\r\n") + "说明: " + system2.Remarks + "\r\n") + "投影: " + system2.Projection.Name + "\r\n") + "参数:\r\n" + str3;
            str2 = ((((str2 + "线性单位: " + system2.CoordinateUnit.Name + " (" + system2.CoordinateUnit.MetersPerUnit.ToString() + ")\r\n") + "地理坐标系:\r\n") + "  名称: " + geographicCoordinateSystem.Name + "\r\n") + "  缩略名: " + geographicCoordinateSystem.Abbreviation + "\r\n") + "  说明: " + geographicCoordinateSystem.Remarks + "\r\n";
            str2 = str2 + "  角度单位: " + geographicCoordinateSystem.CoordinateUnit.Name + " (" + geographicCoordinateSystem.CoordinateUnit.RadiansPerUnit.ToString() + ")\r\n";
            double num = 1.0 / geographicCoordinateSystem.Datum.Spheroid.Flattening;
            return ((((((str2 + "  本初子午线: " + geographicCoordinateSystem.PrimeMeridian.Name + " (" + geographicCoordinateSystem.PrimeMeridian.Longitude.ToString() + ")\r\n") + "  数据: " + geographicCoordinateSystem.Datum.Name + "\r\n") + "    椭球体: " + geographicCoordinateSystem.Datum.Spheroid.Name + "\r\n") + "    长半轴: " + geographicCoordinateSystem.Datum.Spheroid.SemiMajorAxis.ToString() + "\r\n") + "    短半轴: " + geographicCoordinateSystem.Datum.Spheroid.SemiMinorAxis.ToString() + "\r\n") + "    扁率倒数: " + num.ToString());
        }

        private void VCSCoordinateInfoPage_Load(object sender, EventArgs e)
        {
            if (!this.bool_0)
            {
                this.btnClear.Enabled = false;
                this.btnImport.Enabled = false;
                this.btnModify.Enabled = false;
                this.btnNew.Enabled = false;
                this.btnSaveAs.Enabled = false;
                this.btnSelect.Enabled = false;
                this.textBoxName.Properties.ReadOnly = false;
            }
            if (this.iverticalCoordinateSystem_0 != null)
            {
                this.textBoxName.Text = this.iverticalCoordinateSystem_0.Name;
                this.textBoxDetail.Text = this.method_0(this.iverticalCoordinateSystem_0);
            }
        }

        public bool IsEdit
        {
            set
            {
                this.bool_0 = value;
            }
        }

        public ISpatialReferenceInfo SpatialReference
        {
            get
            {
                return this.iverticalCoordinateSystem_0;
            }
            set
            {
                this.iverticalCoordinateSystem_0 = value as IVerticalCoordinateSystem;
            }
        }

        internal delegate void SpatialReferenceChangedHandler(object object_0);
    }
}

