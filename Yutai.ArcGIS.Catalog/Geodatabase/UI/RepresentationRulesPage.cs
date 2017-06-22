using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal partial class RepresentationRulesPage : UserControl
    {
        private IContainer icontainer_0 = null;
        private IFeatureClass ifeatureClass_0 = null;
        private IRepresentationRules irepresentationRules_0 = null;
    //
        public RepresentationRulesPage()
        {
            this.InitializeComponent();
        }

        private void btnAddLayer_Click(object sender, EventArgs e)
        {
            IRepresentationRule repRule = this.method_1(this.ifeatureClass_0);
            this.irepresentationRules_0.Add(repRule);
            this.representationruleListBox1.Reset();
        }

        private void btnDeleteLayer_Click(object sender, EventArgs e)
        {
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.representationruleListBox1.SelectedIndex;
            IRepresentationRule selectRepresentationRule = this.representationruleListBox1.SelectRepresentationRule;
            int selectRepresentationRuleID = this.representationruleListBox1.SelectRepresentationRuleID;
            string selectRepresentationRuleName = this.representationruleListBox1.SelectRepresentationRuleName;
            this.irepresentationRules_0.set_Index(selectRepresentationRuleID, selectedIndex - 1);
            this.representationruleListBox1.Reset();
        }

        private void btnOther_Click(object sender, EventArgs e)
        {
            this.contextMenuStrip1.Show(this, this.btnOther.Right, this.btnOther.Top);
        }

 private void importSymbolToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

 private void loadRuleToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private IBasicSymbol method_0(IFeatureClass ifeatureClass_1)
        {
            IBasicSymbol symbol = null;
            if ((ifeatureClass_1.ShapeType == esriGeometryType.esriGeometryMultipoint) || (ifeatureClass_1.ShapeType == esriGeometryType.esriGeometryPoint))
            {
                return new BasicMarkerSymbolClass();
            }
            if (ifeatureClass_1.ShapeType == esriGeometryType.esriGeometryPolyline)
            {
                return new BasicLineSymbolClass();
            }
            if (ifeatureClass_1.ShapeType == esriGeometryType.esriGeometryPolygon)
            {
                symbol = new BasicFillSymbolClass();
            }
            return symbol;
        }

        private IRepresentationRule method_1(IFeatureClass ifeatureClass_1)
        {
            IBasicSymbol symbol = this.method_0(ifeatureClass_1);
            IRepresentationRule rule = new RepresentationRuleClass();
            rule.InsertLayer(0, symbol);
            return rule;
        }

        private void representationruleListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            IRepresentationRule selectRepresentationRule = this.representationruleListBox1.SelectRepresentationRule;
            if (selectRepresentationRule != null)
            {
                //this.represationRuleCtrl1.RepresentationRule = selectRepresentationRule;
                //this.represationRuleCtrl1.Init();
                this.btnDeleteLayer.Enabled = this.representationruleListBox1.Items.Count > 1;
                if (this.representationruleListBox1.Items.Count > 1)
                {
                    this.btnMoveUp.Enabled = this.representationruleListBox1.SelectedIndex != 0;
                    this.tnMoveDown.Enabled = this.representationruleListBox1.SelectedIndex != (this.representationruleListBox1.Items.Count - 1);
                }
                else
                {
                    this.btnMoveUp.Enabled = false;
                    this.tnMoveDown.Enabled = false;
                }
            }
            else
            {
                //this.represationRuleCtrl1.RepresentationRule = null;
                //this.represationRuleCtrl1.Init();
                this.btnDeleteLayer.Enabled = false;
                this.btnMoveUp.Enabled = false;
                this.tnMoveDown.Enabled = false;
            }
        }

        private void RepresentationRulesPage_Load(object sender, EventArgs e)
        {
            this.representationruleListBox1.Init(this.irepresentationRules_0);
            //this.represationRuleCtrl1.GeometryType_2 = this.ifeatureClass_0.ShapeType;
            //this.represationRuleCtrl1.CanEdit = false;
        }

        private void tnMoveDown_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.representationruleListBox1.SelectedIndex;
            IRepresentationRule selectRepresentationRule = this.representationruleListBox1.SelectRepresentationRule;
            int selectRepresentationRuleID = this.representationruleListBox1.SelectRepresentationRuleID;
            string selectRepresentationRuleName = this.representationruleListBox1.SelectRepresentationRuleName;
            this.irepresentationRules_0.set_Index(selectRepresentationRuleID, selectedIndex + 1);
            this.representationruleListBox1.Reset();
        }

        public IFeatureClass FeatureClass
        {
            set
            {
                this.ifeatureClass_0 = value;
            }
        }

        public IRepresentationRules RepresentationRules
        {
            set
            {
                this.irepresentationRules_0 = value;
            }
        }
    }
}

