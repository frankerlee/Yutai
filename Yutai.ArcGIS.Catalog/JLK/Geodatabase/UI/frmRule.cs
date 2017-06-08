namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.Geodatabase;
    using ESRI.ArcGIS.Geometry;
    using JLK.Editors;
    using JLK.Editors.Controls;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class frmRule : Form
    {
        private bool bool_0;
        private Button btnCancel;
        private Button btnOK;
        private CheckEdit chkShowError;
        private System.Windows.Forms.ComboBox comboDestClass;
        private System.Windows.Forms.ComboBox comboOriginClass;
        private System.Windows.Forms.ComboBox comboRule;
        private esriTopologyRuleType[] esriTopologyRuleType_0 = new esriTopologyRuleType[] { esriTopologyRuleType.esriTRTPointCoveredByAreaBoundary, esriTopologyRuleType.esriTRTPointCoveredByLineEndpoint, esriTopologyRuleType.esriTRTPointCoveredByLine, esriTopologyRuleType.esriTRTPointProperlyInsideArea };
        private esriTopologyRuleType[] esriTopologyRuleType_1 = new esriTopologyRuleType[] { esriTopologyRuleType.esriTRTLineNoOverlap, esriTopologyRuleType.esriTRTLineNoIntersection, esriTopologyRuleType.esriTRTLineCoveredByLineClass, esriTopologyRuleType.esriTRTLineNoOverlapLine, esriTopologyRuleType.esriTRTLineCoveredByAreaBoundary, esriTopologyRuleType.esriTRTLineNoDangles, esriTopologyRuleType.esriTRTLineNoPseudos, esriTopologyRuleType.esriTRTLineNoSelfOverlap, esriTopologyRuleType.esriTRTLineNoSelfIntersect, esriTopologyRuleType.esriTRTLineNoMultipart, esriTopologyRuleType.esriTRTLineNoIntersectOrInteriorTouch, esriTopologyRuleType.esriTRTLineEndpointCoveredByPoint };
        private esriTopologyRuleType[] esriTopologyRuleType_2 = new esriTopologyRuleType[] { esriTopologyRuleType.esriTRTAreaNoOverlap, esriTopologyRuleType.esriTRTAreaNoGaps, esriTopologyRuleType.esriTRTAreaNoOverlapArea, esriTopologyRuleType.esriTRTAreaCoveredByAreaClass, esriTopologyRuleType.esriTRTAreaAreaCoverEachOther, esriTopologyRuleType.esriTRTAreaCoveredByArea, esriTopologyRuleType.esriTRTAreaBoundaryCoveredByLine, esriTopologyRuleType.esriTRTAreaBoundaryCoveredByAreaBoundary, esriTopologyRuleType.esriTRTAreaContainPoint };
        private GroupBox groupBox1;
        private IArray iarray_0;
        private IArray iarray_1 = new ArrayClass();
        private IContainer icontainer_0;
        private ImageComboBoxEdit imageComboBoxEdit1;
        private ImageList imageList_0;
        private int int_0;
        private ITopologyRule itopologyRule_0 = new TopologyRuleClass();
        private Label label1;
        private Label label2;
        private Label label3;
        private Label lblTopoInfo1;
        private Label lblTopoInfo2;
        private string[] string_0 = new string[] { "必须被面要素边界线覆盖，使用", "必须被线要素终点覆盖，使用", "点必须被线要素覆盖，使用", "点落在面要素内，使用" };
        private string[] string_1 = new string[] { "不能重叠", "不能相交", "必须被线要素覆盖，使用", "不能重叠，与", "必须被面要素边界线覆盖，使用", "不能有悬挂点", "不能有伪节点", "不能自重叠", "不能自相交", "必须为单部分", "不能相交或内部相接", "线终点必须被点要素覆盖，用" };
        private string[] string_2 = new string[] { "不能重叠", "不能有缝隙", "不能与其他面层重叠，使用", "必须被面要素类覆盖，使用", "必须和其它面要素层相互覆盖，使用", "必须被覆盖，使用", "边界线必须被线要素覆盖，使用", "面边界线必须被其它面层边界线覆盖，使用", "包含点" };
        private string[] string_3 = new string[] { 
            "第一层中的点要素必须被第二层中的面要素的边界覆盖。", "点层中没有落在面层边界上的要素被视为错误。", "第一层中的点要素必须被第二层中的线要素的终点覆盖。", "点层中没有落在线的终点上的要素被视为错误。", "第一层的点要素必须被第二层中的线要素覆盖。", "任何没被线要素覆盖的点被视为错误。", "第一层的点要素必须完全位于第二层的面要素之内。", "任何没有位于面要素之内的点被视为错误。", "同一层中线要素不能与其他线重叠。", "任何重叠的线段被视为错误。", "同一线层中的线不能与其他线相交或重叠", "任何重叠的线段或相交点都被视为错误", "第一层的线段必须与第二层的线一致", "位于第一层内与第二层不一致的线段被视为错误。", "第一层中线不能与第二层的线重叠。", "任何第二层与第一层要素重叠的线段被视为错误。", 
            "第一层中的线要素必须被第二层中的面要素的边界覆盖。", "任何位于线层而不被面层边界覆盖的线段被视为错误。", "一条线必须与同层中其他线的终点相接。", "任何不与其他线相接的线的终点被视为错误。", "一条线必须与同层中多于一条的线在终点处相接。", "任何只有两条线相接的终点被视为错误", "一个层中的线要素不允许自重叠。", "任何自重叠的线段被视为错误。", "一个层中的要素不允许自相交或自重叠。", "任何自重叠的线段或自相交的点别视为错误。", "一个层中的线要素不允许超过一个部分。", "任何超过一个部分的线要素被视为错误。", "同一层的线必须在终点处与另外的线相接。", "任何重叠的线或相交的点被视为错误。", "第一层中线的终点必须被第二层中的点要素覆盖。", "任何没有被点要素覆盖的终点被视为错误。", 
            "同一层内面与面不能重叠。", "任何要素重叠的区域被视为错误", "同一层的面之间不能存在缝隙", "缝隙的边界被视为错误。", "第一层的面不能与第二层的面重叠。", "任何第二层与第一层要素重叠的区域被视为错误。", "第一层的面要素必须被第二层的面要素覆盖。", "任何第一层没有被第二层的要素覆盖的区域被视为错误。", "第一层的面要素必须和第二层的面要素相互覆盖。", "任意一层没有被另一层的要素覆盖的区域被视为错误。", "第一层的一个面要素必须完全包含在第二层的一个面要素内。", "任何位于第一层中没有被第二层中的一个要素完全包含的区域被视为错误。", "第一层中面要素的边界必须和第二层中的线要素覆盖。", "面要素中不与线要素相符的边界被视为错误。", "第一层中面要素的边界必须被第二层中的面要素边界覆盖。", "任何没有被第二层的面要素边界覆盖的第一层的面要素边界被为错误。", 
            "第一层的面要素必须至少包含第二层中的一个点要素。", "任何没有包含点要素的面要素被为错误。"
         };

        public frmRule()
        {
            this.InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.itopologyRule_0.OriginClassID = ((IFeatureClass) this.iarray_0.get_Element(this.comboOriginClass.SelectedIndex)).ObjectClassID;
            this.itopologyRule_0.AllOriginSubtypes = true;
            this.bool_0 = true;
            if (this.comboDestClass.Items.Count > 0)
            {
                if (this.itopologyRule_0.OriginClassID == ((IFeatureClass) this.iarray_1.get_Element(this.comboDestClass.SelectedIndex)).ObjectClassID)
                {
                    this.bool_0 = false;
                    base.Close();
                    return;
                }
                this.itopologyRule_0.DestinationClassID = ((IFeatureClass) this.iarray_1.get_Element(this.comboDestClass.SelectedIndex)).ObjectClassID;
            }
            switch (this.int_0)
            {
                case 1:
                    this.itopologyRule_0.TopologyRuleType = this.esriTopologyRuleType_0[this.comboRule.SelectedIndex];
                    this.itopologyRule_0.Name = this.string_0[this.comboRule.SelectedIndex];
                    break;

                case 2:
                    this.itopologyRule_0.TopologyRuleType = this.esriTopologyRuleType_1[this.comboRule.SelectedIndex];
                    this.itopologyRule_0.Name = this.string_1[this.comboRule.SelectedIndex];
                    break;

                case 3:
                    this.itopologyRule_0.TopologyRuleType = this.esriTopologyRuleType_2[this.comboRule.SelectedIndex];
                    this.itopologyRule_0.Name = this.string_2[this.comboRule.SelectedIndex];
                    break;
            }
            base.Close();
        }

        private void chkShowError_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkShowError.Checked)
            {
                this.imageComboBoxEdit1.SelectedIndex--;
            }
            else
            {
                this.imageComboBoxEdit1.SelectedIndex++;
            }
        }

        private void comboOriginClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            IFeatureClass class2 = (IFeatureClass) this.iarray_0.get_Element(this.comboOriginClass.SelectedIndex);
            if (class2.ShapeType == esriGeometryType.esriGeometryPoint)
            {
                this.int_0 = 1;
                this.method_0(this.int_0);
            }
            else if ((class2.ShapeType == esriGeometryType.esriGeometryPolyline) || (class2.ShapeType == esriGeometryType.esriGeometryLine))
            {
                this.int_0 = 2;
                this.method_0(this.int_0);
            }
            else if (class2.ShapeType == esriGeometryType.esriGeometryPolygon)
            {
                this.int_0 = 3;
                this.method_0(this.int_0);
            }
        }

        private void comboRule_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.iarray_1.RemoveAll();
            this.comboDestClass.Items.Clear();
            this.comboDestClass.Enabled = false;
            if (this.int_0 == 1)
            {
                if ((this.esriTopologyRuleType_0[this.comboRule.SelectedIndex] == esriTopologyRuleType.esriTRTPointCoveredByAreaBoundary) || (this.esriTopologyRuleType_0[this.comboRule.SelectedIndex] == esriTopologyRuleType.esriTRTPointProperlyInsideArea))
                {
                    this.method_1(3);
                }
                else if ((this.esriTopologyRuleType_0[this.comboRule.SelectedIndex] == esriTopologyRuleType.esriTRTPointCoveredByLine) || (this.esriTopologyRuleType_0[this.comboRule.SelectedIndex] == esriTopologyRuleType.esriTRTPointCoveredByLineEndpoint))
                {
                    this.method_1(2);
                }
                if (this.chkShowError.Checked)
                {
                    this.imageComboBoxEdit1.SelectedIndex = 2 * this.comboRule.SelectedIndex;
                }
                else
                {
                    this.imageComboBoxEdit1.SelectedIndex = (2 * this.comboRule.SelectedIndex) + 1;
                }
                this.lblTopoInfo1.Text = this.string_3[2 * this.comboRule.SelectedIndex];
                this.lblTopoInfo2.Text = this.string_3[(2 * this.comboRule.SelectedIndex) + 1];
            }
            else if (this.int_0 == 2)
            {
                if (this.esriTopologyRuleType_1[this.comboRule.SelectedIndex] == esriTopologyRuleType.esriTRTLineCoveredByAreaBoundary)
                {
                    this.method_1(3);
                }
                else if ((this.esriTopologyRuleType_1[this.comboRule.SelectedIndex] == esriTopologyRuleType.esriTRTLineCoveredByLineClass) || (this.esriTopologyRuleType_1[this.comboRule.SelectedIndex] == esriTopologyRuleType.esriTRTLineNoOverlapLine))
                {
                    this.method_1(2);
                }
                else if (this.esriTopologyRuleType_1[this.comboRule.SelectedIndex] == esriTopologyRuleType.esriTRTLineEndpointCoveredByPoint)
                {
                    this.method_1(1);
                }
                if (this.chkShowError.Checked)
                {
                    this.imageComboBoxEdit1.SelectedIndex = 8 + (2 * this.comboRule.SelectedIndex);
                }
                else
                {
                    this.imageComboBoxEdit1.SelectedIndex = 9 + (2 * this.comboRule.SelectedIndex);
                }
                this.lblTopoInfo1.Text = this.string_3[(2 * this.comboRule.SelectedIndex) + 8];
                this.lblTopoInfo2.Text = this.string_3[(2 * this.comboRule.SelectedIndex) + 9];
            }
            else if (this.int_0 == 3)
            {
                if ((((this.esriTopologyRuleType_2[this.comboRule.SelectedIndex] == esriTopologyRuleType.esriTRTAreaAreaCoverEachOther) || (this.esriTopologyRuleType_2[this.comboRule.SelectedIndex] == esriTopologyRuleType.esriTRTAreaBoundaryCoveredByAreaBoundary)) || ((this.esriTopologyRuleType_2[this.comboRule.SelectedIndex] == esriTopologyRuleType.esriTRTAreaCoveredByArea) || (this.esriTopologyRuleType_2[this.comboRule.SelectedIndex] == esriTopologyRuleType.esriTRTAreaCoveredByAreaClass))) || (this.esriTopologyRuleType_2[this.comboRule.SelectedIndex] == esriTopologyRuleType.esriTRTAreaNoOverlapArea))
                {
                    this.method_1(3);
                }
                else if (this.esriTopologyRuleType_2[this.comboRule.SelectedIndex] == esriTopologyRuleType.esriTRTAreaBoundaryCoveredByLine)
                {
                    this.method_1(2);
                }
                else if (this.esriTopologyRuleType_2[this.comboRule.SelectedIndex] == esriTopologyRuleType.esriTRTAreaContainPoint)
                {
                    this.method_1(1);
                }
                if (this.chkShowError.Checked)
                {
                    this.imageComboBoxEdit1.SelectedIndex = 0x20 + (2 * this.comboRule.SelectedIndex);
                }
                else
                {
                    this.imageComboBoxEdit1.SelectedIndex = 0x21 + (2 * this.comboRule.SelectedIndex);
                }
                this.lblTopoInfo1.Text = this.string_3[(2 * this.comboRule.SelectedIndex) + 0x20];
                this.lblTopoInfo2.Text = this.string_3[(2 * this.comboRule.SelectedIndex) + 0x21];
            }
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void frmRule_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < this.iarray_0.Count; i++)
            {
                IFeatureClass class2 = (IFeatureClass) this.iarray_0.get_Element(i);
                this.comboOriginClass.Items.Add(class2.AliasName);
            }
            if (this.comboOriginClass.Items.Count > 0)
            {
                this.comboOriginClass.SelectedIndex = 0;
            }
        }

        private void InitializeComponent()
        {
            this.icontainer_0 = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmRule));
            this.label1 = new Label();
            this.comboOriginClass = new System.Windows.Forms.ComboBox();
            this.label2 = new Label();
            this.comboRule = new System.Windows.Forms.ComboBox();
            this.comboDestClass = new System.Windows.Forms.ComboBox();
            this.label3 = new Label();
            this.btnOK = new Button();
            this.btnCancel = new Button();
            this.imageList_0 = new ImageList(this.icontainer_0);
            this.groupBox1 = new GroupBox();
            this.lblTopoInfo2 = new Label();
            this.lblTopoInfo1 = new Label();
            this.chkShowError = new CheckEdit();
            this.imageComboBoxEdit1 = new ImageComboBoxEdit();
            this.groupBox1.SuspendLayout();
            this.chkShowError.Properties.BeginInit();
            this.imageComboBoxEdit1.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 0x10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x4d, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "要素类的要素";
            this.comboOriginClass.Location = new System.Drawing.Point(8, 40);
            this.comboOriginClass.Name = "comboOriginClass";
            this.comboOriginClass.Size = new Size(0xc0, 20);
            this.comboOriginClass.TabIndex = 1;
            this.comboOriginClass.SelectedIndexChanged += new EventHandler(this.comboOriginClass_SelectedIndexChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 0x40);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "规则";
            this.comboRule.Location = new System.Drawing.Point(8, 0x58);
            this.comboRule.Name = "comboRule";
            this.comboRule.Size = new Size(0xc0, 20);
            this.comboRule.TabIndex = 3;
            this.comboRule.SelectedIndexChanged += new EventHandler(this.comboRule_SelectedIndexChanged);
            this.comboDestClass.Location = new System.Drawing.Point(8, 0x88);
            this.comboDestClass.Name = "comboDestClass";
            this.comboDestClass.Size = new Size(0xc0, 20);
            this.comboDestClass.TabIndex = 4;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 120);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x29, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "要素类";
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(0x150, 0xc0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x38, 0x18);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(0x198, 0xc0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x40, 0x18);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.imageList_0.ImageStream = (ImageListStreamer) manager.GetObject("imageList1.ImageStream");
            this.imageList_0.TransparentColor = Color.Transparent;
            this.imageList_0.Images.SetKeyName(0, "");
            this.imageList_0.Images.SetKeyName(1, "");
            this.imageList_0.Images.SetKeyName(2, "");
            this.imageList_0.Images.SetKeyName(3, "");
            this.imageList_0.Images.SetKeyName(4, "");
            this.imageList_0.Images.SetKeyName(5, "");
            this.imageList_0.Images.SetKeyName(6, "");
            this.imageList_0.Images.SetKeyName(7, "");
            this.imageList_0.Images.SetKeyName(8, "");
            this.imageList_0.Images.SetKeyName(9, "");
            this.imageList_0.Images.SetKeyName(10, "");
            this.imageList_0.Images.SetKeyName(11, "");
            this.imageList_0.Images.SetKeyName(12, "");
            this.imageList_0.Images.SetKeyName(13, "");
            this.imageList_0.Images.SetKeyName(14, "");
            this.imageList_0.Images.SetKeyName(15, "");
            this.imageList_0.Images.SetKeyName(0x10, "");
            this.imageList_0.Images.SetKeyName(0x11, "");
            this.imageList_0.Images.SetKeyName(0x12, "");
            this.imageList_0.Images.SetKeyName(0x13, "");
            this.imageList_0.Images.SetKeyName(20, "");
            this.imageList_0.Images.SetKeyName(0x15, "");
            this.imageList_0.Images.SetKeyName(0x16, "");
            this.imageList_0.Images.SetKeyName(0x17, "");
            this.imageList_0.Images.SetKeyName(0x18, "");
            this.imageList_0.Images.SetKeyName(0x19, "");
            this.imageList_0.Images.SetKeyName(0x1a, "");
            this.imageList_0.Images.SetKeyName(0x1b, "");
            this.imageList_0.Images.SetKeyName(0x1c, "");
            this.imageList_0.Images.SetKeyName(0x1d, "");
            this.imageList_0.Images.SetKeyName(30, "");
            this.imageList_0.Images.SetKeyName(0x1f, "");
            this.imageList_0.Images.SetKeyName(0x20, "");
            this.imageList_0.Images.SetKeyName(0x21, "");
            this.imageList_0.Images.SetKeyName(0x22, "");
            this.imageList_0.Images.SetKeyName(0x23, "");
            this.imageList_0.Images.SetKeyName(0x24, "");
            this.imageList_0.Images.SetKeyName(0x25, "");
            this.imageList_0.Images.SetKeyName(0x26, "");
            this.imageList_0.Images.SetKeyName(0x27, "");
            this.imageList_0.Images.SetKeyName(40, "");
            this.imageList_0.Images.SetKeyName(0x29, "");
            this.imageList_0.Images.SetKeyName(0x2a, "");
            this.imageList_0.Images.SetKeyName(0x2b, "");
            this.imageList_0.Images.SetKeyName(0x2c, "");
            this.imageList_0.Images.SetKeyName(0x2d, "");
            this.imageList_0.Images.SetKeyName(0x2e, "");
            this.imageList_0.Images.SetKeyName(0x2f, "");
            this.imageList_0.Images.SetKeyName(0x30, "");
            this.imageList_0.Images.SetKeyName(0x31, "");
            this.groupBox1.Controls.Add(this.lblTopoInfo2);
            this.groupBox1.Controls.Add(this.lblTopoInfo1);
            this.groupBox1.Controls.Add(this.chkShowError);
            this.groupBox1.Controls.Add(this.imageComboBoxEdit1);
            this.groupBox1.Location = new System.Drawing.Point(0xd0, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x110, 0xb0);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "规则描述";
            this.lblTopoInfo2.Location = new System.Drawing.Point(0x68, 0x68);
            this.lblTopoInfo2.Name = "lblTopoInfo2";
            this.lblTopoInfo2.Size = new Size(160, 0x30);
            this.lblTopoInfo2.TabIndex = 13;
            this.lblTopoInfo1.Location = new System.Drawing.Point(0x68, 0x20);
            this.lblTopoInfo1.Name = "lblTopoInfo1";
            this.lblTopoInfo1.Size = new Size(160, 0x30);
            this.lblTopoInfo1.TabIndex = 12;
            this.chkShowError.EditValue = true;
            this.chkShowError.Location = new System.Drawing.Point(8, 0x98);
            this.chkShowError.Name = "chkShowError";
            this.chkShowError.Properties.Caption = "显示错误";
            this.chkShowError.Size = new Size(0x4b, 0x13);
            this.chkShowError.TabIndex = 11;
            this.chkShowError.CheckedChanged += new EventHandler(this.chkShowError_CheckedChanged);
            this.imageComboBoxEdit1.EditValue = 0;
            this.imageComboBoxEdit1.Location = new System.Drawing.Point(9, 0x18);
            this.imageComboBoxEdit1.Name = "imageComboBoxEdit1";
            this.imageComboBoxEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.imageComboBoxEdit1.Properties.Items.AddRange(new ImageComboBoxItem[] { 
                new ImageComboBoxItem("", 0, 0), new ImageComboBoxItem("", 1, 1), new ImageComboBoxItem("", 2, 2), new ImageComboBoxItem("", 3, 3), new ImageComboBoxItem("", 4, 4), new ImageComboBoxItem("", 5, 5), new ImageComboBoxItem("", 6, 6), new ImageComboBoxItem("", 7, 7), new ImageComboBoxItem("", 8, 8), new ImageComboBoxItem("", 9, 9), new ImageComboBoxItem("", 10, 10), new ImageComboBoxItem("", 11, 11), new ImageComboBoxItem("", 12, 12), new ImageComboBoxItem("", 13, 13), new ImageComboBoxItem("", 14, 14), new ImageComboBoxItem("", 15, 15), 
                new ImageComboBoxItem("", 0x10, 0x10), new ImageComboBoxItem("", 0x11, 0x11), new ImageComboBoxItem("", 0x12, 0x12), new ImageComboBoxItem("", 0x13, 0x13), new ImageComboBoxItem("", 20, 20), new ImageComboBoxItem("", 0x15, 0x15), new ImageComboBoxItem("", 0x16, 0x16), new ImageComboBoxItem("", 0x17, 0x17), new ImageComboBoxItem("", 0x18, 0x18), new ImageComboBoxItem("", 0x19, 0x19), new ImageComboBoxItem("", 0x1a, 0x1a), new ImageComboBoxItem("", 0x1b, 0x1b), new ImageComboBoxItem("", 0x1c, 0x1c), new ImageComboBoxItem("", 0x1d, 0x1d), new ImageComboBoxItem("", 30, 30), new ImageComboBoxItem("", 0x1f, 0x1f), 
                new ImageComboBoxItem("", 0x20, 0x20), new ImageComboBoxItem("", 0x21, 0x21), new ImageComboBoxItem("", 0x22, 0x22), new ImageComboBoxItem("", 0x23, 0x23), new ImageComboBoxItem("", 0x24, 0x24), new ImageComboBoxItem("", 0x25, 0x25), new ImageComboBoxItem("", 0x26, 0x26), new ImageComboBoxItem("", 0x27, 0x27), new ImageComboBoxItem("", 40, 40), new ImageComboBoxItem("", 0x29, 0x29), new ImageComboBoxItem("", 0x2a, 0x2a), new ImageComboBoxItem("", 0x2b, 0x2b), new ImageComboBoxItem("", 0x2c, 0x2c), new ImageComboBoxItem("", 0x2d, 0x2d), new ImageComboBoxItem("", 0x2e, 0x2e), new ImageComboBoxItem("", 0x2f, 0x2f), 
                new ImageComboBoxItem("", 0x30, 0x30), new ImageComboBoxItem("", 0x31, 0x31)
             });
            this.imageComboBoxEdit1.Properties.LargeImages = this.imageList_0;
            this.imageComboBoxEdit1.Properties.ReadOnly = true;
            this.imageComboBoxEdit1.Properties.ShowDropDown = ShowDropDown.Never;
            this.imageComboBoxEdit1.Properties.SmallImages = this.imageList_0;
            this.imageComboBoxEdit1.Size = new Size(0x58, 0x7a);
            this.imageComboBoxEdit1.TabIndex = 9;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x1e8, 0xdd);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.comboDestClass);
            base.Controls.Add(this.comboRule);
            base.Controls.Add(this.comboOriginClass);
            base.Icon = (Icon) manager.GetObject("$Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmRule";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "添加规则";
            base.Load += new EventHandler(this.frmRule_Load);
            this.groupBox1.ResumeLayout(false);
            this.chkShowError.Properties.EndInit();
            this.imageComboBoxEdit1.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void method_0(int int_1)
        {
            int num2;
            this.comboRule.Items.Clear();
            switch (int_1)
            {
                case 1:
                    for (num2 = 0; num2 < this.string_0.Length; num2++)
                    {
                        this.comboRule.Items.Add(this.string_0[num2]);
                    }
                    break;

                case 2:
                    for (num2 = 0; num2 < this.string_1.Length; num2++)
                    {
                        this.comboRule.Items.Add(this.string_1[num2]);
                    }
                    break;

                case 3:
                    for (num2 = 0; num2 < this.string_2.Length; num2++)
                    {
                        this.comboRule.Items.Add(this.string_2[num2]);
                    }
                    break;
            }
            this.comboRule.SelectedIndex = 0;
        }

        private void method_1(int int_1)
        {
            this.iarray_1.RemoveAll();
            bool flag = false;
            for (int i = 0; i < this.iarray_0.Count; i++)
            {
                IFeatureClass unk = (IFeatureClass) this.iarray_0.get_Element(i);
                if ((int_1 == 1) && (unk.ShapeType == esriGeometryType.esriGeometryPoint))
                {
                    flag = true;
                }
                else if ((int_1 == 2) && ((unk.ShapeType == esriGeometryType.esriGeometryPolyline) || (unk.ShapeType == esriGeometryType.esriGeometryLine)))
                {
                    flag = true;
                }
                else if ((int_1 == 3) && (unk.ShapeType == esriGeometryType.esriGeometryPolygon))
                {
                    flag = true;
                }
                if (flag)
                {
                    this.comboDestClass.Items.Add(unk.AliasName);
                    this.iarray_1.Add(unk);
                    flag = false;
                }
            }
            if (this.comboDestClass.Items.Count > 0)
            {
                this.comboDestClass.SelectedIndex = 0;
                this.comboDestClass.Enabled = true;
                this.btnOK.Enabled = true;
            }
            else
            {
                this.comboDestClass.Enabled = false;
                this.btnOK.Enabled = false;
            }
        }

        public bool bVaildRule
        {
            get
            {
                return this.bool_0;
            }
        }

        public IArray OriginClassArray
        {
            set
            {
                this.iarray_0 = value;
            }
        }

        public ITopologyRule TopologyRule
        {
            get
            {
                return this.itopologyRule_0;
            }
        }
    }
}

