using System.ComponentModel;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib.ElementUI
{
    public class frmExpressSet : Form
    {
        private IContainer icontainer_0 = null;

        public frmExpressSet()
        {
            this.method_0();
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void method_0()
        {
            this.icontainer_0 = new Container();
            base.AutoScaleMode = AutoScaleMode.Font;
            this.Text = "frmExpressSet";
        }
    }
}