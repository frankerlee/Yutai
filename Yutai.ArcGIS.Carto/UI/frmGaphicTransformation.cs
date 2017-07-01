using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.ControlExtend;
using Yutai.ArcGIS.Common.Wrapper;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class frmGaphicTransformation : Form
    {
        private bool bool_0 = false;
        private IContainer icontainer_0 = null;

        private IGeoTransformation[] igeoTransformation_0 = new IGeoTransformation[]
        {
            new GeocentricTranslationClass(), new MolodenskyTransformationClass(),
            new AbridgedMolodenskyTransformationClass(), new PositionVectorTransformationClass(),
            new CoordinateFrameTransformationClass(), new NADCONTransformationClass(), new HARNTransformationClass(),
            new LongitudeRotationTransformationClass()
        };

        private IGeoTransformation igeoTransformation_1 = null;
        private int[] int_0 = new int[] {4214, 4610, 4326};
        private ISpatialReference ispatialReference_0 = null;
        private ISpatialReference ispatialReference_1 = null;

        private string[] string_0 = new string[]
            {"地心装换", "Molodendky装换", "简化Molodendky装换", "位置矢量装换", "坐标框架装换", "基于NADCON装换", "基于HRAN装换", "经度旋转装换"};

        private string[] string_1 = new string[] {"X轴平移", "Y轴平移", "Z轴平移", "X轴旋转", "Y轴旋转", "Z轴旋转", "缩放"};
        private string[] string_2 = new string[] {"X轴平移", "Y轴平移", "Z轴平移"};

        public frmGaphicTransformation()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.cboTargetGCS.SelectedIndex == -1)
            {
                MessageBox.Show("请选择目标地理框架");
            }
            else if (this.txtSourGCS.Tag != null)
            {
                if (this.cboHCSTransformMethod.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择转转方法");
                }
                else if (this.txtName.Text.Trim().Length == 0)
                {
                    MessageBox.Show("请输入新建地理坐标转转名称");
                }
                else
                {
                    ISpatialReferenceFactory factory = new SpatialReferenceEnvironmentClass();
                    ISpatialReference to = null;
                    if (this.cboTargetGCS.SelectedIndex == 0)
                    {
                        to = factory.CreateGeographicCoordinateSystem(4214);
                    }
                    else if (this.cboTargetGCS.SelectedIndex == 1)
                    {
                        to = factory.CreateGeographicCoordinateSystem(4610);
                    }
                    else
                    {
                        to = factory.CreateGeographicCoordinateSystem(4326);
                    }
                    if (this.igeoTransformation_1 != null)
                    {
                        this.igeoTransformation_1.Name = this.txtName.Text.Trim();
                    }
                    else
                    {
                        IGeoTransformation geoTransformation =
                            (this.cboHCSTransformMethod.SelectedItem as Class1).GeoTransformation;
                        geoTransformation.PutSpatialReferences(this.txtSourGCS.Tag as ISpatialReference, to);
                        geoTransformation.Name = this.txtName.Text;
                        this.igeoTransformation_1 = geoTransformation;
                    }
                    base.DialogResult = DialogResult.OK;
                }
            }
        }

        private void cboHCSTransformMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.paramlistView.Items.Clear();
                if (this.cboHCSTransformMethod.SelectedIndex != -1)
                {
                    IGeoTransformation geoTransformation;
                    int num;
                    double[] numArray = new double[7];
                    string[] items = new string[2];
                    if (this.igeoTransformation_1 != null)
                    {
                        geoTransformation = this.igeoTransformation_1;
                    }
                    else
                    {
                        geoTransformation = (this.cboHCSTransformMethod.SelectedItem as Class1).GeoTransformation;
                        ISpatialReference to =
                            (this.cboTargetGCS.SelectedItem as ObjectWrap).Object as ISpatialReference;
                        geoTransformation.PutSpatialReferences(this.txtSourGCS.Tag as ISpatialReference, to);
                    }
                    if (geoTransformation is ICoordinateFrameTransformation)
                    {
                        (geoTransformation as ICoordinateFrameTransformation).GetParameters(out numArray[0],
                            out numArray[1], out numArray[2], out numArray[3], out numArray[4], out numArray[5],
                            out numArray[6]);
                        for (num = 0; num < this.string_1.Length; num++)
                        {
                            items[0] = this.string_1[num];
                            items[1] = numArray[num].ToString();
                            this.paramlistView.Items.Add(new ListViewItem(items));
                        }
                    }
                    else if (geoTransformation is IMolodenskyTransformation)
                    {
                        (geoTransformation as IMolodenskyTransformation).GetParameters(out numArray[0], out numArray[1],
                            out numArray[2]);
                        for (num = 0; num < this.string_2.Length; num++)
                        {
                            items[0] = this.string_2[num];
                            items[1] = numArray[num].ToString();
                            this.paramlistView.Items.Add(new ListViewItem(items));
                        }
                    }
                    else if (geoTransformation is IGeocentricTranslation)
                    {
                        (geoTransformation as IGeocentricTranslation).GetParameters(out numArray[0], out numArray[1],
                            out numArray[2]);
                        for (num = 0; num < this.string_2.Length; num++)
                        {
                            items[0] = this.string_2[num];
                            items[1] = numArray[num].ToString();
                            this.paramlistView.Items.Add(new ListViewItem(items));
                        }
                    }
                    else if (geoTransformation is IPositionVectorTransformation)
                    {
                        (geoTransformation as IPositionVectorTransformation).GetParameters(out numArray[0],
                            out numArray[1], out numArray[2], out numArray[3], out numArray[4], out numArray[5],
                            out numArray[6]);
                        for (num = 0; num < this.string_1.Length; num++)
                        {
                            items[0] = this.string_1[num];
                            items[1] = numArray[num].ToString();
                            this.paramlistView.Items.Add(new ListViewItem(items));
                        }
                    }
                    else if (!(geoTransformation is IGridTransformation) &&
                             !(geoTransformation is ILongitudeRotationTransformation))
                    {
                    }
                }
            }
        }

        private void cboTargetGCS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.cboHCSTransformMethod_SelectedIndexChanged(this, e);
            }
        }

        private void frmGaphicTransformation_Load(object sender, EventArgs e)
        {
            int num;
            for (num = 0; num < this.igeoTransformation_0.Length; num++)
            {
                this.cboHCSTransformMethod.Items.Add(new Class1(this.igeoTransformation_0[num], this.string_0[num]));
            }
            this.cboTargetGCS.Items.Clear();
            ISpatialReferenceFactory factory = new SpatialReferenceEnvironmentClass();
            ISpatialReference reference = null;
            for (num = 0; num < this.int_0.Length; num++)
            {
                reference = factory.CreateGeographicCoordinateSystem(this.int_0[num]);
                this.cboTargetGCS.Items.Add(new ObjectWrap(reference));
            }
            if (this.ispatialReference_1 != null)
            {
                this.cboTargetGCS.Text = this.ispatialReference_1.Name;
                this.cboTargetGCS.Enabled = false;
            }
            else
            {
                this.cboTargetGCS.SelectedIndex = 0;
            }
            this.txtName.Text = "新装换";
            this.paramlistView.ValueChanged += new ValueChangedHandler(this.method_0);
            if (this.ispatialReference_0 is IProjectedCoordinateSystem)
            {
                this.txtSourGCS.Text =
                    (this.ispatialReference_0 as IProjectedCoordinateSystem).GeographicCoordinateSystem.Name;
                this.txtSourGCS.Tag =
                    (this.ispatialReference_0 as IProjectedCoordinateSystem).GeographicCoordinateSystem;
            }
            else if (this.ispatialReference_0 is IGeographicCoordinateSystem)
            {
                this.txtSourGCS.Text = this.ispatialReference_0.Name;
                this.txtSourGCS.Tag = this.ispatialReference_0;
            }
            this.bool_0 = true;
            if (this.igeoTransformation_1 != null)
            {
                this.txtName.Text = this.igeoTransformation_1.Name;
                if (this.igeoTransformation_1 is ICoordinateFrameTransformation)
                {
                    this.cboHCSTransformMethod.SelectedIndex = 4;
                }
                else if (this.igeoTransformation_1 is IMolodenskyTransformation)
                {
                    if (this.igeoTransformation_1 is MolodenskyTransformationClass)
                    {
                        this.cboHCSTransformMethod.SelectedIndex = 1;
                    }
                    else if (this.igeoTransformation_1 is AbridgedMolodenskyTransformationClass)
                    {
                        this.cboHCSTransformMethod.SelectedIndex = 2;
                    }
                }
                else if (this.igeoTransformation_1 is IGeocentricTranslation)
                {
                    this.cboHCSTransformMethod.SelectedIndex = 0;
                }
                else if (this.igeoTransformation_1 is IPositionVectorTransformation)
                {
                    this.cboHCSTransformMethod.SelectedIndex = 3;
                }
                else if (this.igeoTransformation_1 is IGridTransformation)
                {
                    if (this.igeoTransformation_1 is NADCONTransformationClass)
                    {
                        this.cboHCSTransformMethod.SelectedIndex = 5;
                    }
                    else if (this.igeoTransformation_1 is HARNTransformationClass)
                    {
                        this.cboHCSTransformMethod.SelectedIndex = 6;
                    }
                }
                else if (this.igeoTransformation_1 is ILongitudeRotationTransformation)
                {
                    this.cboHCSTransformMethod.SelectedIndex = 7;
                }
                this.cboHCSTransformMethod.Enabled = false;
            }
            else
            {
                this.cboHCSTransformMethod.SelectedIndex = 0;
            }
        }

        private void method_0(object sender, ValueChangedEventArgs e)
        {
            IGeoTransformation geoTransformation;
            double[] numArray = new double[7];
            if (this.igeoTransformation_1 != null)
            {
                geoTransformation = this.igeoTransformation_1;
            }
            else
            {
                geoTransformation = (this.cboHCSTransformMethod.SelectedItem as Class1).GeoTransformation;
            }
            if (geoTransformation is ICoordinateFrameTransformation)
            {
                (geoTransformation as ICoordinateFrameTransformation).GetParameters(out numArray[0], out numArray[1],
                    out numArray[2], out numArray[3], out numArray[4], out numArray[5], out numArray[6]);
                try
                {
                    numArray[e.Row] = int.Parse(e.NewValue.ToString());
                    (geoTransformation as ICoordinateFrameTransformation).PutParameters(numArray[0], numArray[1],
                        numArray[2], numArray[3], numArray[4], numArray[5], numArray[6]);
                }
                catch
                {
                }
            }
            else if (geoTransformation is IMolodenskyTransformation)
            {
                (geoTransformation as IMolodenskyTransformation).GetParameters(out numArray[0], out numArray[1],
                    out numArray[2]);
                try
                {
                    numArray[e.Row] = int.Parse(e.NewValue.ToString());
                    (geoTransformation as IMolodenskyTransformation).PutParameters(numArray[0], numArray[1], numArray[2]);
                }
                catch
                {
                }
            }
            else if (geoTransformation is IGeocentricTranslation)
            {
                (geoTransformation as IGeocentricTranslation).GetParameters(out numArray[0], out numArray[1],
                    out numArray[2]);
                try
                {
                    numArray[e.Row] = int.Parse(e.NewValue.ToString());
                    (geoTransformation as IGeocentricTranslation).PutParameters(numArray[0], numArray[1], numArray[2]);
                }
                catch
                {
                }
            }
            else if (geoTransformation is IPositionVectorTransformation)
            {
                (geoTransformation as IPositionVectorTransformation).GetParameters(out numArray[0], out numArray[1],
                    out numArray[2], out numArray[3], out numArray[4], out numArray[5], out numArray[6]);
                try
                {
                    numArray[e.Row] = int.Parse(e.NewValue.ToString());
                    (geoTransformation as IPositionVectorTransformation).PutParameters(numArray[0], numArray[1],
                        numArray[2], numArray[3], numArray[4], numArray[5], numArray[6]);
                }
                catch
                {
                }
            }
        }

        public IGeoTransformation GeoTransformations
        {
            get { return this.igeoTransformation_1; }
            set
            {
                this.igeoTransformation_1 = value;
                this.igeoTransformation_1.GetSpatialReferences(out this.ispatialReference_0,
                    out this.ispatialReference_1);
            }
        }

        public ISpatialReference SourceSpatialReference
        {
            set { this.ispatialReference_0 = value; }
        }

        public ISpatialReference TargetSpatialReference
        {
            set { this.ispatialReference_1 = value; }
        }

        private partial class Class1
        {
            private IGeoTransformation igeoTransformation_0;
            private string string_0;

            internal Class1(IGeoTransformation igeoTransformation_1)
            {
                this.igeoTransformation_0 = null;
                this.string_0 = "";
                this.igeoTransformation_0 = igeoTransformation_1;
            }

            internal Class1(IGeoTransformation igeoTransformation_1, string string_1)
            {
                this.igeoTransformation_0 = null;
                this.string_0 = "";
                this.igeoTransformation_0 = igeoTransformation_1;
                this.string_0 = string_1;
            }

            public override string ToString()
            {
                return this.string_0;
            }

            internal IGeoTransformation GeoTransformation
            {
                get { return this.igeoTransformation_0; }
            }
        }
    }
}