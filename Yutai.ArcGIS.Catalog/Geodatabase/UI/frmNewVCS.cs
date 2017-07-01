using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.ControlExtend;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmNewVCS : Form
    {
        private bool bool_0 = false;
        private double[] double_0;
        private IContainer icontainer_0 = null;
        private int int_0 = 0;
        private int[] int_1 = new int[] {9001, 9036};
        private int[] int_2 = new int[] {6214, 6024, 6326, 6610};
        private int[] int_3 = new int[] {7024, 7030, 7049};
        private IParameter[] iparameter_0 = new IParameter[2];
        private ISpatialReferenceFactory ispatialReferenceFactory_0 = new SpatialReferenceEnvironmentClass();
        private IVerticalCoordinateSystem iverticalCoordinateSystem_0 = null;

        public frmNewVCS()
        {
            double[] numArray2 = new double[2];
            numArray2[1] = 1.0;
            this.double_0 = numArray2;
            this.bool_1 = false;
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.textEditName.Text.Length == 0)
            {
                MessageBox.Show("未指定名字，必须为坐标系统指定一个名字！");
            }
            else
            {
                double num;
                if (this.cboLineUnitName.SelectedIndex <= 0)
                {
                    num = Convert.ToDouble(this.txtValue.Text);
                    ((ILinearUnitEdit) this.iunit_0).DefineEx(this.cboLineUnitName.Text, null, null, null, ref num);
                }
                if (this.rdoVCSBase.SelectedIndex == 0)
                {
                    string name = this.cboProjectName.Text;
                    if (name.Trim().Length == 0)
                    {
                        MessageBox.Show("请选择数据框架！");
                        return;
                    }
                    this.ihvdatum_0 = new VerticalDatumClass();
                    ((IVerticalDatumEdit) this.ihvdatum_0).DefineEx(name, name, name, name);
                }
                else
                {
                    if (this.cboDatumName.Text.Trim().Length == 0)
                    {
                        MessageBox.Show("请选择数据框架！");
                        return;
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
                        ((ISpheroidEdit) this.ispheroid_0).DefineEx(this.cboSpheres.Text, null, null, null, ref num,
                            ref num2);
                    }
                    if (this.cboDatumName.SelectedIndex <= 0)
                    {
                        this.ihvdatum_0 = new DatumClass();
                        ((IDatumEdit) this.ihvdatum_0).DefineEx(this.cboDatumName.Text, null, null, null,
                            this.ispheroid_0);
                    }
                }
                object text = this.textEditName.Text;
                object alias = this.textEditName.Text;
                object abbreviation = this.textEditName.Text;
                object remarks = this.textEditName.Text;
                object useage = "everywhere!";
                object hvDatum = this.ihvdatum_0;
                object projectedUnit = this.iunit_0;
                object verticalShift = this.double_0[0];
                object positiveDirection = this.double_0[1];
                if (this.iverticalCoordinateSystem_0 == null)
                {
                    this.iverticalCoordinateSystem_0 = new VerticalCoordinateSystemClass();
                }
                (this.iverticalCoordinateSystem_0 as IVerticalCoordinateSystemEdit).Define(ref text, ref alias,
                    ref abbreviation, ref remarks, ref useage, ref hvDatum, ref projectedUnit, ref verticalShift,
                    ref positiveDirection);
                base.DialogResult = DialogResult.OK;
            }
        }

        private void cboDatumName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboDatumName.SelectedIndex <= 0)
            {
                this.cboSpheres.Enabled = true;
                if (this.cboSpheres.SelectedIndex == -1)
                {
                    this.cboSpheres.SelectedIndex = 0;
                }
            }
            else
            {
                this.cboSpheres.Enabled = false;
                if (this.bool_0)
                {
                    this.ihvdatum_0 =
                        this.ispatialReferenceFactory_0.CreateDatum(this.int_2[this.cboDatumName.SelectedIndex - 1]) as
                            IHVDatum;
                    this.ispheroid_0 = (this.ihvdatum_0 as IDatum).Spheroid;
                    this.bool_0 = false;
                }
                int index = this.cboSpheres.Properties.Items.IndexOf((this.ihvdatum_0 as IDatum).Spheroid.Name);
                if (index != -1)
                {
                    this.cboSpheres.SelectedIndex = index;
                }
                this.bool_0 = true;
            }
        }

        private void cboLineUnitName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboLineUnitName.SelectedIndex <= 0)
            {
                this.method_1(this.txtValue, false);
            }
            else
            {
                this.method_1(this.txtValue, true);
                if (this.bool_0)
                {
                    this.iunit_0 =
                        this.ispatialReferenceFactory_0.CreateUnit(this.int_1[this.cboLineUnitName.SelectedIndex - 1]);
                }
                this.txtValue.Text = (this.iunit_0 as ILinearUnit).MetersPerUnit.ToString();
            }
        }

        private void cboProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cboSpheres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboSpheres.SelectedIndex <= 0)
            {
                this.method_1(this.txtMajorAxis, false);
                this.method_1(this.txtMiniorAxis, false);
                this.method_1(this.txtFlattening, false);
            }
            else
            {
                this.method_1(this.txtMajorAxis, true);
                this.method_1(this.txtMiniorAxis, true);
                this.method_1(this.txtFlattening, true);
                if (this.bool_0)
                {
                    this.ispheroid_0 =
                        this.ispatialReferenceFactory_0.CreateSpheroid(this.int_3[this.cboSpheres.SelectedIndex - 1]);
                }
                this.txtMajorAxis.Text = this.ispheroid_0.SemiMajorAxis.ToString();
                this.txtMiniorAxis.Text = this.ispheroid_0.SemiMinorAxis.ToString();
                this.txtFlattening.Text = (1.0/this.ispheroid_0.Flattening).ToString();
            }
        }

        private void frmNewVCS_Load(object sender, EventArgs e)
        {
            int index;
            if (this.bool_1)
            {
                this.Text = "编辑垂直坐标系";
            }
            else
            {
                this.Text = "新建垂直坐标系";
            }
            this.rdoMiniorAxis.Checked = true;
            this.txtFlattening.Enabled = false;
            if (this.iverticalCoordinateSystem_0 == null)
            {
                this.bool_0 = true;
                index = -1;
                this.iverticalCoordinateSystem_0 = new VerticalCoordinateSystemClass();
                this.iunit_0 = this.iverticalCoordinateSystem_0.CoordinateUnit;
                this.ihvdatum_0 = this.iverticalCoordinateSystem_0.Datum;
                if (this.ihvdatum_0 is IDatum)
                {
                    this.ispheroid_0 = (this.ihvdatum_0 as IDatum).Spheroid;
                    this.rdoVCSBase.SelectedIndex = 1;
                    index = this.cboDatumName.Properties.Items.IndexOf((this.ihvdatum_0 as IDatum).Name);
                    if (index != -1)
                    {
                        this.cboDatumName.SelectedIndex = index;
                    }
                    else
                    {
                        this.cboDatumName.Text = (this.ihvdatum_0 as IDatum).Name;
                        if (this.cboDatumName.Text == "")
                        {
                            this.cboDatumName.SelectedIndex = 0;
                        }
                        else
                        {
                            index = this.cboSpheres.Properties.Items.IndexOf((this.ihvdatum_0 as IDatum).Spheroid.Name);
                            if (index != -1)
                            {
                                this.cboSpheres.SelectedIndex = index;
                            }
                            else
                            {
                                this.cboSpheres.Text = (this.ihvdatum_0 as IDatum).Spheroid.Name;
                                this.txtMajorAxis.Text = this.ispheroid_0.SemiMajorAxis.ToString();
                                this.txtMiniorAxis.Text = this.ispheroid_0.SemiMinorAxis.ToString();
                                double num2 = 1.0/this.ispheroid_0.Flattening;
                                this.txtFlattening.Text = num2.ToString();
                            }
                        }
                    }
                }
                else
                {
                    this.rdoVCSBase.SelectedIndex = 0;
                    this.cboProjectName.Text = (this.ihvdatum_0 as IVerticalDatum).Name;
                }
                this.cboLineUnitName.SelectedIndex = 1;
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
            }
            else
            {
                this.textEditName.Text = this.iverticalCoordinateSystem_0.Name;
                this.ihvdatum_0 = this.iverticalCoordinateSystem_0.Datum;
                index = -1;
                this.iunit_0 = this.iverticalCoordinateSystem_0.CoordinateUnit;
                if (this.ihvdatum_0 is IDatum)
                {
                    this.ispheroid_0 = (this.ihvdatum_0 as IDatum).Spheroid;
                    this.rdoVCSBase.SelectedIndex = 1;
                    index = this.cboDatumName.Properties.Items.IndexOf((this.ihvdatum_0 as IDatum).Name);
                    if (index != -1)
                    {
                        this.cboDatumName.SelectedIndex = index;
                    }
                    else
                    {
                        this.cboDatumName.Text = (this.ihvdatum_0 as IDatum).Name;
                        index = this.cboSpheres.Properties.Items.IndexOf((this.ihvdatum_0 as IDatum).Spheroid.Name);
                        if (index != -1)
                        {
                            this.cboSpheres.SelectedIndex = index;
                        }
                        else
                        {
                            this.cboSpheres.Text = (this.ihvdatum_0 as IDatum).Spheroid.Name;
                            this.txtMajorAxis.Text = this.ispheroid_0.SemiMajorAxis.ToString();
                            this.txtMiniorAxis.Text = this.ispheroid_0.SemiMinorAxis.ToString();
                            this.txtFlattening.Text = (1.0/this.ispheroid_0.Flattening).ToString();
                        }
                    }
                }
                else
                {
                    this.rdoVCSBase.SelectedIndex = 0;
                    this.cboProjectName.Text = (this.ihvdatum_0 as IVerticalDatum).Name;
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
                (this.iverticalCoordinateSystem_0 as IVerticalCoordinateSystemGEN).GetParameters(ref this.iparameter_0);
                if (this.iparameter_0[0] != null)
                {
                    this.double_0[0] = this.iparameter_0[0].Value;
                    if (this.iparameter_0[1] != null)
                    {
                        this.double_0[1] = this.iparameter_0[1].Value;
                    }
                }
                this.bool_0 = true;
            }
            this.method_0(this.iparameter_0);
        }

        private void method_0(IParameter[] iparameter_1)
        {
            this.paramlistView.Items.Clear();
            string[] items = new string[] {"垂直偏移", this.double_0[0].ToString()};
            this.paramlistView.Items.Add(new ListViewItem(items));
            items[0] = "方向";
            items[1] = this.double_0[1].ToString();
            this.paramlistView.Items.Add(new ListViewItem(items));
        }

        private void method_1(TextEdit textEdit_0, bool bool_2)
        {
            textEdit_0.Properties.ReadOnly = bool_2;
            if (textEdit_0.Properties.ReadOnly)
            {
                textEdit_0.BackColor = SystemColors.InactiveBorder;
            }
            else
            {
                textEdit_0.BackColor = SystemColors.Window;
            }
        }

        private void method_2(object sender, ValueChangedEventArgs e)
        {
            try
            {
                double num = Convert.ToDouble(e.NewValue);
                this.double_0[e.Row] = num;
            }
            catch
            {
                MessageBox.Show("数据格式错!");
            }
        }

        private void paramlistView_SelectedIndexChanged(object sender, EventArgs e)
        {
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

        private void rdoVCSBase_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool flag = this.rdoVCSBase.SelectedIndex == 0;
            this.cboProjectName.Enabled = flag;
            this.cboDatumName.Enabled = !flag;
            this.groupBoxSpere.Enabled = !flag;
        }

        public IVerticalCoordinateSystem VerticalCoordinateSystem
        {
            get { return this.iverticalCoordinateSystem_0; }
            set
            {
                this.bool_1 = true;
                this.iverticalCoordinateSystem_0 = value;
            }
        }
    }
}