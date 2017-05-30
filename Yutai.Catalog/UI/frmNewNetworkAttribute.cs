namespace Yutai.Catalog.UI
{
    using ESRI.ArcGIS.Geodatabase;
  
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class frmNewNetworkAttribute : Form
    {
        private Button btnOK;
        private ComboBox cboDataType;
        private ComboBox cboUnit;
        private ComboBox cboUsageType;
        private IContainer icontainer_0 = null;
        private INetworkAttribute inetworkAttribute_0 = null;
        private Label labelControl1;
        private Label labelControl2;
        private Label labelControl3;
        private Label labelControl4;
        private Button Button2;
        private Button txtName;

        public frmNewNetworkAttribute()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtName.Text.Length != 0)
            {
                this.inetworkAttribute_0 = new EvaluatedNetworkAttribute();
                this.inetworkAttribute_0.Name = this.txtName.Text.Trim();
                this.inetworkAttribute_0.UsageType = (esriNetworkAttributeUsageType) this.cboUsageType.SelectedIndex;
                this.inetworkAttribute_0.Units = this.method_1();
                this.inetworkAttribute_0.DataType = this.method_2();
                base.DialogResult = DialogResult.OK;
            }
        }

        private void cboUsageType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.method_0();
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmNewNetworkAttribute_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

        private void InitializeComponent()
        {
            this.labelControl1 = new Label();
            this.txtName = new Button();
            this.labelControl2 = new Label();
            this.labelControl3 = new Label();
            this.labelControl4 = new Label();
            this.cboUsageType = new ComboBox();
            this.cboUnit = new ComboBox();
            this.cboDataType = new ComboBox();
            this.btnOK = new Button();
            this.Button2 = new Button();
           
            base.SuspendLayout();
            this.labelControl1.Location = new Point(12, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new Size(0x1c, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "名字:";
            this.txtName.Location = new Point(0x48, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(0x8d, 0x15);
            this.txtName.TabIndex = 1;
            this.txtName.TextChanged += new EventHandler(this.txtName_EditValueChanged);
            this.labelControl2.Location = new Point(12, 0x30);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new Size(0x30, 14);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "使用方式";
            this.labelControl3.Location = new Point(12, 0x52);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new Size(0x18, 14);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "单位";
            this.labelControl4.Location = new Point(12, 0x7a);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new Size(0x30, 14);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "数据类型";
            this.cboUsageType.Text = "Cost";
            this.cboUsageType.Location = new Point(0x48, 0x30);
            this.cboUsageType.Name = "cboUsageType";
            
            this.cboUsageType.Items.AddRange(new object[] { "Cost", "Descriptor", "Restriction", "Hierarchy" });
            this.cboUsageType.Size = new Size(0x8d, 0x15);
            this.cboUsageType.TabIndex = 5;
            this.cboUsageType.SelectedIndexChanged += new EventHandler(this.cboUsageType_SelectedIndexChanged);
            this.cboUnit.Text = "未知单位";
            this.cboUnit.Location = new Point(0x48, 0x52);
            this.cboUnit.Name = "cboUnit";
          
            this.cboUnit.Items.AddRange(new object[] { "未知单位", "英寸", "英尺", "码", "英里", "海里", "毫米", "厘米", "米", "公里", "十进制度", "分米", "秒", "分钟", "小时", "天" });
            this.cboUnit.Size = new Size(0x8d, 0x15);
            this.cboUnit.TabIndex = 6;
            this.cboDataType.Location = new Point(0x48, 0x73);
            this.cboDataType.Name = "cboDataType";
            
            this.cboDataType.Size = new Size(0x8d, 0x15);
            this.cboDataType.TabIndex = 7;
            this.btnOK.Enabled = false;
            this.btnOK.Location = new Point(0x3b, 0x99);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.Button2.DialogResult = DialogResult.Cancel;
            this.Button2.Location = new Point(0xa3, 0x98);
            this.Button2.Name = "Button2";
            this.Button2.Size = new Size(0x4b, 0x17);
            this.Button2.TabIndex = 9;
            this.Button2.Text = "取消";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x10f, 0xb9);
            base.Controls.Add(this.Button2);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.cboDataType);
            base.Controls.Add(this.cboUnit);
            base.Controls.Add(this.cboUsageType);
            base.Controls.Add(this.labelControl4);
            base.Controls.Add(this.labelControl3);
            base.Controls.Add(this.labelControl2);
            base.Controls.Add(this.txtName);
            base.Controls.Add(this.labelControl1);
            base.Name = "frmNewNetworkAttribute";
            this.Text = "新建网络属性";
            base.Load += new EventHandler(this.frmNewNetworkAttribute_Load);
         
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void method_0()
        {
            this.cboDataType.Items.Clear();
            if (this.cboUsageType.SelectedIndex == 0)
            {
                this.cboDataType.Items.AddRange(new object[] { "Integer", "Float", "Double" });
            }
            else if (this.cboUsageType.SelectedIndex == 1)
            {
                this.cboDataType.Items.AddRange(new object[] { "Integer", "Float", "Double", "Boolean" });
            }
            else if (this.cboUsageType.SelectedIndex == 2)
            {
                this.cboDataType.Items.AddRange(new object[] { "Boolean" });
            }
            else if (this.cboUsageType.SelectedIndex == 3)
            {
                this.cboDataType.Items.AddRange(new object[] { "Integer" });
            }
            if (this.cboDataType.Items.Count > 0)
            {
                this.cboDataType.SelectedIndex = 0;
            }
        }

        private esriNetworkAttributeUnits method_1()
        {
            if ((this.cboUnit.SelectedIndex >= 0) && (this.cboUnit.SelectedIndex <= 1))
            {
                return (esriNetworkAttributeUnits) this.cboUnit.SelectedIndex;
            }
            if ((this.cboUnit.SelectedIndex >= 2) && (this.cboUnit.SelectedIndex <= 11))
            {
                return (esriNetworkAttributeUnits) (this.cboUnit.SelectedIndex + 1);
            }
            if ((this.cboUnit.SelectedIndex >= 12) && (this.cboUnit.SelectedIndex <= 15))
            {
                return (esriNetworkAttributeUnits) (this.cboUnit.SelectedIndex + 8);
            }
            return esriNetworkAttributeUnits.esriNAUUnknown;
        }

        private esriNetworkAttributeDataType method_2()
        {
            if (this.cboUsageType.SelectedIndex == 0)
            {
                switch (this.cboDataType.SelectedIndex)
                {
                    case 0:
                        return esriNetworkAttributeDataType.esriNADTInteger;

                    case 1:
                        return esriNetworkAttributeDataType.esriNADTFloat;

                    case 2:
                        return esriNetworkAttributeDataType.esriNADTDouble;
                }
            }
            else if (this.cboUsageType.SelectedIndex == 1)
            {
                this.cboDataType.Items.AddRange(new object[] { "Integer", "Float", "Double", "Boolean" });
                switch (this.cboDataType.SelectedIndex)
                {
                    case 0:
                        return esriNetworkAttributeDataType.esriNADTInteger;

                    case 1:
                        return esriNetworkAttributeDataType.esriNADTFloat;

                    case 2:
                        return esriNetworkAttributeDataType.esriNADTDouble;

                    case 3:
                        return esriNetworkAttributeDataType.esriNADTBoolean;
                }
            }
            else if (this.cboUsageType.SelectedIndex == 2)
            {
                this.cboDataType.Items.AddRange(new object[] { "Boolean" });
                if (this.cboDataType.SelectedIndex == 0)
                {
                    return esriNetworkAttributeDataType.esriNADTBoolean;
                }
            }
            else if (this.cboUsageType.SelectedIndex == 3)
            {
                this.cboDataType.Items.AddRange(new object[] { "Integer" });
                if (this.cboDataType.SelectedIndex == 0)
                {
                    return esriNetworkAttributeDataType.esriNADTInteger;
                }
            }
            return esriNetworkAttributeDataType.esriNADTBoolean;
        }

        private void txtName_EditValueChanged(object sender, EventArgs e)
        {
            if (this.txtName.Text.Trim().Length == 0)
            {
                this.btnOK.Enabled = false;
            }
            else
            {
                this.btnOK.Enabled = true;
            }
        }

        public INetworkAttribute NetworkAttribute
        {
            get
            {
                return this.inetworkAttribute_0;
            }
            set
            {
                this.inetworkAttribute_0 = value;
            }
        }
    }
}

