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
    internal partial class NAWeightFilterPropertyPage : UserControl
    {
        private bool m_CanDo = false;
        private bool m_IsDirty = false;
        private bool m_TestOK = false;

        public NAWeightFilterPropertyPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            if (this.m_IsDirty)
            {
                string[] strArray;
                object[] objArray;
                object[] objArray2;
                double num;
                int num2;
                string[] strArray2;
                if (this.cbofromToEdgeWeight.SelectedIndex > 0)
                {
                    NetworkAnalyst.FromToEdgeFilterWeight = (this.cbofromToEdgeWeight.SelectedItem as WeightWrap).NetWeight;
                }
                else
                {
                    NetworkAnalyst.FromToEdgeFilterWeight = null;
                }
                if (this.cboToFromEdgeWeight.SelectedIndex > 0)
                {
                    NetworkAnalyst.ToFromEdgeFilterWeight = (this.cboToFromEdgeWeight.SelectedItem as WeightWrap).NetWeight;
                }
                else
                {
                    NetworkAnalyst.ToFromEdgeFilterWeight = null;
                }
                if ((this.cboToFromEdgeWeight.SelectedIndex > 0) && (this.cbofromToEdgeWeight.SelectedIndex > 0))
                {
                }
                if (this.cboJunWeight.SelectedIndex > 0)
                {
                    NetworkAnalyst.JunctionFilterWeight = (this.cboJunWeight.SelectedItem as WeightWrap).NetWeight;
                }
                else
                {
                    NetworkAnalyst.JunctionFilterWeight = null;
                }
                NetworkAnalyst.ApplyJuncFilterWeight = this.chkJunapplyNot.Checked;
                NetworkAnalyst.ApplyEdgeFilterWeight = this.chkEdgeapplyNot.Checked;
                if (this.txtJFWRange.Text.Trim().Length == 0)
                {
                    NetworkAnalyst.JuncfromValues = null;
                    NetworkAnalyst.JunctoValues = null;
                }
                else
                {
                    strArray = this.txtJFWRange.Text.Trim().Split(new char[] { ',' });
                    objArray = new object[strArray.Length];
                    objArray2 = new object[strArray.Length];
                    try
                    {
                        for (num2 = 0; num2 < strArray.Length; num2++)
                        {
                            strArray2 = strArray[num2].Trim().Split(new char[] { '-' });
                            if (strArray2.Length > 2)
                            {
                                MessageBox.Show("权重域输入错误，请检查!");
                                return false;
                            }
                            if (strArray2.Length == 1)
                            {
                                try
                                {
                                    num = double.Parse(strArray2[0]);
                                    objArray[num2] = num;
                                    objArray2[num2] = num;
                                }
                                catch
                                {
                                    MessageBox.Show("权重域输入错误，请检查!");
                                    return false;
                                }
                            }
                            else if (strArray2.Length == 2)
                            {
                                try
                                {
                                    num = double.Parse(strArray2[0]);
                                    objArray[num2] = num;
                                    num = double.Parse(strArray2[1]);
                                    objArray2[num2] = num;
                                }
                                catch
                                {
                                    MessageBox.Show("权重域输入错误，请检查!");
                                    return false;
                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                    NetworkAnalyst.JuncfromValues = objArray;
                    NetworkAnalyst.JunctoValues = objArray2;
                }
                if (this.txtEFWRange.Text.Trim().Length == 0)
                {
                    NetworkAnalyst.EdgefromValues = null;
                    NetworkAnalyst.EdgetoValues = null;
                }
                else
                {
                    strArray = this.txtEFWRange.Text.Trim().Split(new char[] { ',' });
                    objArray = new object[strArray.Length];
                    objArray2 = new object[strArray.Length];
                    try
                    {
                        for (num2 = 0; num2 < strArray.Length; num2++)
                        {
                            strArray2 = strArray[num2].Trim().Split(new char[] { '-' });
                            if (strArray2.Length > 2)
                            {
                                MessageBox.Show("权重域输入错误，请检查!");
                                return false;
                            }
                            if (strArray2.Length == 1)
                            {
                                try
                                {
                                    num = double.Parse(strArray2[0]);
                                    objArray[num2] = num;
                                    objArray2[num2] = num;
                                }
                                catch
                                {
                                    MessageBox.Show("权重域输入错误，请检查!");
                                    return false;
                                }
                            }
                            else if (strArray2.Length == 2)
                            {
                                try
                                {
                                    num = double.Parse(strArray2[0]);
                                    objArray[num2] = num;
                                    objArray2[num2] = double.Parse(strArray2[1]);
                                }
                                catch
                                {
                                    MessageBox.Show("权重域输入错误，请检查!");
                                    return false;
                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                    NetworkAnalyst.EdgefromValues = objArray;
                    NetworkAnalyst.EdgetoValues = objArray2;
                }
                this.m_IsDirty = false;
            }
            return true;
        }

        private void cbofromToEdgeWeight_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((this.cbofromToEdgeWeight.SelectedIndex > 0) && (this.cboToFromEdgeWeight.SelectedIndex > 0))
            {
                this.btnValiateEFWRange.Enabled = true;
                this.txtEFWRange.Enabled = true;
            }
            else
            {
                this.btnValiateEFWRange.Enabled = false;
                this.txtEFWRange.Enabled = false;
            }
            this.m_IsDirty = true;
        }

        private void cboJunWeight_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboJunWeight.SelectedIndex > 0)
            {
                this.btnValiteJFWRange.Enabled = true;
                this.txtJFWRange.Enabled = true;
            }
            else
            {
                this.btnValiteJFWRange.Enabled = false;
                this.txtJFWRange.Enabled = false;
            }
        }

        private void cboToFromEdgeWeight_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((this.cbofromToEdgeWeight.SelectedIndex > 0) && (this.cboToFromEdgeWeight.SelectedIndex > 0))
            {
                this.btnValiateEFWRange.Enabled = true;
                this.txtEFWRange.Enabled = true;
            }
            else
            {
                this.btnValiateEFWRange.Enabled = false;
                this.txtEFWRange.Enabled = false;
            }
            this.m_IsDirty = true;
        }

        private void chkEdgeapplyNot_CheckedChanged(object sender, EventArgs e)
        {
            this.m_IsDirty = true;
        }

        private void chkJunapplyNot_CheckedChanged(object sender, EventArgs e)
        {
            this.m_IsDirty = true;
        }

 private void Init()
        {
            IEnumNetWeightAssociation association;
            INetWeightAssociation association2;
            string str;
            int num4;
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
            IEnumFeatureClass class2 = NetworkAnalyst.m_pAnalystGN.get_ClassesByType(esriFeatureType.esriFTSimpleJunction);
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
                        if ((NetworkAnalyst.JunctionFilterWeight != null) && (association2.WeightID == NetworkAnalyst.JunctionFilterWeight.WeightID))
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
                        if ((NetworkAnalyst.JunctionFilterWeight != null) && (association2.WeightID == NetworkAnalyst.JunctionFilterWeight.WeightID))
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
                        this.cbofromToEdgeWeight.Properties.Items.Add(new WeightWrap(network.get_Weight(association2.WeightID)));
                        this.cboToFromEdgeWeight.Properties.Items.Add(new WeightWrap(network.get_Weight(association2.WeightID)));
                        if ((NetworkAnalyst.FromToEdgeFilterWeight != null) && (association2.WeightID == NetworkAnalyst.FromToEdgeFilterWeight.WeightID))
                        {
                            num2 = this.cbofromToEdgeWeight.Properties.Items.Count - 1;
                        }
                        if ((NetworkAnalyst.ToFromEdgeFilterWeight != null) && (association2.WeightID == NetworkAnalyst.ToFromEdgeFilterWeight.WeightID))
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
                        this.cbofromToEdgeWeight.Properties.Items.Add(new WeightWrap(network.get_Weight(association2.WeightID)));
                        this.cboToFromEdgeWeight.Properties.Items.Add(new WeightWrap(network.get_Weight(association2.WeightID)));
                        if ((NetworkAnalyst.FromToEdgeFilterWeight != null) && (association2.WeightID == NetworkAnalyst.FromToEdgeFilterWeight.WeightID))
                        {
                            num2 = this.cbofromToEdgeWeight.Properties.Items.Count - 1;
                        }
                        if ((NetworkAnalyst.ToFromEdgeFilterWeight != null) && (association2.WeightID == NetworkAnalyst.ToFromEdgeFilterWeight.WeightID))
                        {
                            num3 = this.cboToFromEdgeWeight.Properties.Items.Count - 1;
                        }
                    }
                }
            }
            this.chkJunapplyNot.Checked = NetworkAnalyst.ApplyJuncFilterWeight;
            this.chkEdgeapplyNot.Checked = NetworkAnalyst.ApplyEdgeFilterWeight;
            this.cboJunWeight.SelectedIndex = num;
            this.cbofromToEdgeWeight.SelectedIndex = num2;
            this.cboToFromEdgeWeight.SelectedIndex = num3;
            if (num == 0)
            {
                this.btnValiteJFWRange.Enabled = false;
                this.txtJFWRange.Enabled = false;
            }
            if ((num2 == 0) || (num3 == 0))
            {
                this.btnValiateEFWRange.Enabled = false;
                this.txtEFWRange.Enabled = false;
            }
            if (NetworkAnalyst.JuncfromValues != null)
            {
                if (NetworkAnalyst.JuncfromValues[0] == NetworkAnalyst.JunctoValues[0])
                {
                    str = NetworkAnalyst.JuncfromValues[0].ToString();
                }
                else
                {
                    str = NetworkAnalyst.JuncfromValues[0].ToString() + " - " + NetworkAnalyst.JunctoValues[0].ToString();
                }
                for (num4 = 1; num4 < NetworkAnalyst.JuncfromValues.Length; num4++)
                {
                    if (NetworkAnalyst.JuncfromValues[num4] == NetworkAnalyst.JunctoValues[num4])
                    {
                        str = str + " , " + NetworkAnalyst.JuncfromValues[0].ToString();
                    }
                    else
                    {
                        str = str + " , " + NetworkAnalyst.JuncfromValues[0].ToString() + " - " + NetworkAnalyst.JunctoValues[0].ToString();
                    }
                }
                this.txtJFWRange.Text = str;
            }
            else
            {
                this.txtJFWRange.Text = "";
            }
            if (NetworkAnalyst.EdgefromValues != null)
            {
                if (NetworkAnalyst.EdgefromValues[0] == NetworkAnalyst.EdgetoValues[0])
                {
                    str = NetworkAnalyst.EdgefromValues[0].ToString();
                }
                else
                {
                    str = NetworkAnalyst.EdgefromValues[0].ToString() + " - " + NetworkAnalyst.EdgetoValues[0].ToString();
                }
                for (num4 = 1; num4 < NetworkAnalyst.JuncfromValues.Length; num4++)
                {
                    if (NetworkAnalyst.EdgefromValues[num4] == NetworkAnalyst.EdgetoValues[num4])
                    {
                        str = str + " , " + NetworkAnalyst.EdgefromValues[0].ToString();
                    }
                    else
                    {
                        str = str + " , " + NetworkAnalyst.EdgefromValues[0].ToString() + " - " + NetworkAnalyst.EdgetoValues[0].ToString();
                    }
                }
                this.txtEFWRange.Text = str;
            }
            else
            {
                this.txtEFWRange.Text = "";
            }
            this.m_CanDo = true;
        }

 private void NAWeightFilterPropertyPage_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private void txtEFWRange_EditValueChanged(object sender, EventArgs e)
        {
            this.m_IsDirty = true;
        }

        private void txtJFWRange_EditValueChanged(object sender, EventArgs e)
        {
            this.m_IsDirty = true;
        }
    }
}

