using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class TKStyleSelectPage : UserControl
    {
        private IContainer icontainer_0 = null;
        private YTTKAssiatant jlktkassiatant_0 = null;

        public TKStyleSelectPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            this.jlktkassiatant_0.TKType = this.rdoStandard.Checked ? TKType.TKStandard : TKType.TKRand;
            return true;
        }

 private void TKStyleSelectPage_Load(object sender, EventArgs e)
        {
            this.rdoStandard.Checked = this.jlktkassiatant_0.TKType == TKType.TKStandard;
        }

        internal YTTKAssiatant YTTKAssiatant
        {
            set
            {
                this.jlktkassiatant_0 = value;
            }
        }
    }
}

