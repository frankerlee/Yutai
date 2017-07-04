using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Printing.Properties;
using Yutai.Plugins.Services;
using Yutai.UI.Controls;

namespace Yutai.Plugins.Printing.Forms
{
    public partial class TemplateConfigPage : ConfigPageBase, IConfigPage
    {
        private readonly IConfigService _configService;
        private PrintingPlugin _plugin;

        public TemplateConfigPage(IConfigService configService,PrintingPlugin plugin)
        {
            if (configService == null) throw new ArgumentNullException("configService");
            _configService = configService;
            _plugin = plugin;
            InitializeComponent();
            Initialize();
        }


        public void Initialize()
        {
            txtDB.Text = _plugin.PrintingConfig.TemplateConnectionString;
            foreach (var indexMap in _plugin.PrintingConfig.IndexMaps)
            {
                ListViewItem item=new ListViewItem(new string[] {indexMap.Name,indexMap.IndexLayerName,indexMap.TemplateName,indexMap.SearchFields,indexMap.NameField});
                item.Tag = indexMap;
                lstIndexes.Items.Add(item);
            }
        }

        public string PageName
        {
            get { return "制图配置"; }
        }

        public void Save()
        {
            _plugin.PrintingConfig.TemplateConnectionString = txtDB.Text;
          _plugin.PrintingConfig.Save();
        }

        public string Key
        {
            get { return "Printing"; }
        }


        public Bitmap Icon
        {
            get { return Resources.icon_clip_print; }
        }

        public ConfigPageType PageType
        {
            get { return ConfigPageType.General; }
        }

        public string Description
        {
            get { return "制图模块设置"; }
        }

        public bool VariableHeight
        {
            get { return false; }
        }

        private void btnTemplateDB_Click(object sender, EventArgs e)
        {
            frmOpenFile openFile = new frmOpenFile();
            openFile.Text = "选择模板数据库";
            openFile.AllowMultiSelect = false;
            openFile.AddFilter(new MyGxFilterPersonalGeodatabases(), true);
            openFile.AddFilter(new MyGxFilterFileGeodatabases(), false);
            openFile.AddFilter(new MyGxFilterEnteripesGeoDatabases(), false);
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                IGxDatabase database = openFile.Items.get_Element(0) as IGxDatabase;
                if (database != null)
                {
                   
                    IFeatureWorkspace pWorkspace =
                        Yutai.ArcGIS.Common.Helpers.WorkspaceHelper.GetWorkspace((database as IGxObject).FullName);
                    if (pWorkspace == null) return;
                    bool isExists1=((IWorkspace2) pWorkspace).get_NameExists(esriDatasetType.esriDTTable, "MAPTEMPLATE");
                    bool isExists2 = ((IWorkspace2)pWorkspace).get_NameExists(esriDatasetType.esriDTTable, "MAPTEMPLATECLASS");
                    bool isExists3 = ((IWorkspace2)pWorkspace).get_NameExists(esriDatasetType.esriDTTable, "MAPTEMPLATEELEMENT");
                    bool isExists4 = ((IWorkspace2)pWorkspace).get_NameExists(esriDatasetType.esriDTTable, "MAPTEMPLATEEPARAM");
                    if (isExists4 && isExists1 && isExists3 && isExists2)
                    {
                        this.txtDB.Text = BuildConnectionString((database as IGxObject).FullName);
                    }
                    else
                    {
                        MessageService.Current.Warn("该数据库内没有地图制图模板数据!请重新选择!");
                    }
                }
            }
        }
        private string BuildConnectionString(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            string ext = fileInfo.Extension.Substring(1);
            return string.Format("dbclient={0};gdbname={1}", ext, fileName);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstIndexes.SelectedItems.Count == 0)
            {
                MessageService.Current.Warn("请先选择要删除的索引图配置");
                return;
            }
            if (MessageService.Current.Ask("你确认删除选中的索引图配置吗?") != true) return;
            foreach (ListViewItem item in lstIndexes.SelectedItems)
            {
                lstIndexes.Items.Remove(item);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            frmIndexMap indexMapFrm=new frmIndexMap(txtDB.Text, new IndexMap(), _plugin);
            if (indexMapFrm.ShowDialog() != DialogResult.OK) return;
            IIndexMap indexMap = indexMapFrm.IndexMap;
            IIndexMap oldIdx = _plugin.PrintingConfig.IndexMaps.FirstOrDefault(c => c.Name == indexMap.Name);
            if (oldIdx != null)
            {
                MessageService.Current.Warn("同名索引设置以及存在，无法保存!");
                return;
            }
            _plugin.PrintingConfig.IndexMaps.Add(indexMap);
           ListViewItem item=new ListViewItem(new string[]
            {
                indexMap.Name, indexMap.IndexLayerName, indexMap.TemplateName, indexMap.SearchFields,
                indexMap.NameField
            });
            item.Tag = indexMap;
            lstIndexes.Items.Add(item);
        }

        private void lstIndexes_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.lstIndexes.SelectedItems.Count != 1) return;
            IIndexMap pIndexMap = ((IIndexMap) lstIndexes.SelectedItems[0].Tag);
            string oldName = pIndexMap.Name;
            frmIndexMap indexMapFrm = new frmIndexMap(txtDB.Text, pIndexMap, _plugin);
            if (indexMapFrm.ShowDialog() != DialogResult.OK) return;
            lstIndexes.Items.Remove(this.lstIndexes.SelectedItems[0]);
            _plugin.PrintingConfig.IndexMaps.Remove(pIndexMap);
            IIndexMap indexMap = indexMapFrm.IndexMap;
            IIndexMap oldIdx = _plugin.PrintingConfig.IndexMaps.FirstOrDefault(c => c.Name == indexMap.Name);
            if (oldIdx != null)
            {
                MessageService.Current.Warn("同名索引设置以及存在，无法保存!");
                return;
            }
            _plugin.PrintingConfig.IndexMaps.Add(indexMap);
            ListViewItem item = new ListViewItem(new string[]
             {
                indexMap.Name, indexMap.IndexLayerName, indexMap.TemplateName, indexMap.SearchFields,
                indexMap.NameField
             });
            item.Tag = indexMap;
            lstIndexes.Items.Add(item);

        }
    }
}