namespace Yutai.ArcGIS.Common.Overview
{
    public class OverviewLayerSetting
    {
        private double double_0 = 0.0;
        private double double_1 = 0.0;
        private string string_0 = "";

        public string LayerName
        {
            get { return this.string_0; }
            set { this.string_0 = value; }
        }

        public double MaxScale
        {
            get { return this.double_1; }
            set { this.double_1 = value; }
        }

        public double MinScale
        {
            get { return this.double_0; }
            set { this.double_0 = value; }
        }
    }
}