using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Wrapper;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmAddRuleByClass : Form
    {
        private esriTopologyRuleType[] esriTopologyRuleType_0 = new esriTopologyRuleType[]
        {
            esriTopologyRuleType.esriTRTLineNoOverlap, esriTopologyRuleType.esriTRTLineNoIntersection,
            esriTopologyRuleType.esriTRTLineNoDangles, esriTopologyRuleType.esriTRTLineNoPseudos,
            esriTopologyRuleType.esriTRTLineNoSelfOverlap, esriTopologyRuleType.esriTRTLineNoSelfIntersect,
            esriTopologyRuleType.esriTRTLineNoMultipart, esriTopologyRuleType.esriTRTLineNoIntersectOrInteriorTouch,
            esriTopologyRuleType.esriTRTAreaNoOverlap, esriTopologyRuleType.esriTRTAreaNoGaps
        };

        private ITopologyRule[] itopologyRule_0 = null;

        private string[] string_0 = new string[]
            {"线不能重叠", "线不能相交", "线不能有悬挂点", "线不能有伪节点", "线不能自重叠", "线不能自相交", "线必须为单部分", "线不能相交或内部相接", "面不能重叠", "面不能有缝隙"};

        private string[] string_1 = new string[]
        {
            "同一层中线要素不能与其他线重叠。", "任何重叠的线段被视为错误。", "同一线层中的线不能与其他线相交或重叠", "任何重叠的线段或相交点都被视为错误", "一条线必须与同层中其他线的终点相接。",
            "任何不与其他线相接的线的终点被视为错误。", "一条线必须与同层中多于一条的线在终点处相接。", "任何只有两条线相接的终点被视为错误", "一个层中的线要素不允许自重叠。", "任何自重叠的线段被视为错误。",
            "一个层中的要素不允许自相交或自重叠。", "任何自重叠的线段或自相交的点别视为错误。", "一个层中的线要素不允许超过一个部分。", "任何超过一个部分的线要素被视为错误。",
            "同一层的线必须在终点处与另外的线相接。", "任何重叠的线或相交的点被视为错误。",
            "同一层内面与面不能重叠。", "任何要素重叠的区域被视为错误", "同一层的面之间不能存在缝隙", "缝隙的边界被视为错误。"
        };

        public frmAddRuleByClass()
        {
            this.InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.checkedListBox1.CheckedItems.Count > 0)
            {
                this.itopologyRule_0 = new ITopologyRule[this.checkedListBox1.CheckedItems.Count];
                for (int i = 0; i < this.checkedListBox1.CheckedItems.Count; i++)
                {
                    ObjectWrap wrap = this.checkedListBox1.CheckedItems[i] as ObjectWrap;
                    this.itopologyRule_0[i] = new TopologyRuleClass
                    {
                        OriginClassID = ((IFeatureClass) wrap.Object).ObjectClassID,
                        AllOriginSubtypes = true,
                        TopologyRuleType = this.esriTopologyRuleType_0[this.comboRule.SelectedIndex],
                        Name = this.string_0[this.comboRule.SelectedIndex]
                    };
                }
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

        private void comboRule_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.checkedListBox1.Items.Clear();
            if (this.comboRule.SelectedIndex != -1)
            {
                int num;
                IFeatureClass class2;
                if (this.comboRule.SelectedIndex < 8)
                {
                    for (num = 0; num < this.iarray_0.Count; num++)
                    {
                        class2 = (IFeatureClass) this.iarray_0.get_Element(num);
                        if (class2.ShapeType == esriGeometryType.esriGeometryPolyline)
                        {
                            this.checkedListBox1.Items.Add(new ObjectWrap(class2));
                        }
                    }
                }
                else
                {
                    for (num = 0; num < this.iarray_0.Count; num++)
                    {
                        class2 = (IFeatureClass) this.iarray_0.get_Element(num);
                        if (class2.ShapeType == esriGeometryType.esriGeometryPolygon)
                        {
                            this.checkedListBox1.Items.Add(new ObjectWrap(class2));
                        }
                    }
                }
                this.lblTopoInfo1.Text = this.string_1[2*this.comboRule.SelectedIndex];
                this.lblTopoInfo2.Text = this.string_1[(2*this.comboRule.SelectedIndex) + 1];
                if (this.chkShowError.Checked)
                {
                    this.imageComboBoxEdit1.SelectedIndex = 2*this.comboRule.SelectedIndex;
                }
                else
                {
                    this.imageComboBoxEdit1.SelectedIndex = (2*this.comboRule.SelectedIndex) + 1;
                }
            }
        }

        private void frmAddRuleByClass_Load(object sender, EventArgs e)
        {
            foreach (string str in this.string_0)
            {
                this.comboRule.Items.Add(str);
            }
            this.comboRule.SelectedIndex = 0;
        }

        public bool bVaildRule
        {
            get { return this.bool_0; }
        }

        public IArray OriginClassArray
        {
            set { this.iarray_0 = value; }
        }

        public ITopologyRule[] TopologyRules
        {
            get { return this.itopologyRule_0; }
        }
    }
}