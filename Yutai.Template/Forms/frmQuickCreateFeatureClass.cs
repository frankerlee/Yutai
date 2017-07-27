using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Services;
using Yutai.Plugins.Template.Concretes;
using Yutai.Plugins.Template.Interfaces;
using WorkspaceHelper = Yutai.ArcGIS.Common.Helpers.WorkspaceHelper;

namespace Yutai.Plugins.Template.Forms
{
    public partial class frmQuickCreateFeatureClass : Form
    {
        private IMap _map;
        private IAppContext _context;
        private TemplatePlugin _plugin;

        private IObjectTemplate _template;
        private bool _isSingle = false;

        public IObjectTemplate SingleTemplate
        {
            set
            {
                _template = value;
                _isSingle = true;
                cmbTemplate.Items.Clear();
                cmbTemplate.Items.Add(_template.Name);
                cmbTemplate.SelectedIndex = 0;
                cmbTemplate.Enabled = false;
            }
        }
        public frmQuickCreateFeatureClass()
        {
            InitializeComponent();
        }

        public frmQuickCreateFeatureClass(IAppContext context,TemplatePlugin plugin)
        {
            InitializeComponent();
            _context = context;
            _map = _context.FocusMap;
            _plugin = plugin;
            ISpatialReference spatialReference = _map.SpatialReference;
            txtSpatialRef.Text=spatialReference.Name;

            IActiveView pActiveView = _map as IActiveView;
            IEnvelope pEnv = pActiveView.Extent;
            txtXMin.EditValue = Math.Floor(pEnv.XMin);
            txtYMin.EditValue = Math.Floor(pEnv.YMin);
            txtXMax.EditValue = Math.Ceiling(pEnv.XMax);
            txtYMax.EditValue = Math.Ceiling(pEnv.YMax);
            LoadTemplates();
        }

        private void LoadTemplates()
        {
            if (_plugin.TemplateDatabase.Templates==null || _plugin.TemplateDatabase.Templates.Count == 0)
            {
                _plugin.TemplateDatabase.Connect();
                _plugin.TemplateDatabase.LoadTemplates();
                _plugin.TemplateDatabase.DisConnect();
            }
            foreach (IObjectTemplate template in _plugin.TemplateDatabase.Templates)
            {
                cmbTemplate.Items.Add(template.Name);
            }
        }

        private void btnDB_Click(object sender, EventArgs e)
        {
            frmOpenFile openFile=new frmOpenFile();
            openFile.AllowMultiSelect = false;
            openFile.AddFilter(new MyGxFilterWorkspaces(), true);
            openFile.AddFilter(new MyGxFilterFeatureDatasets(), false);
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                IGxObject gxObject = openFile.Items.get_Element(0);
                if (gxObject is IGxDatabase)
                {
                    IGxDatabase database = gxObject as IGxDatabase;
                    txtDB.Text = database.WorkspaceName.PathName;
                    txtDB.Tag = database;
                    label1.Tag = "Database";
                    xtraTabControl1.TabPages[0].PageVisible = true;
                }
                else if (gxObject is IGxDataset)
                {
                    IGxDataset dataset = gxObject as IGxDataset;
                    txtDB.Text = dataset.DatasetName.WorkspaceName.PathName + "\\" + dataset.DatasetName.Name;
                    txtDB.Tag = dataset;
                    label1.Tag = "Dataset";
                    xtraTabControl1.TabPages[0].PageVisible = false;
                }
            }
        }

        private void btnCurrentExtent_Click(object sender, EventArgs e)
        {
            IActiveView pActiveView = _map as IActiveView;
            IEnvelope pEnv = pActiveView.Extent;
            txtXMin.EditValue = pEnv.XMin;
            txtYMin.EditValue = pEnv.YMin;
            txtXMax.EditValue = pEnv.XMax;
            txtYMax.EditValue = pEnv.YMax;
        }

        private void btnFullExtent_Click(object sender, EventArgs e)
        {
            IActiveView pActiveView = _map as IActiveView;
            IEnvelope pEnv = pActiveView.FullExtent;
            txtXMin.EditValue = pEnv.XMin;
            txtYMin.EditValue = pEnv.YMin;
            txtXMax.EditValue = pEnv.XMax;
            txtYMax.EditValue = pEnv.YMax;
        }

