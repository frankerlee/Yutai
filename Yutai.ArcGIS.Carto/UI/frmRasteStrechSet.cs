using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Carto.UI
{
    public class frmRasteStrechSet : Form
    {
        private Button btnOK;
        private Button button2;
        private CheckBox checkBox1;
        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private GroupBox groupBox1;
        private IContainer icontainer_0 = null;
        private IRasterStretch2 irasterStretch2_0 = null;
        private Label label1;
        private Label label2;
        private TabControl tabControl1;
        private TextBox textBox1;

        public frmRasteStrechSet()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.tabControl1.TabPages.Count; i++)
            {
                (this.tabControl1.TabPages[i].Controls[0] as StatisticsControl).Apply();
            }
            if (this.comboBox1.SelectedIndex == 2)
            {
                try
                {
                    this.irasterStretch2_0.StandardDeviationsParam = double.Parse(this.textBox1.Text);
                }
                catch
                {
                }
            }
            this.irasterStretch2_0.Invert = this.checkBox1.Checked;
            switch (this.comboBox1.SelectedIndex)
            {
                case 0:
                    this.irasterStretch2_0.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_NONE;
                    break;

                case 1:
                    this.irasterStretch2_0.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_Custom;
                    break;

                case 2:
                    this.irasterStretch2_0.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_StandardDeviations;
                    break;

                case 3:
                    this.irasterStretch2_0.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_HistogramEqualize;
                    break;

                case 4:
                    this.irasterStretch2_0.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_MinimumMaximum;
                    break;

                case 5:
                    this.irasterStretch2_0.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_HistogramSpecification;
                    break;
            }
            switch (this.comboBox2.SelectedIndex)
            {
                case 0:
                    this.irasterStretch2_0.StretchStatsType = esriRasterStretchStatsTypeEnum.esriRasterStretchStats_Dataset;
                    break;

                case 1:
                    this.irasterStretch2_0.StretchStatsType = esriRasterStretchStatsTypeEnum.esriRasterStretchStats_GlobalStats;
                    break;
            }
            base.DialogResult = DialogResult.OK;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.label2.Visible = this.comboBox1.SelectedIndex == 2;
            this.textBox1.Visible = this.comboBox1.SelectedIndex == 2;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.tabControl1.Visible = this.comboBox2.SelectedIndex == 1;
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmRasteStrechSet_Load(object sender, EventArgs e)
        {
            IArray stretchStats = this.irasterStretch2_0.StretchStats;
            for (int i = 0; i < stretchStats.Count; i++)
            {
                object obj2 = stretchStats.get_Element(i);
                if (obj2 is IStatsHistogram)
                {
                    StatisticsControl control = new StatisticsControl {
                        StatsHistogram = obj2 as IStatsHistogram,
                        Dock = DockStyle.Fill
                    };
                    TabPage page = new TabPage();
                    if (stretchStats.Count == 1)
                    {
                        page.Text = "统计";
                    }
                    else if (i == 0)
                    {
                        page.Text = "红";
                    }
                    else if (i == 1)
                    {
                        page.Text = "绿";
                    }
                    else
                    {
                        page.Text = "蓝";
                    }
                    page.Controls.Add(control);
                    this.tabControl1.TabPages.Add(page);
                }
            }
            this.textBox1.Text = this.irasterStretch2_0.StandardDeviationsParam.ToString();
            this.checkBox1.Checked = this.irasterStretch2_0.Invert;
            switch (this.irasterStretch2_0.StretchType)
            {
                case esriRasterStretchTypesEnum.esriRasterStretch_NONE:
                    this.comboBox1.SelectedIndex = 0;
                    break;

                case esriRasterStretchTypesEnum.esriRasterStretch_Custom:
                    this.comboBox1.SelectedIndex = 1;
                    break;

                case esriRasterStretchTypesEnum.esriRasterStretch_StandardDeviations:
                    this.comboBox1.SelectedIndex = 2;
                    break;

                case esriRasterStretchTypesEnum.esriRasterStretch_HistogramEqualize:
                    this.comboBox1.SelectedIndex = 3;
                    break;

                case esriRasterStretchTypesEnum.esriRasterStretch_MinimumMaximum:
                    this.comboBox1.SelectedIndex = 4;
                    break;

                case esriRasterStretchTypesEnum.esriRasterStretch_HistogramSpecification:
                    this.comboBox1.SelectedIndex = 5;
                    break;
            }
            switch (this.irasterStretch2_0.StretchStatsType)
            {
                case esriRasterStretchStatsTypeEnum.esriRasterStretchStats_Dataset:
                    this.comboBox2.SelectedIndex = 0;
                    break;

                case esriRasterStretchStatsTypeEnum.esriRasterStretchStats_GlobalStats:
                    this.comboBox2.SelectedIndex = 1;
                    break;
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRasteStrechSet));
            this.label1 = new Label();
            this.comboBox1 = new ComboBox();
            this.label2 = new Label();
            this.textBox1 = new TextBox();
            this.groupBox1 = new GroupBox();
            this.tabControl1 = new TabControl();
            this.comboBox2 = new ComboBox();
            this.checkBox1 = new CheckBox();
            this.btnOK = new Button();
            this.button2 = new Button();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "类型";
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] { "无", "自定义", "标准偏差", "等距直方图", "最小-最大值", "标准化直方图" });
            this.comboBox1.Location = new Point(0x4b, 13);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(0xcc, 20);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 0x2d);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "标注偏差";
            this.textBox1.Location = new Point(0x4b, 0x2a);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(100, 0x15);
            this.textBox1.TabIndex = 3;
            this.groupBox1.Controls.Add(this.comboBox2);
            this.groupBox1.Controls.Add(this.tabControl1);
            this.groupBox1.Location = new Point(14, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x101, 0xc9);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "统计信息";
            this.tabControl1.Location = new Point(10, 0x2c);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(0xeb, 0x97);
            this.tabControl1.TabIndex = 0;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] { "使用整个数据集", "采用以下设置" });
            this.comboBox2.Location = new Point(0x1d, 0x12);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new Size(0xab, 20);
            this.comboBox2.TabIndex = 2;
            this.comboBox2.SelectedIndexChanged += new EventHandler(this.comboBox2_SelectedIndexChanged);
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new Point(0xdf, 0x2c);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new Size(0x30, 0x10);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "反转";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.btnOK.Location = new Point(100, 0x11f);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.button2.DialogResult = DialogResult.Cancel;
            this.button2.Location = new Point(0xce, 0x11f);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x4b, 0x17);
            this.button2.TabIndex = 7;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            base.ClientSize = new Size(300, 0x142);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.checkBox1);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.comboBox1);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = ((System.Drawing.Icon)resources.GetObject("$Icon"));
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmRasteStrechSet";
            this.Text = "拉伸设置";
            base.Load += new EventHandler(this.frmRasteStrechSet_Load);
            this.groupBox1.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public IRasterStretch2 RasterStretch
        {
            set
            {
                this.irasterStretch2_0 = value;
            }
        }
    }
}

