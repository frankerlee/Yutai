using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class SceneRenderPropertyPage : UserControl, ILayerAndStandaloneTablePropertyPage
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private I3DProperties i3DProperties_0 = null;
        private IBasicMap ibasicMap_0 = null;
        private IContainer icontainer_0 = null;
        private ILayer ilayer_0 = null;

        public SceneRenderPropertyPage()
        {
            this.InitializeComponent();
            this.Text = "渲染设置";
        }

        public bool Apply()
        {
            if (this.bool_0)
            {
                this.bool_0 = false;
                if (this.rdordoRenderCache.Checked)
                {
                    this.i3DProperties_0.RenderMode = esriRenderMode.esriRenderCache;
                }
                else if (this.rdoRenderImmediate.Checked)
                {
                    this.i3DProperties_0.RenderMode = esriRenderMode.esriRenderImmediate;
                }
                if (this.rdoRenderAlways.Checked)
                {
                    this.i3DProperties_0.RenderVisibility = esriRenderVisibility.esriRenderAlways;
                }
                else if (this.rdoRenderWhenNavigating.Checked)
                {
                    this.i3DProperties_0.RenderVisibility = esriRenderVisibility.esriRenderWhenNavigating;
                }
                else if (this.rdoRenderWhenStopped.Checked)
                {
                    this.i3DProperties_0.RenderVisibility = esriRenderVisibility.esriRenderWhenStopped;
                }
                this.i3DProperties_0.RenderRefreshRate = (double) this.txtRenderRefreshRate.Value;
                this.i3DProperties_0.Illuminate = this.chkIlluminate.Checked;
                this.i3DProperties_0.SmoothShading = this.chkSmoothShading.Checked;
                this.i3DProperties_0.DepthPriorityValue = (short) (this.cboDepthPriorityValue.SelectedIndex + 1);
            }
            return true;
        }

        private void cboDepthPriorityValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_1)
            {
                this.bool_0 = true;
            }
        }

        private void chkIlluminate_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_1)
            {
                this.bool_0 = true;
            }
        }

        private void chkSmoothShading_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_1)
            {
                this.bool_0 = true;
            }
        }

        private void rdordoRenderCache_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_1)
            {
                this.bool_0 = true;
            }
        }

        private void rdoRenderAlways_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_1)
            {
                this.bool_0 = true;
            }
        }

        private void rdoRenderImmediate_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_1)
            {
                this.bool_0 = true;
            }
        }

        private void rdoRenderWhenNavigating_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_1)
            {
                this.bool_0 = true;
            }
        }

        private void rdoRenderWhenStopped_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_1)
            {
                this.bool_0 = true;
            }
        }

        private void SceneRenderPropertyPage_Load(object sender, EventArgs e)
        {
            ILayerExtensions extensions = this.ilayer_0 as ILayerExtensions;
            for (int i = 0; i <= (extensions.ExtensionCount - 1); i++)
            {
                if (extensions.get_Extension(i) is I3DProperties)
                {
                    I3DProperties properties = extensions.get_Extension(i) as I3DProperties;
                    this.i3DProperties_0 = properties;
                    if (this.i3DProperties_0.RenderMode == esriRenderMode.esriRenderCache)
                    {
                        this.rdordoRenderCache.Checked = true;
                    }
                    else if (this.i3DProperties_0.RenderMode == esriRenderMode.esriRenderImmediate)
                    {
                        this.rdoRenderImmediate.Checked = true;
                    }
                    if (this.i3DProperties_0.RenderVisibility == esriRenderVisibility.esriRenderAlways)
                    {
                        this.rdoRenderAlways.Checked = true;
                    }
                    else if (this.i3DProperties_0.RenderVisibility == esriRenderVisibility.esriRenderWhenNavigating)
                    {
                        this.rdoRenderWhenNavigating.Checked = true;
                    }
                    else if (this.i3DProperties_0.RenderVisibility == esriRenderVisibility.esriRenderWhenStopped)
                    {
                        this.rdoRenderWhenStopped.Checked = true;
                    }
                    this.txtRenderRefreshRate.Value = (decimal) this.i3DProperties_0.RenderRefreshRate;
                    this.chkIlluminate.Checked = this.i3DProperties_0.Illuminate;
                    this.chkSmoothShading.Checked = this.i3DProperties_0.SmoothShading;
                    this.cboDepthPriorityValue.SelectedIndex = this.i3DProperties_0.DepthPriorityValue - 1;
                    break;
                }
            }
            this.bool_1 = true;
        }

        private void txtRenderRefreshRate_ValueChanged(object sender, EventArgs e)
        {
            if (this.bool_1)
            {
                this.bool_0 = true;
            }
        }

        public IBasicMap FocusMap
        {
            set { this.ibasicMap_0 = value; }
        }

        public bool IsPageDirty
        {
            get { return this.bool_0; }
        }

        public object SelectItem
        {
            set { this.ilayer_0 = value as ILayer; }
        }
    }
}