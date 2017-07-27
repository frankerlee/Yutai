using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Services;
using Yutai.Plugins.Template.Concretes;
using Yutai.Plugins.Template.Forms;
using Yutai.Plugins.Template.Interfaces;
using Yutai.UI.Controls;
using Cursor = System.Windows.Forms.Cursor;

namespace Yutai.Plugins.Template.Views
{
    public partial class TemplateView :  DockPanelControlBase, ITemplateView
    {
        private List<ITemplateDatabase> _databases;
        private TemplatePlugin _plugin;
        private IAppContext _context;
        private TreeNode _copyObject;
      
        public TemplateView()
        {
            InitializeComponent();
           
        }

        public void Initialize(IAppContext context, TemplatePlugin plugin)
        {
            try
            {
                _context = context;
                _plugin = plugin;
                if (_plugin != null)
                {
                    _databases = new List<ITemplateDatabase>();
                    LoadDefaultDatabase();
                }
              
            }
            catch (Exception ex)
            {
                MessageService.Current.Warn(ex.Message);
            }
        }

      

        private void LoadDefaultDatabase()
        {
            if (_plugin != null && _plugin.TemplateDatabase != null)
            {
                _databases.Add(_plugin.TemplateDatabase);
            }
            LoadDatabases();
        }

        private void btnAddConnection_Click(object sender, EventArgs e)
        {
            AddConnection();
        }

        private void LoadDatabases()
        {
            trvDatabase.Nodes.Clear();
            trvDatabase.Nodes.Add("Databases", "模板数据库", 0);
            int i = 0;
            foreach (ITemplateDatabase database in _databases)
            {
                if (database.Workspace == null)
                {
                    database.Connect();
                }
                string dbName = ((IWorkspace)database.Workspace).PathName;
                TreeNode dbNode = trvDatabase.Nodes[0].Nodes.Add(dbName, dbName, 1);
                dbNode.Tag = database;
                LoadDatabase(database, dbNode);
                //LoadDomains(database);
            }
        }

        private void LoadDatabase(ITemplateDatabase database, TreeNode pNode)
        {
            if (database.Workspace == null)
            {
                database.Connect();
            }
            string dbName = ((IWorkspace)database.Workspace).PathName;
            if (database.IsTemplateDatabase())
            {
                TreeNode tmpNode = pNode.Nodes.Add(dbName + "\\模板", "模板", 2);
                TreeNode domainNode = pNode.Nodes.Add(dbName + "\\数据字典", "数据字典", 3);
                TreeNode datasetNode = pNode.Nodes.Add(dbName + "\\数据集", "数据集", 7);
            }
        }

        private void LoadTemplates(ITemplateDatabase database, TreeNode parentNode)
        {
            database.LoadTemplates();
            foreach (IObjectTemplate template in database.Templates)
            {
                string key= ((IWorkspace)database.Workspace).PathName+"\\templates\\"+template.Name;
                TreeNode tmpNode = parentNode.Nodes.Add(key, template.Name, 4);
                tmpNode.Tag = template;
            }
        }

        private void LoadDatasets(ITemplateDatabase database, TreeNode parentNode)
        {
            database.LoadDatasets();
            foreach (IObjectDataset dataset in database.Datasets)
            {
                string key = ((IWorkspace)database.Workspace).PathName + "\\datasets\\" + dataset.Name;
                TreeNode tmpNode = parentNode.Nodes.Add(key, dataset.Name, 8);
                tmpNode.Tag = dataset;
            }
        }

        private void LoadDomains(ITemplateDatabase database, TreeNode parentNode)
        {
            database.LoadDomains();
            foreach (IYTDomain domain in database.Domains)
            {
                string key = ((IWorkspace)database.Workspace).PathName + "\\domains\\" + domain.Name;
                TreeNode tmpNode = parentNode.Nodes.Add(key, domain.Name, 6);
                tmpNode.Tag = domain;
            }
        }

        #region Override DockPanelControlBase

        public override Bitmap Image
        {
            get { return Properties.Resources.icon_template_fc; }
        }

