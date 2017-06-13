using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
    public partial class TablePage : TabPage, ITableView
    {
        private IAppContext _context;
        private ITableEditorView _view;
        private IFeatureLayer _featureLayer;
        private Header _header;
        private VirtualGrid _virtualGrid;
        private IActiveViewEvents_Event _activeViewEventsEvent;
        public TablePage(IAppContext context, ITableEditorView view, IFeatureLayer featureLayer)
        {
            InitializeComponent();
            _context = context;
            _view = view;
            _featureLayer = featureLayer;

            this.Name = featureLayer.Name;
            this.Text = featureLayer.Name;

            InitControls();

            _activeViewEventsEvent = context.MapControl.Map as IActiveViewEvents_Event;
            if (_activeViewEventsEvent != null)
            {
                _activeViewEventsEvent.SelectionChanged += _activeViewEventsEvent_SelectionChanged;
                _activeViewEventsEvent.ItemDeleted += _activeViewEventsEvent_ItemDeleted;
            }
        }

        private void _activeViewEventsEvent_ItemDeleted(object Item)
        {
            IFeatureLayer pLayer = Item as IFeatureLayer;
            if (pLayer != null && pLayer.Name == _featureLayer.Name)
            {
                _view.ClosePage(_featureLayer.Name);
            }
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
            _virtualGrid.SelectionChanged(oids);
        }

        public sealed override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        private void InitControls()
        {
            _header = new Header();
            _virtualGrid = new VirtualGrid();
            this.SuspendLayout();


            _header.Dock = DockStyle.Top;
            _header.TabIndex = 0;
            _header.Value = Text;
            _header.Close += _header_Close;

            _virtualGrid.Dock = DockStyle.Fill;
            _virtualGrid.FeatureLayer = _featureLayer;
            _virtualGrid.ShowTable(null);
            _virtualGrid.TabIndex = 1;
            _virtualGrid.SelectFeatures += _virtualGrid_SelectFeatures;

            this.Controls.Add(_virtualGrid);
            this.Controls.Add(_header);
            this.ResumeLayout(false);
        }

        private void _virtualGrid_SelectFeatures(object sender, EventArgs e)
        {
            _activeViewEventsEvent.SelectionChanged -= _activeViewEventsEvent_SelectionChanged;
            _view?.MapView.SelectFeatures(_virtualGrid.FeatureLayer, _virtualGrid.SelectedOIDs);
            _activeViewEventsEvent.SelectionChanged += _activeViewEventsEvent_SelectionChanged;
        }

        private void _header_Close(object sender, string e)
        {
            _view.ClosePage(e);
        }

        public int CurrentOID
        {
            get { return _virtualGrid.CurrentOID; }
        }

        public IFeatureLayer FeatureLayer
        {
            get { return _featureLayer; }
        }

        public void SelectAll()
        {
            _virtualGrid.SelectAll();
        }

        public void SelectNone()
        {
            _virtualGrid.SelectNone();
        }

        public void InvertSelection()
        {
            _virtualGrid.InvertSelection();
        }

        public void ReloadData(string whereCaluse)
        {
            _virtualGrid.ClearTable();
            _virtualGrid.ShowTable(whereCaluse);
        }

        public string StrGeometry => _virtualGrid.m_strGeometry;
        public void AddColumn(IField field)
        {
            _virtualGrid.AddColumnToGrid(field);
        }

        public void ShowAlias(bool isAlias)
        {
            if (isAlias)
            {
                _virtualGrid.ShowAlias();
            }
            else
            {
                _virtualGrid.ShowName();
            }
        }
    }
}
