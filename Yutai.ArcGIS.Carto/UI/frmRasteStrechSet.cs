using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class frmRasteStrechSet : Form
    {
        private IContainer icontainer_0 = null;
        private IRasterStretch2 irasterStretch2_0 = null;

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
                    this.irasterStretch2_0.StretchType =
                        esriRasterStretchTypesEnum.esriRasterStretch_HistogramSpecification;
                    break;
            }
            switch (this.comboBox2.SelectedIndex)
            {
                case 0:
                    this.irasterStretch2_0.StretchStatsType =
                        esriRasterStretchStatsTypeEnum.esriRasterStretchStats_Dataset;
                    break;

                case 1:
                    this.irasterStretch2_0.StretchStatsType =
                        esriRasterStretchStatsTypeEnum.esriRasterStretchStats_GlobalStats;
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

        private void frmRasteStrechSet_Load(object sender, EventArgs e)
        {
            IArray stretchStats = this.irasterStretch2_0.StretchStats;
            for (int i = 0; i < stretchStats.Count; i++)
            {
                object obj2 = stretchStats.get_Element(i);
                if (obj2 is IStatsHistogram)
                {
                    StatisticsControl control = new StatisticsControl
                    {
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

        public IRasterStretch2 RasterStretch
        {
            set { this.irasterStretch2_0 = value; }
        }
    }
}