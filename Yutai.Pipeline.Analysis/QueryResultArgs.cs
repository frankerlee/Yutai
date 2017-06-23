using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.Pipeline.Analysis
{
    public class QueryResultArgs : EventArgs
    {
        private IFeatureCursor _cursor;
        private IFeatureSelection _selection;
        public QueryResultArgs()
        {
            _cursor = null;
            _selection = null;
        }

        public QueryResultArgs(IFeatureCursor cursor, IFeatureSelection pSelection)
        {
            _cursor = cursor;
            _selection = pSelection;
        }
        public IFeatureCursor Cursor { get { return _cursor; } set { _cursor = value; } }
        public IFeatureSelection Selection { get { return _selection; } set { _selection = value; } }
    }
}