using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal partial class GeometryNewConnectivityPropertyPage : UserControl
    {
        private Container container_0 = null;
        private IGeometricNetwork igeometricNetwork_0 = null;

        public GeometryNewConnectivityPropertyPage()
        {
            this.InitializeComponent();
        }

        private void cboConnectivityRule_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.listView1.Items.Clear();
            if (this.cboConnectivityRule.SelectedIndex != -1)
            {
                ListViewItem item;
                ISubtypes featureClass =
                    (this.cboConnectivityRule.SelectedItem as FeatureClassWrap).FeatureClass as ISubtypes;
                string[] items = new string[2];
                if (featureClass.HasSubtype)
                {
                    int num;
                    IEnumSubtype subtypes = featureClass.Subtypes;
                    subtypes.Reset();
                    for (string str = subtypes.Next(out num); str != null; str = subtypes.Next(out num))
                    {
                        items[0] = str;
                        items[1] = num.ToString();
                        item = new ListViewItem(items)
                        {
                            Tag = num
                        };
                        this.listView1.Items.Add(item);
                    }
                }
                else
                {
                    items[0] = this.cboConnectivityRule.Text;
                    items[1] = "0";
                    item = new ListViewItem(items)
                    {
                        Tag = 0
                    };
                    this.listView1.Items.Add(item);
                }
            }
        }

        private void GeometryNewConnectivityPropertyPage_Load(object sender, EventArgs e)
        {
            IFeatureClass class3;
            IEnumFeatureClass class2 = this.igeometricNetwork_0.get_ClassesByType(esriFeatureType.esriFTSimpleJunction);
            class2.Reset();
            for (class3 = class2.Next(); class3 != null; class3 = class2.Next())
            {
                this.cboConnectivityRule.Properties.Items.Add(new FeatureClassWrap(class3));
            }
            class2 = this.igeometricNetwork_0.get_ClassesByType(esriFeatureType.esriFTComplexJunction);
            class2.Reset();
            for (class3 = class2.Next(); class3 != null; class3 = class2.Next())
            {
                this.cboConnectivityRule.Properties.Items.Add(new FeatureClassWrap(class3));
            }
            class2 = this.igeometricNetwork_0.get_ClassesByType(esriFeatureType.esriFTSimpleEdge);
            class2.Reset();
            for (class3 = class2.Next(); class3 != null; class3 = class2.Next())
            {
                this.cboConnectivityRule.Properties.Items.Add(new FeatureClassWrap(class3));
            }
            class2 = this.igeometricNetwork_0.get_ClassesByType(esriFeatureType.esriFTComplexEdge);
            class2.Reset();
            for (class3 = class2.Next(); class3 != null; class3 = class2.Next())
            {
                this.cboConnectivityRule.Properties.Items.Add(new FeatureClassWrap(class3));
            }
            this.method_0(this.igeometricNetwork_0);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView1.SelectedIndices.Count != 1)
            {
            }
        }

        private void method_0(IGeometricNetwork igeometricNetwork_1)
        {
            try
            {
                IFeatureClassContainer featureDataset = (IFeatureClassContainer) igeometricNetwork_1.FeatureDataset;
                IEnumRule rules = igeometricNetwork_1.Rules;
                rules.Reset();
                for (IRule rule2 = rules.Next(); rule2 != null; rule2 = rules.Next())
                {
                    if (rule2 is IConnectivityRule)
                    {
                        IConnectivityRule rule3 = (IConnectivityRule) rule2;
                        if (rule3.Category == -1)
                        {
                            IJunctionConnectivityRule2 rule4 = (IJunctionConnectivityRule2) rule3;
                            featureDataset.get_ClassByID(rule4.EdgeClassID);
                            featureDataset.get_ClassByID(rule4.JunctionClassID);
                        }
                        else if (rule3.Type == esriRuleType.esriRTEdgeConnectivity)
                        {
                            IEdgeConnectivityRule rule5 = (IEdgeConnectivityRule) rule3;
                            featureDataset.get_ClassByID(rule5.FromEdgeClassID);
                            featureDataset.get_ClassByID(rule5.ToEdgeClassID);
                            featureDataset.get_ClassByID(rule5.DefaultJunctionClassID);
                            for (int i = 0; i < rule5.JunctionCount; i++)
                            {
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        public IGeometricNetwork GeometricNetwork
        {
            set { this.igeometricNetwork_0 = value; }
        }

        internal partial class ConnectivityRuleWrap
        {
            private IConnectivityRule iconnectivityRule_0 = null;

            public ConnectivityRuleWrap(IConnectivityRule iconnectivityRule_1)
            {
                this.iconnectivityRule_0 = iconnectivityRule_1;
            }

            public override string ToString()
            {
                return this.iconnectivityRule_0.Helpstring;
            }

            public IConnectivityRule ConnectivityRule
            {
                get { return this.iconnectivityRule_0; }
            }
        }

        internal partial class FeatureClassWrap
        {
            private IFeatureClass ifeatureClass_0 = null;

            public FeatureClassWrap(IFeatureClass ifeatureClass_1)
            {
                this.ifeatureClass_0 = ifeatureClass_1;
            }

            public override string ToString()
            {
                return this.ifeatureClass_0.AliasName;
            }

            public IFeatureClass FeatureClass
            {
                get { return this.ifeatureClass_0; }
            }
        }
    }
}