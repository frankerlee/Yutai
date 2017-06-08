using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    public class GridAxisPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private StyleButton btnStyle;
        private StyleButton btnSubStyle;
        private ComboBoxEdit cboBorderType;
        private CheckEdit chkMainBottom;
        private CheckEdit chkMainLeft;
        private CheckEdit chkMainRight;
        private CheckEdit chkMainTop;
        private CheckEdit chkSubBottom;
        private CheckEdit chkSubLeft;
        private CheckEdit chkSubRight;
        private CheckEdit chkSubTop;
        private Container components = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
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
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private IMapGridBorder m_pBorder = null;
        internal static IMapGrid m_pMapGrid = null;
        internal static IMapGrid m_pOldMapGrid = null;
        public static IStyleGallery m_pSG = null;
        private string m_Title = "坐标轴";
        private RadioGroup rdoSubTickPlace;
        private RadioGroup rdoTickPalce;
        private SpinEdit txtSubCount;
        private SpinEdit txtSubTickLength;
        private SpinEdit txtTickLength;

        public event OnValueChangeEventHandler OnValueChange;

        public GridAxisPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
                m_pMapGrid.SetTickVisibility(this.chkMainLeft.Checked, this.chkMainTop.Checked, this.chkMainRight.Checked, this.chkMainBottom.Checked);
                m_pMapGrid.TickLineSymbol = this.btnStyle.Style as ILineSymbol;
                double num = (double) this.txtTickLength.Value;
                num = (this.rdoTickPalce.SelectedIndex == 0) ? -num : num;
                m_pMapGrid.TickLength = num;
                m_pMapGrid.SetSubTickVisibility(this.chkSubLeft.Checked, this.chkSubTop.Checked, this.chkSubRight.Checked, this.chkSubBottom.Checked);
                m_pMapGrid.SubTickLineSymbol = this.btnSubStyle.Style as ILineSymbol;
                num = (double) this.txtSubTickLength.Value;
                num = (this.rdoSubTickPlace.SelectedIndex == 0) ? -num : num;
                m_pMapGrid.SubTickLength = num;
                m_pMapGrid.SubTickCount = (short) this.txtSubCount.Value;
                m_pMapGrid.Border = this.m_pBorder;
            }
        }

        private void btnStyle_Click(object sender, EventArgs e)
        {
            frmSymbolSelector selector = new frmSymbolSelector();
            if (selector != null)
            {
                selector.SetStyleGallery(m_pSG);
                selector.SetSymbol(this.btnStyle.Style);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    this.m_IsPageDirty = true;
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
                selector.SetStyleGallery(m_pSG);
                selector.SetSymbol(this.btnSubStyle.Style);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    this.m_IsPageDirty = true;
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
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.cboBorderType.SelectedIndex == 0)
                {
                    this.m_pBorder = new SimpleMapGridBorderClass();
                }
                else if (this.cboBorderType.SelectedIndex == 1)
                {
                    this.m_pBorder = new CalibratedMapGridBorderClass();
                }
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void chkMainTop_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        ~GridAxisPropertyPage()
        {
            m_pMapGrid = null;
            m_pOldMapGrid = null;
        }

        private void GridAxisPropertyPage_Load(object sender, EventArgs e)
        {
            this.Text = "坐标轴";
            if (m_pMapGrid != null)
            {
                bool leftVis = false;
                bool topVis = false;
                bool bottomVis = false;
                bool rightVis = false;
                m_pMapGrid.QueryTickVisibility(ref leftVis, ref topVis, ref rightVis, ref bottomVis);
                this.chkMainBottom.Checked = bottomVis;
                this.chkMainLeft.Checked = leftVis;
                this.chkMainRight.Checked = rightVis;
                this.chkMainTop.Checked = topVis;
                this.btnStyle.Style = m_pMapGrid.TickLineSymbol;
                double tickLength = m_pMapGrid.TickLength;
                this.rdoTickPalce.SelectedIndex = (tickLength < 0.0) ? 0 : 1;
                this.txtTickLength.Value = (decimal) Math.Abs(tickLength);
                m_pMapGrid.QuerySubTickVisibility(ref leftVis, ref topVis, ref rightVis, ref bottomVis);
                this.chkSubBottom.Checked = bottomVis;
                this.chkSubLeft.Checked = leftVis;
                this.chkSubRight.Checked = rightVis;
                this.chkSubTop.Checked = topVis;
                this.btnSubStyle.Style = m_pMapGrid.SubTickLineSymbol;
                tickLength = m_pMapGrid.SubTickLength;
                this.rdoSubTickPlace.SelectedIndex = (tickLength < 0.0) ? 0 : 1;
                this.txtSubTickLength.Value = (decimal) Math.Abs(tickLength);
                this.txtSubCount.Text = m_pMapGrid.SubTickCount.ToString();
                if (this.m_pBorder is ISimpleMapGridBorder)
                {
                    this.cboBorderType.SelectedIndex = 0;
                }
                else if (this.m_pBorder is ICalibratedMapGridBorder)
                {
                    this.cboBorderType.SelectedIndex = 1;
                }
                else
                {
                    this.cboBorderType.SelectedIndex = -1;
                }
            }
            this.m_CanDo = true;
        }

        public void Hide()
        {
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.label4 = new Label();
            this.txtTickLength = new SpinEdit();
            this.label3 = new Label();
            this.rdoTickPalce = new RadioGroup();
            this.btnStyle = new StyleButton();
            this.label2 = new Label();
            this.label1 = new Label();
            this.chkMainRight = new CheckEdit();
            this.chkMainBottom = new CheckEdit();
            this.chkMainLeft = new CheckEdit();
            this.chkMainTop = new CheckEdit();
            this.groupBox2 = new GroupBox();
            this.txtSubCount = new SpinEdit();
            this.label9 = new Label();
            this.label5 = new Label();
            this.txtSubTickLength = new SpinEdit();
            this.label6 = new Label();
            this.rdoSubTickPlace = new RadioGroup();
            this.btnSubStyle = new StyleButton();
            this.label7 = new Label();
            this.label8 = new Label();
            this.chkSubRight = new CheckEdit();
            this.chkSubBottom = new CheckEdit();
            this.chkSubLeft = new CheckEdit();
            this.chkSubTop = new CheckEdit();
            this.groupBox3 = new GroupBox();
            this.label10 = new Label();
            this.cboBorderType = new ComboBoxEdit();
            this.groupBox1.SuspendLayout();
            this.txtTickLength.Properties.BeginInit();
            this.rdoTickPalce.Properties.BeginInit();
            this.chkMainRight.Properties.BeginInit();
            this.chkMainBottom.Properties.BeginInit();
            this.chkMainLeft.Properties.BeginInit();
            this.chkMainTop.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.txtSubCount.Properties.BeginInit();
            this.txtSubTickLength.Properties.BeginInit();
            this.rdoSubTickPlace.Properties.BeginInit();
            this.chkSubRight.Properties.BeginInit();
            this.chkSubBottom.Properties.BeginInit();
            this.chkSubLeft.Properties.BeginInit();
            this.chkSubTop.Properties.BeginInit();
            this.groupBox3.SuspendLayout();
            this.cboBorderType.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtTickLength);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.rdoTickPalce);
            this.groupBox1.Controls.Add(this.btnStyle);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.chkMainRight);
            this.groupBox1.Controls.Add(this.chkMainBottom);
            this.groupBox1.Controls.Add(this.chkMainLeft);
            this.groupBox1.Controls.Add(this.chkMainTop);
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x108, 0x80);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "主分划刻度";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(240, 0x58);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x11, 0x11);
            this.label4.TabIndex = 10;
            this.label4.Text = "点";
            int[] bits = new int[4];
            this.txtTickLength.EditValue = new decimal(bits);
            this.txtTickLength.Location = new Point(0xb8, 0x58);
            this.txtTickLength.Name = "txtTickLength";
            this.txtTickLength.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtTickLength.Properties.UseCtrlIncrement = false;
            this.txtTickLength.Size = new Size(0x30, 0x17);
            this.txtTickLength.TabIndex = 9;
            this.txtTickLength.EditValueChanged += new EventHandler(this.txtTickLength_EditValueChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x80, 0x58);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x36, 0x11);
            this.label3.TabIndex = 8;
            this.label3.Text = "刻度大于";
            this.rdoTickPalce.Location = new Point(0x40, 80);
            this.rdoTickPalce.Name = "rdoTickPalce";
            this.rdoTickPalce.Properties.Appearance.BackColor = SystemColors.Control;
            this.rdoTickPalce.Properties.Appearance.Options.UseBackColor = true;
            this.rdoTickPalce.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoTickPalce.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "在内部"), new RadioGroupItem(null, "在外部") });
            this.rdoTickPalce.Size = new Size(0x48, 40);
            this.rdoTickPalce.TabIndex = 7;
            this.rdoTickPalce.SelectedIndexChanged += new EventHandler(this.rdoTickPalce_SelectedIndexChanged);
            this.btnStyle.Location = new Point(0x38, 0x30);
            this.btnStyle.Name = "btnStyle";
            this.btnStyle.Size = new Size(0x40, 0x18);
            this.btnStyle.Style = null;
            this.btnStyle.TabIndex = 6;
            this.btnStyle.Click += new EventHandler(this.btnStyle_Click);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 0x58);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x36, 0x11);
            this.label2.TabIndex = 5;
            this.label2.Text = "显示刻度";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0x38);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 0x11);
            this.label1.TabIndex = 4;
            this.label1.Text = "符号:";
            this.chkMainRight.Location = new Point(0xb0, 0x18);
            this.chkMainRight.Name = "chkMainRight";
            this.chkMainRight.Properties.Caption = "右";
            this.chkMainRight.Size = new Size(0x30, 0x13);
            this.chkMainRight.TabIndex = 3;
            this.chkMainRight.CheckedChanged += new EventHandler(this.chkMainTop_CheckedChanged);
            this.chkMainBottom.Location = new Point(120, 0x18);
            this.chkMainBottom.Name = "chkMainBottom";
            this.chkMainBottom.Properties.Caption = "下";
            this.chkMainBottom.Size = new Size(0x30, 0x13);
            this.chkMainBottom.TabIndex = 2;
            this.chkMainBottom.CheckedChanged += new EventHandler(this.chkMainTop_CheckedChanged);
            this.chkMainLeft.Location = new Point(0x40, 0x18);
            this.chkMainLeft.Name = "chkMainLeft";
            this.chkMainLeft.Properties.Caption = "左";
            this.chkMainLeft.Size = new Size(0x30, 0x13);
            this.chkMainLeft.TabIndex = 1;
            this.chkMainLeft.CheckedChanged += new EventHandler(this.chkMainTop_CheckedChanged);
            this.chkMainTop.Location = new Point(8, 0x18);
            this.chkMainTop.Name = "chkMainTop";
            this.chkMainTop.Properties.Caption = "上";
            this.chkMainTop.Size = new Size(0x30, 0x13);
            this.chkMainTop.TabIndex = 0;
            this.chkMainTop.CheckedChanged += new EventHandler(this.chkMainTop_CheckedChanged);
            this.groupBox2.Controls.Add(this.txtSubCount);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtSubTickLength);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.rdoSubTickPlace);
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
            bits = new int[4];
            this.txtSubCount.EditValue = new decimal(bits);
            this.txtSubCount.Location = new Point(80, 0x30);
            this.txtSubCount.Name = "txtSubCount";
            this.txtSubCount.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            bits = new int[4];
            bits[0] = 100;
            this.txtSubCount.Properties.MaxValue = new decimal(bits);
            this.txtSubCount.Properties.UseCtrlIncrement = false;
            this.txtSubCount.Size = new Size(0x30, 0x17);
            this.txtSubCount.TabIndex = 0x13;
            this.txtSubCount.EditValueChanged += new EventHandler(this.txtTickLength_EditValueChanged);
            this.label9.AutoSize = true;
            this.label9.Location = new Point(8, 0x38);
            this.label9.Name = "label9";
            this.label9.Size = new Size(60, 0x11);
            this.label9.TabIndex = 0x12;
            this.label9.Text = "子刻度数:";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(240, 120);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x11, 0x11);
            this.label5.TabIndex = 0x11;
            this.label5.Text = "点";
            bits = new int[4];
            this.txtSubTickLength.EditValue = new decimal(bits);
            this.txtSubTickLength.Location = new Point(0xb8, 120);
            this.txtSubTickLength.Name = "txtSubTickLength";
            this.txtSubTickLength.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtSubTickLength.Properties.UseCtrlIncrement = false;
            this.txtSubTickLength.Size = new Size(0x30, 0x17);
            this.txtSubTickLength.TabIndex = 0x10;
            this.txtSubTickLength.EditValueChanged += new EventHandler(this.txtTickLength_EditValueChanged);
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x80, 120);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x36, 0x11);
            this.label6.TabIndex = 15;
            this.label6.Text = "刻度大于";
            this.rdoSubTickPlace.Location = new Point(0x40, 0x70);
            this.rdoSubTickPlace.Name = "rdoSubTickPlace";
            this.rdoSubTickPlace.Properties.Appearance.BackColor = SystemColors.Control;
            this.rdoSubTickPlace.Properties.Appearance.Options.UseBackColor = true;
            this.rdoSubTickPlace.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoSubTickPlace.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "在内部"), new RadioGroupItem(null, "在外部") });
            this.rdoSubTickPlace.Size = new Size(0x48, 40);
            this.rdoSubTickPlace.TabIndex = 14;
            this.rdoSubTickPlace.EditValueChanged += new EventHandler(this.rdoTickPalce_SelectedIndexChanged);
            this.btnSubStyle.Location = new Point(0x38, 80);
            this.btnSubStyle.Name = "btnSubStyle";
            this.btnSubStyle.Size = new Size(0x40, 0x18);
            this.btnSubStyle.Style = null;
            this.btnSubStyle.TabIndex = 13;
            this.btnSubStyle.Click += new EventHandler(this.btnSubStyle_Click);
            this.label7.AutoSize = true;
            this.label7.Location = new Point(8, 120);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x36, 0x11);
            this.label7.TabIndex = 12;
            this.label7.Text = "显示刻度";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(8, 0x58);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x23, 0x11);
            this.label8.TabIndex = 11;
            this.label8.Text = "符号:";
            this.chkSubRight.Location = new Point(0xb0, 0x18);
            this.chkSubRight.Name = "chkSubRight";
            this.chkSubRight.Properties.Caption = "右";
            this.chkSubRight.Size = new Size(0x30, 0x13);
            this.chkSubRight.TabIndex = 7;
            this.chkSubRight.CheckedChanged += new EventHandler(this.chkMainTop_CheckedChanged);
            this.chkSubBottom.Location = new Point(120, 0x18);
            this.chkSubBottom.Name = "chkSubBottom";
            this.chkSubBottom.Properties.Caption = "下";
            this.chkSubBottom.Size = new Size(0x30, 0x13);
            this.chkSubBottom.TabIndex = 6;
            this.chkSubBottom.CheckedChanged += new EventHandler(this.chkMainTop_CheckedChanged);
            this.chkSubLeft.Location = new Point(0x40, 0x18);
            this.chkSubLeft.Name = "chkSubLeft";
            this.chkSubLeft.Properties.Caption = "左";
            this.chkSubLeft.Size = new Size(0x30, 0x13);
            this.chkSubLeft.TabIndex = 5;
            this.chkSubLeft.CheckedChanged += new EventHandler(this.chkMainTop_CheckedChanged);
            this.chkSubTop.Location = new Point(8, 0x18);
            this.chkSubTop.Name = "chkSubTop";
            this.chkSubTop.Properties.Caption = "上";
            this.chkSubTop.Size = new Size(0x30, 0x13);
            this.chkSubTop.TabIndex = 4;
            this.chkSubTop.CheckedChanged += new EventHandler(this.chkMainTop_CheckedChanged);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.cboBorderType);
            this.groupBox3.Location = new Point(8, 0x138);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x108, 80);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "边框属性";
            this.label10.AutoSize = true;
            this.label10.Location = new Point(8, 0x20);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x23, 0x11);
            this.label10.TabIndex = 12;
            this.label10.Text = "边框:";
            this.cboBorderType.EditValue = "";
            this.cboBorderType.Location = new Point(0x30, 0x20);
            this.cboBorderType.Name = "cboBorderType";
            this.cboBorderType.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboBorderType.Properties.Items.AddRange(new object[] { "简单的边框", "校准的边框" });
            this.cboBorderType.Size = new Size(0x70, 0x17);
            this.cboBorderType.TabIndex = 0;
            this.cboBorderType.SelectedIndexChanged += new EventHandler(this.cboBorderType_SelectedIndexChanged);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "GridAxisPropertyPage";
            base.Size = new Size(0x128, 400);
            base.Load += new EventHandler(this.GridAxisPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.txtTickLength.Properties.EndInit();
            this.rdoTickPalce.Properties.EndInit();
            this.chkMainRight.Properties.EndInit();
            this.chkMainBottom.Properties.EndInit();
            this.chkMainLeft.Properties.EndInit();
            this.chkMainTop.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.txtSubCount.Properties.EndInit();
            this.txtSubTickLength.Properties.EndInit();
            this.rdoSubTickPlace.Properties.EndInit();
            this.chkSubRight.Properties.EndInit();
            this.chkSubBottom.Properties.EndInit();
            this.chkSubLeft.Properties.EndInit();
            this.chkSubTop.Properties.EndInit();
            this.groupBox3.ResumeLayout(false);
            this.cboBorderType.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void rdoTickPalce_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        public void SetObjects(object @object)
        {
            if (@object is IMapGrid)
            {
                m_pMapGrid = @object as IMapGrid;
                this.m_pBorder = m_pMapGrid.Border;
            }
        }

        private void txtTickLength_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.m_IsPageDirty;
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
                return this.m_Title;
            }
            set
            {
                this.m_Title = value;
            }
        }
    }
}

