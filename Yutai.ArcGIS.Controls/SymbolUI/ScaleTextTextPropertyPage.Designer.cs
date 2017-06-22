using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    partial class ScaleTextTextPropertyPage
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

       
        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.symbolItem1 = new SymbolItem();
            this.groupBox2 = new GroupBox();
            this.rdoStyle = new RadioGroup();
            this.groupBox3 = new GroupBox();
            this.txtMapUnitLabel = new TextEdit();
            this.cboMapUnit = new ComboBoxEdit();
            this.txtPageUnitLabel = new TextEdit();
            this.cboPageUnit = new ComboBoxEdit();
            this.label3 = new Label();
            this.label4 = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.rdoStyle.Properties.BeginInit();
            this.groupBox3.SuspendLayout();
            this.txtMapUnitLabel.Properties.BeginInit();
            this.cboMapUnit.Properties.BeginInit();
            this.txtPageUnitLabel.Properties.BeginInit();
            this.cboPageUnit.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.symbolItem1);
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(248, 56);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "预览";
            this.symbolItem1.BackColor = SystemColors.ControlLight;
            this.symbolItem1.Dock = DockStyle.Fill;
            this.symbolItem1.Location = new Point(3, 17);
            this.symbolItem1.Name = "symbolItem1";
            this.symbolItem1.Size = new Size(242, 36);
            this.symbolItem1.Symbol = null;
            this.symbolItem1.TabIndex = 2;
            this.groupBox2.Controls.Add(this.rdoStyle);
            this.groupBox2.Location = new Point(8, 71);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(248, 72);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "样式";
            this.rdoStyle.Location = new Point(8, 16);
            this.rdoStyle.Name = "rdoStyle";
            this.rdoStyle.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoStyle.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "绝对比例"), new RadioGroupItem(null, "相对比例") });
            this.rdoStyle.Size = new Size(136, 48);
            this.rdoStyle.TabIndex = 0;
            this.rdoStyle.SelectedIndexChanged += new EventHandler(this.rdoStyle_SelectedIndexChanged);
            this.groupBox3.Controls.Add(this.txtMapUnitLabel);
            this.groupBox3.Controls.Add(this.cboMapUnit);
            this.groupBox3.Controls.Add(this.txtPageUnitLabel);
            this.groupBox3.Controls.Add(this.cboPageUnit);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new Point(8, 151);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(248, 120);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "格式";
            this.txtMapUnitLabel.EditValue = "";
            this.txtMapUnitLabel.Location = new Point(136, 80);
            this.txtMapUnitLabel.Name = "txtMapUnitLabel";
            this.txtMapUnitLabel.Size = new Size(80, 21);
            this.txtMapUnitLabel.TabIndex = 7;
            this.txtMapUnitLabel.EditValueChanged += new EventHandler(this.txtMapUnitLabel_EditValueChanged);
            this.cboMapUnit.EditValue = "";
            this.cboMapUnit.Location = new Point(16, 80);
            this.cboMapUnit.Name = "cboMapUnit";
            this.cboMapUnit.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboMapUnit.Properties.Items.AddRange(new object[] { "未知单位", "英寸", "点", "英尺", "码", "英里", "海里", "毫米", "厘米", "米", "公里", "十进制度", "分米" });
            this.cboMapUnit.Size = new Size(104, 21);
            this.cboMapUnit.TabIndex = 6;
            this.cboMapUnit.SelectedIndexChanged += new EventHandler(this.cboMapUnit_SelectedIndexChanged);
            this.txtPageUnitLabel.EditValue = "";
            this.txtPageUnitLabel.Location = new Point(136, 34);
            this.txtPageUnitLabel.Name = "txtPageUnitLabel";
            this.txtPageUnitLabel.Size = new Size(80, 21);
            this.txtPageUnitLabel.TabIndex = 5;
            this.txtPageUnitLabel.EditValueChanged += new EventHandler(this.txtPageUnitLabel_EditValueChanged);
            this.cboPageUnit.EditValue = "";
            this.cboPageUnit.Location = new Point(16, 34);
            this.cboPageUnit.Name = "cboPageUnit";
            this.cboPageUnit.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboPageUnit.Properties.Items.AddRange(new object[] { "厘米", "英寸", "点" });
            this.cboPageUnit.Size = new Size(96, 21);
            this.cboPageUnit.TabIndex = 4;
            this.cboPageUnit.SelectedIndexChanged += new EventHandler(this.cboPageUnit_SelectedIndexChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(136, 64);
            this.label3.Name = "label3";
            this.label3.Size = new Size(29, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "标记";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(16, 64);
            this.label4.Name = "label4";
            this.label4.Size = new Size(54, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "地图单位";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(144, 18);
            this.label2.Name = "label2";
            this.label2.Size = new Size(29, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "标记";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 18);
            this.label1.Name = "label1";
            this.label1.Size = new Size(54, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "页面单位";
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "ScaleTextTextPropertyPage";
            base.Size = new Size(280, 304);
            base.Load += new EventHandler(this.ScaleTextTextPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.rdoStyle.Properties.EndInit();
            this.groupBox3.ResumeLayout(false);
            this.txtMapUnitLabel.Properties.EndInit();
            this.cboMapUnit.Properties.EndInit();
            this.txtPageUnitLabel.Properties.EndInit();
            this.cboPageUnit.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private ComboBoxEdit cboMapUnit;
        private ComboBoxEdit cboPageUnit;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private RadioGroup rdoStyle;
        private SymbolItem symbolItem1;
        private TextEdit txtMapUnitLabel;
        private TextEdit txtPageUnitLabel;
    }
}