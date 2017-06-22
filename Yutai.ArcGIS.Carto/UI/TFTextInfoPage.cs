using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class TFTextInfoPage : UserControl
    {
        private IContainer icontainer_0 = null;
        private YTTKAssiatant jlktkassiatant_0 = null;

        public TFTextInfoPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            this.jlktkassiatant_0.LeftLowText = this.txtLeftLowTxt.Text;
            this.jlktkassiatant_0.RightLowText = this.txtRightLowTxt.Text;
            this.jlktkassiatant_0.RightUpText = this.txtRightUpTxt.Text;
            this.jlktkassiatant_0.LeftBorderOutText = this.txtZTDW.Text;
            return true;
        }

 public void Init()
        {
            this.txtLeftLowTxt.Text = this.jlktkassiatant_0.LeftLowText;
            this.txtRightLowTxt.Text = this.jlktkassiatant_0.RightLowText;
            this.txtRightUpTxt.Text = this.jlktkassiatant_0.RightUpText;
            this.txtZTDW.Text = this.jlktkassiatant_0.LeftBorderOutText;
        }

 private void TFTextInfoPage_Load(object sender, EventArgs e)
        {
            this.Init();
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

