using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.ControlExtend;
using Yutai.ArcGIS.Common.Helpers;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal partial class ProjCoordSys : UserControl
    {
        private bool bool_0 = false;
        private Container container_0 = null;
        private int int_0 = 0;
        private int[] int_1 = new int[] {43005, 43020, 43033, 43004, 43006};
        private int[] int_2 = new int[] {9001, 9036};
        private IParameter[] iparameter_0 = new IParameter[25];
        private ISpatialReferenceFactory ispatialReferenceFactory_0 = new SpatialReferenceEnvironmentClass();

        public ProjCoordSys()
        {
            this.InitializeComponent();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            frmSpatialRefrence refrence = new frmSpatialRefrence
            {
                Text = "地理坐标系属性",
                SpatialRefrence = this.igeographicCoordinateSystem_0
            };
            if (refrence.ShowDialog() == DialogResult.OK)
            {
                this.igeographicCoordinateSystem_0 = (IGeographicCoordinateSystem) refrence.SpatialRefrence;
                this.method_3(this.igeographicCoordinateSystem_0);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmSpatialRefrence refrence = new frmSpatialRefrence
            {
                Text = "新建地理坐标系",
                SpatialRefrenceType = frmSpatialRefrence.enumSpatialRefrenceType.enumGeographicCoord
            };
            if (refrence.ShowDialog() == DialogResult.OK)
            {
                this.igeographicCoordinateSystem_0 = (IGeographicCoordinateSystem) refrence.SpatialRefrence;
                this.method_3(this.igeographicCoordinateSystem_0);
                this.btnModify.Enabled = true;
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "空间参考文件 (*.prj)|*.prj",
                RestoreDirectory = true
            };
            while (dialog.ShowDialog() == DialogResult.OK)
            {
                this.igeographicCoordinateSystem_0 =
                    ((ISpatialReferenceFactory2) this.ispatialReferenceFactory_0).CreateESRISpatialReferenceFromPRJFile(
                        dialog.FileName) as IGeographicCoordinateSystem;
                if (this.igeographicCoordinateSystem_0 != null)
                {
                    this.method_3(this.igeographicCoordinateSystem_0);
                    this.btnModify.Enabled = true;
                    dialog = null;
                    return;
                }
                MessageBox.Show("请选择包含地理坐标系的空间参考文件!");
            }
        }

        private void cboLineUnitName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboLineUnitName.SelectedIndex <= 0)
            {
                this.method_6(this.txtValue, false);
            }
            else
            {
                this.method_6(this.txtValue, true);
                if (this.bool_0)
                {
                    this.iunit_0 =
                        this.ispatialReferenceFactory_0.CreateUnit(this.int_2[this.cboLineUnitName.SelectedIndex - 1]);
                }
                this.txtValue.Text = (this.iunit_0 as ILinearUnit).MetersPerUnit.ToString();
            }
        }

        private void cboProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.iprojection_0 =
                    this.ispatialReferenceFactory_0.CreateProjection(this.int_1[this.cboProjectName.SelectedIndex]);
                IParameter[] defaultParameters = ((IProjectionGEN) this.iprojection_0).GetDefaultParameters();
                int index = 0;
                while (index < defaultParameters.Length)
                {
                    this.iparameter_0[index] = defaultParameters[index];
                    index++;
                }
                if (this.iparameter_0.Length > defaultParameters.Length)
                {
                    this.iparameter_0[index] = null;
                }
                this.method_4(this.iparameter_0);
            }
        }

        public IProjectedCoordinateSystem GetSpatialRefrence()
        {
            int num3;
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
            if (this.igeographicCoordinateSystem_0 == null)
            {
                MessageBox.Show("未指定地理坐标，必须创建或指定地理坐标系！");
                return null;
            }
            object alias = Missing.Value;
            if (this.cboLineUnitName.SelectedIndex <= 0)
            {
                double metersPerUnit = Convert.ToDouble(this.txtValue.Text);
                ((ILinearUnitEdit) this.iunit_0).DefineEx(this.cboLineUnitName.Text, null, null, null, ref metersPerUnit);
            }
            object text = this.textEditName.Text;
            object gcs = this.igeographicCoordinateSystem_0;
            object projectedUnit = this.iunit_0;
            object projection = this.iprojection_0;
            int num2 = 0;
            for (num3 = 0; num3 < this.iparameter_0.Length; num3++)
            {
                num2++;
                if (this.iparameter_0[num3] == null)
                {
                    break;
                }
            }
            IParameter[] parameterArray = new IParameter[num2];
            for (num3 = 0; num3 < num2; num3++)
            {
                parameterArray[num3] = this.iparameter_0[num3];
            }
            object parameters = parameterArray;
            try
            {
                ((IProjectedCoordinateSystemEdit) this.iprojectedCoordinateSystem_0).Define(ref text, ref alias,
                    ref alias, ref alias, ref alias, ref gcs, ref projectedUnit, ref projection, ref parameters);
            }
            catch
            {
                MessageBox.Show("无法定义投影坐标!");
                return null;
            }
            return this.iprojectedCoordinateSystem_0;
        }

        private void method_0()
        {
            this.cboProjectName.Properties.Items.Add("Gauss_Kruger");
            this.cboProjectName.Properties.Items.Add("Lambert_Conformal_Conic");
            this.cboProjectName.Properties.Items.Add("Lambert_Azimuthal_Equal_Area");
            this.cboProjectName.Properties.Items.Add("Mercator");
            this.cboProjectName.Properties.Items.Add("Transverse_Mercator");
        }

        private void method_1()
        {
            this.cboLineUnitName.Properties.Items.Add("自定义");
            this.cboLineUnitName.Properties.Items.Add("Meter");
            this.cboLineUnitName.Properties.Items.Add("Kilometer");
        }

        private void method_2()
        {
            if (this.iprojectedCoordinateSystem_0 == null)
            {
                this.bool_0 = true;
                this.cboProjectName.SelectedIndex = 0;
                this.cboLineUnitName.SelectedIndex = 1;
                this.btnModify.Enabled = false;
                this.iprojectedCoordinateSystem_0 = new ProjectedCoordinateSystemClass();
            }
            else
            {
                this.textEditName.Text = this.iprojectedCoordinateSystem_0.Name;
                this.igeographicCoordinateSystem_0 = this.iprojectedCoordinateSystem_0.GeographicCoordinateSystem;
                this.iprojection_0 = this.iprojectedCoordinateSystem_0.Projection;
                this.iunit_0 = this.iprojectedCoordinateSystem_0.CoordinateUnit;
                ((IProjectedCoordinateSystem4GEN) this.iprojectedCoordinateSystem_0).GetParameters(ref this.iparameter_0);
                this.method_3(this.igeographicCoordinateSystem_0);
                int index = this.cboProjectName.Properties.Items.IndexOf(this.iprojection_0.Name);
                if (index != -1)
                {
                    this.cboProjectName.SelectedIndex = index;
                }
                else
                {
                    this.cboProjectName.Text = this.iprojection_0.Name;
                }
                index = this.cboLineUnitName.Properties.Items.IndexOf(this.iunit_0.Name);
                if (index != -1)
                {
                    this.cboLineUnitName.SelectedIndex = index;
                }
                else
                {
                    this.cboLineUnitName.Text = this.iunit_0.Name;
                    this.txtValue.Text = (this.iunit_0 as ILinearUnit).MetersPerUnit.ToString();
                }
                this.method_4(this.iparameter_0);
                this.btnModify.Enabled = true;
            }
            this.bool_0 = true;
        }

        private void method_3(IGeographicCoordinateSystem igeographicCoordinateSystem_1)
        {
            string[] strArray = new string[]
            {
                "名称: ", igeographicCoordinateSystem_1.Name, "\r\n缩略名: ", igeographicCoordinateSystem_1.Abbreviation,
                "\r\n说明: ", igeographicCoordinateSystem_1.Remarks, "\r\n角度单位: ",
                igeographicCoordinateSystem_1.CoordinateUnit.Name, " (",
                igeographicCoordinateSystem_1.CoordinateUnit.RadiansPerUnit.ToString(), ")\r\n本初子午线: ",
                igeographicCoordinateSystem_1.PrimeMeridian.Name, " (",
                igeographicCoordinateSystem_1.PrimeMeridian.Longitude.ToString(), ")\r\n数据: ",
                igeographicCoordinateSystem_1.Datum.Name,
                "\r\n  椭球体: ", igeographicCoordinateSystem_1.Datum.Spheroid.Name, "\r\n    长半轴: ",
                igeographicCoordinateSystem_1.Datum.Spheroid.SemiMajorAxis.ToString(), "\r\n    短半轴: ",
                igeographicCoordinateSystem_1.Datum.Spheroid.SemiMinorAxis.ToString(), "\r\n    扁率倒数: ",
                (1.0/igeographicCoordinateSystem_1.Datum.Spheroid.Flattening).ToString()
            };
            string str = string.Concat(strArray);
            this.textBoxGeoCoodSys.Text = str;
        }

        private void method_4(IParameter[] iparameter_1)
        {
            this.paramlistView.Items.Clear();
            string[] items = new string[2];
            for (int i = 0; i < iparameter_1.Length; i++)
            {
                if (iparameter_1[i] == null)
                {
                    break;
                }
                items[0] = iparameter_1[i].Name;
                items[1] = iparameter_1[i].Value.ToString();
                this.paramlistView.Items.Add(new ListViewItem(items));
            }
        }

        private void method_5(object sender, ValueChangedEventArgs e)
        {
            try
            {
                double num = Convert.ToDouble(e.NewValue);
                ((IParameterEdit) this.iparameter_0[e.Row]).Value = num;
            }
            catch
            {
                MessageBox.Show("数据格式错!", "投影");
            }
        }

        private void method_6(TextEdit textEdit_0, bool bool_1)
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

        private void ProjCoordSys_Load(object sender, EventArgs e)
        {
            this.method_0();
            this.method_1();
            this.method_2();
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
            get { return this.igeographicCoordinateSystem_0; }
        }

        public IParameter[] Paramters
        {
            get
            {
                int index = 0;
                while (index < this.iparameter_0.Length)
                {
                    if (this.iparameter_0[index] == null)
                    {
                        break;
                    }
                    index++;
                }
                IParameter[] parameterArray = new IParameter[index];
                for (int i = 0; i < index; i++)
                {
                    parameterArray[i] = this.iparameter_0[i];
                }
                return parameterArray;
            }
        }

        public IProjectedCoordinateSystem ProjectedCoordinateSystem
        {
            set { this.iprojectedCoordinateSystem_0 = value; }
        }

        public IProjection Projection
        {
            get { return this.iprojection_0; }
        }
    }
}