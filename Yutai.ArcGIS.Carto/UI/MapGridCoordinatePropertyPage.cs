using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class MapGridCoordinatePropertyPage : UserControl
    {
        private bool bool_0 = false;
        private StyleButton btnMainLineStyle;
        private StyleButton btnSubLineStyle;
        private CheckEdit chkSubTickVisibility;
        private CheckEdit chkTickVisibility;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private IMapGrid imapGrid_0 = null;
        private IStyleGallery istyleGallery_0 = ApplicationBase.StyleGallery;
        private ITextSymbol itextSymbol_0 = new TextSymbolClass();
        private Label label1;
        private Label label2;
        private SpinEdit spinSubTickCount;
        private StyleButton styleButton1;

        public MapGridCoordinatePropertyPage()
        {
            this.InitializeComponent();
        }

        private void btnMainLineStyle_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.istyleGallery_0);
                    selector.SetSymbol(this.imapGrid_0.TickLineSymbol);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        this.imapGrid_0.TickLineSymbol = selector.GetSymbol() as ILineSymbol;
                    }
                }
            }
            catch
            {
            }
        }

        private void btnSubLineStyle_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.istyleGallery_0);
                    selector.SetSymbol(this.imapGrid_0.SubTickLineSymbol);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        this.imapGrid_0.SubTickLineSymbol = selector.GetSymbol() as ILineSymbol;
                    }
                }
            }
            catch
            {
            }
        }

        private void chkSubTickVisibility_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                bool leftVis = this.chkSubTickVisibility.Checked;
                this.imapGrid_0.SetSubTickVisibility(leftVis, leftVis, leftVis, leftVis);
                this.btnSubLineStyle.Enabled = leftVis;
                this.spinSubTickCount.Enabled = leftVis;
            }
        }

        private void chkTickVisibility_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                bool leftVis = this.chkTickVisibility.Checked;
                this.imapGrid_0.SetTickVisibility(leftVis, leftVis, leftVis, leftVis);
                this.btnMainLineStyle.Enabled = leftVis;
            }
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        public void Init()
        {
            this.bool_0 = false;
            if (this.imapGrid_0 != null)
            {
                this.btnMainLineStyle.Style = this.imapGrid_0.TickLineSymbol;
                this.btnSubLineStyle.Style = this.imapGrid_0.SubTickLineSymbol;
                bool leftVis = false;
                bool rightVis = false;
                bool topVis = false;
                bool bottomVis = false;
                this.imapGrid_0.QueryTickVisibility(ref leftVis, ref topVis, ref rightVis, ref bottomVis);
                this.chkTickVisibility.Checked = leftVis;
                this.btnMainLineStyle.Enabled = leftVis;
                this.imapGrid_0.QuerySubTickVisibility(ref leftVis, ref topVis, ref rightVis, ref bottomVis);
                this.chkSubTickVisibility.Checked = leftVis;
                this.btnSubLineStyle.Enabled = leftVis;
                this.spinSubTickCount.Enabled = leftVis;
                this.spinSubTickCount.Value = this.imapGrid_0.SubTickCount;
                IGridLabel labelFormat = this.imapGrid_0.LabelFormat;
                this.itextSymbol_0.Font = labelFormat.Font;
                this.itextSymbol_0.Color = labelFormat.Color;
                this.itextSymbol_0.Text = labelFormat.DisplayName;
                this.styleButton1.Style = this.itextSymbol_0;
                if (labelFormat is IMixedFontGridLabel)
                {
                    (labelFormat as IMixedFontGridLabel).NumGroupedDigits = 2;
                    this.imapGrid_0.LabelFormat = labelFormat;
                }
                if (labelFormat is IFormattedGridLabel)
                {
                    INumericFormat format = new NumericFormatClass {
                        RoundingValue = 0
                    };
                    (labelFormat as IFormattedGridLabel).Format = format as INumberFormat;
                    this.imapGrid_0.LabelFormat = labelFormat;
                }
            }
            this.bool_0 = true;
        }

        private void InitializeComponent()
        {
            this.groupBox2 = new GroupBox();
            this.styleButton1 = new StyleButton();
            this.label1 = new Label();
            this.groupBox1 = new GroupBox();
            this.spinSubTickCount = new SpinEdit();
            this.label2 = new Label();
            this.btnSubLineStyle = new StyleButton();
            this.btnMainLineStyle = new StyleButton();
            this.chkSubTickVisibility = new CheckEdit();
            this.chkTickVisibility = new CheckEdit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.spinSubTickCount.Properties.BeginInit();
            this.chkSubTickVisibility.Properties.BeginInit();
            this.chkTickVisibility.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox2.Controls.Add(this.styleButton1);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new Point(8, 0xb0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(240, 0x48);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "标注";
            this.styleButton1.Location = new Point(0x68, 0x20);
            this.styleButton1.Name = "styleButton1";
            this.styleButton1.Size = new Size(0x54, 0x20);
            this.styleButton1.Style = null;
            this.styleButton1.TabIndex = 3;
            this.styleButton1.Click += new EventHandler(this.styleButton1_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 0x27);
            this.label1.Name = "label1";
            this.label1.Size = new Size(60, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "文本样式:";
            this.groupBox1.Controls.Add(this.spinSubTickCount);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnSubLineStyle);
            this.groupBox1.Controls.Add(this.btnMainLineStyle);
            this.groupBox1.Controls.Add(this.chkSubTickVisibility);
            this.groupBox1.Controls.Add(this.chkTickVisibility);
            this.groupBox1.Location = new Point(8, 0x10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(240, 0x88);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "坐标轴";
            int[] bits = new int[4];
            this.spinSubTickCount.EditValue = new decimal(bits);
            this.spinSubTickCount.Location = new Point(0x70, 0x60);
            this.spinSubTickCount.Name = "spinSubTickCount";
            this.spinSubTickCount.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.spinSubTickCount.Properties.UseCtrlIncrement = false;
            this.spinSubTickCount.Size = new Size(0x48, 0x17);
            this.spinSubTickCount.TabIndex = 5;
            this.spinSubTickCount.EditValueChanged += new EventHandler(this.spinSubTickCount_EditValueChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x20, 0x68);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x4f, 0x11);
            this.label2.TabIndex = 4;
            this.label2.Text = "主分划刻度数";
            this.btnSubLineStyle.Location = new Point(0x68, 0x3d);
            this.btnSubLineStyle.Name = "btnSubLineStyle";
            this.btnSubLineStyle.Size = new Size(0x48, 0x18);
            this.btnSubLineStyle.Style = null;
            this.btnSubLineStyle.TabIndex = 3;
            this.btnSubLineStyle.Click += new EventHandler(this.btnSubLineStyle_Click);
            this.btnMainLineStyle.Location = new Point(0x68, 0x17);
            this.btnMainLineStyle.Name = "btnMainLineStyle";
            this.btnMainLineStyle.Size = new Size(0x48, 0x19);
            this.btnMainLineStyle.Style = null;
            this.btnMainLineStyle.TabIndex = 2;
            this.btnMainLineStyle.Click += new EventHandler(this.btnMainLineStyle_Click);
            this.chkSubTickVisibility.Location = new Point(0x10, 0x3e);
            this.chkSubTickVisibility.Name = "chkSubTickVisibility";
            this.chkSubTickVisibility.Properties.Caption = "次划分刻度";
            this.chkSubTickVisibility.Size = new Size(0x58, 0x13);
            this.chkSubTickVisibility.TabIndex = 1;
            this.chkSubTickVisibility.CheckedChanged += new EventHandler(this.chkSubTickVisibility_CheckedChanged);
            this.chkTickVisibility.Location = new Point(0x10, 0x19);
            this.chkTickVisibility.Name = "chkTickVisibility";
            this.chkTickVisibility.Properties.Caption = "主划分刻度";
            this.chkTickVisibility.Size = new Size(0x58, 0x13);
            this.chkTickVisibility.TabIndex = 0;
            this.chkTickVisibility.CheckedChanged += new EventHandler(this.chkTickVisibility_CheckedChanged);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "MapGridCoordinatePropertyPage";
            base.Size = new Size(0x148, 0x138);
            base.Load += new EventHandler(this.MapGridCoordinatePropertyPage_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.spinSubTickCount.Properties.EndInit();
            this.chkSubTickVisibility.Properties.EndInit();
            this.chkTickVisibility.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void MapGridCoordinatePropertyPage_Load(object sender, EventArgs e)
        {
        }

        private void spinSubTickCount_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    this.imapGrid_0.SubTickCount = (short) this.spinSubTickCount.Value;
                }
                catch
                {
                }
            }
        }

        private void styleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                selector.SetStyleGallery(this.istyleGallery_0);
                selector.SetSymbol(this.itextSymbol_0);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    object symbol = selector.GetSymbol();
                    this.itextSymbol_0 = symbol as ITextSymbol;
                    IGridLabel labelFormat = this.imapGrid_0.LabelFormat;
                    labelFormat.Font = this.itextSymbol_0.Font;
                    labelFormat.Color = this.itextSymbol_0.Color;
                    this.imapGrid_0.LabelFormat = labelFormat;
                    this.styleButton1.Style = this.itextSymbol_0;
                }
            }
            catch
            {
            }
        }

        public IMapGrid MapGrid
        {
            set
            {
                this.imapGrid_0 = value;
            }
        }
    }
}