        private void chkIndex_CheckedChanged(object sender, EventArgs e)
        {
            grpIndex.Visible = chkIndex.Checked;
            grpIndex.Enabled = chkIndex.Checked;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private bool Validate(bool showMsg)
        {
            string locType = label1.Tag != null ? label1.Tag.ToString() : "";
            if (string.IsNullOrEmpty(locType))
            {
                if(showMsg) MessageService.Current.Warn("请选择保存位置！");
                return false;
            }
            if (cmbTemplate.SelectedIndex < 0)
            {
                if (showMsg) MessageService.Current.Warn("请选择要素类模板！");
                return false;
            }
            if (chkIndex.Checked &&
                (Convert.ToDouble(txtWidth.EditValue) <= 0 || Convert.ToDouble(txtHeight.EditValue) <= 0))
            {
                if (showMsg) MessageService.Current.Warn("请输入索引图单幅实际宽度和高度！");
                return false;
            }
            if (txtName.Text.Trim() == "")
            {
                if (showMsg) MessageService.Current.Warn("请输入要素类名称！");
                return false;
            }
            return true;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(Validate(true)==false)return;
            string locType = label1.Tag != null ? label1.Tag.ToString() : "";
            if (string.IsNullOrEmpty(locType)) return;
            object createLoc;
            IWorkspace2 workspace2 = null;
            IObjectTemplate template = null;
            if (_isSingle)
            {
                template = _template;
            }
            else
            {
                template =_plugin.TemplateDatabase.Templates.FirstOrDefault(c => c.Name == cmbTemplate.SelectedItem.ToString());
            }
            
            if (template == null) return;
            if (locType.Contains("Dataset"))
            {
                IGxDataset pDataset = txtDB.Tag as IGxDataset;
                IFeatureDataset fDataset = ((IGxObject)pDataset).InternalObjectName.Open();
                workspace2 = fDataset.Workspace as IWorkspace2;
                createLoc = fDataset;
            }
            else
            {
                IGxDatabase pDatabase = txtDB.Tag as IGxDatabase;
               IFeatureWorkspace workspace = ((IGxObject) pDatabase).InternalObjectName.Open();
                workspace2 = workspace as IWorkspace2;
                createLoc = workspace;
            }

            if (createLoc == null) return;
            string fcName = txtName.EditValue.ToString().Trim();
            IWorkspace2 workSpace2 = createLoc is IFeatureDataset
                ? ((IFeatureDataset) createLoc).Workspace as IWorkspace2
                : createLoc as IWorkspace2;
            if (workSpace2.NameExists[esriDatasetType.esriDTFeatureClass, fcName])
            {
                MessageService.Current.Warn("该名称已经存在，请重新输入！");
                return ;
            }
            ISpatialReference pSpatialReference = _map.SpatialReference;
            
            IFieldsEdit pFieldsEdit=new Fields() as IFieldsEdit;
            IField pField = FieldHelper.CreateOIDField();
            pFieldsEdit.AddField(pField);
            if (locType.Contains("Dataset"))
            {
                IGeoDataset pFDataset = createLoc as IGeoDataset;
                pField = FieldHelper.CreateGeometryField(template.GeometryType, pFDataset.SpatialReference);
                pSpatialReference = pFDataset.SpatialReference;
                pFieldsEdit.AddField(pField);
            }
            else
            {
                pField = FieldHelper.CreateGeometryField(template.GeometryType, _map.SpatialReference);
                pFieldsEdit.AddField(pField);
            }
            string keyName = "";
            foreach (YTField ytField in template.Fields)
            {
                pField = ytField.CreateField();
                pFieldsEdit.AddField(pField);
                if (ytField.IsKey) keyName = ytField.Name;
            }

            IFeatureClass pClass=WorkspaceOperator.CreateFeatureClass(createLoc, txtName.Text, pSpatialReference, template.FeatureType,
                template.GeometryType, (IFields) pFieldsEdit, null, null, "");

            if (pClass == null)
            {
                MessageService.Current.Info("创建失败!");
                return;
            }
            if (pClass != null && chkIndex.Checked==false)
            {
                MapHelper.AddFeatureLayer((IBasicMap) _map,pClass);
                MessageService.Current.Info("创建成功并已经加载图层!");
                DialogResult = DialogResult.OK;
                return;
            }
            IEnvelope pEnv=new Envelope() as IEnvelope;
            pEnv.PutCoords(Convert.ToDouble(txtXMin.Text), Convert.ToDouble(txtYMin.Text), Convert.ToDouble(txtXMax.Text), Convert.ToDouble(txtYMax.Text));

            IWorkspaceEdit pWksEdit = ((IDataset) pClass).Workspace as IWorkspaceEdit;
            pWksEdit.StartEditing(false);
            pWksEdit.StartEditOperation();
            IndexHelper.CreateGridIndex(pClass, pEnv, Convert.ToDouble(txtWidth.Text),
                Convert.ToDouble(txtHeight.Text), keyName);
            pWksEdit.StopEditOperation();
            pWksEdit.StopEditing(true);
            MapHelper.AddFeatureLayer((IBasicMap)_map, pClass);
            MessageService.Current.Info("创建成功并已经加载图层!");
            DialogResult = DialogResult.OK;
        }
    }
}
