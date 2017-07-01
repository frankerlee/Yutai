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

namespace Yutai.ArcGIS.Controls.Controls
{
    public partial class ProjCoordSys : UserControl
    {
        private bool m_CanDo = false;
        private int m_ErrorCount = 0;
        private IParameter[] m_pParamters = new IParameter[25];
        private ISpatialReferenceFactory m_SpatialFactory = new SpatialReferenceEnvironmentClass();
        private int[] ProjectType = new int[] {43005, 43020, 43033, 43004, 43006};
        private int[] UnitType = new int[] {9001, 9036};

        public ProjCoordSys()
        {
            this.InitializeComponent();
        }

        private void AddLineUnitToComboBox()
        {
            this.cboLineUnitName.Properties.Items.Add("自定义");
            this.cboLineUnitName.Properties.Items.Add("Meter");
            this.cboLineUnitName.Properties.Items.Add("Kilometer");
        }

        private void AddProjectType()
        {
            this.cboProjectName.Properties.Items.Add("Gauss_Kruger");
            this.cboProjectName.Properties.Items.Add("Lambert_Conformal_Conic");
            this.cboProjectName.Properties.Items.Add("Lambert_Azimuthal_Equal_Area");
            this.cboProjectName.Properties.Items.Add("Mercator");
            this.cboProjectName.Properties.Items.Add("Transverse_Mercator");
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            frmSpatialRefrence refrence = new frmSpatialRefrence
            {
                Text = "地理坐标系属性",
                SpatialRefrence = this.m_GeoCoordSys
            };
            if (refrence.ShowDialog() == DialogResult.OK)
            {
                this.m_GeoCoordSys = (IGeographicCoordinateSystem) refrence.SpatialRefrence;
                this.WriteGeoCoordSysInfo(this.m_GeoCoordSys);
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
                this.m_GeoCoordSys = (IGeographicCoordinateSystem) refrence.SpatialRefrence;
                this.WriteGeoCoordSysInfo(this.m_GeoCoordSys);
                this.btnModify.Enabled = true;
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            bool flag;
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "空间参考文件 (*.prj)|*.prj",
                RestoreDirectory = true
            };
            Label_0095:
            flag = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.m_GeoCoordSys =
                    ((ISpatialReferenceFactory2) this.m_SpatialFactory).CreateESRISpatialReferenceFromPRJFile(
                        dialog.FileName) as IGeographicCoordinateSystem;
                if (this.m_GeoCoordSys == null)
                {
                    MessageBox.Show("请选择包含地理坐标系的空间参考文件!");
                    goto Label_0095;
                }
                this.WriteGeoCoordSysInfo(this.m_GeoCoordSys);
                this.btnModify.Enabled = true;
                dialog = null;
            }
        }

