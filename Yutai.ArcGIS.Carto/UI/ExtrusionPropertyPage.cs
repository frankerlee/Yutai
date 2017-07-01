using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class ExtrusionPropertyPage : UserControl, ILayerAndStandaloneTablePropertyPage
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private I3DProperties i3DProperties_0 = null;
        private IBasicMap ibasicMap_0 = null;
        private IContainer icontainer_0 = null;
        private ILayer ilayer_0 = null;

        public ExtrusionPropertyPage()
        {
            this.InitializeComponent();
            this.Text = "Extrusion";
        }

        public bool Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                if (this.chkExtrusion.Checked)
                {
                    this.i3DProperties_0.ExtrusionType = (esriExtrusionType) (this.cboExtrusionType.SelectedIndex + 1);
                }
                else
                {
                    this.i3DProperties_0.ExtrusionType = esriExtrusionType.esriExtrusionNone;
                }
                this.i3DProperties_0.ExtrusionExpressionString = this.txtExtrusionValue.Text;
            }
            return true;
        }

        private void cboExtrusionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
            }
        }

        private void chkExtrusion_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                this.cboExtrusionType.Enabled = this.chkExtrusion.Checked;
            }
        }

        private void ExtrusionPropertyPage_Load(object sender, EventArgs e)
        {
            ILayerExtensions extensions = this.ilayer_0 as ILayerExtensions;
            this.cboExtrusionType.SelectedIndex = 0;
            for (int i = 0; i <= (extensions.ExtensionCount - 1); i++)
            {
                if (extensions.get_Extension(i) is I3DProperties)
                {
                    I3DProperties properties = extensions.get_Extension(i) as I3DProperties;
                    this.i3DProperties_0 = properties;
                    if (this.i3DProperties_0.ExtrusionType == esriExtrusionType.esriExtrusionNone)
                    {
                        this.chkExtrusion.Checked = false;
                        this.cboExtrusionType.Enabled = false;
                    }
                    else
                    {
                        this.chkExtrusion.Checked = true;
                        this.cboExtrusionType.SelectedIndex = ((int) this.i3DProperties_0.ExtrusionType) - 1;
                    }
                    this.txtExtrusionValue.Text = this.i3DProperties_0.ExtrusionExpressionString;
                    break;
                }
            }
            this.bool_0 = true;
        }

        private void txtExtrusionValue_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
            }
        }

        public IBasicMap FocusMap
        {
            set { this.ibasicMap_0 = value; }
        }

        public bool IsPageDirty
        {
            get { return this.bool_1; }
        }

        public object SelectItem
        {
            set { this.ilayer_0 = value as ILayer; }
        }
    }
}