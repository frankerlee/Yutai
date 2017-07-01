using System;

namespace Yutai.ArcGIS.Common.ExtendClass
{
    public class BaseSeriesProperties : ISeriesProperties
    {
        public int Color
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public bool ColorEach
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string ColorMember
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public object DataSource
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string LabelMember
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string PercentFormat
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        internal Steema.TeeChart.Styles.Series Series { get; set; }

        public bool ShowInLegend
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string Title
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public BaseSeriesProperties()
        {
        }
    }
}