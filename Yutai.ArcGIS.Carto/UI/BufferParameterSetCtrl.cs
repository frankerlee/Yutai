using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class BufferParameterSetCtrl : UserControl
    {
        private ComboBoxEdit cboDisField;
        private ComboBoxEdit cboUnits;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label lblRingCount;
        private Label lblRingDis;
        private RadioButton rdoFromProperty;
        private RadioButton rdoMoreBuffer;
        private RadioButton rdoSetDis;
        private SpinEdit txtCount;
        private SpinEdit txtDistances;
        private SpinEdit txtSpace;

        public BufferParameterSetCtrl()
        {
            this.InitializeComponent();
        }

        private void BufferParameterSetCtrl_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private void cboDisField_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboDisField.SelectedIndex != -1)
            {
                BufferHelper.m_BufferHelper.m_FieldName = this.cboDisField.Text;
            }
        }

        private void cboUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboUnits.SelectedIndex != -1)
            {
                BufferHelper.m_BufferHelper.m_Units = (esriUnits) this.cboUnits.SelectedIndex;
            }
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        public void Init()
        {
            this.cboDisField.Properties.Items.Clear();
            if (BufferHelper.m_BufferHelper.m_SourceType == 0)
            {
                this.rdoFromProperty.Enabled = false;
                this.cboDisField.Enabled = false;
                if (this.rdoFromProperty.Checked)
                {
                    this.rdoSetDis.Checked = true;
                    this.rdoFromProperty.Checked = false;
                    BufferHelper.m_BufferHelper.m_BufferType = 0;
                }
            }
            else
            {
                IFields fields = BufferHelper.m_BufferHelper.m_pFeatureLayer.FeatureClass.Fields;
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    IField field = fields.get_Field(i);
                    if ((((field.Type == esriFieldType.esriFieldTypeDouble) || (field.Type == esriFieldType.esriFieldTypeSingle)) || (field.Type == esriFieldType.esriFieldTypeSmallInteger)) || (field.Type == esriFieldType.esriFieldTypeInteger))
                    {
                        this.cboDisField.Properties.Items.Add(field.AliasName);
                    }
                }
                this.cboDisField.Enabled = true;
                if (this.cboDisField.Properties.Items.Count > 0)
                {
                    this.cboDisField.SelectedIndex = 0;
                }
                this.rdoFromProperty.Enabled = true;
                if (this.rdoSetDis.Checked)
                {
                    BufferHelper.m_BufferHelper.m_BufferType = 0;
                }
                if (this.rdoFromProperty.Checked)
                {
                    BufferHelper.m_BufferHelper.m_BufferType = 1;
                }
            }
            this.method_0();
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.txtSpace = new SpinEdit();
            this.txtCount = new SpinEdit();
            this.lblRingDis = new Label();
            this.lblRingCount = new Label();
            this.txtDistances = new SpinEdit();
            this.cboDisField = new ComboBoxEdit();
            this.rdoMoreBuffer = new RadioButton();
            this.rdoFromProperty = new RadioButton();
            this.rdoSetDis = new RadioButton();
            this.groupBox2 = new GroupBox();
            this.cboUnits = new ComboBoxEdit();
            this.groupBox1.SuspendLayout();
            this.txtSpace.Properties.BeginInit();
            this.txtCount.Properties.BeginInit();
            this.txtDistances.Properties.BeginInit();
            this.cboDisField.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.cboUnits.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.txtSpace);
            this.groupBox1.Controls.Add(this.txtCount);
            this.groupBox1.Controls.Add(this.lblRingDis);
            this.groupBox1.Controls.Add(this.lblRingCount);
            this.groupBox1.Controls.Add(this.txtDistances);
            this.groupBox1.Controls.Add(this.cboDisField);
            this.groupBox1.Controls.Add(this.rdoMoreBuffer);
            this.groupBox1.Controls.Add(this.rdoFromProperty);
            this.groupBox1.Controls.Add(this.rdoSetDis);
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x1b0, 200);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "如何创建缓冲";
            int[] bits = new int[4];
            this.txtSpace.EditValue = new decimal(bits);
            this.txtSpace.Location = new Point(0x68, 0xa8);
            this.txtSpace.Name = "txtSpace";
            this.txtSpace.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtSpace.Size = new Size(0x80, 0x15);
            this.txtSpace.TabIndex = 10;
            this.txtSpace.EditValueChanged += new EventHandler(this.txtSpace_EditValueChanged);
            bits = new int[4];
            bits[0] = 1;
            this.txtCount.EditValue = new decimal(bits);
            this.txtCount.Location = new Point(0x68, 0x80);
            this.txtCount.Name = "txtCount";
            this.txtCount.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            bits = new int[4];
            bits[0] = 1;
            this.txtCount.Properties.MaxValue = new decimal(bits);
            bits = new int[4];
            bits[0] = 1;
            this.txtCount.Properties.MinValue = new decimal(bits);
            this.txtCount.Size = new Size(0x80, 0x15);
            this.txtCount.TabIndex = 9;
            this.txtCount.EditValueChanged += new EventHandler(this.txtCount_EditValueChanged);
            this.lblRingDis.AutoSize = true;
            this.lblRingDis.Location = new Point(0x10, 0xa8);
            this.lblRingDis.Name = "lblRingDis";
            this.lblRingDis.Size = new Size(0x53, 12);
            this.lblRingDis.TabIndex = 8;
            this.lblRingDis.Text = "环之间的距离:";
            this.lblRingCount.AutoSize = true;
            this.lblRingCount.Location = new Point(0x10, 0x88);
            this.lblRingCount.Name = "lblRingCount";
            this.lblRingCount.Size = new Size(0x23, 12);
            this.lblRingCount.TabIndex = 7;
            this.lblRingCount.Text = "环数:";
            bits = new int[4];
            this.txtDistances.EditValue = new decimal(bits);
            this.txtDistances.Location = new Point(0x80, 0x11);
            this.txtDistances.Name = "txtDistances";
            this.txtDistances.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtDistances.Size = new Size(0x80, 0x15);
            this.txtDistances.TabIndex = 6;
            this.txtDistances.EditValueChanged += new EventHandler(this.txtDistances_EditValueChanged);
            this.cboDisField.EditValue = "";
            this.cboDisField.Location = new Point(0x34, 0x4d);
            this.cboDisField.Name = "cboDisField";
            this.cboDisField.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboDisField.Size = new Size(0xc4, 0x15);
            this.cboDisField.TabIndex = 3;
            this.cboDisField.SelectedIndexChanged += new EventHandler(this.cboDisField_SelectedIndexChanged);
            this.rdoMoreBuffer.Location = new Point(0x10, 0x68);
            this.rdoMoreBuffer.Name = "rdoMoreBuffer";
            this.rdoMoreBuffer.Size = new Size(160, 0x18);
            this.rdoMoreBuffer.TabIndex = 2;
            this.rdoMoreBuffer.Text = "生成多个缓冲环";
            this.rdoMoreBuffer.Click += new EventHandler(this.rdoMoreBuffer_Click);
            this.rdoFromProperty.Location = new Point(0x10, 0x30);
            this.rdoFromProperty.Name = "rdoFromProperty";
            this.rdoFromProperty.Size = new Size(160, 0x18);
            this.rdoFromProperty.TabIndex = 1;
            this.rdoFromProperty.Text = "从属性值获取距离";
            this.rdoFromProperty.Click += new EventHandler(this.rdoFromProperty_Click);
            this.rdoSetDis.Checked = true;
            this.rdoSetDis.Location = new Point(0x10, 0x10);
            this.rdoSetDis.Name = "rdoSetDis";
            this.rdoSetDis.Size = new Size(0x58, 0x18);
            this.rdoSetDis.TabIndex = 0;
            this.rdoSetDis.TabStop = true;
            this.rdoSetDis.Text = "指定距离";
            this.rdoSetDis.Click += new EventHandler(this.rdoSetDis_Click);
            this.groupBox2.Controls.Add(this.cboUnits);
            this.groupBox2.Location = new Point(8, 0xd8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x1b0, 0x38);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "缓冲距离单位设置";
            this.cboUnits.EditValue = "米";
            this.cboUnits.Location = new Point(0x30, 0x18);
            this.cboUnits.Name = "cboUnits";
            this.cboUnits.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboUnits.Properties.Items.AddRange(new object[] { "未知单位", "英寸", "点", "英尺", "码", "英里", "海里", "毫米", "厘米", "米", "公里", "十进制度", "分米" });
            this.cboUnits.Size = new Size(0x88, 0x15);
            this.cboUnits.TabIndex = 0;
            this.cboUnits.SelectedIndexChanged += new EventHandler(this.cboUnits_SelectedIndexChanged);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "BufferParameterSetCtrl";
            base.Size = new Size(0x1d0, 0x120);
            base.Load += new EventHandler(this.BufferParameterSetCtrl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.txtSpace.Properties.EndInit();
            this.txtCount.Properties.EndInit();
            this.txtDistances.Properties.EndInit();
            this.cboDisField.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.cboUnits.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void method_0()
        {
            this.txtDistances.Enabled = this.rdoSetDis.Checked;
            this.cboDisField.Enabled = this.rdoFromProperty.Checked;
            this.lblRingCount.Enabled = this.rdoMoreBuffer.Checked;
            this.txtCount.Enabled = this.rdoMoreBuffer.Checked;
            this.lblRingCount.Enabled = this.rdoMoreBuffer.Checked;
            this.txtSpace.Enabled = this.rdoMoreBuffer.Checked;
        }

        private void rdoFromProperty_Click(object sender, EventArgs e)
        {
            BufferHelper.m_BufferHelper.m_BufferType = 1;
            this.method_0();
        }

        private void rdoMoreBuffer_Click(object sender, EventArgs e)
        {
            BufferHelper.m_BufferHelper.m_BufferType = 2;
            this.method_0();
        }

        private void rdoSetDis_Click(object sender, EventArgs e)
        {
            BufferHelper.m_BufferHelper.m_BufferType = 0;
            this.method_0();
        }

        private void txtCount_EditValueChanged(object sender, EventArgs e)
        {
            BufferHelper.m_BufferHelper.m_Count = (int) this.txtCount.EditValue;
        }

        private void txtDistances_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                BufferHelper.m_BufferHelper.m_dblRadius = double.Parse(this.txtDistances.Text);
            }
            catch
            {
            }
        }

        private void txtSpace_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                BufferHelper.m_BufferHelper.m_dblStep = double.Parse(this.txtSpace.Text);
            }
            catch
            {
            }
        }
    }
}

