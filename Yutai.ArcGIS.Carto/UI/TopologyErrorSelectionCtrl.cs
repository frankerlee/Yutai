using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ItemCheckEventArgs = DevExpress.XtraEditors.Controls.ItemCheckEventArgs;
using ItemCheckEventHandler = DevExpress.XtraEditors.Controls.ItemCheckEventHandler;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class TopologyErrorSelectionCtrl : UserControl, ILayerAndStandaloneTablePropertyPage
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Container container_0 = null;
        private ITopologyErrorSelection itopologyErrorSelection_0 = null;

        public TopologyErrorSelectionCtrl()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                for (int i = 0; i < this.itopologyErrorSelection_0.RuleTypeCount; i++)
                {
                    CheckedListBoxItem item = this.checkedListBoxControl1.Items[i];
                    this.itopologyErrorSelection_0.set_Selectable(i, item.CheckState == CheckState.Checked);
                }
                this.itopologyErrorSelection_0.SelectErrors = this.chkSelectErrors.Checked;
                this.itopologyErrorSelection_0.SelectExceptions = this.chkSelectExceptions.Checked;
            }
            return true;
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.checkedListBoxControl1.Items.Count; i++)
            {
                CheckedListBoxItem item = this.checkedListBoxControl1.Items[i];
                item.CheckState = CheckState.Unchecked;
            }
            this.bool_1 = false;
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.checkedListBoxControl1.Items.Count; i++)
            {
                CheckedListBoxItem item = this.checkedListBoxControl1.Items[i];
                item.CheckState = CheckState.Checked;
            }
            this.bool_1 = false;
        }

        private void checkedListBoxControl1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = false;
            }
        }

        private void chkSelectErrors_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = false;
            }
        }

        private void chkSelectExceptions_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = false;
            }
        }

        private string method_0(esriTopologyRuleType esriTopologyRuleType_0)
        {
            switch (esriTopologyRuleType_0)
            {
                case esriTopologyRuleType.esriTRTAny:
                    return "所有错误";

                case esriTopologyRuleType.esriTRTFeatureLargerThanClusterTolerance:
                    return "必需大于集束容限值";

                case esriTopologyRuleType.esriTRTAreaNoGaps:
                    return "面不能有缝隙";

                case esriTopologyRuleType.esriTRTAreaNoOverlap:
                    return "面不能重叠";

                case esriTopologyRuleType.esriTRTAreaCoveredByAreaClass:
                    return "面必须被面要素类覆盖";

                case esriTopologyRuleType.esriTRTAreaAreaCoverEachOther:
                    return "面必须和其它面要素层相互覆盖";

                case esriTopologyRuleType.esriTRTAreaCoveredByArea:
                    return "面必须被面覆盖";

                case esriTopologyRuleType.esriTRTAreaNoOverlapArea:
                    return "面不能与其他面层重叠";

                case esriTopologyRuleType.esriTRTLineCoveredByAreaBoundary:
                    return "线必须被面要素边界线覆盖";

                case esriTopologyRuleType.esriTRTPointCoveredByAreaBoundary:
                    return "点必须被面要素边界线覆盖";

                case esriTopologyRuleType.esriTRTPointProperlyInsideArea:
                    return "点落在面要素内";

                case esriTopologyRuleType.esriTRTLineNoOverlap:
                    return "线不能重叠";

                case esriTopologyRuleType.esriTRTLineNoIntersection:
                    return "线不能相交";

                case esriTopologyRuleType.esriTRTLineNoDangles:
                    return "线不能有悬挂点";

                case esriTopologyRuleType.esriTRTLineNoPseudos:
                    return "线不能有伪节点";

                case esriTopologyRuleType.esriTRTLineCoveredByLineClass:
                    return "线必须被线要素覆盖";

                case esriTopologyRuleType.esriTRTLineNoOverlapLine:
                    return "线与线不能重叠";

                case esriTopologyRuleType.esriTRTPointCoveredByLine:
                    return "点必须被线要素覆盖";

                case esriTopologyRuleType.esriTRTPointCoveredByLineEndpoint:
                    return "点必须被线要素终点覆盖";

                case esriTopologyRuleType.esriTRTAreaBoundaryCoveredByLine:
                    return "面边界线必须被线要素覆盖";

                case esriTopologyRuleType.esriTRTAreaBoundaryCoveredByAreaBoundary:
                    return "面边界线必须被其它面层边界线覆盖";

                case esriTopologyRuleType.esriTRTLineNoSelfOverlap:
                    return "线不能自重叠";

                case esriTopologyRuleType.esriTRTLineNoSelfIntersect:
                    return "线不能自相交";

                case esriTopologyRuleType.esriTRTLineNoIntersectOrInteriorTouch:
                    return "线不能相交或内部相接";

                case esriTopologyRuleType.esriTRTLineEndpointCoveredByPoint:
                    return "线终点必须被点要素覆盖";

                case esriTopologyRuleType.esriTRTAreaContainPoint:
                    return "面包含点";

                case esriTopologyRuleType.esriTRTLineNoMultipart:
                    return "线必须为单部分";
            }
            return "所有错误";
        }

        private void TopologyErrorSelectionCtrl_Load(object sender, EventArgs e)
        {
            this.chkSelectErrors.Checked = this.itopologyErrorSelection_0.SelectErrors;
            this.chkSelectExceptions.Checked = this.itopologyErrorSelection_0.SelectExceptions;
            this.checkedListBoxControl1.Items.Clear();
            for (int i = 0; i < this.itopologyErrorSelection_0.RuleTypeCount; i++)
            {
                string str = this.method_0(this.itopologyErrorSelection_0.get_RuleType(i));
                bool isChecked = this.itopologyErrorSelection_0.get_Selectable(i);
                this.checkedListBoxControl1.Items.Add(str, isChecked);
            }
            this.bool_0 = true;
        }

        public IBasicMap FocusMap
        {
            set { }
        }

        public bool IsPageDirty
        {
            get { return this.bool_1; }
        }

        public object SelectItem
        {
            set { this.itopologyErrorSelection_0 = value as ITopologyErrorSelection; }
        }
    }
}