        public override string Caption
        {
            get { return "模板数据库管理"; }
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

        public override string DefaultNestDockName
        {
            get { return ""; }
        }

        public const string DefaultDockName = "TemplateDatabase_Viewer";

        public IEnumerable<ToolStripItemCollection> ToolStrips
        {
            get { yield break; }
        }

        public IEnumerable<Control> Buttons
        {
            get { yield break; }
        }

        #endregion

   

     
        private void AddConnection()
        {
            frmOpenFile frm = new frmOpenFile();
            frm.AddFilter(new MyGxFilterGeoDatabases(), true);
            frm.Text = "选择数据库";
            if (frm.DoModalOpen() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                IGxDatabase database = frm.Items.get_Element(0) as IGxDatabase;
                if (database != null)
                {
                    ITemplateDatabase findDB =
                        _databases.FirstOrDefault(c => c.DatabaseName == (database as IGxObject).FullName);
                    if (findDB != null)
                    {
                        MessageService.Current.Warn("数据库已经连接，无需再次连接!");
                        return;
                    }
                    IFeatureWorkspace pWorkspace =
                        Yutai.ArcGIS.Common.Helpers.WorkspaceHelper.GetWorkspace((database as IGxObject).FullName);
                    if (pWorkspace == null) return;
                    bool isExists1 = ((IWorkspace2)pWorkspace).get_NameExists(esriDatasetType.esriDTTable, "YT_TEMPLATE_FEATURECLASS");
                   // bool isExists2 = ((IWorkspace2)pWorkspace).get_NameExists(esriDatasetType.esriDTTable, "YT_TEMPLATE_FIELD");
                    bool isExists3 = ((IWorkspace2)pWorkspace).get_NameExists(esriDatasetType.esriDTTable, "YT_TEMPLATE_DOMAIN");
                    bool isExists4 = ((IWorkspace2)pWorkspace).get_NameExists(esriDatasetType.esriDTTable, "YT_TEMPLATE_DATASET");
                    ITemplateDatabase templateDatabase = new TemplateDatabase();
                    templateDatabase.DatabaseName = (database as IGxObject).FullName;
                    templateDatabase.Workspace = pWorkspace;
                    _databases.Add(templateDatabase);
                    LoadDatabases();
                    //if (isExists4 && isExists1 && isExists3 && isExists2)
                    //{
                    //    this.txtDB.Text = BuildConnectionString((database as IGxObject).FullName);
                    //}
                    //else
                    //{
                    //    MessageService.Current.Warn("该数据库内没有地图制图模板数据!请重新选择!");
                    //}
                }
                Cursor.Current = Cursors.Default;
            }
           // LoadDatabases();
        }

        private void trvDatabase_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            TreeNode currentNode = trvDatabase.GetNodeAt(e.Location);
            if (currentNode == null) return;
            trvDatabase.SelectedNode = currentNode;

            if (currentNode.Level == 0)
            {
                trvDatabase.ContextMenuStrip = menuDatabase;
            }
            else if (currentNode.Level == 1)
            {
                trvDatabase.ContextMenuStrip = menuConnection;
                ITemplateDatabase database=currentNode.Tag as ITemplateDatabase;
                if (database.IsTemplateDatabase())
                {
                    mnuRegisterTemplate.Enabled = false;
                }
                else
                {
                    mnuRegisterTemplate.Enabled = true;
                }
                if (_copyObject == null)
                {
                    mnuPasteObject.Enabled = false;
                }
                else
                    mnuPasteObject.Enabled = true;
            }
            else if (currentNode.Level == 2)
            {
                if (currentNode.Text == "数据字典")
                {
                    trvDatabase.ContextMenuStrip = menuDomains;
                }
                else if (currentNode.Text == "模板")
                {
                    trvDatabase.ContextMenuStrip = menuFCTemplates;
                }
                else if (currentNode.Text == "数据集")
                {
                    trvDatabase.ContextMenuStrip = menuDatasets;
                }
            }
            else if (currentNode.Level == 3)
            {
                trvDatabase.ContextMenuStrip = menuObject;
            }
        }

        private void mnuAddConnection_Click(object sender, EventArgs e)
        {
            AddConnection();
        }

        private void mnuRefresh_Click(object sender, EventArgs e)
        {
            LoadDatabases();
        }

        private void mnuRemoveConnection_Click(object sender, EventArgs e)
        {
            TreeNode currentNode = trvDatabase.SelectedNode;
            string dbName = ((ITemplateDatabase) currentNode.Tag).DatabaseName;
            ITemplateDatabase tempDB = _databases.FirstOrDefault(c => c.DatabaseName == dbName);
            if (tempDB != null)
            {
                _databases.Remove(tempDB);
                currentNode.Parent.Nodes.Remove(currentNode);
            }
        }

        private void mnuConnectionRefresh_Click(object sender, EventArgs e)
        {
            TreeNode dbNode = trvDatabase.SelectedNode;
            if (dbNode.Level != 1) return;
            for (int i = 0; i < dbNode.Nodes.Count; i++)
            {
                dbNode.Nodes[i].Nodes.Clear();
            }
        }

