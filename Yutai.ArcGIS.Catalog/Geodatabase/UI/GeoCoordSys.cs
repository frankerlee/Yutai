using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Helpers;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal partial class GeoCoordSys : UserControl
    {
        private bool bool_0 = false;
        private Container container_0 = null;
        private int int_0 = 0;
        private int[] int_1 = new int[] {6214, 6024, 6326, 6610};
        private int[] int_2 = new int[] {7024, 7030, 7049};
        private int[] int_3 = new int[] {9102, 9106, 9103, 9104, 9101};
        private int[] int_4 = new int[] {8901, 8912, 8907};
        private ISpatialReferenceFactory ispatialReferenceFactory_0 = new SpatialReferenceEnvironmentClass();

        public GeoCoordSys()
        {
            this.InitializeComponent();
        }

        private void cboAngleName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboAngleName.SelectedIndex <= 0)
            {
                this.method_5(this.txtValue, false);
            }
            else
            {
                this.method_5(this.txtValue, true);
                if (this.bool_0)
                {
                    this.iunit_0 =
                        this.ispatialReferenceFactory_0.CreateUnit(this.int_3[this.cboAngleName.SelectedIndex - 1]);
                }
                if (this.iunit_0 != null)
                {
                    this.txtValue.Text = ((IAngularUnit) this.iunit_0).RadiansPerUnit.ToString();
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
                    if ((this.idatum_0 != null) && (this.idatum_0.Spheroid != null))
                    {
                        index = this.cboSpheres.Properties.Items.IndexOf(this.idatum_0.Spheroid.Name);
                    }
                    if (index != -1)
                    {
                        this.cboSpheres.SelectedIndex = index;
                    }
                }
                else
                {
                    this.cboSpheres.Enabled = false;
                    if (this.bool_0)
                    {
                        this.idatum_0 =
                            this.ispatialReferenceFactory_0.CreateDatum(this.int_1[this.cboDatumName.SelectedIndex - 1]);
                        this.ispheroid_0 = this.idatum_0.Spheroid;
                        this.bool_0 = false;
                    }
                    index = this.cboSpheres.Properties.Items.IndexOf(this.idatum_0.Spheroid.Name);
                    if (index != -1)
                    {
                        this.cboSpheres.SelectedIndex = index;
                    }
                    this.bool_0 = true;
                }
            }
        }

        private void cboPrimeMeridians_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboPrimeMeridians.SelectedIndex <= 0)
            {
                this.method_5(this.txtDegree, false);
                this.method_5(this.txtMinute, false);
                this.method_5(this.txtSecond, false);
            }
            else
            {
                this.method_5(this.txtDegree, true);
                this.method_5(this.txtMinute, true);
                this.method_5(this.txtSecond, true);
                if (this.bool_0)
                {
                    this.iprimeMeridian_0 =
                        this.ispatialReferenceFactory_0.CreatePrimeMeridian(
                            this.int_4[this.cboPrimeMeridians.SelectedIndex - 1]);
                }
                if (this.iprimeMeridian_0 != null)
                {
                    int longitude = (int) this.iprimeMeridian_0.Longitude;
                    double num2 = (this.iprimeMeridian_0.Longitude - longitude)*60.0;
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
                this.method_5(this.txtMajorAxis, false);
                this.method_5(this.txtMiniorAxis, false);
                this.method_5(this.txtFlattening, false);
            }
            else
            {
                this.method_5(this.txtMajorAxis, true);
                this.method_5(this.txtMiniorAxis, true);
                this.method_5(this.txtFlattening, true);
                if (this.bool_0)
                {
                    this.ispheroid_0 =
                        this.ispatialReferenceFactory_0.CreateSpheroid(this.int_2[this.cboSpheres.SelectedIndex - 1]);
                }
                if (this.ispheroid_0 != null)
                {
                    this.txtMajorAxis.Text = this.ispheroid_0.SemiMajorAxis.ToString();
                    this.txtMiniorAxis.Text = this.ispheroid_0.SemiMinorAxis.ToString();
                    this.txtFlattening.Text = (1.0/this.ispheroid_0.Flattening).ToString();
                }
            }
        }

        private void GeoCoordSys_Load(object sender, EventArgs e)
        {
            this.method_0();
            this.method_1();
            this.method_2();
            this.method_3();
            this.rdoMiniorAxis.Checked = true;
            this.txtFlattening.Enabled = false;
            this.method_4();
            this.bool_0 = true;
        }

        public IGeographicCoordinateSystem GetSpatialRefrence()
        {
            double num;
            if (this.int_0 > 0)
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
                ((ISpheroidEdit) this.ispheroid_0).DefineEx(this.cboSpheres.Text, null, null, null, ref num, ref num2);
            }
            if (this.cboDatumName.SelectedIndex <= 0)
            {
                ((IDatumEdit) this.idatum_0).DefineEx(this.cboDatumName.Text, null, null, null, this.ispheroid_0);
            }
            if (this.cboAngleName.SelectedIndex <= 0)
            {
                num = Convert.ToDouble(this.txtValue.Text);
                ((IAngularUnitEdit) this.iunit_0).DefineEx(this.cboAngleName.Text, null, null, null, ref num);
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
                ((IPrimeMeridianEdit) this.iprimeMeridian_0).DefineEx(this.cboPrimeMeridians.Text, null, null, null,
                    ref num);
            }
            this.igeographicCoordinateSystem_0 = new GeographicCoordinateSystemClass();
            IGeographicCoordinateSystemEdit edit = this.igeographicCoordinateSystem_0 as IGeographicCoordinateSystemEdit;
            IAngularUnit geographicUnit = this.iunit_0 as IAngularUnit;
            string alias = "";
            try
            {
                edit.DefineEx(this.textEditName.Text, alias, alias, alias, alias, this.idatum_0, this.iprimeMeridian_0,
                    geographicUnit);
            }
            catch
            {
                MessageBox.Show("无法定义地理坐标!");
                return null;
            }
            return this.igeographicCoordinateSystem_0;
        }

        private void method_0()
        {
            this.cboDatumName.Properties.Items.Add("自定义");
            this.cboDatumName.Properties.Items.Add("D_Beijing_1954");
            this.cboDatumName.Properties.Items.Add("D_Krasovsky_1940");
            this.cboDatumName.Properties.Items.Add("D_WGS_1984");
            this.cboDatumName.Properties.Items.Add("D_Xian_1980");
        }

        private void method_1()
        {
            this.cboSpheres.Properties.Items.Add("自定义");
            this.cboSpheres.Properties.Items.Add("Krasovsky_1940");
            this.cboSpheres.Properties.Items.Add("WGS_1984");
            this.cboSpheres.Properties.Items.Add("Xian_1980");
        }

        private void method_2()
        {
            this.cboAngleName.Properties.Items.Add("自定义");
            this.cboAngleName.Properties.Items.Add("Degree");
            this.cboAngleName.Properties.Items.Add("Gon");
            this.cboAngleName.Properties.Items.Add("Minute");
            this.cboAngleName.Properties.Items.Add("Second");
            this.cboAngleName.Properties.Items.Add("Radian");
        }

        private void method_3()
        {
            this.cboPrimeMeridians.Properties.Items.Add("自定义");
            this.cboPrimeMeridians.Properties.Items.Add("Greenwich");
            this.cboPrimeMeridians.Properties.Items.Add("Athens");
            this.cboPrimeMeridians.Properties.Items.Add("Bern");
        }

        private void method_4()
        {
            if (this.igeographicCoordinateSystem_0 != null)
            {
                this.textEditName.Text = this.igeographicCoordinateSystem_0.Name;
                this.idatum_0 = this.igeographicCoordinateSystem_0.Datum;
                int index = this.cboDatumName.Properties.Items.IndexOf(this.idatum_0.Name);
                this.ispheroid_0 = this.idatum_0.Spheroid;
                if (index != -1)
                {
                    this.cboDatumName.SelectedIndex = index;
                }
                else
                {
                    this.cboDatumName.Text = this.idatum_0.Name;
                    index = this.cboSpheres.Properties.Items.IndexOf(this.idatum_0.Spheroid.Name);
                    if (index != -1)
                    {
                        this.cboSpheres.SelectedIndex = index;
                    }
                    else
                    {
                        this.cboSpheres.Text = this.idatum_0.Spheroid.Name;
                        this.txtMajorAxis.Text = this.ispheroid_0.SemiMajorAxis.ToString();
                        this.txtMiniorAxis.Text = this.ispheroid_0.SemiMinorAxis.ToString();
                        this.txtFlattening.Text = (1.0/this.ispheroid_0.Flattening).ToString();
                    }
                }
                this.iunit_0 = this.igeographicCoordinateSystem_0.CoordinateUnit;
                index = this.cboAngleName.Properties.Items.IndexOf(this.iunit_0.Name);
                if (index != -1)
                {
                    this.cboAngleName.SelectedIndex = index;
                }
                else
                {
                    this.cboAngleName.Text = this.iunit_0.Name;
                    this.txtValue.Text = ((IAngularUnit) this.iunit_0).RadiansPerUnit.ToString();
                }
                this.iprimeMeridian_0 = this.igeographicCoordinateSystem_0.PrimeMeridian;
                index = this.cboPrimeMeridians.Properties.Items.IndexOf(this.iprimeMeridian_0.Name);
                if (index != -1)
                {
                    this.cboPrimeMeridians.SelectedIndex = index;
                }
                else
                {
                    this.cboPrimeMeridians.Text = this.iprimeMeridian_0.Name;
                    int longitude = (int) this.iprimeMeridian_0.Longitude;
                    double num4 = (this.iprimeMeridian_0.Longitude - longitude)*60.0;
                    int num5 = (int) num4;
                    num4 = (num4 - num5)*60.0;
                    this.txtDegree.Text = longitude.ToString();
                    this.txtMinute.Text = num5.ToString();
                    this.txtSecond.Text = num4.ToString();
                }
            }
            else
            {
                this.igeographicCoordinateSystem_0 = new GeographicCoordinateSystemClass();
                this.idatum_0 = this.igeographicCoordinateSystem_0.Datum;
                this.ispheroid_0 = this.idatum_0.Spheroid;
                this.iunit_0 = this.igeographicCoordinateSystem_0.CoordinateUnit;
                this.iprimeMeridian_0 = this.igeographicCoordinateSystem_0.PrimeMeridian;
                this.cboDatumName.SelectedIndex = 0;
                this.cboSpheres.SelectedIndex = 0;
                this.cboAngleName.SelectedIndex = 0;
                this.cboPrimeMeridians.SelectedIndex = 0;
            }
        }

        private void method_5(TextEdit textEdit_0, bool bool_1)
        {
            textEdit_0.Properties.ReadOnly = bool_1;
            if (textEdit_0.Properties.ReadOnly)
            {
                textEdit_0.BackColor = SystemColors.InactiveBorder;
            }
            else
            {
                textEdit_0.BackColor = SystemColors.Window;
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

        private void txtDegree_EditValueChanged(object sender, EventArgs e)
        {
            string str = this.txtDegree.Text.Trim();
            if (str.Length != 0)
            {
                if ((str == "-") || (str == "+"))
                {
                    if (this.txtDegree.ForeColor != Color.Red)
                    {
                        this.int_0++;
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
                            this.int_0--;
                        }
                        this.txtDegree.ForeColor = SystemColors.WindowText;
                    }
                    else
                    {
                        if (this.txtDegree.ForeColor != Color.Red)
                        {
                            this.int_0++;
                        }
                        this.txtDegree.ForeColor = Color.Red;
                    }
                }
                else
                {
                    if (this.txtDegree.ForeColor != Color.Red)
                    {
                        this.int_0++;
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
                        this.int_0++;
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
                            this.int_0--;
                        }
                        this.txtFlattening.ForeColor = SystemColors.WindowText;
                    }
                    else
                    {
                        if (this.txtFlattening.ForeColor != Color.Red)
                        {
                            this.int_0++;
                        }
                        this.txtFlattening.ForeColor = Color.Red;
                    }
                }
                else
                {
                    if (this.txtFlattening.ForeColor != Color.Red)
                    {
                        this.int_0++;
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
                        this.int_0++;
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
                            this.int_0--;
                        }
                        this.txtMajorAxis.ForeColor = SystemColors.WindowText;
                    }
                    else
                    {
                        if (this.txtMajorAxis.ForeColor != Color.Red)
                        {
                            this.int_0++;
                        }
                        this.txtMajorAxis.ForeColor = Color.Red;
                    }
                }
                else
                {
                    if (this.txtMajorAxis.ForeColor != Color.Red)
                    {
                        this.int_0++;
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
                        this.int_0++;
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
                            this.int_0--;
                        }
                        this.txtMiniorAxis.ForeColor = SystemColors.WindowText;
                    }
                    else
                    {
                        if (this.txtMiniorAxis.ForeColor != Color.Red)
                        {
                            this.int_0++;
                        }
                        this.txtMiniorAxis.ForeColor = Color.Red;
                    }
                }
                else
                {
                    if (this.txtMiniorAxis.ForeColor != Color.Red)
                    {
                        this.int_0++;
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
                        this.int_0++;
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
                            this.int_0--;
                        }
                        this.txtMinute.ForeColor = SystemColors.WindowText;
                    }
                    else
                    {
                        if (this.txtMinute.ForeColor != Color.Red)
                        {
                            this.int_0++;
                        }
                        this.txtMinute.ForeColor = Color.Red;
                    }
                }
                else
                {
                    if (this.txtMinute.ForeColor != Color.Red)
                    {
                        this.int_0++;
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
                        this.int_0++;
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
                            this.int_0--;
                        }
                        this.txtSecond.ForeColor = SystemColors.WindowText;
                    }
                    else
                    {
                        if (this.txtSecond.ForeColor != Color.Red)
                        {
                            this.int_0++;
                        }
                        this.txtSecond.ForeColor = Color.Red;
                    }
                }
                else
                {
                    if (this.txtSecond.ForeColor != Color.Red)
                    {
                        this.int_0++;
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
                        this.int_0++;
                    }
                    this.txtValue.ForeColor = Color.Red;
                }
                else if (CommonHelper.IsNmuber(this.txtValue.Text))
                {
                    if (Convert.ToDouble(this.txtValue.Text) > 0.0)
                    {
                        if (this.txtValue.ForeColor != SystemColors.WindowText)
                        {
                            this.int_0--;
                        }
                        this.txtValue.ForeColor = SystemColors.WindowText;
                    }
                    else
                    {
                        if (this.txtValue.ForeColor != Color.Red)
                        {
                            this.int_0++;
                        }
                        this.txtValue.ForeColor = Color.Red;
                    }
                }
                else
                {
                    if (this.txtValue.ForeColor != Color.Red)
                    {
                        this.int_0++;
                    }
                    this.txtValue.ForeColor = Color.Red;
                }
            }
        }

        public IGeographicCoordinateSystem GeographicCoordinateSystem
        {
            set { this.igeographicCoordinateSystem_0 = value; }
        }
    }
}