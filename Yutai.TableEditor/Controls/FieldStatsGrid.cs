using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.Plugins.TableEditor.Controls
{
    public partial class FieldStatsGrid : UserControl
    {
        private IFeatureLayer _featureLayer;
        private string _fieldName;
        private List<double> _values;
        private string _result;

        public FieldStatsGrid()
        {
            InitializeComponent();
        }

        public IFeatureLayer FeatureLayer
        {
            get { return _featureLayer; }
            set { _featureLayer = value; }
        }

        public string FieldName
        {
            get { return _fieldName; }
            set { _fieldName = value; }
        }

        public int Count
        {
            get { return _values.Count; }
        }

        public double Max
        {
            get { return _values.Max(); }
        }

        public double Min
        {
            get { return _values.Min(); }
        }

        public double Sum
        {
            get { return _values.Sum(); }
        }

        public double Avg
        {
            get { return _values.Average(); }
        }

        public double StdDev
        {
            get { return CalculateStdDev(_values); }
        }

        public int None
        {
            get { return _featureLayer.FeatureClass.FeatureCount(null) - Count; }
        }

        public string Result
        {
            get { return _result; }
        }

        public void Statistics()
        {
            _values = new List<double>();
            int idx = _featureLayer.FeatureClass.FindField(_fieldName);
            if (idx <= -1)
                return;
            try
            {
                IQueryFilter pQueryFilter = new QueryFilterClass()
                {
                    SubFields = _fieldName
                };
                IFeatureCursor pFeatureCursor = _featureLayer.FeatureClass.Search(pQueryFilter, false);
                IFeature pFeature;
                while ((pFeature = pFeatureCursor.NextFeature()) != null)
                {
                    object obj = pFeature.Value[idx];
                    if (obj is DBNull)
                        continue;
                    double value = Convert.ToDouble(obj);
                    _values.Add(value);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public void ShowResult()
        {
            _result = $@"计数:	{Count}
最小值:	{Min}
最大值:	{Max}
总和:	{Sum}
平均值:	{Avg}
标准差:	{StdDev}
空:	{None}";
            txtResult.Text = _result;
        }

        private static double CalculateStdDev(List<double> values)
        {
            double ret = 0;
            if (values.Any())
            {
                //  计算平均数   
                double avg = values.Average();
                //  计算各数值与平均数的差值的平方，然后求和 
                double sum = values.Sum(d => Math.Pow(d - avg, 2));
                //  除以数量，然后开方
                ret = Math.Sqrt(sum/values.Count);
            }
            return ret;
        }
    }
}