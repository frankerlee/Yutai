using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.DataSourcesRaster;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class StatisticsControl : UserControl
    {
        private IContainer icontainer_0 = null;
        private IStatsHistogram istatsHistogram_0 = null;

        public StatisticsControl()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            try
            {
                this.istatsHistogram_0.Min = double.Parse(this.textBox1.Text);
                this.istatsHistogram_0.Max = double.Parse(this.textBox2.Text);
                this.istatsHistogram_0.Mean = double.Parse(this.textBox3.Text);
                this.istatsHistogram_0.StdDev = double.Parse(this.textBox4.Text);
            }
            catch
            {
                return false;
            }
            return true;
        }

 private void StatisticsControl_Load(object sender, EventArgs e)
        {
            double num;
            double num2;
            double num3;
            double num4;
            this.istatsHistogram_0.HasStats();
            this.istatsHistogram_0.Update();
            this.istatsHistogram_0.QueryStats(out num, out num2, out num3, out num4);
            this.textBox1.Text = num.ToString();
            this.textBox2.Text = num2.ToString();
            this.textBox3.Text = num3.ToString();
            this.textBox4.Text = num4.ToString();
        }

        public IStatsHistogram StatsHistogram
        {
            set
            {
                this.istatsHistogram_0 = value;
            }
        }
    }
}

