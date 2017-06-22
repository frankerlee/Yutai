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
    partial class BufferParameterSetCtrl
    {
        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
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
            this.groupBox1.Size = new Size(432, 200);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "如何创建缓冲";
            int[] bits = new int[4];
            this.txtSpace.EditValue = new decimal(bits);
            this.txtSpace.Location = new Point(104, 168);
            this.txtSpace.Name = "txtSpace";
            this.txtSpace.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtSpace.Size = new Size(128, 21);
            this.txtSpace.TabIndex = 10;
            this.txtSpace.EditValueChanged += new EventHandler(this.txtSpace_EditValueChanged);
            int[] bits4 = new int[4];
            bits4[0] = 1;
            this.txtCount.EditValue = new decimal(bits4);
            this.txtCount.Location = new Point(104, 128);
            this.txtCount.Name = "txtCount";
            this.txtCount.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            int[] bits2 = new int[4];
            bits2[0] = 1;
            this.txtCount.Properties.MaxValue = new decimal(bits2);
            int[] bits3 = new int[4];
            bits3[0] = 1;
            this.txtCount.Properties.MinValue = new decimal(bits3);
            this.txtCount.Size = new Size(128, 21);
            this.txtCount.TabIndex = 9;
            this.txtCount.EditValueChanged += new EventHandler(this.txtCount_EditValueChanged);
            this.lblRingDis.AutoSize = true;
            this.lblRingDis.Location = new Point(16, 168);
            this.lblRingDis.Name = "lblRingDis";
            this.lblRingDis.Size = new Size(83, 12);
            this.lblRingDis.TabIndex = 8;
            this.lblRingDis.Text = "环之间的距离:";
            this.lblRingCount.AutoSize = true;
            this.lblRingCount.Location = new Point(16, 136);
            this.lblRingCount.Name = "lblRingCount";
            this.lblRingCount.Size = new Size(35, 12);
            this.lblRingCount.TabIndex = 7;
            this.lblRingCount.Text = "环数:";
            int[] bits5 = new int[4];
            this.txtDistances.EditValue = new decimal(bits5);
            this.txtDistances.Location = new Point(128, 17);
            this.txtDistances.Name = "txtDistances";
            this.txtDistances.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtDistances.Size = new Size(128, 21);
            this.txtDistances.TabIndex = 6;
            this.txtDistances.EditValueChanged += new EventHandler(this.txtDistances_EditValueChanged);
            this.cboDisField.EditValue = "";
            this.cboDisField.Location = new Point(52, 77);
            this.cboDisField.Name = "cboDisField";
            this.cboDisField.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboDisField.Size = new Size(196, 21);
            this.cboDisField.TabIndex = 3;
            this.cboDisField.SelectedIndexChanged += new EventHandler(this.cboDisField_SelectedIndexChanged);
            this.rdoMoreBuffer.Location = new Point(16, 104);
            this.rdoMoreBuffer.Name = "rdoMoreBuffer";
            this.rdoMoreBuffer.Size = new Size(160, 24);
            this.rdoMoreBuffer.TabIndex = 2;
            this.rdoMoreBuffer.Text = "生成多个缓冲环";
            this.rdoMoreBuffer.Click += new EventHandler(this.rdoMoreBuffer_Click);
            this.rdoFromProperty.Location = new Point(16, 48);
            this.rdoFromProperty.Name = "rdoFromProperty";
            this.rdoFromProperty.Size = new Size(160, 24);
            this.rdoFromProperty.TabIndex = 1;
            this.rdoFromProperty.Text = "从属性值获取距离";
            this.rdoFromProperty.Click += new EventHandler(this.rdoFromProperty_Click);
            this.rdoSetDis.Checked = true;
            this.rdoSetDis.Location = new Point(16, 16);
            this.rdoSetDis.Name = "rdoSetDis";
            this.rdoSetDis.Size = new Size(88, 24);
            this.rdoSetDis.TabIndex = 0;
            this.rdoSetDis.TabStop = true;
            this.rdoSetDis.Text = "指定距离";
            this.rdoSetDis.Click += new EventHandler(this.rdoSetDis_Click);
            this.groupBox2.Controls.Add(this.cboUnits);
            this.groupBox2.Location = new Point(8, 216);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(432, 56);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "缓冲距离单位设置";
            this.cboUnits.EditValue = "米";
            this.cboUnits.Location = new Point(48, 24);
            this.cboUnits.Name = "cboUnits";
            this.cboUnits.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboUnits.Properties.Items.AddRange(new object[] { "未知单位", "英寸", "点", "英尺", "码", "英里", "海里", "毫米", "厘米", "米", "公里", "十进制度", "分米" });
            this.cboUnits.Size = new Size(136, 21);
            this.cboUnits.TabIndex = 0;
            this.cboUnits.SelectedIndexChanged += new EventHandler(this.cboUnits_SelectedIndexChanged);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "BufferParameterSetCtrl";
            base.Size = new Size(464, 288);
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

       
        private ComboBoxEdit cboDisField;
        private ComboBoxEdit cboUnits;
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
    }
}