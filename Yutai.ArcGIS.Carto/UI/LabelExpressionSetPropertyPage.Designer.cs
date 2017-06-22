using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class LabelExpressionSetPropertyPage
    {
        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_2);
        }

       
 private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.btnAppend = new SimpleButton();
            this.listView1 = new ListView();
            this.groupBox2 = new GroupBox();
            this.label1 = new Label();
            this.cboExpressionParser = new ComboBoxEdit();
            this.txtExpression = new MemoEdit();
            this.checkEdit1 = new CheckEdit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.cboExpressionParser.Properties.BeginInit();
            this.txtExpression.Properties.BeginInit();
            this.checkEdit1.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.btnAppend);
            this.groupBox1.Controls.Add(this.listView1);
            this.groupBox1.Location = new Point(16, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(256, 144);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "标注字段";
            this.btnAppend.Location = new Point(24, 112);
            this.btnAppend.Name = "btnAppend";
            this.btnAppend.Size = new Size(64, 24);
            this.btnAppend.TabIndex = 1;
            this.btnAppend.Text = "添加";
            this.btnAppend.Click += new EventHandler(this.btnAppend_Click);
            this.listView1.Location = new Point(16, 24);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(232, 80);
            this.listView1.TabIndex = 0;
            this.listView1.View = View.SmallIcon;
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cboExpressionParser);
            this.groupBox2.Controls.Add(this.txtExpression);
            this.groupBox2.Location = new Point(8, 184);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(264, 152);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "表达式";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(24, 112);
            this.label1.Name = "label1";
            this.label1.Size = new Size(48, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "解析器:";
            this.cboExpressionParser.EditValue = "VBScript";
            this.cboExpressionParser.Location = new Point(80, 112);
            this.cboExpressionParser.Name = "cboExpressionParser";
            this.cboExpressionParser.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboExpressionParser.Properties.Enabled = false;
            this.cboExpressionParser.Properties.Items.AddRange(new object[] { "JScript", "VBScript" });
            this.cboExpressionParser.Size = new Size(104, 23);
            this.cboExpressionParser.TabIndex = 1;
            this.cboExpressionParser.SelectedIndexChanged += new EventHandler(this.cboExpressionParser_SelectedIndexChanged);
            this.txtExpression.EditValue = "";
            this.txtExpression.Location = new Point(16, 24);
            this.txtExpression.Name = "txtExpression";
            this.txtExpression.Size = new Size(224, 72);
            this.txtExpression.TabIndex = 0;
            this.txtExpression.EditValueChanged += new EventHandler(this.txtExpression_EditValueChanged);
            this.checkEdit1.Location = new Point(176, 160);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "高级";
            this.checkEdit1.Size = new Size(72, 19);
            this.checkEdit1.TabIndex = 2;
            this.checkEdit1.CheckedChanged += new EventHandler(this.checkEdit1_CheckedChanged);
            base.Controls.Add(this.checkEdit1);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "LabelExpressionSetPropertyPage";
            base.Size = new Size(296, 344);
            base.Load += new EventHandler(this.LabelExpressionSetPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.cboExpressionParser.Properties.EndInit();
            this.txtExpression.Properties.EndInit();
            this.checkEdit1.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnAppend;
        private ComboBoxEdit cboExpressionParser;
        private CheckEdit checkEdit1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private ILabelEngineLayerProperties ilabelEngineLayerProperties_0;
        private Label label1;
        private ListView listView1;
        private MemoEdit txtExpression;
    }
}