        private void mnuAddFCTemplate_Click(object sender, EventArgs e)
        {
            TreeNode pNode = trvDatabase.SelectedNode;
            if (pNode.Level != 2) return;
            ITemplateDatabase templateDatabase=pNode.Parent.Tag as ITemplateDatabase;
            frmEditTemplate frm=new frmEditTemplate();
            IObjectTemplate template=new ObjectTemplate();
            template.Database = templateDatabase;
            frm.SetTemplate(template);
            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {
                pNode.Nodes.Clear();
                LoadTemplates(templateDatabase, pNode);
            }

        }

        private void mnuFCTemplateRefresh_Click(object sender, EventArgs e)
        {
            TreeNode node = trvDatabase.SelectedNode;
            if (node.Text == "模板")
            {

                node.Nodes.Clear();
                LoadTemplates(node.Parent.Tag as ITemplateDatabase, node);

            }
        }

        private void mnuAddDomain_Click(object sender, EventArgs e)
        {
            TreeNode node = trvDatabase.SelectedNode;
            if (node.Text == "数据字典")
            {
                IYTDomain domain = new YTDomain();
                frmEditDomain frm = new frmEditDomain();
                frm.SetDatabase(node.Parent.Tag as ITemplateDatabase);
                frm.SetDomain(domain);
                DialogResult result = frm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    node.Nodes.Clear();
                    LoadDomains(node.Parent.Tag as ITemplateDatabase, node);
                }
            }
           
        }

        private void mnuDomainRefresh_Click(object sender, EventArgs e)
        {
            TreeNode node = trvDatabase.SelectedNode;
            if (node.Text == "数据字典")
            {
               
                node.Nodes.Clear();
                LoadDomains(node.Parent.Tag as ITemplateDatabase, node);
                
            }
        }

        private void mnuDelete_Click(object sender, EventArgs e)
        {
            DeleteObject();
        }

