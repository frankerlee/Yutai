using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal class DimesionFeaturePropertyPage : UserControl
    {
        private ComboBoxEdit cboUnits;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label2;
        private RadioGroup radioGroupStyle;
        private TextEdit txtReferenceScale;

        public DimesionFeaturePropertyPage()
        {
            this.InitializeComponent();
        }

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
            this.label2 = new Label();
            this.txtReferenceScale = new TextEdit();
            this.cboUnits = new ComboBoxEdit();
            this.groupBox2 = new GroupBox();
            this.radioGroupStyle = new RadioGroup();
            this.groupBox1.SuspendLayout();
            this.txtReferenceScale.Properties.BeginInit();
            this.cboUnits.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.radioGroupStyle.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.cboUnits);
            this.groupBox1.Controls.Add(this.txtReferenceScale);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(0x10, 0x10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xd8, 0x60);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "参考比例";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0x20);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x42, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "参考比例1:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 0x40);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x36, 0x11);
            this.label2.TabIndex = 1;
            this.label2.Text = "地图单位";
            this.txtReferenceScale.EditValue = "1";
            this.txtReferenceScale.Location = new Point(0x58, 0x18);
            this.txtReferenceScale.Name = "txtReferenceScale";
            this.txtReferenceScale.Size = new Size(0x70, 0x17);
            this.txtReferenceScale.TabIndex = 2;
            this.cboUnits.EditValue = "点";
            this.cboUnits.Location = new Point(0x58, 0x38);
            this.cboUnits.Name = "cboUnits";
            this.cboUnits.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboUnits.Properties.Items.AddRange(new object[] { "未知单位", "英寸", "点", "英尺", "码", "英里", "海里", "毫米", "厘米", "米", "公里", "十进制度", "分米" });
            this.cboUnits.Size = new Size(0x70, 0x17);
            this.cboUnits.TabIndex = 3;
            this.groupBox2.Controls.Add(this.radioGroupStyle);
            this.groupBox2.Location = new Point(0x10, 0x80);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0xd8, 0x88);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "样式设置";
            this.groupBox2.Visible = false;
            this.radioGroupStyle.Location = new Point(8, 0x18);
            this.radioGroupStyle.Name = "radioGroupStyle";
            this.radioGroupStyle.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroupStyle.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroupStyle.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroupStyle.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "新建样式"), new RadioGroupItem(null, "自定义样式"), new RadioGroupItem(null, "导入样式") });
            this.radioGroupStyle.Size = new Size(0xb0, 0x58);
            this.radioGroupStyle.TabIndex = 0;
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "DimesionFeaturePropertyPage";
            base.Size = new Size(0x110, 0x138);
            this.groupBox1.ResumeLayout(false);
            this.txtReferenceScale.Properties.EndInit();
            this.cboUnits.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.radioGroupStyle.Properties.EndInit();
            base.ResumeLayout(false);
        }
    }
}

