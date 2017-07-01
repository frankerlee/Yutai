using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Services;
using Yutai.Services.Serialization;
using Yutai.Shared;
using WorkspaceHelper = Yutai.ArcGIS.Common.Helpers.WorkspaceHelper;

namespace Yutai.Plugins.Locator.Views
{
    public partial class LocatorDockPanel : Yutai.UI.Controls.DockPanelControlBase, ILocatorView
    {
        private IAppContext _context;
        private DataTable _dataTable;
        private string _locatorName;
        private IActiveViewEvents_Event activeViewEvents;
        private IMapControlEvents2_Event mapEvent;
        private IMap _map;
        private int _searchCount = 0;
        private bool _zoomShape = false;
        private List<XmlLocator> _locators;


        public LocatorDockPanel(IAppContext context)
        {
            if (context == null) throw new ArgumentNullException("context");
            _context = context;
            InitializeComponent();
            TabPosition = 4;
            btnZoom.Tag = 0;
            btnZoom.Click += (s, e) => { _zoomShape = btnZoom.Checked; };
            cmbLocators.SelectedIndexChanged += cmbLocators_SelectedIndexChanged;
            // btnSearch.Click += BtnSearch_Click;
            InitLocatorTables();
            _map = _context.MapControl.ActiveView as IMap;
            cardView1.OptionsBehavior.AutoHorzWidth = true;
        }


        private void BtnSearch_Click(object sender, EventArgs e)
        {
            TrySearch(true);
        }

