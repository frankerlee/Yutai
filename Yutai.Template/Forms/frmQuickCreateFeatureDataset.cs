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
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Services;
using Yutai.Plugins.Template.Interfaces;

namespace Yutai.Plugins.Template.Forms
{
    public partial class frmQuickCreateFeatureDataset : Form
    {
        private IMap _map;

        private IAppContext _context;
        private TemplatePlugin _plugin;
        private List<IObjectTemplate> _templates = null;
        public frmQuickCreateFeatureDataset()
        {
            InitializeComponent();
        }
        public frmQuickCreateFeatureDataset(IAppContext context, TemplatePlugin plugin)
        {
            InitializeComponent();
            _context = context;
            _map = _context.FocusMap;
            _plugin = plugin;
            ISpatialReference spatialReference = _map.SpatialReference;
            txtSpatialRef.Text = spatialReference.Name;

            IActiveView pActiveView = _map as IActiveView;
            IEnvelope pEnv = pActiveView.Extent;
            txtXMin.EditValue = Math.Floor(pEnv.XMin);
            txtYMin.EditValue = Math.Floor(pEnv.YMin);
            txtXMax.EditValue = Math.Ceiling(pEnv.XMax);
            txtYMax.EditValue = Math.Ceiling(pEnv.YMax);
            LoadObjectDatasets();
            chkNamePre.Checked = true;
            chkNameNext.Checked = false;

        }

        private void LoadObjectDatasets()
        {
            if (_plugin.TemplateDatabase.Datasets == null || _plugin.TemplateDatabase.Datasets.Count==0)
            {
                _plugin.TemplateDatabase.Connect();
                _plugin.TemplateDatabase.LoadDatasets();
                _plugin.TemplateDatabase.DisConnect();
            }
            foreach (IObjectDataset template in _plugin.TemplateDatabase.Datasets)
            {
                cmbTemplate.Items.Add(template.Name);
            }
        }
        private void LoadTemplates()
        {
            if (_plugin.TemplateDatabase.Templates == null || _plugin.TemplateDatabase.Templates.Count == 0)
            {
                _plugin.TemplateDatabase.Connect();
                List<IObjectTemplate> templates =
                    _plugin.TemplateDatabase.GetTemplatesByDataset(cmbTemplate.SelectedItem.ToString());
                _plugin.TemplateDatabase.DisConnect();
            }
        }

        private void btnDB_Click(object sender, EventArgs e)
        {
            frmOpenFile openFile = new frmOpenFile();
            openFile.AllowMultiSelect = false;
            openFile.AddFilter(new MyGxFilterWorkspaces(), true);
         
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

        

        private void chkNameNext_CheckedChanged(object sender, EventArgs e)
        {
            txtNameNext.Visible = chkNameNext.Checked;
            txtAliasNext.Visible = chkNameNext.Checked;
        }

        private void chkNamePre_CheckedChanged(object sender, EventArgs e)
        {
            txtPreName.Visible = chkNamePre.Checked;
            txtAliasPre.Visible = chkNamePre.Checked;
        }

        private void cmbTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkFeatureClasses.Items.Clear();
            string dsName = cmbTemplate.Text;
            _plugin.TemplateDatabase.Connect();
          _templates =
                _plugin.TemplateDatabase.GetTemplatesByDataset(dsName);
            _plugin.TemplateDatabase.DisConnect();
            if (_templates == null) return;
            foreach (IObjectTemplate template in _templates)
            {
                chkFeatureClasses.Items.Add(new ObjectTemplateInfo(template), true);
            }
        }

        private class ObjectTemplateInfo
        {
            public IObjectTemplate _Template;

            public ObjectTemplateInfo(IObjectTemplate template)
            {
                _Template = template;
            }

