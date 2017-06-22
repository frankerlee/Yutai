using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public partial class MapTemplateTypePage : UserControl
    {
        private IContainer icontainer_0 = null;
        [CompilerGenerated]

        public MapTemplateTypePage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.rdoStandard.Checked)
            {
                this.MapTemplate.MapFramingType = MapFramingType.StandardFraming;
            }
            else
            {
                this.MapTemplate.MapFramingType = MapFramingType.AnyFraming;
            }
        }

 private void MapTemplateTypePage_Load(object sender, EventArgs e)
        {
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void rdoStandard_CheckedChanged(object sender, EventArgs e)
        {
        }

        public MapCartoTemplateLib.MapTemplate MapTemplate
        {
            [CompilerGenerated]
            get
            {
                return this.mapTemplate_0;
            }
            [CompilerGenerated]
            set
            {
                this.mapTemplate_0 = value;
            }
        }
    }
}

