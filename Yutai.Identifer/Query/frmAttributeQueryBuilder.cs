using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Syncfusion.Windows.Forms;
using Yutai.Plugins.Identifer.Common;
using Yutai.Plugins.Identifer.Enums;
using Yutai.Plugins.Identifer.Helpers;
using Yutai.Plugins.Interfaces;
using Yutai.Shared;

namespace Yutai.Plugins.Identifer.Query
{
    public partial class frmAttributeQueryBuilder : MetroForm
    {
        //后期可能改命令可以同时对二维和三维窗口进行查询

        private UcAttributeQueryBuilder m_pAttributeQuery = new UcAttributeQueryBuilder();

        private IAppContext _context;

        private IBasicMap m_pMap;

        private IScene m_pScene;


        private QueryTargerEnum _queryTargerEnum;
        private ILayer m_SelLayer = null;

        private esriSelectionResultEnum m_SelectionResultType = esriSelectionResultEnum.esriSelectionResultNew;

        public QueryTargerEnum QueryTargerEnum
        {
            get { return _queryTargerEnum; }
            set { _queryTargerEnum = value; }
        }

        public IBasicMap Map
        {
            set
            {
                this.m_pMap = value;
                _queryTargerEnum = QueryTargerEnum.View2D;
            }
        }

        public IScene Scene
        {
            set
            {
                this.m_pScene = value;
                _queryTargerEnum = QueryTargerEnum.View3D;
            }
        }

        public frmAttributeQueryBuilder(IAppContext context)
        {
            this.InitializeComponent();
            this.m_pAttributeQuery.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(this.m_pAttributeQuery);
            _context = context;
        }

        public frmAttributeQueryBuilder()
        {
            this.InitializeComponent();
            this.m_pAttributeQuery.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(this.m_pAttributeQuery);
        }

        private void AddGroupLayer(ICompositeLayer pCompositeLayer)
        {
            for (int i = 0; i < pCompositeLayer.Count; i++)
            {
                ILayer layer = pCompositeLayer.Layer[i];
                if (layer is IGroupLayer)
                {
                    this.AddGroupLayer(layer as ICompositeLayer);
                }
                else if (layer is IFeatureLayer)
                {
                    this.comboBoxLayer.Items.Add(new LayerObject(layer));
                }
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (this.m_SelLayer != null)
            {
                this.m_pAttributeQuery.Apply();
                try
                {
                    IQueryFilter queryFilterClass = new QueryFilter()
                    {
                        WhereClause = this.m_pAttributeQuery.WhereCaluse
                    };
                    IFeatureSelection mSelLayer = this.m_SelLayer as IFeatureSelection;
                    if (mSelLayer != null)
                    {
                        mSelLayer.SelectFeatures(queryFilterClass, this.m_SelectionResultType, false);
                        if (mSelLayer.SelectionSet.Count < 1)
                        {
                            MessageBox.Show("没有符合条件的纪录！");
                        }
                        else if (!this.chkZoomToSelect.Checked)
                        {
                            if (_queryTargerEnum == QueryTargerEnum.View2D)
                                (this.m_pMap as IActiveView).Refresh();
                            else
                            {
                                (this.m_pScene as IActiveView).Refresh();
                            }
                        }
                        else
                        {
                            if (_queryTargerEnum == QueryTargerEnum.View2D)
                                CommonHelper.Zoom2SelectedFeature(this.m_pMap as IActiveView);
                            else
                            {
                                CommonHelper.Zoom2SelectedFeature(this.m_pScene as IActiveView);
                            }
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                catch (Exception exception)
                {
                    Logger.Current.Error("信息查询错误", exception.Message);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.m_pAttributeQuery.ClearWhereCaluse();
        }

        private void cboSelectType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.m_SelectionResultType = (esriSelectionResultEnum) this.cboSelectType.SelectedIndex;
        }

        private void chkShowSelectbaleLayer_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.m_pMap.LayerCount; i++)
            {
                ILayer layer = this.m_pMap.Layer[i];
                if (layer is IFeatureLayer)
                {
                    if (this.chkShowSelectbaleLayer.Checked)
                    {
                        if (!(layer as IFeatureLayer).Selectable)
                        {
                            continue;
                        }
                    }
                    this.comboBoxLayer.Items.Add(new LayerObject(layer));
                }
            }
            if (this.comboBoxLayer.Items.Count > 0)
            {
                this.comboBoxLayer.SelectedIndex = 0;
            }
        }

        private void comboBoxLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxLayer.SelectedItem != null)
            {
                this.m_SelLayer = (this.comboBoxLayer.SelectedItem as LayerObject).Layer;
                this.m_pAttributeQuery.CurrentLayer = this.m_SelLayer;
            }
        }


        private void frmAttributeQueryBuilder_Load(object sender, EventArgs e)
        {
            cmbMap.SelectedIndex = (int) _queryTargerEnum;
        }

        private void InitControl()
        {
            if (_queryTargerEnum == QueryTargerEnum.View2D)
            {
                if (m_pMap == null && _context != null) m_pMap = _context.MapControl.Map as IBasicMap;
                InitControlByMap();
            }
            else
            {
                if (m_pScene == null && _context != null && _context.SceneControl != null)
                {
                    m_pScene = _context.SceneControl.Scene as IScene;
                    InitControlByScene();
                }
                else
                {
                    cmbMap.Enabled = false;
                    cmbMap.SelectedIndex = 0;
                }
            }
        }

        private void InitControlByMap()
        {
            this.comboBoxLayer.Items.Clear();
            for (int i = 0; i < this.m_pMap.LayerCount; i++)
            {
                ILayer layer = this.m_pMap.Layer[i];
                if (layer is IGroupLayer)
                {
                    this.AddGroupLayer(layer as ICompositeLayer);
                }
                else if (layer is IFeatureLayer)
                {
                    this.comboBoxLayer.Items.Add(new LayerObject(layer));
                }
            }
            if (this.comboBoxLayer.Items.Count > 0)
            {
                this.comboBoxLayer.SelectedIndex = 0;
            }
        }

        private void InitControlByScene()
        {
            this.comboBoxLayer.Items.Clear();
            for (int i = 0; i < this.m_pScene.LayerCount; i++)
            {
                ILayer layer = this.m_pScene.Layer[i];
                if (layer is IGroupLayer)
                {
                    this.AddGroupLayer(layer as ICompositeLayer);
                }
                else if (layer is IFeatureLayer)
                {
                    this.comboBoxLayer.Items.Add(new LayerObject(layer));
                }
            }
            if (this.comboBoxLayer.Items.Count > 0)
            {
                this.comboBoxLayer.SelectedIndex = 0;
            }
        }

        private void cmbMap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMap.SelectedIndex == 0)
            {
                _queryTargerEnum = QueryTargerEnum.View2D;
            }
            else
            {
                _queryTargerEnum = QueryTargerEnum.View3D;
            }
            InitControl();
        }
    }
}