        private void cmbLocators_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLocators.SelectedIndex < 0)
            {
                // btnSearch.Enabled = false;
                return;
            }
            //btnSearch.Enabled = true;
            TrySearch(false);
        }

        public void TrySearch(bool allowKeyEmpty)
        {
            if (cmbLocators.SelectedIndex < 0) return;
            _dataTable.Rows.Clear();
            _searchCount = 0;
            string searchKey = txtKey.Text.Trim();
            if (allowKeyEmpty == false && string.IsNullOrEmpty(searchKey)) return;
            XmlLocator locator = FindLocatorByName(cmbLocators.SelectedItem.ToString());
            if (locator == null) return;
            _map = _context.MapControl.Map as IMap;
            ILayer pLayer = LayerHelper.QueryLayerByDisplayName(_map, locator.Layer);
            if (pLayer == null)
            {
                MessageService.Current.Warn("找不到定位所需的图层" + locator.Layer);
                return;
            }
            if (pLayer is IGroupLayer && pLayer is ICompositeLayer)
            {
                ICompositeLayer pGroupLayer = pLayer as ICompositeLayer;
                for (int i = 0; i < pGroupLayer.Count; i++)
                {
                    ILayer pSubLayer = pGroupLayer.Layer[i];
                    SearchLayer(pSubLayer, locator, searchKey);
                }
            }
            else if (pLayer is IFeatureLayer)
            {
                SearchLayer(pLayer, locator, searchKey);
            }

            this.grdResult.Update();
        }

        private void SearchLayer(ILayer pSubLayer, XmlLocator locator, string searchKey)
        {
            if (_searchCount > _context.Config.LocatorMaxCount) return;
            IQueryFilter queryFilter = new QueryFilter();
            IFeatureClass pClass = ((IFeatureLayer) pSubLayer).FeatureClass;
            string likeStr = WorkspaceHelper.GetSpecialCharacter(pClass as IDataset,
                esriSQLSpecialCharacters.esriSQL_WildcardManyMatch);
            if (!string.IsNullOrEmpty(searchKey))
            {
                queryFilter.WhereClause = BuildWhereClause(locator.SearchFields, searchKey, likeStr);
            }
            IFeatureCursor cursor = pClass.Search(queryFilter, false);
            IFeature pFeature = cursor.NextFeature();
            int nameIdx = GetFieldIdx(cursor, locator.NameField);
            int addIdx = GetFieldIdx(cursor, locator.AddressField);
            int descIdx = GetFieldIdx(cursor, locator.DescriptionField);
            int telIdx = GetFieldIdx(cursor, locator.TelephoneField);
            int emailIdx = GetFieldIdx(cursor, locator.EmailField);
            int phoIdx = GetFieldIdx(cursor, locator.PhotoField);

            while (pFeature != null)
            {
                IGeometry pGeometry = pFeature.Shape;
                if (pGeometry.IsEmpty)
                {
                    pFeature = cursor.NextFeature();
                    continue;
                }

                DataRow row = _dataTable.NewRow();
                row["图层"] = pSubLayer.Name;
                row["序号"] = pFeature.OID;
                row["名称"] = nameIdx < 0 ? "" : pFeature.get_Value(nameIdx);
                row["地址"] = addIdx < 0 ? "" : pFeature.get_Value(addIdx);
                row["说明"] = descIdx < 0 ? "" : pFeature.get_Value(descIdx);
                row["电话"] = telIdx < 0 ? "" : pFeature.get_Value(telIdx);
                row["邮箱"] = emailIdx < 0 ? "" : pFeature.get_Value(emailIdx);
                row["要素"] = pFeature.Shape;
                row["照片"] = phoIdx < 0 ? null : pFeature.get_Value(phoIdx);
                _dataTable.Rows.Add(row);
                _searchCount++;
                if (_searchCount > _context.Config.LocatorMaxCount) break;
                pFeature = cursor.NextFeature();
            }

            OtherHelper.ReleaseObject(cursor);
        }

        private int GetFieldIdx(IFeatureCursor cursor, string locatorNameField)
        {
            if (string.IsNullOrEmpty(locatorNameField)) return -1;
            return cursor.FindField(locatorNameField);
        }


        private string BuildWhereClause(string locatorSearchFields, string searchKey, string likeStr)
        {
            string[] fields = locatorSearchFields.Split(',');
            string whereClause = "";
            for (int i = 0; i < fields.Length; i++)
            {
                if (i == 0)
                    whereClause = string.Format("{0} Like '{1}{2}{1}' ", fields[i], likeStr, searchKey);
                else
                    whereClause += string.Format(" OR {0} Like '{1}{2}{1}' ", fields[i], likeStr, searchKey);
            }
            return whereClause;
        }

        private XmlLocator FindLocatorByName(string toString)
        {
            if (_locators == null) return null;


            foreach (XmlLocator locator in _locators)
            {
                if (locator.Name == toString)
                    return locator;
            }
            return null;
        }

        public string LocatorName
        {
            get { return _locatorName; }
        }

        public bool ZoomToShape
        {
            get { return btnZoom.Checked; }
        }

        public void Clear()
        {
            _dataTable.Rows.Clear();
        }

        public event Action LocatorChanged;
        public event Action ItemSelected;

        private void InitGridSettings()
        {
        }


        private void InitLocatorTables()
        {
            _dataTable = new DataTable("定位结果");
            DataColumn column = new DataColumn("图层", typeof(string));
            _dataTable.Columns.Add(column);
            column = new DataColumn("序号", typeof(int));
            _dataTable.Columns.Add(column);
            column = new DataColumn("名称", typeof(string));
            column.AllowDBNull = true;
            _dataTable.Columns.Add(column);
            column = new DataColumn("地址", typeof(string));
            column.AllowDBNull = true;
            _dataTable.Columns.Add(column);
            column = new DataColumn("说明", typeof(string));
            column.AllowDBNull = true;
            _dataTable.Columns.Add(column);
            column = new DataColumn("电话", typeof(string));
            column.AllowDBNull = true;
            _dataTable.Columns.Add(column);
            column = new DataColumn("邮箱", typeof(string));
            column.AllowDBNull = true;
            _dataTable.Columns.Add(column);
            column = new DataColumn("要素", typeof(object));
            column.AllowDBNull = true;
            _dataTable.Columns.Add(column);
            column = new DataColumn("照片", typeof(object));
            column.AllowDBNull = true;
            _dataTable.Columns.Add(column);

            this.grdResult.DataSource = _dataTable;
            InitGridSettings();
        }

        public IEnumerable<Control> Buttons
        {
            get
            {
                yield return btnClear;
                yield return btnSearch;
            }
        }


        /// <summary>
        /// Gets the ok button.
        /// </summary>
        public ButtonBase OkButton
        {
            get { return null; }
        }

        public IEnumerable<ToolStripItemCollection> ToolStrips
        {
            get { return null; }
        }

        public void Initialize(IAppContext context)
        {
            _context = context;
            UpdateView();
        }

        public void UpdateView()
        {
        }

        public void LoadLocators()
        {
            this.cmbLocators.Items.Clear();
            ISecureContext secureContext = _context as ISecureContext;
            if (secureContext.YutaiProject == null) return;
            XmlPlugin plugCfg = secureContext.YutaiProject.FindPlugin("2b81c89a-ee45-4276-9dc1-72bbbf07f53f");
            if (plugCfg == null) return;
            //修改为从配置文件里面读取
            string locatorXml = FileHelper.GetFullPath(plugCfg.ConfigXML);
            FileInfo fileInfo = new FileInfo(locatorXml);
            if (!fileInfo.Exists) return;
            using (var reader = new StreamReader(locatorXml))
            {
                string state = reader.ReadToEnd();
                var config = state.Deserialize<PluginConfig>();
                _locators = config.Locators;
                foreach (XmlLocator locator in config.Locators)
                {
                    this.cmbLocators.Items.Add(locator.Name);
                }
                _map = _context.MapControl.ActiveView as IMap;
            }
        }

        public override Bitmap Image
        {
            get { return Properties.Resources.icon_locator_small; }
        }

        public override string Caption
        {
            get { return "位置查看器"; }
            set { Caption = value; }
        }

        public override DockPanelState DefaultDock
        {
            get { return DockPanelState.Right; }
        }

        public override string DockName
        {
            get { return DefaultDockName; }
        }

        public virtual string DefaultNestDockName
        {
            get { return ""; }
        }

        public const string DefaultDockName = "Plug_Locatoe_Result";

        private void cardView1_FocusedRowChanged(object sender,
            DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow row = cardView1.GetDataRow(e.FocusedRowHandle);
            if (row["要素"] == null) return;
            IGeometry geometry = row["要素"] as IGeometry;
            if (ZoomToShape)
            {
                EsriUtils.ZoomToGeometry(geometry, _map, 2);
                FlashUtility.FlashGeometry(geometry, _context.MapControl);
            }
        }
    }
}