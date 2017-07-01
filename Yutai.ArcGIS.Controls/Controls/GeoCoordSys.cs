using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Helpers;

namespace Yutai.ArcGIS.Controls.Controls
{
    internal partial class GeoCoordSys : UserControl
    {
        private int[] DatumeType = new int[] {6214, 6024, 6326, 6610};
        private bool m_CanDo = false;
        private int m_ErrorCount = 0;
        private ISpatialReferenceFactory m_SpatialFactory = new SpatialReferenceEnvironmentClass();
        private int[] PrimeMeridiansType = new int[] {8901, 8912, 8907};
        private int[] SpheresType = new int[] {7024, 7030, 7049};
        private int[] UnitType = new int[] {9102, 9106, 9103, 9104, 9101};

        public GeoCoordSys()
        {
            this.InitializeComponent();
        }

        private void AddAngleUnitToComboBox()
        {
            this.cboAngleName.Properties.Items.Add("自定义");
            this.cboAngleName.Properties.Items.Add("Degree");
            this.cboAngleName.Properties.Items.Add("Gon");
            this.cboAngleName.Properties.Items.Add("Minute");
            this.cboAngleName.Properties.Items.Add("Second");
            this.cboAngleName.Properties.Items.Add("Radian");
        }

        private void AddDatumToComboBox()
        {
            this.cboDatumName.Properties.Items.Add("自定义");
            this.cboDatumName.Properties.Items.Add("D_Beijing_1954");
            this.cboDatumName.Properties.Items.Add("D_Krasovsky_1940");
            this.cboDatumName.Properties.Items.Add("D_WGS_1984");
            this.cboDatumName.Properties.Items.Add("D_Xian_1980");
        }

        private void AddPrimeMeridiansToComboBox()
        {
            this.cboPrimeMeridians.Properties.Items.Add("自定义");
            this.cboPrimeMeridians.Properties.Items.Add("Greenwich");
            this.cboPrimeMeridians.Properties.Items.Add("Athens");
            this.cboPrimeMeridians.Properties.Items.Add("Bern");
        }

        private void AddSpheresToComboBox()
        {
            this.cboSpheres.Properties.Items.Add("自定义");
            this.cboSpheres.Properties.Items.Add("Krasovsky_1940");
            this.cboSpheres.Properties.Items.Add("WGS_1984");
            this.cboSpheres.Properties.Items.Add("Xian_1980");
        }

