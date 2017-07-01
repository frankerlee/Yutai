using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Syncfusion.Windows.Forms.Tools;
using Yutai.Plugins.Identifer.Common;
using Yutai.Shared;

namespace Yutai.Plugins.Identifer.Query
{
    public partial class UcAttributeQuery : UserControl
    {
        private IScene _scene;
        private IBasicMap _map;

        private ILayer _layer = null;

        private esriSelectionResultEnum _selectionResultEnum = esriSelectionResultEnum.esriSelectionResultNew;

        public IBasicMap Map
        {
            set { this._map = value; }
        }

        public UcAttributeQuery()
        {
            this.InitializeComponent();
            this.attributeQueryBuliderControl.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(this.attributeQueryBuliderControl);
            this.Text = "属性查询";
        }

        private void AttributeQueryControl_Load(object sender, EventArgs e)
        {
            this.InitLayers();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (this._layer != null)
            {
                this.attributeQueryBuliderControl.Apply();
                try
                {
                    IQueryFilter queryFilterClass = new QueryFilter()
                    {
                        WhereClause = this.attributeQueryBuliderControl.WhereCaluse
                    };
                    IFeatureSelection ilayer0 = this._layer as IFeatureSelection;
                    if (ilayer0 != null)
                    {
                        ilayer0.SelectFeatures(queryFilterClass, this._selectionResultEnum, false);
                        if (ilayer0.SelectionSet.Count >= 1)
                        {
                            (this._map as IActiveView).Refresh();
                        }
                        else
                        {
                            MessageBox.Show("没有符合条件的纪录！");
                        }
                    }
                }
                catch (Exception exception)
                {
                    Logger.Current.Warn("属性查询出错", exception, null);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.attributeQueryBuliderControl.ClearWhereCaluse();
        }

        private void cboSelectType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._selectionResultEnum = (esriSelectionResultEnum) this.cboSelectType.SelectedIndex;
        }

        private void chkShowSelectbaleLayer_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this._map.LayerCount; i++)
            {
                ILayer layer = this._map.Layer[i];
                if (layer is IFeatureLayer &&
                    (!this.chkShowSelectbaleLayer.Checked || (layer as IFeatureLayer).Selectable))
                {
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
                this._layer = (this.comboBoxLayer.SelectedItem as LayerObject).Layer;
                this.attributeQueryBuliderControl.CurrentLayer = this._layer;
            }
        }


        private void LoadGroupLayer(ICompositeLayer compLayer)
        {
            for (int i = 0; i < compLayer.Count; i++)
            {
                ILayer layer = compLayer.Layer[i];
                if (layer is IGroupLayer)
                {
                    this.LoadGroupLayer(layer as ICompositeLayer);
                }
                else if (layer is IFeatureLayer)
                {
                    this.comboBoxLayer.Items.Add(new LayerObject(layer));
                }
            }
        }

        private void InitLayers()
        {
            this.comboBoxLayer.Items.Clear();
            for (int i = 0; i < this._map.LayerCount; i++)
            {
                ILayer layer = this._map.Layer[i];
                if (layer is IGroupLayer)
                {
                    this.LoadGroupLayer(layer as ICompositeLayer);
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
    }
}