        private void cboLineUnitName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboLineUnitName.SelectedIndex <= 0)
            {
                this.SetEditReadOnly(this.txtValue, false);
            }
            else
            {
                this.SetEditReadOnly(this.txtValue, true);
                if (this.m_CanDo)
                {
                    this.m_Unit = this.m_SpatialFactory.CreateUnit(this.UnitType[this.cboLineUnitName.SelectedIndex - 1]);
                }
                this.txtValue.Text = (this.m_Unit as ILinearUnit).MetersPerUnit.ToString();
            }
        }

        private void cboProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_Projection =
                    this.m_SpatialFactory.CreateProjection(this.ProjectType[this.cboProjectName.SelectedIndex]);
                IParameter[] defaultParameters = ((IProjectionGEN) this.m_Projection).GetDefaultParameters();
                int index = 0;
                while (index < defaultParameters.Length)
                {
                    this.m_pParamters[index] = defaultParameters[index];
                    index++;
                }
                if (this.m_pParamters.Length > defaultParameters.Length)
                {
                    this.m_pParamters[index] = null;
                }
                this.WriteToParamListView(this.m_pParamters);
            }
        }

        public IProjectedCoordinateSystem GetSpatialRefrence()
        {
            int num3;
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
            if (this.m_GeoCoordSys == null)
            {
                MessageBox.Show("未指定地理坐标，必须创建或指定地理坐标系！");
                return null;
            }
            object alias = Missing.Value;
            if (this.cboLineUnitName.SelectedIndex <= 0)
            {
                double metersPerUnit = Convert.ToDouble(this.txtValue.Text);
                ((ILinearUnitEdit) this.m_Unit).DefineEx(this.cboLineUnitName.Text, null, null, null, ref metersPerUnit);
            }
            object text = this.textEditName.Text;
            object geoCoordSys = this.m_GeoCoordSys;
            object unit = this.m_Unit;
            object projection = this.m_Projection;
            int num2 = 0;
            for (num3 = 0; num3 < this.m_pParamters.Length; num3++)
            {
                num2++;
                if (this.m_pParamters[num3] == null)
                {
                    break;
                }
            }
            IParameter[] parameterArray = new IParameter[num2];
            for (num3 = 0; num3 < num2; num3++)
            {
                parameterArray[num3] = this.m_pParamters[num3];
            }
            object parameters = parameterArray;
            try
            {
                ((IProjectedCoordinateSystemEdit) this.m_ProjectedCoordSys).Define(ref text, ref alias, ref alias,
                    ref alias, ref alias, ref geoCoordSys, ref unit, ref projection, ref parameters);
            }
            catch
            {
                MessageBox.Show("无法定义投影坐标!");
                return null;
            }
            return this.m_ProjectedCoordSys;
        }

        private void InitControl()
        {
            if (this.m_ProjectedCoordSys == null)
            {
                this.m_CanDo = true;
                this.cboProjectName.SelectedIndex = 0;
                this.cboLineUnitName.SelectedIndex = 1;
                this.btnModify.Enabled = false;
                this.m_ProjectedCoordSys = new ProjectedCoordinateSystemClass();
            }
            else
            {
                this.textEditName.Text = this.m_ProjectedCoordSys.Name;
                this.m_GeoCoordSys = this.m_ProjectedCoordSys.GeographicCoordinateSystem;
                this.m_Projection = this.m_ProjectedCoordSys.Projection;
                this.m_Unit = this.m_ProjectedCoordSys.CoordinateUnit;
                ((IProjectedCoordinateSystem4GEN) this.m_ProjectedCoordSys).GetParameters(ref this.m_pParamters);
                this.WriteGeoCoordSysInfo(this.m_GeoCoordSys);
                int index = this.cboProjectName.Properties.Items.IndexOf(this.m_Projection.Name);
                if (index != -1)
                {
                    this.cboProjectName.SelectedIndex = index;
                }
                else
                {
                    this.cboProjectName.Text = this.m_Projection.Name;
                }
                index = this.cboLineUnitName.Properties.Items.IndexOf(this.m_Unit.Name);
                if (index != -1)
                {
                    this.cboLineUnitName.SelectedIndex = index;
                }
                else
                {
                    this.cboLineUnitName.Text = this.m_Unit.Name;
                    this.txtValue.Text = (this.m_Unit as ILinearUnit).MetersPerUnit.ToString();
                }
                this.WriteToParamListView(this.m_pParamters);
                this.btnModify.Enabled = true;
            }
            this.m_CanDo = true;
        }

        private void paramlistView_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            try
            {
                double num = Convert.ToDouble(e.NewValue);
                ((IParameterEdit) this.m_pParamters[e.Row]).Value = num;
            }
            catch
            {
                MessageBox.Show("数据格式错!", "投影");
            }
        }

        private void ProjCoordSys_Load(object sender, EventArgs e)
        {
            this.AddProjectType();
            this.AddLineUnitToComboBox();
            this.InitControl();
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

        private void WriteGeoCoordSysInfo(IGeographicCoordinateSystem GeoCoordSys)
        {
            string[] strArray = new string[]
            {
                "名称: ", GeoCoordSys.Name, "\r\n缩略名: ", GeoCoordSys.Abbreviation, "\r\n说明: ", GeoCoordSys.Remarks,
                "\r\n角度单位: ", GeoCoordSys.CoordinateUnit.Name, " (",
                GeoCoordSys.CoordinateUnit.RadiansPerUnit.ToString(), ")\r\n本初子午线: ", GeoCoordSys.PrimeMeridian.Name,
                " (", GeoCoordSys.PrimeMeridian.Longitude.ToString(), ")\r\n数据: ", GeoCoordSys.Datum.Name,
                "\r\n  椭球体: ", GeoCoordSys.Datum.Spheroid.Name, "\r\n    长半轴: ",
                GeoCoordSys.Datum.Spheroid.SemiMajorAxis.ToString(), "\r\n    短半轴: ",
                GeoCoordSys.Datum.Spheroid.SemiMinorAxis.ToString(), "\r\n    扁率倒数: ",
                (1.0/GeoCoordSys.Datum.Spheroid.Flattening).ToString()
            };
            string str = string.Concat(strArray);
            this.textBoxGeoCoodSys.Text = str;
        }

        private void WriteToParamListView(IParameter[] pParamters)
        {
            this.paramlistView.Items.Clear();
            string[] items = new string[2];
            for (int i = 0; i < pParamters.Length; i++)
            {
                if (pParamters[i] == null)
                {
                    break;
                }
                items[0] = pParamters[i].Name;
                items[1] = pParamters[i].Value.ToString();
                this.paramlistView.Items.Add(new ListViewItem(items));
            }
        }

        public IGeographicCoordinateSystem GeographicCoordinateSystem
        {
            get { return this.m_GeoCoordSys; }
        }

        public IParameter[] Paramters
        {
            get
            {
                int index = 0;
                while (index < this.m_pParamters.Length)
                {
                    if (this.m_pParamters[index] == null)
                    {
                        break;
                    }
                    index++;
                }
                IParameter[] parameterArray = new IParameter[index];
                for (int i = 0; i < index; i++)
                {
                    parameterArray[i] = this.m_pParamters[i];
                }
                return parameterArray;
            }
        }

        public IProjectedCoordinateSystem ProjectedCoordinateSystem
        {
            set { this.m_ProjectedCoordSys = value; }
        }

        public IProjection Projection
        {
            get { return this.m_Projection; }
        }
    }
}