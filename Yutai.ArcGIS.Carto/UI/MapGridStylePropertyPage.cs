using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class MapGridStylePropertyPage : UserControl
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private StyleButton btnGraticulestyle;
        private StyleButton btnIndexGridStyle;
        private StyleButton btnMeasuredGridStyle;
        private SimpleButton btnSpatialReference;
        private Container container_0 = null;
        private GroupBox groupBoxCoordinate;
        private GroupBox groupBoxGraticuleSpace;
        private GroupBox groupBoxGraticuleStyle;
        private GroupBox groupBoxIndexGridSpace;
        private GroupBox groupBoxIndexGridStyle;
        private GroupBox groupBoxMeasuredGridSpace;
        private GroupBox groupBoxMeasuredGridStyle;
        private IMapFrame imapFrame_0 = null;
        private IMapGrid imapGrid_0 = null;
        private IStyleGallery istyleGallery_0 = ApplicationBase.StyleGallery;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label16;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label lblXUnit;
        private Label lblYUnit;
        private RadioGroup rdoGraticuleStyle;
        private RadioGroup rdoIndexGridStyle;
        private RadioGroup rdoMeasuredGridStyle;
        private TextEdit txtColumnCount;
        private TextEdit txtHatchIntervalXDegree;
        private TextEdit txtHatchIntervalXMinute;
        private TextEdit txtHatchIntervalXSecond;
        private TextEdit txtHatchIntervalYDegree;
        private TextEdit txtHatchIntervalYMinute;
        private TextEdit txtHatchIntervalYSecond;
        private TextEdit txtMeasuredGridXSpace;
        private TextEdit txtMeasuredGridYSpace;
        private TextEdit txtRowCount;
        private MemoEdit txtSpatialReference;

        public MapGridStylePropertyPage()
        {
            this.InitializeComponent();
        }

        private void btnGraticulestyle_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.istyleGallery_0);
                    selector.SetSymbol(this.imapGrid_0.LineSymbol);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        this.imapGrid_0.LineSymbol = selector.GetSymbol() as ILineSymbol;
                    }
                }
            }
            catch
            {
            }
        }

        private void btnIndexGridStyle_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.istyleGallery_0);
                    selector.SetSymbol(this.imapGrid_0.LineSymbol);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        this.imapGrid_0.LineSymbol = selector.GetSymbol() as ILineSymbol;
                    }
                }
            }
            catch
            {
            }
        }

        private void btnMeasuredGridStyle_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.istyleGallery_0);
                    if (this.rdoMeasuredGridStyle.SelectedIndex == 2)
                    {
                        selector.SetSymbol(this.imapGrid_0.LineSymbol);
                        if (selector.ShowDialog() == DialogResult.OK)
                        {
                            this.imapGrid_0.LineSymbol = selector.GetSymbol() as ILineSymbol;
                        }
                    }
                    else if (this.rdoMeasuredGridStyle.SelectedIndex == 1)
                    {
                        selector.SetSymbol(this.imapGrid_0.TickMarkSymbol);
                        if (selector.ShowDialog() == DialogResult.OK)
                        {
                            this.imapGrid_0.TickMarkSymbol = selector.GetSymbol() as IMarkerSymbol;
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void btnSpatialReference_Click(object sender, EventArgs e)
        {
            if (this.txtSpatialReference.Tag != null)
            {
                frmSpatialReference reference = new frmSpatialReference {
                    SpatialRefrence = this.txtSpatialReference.Tag as ISpatialReference
                };
                if (reference.ShowDialog() == DialogResult.OK)
                {
                    this.txtSpatialReference.Tag = reference.SpatialRefrence;
                    this.txtSpatialReference.Text = reference.SpatialRefrence.Name;
                }
            }
        }

        public void DegreeToDMS(double double_0, out double double_1, out double double_2, out double double_3)
        {
            Math.Sign(double_0);
            double_0 = Math.Abs(double_0);
            double_1 = Math.Floor(double_0);
            double_0 = (double_0 - double_1) * 60.0;
            double_2 = Math.Floor(double_0);
            double_3 = (double_0 - double_2) * 60.0;
        }

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_2);
        }

        public bool Do()
        {
            double num6;
            double num7;
            if (this.imapGrid_0 is IIndexGrid)
            {
                try
                {
                    int num = int.Parse(this.txtRowCount.Text);
                    (this.imapGrid_0 as IIndexGrid).RowCount = num;
                }
                catch
                {
                }
                try
                {
                    int num2 = int.Parse(this.txtColumnCount.Text);
                    (this.imapGrid_0 as IIndexGrid).ColumnCount = num2;
                    goto Label_023F;
                }
                catch
                {
                    goto Label_023F;
                }
            }
            if (this.imapGrid_0 is IGraticule)
            {
                double num3;
                double num4;
                double num5;
                try
                {
                    num3 = double.Parse(this.txtHatchIntervalXDegree.Text);
                    num4 = double.Parse(this.txtHatchIntervalXMinute.Text);
                    num5 = double.Parse(this.txtHatchIntervalXSecond.Text);
                }
                catch
                {
                    MessageBox.Show("数据格式错误!");
                    return false;
                }
                if (num3 < 0.0)
                {
                    num6 = (num3 - (num4 / 60.0)) - (num5 / 3600.0);
                }
                else
                {
                    num6 = (num3 + (num4 / 60.0)) + (num5 / 3600.0);
                }
                try
                {
                    num3 = double.Parse(this.txtHatchIntervalYDegree.Text);
                    num4 = double.Parse(this.txtHatchIntervalYMinute.Text);
                    num5 = double.Parse(this.txtHatchIntervalYSecond.Text);
                }
                catch
                {
                    MessageBox.Show("数据格式错误!");
                    return false;
                }
                if (num3 < 0.0)
                {
                    num7 = (num3 - (num4 / 60.0)) - (num5 / 3600.0);
                }
                else
                {
                    num7 = (num3 + (num4 / 60.0)) + (num5 / 3600.0);
                }
                (this.imapGrid_0 as IMeasuredGrid).XIntervalSize = num6;
                (this.imapGrid_0 as IMeasuredGrid).YIntervalSize = num7;
            }
            else if (this.imapGrid_0 is IMeasuredGrid)
            {
                try
                {
                    num6 = double.Parse(this.txtMeasuredGridXSpace.Text);
                    num7 = double.Parse(this.txtMeasuredGridYSpace.Text);
                }
                catch
                {
                    MessageBox.Show("数据格式错误!");
                    return false;
                }
                (this.imapGrid_0 as IMeasuredGrid).XIntervalSize = num6;
                (this.imapGrid_0 as IMeasuredGrid).YIntervalSize = num7;
            }
        Label_023F:
            return true;
        }

        public void Init()
        {
            this.bool_0 = false;
            if (this.imapGrid_0 is IIndexGrid)
            {
                this.groupBoxIndexGridSpace.Visible = true;
                this.groupBoxIndexGridStyle.Visible = true;
                this.groupBoxGraticuleSpace.Visible = false;
                this.groupBoxGraticuleStyle.Visible = false;
                this.groupBoxMeasuredGridStyle.Visible = false;
                this.groupBoxMeasuredGridSpace.Visible = false;
                this.groupBoxCoordinate.Visible = false;
                this.txtRowCount.Text = (this.imapGrid_0 as IIndexGrid).RowCount.ToString();
                this.txtColumnCount.Text = (this.imapGrid_0 as IIndexGrid).ColumnCount.ToString();
                if (this.imapGrid_0.LineSymbol != null)
                {
                    this.rdoIndexGridStyle.SelectedIndex = 1;
                    this.btnIndexGridStyle.Style = this.imapGrid_0.LineSymbol;
                    this.btnIndexGridStyle.Enabled = true;
                }
                else
                {
                    this.rdoMeasuredGridStyle.SelectedIndex = 0;
                    this.btnIndexGridStyle.Style = null;
                    this.btnIndexGridStyle.Enabled = false;
                }
            }
            else if (this.imapGrid_0 is IGraticule)
            {
                double num4;
                double num5;
                double num6;
                this.groupBoxIndexGridSpace.Visible = false;
                this.groupBoxIndexGridStyle.Visible = false;
                this.groupBoxGraticuleSpace.Visible = true;
                this.groupBoxGraticuleStyle.Visible = true;
                this.groupBoxMeasuredGridStyle.Visible = false;
                this.groupBoxMeasuredGridSpace.Visible = false;
                this.groupBoxCoordinate.Visible = false;
                double xIntervalSize = (this.imapGrid_0 as IMeasuredGrid).XIntervalSize;
                double yIntervalSize = (this.imapGrid_0 as IMeasuredGrid).YIntervalSize;
                this.DegreeToDMS(xIntervalSize, out num4, out num5, out num6);
                this.txtHatchIntervalXDegree.Text = num4.ToString();
                this.txtHatchIntervalXMinute.Text = num5.ToString("00");
                this.txtHatchIntervalXSecond.Text = num6.ToString("00.##");
                this.DegreeToDMS(yIntervalSize, out num4, out num5, out num6);
                this.txtHatchIntervalYDegree.Text = num4.ToString();
                this.txtHatchIntervalYMinute.Text = num5.ToString("00");
                this.txtHatchIntervalYSecond.Text = num6.ToString("00.##");
                this.btnGraticulestyle.Enabled = true;
                if (this.imapGrid_0.LineSymbol != null)
                {
                    this.rdoGraticuleStyle.SelectedIndex = 2;
                    this.btnGraticulestyle.Style = this.imapGrid_0.LineSymbol;
                }
                else if (this.imapGrid_0.TickMarkSymbol != null)
                {
                    this.rdoGraticuleStyle.SelectedIndex = 1;
                    this.btnGraticulestyle.Style = this.imapGrid_0.TickMarkSymbol;
                }
                else
                {
                    this.rdoGraticuleStyle.SelectedIndex = 0;
                    this.btnGraticulestyle.Style = null;
                    this.btnGraticulestyle.Enabled = false;
                }
            }
            else if (this.imapGrid_0 is IMeasuredGrid)
            {
                this.groupBoxIndexGridSpace.Visible = false;
                this.groupBoxIndexGridStyle.Visible = false;
                this.groupBoxGraticuleSpace.Visible = false;
                this.groupBoxGraticuleStyle.Visible = false;
                this.groupBoxMeasuredGridStyle.Visible = true;
                this.groupBoxMeasuredGridSpace.Visible = true;
                this.groupBoxCoordinate.Visible = true;
                if ((this.imapGrid_0 as IProjectedGrid).SpatialReference != null)
                {
                    this.txtSpatialReference.Text = (this.imapGrid_0 as IProjectedGrid).SpatialReference.Name;
                    this.txtSpatialReference.Tag = (this.imapGrid_0 as IProjectedGrid).SpatialReference;
                }
                else if (this.imapFrame_0.Map.SpatialReference != null)
                {
                    this.txtSpatialReference.Tag = this.imapFrame_0.Map.SpatialReference;
                    this.txtSpatialReference.Text = "<与数据框架相同>\r\n" + this.imapFrame_0.Map.SpatialReference.Name;
                }
                else
                {
                    this.txtSpatialReference.Text = "<无坐标系>";
                    this.txtSpatialReference.Tag = null;
                }
                this.txtMeasuredGridXSpace.Text = (this.imapGrid_0 as IMeasuredGrid).XIntervalSize.ToString("0.###");
                this.txtMeasuredGridYSpace.Text = (this.imapGrid_0 as IMeasuredGrid).YIntervalSize.ToString("0.###");
                this.btnMeasuredGridStyle.Enabled = true;
                if (this.imapGrid_0.LineSymbol != null)
                {
                    this.rdoMeasuredGridStyle.SelectedIndex = 2;
                    this.btnMeasuredGridStyle.Style = this.imapGrid_0.LineSymbol;
                }
                else if (this.imapGrid_0.TickMarkSymbol != null)
                {
                    this.rdoMeasuredGridStyle.SelectedIndex = 1;
                    this.btnMeasuredGridStyle.Style = this.imapGrid_0.TickMarkSymbol;
                }
                else
                {
                    this.rdoMeasuredGridStyle.SelectedIndex = 0;
                    this.btnMeasuredGridStyle.Style = null;
                    this.btnMeasuredGridStyle.Enabled = false;
                }
            }
            this.bool_0 = true;
        }

        private void InitializeComponent()
        {
            this.groupBoxMeasuredGridSpace = new GroupBox();
            this.lblYUnit = new Label();
            this.lblXUnit = new Label();
            this.txtMeasuredGridYSpace = new TextEdit();
            this.txtMeasuredGridXSpace = new TextEdit();
            this.label2 = new Label();
            this.label1 = new Label();
            this.groupBoxGraticuleStyle = new GroupBox();
            this.label14 = new Label();
            this.btnGraticulestyle = new StyleButton();
            this.rdoGraticuleStyle = new RadioGroup();
            this.groupBoxMeasuredGridStyle = new GroupBox();
            this.label15 = new Label();
            this.btnMeasuredGridStyle = new StyleButton();
            this.rdoMeasuredGridStyle = new RadioGroup();
            this.groupBoxCoordinate = new GroupBox();
            this.btnSpatialReference = new SimpleButton();
            this.txtSpatialReference = new MemoEdit();
            this.groupBoxGraticuleSpace = new GroupBox();
            this.label9 = new Label();
            this.label8 = new Label();
            this.label7 = new Label();
            this.txtHatchIntervalYSecond = new TextEdit();
            this.txtHatchIntervalYMinute = new TextEdit();
            this.txtHatchIntervalXSecond = new TextEdit();
            this.txtHatchIntervalXMinute = new TextEdit();
            this.label3 = new Label();
            this.label4 = new Label();
            this.txtHatchIntervalYDegree = new TextEdit();
            this.txtHatchIntervalXDegree = new TextEdit();
            this.label5 = new Label();
            this.label6 = new Label();
            this.groupBoxIndexGridSpace = new GroupBox();
            this.label10 = new Label();
            this.label11 = new Label();
            this.txtRowCount = new TextEdit();
            this.txtColumnCount = new TextEdit();
            this.label12 = new Label();
            this.label13 = new Label();
            this.groupBoxIndexGridStyle = new GroupBox();
            this.label16 = new Label();
            this.btnIndexGridStyle = new StyleButton();
            this.rdoIndexGridStyle = new RadioGroup();
            this.groupBoxMeasuredGridSpace.SuspendLayout();
            this.txtMeasuredGridYSpace.Properties.BeginInit();
            this.txtMeasuredGridXSpace.Properties.BeginInit();
            this.groupBoxGraticuleStyle.SuspendLayout();
            this.rdoGraticuleStyle.Properties.BeginInit();
            this.groupBoxMeasuredGridStyle.SuspendLayout();
            this.rdoMeasuredGridStyle.Properties.BeginInit();
            this.groupBoxCoordinate.SuspendLayout();
            this.txtSpatialReference.Properties.BeginInit();
            this.groupBoxGraticuleSpace.SuspendLayout();
            this.txtHatchIntervalYSecond.Properties.BeginInit();
            this.txtHatchIntervalYMinute.Properties.BeginInit();
            this.txtHatchIntervalXSecond.Properties.BeginInit();
            this.txtHatchIntervalXMinute.Properties.BeginInit();
            this.txtHatchIntervalYDegree.Properties.BeginInit();
            this.txtHatchIntervalXDegree.Properties.BeginInit();
            this.groupBoxIndexGridSpace.SuspendLayout();
            this.txtRowCount.Properties.BeginInit();
            this.txtColumnCount.Properties.BeginInit();
            this.groupBoxIndexGridStyle.SuspendLayout();
            this.rdoIndexGridStyle.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBoxMeasuredGridSpace.Controls.Add(this.lblYUnit);
            this.groupBoxMeasuredGridSpace.Controls.Add(this.lblXUnit);
            this.groupBoxMeasuredGridSpace.Controls.Add(this.txtMeasuredGridYSpace);
            this.groupBoxMeasuredGridSpace.Controls.Add(this.txtMeasuredGridXSpace);
            this.groupBoxMeasuredGridSpace.Controls.Add(this.label2);
            this.groupBoxMeasuredGridSpace.Controls.Add(this.label1);
            this.groupBoxMeasuredGridSpace.Location = new System.Drawing.Point(8, 0xb0);
            this.groupBoxMeasuredGridSpace.Name = "groupBoxMeasuredGridSpace";
            this.groupBoxMeasuredGridSpace.Size = new Size(240, 80);
            this.groupBoxMeasuredGridSpace.TabIndex = 3;
            this.groupBoxMeasuredGridSpace.TabStop = false;
            this.groupBoxMeasuredGridSpace.Text = "间隔";
            this.lblYUnit.AutoSize = true;
            this.lblYUnit.Location = new System.Drawing.Point(0x98, 0x30);
            this.lblYUnit.Name = "lblYUnit";
            this.lblYUnit.Size = new Size(0, 0x11);
            this.lblYUnit.TabIndex = 5;
            this.lblXUnit.AutoSize = true;
            this.lblXUnit.Location = new System.Drawing.Point(0x98, 0x18);
            this.lblXUnit.Name = "lblXUnit";
            this.lblXUnit.Size = new Size(0, 0x11);
            this.lblXUnit.TabIndex = 4;
            this.txtMeasuredGridYSpace.EditValue = "";
            this.txtMeasuredGridYSpace.Location = new System.Drawing.Point(40, 0x30);
            this.txtMeasuredGridYSpace.Name = "txtMeasuredGridYSpace";
            this.txtMeasuredGridYSpace.Size = new Size(0x68, 0x17);
            this.txtMeasuredGridYSpace.TabIndex = 3;
            this.txtMeasuredGridXSpace.EditValue = "";
            this.txtMeasuredGridXSpace.Location = new System.Drawing.Point(40, 0x18);
            this.txtMeasuredGridXSpace.Name = "txtMeasuredGridXSpace";
            this.txtMeasuredGridXSpace.Size = new Size(0x68, 0x17);
            this.txtMeasuredGridXSpace.TabIndex = 2;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 0x34);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 0x11);
            this.label2.TabIndex = 1;
            this.label2.Text = "Y轴:";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 0x1d);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "X轴:";
            this.groupBoxGraticuleStyle.Controls.Add(this.label14);
            this.groupBoxGraticuleStyle.Controls.Add(this.btnGraticulestyle);
            this.groupBoxGraticuleStyle.Controls.Add(this.rdoGraticuleStyle);
            this.groupBoxGraticuleStyle.Location = new System.Drawing.Point(8, 0x10);
            this.groupBoxGraticuleStyle.Name = "groupBoxGraticuleStyle";
            this.groupBoxGraticuleStyle.Size = new Size(240, 0x88);
            this.groupBoxGraticuleStyle.TabIndex = 2;
            this.groupBoxGraticuleStyle.TabStop = false;
            this.groupBoxGraticuleStyle.Text = "外观";
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(0x90, 0x18);
            this.label14.Name = "label14";
            this.label14.Size = new Size(0x1d, 0x11);
            this.label14.TabIndex = 4;
            this.label14.Text = "样式";
            this.btnGraticulestyle.Location = new System.Drawing.Point(0x90, 0x30);
            this.btnGraticulestyle.Name = "btnGraticulestyle";
            this.btnGraticulestyle.Size = new Size(0x48, 0x19);
            this.btnGraticulestyle.Style = null;
            this.btnGraticulestyle.TabIndex = 3;
            this.btnGraticulestyle.Click += new EventHandler(this.btnGraticulestyle_Click);
            this.rdoGraticuleStyle.Location = new System.Drawing.Point(0x10, 0x18);
            this.rdoGraticuleStyle.Name = "rdoGraticuleStyle";
            this.rdoGraticuleStyle.Properties.Appearance.BackColor = SystemColors.InactiveBorder;
            this.rdoGraticuleStyle.Properties.Appearance.Options.UseBackColor = true;
            this.rdoGraticuleStyle.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoGraticuleStyle.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "只显示标注"), new RadioGroupItem(null, "刻度标志和标记"), new RadioGroupItem(null, "经纬网格和标注") });
            this.rdoGraticuleStyle.Size = new Size(160, 0x68);
            this.rdoGraticuleStyle.TabIndex = 0;
            this.rdoGraticuleStyle.SelectedIndexChanged += new EventHandler(this.rdoGraticuleStyle_SelectedIndexChanged);
            this.groupBoxMeasuredGridStyle.Controls.Add(this.label15);
            this.groupBoxMeasuredGridStyle.Controls.Add(this.btnMeasuredGridStyle);
            this.groupBoxMeasuredGridStyle.Controls.Add(this.rdoMeasuredGridStyle);
            this.groupBoxMeasuredGridStyle.Location = new System.Drawing.Point(8, 0x10);
            this.groupBoxMeasuredGridStyle.Name = "groupBoxMeasuredGridStyle";
            this.groupBoxMeasuredGridStyle.Size = new Size(240, 0x58);
            this.groupBoxMeasuredGridStyle.TabIndex = 4;
            this.groupBoxMeasuredGridStyle.TabStop = false;
            this.groupBoxMeasuredGridStyle.Text = "外观";
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(0x90, 0x10);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x1d, 0x11);
            this.label15.TabIndex = 5;
            this.label15.Text = "样式";
            this.btnMeasuredGridStyle.Location = new System.Drawing.Point(0x90, 40);
            this.btnMeasuredGridStyle.Name = "btnMeasuredGridStyle";
            this.btnMeasuredGridStyle.Size = new Size(0x48, 0x19);
            this.btnMeasuredGridStyle.Style = null;
            this.btnMeasuredGridStyle.TabIndex = 3;
            this.btnMeasuredGridStyle.Click += new EventHandler(this.btnMeasuredGridStyle_Click);
            this.rdoMeasuredGridStyle.Location = new System.Drawing.Point(8, 0x10);
            this.rdoMeasuredGridStyle.Name = "rdoMeasuredGridStyle";
            this.rdoMeasuredGridStyle.Properties.Appearance.BackColor = SystemColors.InactiveBorder;
            this.rdoMeasuredGridStyle.Properties.Appearance.Options.UseBackColor = true;
            this.rdoMeasuredGridStyle.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoMeasuredGridStyle.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "只显示标注"), new RadioGroupItem(null, "刻度标志和标记"), new RadioGroupItem(null, "格网和标注") });
            this.rdoMeasuredGridStyle.Size = new Size(160, 0x40);
            this.rdoMeasuredGridStyle.TabIndex = 0;
            this.rdoMeasuredGridStyle.SelectedIndexChanged += new EventHandler(this.rdoMeasuredGridStyle_SelectedIndexChanged);
            this.groupBoxCoordinate.Controls.Add(this.btnSpatialReference);
            this.groupBoxCoordinate.Controls.Add(this.txtSpatialReference);
            this.groupBoxCoordinate.Location = new System.Drawing.Point(8, 0x6b);
            this.groupBoxCoordinate.Name = "groupBoxCoordinate";
            this.groupBoxCoordinate.Size = new Size(240, 0x40);
            this.groupBoxCoordinate.TabIndex = 5;
            this.groupBoxCoordinate.TabStop = false;
            this.groupBoxCoordinate.Text = "坐标系";
            this.btnSpatialReference.Location = new System.Drawing.Point(160, 0x18);
            this.btnSpatialReference.Name = "btnSpatialReference";
            this.btnSpatialReference.Size = new Size(0x30, 0x18);
            this.btnSpatialReference.TabIndex = 1;
            this.btnSpatialReference.Text = "属性...";
            this.btnSpatialReference.Click += new EventHandler(this.btnSpatialReference_Click);
            this.txtSpatialReference.EditValue = "";
            this.txtSpatialReference.Location = new System.Drawing.Point(0x10, 0x10);
            this.txtSpatialReference.Name = "txtSpatialReference";
            this.txtSpatialReference.Size = new Size(0x80, 40);
            this.txtSpatialReference.TabIndex = 0;
            this.groupBoxGraticuleSpace.Controls.Add(this.label9);
            this.groupBoxGraticuleSpace.Controls.Add(this.label8);
            this.groupBoxGraticuleSpace.Controls.Add(this.label7);
            this.groupBoxGraticuleSpace.Controls.Add(this.txtHatchIntervalYSecond);
            this.groupBoxGraticuleSpace.Controls.Add(this.txtHatchIntervalYMinute);
            this.groupBoxGraticuleSpace.Controls.Add(this.txtHatchIntervalXSecond);
            this.groupBoxGraticuleSpace.Controls.Add(this.txtHatchIntervalXMinute);
            this.groupBoxGraticuleSpace.Controls.Add(this.label3);
            this.groupBoxGraticuleSpace.Controls.Add(this.label4);
            this.groupBoxGraticuleSpace.Controls.Add(this.txtHatchIntervalYDegree);
            this.groupBoxGraticuleSpace.Controls.Add(this.txtHatchIntervalXDegree);
            this.groupBoxGraticuleSpace.Controls.Add(this.label5);
            this.groupBoxGraticuleSpace.Controls.Add(this.label6);
            this.groupBoxGraticuleSpace.Location = new System.Drawing.Point(8, 0x98);
            this.groupBoxGraticuleSpace.Name = "groupBoxGraticuleSpace";
            this.groupBoxGraticuleSpace.Size = new Size(240, 0x68);
            this.groupBoxGraticuleSpace.TabIndex = 6;
            this.groupBoxGraticuleSpace.TabStop = false;
            this.groupBoxGraticuleSpace.Text = "间隔";
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(0xb8, 20);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x11, 0x11);
            this.label9.TabIndex = 12;
            this.label9.Text = "秒";
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(0x90, 20);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x11, 0x11);
            this.label8.TabIndex = 11;
            this.label8.Text = "分";
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(0x63, 20);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x11, 0x11);
            this.label7.TabIndex = 10;
            this.label7.Text = "度";
            this.txtHatchIntervalYSecond.EditValue = "";
            this.txtHatchIntervalYSecond.Location = new System.Drawing.Point(0xa8, 0x48);
            this.txtHatchIntervalYSecond.Name = "txtHatchIntervalYSecond";
            this.txtHatchIntervalYSecond.Size = new Size(0x20, 0x17);
            this.txtHatchIntervalYSecond.TabIndex = 9;
            this.txtHatchIntervalYMinute.EditValue = "";
            this.txtHatchIntervalYMinute.Location = new System.Drawing.Point(0x80, 0x48);
            this.txtHatchIntervalYMinute.Name = "txtHatchIntervalYMinute";
            this.txtHatchIntervalYMinute.Size = new Size(0x20, 0x17);
            this.txtHatchIntervalYMinute.TabIndex = 8;
            this.txtHatchIntervalXSecond.EditValue = "";
            this.txtHatchIntervalXSecond.Location = new System.Drawing.Point(0xa8, 40);
            this.txtHatchIntervalXSecond.Name = "txtHatchIntervalXSecond";
            this.txtHatchIntervalXSecond.Size = new Size(0x20, 0x17);
            this.txtHatchIntervalXSecond.TabIndex = 7;
            this.txtHatchIntervalXMinute.EditValue = "";
            this.txtHatchIntervalXMinute.Location = new System.Drawing.Point(0x80, 40);
            this.txtHatchIntervalXMinute.Name = "txtHatchIntervalXMinute";
            this.txtHatchIntervalXMinute.Size = new Size(0x20, 0x17);
            this.txtHatchIntervalXMinute.TabIndex = 6;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0xa8, 0x40);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0, 0x11);
            this.label3.TabIndex = 5;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0xa8, 40);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0, 0x11);
            this.label4.TabIndex = 4;
            this.txtHatchIntervalYDegree.EditValue = "";
            this.txtHatchIntervalYDegree.Location = new System.Drawing.Point(0x48, 0x48);
            this.txtHatchIntervalYDegree.Name = "txtHatchIntervalYDegree";
            this.txtHatchIntervalYDegree.Size = new Size(0x30, 0x17);
            this.txtHatchIntervalYDegree.TabIndex = 3;
            this.txtHatchIntervalXDegree.EditValue = "";
            this.txtHatchIntervalXDegree.Location = new System.Drawing.Point(0x48, 40);
            this.txtHatchIntervalXDegree.Name = "txtHatchIntervalXDegree";
            this.txtHatchIntervalXDegree.Size = new Size(0x30, 0x17);
            this.txtHatchIntervalXDegree.TabIndex = 2;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0x18, 0x48);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x1d, 0x11);
            this.label5.TabIndex = 1;
            this.label5.Text = "经度";
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(0x18, 0x2a);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x1d, 0x11);
            this.label6.TabIndex = 0;
            this.label6.Text = "纬度";
            this.groupBoxIndexGridSpace.Controls.Add(this.label10);
            this.groupBoxIndexGridSpace.Controls.Add(this.label11);
            this.groupBoxIndexGridSpace.Controls.Add(this.txtRowCount);
            this.groupBoxIndexGridSpace.Controls.Add(this.txtColumnCount);
            this.groupBoxIndexGridSpace.Controls.Add(this.label12);
            this.groupBoxIndexGridSpace.Controls.Add(this.label13);
            this.groupBoxIndexGridSpace.Location = new System.Drawing.Point(8, 160);
            this.groupBoxIndexGridSpace.Name = "groupBoxIndexGridSpace";
            this.groupBoxIndexGridSpace.Size = new Size(240, 80);
            this.groupBoxIndexGridSpace.TabIndex = 7;
            this.groupBoxIndexGridSpace.TabStop = false;
            this.groupBoxIndexGridSpace.Text = "间隔";
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(0x98, 0x30);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0, 0x11);
            this.label10.TabIndex = 5;
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(0x98, 0x18);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0, 0x11);
            this.label11.TabIndex = 4;
            this.txtRowCount.EditValue = "";
            this.txtRowCount.Location = new System.Drawing.Point(40, 0x30);
            this.txtRowCount.Name = "txtRowCount";
            this.txtRowCount.Size = new Size(0x68, 0x17);
            this.txtRowCount.TabIndex = 3;
            this.txtColumnCount.EditValue = "";
            this.txtColumnCount.Location = new System.Drawing.Point(40, 0x18);
            this.txtColumnCount.Name = "txtColumnCount";
            this.txtColumnCount.Size = new Size(0x68, 0x17);
            this.txtColumnCount.TabIndex = 2;
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 0x34);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x11, 0x11);
            this.label12.TabIndex = 1;
            this.label12.Text = "行";
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 0x1d);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x11, 0x11);
            this.label13.TabIndex = 0;
            this.label13.Text = "列";
            this.groupBoxIndexGridStyle.Controls.Add(this.label16);
            this.groupBoxIndexGridStyle.Controls.Add(this.btnIndexGridStyle);
            this.groupBoxIndexGridStyle.Controls.Add(this.rdoIndexGridStyle);
            this.groupBoxIndexGridStyle.Location = new System.Drawing.Point(8, 0x10);
            this.groupBoxIndexGridStyle.Name = "groupBoxIndexGridStyle";
            this.groupBoxIndexGridStyle.Size = new Size(240, 0x80);
            this.groupBoxIndexGridStyle.TabIndex = 8;
            this.groupBoxIndexGridStyle.TabStop = false;
            this.groupBoxIndexGridStyle.Text = "外观";
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(0x90, 0x20);
            this.label16.Name = "label16";
            this.label16.Size = new Size(0x1d, 0x11);
            this.label16.TabIndex = 5;
            this.label16.Text = "样式";
            this.btnIndexGridStyle.Location = new System.Drawing.Point(0x88, 0x40);
            this.btnIndexGridStyle.Name = "btnIndexGridStyle";
            this.btnIndexGridStyle.Size = new Size(0x48, 0x19);
            this.btnIndexGridStyle.Style = null;
            this.btnIndexGridStyle.TabIndex = 3;
            this.btnIndexGridStyle.Click += new EventHandler(this.btnIndexGridStyle_Click);
            this.rdoIndexGridStyle.Location = new System.Drawing.Point(8, 0x10);
            this.rdoIndexGridStyle.Name = "rdoIndexGridStyle";
            this.rdoIndexGridStyle.Properties.Appearance.BackColor = SystemColors.InactiveBorder;
            this.rdoIndexGridStyle.Properties.Appearance.Options.UseBackColor = true;
            this.rdoIndexGridStyle.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoIndexGridStyle.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "只显示索引标签"), new RadioGroupItem(null, "格网和索引标签") });
            this.rdoIndexGridStyle.Size = new Size(160, 0x58);
            this.rdoIndexGridStyle.TabIndex = 0;
            this.rdoIndexGridStyle.SelectedIndexChanged += new EventHandler(this.rdoIndexGridStyle_SelectedIndexChanged);
            base.Controls.Add(this.groupBoxIndexGridStyle);
            base.Controls.Add(this.groupBoxIndexGridSpace);
            base.Controls.Add(this.groupBoxGraticuleSpace);
            base.Controls.Add(this.groupBoxCoordinate);
            base.Controls.Add(this.groupBoxMeasuredGridStyle);
            base.Controls.Add(this.groupBoxMeasuredGridSpace);
            base.Controls.Add(this.groupBoxGraticuleStyle);
            base.Name = "MapGridStylePropertyPage";
            base.Size = new Size(0x110, 0x110);
            base.Load += new EventHandler(this.MapGridStylePropertyPage_Load);
            this.groupBoxMeasuredGridSpace.ResumeLayout(false);
            this.txtMeasuredGridYSpace.Properties.EndInit();
            this.txtMeasuredGridXSpace.Properties.EndInit();
            this.groupBoxGraticuleStyle.ResumeLayout(false);
            this.rdoGraticuleStyle.Properties.EndInit();
            this.groupBoxMeasuredGridStyle.ResumeLayout(false);
            this.rdoMeasuredGridStyle.Properties.EndInit();
            this.groupBoxCoordinate.ResumeLayout(false);
            this.txtSpatialReference.Properties.EndInit();
            this.groupBoxGraticuleSpace.ResumeLayout(false);
            this.txtHatchIntervalYSecond.Properties.EndInit();
            this.txtHatchIntervalYMinute.Properties.EndInit();
            this.txtHatchIntervalXSecond.Properties.EndInit();
            this.txtHatchIntervalXMinute.Properties.EndInit();
            this.txtHatchIntervalYDegree.Properties.EndInit();
            this.txtHatchIntervalXDegree.Properties.EndInit();
            this.groupBoxIndexGridSpace.ResumeLayout(false);
            this.txtRowCount.Properties.EndInit();
            this.txtColumnCount.Properties.EndInit();
            this.groupBoxIndexGridStyle.ResumeLayout(false);
            this.rdoIndexGridStyle.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void MapGridStylePropertyPage_Load(object sender, EventArgs e)
        {
            this.Init();
            this.bool_1 = true;
        }

        private void rdoGraticuleStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.btnGraticulestyle.Enabled = true;
                switch (this.rdoGraticuleStyle.SelectedIndex)
                {
                    case 0:
                        this.imapGrid_0.LineSymbol = null;
                        this.imapGrid_0.TickMarkSymbol = null;
                        this.btnGraticulestyle.Style = null;
                        this.btnGraticulestyle.Enabled = false;
                        break;

                    case 1:
                    {
                        this.imapGrid_0.LineSymbol = null;
                        ISimpleMarkerSymbol symbol2 = new SimpleMarkerSymbolClass {
                            Style = esriSimpleMarkerStyle.esriSMSCross
                        };
                        this.imapGrid_0.TickMarkSymbol = symbol2;
                        this.btnGraticulestyle.Style = this.imapGrid_0.TickMarkSymbol;
                        break;
                    }
                    default:
                    {
                        this.imapGrid_0.TickMarkSymbol = null;
                        ISimpleLineSymbol symbol = new SimpleLineSymbolClass {
                            Width = 1.0
                        };
                        this.imapGrid_0.LineSymbol = symbol;
                        this.btnGraticulestyle.Style = this.imapGrid_0.LineSymbol;
                        break;
                    }
                }
                this.btnGraticulestyle.Invalidate();
            }
        }

        private void rdoIndexGridStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.btnIndexGridStyle.Enabled = true;
                switch (this.rdoIndexGridStyle.SelectedIndex)
                {
                    case 0:
                        this.imapGrid_0.LineSymbol = null;
                        this.btnIndexGridStyle.Style = null;
                        this.btnIndexGridStyle.Enabled = false;
                        break;

                    case 1:
                    {
                        ISimpleLineSymbol symbol = new SimpleLineSymbolClass {
                            Width = 1.0
                        };
                        this.imapGrid_0.LineSymbol = symbol;
                        this.btnIndexGridStyle.Style = this.imapGrid_0.LineSymbol;
                        break;
                    }
                }
                this.btnIndexGridStyle.Invalidate();
            }
        }

        private void rdoMeasuredGridStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.btnMeasuredGridStyle.Enabled = true;
                switch (this.rdoMeasuredGridStyle.SelectedIndex)
                {
                    case 0:
                        this.imapGrid_0.LineSymbol = null;
                        this.imapGrid_0.TickMarkSymbol = null;
                        this.btnMeasuredGridStyle.Style = null;
                        this.btnMeasuredGridStyle.Enabled = false;
                        break;

                    case 1:
                    {
                        this.imapGrid_0.LineSymbol = null;
                        ISimpleMarkerSymbol symbol2 = new SimpleMarkerSymbolClass {
                            Style = esriSimpleMarkerStyle.esriSMSCross
                        };
                        this.imapGrid_0.TickMarkSymbol = symbol2;
                        this.btnMeasuredGridStyle.Style = this.imapGrid_0.TickMarkSymbol;
                        break;
                    }
                    default:
                    {
                        ISimpleLineSymbol symbol = new SimpleLineSymbolClass {
                            Width = 1.0
                        };
                        this.imapGrid_0.TickMarkSymbol = null;
                        this.imapGrid_0.LineSymbol = symbol;
                        this.btnMeasuredGridStyle.Style = this.imapGrid_0.LineSymbol;
                        break;
                    }
                }
                this.btnMeasuredGridStyle.Invalidate();
            }
        }

        public IMapFrame MapFrame
        {
            set
            {
                this.imapFrame_0 = value;
            }
        }

        public IMapGrid MapGrid
        {
            set
            {
                this.imapGrid_0 = value;
                if (this.bool_1)
                {
                    this.Init();
                }
            }
        }
    }
}

