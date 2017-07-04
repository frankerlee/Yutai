using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Carto.MapCartoTemplateLib;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Services;
using WorkspaceHelper = Yutai.ArcGIS.Common.Helpers.WorkspaceHelper;

namespace Yutai.Plugins.Printing.Forms
{
    public partial class frmIndexMap : Form
    {
        private string _connectionString;
        private IIndexMap _indexMap;
        private PrintingPlugin _plugin;

        public frmIndexMap()
        {
            InitializeComponent();
        }

        public frmIndexMap(string connectionString, IIndexMap indexMap, PrintingPlugin plugin)
        {
            InitializeComponent();
            _connectionString = connectionString;
            _indexMap = indexMap;
            _plugin = plugin;
            FillTemplate();
            FillIndexMap();
        }

        private void FillTemplate()
        {
            if(_plugin.TemplateGallery!=null)
            //MapTemplateGallery tmpMapTemplateGallery=new MapTemplateGallery();
            //tmpMapTemplateGallery.SetWorkspace(_connectionString);
            foreach (MapTemplateClass templateClass in _plugin.TemplateGallery.MapTemplateClass)
            {
                    templateClass.Load();
                foreach (MapTemplate template in templateClass.mapTemplate)
                {
                    cmbTemplate.Properties.Items.Add(template.Name);
                }
            }
            
        }

        private void LoadField(IFeatureClass fClass)
        {
            cmbNameField.Properties.Items.Clear();
            chkQueryFields.Items.Clear();
            if (fClass != null)
            {
                IFields pFields = fClass.Fields;
                for (int i = 0; i < fClass.Fields.FieldCount; i++)
                {
                    IField pField = fClass.Fields.Field[i];
                    if (pField.Type != esriFieldType.esriFieldTypeString)

                    {
                        continue;
                    }
                    cmbNameField.Properties.Items.Add(pField.Name);
                    chkQueryFields.Items.Add(new CheckListFieldItem() { m_pField = pField });
                }
            }

        }
        private void FillIndexMap()
        {
            txtName.Text = _indexMap.Name;
            txtLayer.Text = _indexMap.IndexLayerName;
            txtLayer.Tag = _indexMap.WorkspaceName;
            if (string.IsNullOrEmpty(_indexMap.IndexLayerName)) return;
            IFeatureClass fClass = WorkspaceHelper.GetFeatureClass(_indexMap.WorkspaceName, _indexMap.IndexLayerName);
         
            cmbNameField.EditValue = _indexMap.NameField;
            cmbTemplate.EditValue = _indexMap.TemplateName;
            if (string.IsNullOrEmpty(_indexMap.SearchFields))
            {
                string[] sFlds = _indexMap.SearchFields.Split(',');
                for (int i = 0; i < sFlds.Length; i++)
                {
                    int idx=chkQueryFields.Items.IndexOf(sFlds[i]);
                    chkQueryFields.SetItemChecked(idx,true);
                }
            }
        }

        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        public IIndexMap IndexMap
        {
            get { return _indexMap; }
            set { _indexMap = value; }
        }

        private void btnLayer_Click(object sender, EventArgs e)
        {
            frmOpenFile openFile=new frmOpenFile();
            openFile.AllowMultiSelect = false;
            openFile.AddFilter(new MyGxFilterFeatureClasses(), true);
            if (openFile.ShowDialog() != DialogResult.OK) return;
            IGxObject gxObject = openFile.Items.get_Element(0) as IGxObject;
            IFeatureClassName pClassName = gxObject.InternalObjectName as IFeatureClassName;
            //IWorkspace pWorkspace=((IName) pClassName.FeatureDatasetName.WorkspaceName).Open();
            IFeatureClass pClass = ((IName) pClassName).Open();
            if (pClass.ShapeType != esriGeometryType.esriGeometryPolygon)
            {
                MessageService.Current.Warn("索引图层只能是多边形图层，请重新选择!");
                return;
            }
            txtLayer.Text = ((IDataset) pClass).Name;
            txtLayer.Tag = ((IDataset) pClass).Workspace.ConnectionProperties;
            LoadField(pClass);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            IIndexMap indexMap=new IndexMap()
            {
                Name=txtName.Text,
                TemplateName = cmbTemplate.EditValue.ToString(),
                IndexLayerName = txtLayer.EditValue.ToString(),
                WorkspaceName = txtLayer.Tag.ToString(),
                NameField = cmbNameField.EditValue.ToString()
            };

            string cc = "";
            foreach (var item in chkQueryFields.SelectedItems)
            {
                cc += ";" + item.ToString();
            }
            cc = cc.Substring(1);
            indexMap.SearchFields = cc;
            _indexMap = indexMap;
            DialogResult=DialogResult.OK;
        }

       

        internal class CheckListFieldItem
        {
            public IField m_pField;

            public override string ToString()
            {
                string result;
                if (this.m_pField == null)
                {
                    result = "";
                }
                else
                {
                    result = this.m_pField.Name;
                }
                return result;
            }
        }
    }
}
