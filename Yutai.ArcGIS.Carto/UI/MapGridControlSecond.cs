using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class MapGridControlSecond : UserControl
    {
        private bool bool_0 = false;
        private Container container_0 = null;
        private IMapFrame imapFrame_0 = null;
        private IMapGrid imapGrid_0 = null;

        public MapGridControlSecond()
        {
            this.InitializeComponent();
        }

 public void Do()
        {
        }

 private void MapGridControlSecond_Load(object sender, EventArgs e)
        {
            this.method_0();
            this.bool_0 = true;
        }

        private void MapGridControlSecond_VisibleChanged(object sender, EventArgs e)
        {
        }

        private void method_0()
        {
            if ((this.imapGrid_0 != null) && (this.imapGrid_0 is IMeasuredGrid))
            {
                this.txtXSpace.Text = (this.imapGrid_0 as IMeasuredGrid).XIntervalSize.ToString();
                this.txtYSpace.Text = (this.imapGrid_0 as IMeasuredGrid).YIntervalSize.ToString();
            }
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((this.imapGrid_0 != null) && !(this.imapGrid_0 is IMeasuredGrid))
            {
            }
        }

        private void txtXSpace_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                double num = Convert.ToDouble(this.txtXSpace.Text);
                if ((this.imapGrid_0 != null) && (this.imapGrid_0 is IMeasuredGrid))
                {
                    (this.imapGrid_0 as IMeasuredGrid).XIntervalSize = num;
                }
            }
            catch
            {
            }
        }

        private void txtYSpace_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                double num = Convert.ToDouble(this.txtYSpace.Text);
                if ((this.imapGrid_0 != null) && (this.imapGrid_0 is IMeasuredGrid))
                {
                    (this.imapGrid_0 as IMeasuredGrid).YIntervalSize = num;
                }
            }
            catch
            {
            }
        }

        public IMapFrame MapFrame
        {
            set
            {
                this.imapFrame_0 = value;
            }
        }

        public IMapGrid MapGrid
        {
            set
            {
                this.imapGrid_0 = value;
                if (this.bool_0)
                {
                    this.method_0();
                }
            }
        }
    }
}

