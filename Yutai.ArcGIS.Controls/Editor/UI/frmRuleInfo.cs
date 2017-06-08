using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    public class frmRuleInfo : Form
    {
        private string[] AreaRuleDes = new string[] { "不能重叠", "不能有缝隙", "不能与其他面层重叠，使用", "必须被面要素类覆盖，使用", "必须和其它面要素层相互覆盖，使用", "必须被覆盖，使用", "边界线必须被线要素覆盖，使用", "面边界线必须被其它面层边界线覆盖，使用", "包含点" };
        private esriTopologyRuleType[] AreaRuleType = new esriTopologyRuleType[] { esriTopologyRuleType.esriTRTAreaNoOverlap, esriTopologyRuleType.esriTRTAreaNoGaps, esriTopologyRuleType.esriTRTAreaNoOverlapArea, esriTopologyRuleType.esriTRTAreaCoveredByAreaClass, esriTopologyRuleType.esriTRTAreaAreaCoverEachOther, esriTopologyRuleType.esriTRTAreaCoveredByArea, esriTopologyRuleType.esriTRTAreaBoundaryCoveredByLine, esriTopologyRuleType.esriTRTAreaBoundaryCoveredByAreaBoundary, esriTopologyRuleType.esriTRTAreaContainPoint };
        private Button btnOK;
        private CheckEdit chkShowError;
        private IContainer components;
        private GroupBox groupBox1;
        private ImageComboBoxEdit imageComboBoxEdit1;
        private ImageList imageList1;
        private Label label2;
        private Label lblTopoInfo1;
        private Label lblTopoInfo2;
        private string[] LineRuleDes = new string[] { "不能重叠", "不能相交", "必须被线要素覆盖，使用", "不能重叠，与", "必须被面要素边界线覆盖，使用", "不能有悬挂点", "不能有伪节点", "不能自重叠", "不能自相交", "必须为单部分", "不能相交或内部相接", "线终点必须被点要素覆盖，用" };
        private esriTopologyRuleType[] LineRuleType = new esriTopologyRuleType[] { esriTopologyRuleType.esriTRTLineNoOverlap, esriTopologyRuleType.esriTRTLineNoIntersection, esriTopologyRuleType.esriTRTLineCoveredByLineClass, esriTopologyRuleType.esriTRTLineNoOverlapLine, esriTopologyRuleType.esriTRTLineCoveredByAreaBoundary, esriTopologyRuleType.esriTRTLineNoDangles, esriTopologyRuleType.esriTRTLineNoPseudos, esriTopologyRuleType.esriTRTLineNoSelfOverlap, esriTopologyRuleType.esriTRTLineNoSelfIntersect, esriTopologyRuleType.esriTRTLineNoMultipart, esriTopologyRuleType.esriTRTLineNoIntersectOrInteriorTouch, esriTopologyRuleType.esriTRTLineEndpointCoveredByPoint };
        private ITopologyRule m_pTopoRule = null;
        private int m_RuleType;
        private int m_RuleTypeIndex = -1;
        private string[] m_TopoInfo1 = new string[] { 
            "第一层中的点要素必须被第二层中的面要素的边界覆盖。", "点层中没有落在面层边界上的要素被视为错误。", "第一层中的点要素必须被第二层中的线要素的终点覆盖。", "点层中没有落在线的终点上的要素被视为错误。", "第一层的点要素必须被第二层中的线要素覆盖。", "任何没被线要素覆盖的点被视为错误。", "第一层的点要素必须完全位于第二层的面要素之内。", "任何没有位于面要素之内的点被视为错误。", "同一层中线要素不能与其他线重叠。", "任何重叠的线段被视为错误。", "同一线层中的线不能与其他线相交或重叠", "任何重叠的线段或相交点都被视为错误", "第一层的线段必须与第二层的线一致", "位于第一层内与第二层不一致的线段被视为错误。", "第一层中线不能与第二层的线重叠。", "任何第二层与第一层要素重叠的线段被视为错误。", 
            "第一层中的线要素必须被第二层中的面要素的边界覆盖。", "任何位于线层而不被面层边界覆盖的线段被视为错误。", "一条线必须与同层中其他线的终点相接。", "任何不与其他线相接的线的终点被视为错误。", "一条线必须与同层中多于一条的线在终点处相接。", "任何只有两条线相接的终点被视为错误", "一个层中的线要素不允许自重叠。", "任何自重叠的线段被视为错误。", "一个层中的要素不允许自相交或自重叠。", "任何自重叠的线段或自相交的点别视为错误。", "一个层中的线要素不允许超过一个部分。", "任何超过一个部分的线要素被视为错误。", "同一层的线必须在终点处与另外的线相接。", "任何重叠的线或相交的点被视为错误。", "第一层中线的终点必须被第二层中的点要素覆盖。", "任何没有被点要素覆盖的终点被视为错误。", 
            "同一层内面与面不能重叠。", "任何要素重叠的区域被视为错误", "同一层的面之间不能存在缝隙", "缝隙的边界被视为错误。", "第一层的面不能与第二层的面重叠。", "任何第二层与第一层要素重叠的区域被视为错误。", "第一层的面要素必须被第二层的面要素覆盖。", "任何第一层没有被第二层的要素覆盖的区域被视为错误。", "第一层的面要素必须和第二层的面要素相互覆盖。", "任意一层没有被另一层的要素覆盖的区域被视为错误。", "第一层的一个面要素必须完全包含在第二层的一个面要素内。", "任何位于第一层中没有被第二层中的一个要素完全包含的区域被视为错误。", "第一层中面要素的边界必须和第二层中的线要素覆盖。", "面要素中不与线要素相符的边界被视为错误。", "第一层中面要素的边界必须被第二层中的面要素边界覆盖。", "任何没有被第二层的面要素边界覆盖的第一层的面要素边界被为错误。", 
            "第一层的面要素必须至少包含第二层中的一个点要素。", "任何没有包含点要素的面要素被为错误。"
         };
        private esriTopologyRuleType m_Type;
        private string[] PointRuleDes = new string[] { "必须被面要素边界线覆盖，使用", "必须被线要素终点覆盖，使用", "点必须被线要素覆盖，使用", "点落在面要素内，使用" };
        private esriTopologyRuleType[] PointRuleType = new esriTopologyRuleType[] { esriTopologyRuleType.esriTRTPointCoveredByAreaBoundary, esriTopologyRuleType.esriTRTPointCoveredByLineEndpoint, esriTopologyRuleType.esriTRTPointCoveredByLine, esriTopologyRuleType.esriTRTPointProperlyInsideArea };
        private TextEdit txtRuleName;

        public frmRuleInfo()
        {
            this.InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmRule_Load(object sender, EventArgs e)
        {
            switch (this.m_RuleType)
            {
                case 0:
                    if (!this.chkShowError.Checked)
                    {
                        this.imageComboBoxEdit1.SelectedIndex = (2 * this.m_RuleTypeIndex) + 1;
                        break;
                    }
                    this.imageComboBoxEdit1.SelectedIndex = 2 * this.m_RuleTypeIndex;
                    break;

                case 1:
                    if (!this.chkShowError.Checked)
                    {
                        this.imageComboBoxEdit1.SelectedIndex = 9 + (2 * this.m_RuleTypeIndex);
                    }
                    else
                    {
                        this.imageComboBoxEdit1.SelectedIndex = 8 + (2 * this.m_RuleTypeIndex);
                    }
                    this.lblTopoInfo1.Text = this.m_TopoInfo1[(2 * this.m_RuleTypeIndex) + 8];
                    this.lblTopoInfo2.Text = this.m_TopoInfo1[(2 * this.m_RuleTypeIndex) + 9];
                    this.txtRuleName.Text = this.LineRuleDes[this.m_RuleTypeIndex];
                    return;

                case 2:
                    if (!this.chkShowError.Checked)
                    {
                        this.imageComboBoxEdit1.SelectedIndex = 0x21 + (2 * this.m_RuleTypeIndex);
                    }
                    else
                    {
                        this.imageComboBoxEdit1.SelectedIndex = 0x20 + (2 * this.m_RuleTypeIndex);
                    }
                    this.lblTopoInfo1.Text = this.m_TopoInfo1[(2 * this.m_RuleTypeIndex) + 0x20];
                    this.lblTopoInfo2.Text = this.m_TopoInfo1[(2 * this.m_RuleTypeIndex) + 0x21];
                    this.txtRuleName.Text = this.AreaRuleDes[this.m_RuleTypeIndex];
                    return;

                default:
                    return;
            }
            this.lblTopoInfo1.Text = this.m_TopoInfo1[2 * this.m_RuleTypeIndex];
            this.lblTopoInfo2.Text = this.m_TopoInfo1[(2 * this.m_RuleTypeIndex) + 1];
            this.txtRuleName.Text = this.PointRuleDes[this.m_RuleTypeIndex];
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRuleInfo));
            this.label2 = new Label();
            this.btnOK = new Button();
            this.imageList1 = new ImageList(this.components);
            this.groupBox1 = new GroupBox();
            this.lblTopoInfo2 = new Label();
            this.lblTopoInfo1 = new Label();
            this.chkShowError = new CheckEdit();
            this.imageComboBoxEdit1 = new ImageComboBoxEdit();
            this.txtRuleName = new TextEdit();
            this.groupBox1.SuspendLayout();
            this.chkShowError.Properties.BeginInit();
            this.imageComboBoxEdit1.Properties.BeginInit();
            this.txtRuleName.Properties.BeginInit();
            base.SuspendLayout();
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x10, 8);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "规则";
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0xd8, 0xd8);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x38, 0x18);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "关闭";
            this.imageList1.ImageStream = (ImageListStreamer) resources.GetObject("imageList1.ImageStream");
            this.imageList1.TransparentColor = Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            this.imageList1.Images.SetKeyName(4, "");
            this.imageList1.Images.SetKeyName(5, "");
            this.imageList1.Images.SetKeyName(6, "");
            this.imageList1.Images.SetKeyName(7, "");
            this.imageList1.Images.SetKeyName(8, "");
            this.imageList1.Images.SetKeyName(9, "");
            this.imageList1.Images.SetKeyName(10, "");
            this.imageList1.Images.SetKeyName(11, "");
            this.imageList1.Images.SetKeyName(12, "");
            this.imageList1.Images.SetKeyName(13, "");
            this.imageList1.Images.SetKeyName(14, "");
            this.imageList1.Images.SetKeyName(15, "");
            this.imageList1.Images.SetKeyName(0x10, "");
            this.imageList1.Images.SetKeyName(0x11, "");
            this.imageList1.Images.SetKeyName(0x12, "");
            this.imageList1.Images.SetKeyName(0x13, "");
            this.imageList1.Images.SetKeyName(20, "");
            this.imageList1.Images.SetKeyName(0x15, "");
            this.imageList1.Images.SetKeyName(0x16, "");
            this.imageList1.Images.SetKeyName(0x17, "");
            this.imageList1.Images.SetKeyName(0x18, "");
            this.imageList1.Images.SetKeyName(0x19, "");
            this.imageList1.Images.SetKeyName(0x1a, "");
            this.imageList1.Images.SetKeyName(0x1b, "");
            this.imageList1.Images.SetKeyName(0x1c, "");
            this.imageList1.Images.SetKeyName(0x1d, "");
            this.imageList1.Images.SetKeyName(30, "");
            this.imageList1.Images.SetKeyName(0x1f, "");
            this.imageList1.Images.SetKeyName(0x20, "");
            this.imageList1.Images.SetKeyName(0x21, "");
            this.imageList1.Images.SetKeyName(0x22, "");
            this.imageList1.Images.SetKeyName(0x23, "");
            this.imageList1.Images.SetKeyName(0x24, "");
            this.imageList1.Images.SetKeyName(0x25, "");
            this.imageList1.Images.SetKeyName(0x26, "");
            this.imageList1.Images.SetKeyName(0x27, "");
            this.imageList1.Images.SetKeyName(40, "");
            this.imageList1.Images.SetKeyName(0x29, "");
            this.imageList1.Images.SetKeyName(0x2a, "");
            this.imageList1.Images.SetKeyName(0x2b, "");
            this.imageList1.Images.SetKeyName(0x2c, "");
            this.imageList1.Images.SetKeyName(0x2d, "");
            this.imageList1.Images.SetKeyName(0x2e, "");
            this.imageList1.Images.SetKeyName(0x2f, "");
            this.imageList1.Images.SetKeyName(0x30, "");
            this.imageList1.Images.SetKeyName(0x31, "");
            this.groupBox1.Controls.Add(this.lblTopoInfo2);
            this.groupBox1.Controls.Add(this.lblTopoInfo1);
            this.groupBox1.Controls.Add(this.chkShowError);
            this.groupBox1.Controls.Add(this.imageComboBoxEdit1);
            this.groupBox1.Location = new Point(0x10, 0x20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x110, 0xb0);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "规则描述";
            this.lblTopoInfo2.Location = new Point(0x68, 0x68);
            this.lblTopoInfo2.Name = "lblTopoInfo2";
            this.lblTopoInfo2.Size = new Size(160, 0x30);
            this.lblTopoInfo2.TabIndex = 13;
            this.lblTopoInfo1.Location = new Point(0x68, 0x20);
            this.lblTopoInfo1.Name = "lblTopoInfo1";
            this.lblTopoInfo1.Size = new Size(160, 0x30);
            this.lblTopoInfo1.TabIndex = 12;
            this.chkShowError.EditValue = true;
            this.chkShowError.Location = new Point(8, 0x98);
            this.chkShowError.Name = "chkShowError";
            this.chkShowError.Properties.Caption = "显示错误";
            this.chkShowError.Size = new Size(0x4b, 0x13);
            this.chkShowError.TabIndex = 11;
            this.chkShowError.CheckedChanged += new EventHandler(this.chkShowError_CheckedChanged);
            this.imageComboBoxEdit1.EditValue = 0;
            this.imageComboBoxEdit1.Location = new Point(9, 0x18);
            this.imageComboBoxEdit1.Name = "imageComboBoxEdit1";
            this.imageComboBoxEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.imageComboBoxEdit1.Properties.Items.AddRange(new ImageComboBoxItem[] { 
                new ImageComboBoxItem("", 0, 0), new ImageComboBoxItem("", 1, 1), new ImageComboBoxItem("", 2, 2), new ImageComboBoxItem("", 3, 3), new ImageComboBoxItem("", 4, 4), new ImageComboBoxItem("", 5, 5), new ImageComboBoxItem("", 6, 6), new ImageComboBoxItem("", 7, 7), new ImageComboBoxItem("", 8, 8), new ImageComboBoxItem("", 9, 9), new ImageComboBoxItem("", 10, 10), new ImageComboBoxItem("", 11, 11), new ImageComboBoxItem("", 12, 12), new ImageComboBoxItem("", 13, 13), new ImageComboBoxItem("", 14, 14), new ImageComboBoxItem("", 15, 15), 
                new ImageComboBoxItem("", 0x10, 0x10), new ImageComboBoxItem("", 0x11, 0x11), new ImageComboBoxItem("", 0x12, 0x12), new ImageComboBoxItem("", 0x13, 0x13), new ImageComboBoxItem("", 20, 20), new ImageComboBoxItem("", 0x15, 0x15), new ImageComboBoxItem("", 0x16, 0x16), new ImageComboBoxItem("", 0x17, 0x17), new ImageComboBoxItem("", 0x18, 0x18), new ImageComboBoxItem("", 0x19, 0x19), new ImageComboBoxItem("", 0x1a, 0x1a), new ImageComboBoxItem("", 0x1b, 0x1b), new ImageComboBoxItem("", 0x1c, 0x1c), new ImageComboBoxItem("", 0x1d, 0x1d), new ImageComboBoxItem("", 30, 30), new ImageComboBoxItem("", 0x1f, 0x1f), 
                new ImageComboBoxItem("", 0x20, 0x20), new ImageComboBoxItem("", 0x21, 0x21), new ImageComboBoxItem("", 0x22, 0x22), new ImageComboBoxItem("", 0x23, 0x23), new ImageComboBoxItem("", 0x24, 0x24), new ImageComboBoxItem("", 0x25, 0x25), new ImageComboBoxItem("", 0x26, 0x26), new ImageComboBoxItem("", 0x27, 0x27), new ImageComboBoxItem("", 40, 40), new ImageComboBoxItem("", 0x29, 0x29), new ImageComboBoxItem("", 0x2a, 0x2a), new ImageComboBoxItem("", 0x2b, 0x2b), new ImageComboBoxItem("", 0x2c, 0x2c), new ImageComboBoxItem("", 0x2d, 0x2d), new ImageComboBoxItem("", 0x2e, 0x2e), new ImageComboBoxItem("", 0x2f, 0x2f), 
                new ImageComboBoxItem("", 0x30, 0x30), new ImageComboBoxItem("", 0x31, 0x31)
             });
            this.imageComboBoxEdit1.Properties.LargeImages = this.imageList1;
            this.imageComboBoxEdit1.Properties.ReadOnly = true;
            this.imageComboBoxEdit1.Properties.ShowDropDown = ShowDropDown.Never;
            this.imageComboBoxEdit1.Properties.SmallImages = this.imageList1;
            this.imageComboBoxEdit1.Size = new Size(0x58, 0x7a);
            this.imageComboBoxEdit1.TabIndex = 9;
            this.txtRuleName.EditValue = "";
            this.txtRuleName.Location = new Point(0x38, 5);
            this.txtRuleName.Name = "txtRuleName";
            this.txtRuleName.Properties.ReadOnly = true;
            this.txtRuleName.Size = new Size(0xe0, 0x15);
            this.txtRuleName.TabIndex = 10;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x128, 0xf5);
            base.Controls.Add(this.txtRuleName);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.label2);
            base.Icon = (Icon) resources.GetObject("$Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmRuleInfo";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "规则属性";
            base.Load += new EventHandler(this.frmRule_Load);
            this.groupBox1.ResumeLayout(false);
            this.chkShowError.Properties.EndInit();
            this.imageComboBoxEdit1.Properties.EndInit();
            this.txtRuleName.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public ITopologyRule TopologyRule
        {
            set
            {
                int num;
                this.m_pTopoRule = value;
                this.m_Type = this.m_pTopoRule.TopologyRuleType;
                for (num = 0; num < this.PointRuleType.Length; num++)
                {
                    if (this.m_Type == this.PointRuleType[num])
                    {
                        this.m_RuleType = 0;
                        this.m_RuleTypeIndex = num;
                        return;
                    }
                }
                for (num = 0; num < this.LineRuleType.Length; num++)
                {
                    if (this.m_Type == this.LineRuleType[num])
                    {
                        this.m_RuleType = 1;
                        this.m_RuleTypeIndex = num;
                        return;
                    }
                }
                for (num = 0; num < this.AreaRuleType.Length; num++)
                {
                    if (this.m_Type == this.AreaRuleType[num])
                    {
                        this.m_RuleType = 2;
                        this.m_RuleTypeIndex = num;
                        break;
                    }
                }
            }
        }
    }
}

