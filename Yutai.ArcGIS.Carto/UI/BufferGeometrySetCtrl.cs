using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Wrapper;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class BufferGeometrySetCtrl : UserControl
    {
        private Container container_0 = null;

        public BufferGeometrySetCtrl()
        {
            this.InitializeComponent();
        }

        private void BufferGeometrySetCtrl_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

        private void cboLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboLayers.SelectedIndex != -1)
            {
                BufferHelper.m_BufferHelper.m_pFeatureLayer =
                    (this.cboLayers.SelectedItem as ObjectWrap).Object as IFeatureLayer;
                this.lblSelectInfo.Text =
                    (BufferHelper.m_BufferHelper.m_pFeatureLayer as IFeatureSelection).SelectionSet.Count.ToString();
                if ((BufferHelper.m_BufferHelper.m_pFeatureLayer as IFeatureSelection).SelectionSet.Count > 0)
                {
                    this.chkOnlyUseSelectedFeature.Enabled = true;
                    BufferHelper.m_BufferHelper.bUseSelect = this.chkOnlyUseSelectedFeature.Checked;
                }
                else
                {
                    this.chkOnlyUseSelectedFeature.Enabled = false;
                    BufferHelper.m_BufferHelper.bUseSelect = false;
                }
            }
        }

        private void chkOnlyUseSelectedFeature_CheckedChanged(object sender, EventArgs e)
        {
            BufferHelper.m_BufferHelper.bUseSelect = this.chkOnlyUseSelectedFeature.Checked;
        }

        private void method_0()
        {
            UID uid = new UIDClass
            {
                Value = "{6CA416B1-E160-11D2-9F4E-00C04F6BC78E}"
            };
            IEnumLayer layer = BufferHelper.m_BufferHelper.m_pFocusMap.get_Layers(uid, true);
            layer.Reset();
            for (ILayer layer2 = layer.Next(); layer2 is IFeatureLayer; layer2 = layer.Next())
            {
                if (((layer2 as IFeatureLayer).FeatureClass != null) &&
                    ((layer2 as IFeatureLayer).FeatureClass.FeatureType == esriFeatureType.esriFTSimple))
                {
                    this.cboLayers.Properties.Items.Add(new ObjectWrap(layer2));
                }
            }
            if (this.cboLayers.Properties.Items.Count > 0)
            {
                this.cboLayers.SelectedIndex = 0;
            }
            if ((BufferHelper.m_BufferHelper.m_pFocusMap as IGraphicsContainer).Next() == null)
            {
                this.rdoUseGraphic.Enabled = false;
                this.rdoUseFeature.Checked = true;
                BufferHelper.m_BufferHelper.m_SourceType = 1;
                this.cboLayers.Enabled = true;
                if (this.cboLayers.SelectedIndex != -1)
                {
                    BufferHelper.m_BufferHelper.m_pFeatureLayer =
                        (this.cboLayers.SelectedItem as ObjectWrap).Object as IFeatureLayer;
                    this.lblSelectInfo.Text =
                        (BufferHelper.m_BufferHelper.m_pFeatureLayer as IFeatureSelection).SelectionSet.Count.ToString();
                    if ((BufferHelper.m_BufferHelper.m_pFeatureLayer as IFeatureSelection).SelectionSet.Count > 0)
                    {
                        this.chkOnlyUseSelectedFeature.Enabled = true;
                        BufferHelper.m_BufferHelper.bUseSelect = this.chkOnlyUseSelectedFeature.Checked;
                    }
                    else
                    {
                        this.chkOnlyUseSelectedFeature.Enabled = false;
                        BufferHelper.m_BufferHelper.bUseSelect = false;
                    }
                }
            }
            else
            {
                this.chkOnlyUseSelectedFeature.Enabled =
                    (BufferHelper.m_BufferHelper.m_pFocusMap as IGraphicsContainerSelect).ElementSelectionCount > 0;
                if (this.chkOnlyUseSelectedFeature.Enabled)
                {
                    BufferHelper.m_BufferHelper.bUseSelect = this.chkOnlyUseSelectedFeature.Checked;
                }
                else
                {
                    BufferHelper.m_BufferHelper.bUseSelect = false;
                }
            }
        }

        private void rdoUseFeature_Click(object sender, EventArgs e)
        {
            BufferHelper.m_BufferHelper.m_SourceType = 1;
            this.rdoUseGraphic.Checked = false;
            this.rdoUseFeature.Checked = true;
            this.cboLayers.Enabled = true;
            if (this.cboLayers.SelectedIndex != -1)
            {
                BufferHelper.m_BufferHelper.m_pFeatureLayer =
                    (this.cboLayers.SelectedItem as ObjectWrap).Object as IFeatureLayer;
                if ((BufferHelper.m_BufferHelper.m_pFeatureLayer as IFeatureSelection).SelectionSet.Count > 0)
                {
                    this.chkOnlyUseSelectedFeature.Enabled = true;
                    BufferHelper.m_BufferHelper.bUseSelect = this.chkOnlyUseSelectedFeature.Checked;
                }
                else
                {
                    this.chkOnlyUseSelectedFeature.Enabled = false;
                    BufferHelper.m_BufferHelper.bUseSelect = false;
                }
            }
        }

        private void rdoUseGraphic_Click(object sender, EventArgs e)
        {
            BufferHelper.m_BufferHelper.m_SourceType = 0;
            this.rdoUseGraphic.Checked = true;
            this.rdoUseFeature.Checked = false;
            this.cboLayers.Enabled = false;
            this.chkOnlyUseSelectedFeature.Enabled =
                (BufferHelper.m_BufferHelper.m_pFocusMap as IGraphicsContainerSelect).ElementSelectionCount > 0;
            if (this.chkOnlyUseSelectedFeature.Enabled)
            {
                BufferHelper.m_BufferHelper.bUseSelect = this.chkOnlyUseSelectedFeature.Checked;
            }
            else
            {
                BufferHelper.m_BufferHelper.bUseSelect = false;
            }
        }
    }
}