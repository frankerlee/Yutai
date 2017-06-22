using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Catalog.UI;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal partial class CoordinateControl : UserControl
    {
        private bool bool_0 = true;
        private bool bool_1 = false;
        private bool bool_2 = false;
        private Container container_0 = null;
        private ISpatialReferenceFactory ispatialReferenceFactory_0 = new SpatialReferenceEnvironmentClass();

        public event SpatialReferenceChangedHandler SpatialReferenceChanged;

        public event ValueChangedHandler ValueChanged;

        public CoordinateControl()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            this.bool_1 = false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.ispatialReference_0 = new UnknownCoordinateSystemClass();
            this.method_1(this.ispatialReference_0);
            this.btnClear.Enabled = false;
            this.btnModify.Enabled = false;
            this.btnSaveAs.Enabled = false;
            this.method_0();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            frmOpenFile file = new frmOpenFile();
            file.AddFilter(new MyGxFilterDatasets(), true);
            file.AllowMultiSelect = false;
            if (file.ShowDialog() == DialogResult.OK)
            {
                IGxDataset dataset = file.Items.get_Element(0) as IGxDataset;
                if (dataset != null)
                {
                    IGeoDataset dataset2 = dataset.Dataset as IGeoDataset;
                    if (dataset2 != null)
                    {
                        this.ispatialReference_0 = dataset2.SpatialReference;
                        IControlPrecision2 precision = this.ispatialReference_0 as IControlPrecision2;
                        if (this.bool_2 != precision.IsHighPrecision)
                        {
                            if (precision.IsHighPrecision)
                            {
                                precision.IsHighPrecision = this.bool_2;
                                (this.ispatialReference_0 as ISpatialReferenceResolution).ConstructFromHorizon();
                            }
                            else
                            {
                                precision.IsHighPrecision = this.bool_2;
                            }
                        }
                        this.method_1(this.ispatialReference_0);
                        if (this.SpatialReferenceChanged != null)
                        {
                            this.SpatialReferenceChanged(this.ispatialReference_0);
                        }
                        this.method_0();
                    }
                }
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            frmSpatialRefrence refrence = new frmSpatialRefrence {
                SpatialRefrence = this.ispatialReference_0
            };
            if (refrence.ShowDialog() == DialogResult.OK)
            {
                this.ispatialReference_0 = refrence.SpatialRefrence;
                this.method_3();
                this.method_1(this.ispatialReference_0);
                this.method_0();
            }
            refrence.Dispose();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            System.Drawing.Point pos = new System.Drawing.Point(this.btnNew.Location.X, this.btnNew.Location.Y + this.btnNew.Height);
            this.contextMenu_0.Show(this, pos);
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog {
                Filter = "空间参考文件 (*.prj)|*.prj",
                OverwritePrompt = true
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = dialog.FileName;
                ESRI.ArcGIS.esriSystem.IPersistStream stream = (ESRI.ArcGIS.esriSystem.IPersistStream) this.ispatialReference_0;
                IXMLStream stream2 = new XMLStreamClass();
                stream.Save((ESRI.ArcGIS.esriSystem.IStream) stream2, 1);
                stream2.SaveToFile(fileName);
                string str2 = stream2.SaveToString();
                int index = str2.IndexOf("[");
                str2 = str2.Substring(index - 6);
                index = str2.LastIndexOf("]");
                str2 = str2.Substring(0, index + 1);
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                using (System.IO.FileStream stream3 = File.Create(fileName))
                {
                    this.method_2(stream3, str2);
                }
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                Filter = "空间参考文件 (*.prj)|*.prj"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = dialog.FileName;
                this.ispatialReference_0 = ((ISpatialReferenceFactory2) this.ispatialReferenceFactory_0).CreateESRISpatialReferenceFromPRJFile(fileName);
                IControlPrecision2 precision = this.ispatialReference_0 as IControlPrecision2;
                if (this.bool_2 != precision.IsHighPrecision)
                {
                    if (precision.IsHighPrecision)
                    {
                        (this.ispatialReference_0 as ISpatialReferenceResolution).ConstructFromHorizon();
                        precision.IsHighPrecision = this.bool_2;
                    }
                    else
                    {
                        precision.IsHighPrecision = this.bool_2;
                    }
                }
                this.method_1(this.ispatialReference_0);
                this.method_0();
            }
            dialog.Dispose();
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            FrmFastSelSpatial spatial = new FrmFastSelSpatial();
            if (spatial.ShowDialog() == DialogResult.OK)
            {
                this.ispatialReference_0 = spatial.SpatialRefrence;
                this.method_1(this.ispatialReference_0);
                if (this.SpatialReferenceChanged != null)
                {
                    this.SpatialReferenceChanged(this.ispatialReference_0);
                }
            }
        }

        private void CoordinateControl_Load(object sender, EventArgs e)
        {
            if (!this.bool_0)
            {
                this.btnSet.Enabled = false;
                this.btnClear.Enabled = false;
                this.btnImport.Enabled = false;
                this.btnModify.Enabled = false;
                this.btnNew.Enabled = false;
                this.btnSelect.Enabled = false;
                this.textBoxName.Properties.ReadOnly = false;
            }
            if (this.ispatialReference_0 == null)
            {
                this.ispatialReference_0 = new UnknownCoordinateSystemClass();
            }
            this.method_1(this.ispatialReference_0);
        }

 private void menuItem_0_Click(object sender, EventArgs e)
        {
            frmSpatialRefrence refrence = new frmSpatialRefrence {
                SpatialRefrenceType = frmSpatialRefrence.enumSpatialRefrenceType.enumGeographicCoord
            };
            if (refrence.ShowDialog() == DialogResult.OK)
            {
                this.ispatialReference_0 = refrence.SpatialRefrence;
                this.method_3();
                this.method_1(this.ispatialReference_0);
                this.method_0();
            }
        }

        private void menuItem_1_Click(object sender, EventArgs e)
        {
            frmSpatialRefrence refrence = new frmSpatialRefrence {
                SpatialRefrenceType = frmSpatialRefrence.enumSpatialRefrenceType.enumProjectCoord
            };
            if (refrence.ShowDialog() == DialogResult.OK)
            {
                this.ispatialReference_0 = refrence.SpatialRefrence;
                this.method_3();
                this.method_1(this.ispatialReference_0);
                this.method_0();
            }
        }

        private void method_0()
        {
            this.bool_1 = true;
            if (this.ValueChanged != null)
            {
                this.ValueChanged(this, new EventArgs());
            }
        }

        private void method_1(ISpatialReference ispatialReference_1)
        {
            IGeographicCoordinateSystem geographicCoordinateSystem;
            string str;
            this.textBoxName.Text = ispatialReference_1.Name;
            if (ispatialReference_1 is IGeographicCoordinateSystem)
            {
                geographicCoordinateSystem = (IGeographicCoordinateSystem) ispatialReference_1;
                str = ("别名: " + geographicCoordinateSystem.Alias + "\r\n") + "缩略名: " + geographicCoordinateSystem.Abbreviation + "\r\n";
                string[] strArray = new string[21];
                strArray[0] = str;
                strArray[1] = "说明: ";
                strArray[2] = geographicCoordinateSystem.Remarks;
                strArray[3] = "\r\n角度单位: ";
                strArray[4] = geographicCoordinateSystem.CoordinateUnit.Name;
                strArray[5] = " (";
                strArray[6] = geographicCoordinateSystem.CoordinateUnit.RadiansPerUnit.ToString();
                strArray[7] = ")\r\n本初子午线: ";
                strArray[8] = geographicCoordinateSystem.PrimeMeridian.Name;
                strArray[9] = " (";
                strArray[10] = geographicCoordinateSystem.PrimeMeridian.Longitude.ToString();
                strArray[11] = ")\r\n数据: ";
                strArray[12] = geographicCoordinateSystem.Datum.Name;
                strArray[13] = "\r\n  椭球体: ";
                strArray[14] = geographicCoordinateSystem.Datum.Spheroid.Name;
                strArray[15] = "\r\n    长半轴: ";
                strArray[16] = geographicCoordinateSystem.Datum.Spheroid.SemiMajorAxis.ToString();
                strArray[17] = "\r\n    短半轴: ";
                strArray[18] = geographicCoordinateSystem.Datum.Spheroid.SemiMinorAxis.ToString();
                strArray[19] = "\r\n    扁率倒数: ";
                double num = 1.0 / geographicCoordinateSystem.Datum.Spheroid.Flattening;
                strArray[20] = num.ToString();
                str = string.Concat(strArray);
                this.textBoxDetail.Text = str;
                if (this.bool_0)
                {
                    this.btnModify.Enabled = true;
                    this.btnClear.Enabled = true;
                    this.btnSaveAs.Enabled = true;
                }
            }
            else if (!(ispatialReference_1 is IProjectedCoordinateSystem))
            {
                if (ispatialReference_1 is IUnknownCoordinateSystem)
                {
                    str = "未知坐标系统";
                    this.textBoxDetail.Text = str;
                    if (this.ispatialReference_0 is IUnknownCoordinateSystem)
                    {
                        this.btnModify.Enabled = false;
                        this.btnClear.Enabled = false;
                        this.btnSaveAs.Enabled = false;
                    }
                    else if (this.bool_0)
                    {
                        this.btnModify.Enabled = true;
                        this.btnClear.Enabled = true;
                        this.btnSaveAs.Enabled = true;
                    }
                }
                else
                {
                    this.btnClear.Enabled = false;
                    this.btnModify.Enabled = false;
                    this.textBoxDetail.Text = "";
                }
            }
            else
            {
                IProjectedCoordinateSystem system2 = (IProjectedCoordinateSystem) ispatialReference_1;
                geographicCoordinateSystem = system2.GeographicCoordinateSystem;
                IProjection projection = system2.Projection;
                IParameter[] parameters = new IParameter[25];
                ((IProjectedCoordinateSystem4GEN) system2).GetParameters(ref parameters);
                string str2 = "  ";
                for (int i = 0; i < parameters.Length; i++)
                {
                    if (parameters[i] == null)
                    {
                        break;
                    }
                    str2 = str2 + parameters[i].Name + ": " + parameters[i].Value.ToString() + "\r\n ";
                }
                str = (((("别名: " + system2.Alias + "\r\n") + "缩略名: " + system2.Abbreviation + "\r\n") + "说明: " + system2.Remarks + "\r\n") + "投影: " + system2.Projection.Name + "\r\n") + "参数:\r\n" + str2;
                str = ((((str + "线性单位: " + system2.CoordinateUnit.Name + " (" + system2.CoordinateUnit.MetersPerUnit.ToString() + ")\r\n") + "地理坐标系:\r\n") + "  名称: " + geographicCoordinateSystem.Name + "\r\n") + "  缩略名: " + geographicCoordinateSystem.Abbreviation + "\r\n") + "  说明: " + geographicCoordinateSystem.Remarks + "\r\n";
                str = str + "  角度单位: " + geographicCoordinateSystem.CoordinateUnit.Name + " (" + geographicCoordinateSystem.CoordinateUnit.RadiansPerUnit.ToString() + ")\r\n";
                str = (((((str + "  本初子午线: " + geographicCoordinateSystem.PrimeMeridian.Name + " (" + geographicCoordinateSystem.PrimeMeridian.Longitude.ToString() + ")\r\n") + "  数据: " + geographicCoordinateSystem.Datum.Name + "\r\n") + "    椭球体: " + geographicCoordinateSystem.Datum.Spheroid.Name + "\r\n") + "    长半轴: " + geographicCoordinateSystem.Datum.Spheroid.SemiMajorAxis.ToString() + "\r\n") + "    短半轴: " + geographicCoordinateSystem.Datum.Spheroid.SemiMinorAxis.ToString() + "\r\n") + "    扁率倒数: " + ((1.0 / geographicCoordinateSystem.Datum.Spheroid.Flattening)).ToString();
                this.textBoxDetail.Text = str;
                if (this.bool_0)
                {
                    this.btnModify.Enabled = true;
                    this.btnClear.Enabled = true;
                    this.btnSaveAs.Enabled = true;
                }
            }
        }

        private void method_2(System.IO.FileStream fileStream_0, string string_0)
        {
            byte[] bytes = new UTF8Encoding(true).GetBytes(string_0);
            fileStream_0.Write(bytes, 0, bytes.Length);
        }

        private void method_3()
        {
        }

        private void textBoxName_EditValueChanged(object sender, EventArgs e)
        {
            this.method_0();
        }

        public bool IsDirty
        {
            get
            {
                return this.bool_1;
            }
        }

        public bool IsEdit
        {
            set
            {
                this.bool_0 = value;
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
                IControlPrecision2 precision = this.ispatialReference_0 as IControlPrecision2;
                this.bool_2 = precision.IsHighPrecision;
            }
        }

        public delegate void SpatialReferenceChangedHandler(object object_0);
    }
}

