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
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.TableEditor.Views;

namespace Yutai.Plugins.TableEditor.Editor
{
    public partial class TableEditorGrid : UserControl, IGridView
    {
        private IActiveViewEvents_Event _activeViewEventsEvent;
        public TableEditorGrid(IAppContext context, ITableEditorView view)
        {
            InitializeComponent();
            AppContext = context;
            View = view;
            header.AppContext = context;
            header.View = view;
            virtualGrid.AppContext = context;
            virtualGrid.View = view;
            _activeViewEventsEvent = context.MapControl.Map as IActiveViewEvents_Event;
            if (_activeViewEventsEvent != null)
                _activeViewEventsEvent.SelectionChanged += _activeViewEventsEvent_SelectionChanged;
        }

        private void _activeViewEventsEvent_SelectionChanged()
        {
            IFeatureSelection pSelection = FeatureLayer as IFeatureSelection;
            if (pSelection == null || pSelection.SelectionSet == null)
                return;
            ICursor pCursor;
            pSelection.SelectionSet.Search(null, false, out pCursor);
            if (pCursor == null)
                return;
            IRow pRow;
            List<int> oids = new List<int>();
            while ((pRow = pCursor.NextRow()) != null)
            {
                oids.Add(pRow.OID);
            }
            virtualGrid.SelectionChanged(oids);
        }

        public int OID
        {
            get { return virtualGrid.CurrentOID; }
        }

        public IAppContext AppContext { get; set; }
        public ITableEditorView View { get; set; }

        public IFeatureLayer FeatureLayer
        {
            get { return virtualGrid.FeatureLayer; }
            set
            {
                virtualGrid.FeatureLayer = value;
                if (value is IFeatureClass)
                {
                    IFeatureClass pFeatureClass = value as IFeatureClass;
                    header.Value = pFeatureClass.AliasName;
                    header.Handler = pFeatureClass.ObjectClassID;
                }
                else if (value is IFeatureLayer)
                {
                    IFeatureLayer pFeatureLayer = value as IFeatureLayer;
                    header.Value = pFeatureLayer.Name;
                    header.Handler = pFeatureLayer.FeatureClass.ObjectClassID;
                }
            }
        }

        public void SelectAll()
        {
            virtualGrid.SelectAll();
        }

        public void SelectNone()
        {
            virtualGrid.SelectNone();
        }

        public void InvertSelection()
        {
            virtualGrid.InvertSelection();
        }

        public void ReloadData(string whereCaluse)
        {
            virtualGrid.ClearTable();
            virtualGrid.ShowTable(whereCaluse);
        }

        private void virtualGrid_SelectFeaturesHandler(object sender, EventArgs e)
        {
            _activeViewEventsEvent.SelectionChanged -= _activeViewEventsEvent_SelectionChanged;
            View?.MapView.SelectFeatures(virtualGrid.FeatureLayer, virtualGrid.SelectedOIDs);
            _activeViewEventsEvent.SelectionChanged += _activeViewEventsEvent_SelectionChanged;
        }
    }
}
