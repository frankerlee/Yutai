using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Wrapper;

namespace Yutai.ArcGIS.Common.Query.UI
{
    public partial class AttributeQueryControl : UserControl, IDockContent
    {
        private AttributeQueryBuliderControl attributeQueryBuliderControl_0 = new AttributeQueryBuliderControl();
        private Container container_0 = null;
        private esriSelectionResultEnum esriSelectionResultEnum_0 = esriSelectionResultEnum.esriSelectionResultNew;
        private ILayer ilayer_0 = null;

        public AttributeQueryControl()
        {
            this.InitializeComponent();
            this.attributeQueryBuliderControl_0.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(this.attributeQueryBuliderControl_0);
            this.Text = "属性查询";
        }

        private void AttributeQueryControl_Load(object sender, EventArgs e)
        {
            this.method_1();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (this.ilayer_0 != null)
            {
                this.attributeQueryBuliderControl_0.Apply();
                try
                {
                    IQueryFilter filter = new QueryFilter{
                        WhereClause = this.attributeQueryBuliderControl_0.WhereCaluse
                    };
                    IFeatureSelection selection = this.ilayer_0 as IFeatureSelection;
                    if (selection != null)
                    {
                        selection.SelectFeatures(filter, this.esriSelectionResultEnum_0, false);
                        if (selection.SelectionSet.Count < 1)
                        {
                            MessageBox.Show("没有符合条件的纪录！");
                        }
                        else
                        {
                            (this.ibasicMap_0 as IActiveView).Refresh();
                        }
                    }
                }
                catch (Exception exception)
                {
                    CErrorLog.writeErrorLog(this, exception, "");
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.attributeQueryBuliderControl_0.ClearWhereCaluse();
        }

        private void cboSelectType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.esriSelectionResultEnum_0 = (esriSelectionResultEnum) this.cboSelectType.SelectedIndex;
        }

        private void chkShowSelectbaleLayer_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.ibasicMap_0.LayerCount; i++)
            {
                ILayer layer = this.ibasicMap_0.get_Layer(i);
                if ((layer is IFeatureLayer) && (!this.chkShowSelectbaleLayer.Checked || (layer as IFeatureLayer).Selectable))
                {
                    this.comboBoxLayer.Properties.Items.Add(new LayerObject(layer));
                }
            }
            if (this.comboBoxLayer.Properties.Items.Count > 0)
            {
                this.comboBoxLayer.SelectedIndex = 0;
            }
        }

        private void comboBoxLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxLayer.SelectedItem != null)
            {
                this.ilayer_0 = (this.comboBoxLayer.SelectedItem as LayerObject).Layer;
                this.attributeQueryBuliderControl_0.CurrentLayer = this.ilayer_0;
            }
        }

 private void method_0(ICompositeLayer icompositeLayer_0)
        {
            for (int i = 0; i < icompositeLayer_0.Count; i++)
            {
                ILayer layer = icompositeLayer_0.get_Layer(i);
                if (layer is IGroupLayer)
                {
                    this.method_0(layer as ICompositeLayer);
                }
                else if (layer is IFeatureLayer)
                {
                    this.comboBoxLayer.Properties.Items.Add(new LayerObject(layer));
                }
            }
        }

        private void method_1()
        {
            this.comboBoxLayer.Properties.Items.Clear();
            for (int i = 0; i < this.ibasicMap_0.LayerCount; i++)
            {
                ILayer layer = this.ibasicMap_0.get_Layer(i);
                if (layer is IGroupLayer)
                {
                    this.method_0(layer as ICompositeLayer);
                }
                else if (layer is IFeatureLayer)
                {
                    this.comboBoxLayer.Properties.Items.Add(new LayerObject(layer));
                }
            }
            if (this.comboBoxLayer.Properties.Items.Count > 0)
            {
                this.comboBoxLayer.SelectedIndex = 0;
            }
        }

        public DockingStyle DefaultDockingStyle
        {
            get
            {
                return DockingStyle.Float;
            }
        }

        string IDockContent.Name
        {
            get
            {
                return base.Name;
            }
        }

        int IDockContent.Width
        {
            get
            {
                return base.Width;
            }
        }

        public IBasicMap Map
        {
            set
            {
                this.ibasicMap_0 = value;
            }
        }
    }
}

