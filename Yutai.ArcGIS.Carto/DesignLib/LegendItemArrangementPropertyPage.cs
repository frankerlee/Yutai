using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class LegendItemArrangementPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Container container_0 = null;
        private ILegendItem ilegendItem_0 = null;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label16;
        private Label label17;
        private Label label18;
        private Label label19;
        private Label label2;
        private Label label20;
        private Label label21;
        private Label label22;
        private Label label23;
        private Label label24;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Panel panel1;
        private Panel panel2;
        private RadioGroup rdoHLegendItemArrangement;
        private RadioGroup rdoVLegendItemArrangement;
        private string string_0 = "排列方式";

        public event OnValueChangeEventHandler OnValueChange;

        public LegendItemArrangementPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                if (this.ilegendItem_0 is IVerticalLegendItem)
                {
                    (this.ilegendItem_0 as IVerticalLegendItem).Arrangement = (esriLegendItemArrangement) this.rdoVLegendItemArrangement.SelectedIndex;
                }
                else if (this.ilegendItem_0 is IHorizontalLegendItem)
                {
                    (this.ilegendItem_0 as IHorizontalLegendItem).Arrangement = (esriLegendItemArrangement) this.rdoHLegendItemArrangement.SelectedIndex;
                }
            }
        }

        public void Cancel()
        {
        }

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
            this.panel1 = new Panel();
            this.label6 = new Label();
            this.label5 = new Label();
            this.label4 = new Label();
            this.label3 = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            this.rdoHLegendItemArrangement = new RadioGroup();
            this.panel2 = new Panel();
            this.label9 = new Label();
            this.label20 = new Label();
            this.label21 = new Label();
            this.label22 = new Label();
            this.label23 = new Label();
            this.label24 = new Label();
            this.label17 = new Label();
            this.label18 = new Label();
            this.label19 = new Label();
            this.label14 = new Label();
            this.label15 = new Label();
            this.label16 = new Label();
            this.label11 = new Label();
            this.label12 = new Label();
            this.label13 = new Label();
            this.label10 = new Label();
            this.label8 = new Label();
            this.label7 = new Label();
            this.rdoVLegendItemArrangement = new RadioGroup();
            this.panel1.SuspendLayout();
            this.rdoHLegendItemArrangement.Properties.BeginInit();
            this.panel2.SuspendLayout();
            this.rdoVLegendItemArrangement.Properties.BeginInit();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.rdoHLegendItemArrangement);
            this.panel1.Location = new Point(8, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0xc0, 200);
            this.panel1.TabIndex = 0x1b;
            this.panel1.Visible = false;
            this.label6.BackColor = SystemColors.ControlDark;
            this.label6.Location = new Point(140, 0x9d);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x20, 0x10);
            this.label6.TabIndex = 13;
            this.label5.BackColor = SystemColors.ControlDark;
            this.label5.Location = new Point(0x58, 0x85);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x20, 0x10);
            this.label5.TabIndex = 12;
            this.label4.BackColor = SystemColors.ControlDark;
            this.label4.Location = new Point(140, 0x66);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x20, 0x10);
            this.label4.TabIndex = 11;
            this.label3.BackColor = SystemColors.ControlDark;
            this.label3.Location = new Point(0x58, 0x4d);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x20, 0x10);
            this.label3.TabIndex = 10;
            this.label2.BackColor = SystemColors.ControlDark;
            this.label2.Location = new Point(0x25, 0x2f);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x20, 0x10);
            this.label2.TabIndex = 9;
            this.label1.BackColor = SystemColors.ControlDark;
            this.label1.Location = new Point(0x25, 20);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x20, 0x10);
            this.label1.TabIndex = 8;
            this.rdoHLegendItemArrangement.Location = new Point(8, 8);
            this.rdoHLegendItemArrangement.Name = "rdoHLegendItemArrangement";
            this.rdoHLegendItemArrangement.Properties.Appearance.BackColor = SystemColors.Control;
            this.rdoHLegendItemArrangement.Properties.Appearance.Options.UseBackColor = true;
            this.rdoHLegendItemArrangement.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "                标注      说明"), new RadioGroupItem(null, "                说明      标注"), new RadioGroupItem(null, "   标注                   说明"), new RadioGroupItem(null, "   标注      说明"), new RadioGroupItem(null, "   说明                    标注"), new RadioGroupItem(null, "   说明      标注") });
            this.rdoHLegendItemArrangement.Size = new Size(0xb0, 0xb8);
            this.rdoHLegendItemArrangement.TabIndex = 7;
            this.rdoHLegendItemArrangement.SelectedIndexChanged += new EventHandler(this.rdoHLegendItemArrangement_SelectedIndexChanged);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label20);
            this.panel2.Controls.Add(this.label21);
            this.panel2.Controls.Add(this.label22);
            this.panel2.Controls.Add(this.label23);
            this.panel2.Controls.Add(this.label24);
            this.panel2.Controls.Add(this.label17);
            this.panel2.Controls.Add(this.label18);
            this.panel2.Controls.Add(this.label19);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.label15);
            this.panel2.Controls.Add(this.label16);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.rdoVLegendItemArrangement);
            this.panel2.Location = new Point(8, 8);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x128, 0xa8);
            this.panel2.TabIndex = 0x1c;
            this.panel2.Visible = false;
            this.label9.Location = new Point(0xe8, 0x70);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x20, 0x10);
            this.label9.TabIndex = 0x2d;
            this.label9.Text = "标注";
            this.label20.BackColor = SystemColors.ControlDark;
            this.label20.Location = new Point(0xe8, 0x88);
            this.label20.Name = "label20";
            this.label20.Size = new Size(0x20, 0x10);
            this.label20.TabIndex = 0x2c;
            this.label21.Location = new Point(0xd8, 0x58);
            this.label21.Name = "label21";
            this.label21.Size = new Size(0x20, 0x10);
            this.label21.TabIndex = 0x2b;
            this.label21.Text = "说明";
            this.label22.Location = new Point(0xd8, 0x40);
            this.label22.Name = "label22";
            this.label22.Size = new Size(0x20, 0x10);
            this.label22.TabIndex = 0x2a;
            this.label22.Text = "说明";
            this.label23.BackColor = SystemColors.ControlDark;
            this.label23.Location = new Point(0xe8, 40);
            this.label23.Name = "label23";
            this.label23.Size = new Size(0x20, 0x10);
            this.label23.TabIndex = 0x29;
            this.label24.Location = new Point(0xe8, 0x10);
            this.label24.Name = "label24";
            this.label24.Size = new Size(0x20, 0x10);
            this.label24.TabIndex = 40;
            this.label24.Text = "标注";
            this.label17.BackColor = SystemColors.ControlDark;
            this.label17.Location = new Point(0x90, 0x70);
            this.label17.Name = "label17";
            this.label17.Size = new Size(0x20, 0x10);
            this.label17.TabIndex = 0x27;
            this.label18.Location = new Point(0x90, 0x88);
            this.label18.Name = "label18";
            this.label18.Size = new Size(0x20, 0x10);
            this.label18.TabIndex = 0x26;
            this.label18.Text = "标注";
            this.label19.Location = new Point(0x80, 0x58);
            this.label19.Name = "label19";
            this.label19.Size = new Size(0x20, 0x10);
            this.label19.TabIndex = 0x25;
            this.label19.Text = "说明";
            this.label14.Location = new Point(0x80, 40);
            this.label14.Name = "label14";
            this.label14.Size = new Size(0x20, 0x10);
            this.label14.TabIndex = 0x24;
            this.label14.Text = "说明";
            this.label15.Location = new Point(0x90, 0x40);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x20, 0x10);
            this.label15.TabIndex = 0x23;
            this.label15.Text = "标注";
            this.label16.BackColor = SystemColors.ControlDark;
            this.label16.Location = new Point(0x90, 0x10);
            this.label16.Name = "label16";
            this.label16.Size = new Size(0x20, 0x10);
            this.label16.TabIndex = 0x22;
            this.label11.BackColor = SystemColors.ControlDark;
            this.label11.Location = new Point(0x30, 0x88);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x20, 0x10);
            this.label11.TabIndex = 0x21;
            this.label12.Location = new Point(0x20, 0x70);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x20, 0x10);
            this.label12.TabIndex = 0x20;
            this.label12.Text = "说明";
            this.label13.Location = new Point(0x30, 0x58);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x20, 0x10);
            this.label13.TabIndex = 0x1f;
            this.label13.Text = "标注";
            this.label10.Location = new Point(0x30, 40);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x20, 0x10);
            this.label10.TabIndex = 30;
            this.label10.Text = "标注";
            this.label8.Location = new Point(0x20, 0x40);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x20, 0x10);
            this.label8.TabIndex = 0x1d;
            this.label8.Text = "说明";
            this.label7.BackColor = SystemColors.ControlDark;
            this.label7.Location = new Point(0x30, 0x10);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x20, 0x10);
            this.label7.TabIndex = 0x1c;
            this.rdoVLegendItemArrangement.Location = new Point(8, 8);
            this.rdoVLegendItemArrangement.Name = "rdoVLegendItemArrangement";
            this.rdoVLegendItemArrangement.Properties.Appearance.BackColor = SystemColors.Control;
            this.rdoVLegendItemArrangement.Properties.Appearance.Options.UseBackColor = true;
            this.rdoVLegendItemArrangement.Properties.Columns = 3;
            this.rdoVLegendItemArrangement.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(), new RadioGroupItem(), new RadioGroupItem(), new RadioGroupItem(), new RadioGroupItem(), new RadioGroupItem() });
            this.rdoVLegendItemArrangement.Size = new Size(280, 0x98);
            this.rdoVLegendItemArrangement.TabIndex = 0x1b;
            this.rdoVLegendItemArrangement.SelectedIndexChanged += new EventHandler(this.rdoVLegendItemArrangement_SelectedIndexChanged);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Name = "LegendItemArrangementPropertyPage";
            base.Size = new Size(320, 0xe0);
            base.Load += new EventHandler(this.LegendItemArrangementPropertyPage_Load);
            this.panel1.ResumeLayout(false);
            this.rdoHLegendItemArrangement.Properties.EndInit();
            this.panel2.ResumeLayout(false);
            this.rdoVLegendItemArrangement.Properties.EndInit();
            base.ResumeLayout(false);
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

        private void LegendItemArrangementPropertyPage_Load(object sender, EventArgs e)
        {
            this.method_1();
            this.bool_0 = true;
        }

        private void method_0()
        {
            this.bool_1 = true;
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
            }
        }

        private void method_1()
        {
            if (this.ilegendItem_0 is IVerticalLegendItem)
            {
                this.panel2.Visible = true;
                this.rdoVLegendItemArrangement.SelectedIndex = (int) (this.ilegendItem_0 as IVerticalLegendItem).Arrangement;
            }
            else if (this.ilegendItem_0 is IHorizontalLegendItem)
            {
                this.panel1.Visible = true;
                this.rdoHLegendItemArrangement.SelectedIndex = (int) (this.ilegendItem_0 as IHorizontalLegendItem).Arrangement;
            }
        }

        private void rdoHLegendItemArrangement_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.method_0();
            }
        }

        private void rdoVLegendItemArrangement_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.method_0();
            }
        }

        public void ResetControl()
        {
            this.bool_0 = false;
            this.method_1();
            this.bool_0 = true;
        }

        public void SetObjects(object object_0)
        {
            this.ilegendItem_0 = object_0 as ILegendItem;
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_1;
            }
        }

        int IPropertyPage.Height
        {
            get
            {
                return base.Height;
            }
        }

        int IPropertyPage.Width
        {
            get
            {
                return base.Width;
            }
        }

        public string Title
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }
    }
}

