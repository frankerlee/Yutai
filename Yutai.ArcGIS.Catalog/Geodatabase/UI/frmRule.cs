using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmRule : Form
    {
        private esriTopologyRuleType[] esriTopologyRuleType_0 = new esriTopologyRuleType[]
        {
            esriTopologyRuleType.esriTRTPointCoveredByAreaBoundary,
            esriTopologyRuleType.esriTRTPointCoveredByLineEndpoint, esriTopologyRuleType.esriTRTPointCoveredByLine,
            esriTopologyRuleType.esriTRTPointProperlyInsideArea
        };

        private esriTopologyRuleType[] esriTopologyRuleType_1 = new esriTopologyRuleType[]
        {
            esriTopologyRuleType.esriTRTLineNoOverlap, esriTopologyRuleType.esriTRTLineNoIntersection,
            esriTopologyRuleType.esriTRTLineCoveredByLineClass, esriTopologyRuleType.esriTRTLineNoOverlapLine,
            esriTopologyRuleType.esriTRTLineCoveredByAreaBoundary, esriTopologyRuleType.esriTRTLineNoDangles,
            esriTopologyRuleType.esriTRTLineNoPseudos, esriTopologyRuleType.esriTRTLineNoSelfOverlap,
            esriTopologyRuleType.esriTRTLineNoSelfIntersect, esriTopologyRuleType.esriTRTLineNoMultipart,
            esriTopologyRuleType.esriTRTLineNoIntersectOrInteriorTouch,
            esriTopologyRuleType.esriTRTLineEndpointCoveredByPoint
        };

        private esriTopologyRuleType[] esriTopologyRuleType_2 = new esriTopologyRuleType[]
        {
            esriTopologyRuleType.esriTRTAreaNoOverlap, esriTopologyRuleType.esriTRTAreaNoGaps,
            esriTopologyRuleType.esriTRTAreaNoOverlapArea, esriTopologyRuleType.esriTRTAreaCoveredByAreaClass,
            esriTopologyRuleType.esriTRTAreaAreaCoverEachOther, esriTopologyRuleType.esriTRTAreaCoveredByArea,
            esriTopologyRuleType.esriTRTAreaBoundaryCoveredByLine,
            esriTopologyRuleType.esriTRTAreaBoundaryCoveredByAreaBoundary, esriTopologyRuleType.esriTRTAreaContainPoint
        };

        private IArray iarray_1 = new ArrayClass();
        private ITopologyRule itopologyRule_0 = new TopologyRuleClass();
        private string[] string_0 = new string[] {"必须被面要素边界线覆盖，使用", "必须被线要素终点覆盖，使用", "点必须被线要素覆盖，使用", "点落在面要素内，使用"};

        private string[] string_1 = new string[]
        {
            "不能重叠", "不能相交", "必须被线要素覆盖，使用", "不能重叠，与", "必须被面要素边界线覆盖，使用", "不能有悬挂点", "不能有伪节点", "不能自重叠", "不能自相交", "必须为单部分",
            "不能相交或内部相接", "线终点必须被点要素覆盖，用"
        };

        private string[] string_2 = new string[]
        {
            "不能重叠", "不能有缝隙", "不能与其他面层重叠，使用", "必须被面要素类覆盖，使用", "必须和其它面要素层相互覆盖，使用", "必须被覆盖，使用", "边界线必须被线要素覆盖，使用",
            "面边界线必须被其它面层边界线覆盖，使用", "包含点"
        };

        private string[] string_3 = new string[]
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
            this.itopologyRule_0.OriginClassID =
                ((IFeatureClass) this.iarray_0.get_Element(this.comboOriginClass.SelectedIndex)).ObjectClassID;
            this.itopologyRule_0.AllOriginSubtypes = true;
            this.bool_0 = true;
            if (this.comboDestClass.Items.Count > 0)
            {
                if (this.itopologyRule_0.OriginClassID ==
                    ((IFeatureClass) this.iarray_1.get_Element(this.comboDestClass.SelectedIndex)).ObjectClassID)
                {
                    this.bool_0 = false;
                    base.Close();
                    return;
                }
                this.itopologyRule_0.DestinationClassID =
                    ((IFeatureClass) this.iarray_1.get_Element(this.comboDestClass.SelectedIndex)).ObjectClassID;
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
            else if ((class2.ShapeType == esriGeometryType.esriGeometryPolyline) ||
                     (class2.ShapeType == esriGeometryType.esriGeometryLine))
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
                if ((this.esriTopologyRuleType_0[this.comboRule.SelectedIndex] ==
                     esriTopologyRuleType.esriTRTPointCoveredByAreaBoundary) ||
                    (this.esriTopologyRuleType_0[this.comboRule.SelectedIndex] ==
                     esriTopologyRuleType.esriTRTPointProperlyInsideArea))
                {
                    this.method_1(3);
                }
                else if ((this.esriTopologyRuleType_0[this.comboRule.SelectedIndex] ==
                          esriTopologyRuleType.esriTRTPointCoveredByLine) ||
                         (this.esriTopologyRuleType_0[this.comboRule.SelectedIndex] ==
                          esriTopologyRuleType.esriTRTPointCoveredByLineEndpoint))
                {
                    this.method_1(2);
                }
                if (this.chkShowError.Checked)
                {
                    this.imageComboBoxEdit1.SelectedIndex = 2*this.comboRule.SelectedIndex;
                }
                else
                {
                    this.imageComboBoxEdit1.SelectedIndex = (2*this.comboRule.SelectedIndex) + 1;
                }
                this.lblTopoInfo1.Text = this.string_3[2*this.comboRule.SelectedIndex];
                this.lblTopoInfo2.Text = this.string_3[(2*this.comboRule.SelectedIndex) + 1];
            }
            else if (this.int_0 == 2)
            {
                if (this.esriTopologyRuleType_1[this.comboRule.SelectedIndex] ==
                    esriTopologyRuleType.esriTRTLineCoveredByAreaBoundary)
                {
                    this.method_1(3);
                }
                else if ((this.esriTopologyRuleType_1[this.comboRule.SelectedIndex] ==
                          esriTopologyRuleType.esriTRTLineCoveredByLineClass) ||
                         (this.esriTopologyRuleType_1[this.comboRule.SelectedIndex] ==
                          esriTopologyRuleType.esriTRTLineNoOverlapLine))
                {
                    this.method_1(2);
                }
                else if (this.esriTopologyRuleType_1[this.comboRule.SelectedIndex] ==
                         esriTopologyRuleType.esriTRTLineEndpointCoveredByPoint)
                {
                    this.method_1(1);
                }
                if (this.chkShowError.Checked)
                {
                    this.imageComboBoxEdit1.SelectedIndex = 8 + (2*this.comboRule.SelectedIndex);
                }
                else
                {
                    this.imageComboBoxEdit1.SelectedIndex = 9 + (2*this.comboRule.SelectedIndex);
                }
                this.lblTopoInfo1.Text = this.string_3[(2*this.comboRule.SelectedIndex) + 8];
                this.lblTopoInfo2.Text = this.string_3[(2*this.comboRule.SelectedIndex) + 9];
            }
            else if (this.int_0 == 3)
            {
                if ((((this.esriTopologyRuleType_2[this.comboRule.SelectedIndex] ==
                       esriTopologyRuleType.esriTRTAreaAreaCoverEachOther) ||
                      (this.esriTopologyRuleType_2[this.comboRule.SelectedIndex] ==
                       esriTopologyRuleType.esriTRTAreaBoundaryCoveredByAreaBoundary)) ||
                     ((this.esriTopologyRuleType_2[this.comboRule.SelectedIndex] ==
                       esriTopologyRuleType.esriTRTAreaCoveredByArea) ||
                      (this.esriTopologyRuleType_2[this.comboRule.SelectedIndex] ==
                       esriTopologyRuleType.esriTRTAreaCoveredByAreaClass))) ||
                    (this.esriTopologyRuleType_2[this.comboRule.SelectedIndex] ==
                     esriTopologyRuleType.esriTRTAreaNoOverlapArea))
                {
                    this.method_1(3);
                }
                else if (this.esriTopologyRuleType_2[this.comboRule.SelectedIndex] ==
                         esriTopologyRuleType.esriTRTAreaBoundaryCoveredByLine)
                {
                    this.method_1(2);
                }
                else if (this.esriTopologyRuleType_2[this.comboRule.SelectedIndex] ==
                         esriTopologyRuleType.esriTRTAreaContainPoint)
                {
                    this.method_1(1);
                }
                if (this.chkShowError.Checked)
                {
                    this.imageComboBoxEdit1.SelectedIndex = 32 + (2*this.comboRule.SelectedIndex);
                }
                else
                {
                    this.imageComboBoxEdit1.SelectedIndex = 33 + (2*this.comboRule.SelectedIndex);
                }
                this.lblTopoInfo1.Text = this.string_3[(2*this.comboRule.SelectedIndex) + 32];
                this.lblTopoInfo2.Text = this.string_3[(2*this.comboRule.SelectedIndex) + 33];
            }
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
                else if ((int_1 == 2) &&
                         ((unk.ShapeType == esriGeometryType.esriGeometryPolyline) ||
                          (unk.ShapeType == esriGeometryType.esriGeometryLine)))
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
            get { return this.bool_0; }
        }

        public IArray OriginClassArray
        {
            set { this.iarray_0 = value; }
        }

        public ITopologyRule TopologyRule
        {
            get { return this.itopologyRule_0; }
        }
    }
}