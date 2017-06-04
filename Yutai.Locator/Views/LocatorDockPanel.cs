using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Syncfusion.Grouping;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Grid.Grouping;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Services;
using Yutai.Services.Serialization;
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
        public LocatorDockPanel(IAppContext context)
        {
            if (context == null) throw new ArgumentNullException("context");
            _context = context;
            InitializeComponent();
            TabPosition = 4;
            toolZoomToShape.Tag = 0;
            toolZoomToShape.Click += (s, e) => { toolZoomToShape.Checked = !toolZoomToShape.Checked; };
            comboBoxAdv1.SelectedIndexChanged += ComboBoxAdv1_SelectedIndexChanged;
            //btnSearch.Click += BtnSearch_Click;
            InitLocatorTables();
            _map = _context.MapControl.ActiveView as IMap;
            
        }

        
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            TrySearch(true);
        }

        private void ComboBoxAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxAdv1.SelectedIndex < 0)
            {
               // btnSearch.Enabled = false;
                return;
            }
            //btnSearch.Enabled = true;
            TrySearch(false);
        }

        public void TrySearch(bool allowKeyEmpty)
        {
            if (comboBoxAdv1.SelectedIndex < 0) return;
            _dataTable.Rows.Clear();
            _searchCount = 0;
            string searchKey = toolSearchKey.Text.Trim();
            if (allowKeyEmpty == false && string.IsNullOrEmpty(searchKey)) return;
            XmlLocator locator = FindLocatorByName(comboBoxAdv1.SelectedItem.ToString());
            if (locator == null) return;
            _map = _context.MapControl.Map as IMap;
            ILayer pLayer = LayerHelper.QueryLayerByDisplayName(_map, locator.Layer);
            if (pLayer == null)
            {
                MessageService.Current.Warn("找不到定位所需的图层"+locator.Layer);
                return;
            }
            if (pLayer is IGroupLayer && pLayer is ICompositeLayer)
            {
                ICompositeLayer pGroupLayer = pLayer as ICompositeLayer;
                for (int i = 0; i < pGroupLayer.Count; i++)
                {
                    ILayer pSubLayer = pGroupLayer.Layer[i];
                    SearchLayer(pSubLayer, locator,searchKey);
                }
            }
            else if (pLayer is IFeatureLayer)
            {
                SearchLayer(pLayer, locator, searchKey);
            }
            this.grdResult.TableDescriptor.Columns["名称"].Appearance.AnyCell.Font.Bold = true;
            this.grdResult.TableDescriptor.Columns["电话"].Width = this.grdResult.Width / 2;
            this.grdResult.Update();
        }

        private void SearchLayer(ILayer pSubLayer, XmlLocator locator,string searchKey)
        {
            if (_searchCount > _context.Config.LocatorMaxCount) return;
            IQueryFilter queryFilter=new QueryFilter();
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
            int addIdx= GetFieldIdx(cursor, locator.AddressField);
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


        private string BuildWhereClause(string locatorSearchFields, string searchKey,string likeStr)
        {
            string[] fields = locatorSearchFields.Split(',');
            string whereClause = "";
            for (int i = 0; i < fields.Length; i++)
            {
                if (i == 0)
                    whereClause = string.Format("{0} Like '{1}{2}{1}' ", fields[i], likeStr, searchKey);
                else
                    whereClause+= string.Format(" OR {0} Like '{1}{2}{1}' ", fields[i], likeStr, searchKey);
            }
            return whereClause;
        }

        private XmlLocator FindLocatorByName(string toString)
        {
            ISecureContext secureContext = _context as ISecureContext;
            if (secureContext.YutaiProject == null) return null;
            if (secureContext.YutaiProject.Locators == null) return null;
            foreach (XmlLocator locator in secureContext.YutaiProject.Locators)
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
            get { return toolZoomToShape.Checked; }
        }

        public void Clear()
        {
            _dataTable.Rows.Clear();
        }

        public event Action LocatorChanged;
        public event Action ItemSelected;

        private void InitGridSettings()
        {
            Syncfusion.Windows.Forms.Grid.Grouping.GridColumnSetDescriptor gridColumnSetDescriptor1 = new Syncfusion.Windows.Forms.Grid.Grouping.GridColumnSetDescriptor();
            this.grdResult.TableDescriptor.AllowEdit = false;
            this.grdResult.TableDescriptor.AllowNew = false;
            this.grdResult.TableDescriptor.VisibleColumns.Clear();
            gridColumnSetDescriptor1.ColumnSpans.AddRange(new Syncfusion.Windows.Forms.Grid.Grouping.GridColumnSpanDescriptor[] {
            new Syncfusion.Windows.Forms.Grid.Grouping.GridColumnSpanDescriptor("名称", "R0C0:R0C1"),
            new Syncfusion.Windows.Forms.Grid.Grouping.GridColumnSpanDescriptor("地址", "R1C0:R1C1"),
            new Syncfusion.Windows.Forms.Grid.Grouping.GridColumnSpanDescriptor("说明", "R2C0:R2C1"),
            new Syncfusion.Windows.Forms.Grid.Grouping.GridColumnSpanDescriptor("电话", "R3C0"),
            new Syncfusion.Windows.Forms.Grid.Grouping.GridColumnSpanDescriptor("邮箱", "R3C1")});
            gridColumnSetDescriptor1.Name = "ColumnSet 1";
            this.grdResult.TableDescriptor.ColumnSets.AddRange(new Syncfusion.Windows.Forms.Grid.Grouping.GridColumnSetDescriptor[] {
            gridColumnSetDescriptor1});
            this.grdResult.TableDescriptor.GroupedColumns.AddRange(new Syncfusion.Grouping.SortColumnDescriptor[] {
            new Syncfusion.Grouping.SortColumnDescriptor("图层", System.ComponentModel.ListSortDirection.Ascending)});
            this.grdResult.TableDescriptor.TableOptions.AllowDragColumns = true;
            this.grdResult.TableDescriptor.TableOptions.AllowDropDownCell = false;
            this.grdResult.TableDescriptor.TableOptions.ShowRowHeader = false;
            this.grdResult.TableDescriptor.TopLevelGroupOptions.ShowCaption = true;
            this.grdResult.TableDescriptor.TopLevelGroupOptions.ShowColumnHeaders = false;
            this.grdResult.TableDescriptor.TopLevelGroupOptions.ShowSummaries = false;
            this.grdResult.TableDescriptor.VisibleColumns.AddRange(new Syncfusion.Windows.Forms.Grid.Grouping.GridVisibleColumnDescriptor[] {
            new Syncfusion.Windows.Forms.Grid.Grouping.GridVisibleColumnDescriptor("ColumnSet 1")});
            this.grdResult.TableDescriptor.Columns["名称"].Width = this.grdResult.Width/2;
            
            this.grdResult.Update();
            this.grdResult.SelectedRecordsChanged+= GrdResultOnSelectedRecordsChanged;
        }

        private void GrdResultOnSelectedRecordsChanged(object sender, SelectedRecordsChangedEventArgs args)
        {
            if (args.SelectedRecord == null) return;
            if (args.SelectedRecord.Record["要素"] == null) return;
            IGeometry geometry = args.SelectedRecord.Record["要素"] as IGeometry;
            if (ZoomToShape)
            {
                EsriUtils.ZoomToGeometry(geometry, _map, 2);
                FlashUtility.FlashGeometry(geometry, _context.MapControl);
            }
        }

        private void InitLocatorTables()
        {
            _dataTable=new DataTable("定位结果");
            DataColumn column=new DataColumn("图层",typeof(string));
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
            get { yield break; }
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
            get { yield return toolStripEx1.Items; }
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
            this.comboBoxAdv1.Items.Clear();
            ISecureContext secureContext = _context as ISecureContext;
            if(secureContext.YutaiProject==null) return;
            if (secureContext.YutaiProject.Locators == null) return;
            foreach (XmlLocator locator in secureContext.YutaiProject.Locators)
            {
                this.comboBoxAdv1.Items.Add(locator.Name);
            }
            _map=_context.MapControl.ActiveView as IMap;
           
        }

        private int _oldRow;
        private void grdResult_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            Syncfusion.Grouping.Record rec = this.grdResult.Table.CurrentRecord;
            if (rec == null) return;
            if (rec["要素"] == null) return;
            IGeometry geometry = rec["要素"] as IGeometry;
            if(_oldRow ==rec.Id)return;
            if (ZoomToShape)
            {
                EsriUtils.ZoomToGeometry(geometry, _map, 2);
                FlashUtility.FlashGeometry(geometry, _context.MapControl);
            }
            _oldRow = rec.Id;
        }
    }
}