        private void DeleteObject()
        {
            TreeNode currentNode = trvDatabase.SelectedNode;
            ITemplateDatabase database = currentNode.Parent.Parent.Tag as ITemplateDatabase;
            if (currentNode.Parent.Text == "模板")
            {
                IObjectTemplate template = currentNode.Tag as IObjectTemplate;
                database.DeleteObject(template.ID, enumTemplateObjectType.FeatureClass);

            }
            if (currentNode.Parent.Text == "数据字典")
            {
                IYTDomain domain = currentNode.Tag as IYTDomain;
                database.DeleteObject(domain.ID, enumTemplateObjectType.Domain);
            }
            if (currentNode.Parent.Text == "数据集")
            {
                IObjectDataset dataset = currentNode.Tag as IObjectDataset;
                database.DeleteObject(dataset.ID, enumTemplateObjectType.FeatureDataset);

            }
        }
        private void LoadObjectEdit()
        {
            TreeNode currentNode = trvDatabase.SelectedNode;
            if (currentNode.Parent.Text == "模板")
            {
                IObjectTemplate template = currentNode.Tag as IObjectTemplate;
                frmEditTemplate frm = new frmEditTemplate();
                frm.SetTemplate(template);
                frm.SetDatabase(template.Database);
                frm.ShowDialog();
                currentNode.Tag = frm.GetTemplate();
                currentNode.Text = frm.GetTemplate().Name;
            }
            if (currentNode.Parent.Text == "数据字典")
            {
                IYTDomain domain = currentNode.Tag as IYTDomain;
                frmEditDomain frm = new frmEditDomain();
                frm.SetDatabase(currentNode.Parent.Parent.Tag as ITemplateDatabase);
                frm.SetDomain(domain);
                DialogResult result = frm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    TreeNode pNode = currentNode.Parent;
                    pNode.Nodes.Clear();
                    LoadDomains(pNode.Parent.Tag as ITemplateDatabase, pNode);
                }
                currentNode.Tag = frm.GetDomain();
            }
            if (currentNode.Parent.Text == "数据集")
            {
                IObjectDataset dataset = currentNode.Tag as IObjectDataset;
                frmEditDataset frm = new frmEditDataset();
                frm.SetDatabase(currentNode.Parent.Parent.Tag as ITemplateDatabase);
                frm.Dataset = dataset;
                DialogResult result = frm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    currentNode.Tag = frm.Dataset;
                    currentNode.Text = frm.Dataset.Name;
                }
                
            }
        }
        private void mnuProperty_Click(object sender, EventArgs e)
        {
            LoadObjectEdit();
        }

        private void trvDatabase_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode currentNode = e.Node;
            if (e.Node.Level == 2)
            {
                if (currentNode.Text == "数据字典")
                {
                    if (currentNode.Nodes.Count == 0)
                    {
                        LoadDomains(currentNode.Parent.Tag as ITemplateDatabase, currentNode);
                    }

                }
                else if (currentNode.Text == "模板")
                {
                    if (currentNode.Nodes.Count == 0)
                    {
                        LoadTemplates(currentNode.Parent.Tag as ITemplateDatabase, currentNode);
                    }
                }
                else if (currentNode.Text == "数据集")
                {
                    if (currentNode.Nodes.Count == 0)
                    {
                        LoadDatasets(currentNode.Parent.Tag as ITemplateDatabase, currentNode);
                    }
                }
            }
            else if (e.Node.Level == 3)
            {
                LoadObjectEdit();
            }
        }

        private void mnuAddDataset_Click(object sender, EventArgs e)
        {
            TreeNode node = trvDatabase.SelectedNode;
            if (node.Text == "数据集")
            {
                IObjectDataset dataset = new ObjectDataset();
                frmEditDataset frm = new frmEditDataset();
                frm.SetDatabase(node.Parent.Tag as ITemplateDatabase);
                frm.Dataset=dataset;
                DialogResult result = frm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    node.Nodes.Clear();
                    LoadDatasets(node.Parent.Tag as ITemplateDatabase, node);
                }
            }
        }

        private void mnuDatasetRefresh_Click(object sender, EventArgs e)
        {
            TreeNode node = trvDatabase.SelectedNode;
            node.Nodes.Clear();
            LoadDatasets(node.Parent.Tag as ITemplateDatabase, node);
        }

        private void mnuRegisterTemplate_Click(object sender, EventArgs e)
        {
            TreeNode node = trvDatabase.SelectedNode;
            ITemplateDatabase database=node.Tag as ITemplateDatabase;
            database.RegisterTemplateDatabase();
            LoadDatabase(database,node);
        }

        private void mnuCopyObject_Click(object sender, EventArgs e)
        {
            TreeNode node = trvDatabase.SelectedNode;
            _copyObject = node;
        }

        private void mnuPasteObject_Click(object sender, EventArgs e)
        {
            if (_copyObject == null) return;

            TreeNode currentNode = trvDatabase.SelectedNode;
            ITemplateDatabase database=currentNode.Tag as ITemplateDatabase;
            if (_copyObject.Parent.Text == "模板")
            {
                IObjectTemplate template = _copyObject.Tag as IObjectTemplate;
                template.ID = -1;
                template.Database = database;
                frmEditTemplate frm = new frmEditTemplate();
                frm.SetTemplate(template);
                frm.SetDatabase(database);
                DialogResult result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    TreeNode pNode = currentNode.Nodes[0];
                    pNode.Nodes.Clear();
                    LoadTemplates(database, pNode);
                }

            }
            if (_copyObject.Parent.Text == "数据字典")
            {
                IYTDomain domain = _copyObject.Tag as IYTDomain;
                domain.ID = -1;
                
                frmEditDomain frm = new frmEditDomain();
                frm.SetDatabase(database);
                frm.SetDomain(domain);
                DialogResult result = frm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    TreeNode pNode = currentNode.Nodes[1];
                    pNode.Nodes.Clear();
                    LoadDomains(database, pNode);
                }
                //currentNode.Tag = frm.GetDomain();
            }
            if (_copyObject.Parent.Text == "数据集")
            {
                IObjectDataset dataset = _copyObject.Tag as IObjectDataset;
                dataset.ID = -1;
                frmEditDataset frm = new frmEditDataset();
                frm.SetDatabase(database);
                frm.Dataset = dataset;
                DialogResult result = frm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    TreeNode pNode = currentNode.Nodes[2];
                    pNode.Nodes.Clear();
                    LoadDatasets(database, pNode);
                }

            }
            _copyObject = null;
        }

        private void mnuCreateObject_Click(object sender, EventArgs e)
        {
            TreeNode currentNode = trvDatabase.SelectedNode;
            ITemplateDatabase templateDatabase = currentNode.Parent.Parent.Tag as ITemplateDatabase;
            if (currentNode.Parent.Text == "模板")
            {
                IObjectTemplate template = currentNode.Tag as IObjectTemplate;
                frmQuickCreateFeatureClass frm=new frmQuickCreateFeatureClass(_context,_plugin);
                frm.SingleTemplate = template;
                frm.ShowDialog();

            }
            if (currentNode.Parent.Text == "数据字典")
            {
                
            }
            if (currentNode.Parent.Text == "数据集")
            {

                IObjectDataset dataset = currentNode.Tag as IObjectDataset;
                frmQuickCreateFeatureDataset frm = new frmQuickCreateFeatureDataset(_context, templateDatabase);
                frm.SetDataset(dataset.Name);
                frm.ShowDialog();
            }
           
        }
    }
}