            public override string ToString()
            {
                return _Template.Name + ";基本名称:(" + _Template.BaseName + ");基本别名:(" + _Template.AliasName + ")";
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DialogResult=DialogResult.Cancel;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (Validate(true) == false) return;
           
            object createLoc;
            IWorkspace2 workspace2 = null;
            IObjectDataset dataset =
                _plugin.TemplateDatabase.Datasets.FirstOrDefault(c => c.Name == cmbTemplate.SelectedItem.ToString());
            if (dataset == null) return;
          
                IGxDatabase pDataset = txtDB.Tag as IGxDatabase;
                IWorkspace workspace = ((IGxObject)pDataset).InternalObjectName.Open();
                workspace2 = workspace as IWorkspace2;
               

            string namePre = txtPreName.Text.Trim();
            string nameNext = txtNameNext.Text.Trim();
            string aliasPre = txtAliasPre.Text.Trim();
            string aliasNext = txtAliasNext.Text.Trim();
            if (!chkNamePre.Checked) { namePre = "";
                aliasPre = "";
            }
            if (!chkNameNext.Checked) { nameNext = "";
                aliasNext = "";
            }

            string dsName = CombineName(dataset.BaseName, namePre, nameNext);


            if (workspace == null) return;
            
          
        
            if (workspace2.NameExists[esriDatasetType.esriDTFeatureDataset, dsName])
            {
                MessageService.Current.Warn("该名称已经存在，请重新输入！");
                return;
            }
            ISpatialReference pSpatialReference = _map.SpatialReference;

            IFeatureDataset pNewDataset =
                WorkspaceOperator.CreateFeatureDataSet(workspace, dsName, pSpatialReference) as IFeatureDataset;
            string dsAliasName= CombineName(dataset.AliasName, aliasPre, aliasNext);
            
            //(pNewDataset as IClassSchemaEdit).AlterAliasName(dsAliasName);
            foreach (int selectedItem in chkFeatureClasses.CheckedIndices)
            {

                IObjectTemplate template = _templates[selectedItem];
                IFieldsEdit pFieldsEdit = new Fields() as IFieldsEdit;
                IField pField = FieldHelper.CreateOIDField();
                pFieldsEdit.AddField(pField);
               
                    pField = FieldHelper.CreateGeometryField(template.GeometryType, _map.SpatialReference);
                    pFieldsEdit.AddField(pField);
                
                string keyName = "";
                foreach (IYTField ytField in template.Fields)
                {
                    pField = ytField.CreateField();
                    pFieldsEdit.AddField(pField);
                    if (ytField.IsKey) keyName = ytField.Name;
                }

                string fcName = CombineName(template.BaseName, namePre, nameNext);
                string fcAliasName = CombineName(template.AliasName, aliasPre, aliasNext);
                IFeatureClass pClass = WorkspaceOperator.CreateFeatureClass(pNewDataset, fcName, pSpatialReference, template.FeatureType,
                    template.GeometryType, (IFields)pFieldsEdit, null, null, "");
                (pClass as IClassSchemaEdit).AlterAliasName(fcAliasName);
               

                if (pClass == null)
                {
                    MessageService.Current.Info(fcName+"创建失败!");
                    continue;
                }

            }
            MapHelper.AddDataset(_map as IBasicMap, pNewDataset, dsAliasName);
            MessageService.Current.Info("创建成功层!");
            DialogResult = DialogResult.OK;
        }

        private string CombineName(string datasetBaseName, string namePre, string nameNext)
        {
            return namePre + datasetBaseName + nameNext;
        }

        private bool Validate(bool showMsg)
        {
            string locType = label1.Tag != null ? label1.Tag.ToString() : "";
            if (string.IsNullOrEmpty(locType))
            {
                if (showMsg) MessageService.Current.Warn("请选择保存位置！");
                return false;
            }
            if (cmbTemplate.SelectedIndex < 0)
            {
                if (showMsg) MessageService.Current.Warn("请选择图层组模板！");
                return false;
            }
            if (chkNamePre.Checked &&
               string.IsNullOrEmpty(txtPreName.Text.Trim()))
            {
                if (showMsg) MessageService.Current.Warn("请输入图层前缀！");
                return false;
            }
            if (chkNameNext.Checked &&
              string.IsNullOrEmpty(txtNameNext.Text.Trim()))
            {
                if (showMsg) MessageService.Current.Warn("请输入图层后缀！");
                return false;
            }
            return true;
        }
    }
}
