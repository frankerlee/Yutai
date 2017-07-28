namespace Yutai.Pipeline.Editor.Classes
{
    class FeatureSearch : IFeatureSearch
    {
        private double _tolerance = 0.0001;

        public FeatureSearch()
        {
        }

        public double Tolerance
        {
            get { return _tolerance; }
            set { _tolerance = value; }
        }
    }
}