        private void cboAngleName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboAngleName.SelectedIndex <= 0)
            {
                this.SetEditReadOnly(this.txtValue, false);
            }
            else
            {
                this.SetEditReadOnly(this.txtValue, true);
                if (this.m_CanDo)
                {
                    this.m_Unit = this.m_SpatialFactory.CreateUnit(this.UnitType[this.cboAngleName.SelectedIndex - 1]);
                }
                if (this.m_Unit != null)
                {
                    this.txtValue.Text = ((IAngularUnit) this.m_Unit).RadiansPerUnit.ToString();
                }
            }
        }

        private void cboDatumName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboDatumName.SelectedIndex < 0)
            {
                this.cboSpheres.Enabled = true;
            }
            else
            {
                int index;
                if (this.cboDatumName.SelectedIndex == 0)
                {
                    this.cboSpheres.Enabled = true;
                    index = -1;
                    if ((this.m_Datum != null) && (this.m_Datum.Spheroid != null))
                    {
                        index = this.cboSpheres.Properties.Items.IndexOf(this.m_Datum.Spheroid.Name);
                    }
                    if (index != -1)
                    {
                        this.cboSpheres.SelectedIndex = index;
                    }
                }
                else
                {
                    this.cboSpheres.Enabled = false;
                    if (this.m_CanDo)
                    {
                        this.m_Datum =
                            this.m_SpatialFactory.CreateDatum(this.DatumeType[this.cboDatumName.SelectedIndex - 1]);
                        this.m_pSphere = this.m_Datum.Spheroid;
                        this.m_CanDo = false;
                    }
                    index = this.cboSpheres.Properties.Items.IndexOf(this.m_Datum.Spheroid.Name);
                    if (index != -1)
                    {
                        this.cboSpheres.SelectedIndex = index;
                    }
                    this.m_CanDo = true;
                }
            }
        }

        private void cboPrimeMeridians_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboPrimeMeridians.SelectedIndex <= 0)
            {
                this.SetEditReadOnly(this.txtDegree, false);
                this.SetEditReadOnly(this.txtMinute, false);
                this.SetEditReadOnly(this.txtSecond, false);
            }
            else
            {
                this.SetEditReadOnly(this.txtDegree, true);
                this.SetEditReadOnly(this.txtMinute, true);
                this.SetEditReadOnly(this.txtSecond, true);
                if (this.m_CanDo)
                {
                    this.m_PrimeMeridian =
                        this.m_SpatialFactory.CreatePrimeMeridian(
                            this.PrimeMeridiansType[this.cboPrimeMeridians.SelectedIndex - 1]);
                }
                if (this.m_PrimeMeridian != null)
                {
                    int longitude = (int) this.m_PrimeMeridian.Longitude;
                    double num2 = (this.m_PrimeMeridian.Longitude - longitude)*60.0;
                    int num3 = (int) num2;
                    num2 = (num2 - num3)*60.0;
                    this.txtDegree.Text = longitude.ToString();
                    this.txtMinute.Text = num3.ToString();
                    this.txtSecond.Text = num2.ToString();
                }
            }
        }

        private void cboSpheres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboSpheres.SelectedIndex <= 0)
            {
                this.SetEditReadOnly(this.txtMajorAxis, false);
                this.SetEditReadOnly(this.txtMiniorAxis, false);
                this.SetEditReadOnly(this.txtFlattening, false);
            }
            else
            {
                this.SetEditReadOnly(this.txtMajorAxis, true);
                this.SetEditReadOnly(this.txtMiniorAxis, true);
                this.SetEditReadOnly(this.txtFlattening, true);
                if (this.m_CanDo)
                {
                    this.m_pSphere =
                        this.m_SpatialFactory.CreateSpheroid(this.SpheresType[this.cboSpheres.SelectedIndex - 1]);
                }
                if (this.m_pSphere != null)
                {
                    this.txtMajorAxis.Text = this.m_pSphere.SemiMajorAxis.ToString();
                    this.txtMiniorAxis.Text = this.m_pSphere.SemiMinorAxis.ToString();
                    this.txtFlattening.Text = (1.0/this.m_pSphere.Flattening).ToString();
                }
            }
        }

        private void GeoCoordSys_Load(object sender, EventArgs e)
        {
            this.AddDatumToComboBox();
            this.AddSpheresToComboBox();
            this.AddAngleUnitToComboBox();
            this.AddPrimeMeridiansToComboBox();
            this.rdoMiniorAxis.Checked = true;
            this.txtFlattening.Enabled = false;
            this.InitControl();
            this.m_CanDo = true;
        }

        public IGeographicCoordinateSystem GetSpatialRefrence()
        {
            double num;
            if (this.m_ErrorCount > 0)
            {
                MessageBox.Show("请检查输入数据是否正确！");
                return null;
            }
            if (this.textEditName.Text.Length == 0)
            {
                MessageBox.Show("未指定名字，必须为坐标系统指定一个名字！");
                return null;
            }
            if (this.cboSpheres.SelectedIndex <= 0)
            {
                double num2;
                num = Convert.ToDouble(this.txtMajorAxis.Text);
                if (this.rdoFlattening.Checked)
                {
                    num2 = 1.0/Convert.ToDouble(this.txtFlattening.Text);
                }
                else
                {
                    double num3 = Convert.ToDouble(this.txtMiniorAxis.Text);
                    num2 = (num - num3)/num;
                }
                ((ISpheroidEdit) this.m_pSphere).DefineEx(this.cboSpheres.Text, null, null, null, ref num, ref num2);
            }
            if (this.cboDatumName.SelectedIndex <= 0)
            {
                ((IDatumEdit) this.m_Datum).DefineEx(this.cboDatumName.Text, null, null, null, this.m_pSphere);
            }
            if (this.cboAngleName.SelectedIndex <= 0)
            {
                num = Convert.ToDouble(this.txtValue.Text);
                ((IAngularUnitEdit) this.m_Unit).DefineEx(this.cboAngleName.Text, null, null, null, ref num);
            }
            if (this.cboPrimeMeridians.SelectedIndex <= 0)
            {
                double num4 = Convert.ToDouble(this.txtDegree.Text);
                if (num4 < 0.0)
                {
                    num = (num4 - (Convert.ToDouble(this.txtMinute.Text)/60.0)) -
                          (Convert.ToDouble(this.txtSecond.Text)/3600.0);
                }
                else
                {
                    num = (num4 + (Convert.ToDouble(this.txtMinute.Text)/60.0)) +
                          (Convert.ToDouble(this.txtSecond.Text)/3600.0);
                }
                ((IPrimeMeridianEdit) this.m_PrimeMeridian).DefineEx(this.cboPrimeMeridians.Text, null, null, null,
                    ref num);
            }
            this.m_GeoCoordSys = new GeographicCoordinateSystemClass();
            IGeographicCoordinateSystemEdit geoCoordSys = this.m_GeoCoordSys as IGeographicCoordinateSystemEdit;
            IAngularUnit geographicUnit = this.m_Unit as IAngularUnit;
            string alias = "";
            try
            {
                geoCoordSys.DefineEx(this.textEditName.Text, alias, alias, alias, alias, this.m_Datum,
                    this.m_PrimeMeridian, geographicUnit);
            }
            catch
            {
                MessageBox.Show("无法定义地理坐标!");
                return null;
            }
            return this.m_GeoCoordSys;
        }

        private void InitControl()
        {
            if (this.m_GeoCoordSys != null)
            {
                this.textEditName.Text = this.m_GeoCoordSys.Name;
                this.m_Datum = this.m_GeoCoordSys.Datum;
                int index = this.cboDatumName.Properties.Items.IndexOf(this.m_Datum.Name);
                this.m_pSphere = this.m_Datum.Spheroid;
                if (index != -1)
                {
                    this.cboDatumName.SelectedIndex = index;
                }
                else
                {
                    this.cboDatumName.Text = this.m_Datum.Name;
                    index = this.cboSpheres.Properties.Items.IndexOf(this.m_Datum.Spheroid.Name);
                    if (index != -1)
                    {
                        this.cboSpheres.SelectedIndex = index;
                    }
                    else
                    {
                        this.cboSpheres.Text = this.m_Datum.Spheroid.Name;
                        this.txtMajorAxis.Text = this.m_pSphere.SemiMajorAxis.ToString();
                        this.txtMiniorAxis.Text = this.m_pSphere.SemiMinorAxis.ToString();
                        this.txtFlattening.Text = (1.0/this.m_pSphere.Flattening).ToString();
                    }
                }
                this.m_Unit = this.m_GeoCoordSys.CoordinateUnit;
                index = this.cboAngleName.Properties.Items.IndexOf(this.m_Unit.Name);
                if (index != -1)
                {
                    this.cboAngleName.SelectedIndex = index;
                }
                else
                {
                    this.cboAngleName.Text = this.m_Unit.Name;
                    this.txtValue.Text = ((IAngularUnit) this.m_Unit).RadiansPerUnit.ToString();
                }
                this.m_PrimeMeridian = this.m_GeoCoordSys.PrimeMeridian;
                index = this.cboPrimeMeridians.Properties.Items.IndexOf(this.m_PrimeMeridian.Name);
                if (index != -1)
                {
                    this.cboPrimeMeridians.SelectedIndex = index;
                }
                else
                {
                    this.cboPrimeMeridians.Text = this.m_PrimeMeridian.Name;
                    int longitude = (int) this.m_PrimeMeridian.Longitude;
                    double num3 = (this.m_PrimeMeridian.Longitude - longitude)*60.0;
                    int num4 = (int) num3;
                    num3 = (num3 - num4)*60.0;
                    this.txtDegree.Text = longitude.ToString();
                    this.txtMinute.Text = num4.ToString();
                    this.txtSecond.Text = num3.ToString();
                }
            }
            else
            {
                this.m_GeoCoordSys = new GeographicCoordinateSystemClass();
                this.m_Datum = this.m_GeoCoordSys.Datum;
                this.m_pSphere = this.m_Datum.Spheroid;
                this.m_Unit = this.m_GeoCoordSys.CoordinateUnit;
                this.m_PrimeMeridian = this.m_GeoCoordSys.PrimeMeridian;
                this.cboDatumName.SelectedIndex = 0;
                this.cboSpheres.SelectedIndex = 0;
                this.cboAngleName.SelectedIndex = 0;
                this.cboPrimeMeridians.SelectedIndex = 0;
            }
        }

        private void rdoFlattening_Click(object sender, EventArgs e)
        {
            if (this.rdoFlattening.Checked)
            {
                this.txtMiniorAxis.Enabled = false;
                this.txtFlattening.Enabled = true;
            }
        }

        private void rdoMiniorAxis_Click(object sender, EventArgs e)
        {
            if (this.rdoMiniorAxis.Checked)
            {
                this.txtMiniorAxis.Enabled = true;
                this.txtFlattening.Enabled = false;
            }
        }

        private void SetEditReadOnly(TextEdit textEdit, bool bReadOnly)
        {
            textEdit.Properties.ReadOnly = bReadOnly;
            if (textEdit.Properties.ReadOnly)
            {
                textEdit.BackColor = SystemColors.InactiveBorder;
            }
            else
            {
                textEdit.BackColor = SystemColors.Window;
            }
        }

        private void txtDegree_EditValueChanged(object sender, EventArgs e)
        {
            string str = this.txtDegree.Text.Trim();
            if (str.Length != 0)
            {
                if ((str == "-") || (str == "+"))
                {
                    if (this.txtDegree.ForeColor != Color.Red)
                    {
                        this.m_ErrorCount++;
                    }
                    this.txtDegree.ForeColor = Color.Red;
                }
                else if (CommonHelper.IsNmuber(this.txtDegree.Text))
                {
                    double num = Convert.ToDouble(this.txtDegree.Text);
                    if ((num >= -180.0) && (num <= 180.0))
                    {
                        if (this.txtDegree.ForeColor != SystemColors.WindowText)
                        {
                            this.m_ErrorCount--;
                        }
                        this.txtDegree.ForeColor = SystemColors.WindowText;
                    }
                    else
                    {
                        if (this.txtDegree.ForeColor != Color.Red)
                        {
                            this.m_ErrorCount++;
                        }
                        this.txtDegree.ForeColor = Color.Red;
                    }
                }
                else
                {
                    if (this.txtDegree.ForeColor != Color.Red)
                    {
                        this.m_ErrorCount++;
                    }
                    this.txtDegree.ForeColor = Color.Red;
                }
            }
        }

        private void txtFlattening_EditValueChanged(object sender, EventArgs e)
        {
            string str = this.txtFlattening.Text.Trim();
            if (str.Length != 0)
            {
                if ((str == "-") || (str == "+"))
                {
                    if (this.txtFlattening.ForeColor != Color.Red)
                    {
                        this.m_ErrorCount++;
                    }
                    this.txtFlattening.ForeColor = Color.Red;
                }
                else if (CommonHelper.IsNmuber(this.txtFlattening.Text))
                {
                    double num = 0.0;
                    try
                    {
                        num = Convert.ToDouble(this.txtFlattening.Text);
                    }
                    catch
                    {
                    }
                    if (num > 0.0)
                    {
                        if (this.txtFlattening.ForeColor != SystemColors.WindowText)
                        {
                            this.m_ErrorCount--;
                        }
                        this.txtFlattening.ForeColor = SystemColors.WindowText;
                    }
                    else
                    {
                        if (this.txtFlattening.ForeColor != Color.Red)
                        {
                            this.m_ErrorCount++;
                        }
                        this.txtFlattening.ForeColor = Color.Red;
                    }
                }
                else
                {
                    if (this.txtFlattening.ForeColor != Color.Red)
                    {
                        this.m_ErrorCount++;
                    }
                    this.txtFlattening.ForeColor = Color.Red;
                }
            }
        }

        private void txtMajorAxis_EditValueChanged(object sender, EventArgs e)
        {
            string str = this.txtMajorAxis.Text.Trim();
            if (str.Length != 0)
            {
                if ((str == "-") || (str == "+"))
                {
                    if (this.txtMajorAxis.ForeColor != Color.Red)
                    {
                        this.m_ErrorCount++;
                    }
                    this.txtMajorAxis.ForeColor = Color.Red;
                }
                else if (CommonHelper.IsNmuber(this.txtMajorAxis.Text))
                {
                    double num = 0.0;
                    try
                    {
                        num = Convert.ToDouble(this.txtMajorAxis.Text);
                    }
                    catch
                    {
                    }
                    if (num > 0.0)
                    {
                        if (this.txtMajorAxis.ForeColor != SystemColors.WindowText)
                        {
                            this.m_ErrorCount--;
                        }
                        this.txtMajorAxis.ForeColor = SystemColors.WindowText;
                    }
                    else
                    {
                        if (this.txtMajorAxis.ForeColor != Color.Red)
                        {
                            this.m_ErrorCount++;
                        }
                        this.txtMajorAxis.ForeColor = Color.Red;
                    }
                }
                else
                {
                    if (this.txtMajorAxis.ForeColor != Color.Red)
                    {
                        this.m_ErrorCount++;
                    }
                    this.txtMajorAxis.ForeColor = Color.Red;
                }
            }
        }

        private void txtMiniorAxis_EditValueChanged(object sender, EventArgs e)
        {
            string str = this.txtMiniorAxis.Text.Trim();
            if (str.Length != 0)
            {
                if ((str == "-") || (str == "+"))
                {
                    if (this.txtMiniorAxis.ForeColor != Color.Red)
                    {
                        this.m_ErrorCount++;
                    }
                    this.txtMiniorAxis.ForeColor = Color.Red;
                }
                else if (CommonHelper.IsNmuber(this.txtMiniorAxis.Text))
                {
                    double num = 0.0;
                    try
                    {
                        num = Convert.ToDouble(this.txtMiniorAxis.Text);
                    }
                    catch
                    {
                    }
                    if (num > 0.0)
                    {
                        if (this.txtMiniorAxis.ForeColor != SystemColors.WindowText)
                        {
                            this.m_ErrorCount--;
                        }
                        this.txtMiniorAxis.ForeColor = SystemColors.WindowText;
                    }
                    else
                    {
                        if (this.txtMiniorAxis.ForeColor != Color.Red)
                        {
                            this.m_ErrorCount++;
                        }
                        this.txtMiniorAxis.ForeColor = Color.Red;
                    }
                }
                else
                {
                    if (this.txtMiniorAxis.ForeColor != Color.Red)
                    {
                        this.m_ErrorCount++;
                    }
                    this.txtMiniorAxis.ForeColor = Color.Red;
                }
            }
        }

        private void txtMinute_EditValueChanged(object sender, EventArgs e)
        {
            string str = this.txtMinute.Text.Trim();
            if (str.Length != 0)
            {
                if ((str == "-") || (str == "+"))
                {
                    if (this.txtMinute.ForeColor != Color.Red)
                    {
                        this.m_ErrorCount++;
                    }
                    this.txtMinute.ForeColor = Color.Red;
                }
                else if (CommonHelper.IsNmuber(this.txtMinute.Text))
                {
                    double num = Convert.ToDouble(str);
                    if ((num >= 0.0) && (num <= 60.0))
                    {
                        if (this.txtMinute.ForeColor != SystemColors.WindowText)
                        {
                            this.m_ErrorCount--;
                        }
                        this.txtMinute.ForeColor = SystemColors.WindowText;
                    }
                    else
                    {
                        if (this.txtMinute.ForeColor != Color.Red)
                        {
                            this.m_ErrorCount++;
                        }
                        this.txtMinute.ForeColor = Color.Red;
                    }
                }
                else
                {
                    if (this.txtMinute.ForeColor != Color.Red)
                    {
                        this.m_ErrorCount++;
                    }
                    this.txtMinute.ForeColor = Color.Red;
                }
            }
        }

        private void txtSecond_EditValueChanged(object sender, EventArgs e)
        {
            string str = this.txtSecond.Text.Trim();
            if (str.Length != 0)
            {
                if ((str == "-") || (str == "+"))
                {
                    if (this.txtSecond.ForeColor != Color.Red)
                    {
                        this.m_ErrorCount++;
                    }
                    this.txtSecond.ForeColor = Color.Red;
                }
                else if (CommonHelper.IsNmuber(this.txtSecond.Text))
                {
                    double num = Convert.ToDouble(str);
                    if ((num >= 0.0) && (num <= 60.0))
                    {
                        if (this.txtSecond.ForeColor != SystemColors.WindowText)
                        {
                            this.m_ErrorCount--;
                        }
                        this.txtSecond.ForeColor = SystemColors.WindowText;
                    }
                    else
                    {
                        if (this.txtSecond.ForeColor != Color.Red)
                        {
                            this.m_ErrorCount++;
                        }
                        this.txtSecond.ForeColor = Color.Red;
                    }
                }
                else
                {
                    if (this.txtSecond.ForeColor != Color.Red)
                    {
                        this.m_ErrorCount++;
                    }
                    this.txtSecond.ForeColor = Color.Red;
                }
            }
        }

        private void txtValue_EditValueChanged(object sender, EventArgs e)
        {
            string str = this.txtValue.Text.Trim();
            if (str.Length != 0)
            {
                if ((str == "-") || (str == "+"))
                {
                    if (this.txtValue.ForeColor != Color.Red)
                    {
                        this.m_ErrorCount++;
                    }
                    this.txtValue.ForeColor = Color.Red;
                }
                else if (CommonHelper.IsNmuber(this.txtValue.Text))
                {
                    if (Convert.ToDouble(this.txtValue.Text) > 0.0)
                    {
                        if (this.txtValue.ForeColor != SystemColors.WindowText)
                        {
                            this.m_ErrorCount--;
                        }
                        this.txtValue.ForeColor = SystemColors.WindowText;
                    }
                    else
                    {
                        if (this.txtValue.ForeColor != Color.Red)
                        {
                            this.m_ErrorCount++;
                        }
                        this.txtValue.ForeColor = Color.Red;
                    }
                }
                else
                {
                    if (this.txtValue.ForeColor != Color.Red)
                    {
                        this.m_ErrorCount++;
                    }
                    this.txtValue.ForeColor = Color.Red;
                }
            }
        }

        public IGeographicCoordinateSystem GeographicCoordinateSystem
        {
            set { this.m_GeoCoordSys = value; }
        }
    }
}