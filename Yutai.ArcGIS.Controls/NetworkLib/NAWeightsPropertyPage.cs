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
    internal class NAWeightsPropertyPage : UserControl
    {
        private ComboBoxEdit cbofromToEdgeWeight;
        private ComboBoxEdit cboJunWeight;
        private ComboBoxEdit cboToFromEdgeWeight;
        private Container components = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label2;
        private Label label3;
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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
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
                        if ((NetworkAnalyst.JunctionWeight != null) && (association2.WeightID == NetworkAnalyst.JunctionWeight.WeightID))
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
                        if ((NetworkAnalyst.JunctionWeight != null) && (association2.WeightID == NetworkAnalyst.JunctionWeight.WeightID))
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
                        if ((NetworkAnalyst.FromToEdgeWeight != null) && (association2.WeightID == NetworkAnalyst.FromToEdgeWeight.WeightID))
                        {
                            num2 = this.cbofromToEdgeWeight.Properties.Items.Count - 1;
                        }
                        if ((NetworkAnalyst.ToFromEdgeWeight != null) && (association2.WeightID == NetworkAnalyst.ToFromEdgeWeight.WeightID))
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
                        if ((NetworkAnalyst.FromToEdgeWeight != null) && (association2.WeightID == NetworkAnalyst.FromToEdgeWeight.WeightID))
                        {
                            num2 = this.cbofromToEdgeWeight.Properties.Items.Count - 1;
                        }
                        if ((NetworkAnalyst.ToFromEdgeWeight != null) && (association2.WeightID == NetworkAnalyst.ToFromEdgeWeight.WeightID))
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

        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.cboJunWeight = new ComboBoxEdit();
            this.label1 = new Label();
            this.groupBox2 = new GroupBox();
            this.label3 = new Label();
            this.cboToFromEdgeWeight = new ComboBoxEdit();
            this.cbofromToEdgeWeight = new ComboBoxEdit();
            this.label2 = new Label();
            this.groupBox1.SuspendLayout();
            this.cboJunWeight.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.cboToFromEdgeWeight.Properties.BeginInit();
            this.cbofromToEdgeWeight.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.cboJunWeight);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(0x10, 0x18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xf8, 0x58);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "连接点权重";
            this.cboJunWeight.EditValue = "";
            this.cboJunWeight.Location = new System.Drawing.Point(0x10, 0x30);
            this.cboJunWeight.Name = "cboJunWeight";
            this.cboJunWeight.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboJunWeight.Size = new Size(0xd0, 0x17);
            this.cboJunWeight.TabIndex = 1;
            this.cboJunWeight.SelectedIndexChanged += new EventHandler(this.cboJunWeight_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0x10, 0x18);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x67, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "连接点要素的权重";
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cboToFromEdgeWeight);
            this.groupBox2.Controls.Add(this.cbofromToEdgeWeight);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(0x10, 120);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0xf8, 0x98);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "边权重";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0x18, 80);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x55, 0x11);
            this.label3.TabIndex = 6;
            this.label3.Text = "终点-起点权重";
            this.cboToFromEdgeWeight.EditValue = "";
            this.cboToFromEdgeWeight.Location = new System.Drawing.Point(20, 0x68);
            this.cboToFromEdgeWeight.Name = "cboToFromEdgeWeight";
            this.cboToFromEdgeWeight.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboToFromEdgeWeight.Size = new Size(0xd0, 0x17);
            this.cboToFromEdgeWeight.TabIndex = 5;
            this.cboToFromEdgeWeight.SelectedIndexChanged += new EventHandler(this.cboToFromEdgeWeight_SelectedIndexChanged);
            this.cbofromToEdgeWeight.EditValue = "";
            this.cbofromToEdgeWeight.Location = new System.Drawing.Point(20, 0x30);
            this.cbofromToEdgeWeight.Name = "cbofromToEdgeWeight";
            this.cbofromToEdgeWeight.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cbofromToEdgeWeight.Size = new Size(0xd0, 0x17);
            this.cbofromToEdgeWeight.TabIndex = 3;
            this.cbofromToEdgeWeight.SelectedIndexChanged += new EventHandler(this.cbofromToEdgeWeight_SelectedIndexChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 20);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x55, 0x11);
            this.label2.TabIndex = 2;
            this.label2.Text = "起点-终点权重";
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "NAWeightsPropertyPage";
            base.Size = new Size(0x138, 0x150);
            base.Load += new EventHandler(this.NAWeightsPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.cboJunWeight.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.cboToFromEdgeWeight.Properties.EndInit();
            this.cbofromToEdgeWeight.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void NAWeightsPropertyPage_Load(object sender, EventArgs e)
        {
            this.Init();
        }
    }
}

