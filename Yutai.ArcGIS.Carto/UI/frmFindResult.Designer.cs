using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class frmFindResult
    {
        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

       
        private void InitializeComponent()
        {
            this.findResultControl1 = new FindResultControl();
            base.SuspendLayout();
            this.findResultControl1.Dock = DockStyle.Fill;
            this.findResultControl1.Location = new Point(0, 0);
            this.findResultControl1.Name = "findResultControl1";
            this.findResultControl1.Size = new Size(448, 165);
            this.findResultControl1.TabIndex = 0;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(448, 165);
            base.Controls.Add(this.findResultControl1);
            base.Name = "frmFindResult";
            this.Text = "查找结果";
            base.Closing += new CancelEventHandler(this.frmFindResult_Closing);
            base.ResumeLayout(false);
        }

       
        private FindResultControl findResultControl1;
    }
}