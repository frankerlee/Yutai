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
using Syncfusion.Windows.Forms;
using Yutai.Plugins.Identifer.Common;

namespace Yutai.Plugins.Identifer.Query
{
    public partial class frmSetSelectableLayer : MetroForm
    {
        private IMap _mMap;

        private bool _isInit = false;


        public IMap FocusMap
        {
            set { this._mMap = value; }
        }

        public frmSetSelectableLayer()
        {
            this.InitializeComponent();
            //_layers=new Dictionary<int, ILayer>();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.checkedListBoxControl.Items.Count; i++)
            {
                object item = this.checkedListBoxControl.Items[i];
                if (this.checkedListBoxControl.GetItemChecked(i) == true)
                {
                    this.checkedListBoxControl.SetItemChecked(i, false);
                    ((item as LayerItem).Value as IFeatureLayer).Selectable = false;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.checkedListBoxControl.Items.Count; i++)
            {
                object item = this.checkedListBoxControl.Items[i];
                if (this.checkedListBoxControl.GetItemChecked(i) == false)
                {
                    this.checkedListBoxControl.SetItemChecked(i, true);
                    ((item as LayerItem).Value as IFeatureLayer).Selectable = true;
                }
            }
        }

        private void btnSwitchSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.checkedListBoxControl.Items.Count; i++)
            {
                object item = this.checkedListBoxControl.Items[i];
                if (this.checkedListBoxControl.GetItemChecked(i) == false)
                {
                    this.checkedListBoxControl.SetItemChecked(i, true);
                    ((item as LayerItem).Value as IFeatureLayer).Selectable = true;
                }
                else if (this.checkedListBoxControl.GetItemChecked(i) == true)
                {
                    this.checkedListBoxControl.SetItemChecked(i, false);
                    ((item as LayerItem).Value as IFeatureLayer).Selectable = false;
                }
            }
        }

        private void checkedListBoxControl_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (_isInit) return;
            object item = this.checkedListBoxControl.Items[e.Index];

            IFeatureLayer pLayer = (item as LayerItem).Value as IFeatureLayer;
            if (e.NewValue == CheckState.Checked)
                pLayer.Selectable = true;
            else
                pLayer.Selectable = false;
        }


        private void frmSetSelectableLayer_Load(object sender, EventArgs e)
        {
            //_layers.Clear();
            _isInit = true;
            for (int i = 0; i < this._mMap.LayerCount; i++)
            {
                ILayer layer = this._mMap.Layer[i];
                if (layer is IGroupLayer)
                {
                    this.FillCompLayer(layer as ICompositeLayer);
                }
                else if (layer is IFeatureLayer)
                {
                    IFeatureLayer featureLayer = layer as IFeatureLayer;
                    LayerItem item = new LayerItem(featureLayer.Name, featureLayer);
                    CheckState checkState;
                    if (!featureLayer.Selectable)
                    {
                        checkState = CheckState.Unchecked;
                    }
                    else
                    {
                        checkState = CheckState.Checked;
                    }

                    this.checkedListBoxControl.Items.Add(item, checkState);
                }
            }
            _isInit = false;
        }


        private void FillCompLayer(ICompositeLayer compLayer)
        {
            for (int i = 0; i < compLayer.Count; i++)
            {
                ILayer layer = compLayer.Layer[i];
                if (layer is IGroupLayer)
                {
                    this.FillCompLayer(layer as ICompositeLayer);
                }
                else if (layer is IFeatureLayer)
                {
                    IFeatureLayer featureLayer = layer as IFeatureLayer;
                    LayerItem item = new LayerItem(featureLayer.Name, featureLayer);
                    bool checkState;
                    if (!featureLayer.Selectable)
                    {
                        checkState = false;
                    }
                    else
                    {
                        checkState = true;
                    }

                    this.checkedListBoxControl.Items.Add(item, checkState);
                }
            }
        }
    }
}