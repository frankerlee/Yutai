using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class RouteLayerPropertyPage
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
            this.label1 = new Label();
            this.cboRouteID = new ComboBoxEdit();
            this.groupBox2 = new GroupBox();
            this.checkEdit1 = new CheckEdit();
            this.checkEdit2 = new CheckEdit();
            this.radioGroup1 = new RadioGroup();
            this.groupBox1.SuspendLayout();
            this.cboRouteID.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.checkEdit1.Properties.BeginInit();
            this.checkEdit2.Properties.BeginInit();
            this.radioGroup1.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.cboRouteID);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(288, 48);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "路径定义器";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 24);
            this.label1.Name = "label1";
            this.label1.Size = new Size(60, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "路径标识:";
            this.cboRouteID.EditValue = "";
            this.cboRouteID.Location = new Point(80, 16);
            this.cboRouteID.Name = "cboRouteID";
            this.cboRouteID.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboRouteID.Size = new Size(192, 23);
            this.cboRouteID.TabIndex = 1;
            this.groupBox2.Controls.Add(this.radioGroup1);
            this.groupBox2.Controls.Add(this.checkEdit2);
            this.groupBox2.Controls.Add(this.checkEdit1);
            this.groupBox2.Location = new Point(8, 64);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(376, 192);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "路径度量异常";
            this.checkEdit1.Location = new Point(16, 24);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "显示度量值为空的地方";
            this.checkEdit1.Size = new Size(160, 19);
            this.checkEdit1.TabIndex = 0;
            this.checkEdit2.Location = new Point(16, 88);
            this.checkEdit2.Name = "checkEdit2";
            this.checkEdit2.Properties.Caption = "显示度量值不";
            this.checkEdit2.Size = new Size(104, 19);
            this.checkEdit2.TabIndex = 1;
            this.radioGroup1.Location = new Point(128, 88);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup1.Properties.Columns = 2;
            this.radioGroup1.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "增加"), new RadioGroupItem(null, "随数字化方向增加") });
            this.radioGroup1.Size = new Size(232, 24);
            this.radioGroup1.TabIndex = 2;
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "RouteLayerPropertyPage";
            base.Size = new Size(400, 272);
            this.groupBox1.ResumeLayout(false);
            this.cboRouteID.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.checkEdit1.Properties.EndInit();
            this.checkEdit2.Properties.EndInit();
            this.radioGroup1.Properties.EndInit();
            base.ResumeLayout(false);
        }
    
        private ComboBoxEdit cboRouteID;
        private CheckEdit checkEdit1;
        private CheckEdit checkEdit2;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private RadioGroup radioGroup1;
    }
}