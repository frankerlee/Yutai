using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    public partial class frmRuleInfo : Form
    {
        private string[] AreaRuleDes = new string[]
        {
            "不能重叠", "不能有缝隙", "不能与其他面层重叠，使用", "必须被面要素类覆盖，使用", "必须和其它面要素层相互覆盖，使用", "必须被覆盖，使用", "边界线必须被线要素覆盖，使用",
            "面边界线必须被其它面层边界线覆盖，使用", "包含点"
        };

        private esriTopologyRuleType[] AreaRuleType = new esriTopologyRuleType[]
        {
            esriTopologyRuleType.esriTRTAreaNoOverlap, esriTopologyRuleType.esriTRTAreaNoGaps,
            esriTopologyRuleType.esriTRTAreaNoOverlapArea, esriTopologyRuleType.esriTRTAreaCoveredByAreaClass,
            esriTopologyRuleType.esriTRTAreaAreaCoverEachOther, esriTopologyRuleType.esriTRTAreaCoveredByArea,
            esriTopologyRuleType.esriTRTAreaBoundaryCoveredByLine,
            esriTopologyRuleType.esriTRTAreaBoundaryCoveredByAreaBoundary, esriTopologyRuleType.esriTRTAreaContainPoint
        };

        private string[] LineRuleDes = new string[]
        {
            "不能重叠", "不能相交", "必须被线要素覆盖，使用", "不能重叠，与", "必须被面要素边界线覆盖，使用", "不能有悬挂点", "不能有伪节点", "不能自重叠", "不能自相交", "必须为单部分",
            "不能相交或内部相接", "线终点必须被点要素覆盖，用"
        };

        private esriTopologyRuleType[] LineRuleType = new esriTopologyRuleType[]
        {
            esriTopologyRuleType.esriTRTLineNoOverlap, esriTopologyRuleType.esriTRTLineNoIntersection,
            esriTopologyRuleType.esriTRTLineCoveredByLineClass, esriTopologyRuleType.esriTRTLineNoOverlapLine,
            esriTopologyRuleType.esriTRTLineCoveredByAreaBoundary, esriTopologyRuleType.esriTRTLineNoDangles,
            esriTopologyRuleType.esriTRTLineNoPseudos, esriTopologyRuleType.esriTRTLineNoSelfOverlap,
            esriTopologyRuleType.esriTRTLineNoSelfIntersect, esriTopologyRuleType.esriTRTLineNoMultipart,
            esriTopologyRuleType.esriTRTLineNoIntersectOrInteriorTouch,
            esriTopologyRuleType.esriTRTLineEndpointCoveredByPoint
        };

        private ITopologyRule m_pTopoRule = null;
        private int m_RuleTypeIndex = -1;

        private string[] m_TopoInfo1 = new string[]
        {
            "第一层中的点要素必须被第二层中的面要素的边界覆盖。", "点层中没有落在面层边界上的要素被视为错误。", "第一层中的点要素必须被第二层中的线要素的终点覆盖。", "点层中没有落在线的终点上的要素被视为错误。",
            "第一层的点要素必须被第二层中的线要素覆盖。", "任何没被线要素覆盖的点被视为错误。", "第一层的点要素必须完全位于第二层的面要素之内。", "任何没有位于面要素之内的点被视为错误。",
            "同一层中线要素不能与其他线重叠。", "任何重叠的线段被视为错误。", "同一线层中的线不能与其他线相交或重叠", "任何重叠的线段或相交点都被视为错误", "第一层的线段必须与第二层的线一致",
            "位于第一层内与第二层不一致的线段被视为错误。", "第一层中线不能与第二层的线重叠。", "任何第二层与第一层要素重叠的线段被视为错误。",
            "第一层中的线要素必须被第二层中的面要素的边界覆盖。", "任何位于线层而不被面层边界覆盖的线段被视为错误。", "一条线必须与同层中其他线的终点相接。", "任何不与其他线相接的线的终点被视为错误。",
            "一条线必须与同层中多于一条的线在终点处相接。", "任何只有两条线相接的终点被视为错误", "一个层中的线要素不允许自重叠。", "任何自重叠的线段被视为错误。", "一个层中的要素不允许自相交或自重叠。",
            "任何自重叠的线段或自相交的点别视为错误。", "一个层中的线要素不允许超过一个部分。", "任何超过一个部分的线要素被视为错误。", "同一层的线必须在终点处与另外的线相接。",
            "任何重叠的线或相交的点被视为错误。", "第一层中线的终点必须被第二层中的点要素覆盖。", "任何没有被点要素覆盖的终点被视为错误。",
            "同一层内面与面不能重叠。", "任何要素重叠的区域被视为错误", "同一层的面之间不能存在缝隙", "缝隙的边界被视为错误。", "第一层的面不能与第二层的面重叠。",
            "任何第二层与第一层要素重叠的区域被视为错误。", "第一层的面要素必须被第二层的面要素覆盖。", "任何第一层没有被第二层的要素覆盖的区域被视为错误。", "第一层的面要素必须和第二层的面要素相互覆盖。",
            "任意一层没有被另一层的要素覆盖的区域被视为错误。", "第一层的一个面要素必须完全包含在第二层的一个面要素内。", "任何位于第一层中没有被第二层中的一个要素完全包含的区域被视为错误。",
            "第一层中面要素的边界必须和第二层中的线要素覆盖。", "面要素中不与线要素相符的边界被视为错误。", "第一层中面要素的边界必须被第二层中的面要素边界覆盖。",
            "任何没有被第二层的面要素边界覆盖的第一层的面要素边界被为错误。",
            "第一层的面要素必须至少包含第二层中的一个点要素。", "任何没有包含点要素的面要素被为错误。"
        };

        private string[] PointRuleDes = new string[] {"必须被面要素边界线覆盖，使用", "必须被线要素终点覆盖，使用", "点必须被线要素覆盖，使用", "点落在面要素内，使用"};

        private esriTopologyRuleType[] PointRuleType = new esriTopologyRuleType[]
        {
            esriTopologyRuleType.esriTRTPointCoveredByAreaBoundary,
            esriTopologyRuleType.esriTRTPointCoveredByLineEndpoint, esriTopologyRuleType.esriTRTPointCoveredByLine,
            esriTopologyRuleType.esriTRTPointProperlyInsideArea
        };

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

        private void frmRule_Load(object sender, EventArgs e)
        {
            switch (this.m_RuleType)
            {
                case 0:
                    if (!this.chkShowError.Checked)
                    {
                        this.imageComboBoxEdit1.SelectedIndex = (2*this.m_RuleTypeIndex) + 1;
                        break;
                    }
                    this.imageComboBoxEdit1.SelectedIndex = 2*this.m_RuleTypeIndex;
                    break;

                case 1:
                    if (!this.chkShowError.Checked)
                    {
                        this.imageComboBoxEdit1.SelectedIndex = 9 + (2*this.m_RuleTypeIndex);
                    }
                    else
                    {
                        this.imageComboBoxEdit1.SelectedIndex = 8 + (2*this.m_RuleTypeIndex);
                    }
                    this.lblTopoInfo1.Text = this.m_TopoInfo1[(2*this.m_RuleTypeIndex) + 8];
                    this.lblTopoInfo2.Text = this.m_TopoInfo1[(2*this.m_RuleTypeIndex) + 9];
                    this.txtRuleName.Text = this.LineRuleDes[this.m_RuleTypeIndex];
                    return;

                case 2:
                    if (!this.chkShowError.Checked)
                    {
                        this.imageComboBoxEdit1.SelectedIndex = 33 + (2*this.m_RuleTypeIndex);
                    }
                    else
                    {
                        this.imageComboBoxEdit1.SelectedIndex = 32 + (2*this.m_RuleTypeIndex);
                    }
                    this.lblTopoInfo1.Text = this.m_TopoInfo1[(2*this.m_RuleTypeIndex) + 32];
                    this.lblTopoInfo2.Text = this.m_TopoInfo1[(2*this.m_RuleTypeIndex) + 33];
                    this.txtRuleName.Text = this.AreaRuleDes[this.m_RuleTypeIndex];
                    return;

                default:
                    return;
            }
            this.lblTopoInfo1.Text = this.m_TopoInfo1[2*this.m_RuleTypeIndex];
            this.lblTopoInfo2.Text = this.m_TopoInfo1[(2*this.m_RuleTypeIndex) + 1];
            this.txtRuleName.Text = this.PointRuleDes[this.m_RuleTypeIndex];
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