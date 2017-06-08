using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public class GridAxisPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private StyleButton btnStyle;
        private StyleButton btnSubStyle;
        private ComboBox cboBorderType;
        private CheckBox chkMainBottom;
        private CheckBox chkMainLeft;
        private CheckBox chkMainRight;
        private CheckBox chkMainTop;
        private CheckBox chkSubBottom;
        private CheckBox chkSubLeft;
        private CheckBox chkSubRight;
        private CheckBox chkSubTop;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private IMapGrid imapGrid_0 = null;
        private IMapGridBorder imapGridBorder_0 = null;
        private Label label1;
        private Label label10;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        internal static IMapGrid m_pOldMapGrid;
        public static IStyleGallery m_pSG;
        [CompilerGenerated]
        private MapCartoTemplateLib.MapTemplate mapTemplate_0;
        private RadioButton rdoInInner;
        private RadioButton rdoInOutside;
        private RadioButton rdoSubTickInner;
        private RadioButton rdoSubTickOutside;
        private string string_0 = "坐标轴";
        private DomainUpDown txtSubCount;
        private DomainUpDown txtSubTickLength;
        private DomainUpDown txtTikLength;

        public event OnValueChangeEventHandler OnValueChange;

        static GridAxisPropertyPage()
        {
            old_acctor_mc();
        }

        public GridAxisPropertyPage()
        {
            this.InitializeComponent();
            m_pSG = ApplicationBase.StyleGallery;
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                this.imapGrid_0.SetTickVisibility(this.chkMainLeft.Checked, this.chkMainTop.Checked, this.chkMainRight.Checked, this.chkMainBottom.Checked);
                this.imapGrid_0.TickLineSymbol = this.btnStyle.Style as ILineSymbol;
                double num = Convert.ToDouble(this.txtTikLength.Text);
                if (this.rdoInInner.Checked)
                {
                    num = -num;
                }
                this.imapGrid_0.TickLength = num;
                this.imapGrid_0.SetSubTickVisibility(this.chkSubLeft.Checked, this.chkSubTop.Checked, this.chkSubRight.Checked, this.chkSubBottom.Checked);
                this.imapGrid_0.SubTickLineSymbol = this.btnSubStyle.Style as ILineSymbol;
                num = Convert.ToDouble(this.txtSubTickLength.Text);
                if (this.rdoSubTickInner.Checked)
                {
                    num = -num;
                }
                this.imapGrid_0.SubTickLength = num;
                this.imapGrid_0.SubTickCount = Convert.ToInt16(this.txtSubCount.Text);
                this.imapGrid_0.Border = this.imapGridBorder_0;
            }
        }

        private void btnStyle_Click(object sender, EventArgs e)
        {
            frmSymbolSelector selector = new frmSymbolSelector();
            if (selector != null)
            {
                selector.SetStyleGallery(ApplicationBase.StyleGallery);
                selector.SetSymbol(this.btnStyle.Style);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    this.bool_1 = true;
                    this.btnStyle.Style = selector.GetSymbol();
                    if (this.OnValueChange != null)
                    {
                        this.OnValueChange();
                    }
                }
            }
        }

        private void btnSubStyle_Click(object sender, EventArgs e)
        {
            frmSymbolSelector selector = new frmSymbolSelector();
            if (selector != null)
            {
                selector.SetStyleGallery(ApplicationBase.StyleGallery);
                selector.SetSymbol(this.btnSubStyle.Style);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    this.bool_1 = true;
                    this.btnSubStyle.Style = selector.GetSymbol();
                    if (this.OnValueChange != null)
                    {
                        this.OnValueChange();
                    }
                }
            }
        }

        public void Cancel()
        {
        }

        private void cboBorderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.cboBorderType.SelectedIndex == 0)
                {
                    this.imapGridBorder_0 = new SimpleMapGridBorderClass();
                }
                else if (this.cboBorderType.SelectedIndex == 1)
                {
                    this.imapGridBorder_0 = new CalibratedMapGridBorderClass();
                }
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void chkSubTop_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_2);
        }

        ~GridAxisPropertyPage()
        {
            this.imapGrid_0 = null;
            m_pOldMapGrid = null;
        }

        private void GridAxisPropertyPage_Load(object sender, EventArgs e)
        {
            this.imapGrid_0 = this.MapTemplate.MapGrid;
            this.Text = "坐标轴";
            if (this.imapGrid_0 != null)
            {
                bool leftVis = false;
                bool topVis = false;
                bool bottomVis = false;
                bool rightVis = false;
                this.imapGrid_0.QueryTickVisibility(ref leftVis, ref topVis, ref rightVis, ref bottomVis);
                this.chkMainBottom.Checked = bottomVis;
                this.chkMainLeft.Checked = leftVis;
                this.chkMainRight.Checked = rightVis;
                this.chkMainTop.Checked = topVis;
                this.btnStyle.Style = this.imapGrid_0.TickLineSymbol;
                double tickLength = this.imapGrid_0.TickLength;
                if (tickLength < 0.0)
                {
                    this.rdoInInner.Checked = true;
                }
                else
                {
                    this.rdoInOutside.Checked = true;
                }
                this.txtTikLength.Text = string.Format("{0}", Math.Abs(tickLength));
                this.imapGrid_0.QuerySubTickVisibility(ref leftVis, ref topVis, ref rightVis, ref bottomVis);
                this.chkSubBottom.Checked = bottomVis;
                this.chkSubLeft.Checked = leftVis;
                this.chkSubRight.Checked = rightVis;
                this.chkSubTop.Checked = topVis;
                this.btnSubStyle.Style = this.imapGrid_0.SubTickLineSymbol;
                tickLength = this.imapGrid_0.SubTickLength;
                if (tickLength < 0.0)
                {
                    this.rdoSubTickInner.Checked = true;
                }
                else
                {
                    this.rdoSubTickOutside.Checked = true;
                }
                this.txtSubTickLength.Text = string.Format("{0}", Math.Abs(tickLength));
                this.txtSubCount.Text = this.imapGrid_0.SubTickCount.ToString();
                if (this.imapGridBorder_0 is ISimpleMapGridBorder)
                {
                    this.cboBorderType.SelectedIndex = 0;
                }
                else if (this.imapGridBorder_0 is ICalibratedMapGridBorder)
                {
                    this.cboBorderType.SelectedIndex = 1;
                }
                else
                {
                    this.cboBorderType.SelectedIndex = -1;
                }
            }
            this.bool_0 = true;
        }

        public void Hide()
        {
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.label4 = new Label();
            this.label3 = new Label();
            this.btnStyle = new StyleButton();
            this.label2 = new Label();
            this.label1 = new Label();
            this.chkMainRight = new CheckBox();
            this.chkMainBottom = new CheckBox();
            this.chkMainLeft = new CheckBox();
            this.chkMainTop = new CheckBox();
            this.groupBox2 = new GroupBox();
            this.label9 = new Label();
            this.label5 = new Label();
            this.label6 = new Label();
            this.btnSubStyle = new StyleButton();
            this.label7 = new Label();
            this.label8 = new Label();
            this.chkSubRight = new CheckBox();
            this.chkSubBottom = new CheckBox();
            this.chkSubLeft = new CheckBox();
            this.chkSubTop = new CheckBox();
            this.groupBox3 = new GroupBox();
            this.label10 = new Label();
            this.cboBorderType = new ComboBox();
            this.rdoInOutside = new RadioButton();
            this.rdoInInner = new RadioButton();
            this.txtTikLength = new DomainUpDown();
            this.txtSubCount = new DomainUpDown();
            this.rdoSubTickInner = new RadioButton();
            this.rdoSubTickOutside = new RadioButton();
            this.txtSubTickLength = new DomainUpDown();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.txtTikLength);
            this.groupBox1.Controls.Add(this.rdoInInner);
            this.groupBox1.Controls.Add(this.rdoInOutside);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnStyle);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.chkMainRight);
            this.groupBox1.Controls.Add(this.chkMainBottom);
            this.groupBox1.Controls.Add(this.chkMainLeft);
            this.groupBox1.Controls.Add(this.chkMainTop);
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x108, 130);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "主分划刻度";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(240, 0x58);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x11, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "点";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x80, 0x58);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x35, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "刻度大于";
            this.btnStyle.Location = new Point(0x38, 0x30);
            this.btnStyle.Name = "btnStyle";
            this.btnStyle.Size = new Size(0x40, 0x18);
            this.btnStyle.Style = null;
            this.btnStyle.TabIndex = 6;
            this.btnStyle.Click += new EventHandler(this.btnStyle_Click);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 0x58);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "显示刻度";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0x38);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "符号:";
            this.chkMainRight.Location = new Point(0xb0, 0x18);
            this.chkMainRight.Name = "chkMainRight";
            this.chkMainRight.Size = new Size(0x30, 0x13);
            this.chkMainRight.TabIndex = 3;
            this.chkMainRight.Text = "右";
            this.chkMainRight.CheckedChanged += new EventHandler(this.chkSubTop_CheckedChanged);
            this.chkMainBottom.Location = new Point(120, 0x18);
            this.chkMainBottom.Name = "chkMainBottom";
            this.chkMainBottom.Size = new Size(0x30, 0x13);
            this.chkMainBottom.TabIndex = 2;
            this.chkMainBottom.Text = "下";
            this.chkMainBottom.CheckedChanged += new EventHandler(this.chkSubTop_CheckedChanged);
            this.chkMainLeft.Location = new Point(0x40, 0x18);
            this.chkMainLeft.Name = "chkMainLeft";
            this.chkMainLeft.Size = new Size(0x30, 0x13);
            this.chkMainLeft.TabIndex = 1;
            this.chkMainLeft.Text = "左";
            this.chkMainLeft.CheckedChanged += new EventHandler(this.chkSubTop_CheckedChanged);
            this.chkMainTop.Location = new Point(8, 0x18);
            this.chkMainTop.Name = "chkMainTop";
            this.chkMainTop.Size = new Size(0x30, 0x13);
            this.chkMainTop.TabIndex = 0;
            this.chkMainTop.Text = "上";
            this.chkMainTop.CheckedChanged += new EventHandler(this.chkSubTop_CheckedChanged);
            this.groupBox2.Controls.Add(this.txtSubTickLength);
            this.groupBox2.Controls.Add(this.rdoSubTickInner);
            this.groupBox2.Controls.Add(this.rdoSubTickOutside);
            this.groupBox2.Controls.Add(this.txtSubCount);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.btnSubStyle);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.chkSubRight);
            this.groupBox2.Controls.Add(this.chkSubBottom);
            this.groupBox2.Controls.Add(this.chkSubLeft);
            this.groupBox2.Controls.Add(this.chkSubTop);
            this.groupBox2.Location = new Point(8, 0x90);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x108, 160);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "子分划刻度";
            this.label9.AutoSize = true;
            this.label9.Location = new Point(8, 0x38);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x3b, 12);
            this.label9.TabIndex = 0x12;
            this.label9.Text = "子刻度数:";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(240, 120);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x11, 12);
            this.label5.TabIndex = 0x11;
            this.label5.Text = "点";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x80, 120);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x35, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "刻度大于";
            this.btnSubStyle.Location = new Point(0x38, 80);
            this.btnSubStyle.Name = "btnSubStyle";
            this.btnSubStyle.Size = new Size(0x40, 0x18);
            this.btnSubStyle.Style = null;
            this.btnSubStyle.TabIndex = 13;
            this.btnSubStyle.Click += new EventHandler(this.btnSubStyle_Click);
            this.label7.AutoSize = true;
            this.label7.Location = new Point(8, 120);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x35, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "显示刻度";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(8, 0x58);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x23, 12);
            this.label8.TabIndex = 11;
            this.label8.Text = "符号:";
            this.chkSubRight.Location = new Point(0xb0, 0x18);
            this.chkSubRight.Name = "chkSubRight";
            this.chkSubRight.Size = new Size(0x30, 0x13);
            this.chkSubRight.TabIndex = 7;
            this.chkSubRight.Text = "右";
            this.chkSubRight.CheckedChanged += new EventHandler(this.chkSubTop_CheckedChanged);
            this.chkSubBottom.Location = new Point(120, 0x18);
            this.chkSubBottom.Name = "chkSubBottom";
            this.chkSubBottom.Size = new Size(0x30, 0x13);
            this.chkSubBottom.TabIndex = 6;
            this.chkSubBottom.Text = "下";
            this.chkSubBottom.CheckedChanged += new EventHandler(this.chkSubTop_CheckedChanged);
            this.chkSubLeft.Location = new Point(0x40, 0x18);
            this.chkSubLeft.Name = "chkSubLeft";
            this.chkSubLeft.Size = new Size(0x30, 0x13);
            this.chkSubLeft.TabIndex = 5;
            this.chkSubLeft.Text = "左";
            this.chkSubLeft.CheckedChanged += new EventHandler(this.chkSubTop_CheckedChanged);
            this.chkSubTop.Location = new Point(8, 0x18);
            this.chkSubTop.Name = "chkSubTop";
            this.chkSubTop.Size = new Size(0x30, 0x13);
            this.chkSubTop.TabIndex = 4;
            this.chkSubTop.Text = "上";
            this.chkSubTop.CheckedChanged += new EventHandler(this.chkSubTop_CheckedChanged);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.cboBorderType);
            this.groupBox3.Location = new Point(8, 0x138);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x108, 80);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "边框属性";
            this.groupBox3.Visible = false;
            this.label10.AutoSize = true;
            this.label10.Location = new Point(8, 0x20);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x23, 12);
            this.label10.TabIndex = 12;
            this.label10.Text = "边框:";
            this.cboBorderType.Text = "";
            this.cboBorderType.Location = new Point(0x31, 0x1c);
            this.cboBorderType.Name = "cboBorderType";
            this.cboBorderType.Items.AddRange(new object[] { "简单的边框", "校准的边框" });
            this.cboBorderType.Size = new Size(0x70, 0x15);
            this.cboBorderType.TabIndex = 0;
            this.cboBorderType.SelectedIndexChanged += new EventHandler(this.cboBorderType_SelectedIndexChanged);
            this.rdoInOutside.AutoSize = true;
            this.rdoInOutside.Location = new Point(0x43, 0x6c);
            this.rdoInOutside.Name = "rdoInOutside";
            this.rdoInOutside.Size = new Size(0x3b, 0x10);
            this.rdoInOutside.TabIndex = 11;
            this.rdoInOutside.Text = "在外部";
            this.rdoInOutside.UseVisualStyleBackColor = true;
            this.rdoInInner.AutoSize = true;
            this.rdoInInner.Checked = true;
            this.rdoInInner.Location = new Point(0x43, 0x56);
            this.rdoInInner.Name = "rdoInInner";
            this.rdoInInner.Size = new Size(0x3b, 0x10);
            this.rdoInInner.TabIndex = 12;
            this.rdoInInner.TabStop = true;
            this.rdoInInner.Text = "在内部";
            this.rdoInInner.UseVisualStyleBackColor = true;
            this.txtTikLength.Location = new Point(0xbb, 0x56);
            this.txtTikLength.Name = "txtTikLength";
            this.txtTikLength.Size = new Size(0x2f, 0x15);
            this.txtTikLength.TabIndex = 13;
            this.txtTikLength.Text = "0";
            this.txtSubCount.Location = new Point(0x43, 0x36);
            this.txtSubCount.Name = "txtSubCount";
            this.txtSubCount.Size = new Size(0x4b, 0x15);
            this.txtSubCount.TabIndex = 20;
            this.txtSubCount.Text = "0";
            this.rdoSubTickInner.AutoSize = true;
            this.rdoSubTickInner.Checked = true;
            this.rdoSubTickInner.Location = new Point(0x43, 0x76);
            this.rdoSubTickInner.Name = "rdoSubTickInner";
            this.rdoSubTickInner.Size = new Size(0x3b, 0x10);
            this.rdoSubTickInner.TabIndex = 0x16;
            this.rdoSubTickInner.TabStop = true;
            this.rdoSubTickInner.Text = "在内部";
            this.rdoSubTickInner.UseVisualStyleBackColor = true;
            this.rdoSubTickOutside.AutoSize = true;
            this.rdoSubTickOutside.Location = new Point(0x43, 140);
            this.rdoSubTickOutside.Name = "rdoSubTickOutside";
            this.rdoSubTickOutside.Size = new Size(0x3b, 0x10);
            this.rdoSubTickOutside.TabIndex = 0x15;
            this.rdoSubTickOutside.Text = "在外部";
            this.rdoSubTickOutside.UseVisualStyleBackColor = true;
            this.txtSubTickLength.Location = new Point(0xba, 0x75);
            this.txtSubTickLength.Name = "txtSubTickLength";
            this.txtSubTickLength.Size = new Size(0x2f, 0x15);
            this.txtSubTickLength.TabIndex = 0x17;
            this.txtSubTickLength.Text = "0";
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "GridAxisPropertyPage";
            base.Size = new Size(0x120, 0x137);
            base.Load += new EventHandler(this.GridAxisPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            base.ResumeLayout(false);
        }

        private void method_0(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void method_1(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void method_2(object sender, ChangingEventArgs e)
        {
        }

        private static void old_acctor_mc()
        {
            m_pSG = null;
            m_pOldMapGrid = null;
        }

        public void ResetControl()
        {
        }

        public void SetObjects(object object_0)
        {
            this.MapTemplate = object_0 as MapCartoTemplateLib.MapTemplate;
            this.imapGrid_0 = this.MapTemplate.MapGrid;
            this.imapGridBorder_0 = this.imapGrid_0.Border;
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

        public MapCartoTemplateLib.MapTemplate MapTemplate
        {
            [CompilerGenerated]
            get
            {
                return this.mapTemplate_0;
            }
            [CompilerGenerated]
            set
            {
                this.mapTemplate_0 = value;
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

