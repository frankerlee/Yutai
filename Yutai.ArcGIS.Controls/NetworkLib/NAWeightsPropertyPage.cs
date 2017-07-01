using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Controls.NetworkLib
{
    internal partial class NAWeightsPropertyPage : UserControl
    {
        private bool m_CanDo = false;
        private bool m_IsDirty = false;

        public NAWeightsPropertyPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            if (this.m_IsDirty)
            {
                this.m_IsDirty = false;
                if (this.cbofromToEdgeWeight.SelectedIndex > 0)
                {
                    NetworkAnalyst.FromToEdgeWeight = (this.cbofromToEdgeWeight.SelectedItem as WeightWrap).NetWeight;
                }
                else
                {
                    NetworkAnalyst.FromToEdgeWeight = null;
                }
                if (this.cboToFromEdgeWeight.SelectedIndex > 0)
                {
                    NetworkAnalyst.ToFromEdgeWeight = (this.cboToFromEdgeWeight.SelectedItem as WeightWrap).NetWeight;
                }
                else
                {
                    NetworkAnalyst.ToFromEdgeWeight = null;
                }
                if ((this.cboToFromEdgeWeight.SelectedIndex > 0) && (this.cbofromToEdgeWeight.SelectedIndex > 0))
                {
                }
                if (this.cboJunWeight.SelectedIndex > 0)
                {
                    NetworkAnalyst.JunctionWeight = (this.cboJunWeight.SelectedItem as WeightWrap).NetWeight;
                }
                else
                {
                    NetworkAnalyst.JunctionWeight = null;
                }
            }
            return true;
        }

        private void cbofromToEdgeWeight_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.m_IsDirty = true;
        }

        private void cboJunWeight_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.m_IsDirty = true;
        }

        private void cboToFromEdgeWeight_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.m_IsDirty = true;
        }

        private void Init()
        {
            IEnumNetWeightAssociation association;
            INetWeightAssociation association2;
            this.m_CanDo = false;
            this.cboJunWeight.Properties.Items.Clear();
            this.cbofromToEdgeWeight.Properties.Items.Clear();
            this.cboToFromEdgeWeight.Properties.Items.Clear();
            this.cboJunWeight.Properties.Items.Add("<无>");
            this.cbofromToEdgeWeight.Properties.Items.Add("<无>");
            this.cboToFromEdgeWeight.Properties.Items.Add("<无>");
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            INetSchema network = NetworkAnalyst.m_pAnalystGN.Network as INetSchema;
            IEnumFeatureClass class2 =
                NetworkAnalyst.m_pAnalystGN.get_ClassesByType(esriFeatureType.esriFTSimpleJunction);
            class2.Reset();
            IFeatureClass class3 = class2.Next();
            IList list = new ArrayList();
            while (class3 != null)
            {
                association = network.get_WeightAssociationsByTableName(class3.AliasName);
                association.Reset();
                association2 = association.Next();
                while (association2 != null)
                {
                    if (list.IndexOf(association2.WeightID) == -1)
                    {
                        list.Add(association2.WeightID);
                        this.cboJunWeight.Properties.Items.Add(new WeightWrap(network.get_Weight(association2.WeightID)));
                        if ((NetworkAnalyst.JunctionWeight != null) &&
                            (association2.WeightID == NetworkAnalyst.JunctionWeight.WeightID))
                        {
                            num = this.cboJunWeight.Properties.Items.Count - 1;
                        }
                    }
                    association2 = association.Next();
                }
                class3 = class2.Next();
            }
            class2 = NetworkAnalyst.m_pAnalystGN.get_ClassesByType(esriFeatureType.esriFTComplexJunction);
            class2.Reset();
            for (class3 = class2.Next(); class3 != null; class3 = class2.Next())
            {
                association = network.get_WeightAssociationsByTableName(class3.AliasName);
                association.Reset();
                association2 = association.Next();
                while (association2 != null)
                {
                    if (list.IndexOf(association2.WeightID) == -1)
                    {
                        list.Add(association2.WeightID);
                        this.cboJunWeight.Properties.Items.Add(new WeightWrap(network.get_Weight(association2.WeightID)));
                        if ((NetworkAnalyst.JunctionWeight != null) &&
                            (association2.WeightID == NetworkAnalyst.JunctionWeight.WeightID))
                        {
                            num = this.cboJunWeight.Properties.Items.Count - 1;
                        }
                    }
                    association2 = association.Next();
                }
            }
            class2 = NetworkAnalyst.m_pAnalystGN.get_ClassesByType(esriFeatureType.esriFTSimpleEdge);
            class2.Reset();
            class3 = class2.Next();
            list.Clear();
            while (class3 != null)
            {
                association = network.get_WeightAssociationsByTableName(class3.AliasName);
                association.Reset();
                association2 = association.Next();
                while (association2 != null)
                {
                    if (list.IndexOf(association2.WeightID) == -1)
                    {
                        list.Add(association2.WeightID);
                        this.cbofromToEdgeWeight.Properties.Items.Add(
                            new WeightWrap(network.get_Weight(association2.WeightID)));
                        this.cboToFromEdgeWeight.Properties.Items.Add(
                            new WeightWrap(network.get_Weight(association2.WeightID)));
                        if ((NetworkAnalyst.FromToEdgeWeight != null) &&
                            (association2.WeightID == NetworkAnalyst.FromToEdgeWeight.WeightID))
                        {
                            num2 = this.cbofromToEdgeWeight.Properties.Items.Count - 1;
                        }
                        if ((NetworkAnalyst.ToFromEdgeWeight != null) &&
                            (association2.WeightID == NetworkAnalyst.ToFromEdgeWeight.WeightID))
                        {
                            num3 = this.cboToFromEdgeWeight.Properties.Items.Count - 1;
                        }
                    }
                    association2 = association.Next();
                }
                class3 = class2.Next();
            }
            class2 = NetworkAnalyst.m_pAnalystGN.get_ClassesByType(esriFeatureType.esriFTComplexEdge);
            class2.Reset();
            for (class3 = class2.Next(); class3 != null; class3 = class2.Next())
            {
                association = network.get_WeightAssociationsByTableName(class3.AliasName);
                association.Reset();
                for (association2 = association.Next(); association2 != null; association2 = association.Next())
                {
                    if (list.IndexOf(association2.WeightID) == -1)
                    {
                        list.Add(association2.WeightID);
                        this.cbofromToEdgeWeight.Properties.Items.Add(
                            new WeightWrap(network.get_Weight(association2.WeightID)));
                        this.cboToFromEdgeWeight.Properties.Items.Add(
                            new WeightWrap(network.get_Weight(association2.WeightID)));
                        if ((NetworkAnalyst.FromToEdgeWeight != null) &&
                            (association2.WeightID == NetworkAnalyst.FromToEdgeWeight.WeightID))
                        {
                            num2 = this.cbofromToEdgeWeight.Properties.Items.Count - 1;
                        }
                        if ((NetworkAnalyst.ToFromEdgeWeight != null) &&
                            (association2.WeightID == NetworkAnalyst.ToFromEdgeWeight.WeightID))
                        {
                            num3 = this.cboToFromEdgeWeight.Properties.Items.Count - 1;
                        }
                    }
                }
            }
            this.cboJunWeight.SelectedIndex = num;
            this.cbofromToEdgeWeight.SelectedIndex = num2;
            this.cboToFromEdgeWeight.SelectedIndex = num3;
            this.m_CanDo = true;
        }

        private void NAWeightsPropertyPage_Load(object sender, EventArgs e)
        {
            this.Init();
        }
    }
}