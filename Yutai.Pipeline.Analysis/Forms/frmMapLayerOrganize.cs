using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yutai.Pipeline.Config.Concretes;
using Yutai.Pipeline.Config.Helpers;
using Yutai.Pipeline.Config.Interfaces;

namespace Yutai.Pipeline.Analysis.Forms
{
    public partial class frmMapLayerOrganize : XtraForm
    {
        private DataSet _dataSet;
        private DataTable _dataTable;
        private IMap _map;
        private IPipelineConfig _config;
        public frmMapLayerOrganize()
        {
            InitializeComponent();
        }

        public frmMapLayerOrganize(IMap pMap, IPipelineConfig config)
        {
            InitializeComponent();
            _map = pMap;
            _config = config;
            InitData();
        }

        public void InitMap(IMap pMap)
        {
            _map = pMap;
            InitData();
        }
        private void InitData()
        {
            _dataSet = new DataSet("管线配置");
            _dataTable = new DataTable("管线图层");
            DataColumn col = new DataColumn("Workspace", typeof(string));
            col.Caption = "工作空间";
            col.AllowDBNull = true;
            _dataTable.Columns.Add(col);
            col = new DataColumn("FeatureClass", typeof(string));
            col.Caption = "要素类";
            col.AllowDBNull = true;
            _dataTable.Columns.Add(col);
            col = new DataColumn("PipelineLayer", typeof(string));
            col.Caption = "管线组";
            col.AllowDBNull = true;
            _dataTable.Columns.Add(col);
            col = new DataColumn("BasicLayerInfo", typeof(string));
            col.Caption = "具体图层";
            col.AllowDBNull = true;
            _dataTable.Columns.Add(col);

            IArray pArrayClass = ConfigHelper.OrganizeMapWorkspaceAndLayer(_map);
            for(int i=0; i<pArrayClass.Count;i++)
            {
                PipeWorkspaceInfo info = pArrayClass.Element[i] as PipeWorkspaceInfo;
                for(int j=0; j<info.ClassArray.Count;j++)
                {
                    IFeatureClass pClass = info.ClassArray.Element[j] as IFeatureClass;
                    string pShortName = ConfigHelper.GetClassShortName(pClass);
                    DataRow row = _dataTable.NewRow();
                    row[0] = info.Workspace.PathName;
                    row[1] = pShortName;
                    _dataTable.Rows.Add(row);
                }
            }

            gridControl1.DataSource = _dataTable;
        }

        private void btnOrganize_Click(object sender, EventArgs e)
        {
            _config.OrganizeMap(_map);
            _dataTable.Rows.Clear();
            foreach(var oneItem in _config.Layers)
            {
                foreach(var oneSub in oneItem.Layers)
                {
                    if(oneSub.FeatureClass!=null)
                    {
                        DataRow row = _dataTable.NewRow();
                        row[0] = oneItem.Workspace.PathName;
                        row[1] = ConfigHelper.GetClassShortName(oneSub.FeatureClass);
                        row[2] = oneItem.Name;
                        row[3] = oneSub.Name;
                        _dataTable.Rows.Add(row);
                    }
                }
            }
        }
    }
}
