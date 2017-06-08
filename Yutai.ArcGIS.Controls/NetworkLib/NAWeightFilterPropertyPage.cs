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
    internal class NAWeightFilterPropertyPage : UserControl
    {
        private SimpleButton btnValiateEFWRange;
        private SimpleButton btnValiteJFWRange;
        private ComboBoxEdit cbofromToEdgeWeight;
        private ComboBoxEdit cboJunWeight;
        private ComboBoxEdit cboToFromEdgeWeight;
        private CheckEdit chkEdgeapplyNot;
        private CheckEdit chkJunapplyNot;
        private Container components = null;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private bool m_CanDo = false;
        private bool m_IsDirty = false;
        private bool m_TestOK = false;
        private TextEdit txtEFWRange;
        private TextEdit txtJFWRange;

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

        private void InitializeComponent()
        {
            this.groupBox2 = new GroupBox();
            this.btnValiateEFWRange = new SimpleButton();
            this.chkEdgeapplyNot = new CheckEdit();
            this.txtEFWRange = new TextEdit();
            this.label5 = new Label();
            this.label3 = new Label();
            this.cboToFromEdgeWeight = new ComboBoxEdit();
            this.cbofromToEdgeWeight = new ComboBoxEdit();
            this.label2 = new Label();
            this.groupBox3 = new GroupBox();
            this.btnValiteJFWRange = new SimpleButton();
            this.chkJunapplyNot = new CheckEdit();
            this.txtJFWRange = new TextEdit();
            this.label1 = new Label();
            this.cboJunWeight = new ComboBoxEdit();
            this.label4 = new Label();
            this.groupBox2.SuspendLayout();
            this.chkEdgeapplyNot.Properties.BeginInit();
            this.txtEFWRange.Properties.BeginInit();
            this.cboToFromEdgeWeight.Properties.BeginInit();
            this.cbofromToEdgeWeight.Properties.BeginInit();
            this.groupBox3.SuspendLayout();
            this.chkJunapplyNot.Properties.BeginInit();
            this.txtJFWRange.Properties.BeginInit();
            this.cboJunWeight.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox2.Controls.Add(this.btnValiateEFWRange);
            this.groupBox2.Controls.Add(this.chkEdgeapplyNot);
            this.groupBox2.Controls.Add(this.txtEFWRange);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cboToFromEdgeWeight);
            this.groupBox2.Controls.Add(this.cbofromToEdgeWeight);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(0x10, 0x90);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0xf8, 0x90);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "边权重过滤器";
            this.btnValiateEFWRange.Location = new System.Drawing.Point(0xa8, 0x70);
            this.btnValiateEFWRange.Name = "btnValiateEFWRange";
            this.btnValiateEFWRange.Size = new Size(0x40, 0x18);
            this.btnValiateEFWRange.TabIndex = 12;
            this.btnValiateEFWRange.Text = "验证";
            this.chkEdgeapplyNot.Location = new System.Drawing.Point(0x70, 0x70);
            this.chkEdgeapplyNot.Name = "chkEdgeapplyNot";
            this.chkEdgeapplyNot.Properties.Caption = "否";
            this.chkEdgeapplyNot.Size = new Size(0x30, 0x13);
            this.chkEdgeapplyNot.TabIndex = 9;
            this.chkEdgeapplyNot.CheckedChanged += new EventHandler(this.chkEdgeapplyNot_CheckedChanged);
            this.txtEFWRange.EditValue = "";
            this.txtEFWRange.Location = new System.Drawing.Point(0x70, 80);
            this.txtEFWRange.Name = "txtEFWRange";
            this.txtEFWRange.Size = new Size(120, 0x17);
            this.txtEFWRange.TabIndex = 8;
            this.txtEFWRange.EditValueChanged += new EventHandler(this.txtEFWRange_EditValueChanged);
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0x10, 80);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x36, 0x11);
            this.label5.TabIndex = 7;
            this.label5.Text = "权重范围";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0x10, 0x30);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x55, 0x11);
            this.label3.TabIndex = 6;
            this.label3.Text = "终点-起点权重";
            this.cboToFromEdgeWeight.EditValue = "";
            this.cboToFromEdgeWeight.Location = new System.Drawing.Point(0x70, 0x30);
            this.cboToFromEdgeWeight.Name = "cboToFromEdgeWeight";
            this.cboToFromEdgeWeight.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboToFromEdgeWeight.Size = new Size(120, 0x17);
            this.cboToFromEdgeWeight.TabIndex = 5;
            this.cboToFromEdgeWeight.SelectedIndexChanged += new EventHandler(this.cboToFromEdgeWeight_SelectedIndexChanged);
            this.cbofromToEdgeWeight.EditValue = "";
            this.cbofromToEdgeWeight.Location = new System.Drawing.Point(0x70, 0x10);
            this.cbofromToEdgeWeight.Name = "cbofromToEdgeWeight";
            this.cbofromToEdgeWeight.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cbofromToEdgeWeight.Size = new Size(120, 0x17);
            this.cbofromToEdgeWeight.TabIndex = 3;
            this.cbofromToEdgeWeight.SelectedIndexChanged += new EventHandler(this.cbofromToEdgeWeight_SelectedIndexChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 20);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x55, 0x11);
            this.label2.TabIndex = 2;
            this.label2.Text = "起点-终点权重";
            this.groupBox3.Controls.Add(this.btnValiteJFWRange);
            this.groupBox3.Controls.Add(this.chkJunapplyNot);
            this.groupBox3.Controls.Add(this.txtJFWRange);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.cboJunWeight);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(0x10, 0x10);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0xf8, 120);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "连接点权重过滤器";
            this.btnValiteJFWRange.Location = new System.Drawing.Point(0x98, 80);
            this.btnValiteJFWRange.Name = "btnValiteJFWRange";
            this.btnValiteJFWRange.Size = new Size(0x40, 0x18);
            this.btnValiteJFWRange.TabIndex = 11;
            this.btnValiteJFWRange.Text = "验证";
            this.chkJunapplyNot.Location = new System.Drawing.Point(0x58, 0x58);
            this.chkJunapplyNot.Name = "chkJunapplyNot";
            this.chkJunapplyNot.Properties.Caption = "否";
            this.chkJunapplyNot.Size = new Size(0x30, 0x13);
            this.chkJunapplyNot.TabIndex = 10;
            this.chkJunapplyNot.CheckedChanged += new EventHandler(this.chkJunapplyNot_CheckedChanged);
            this.txtJFWRange.EditValue = "";
            this.txtJFWRange.Location = new System.Drawing.Point(0x58, 0x30);
            this.txtJFWRange.Name = "txtJFWRange";
            this.txtJFWRange.Size = new Size(0x90, 0x17);
            this.txtJFWRange.TabIndex = 3;
            this.txtJFWRange.EditValueChanged += new EventHandler(this.txtJFWRange_EditValueChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0x10, 0x38);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x36, 0x11);
            this.label1.TabIndex = 2;
            this.label1.Text = "权重范围";
            this.cboJunWeight.EditValue = "";
            this.cboJunWeight.Location = new System.Drawing.Point(0x58, 0x12);
            this.cboJunWeight.Name = "cboJunWeight";
            this.cboJunWeight.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboJunWeight.Size = new Size(0x90, 0x17);
            this.cboJunWeight.TabIndex = 1;
            this.cboJunWeight.SelectedIndexChanged += new EventHandler(this.cboJunWeight_SelectedIndexChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0x10, 0x18);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x42, 0x11);
            this.label4.TabIndex = 0;
            this.label4.Text = "连接点权重";
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox3);
            base.Name = "NAWeightFilterPropertyPage";
            base.Size = new Size(0x138, 0x138);
            base.Load += new EventHandler(this.NAWeightFilterPropertyPage_Load);
            this.groupBox2.ResumeLayout(false);
            this.chkEdgeapplyNot.Properties.EndInit();
            this.txtEFWRange.Properties.EndInit();
            this.cboToFromEdgeWeight.Properties.EndInit();
            this.cbofromToEdgeWeight.Properties.EndInit();
            this.groupBox3.ResumeLayout(false);
            this.chkJunapplyNot.Properties.EndInit();
            this.txtJFWRange.Properties.EndInit();
            this.cboJunWeight.Properties.EndInit();
            base.ResumeLayout(false);
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

