using System.ComponentModel;
using System.Drawing;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Framework.Docking;

namespace Yutai.ArcGIS.Framework
{
    public class MyDockContext : DockContent
    {
        private IContainer icontainer_0 = null;

        public MyDockContext()
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
            base.SuspendLayout();
            base.ClientSize = new Size(0x124, 0x111);
            base.DockAreas = DockAreas.DockBottom | DockAreas.DockTop | DockAreas.DockRight | DockAreas.DockLeft | DockAreas.Float;
            base.HideOnClose = true;
            base.Name = "MyDockContext";
            base.ResumeLayout(false);
        }
    }
}

