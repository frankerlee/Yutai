using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Catalog.Geodatabase.UI;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class MapGridStylePropertyPage : UserControl
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Container container_0 = null;
        private IMapFrame imapFrame_0 = null;
        private IMapGrid imapGrid_0 = null;
        private IStyleGallery istyleGallery_0 = ApplicationBase.StyleGallery;

        public MapGridStylePropertyPage()
        {
            this.InitializeComponent();
        }

        private void btnGraticulestyle_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.istyleGallery_0);
                    selector.SetSymbol(this.imapGrid_0.LineSymbol);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        this.imapGrid_0.LineSymbol = selector.GetSymbol() as ILineSymbol;
                    }
                }
            }
            catch
            {
            }
        }

        private void btnIndexGridStyle_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.istyleGallery_0);
                    selector.SetSymbol(this.imapGrid_0.LineSymbol);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        this.imapGrid_0.LineSymbol = selector.GetSymbol() as ILineSymbol;
                    }
                }
            }
            catch
            {
            }
        }

        private void btnMeasuredGridStyle_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.istyleGallery_0);
                    if (this.rdoMeasuredGridStyle.SelectedIndex == 2)
                    {
                        selector.SetSymbol(this.imapGrid_0.LineSymbol);
                        if (selector.ShowDialog() == DialogResult.OK)
                        {
                            this.imapGrid_0.LineSymbol = selector.GetSymbol() as ILineSymbol;
                        }
                    }
                    else if (this.rdoMeasuredGridStyle.SelectedIndex == 1)
                    {
                        selector.SetSymbol(this.imapGrid_0.TickMarkSymbol);
                        if (selector.ShowDialog() == DialogResult.OK)
                        {
                            this.imapGrid_0.TickMarkSymbol = selector.GetSymbol() as IMarkerSymbol;
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void btnSpatialReference_Click(object sender, EventArgs e)
        {
            if (this.txtSpatialReference.Tag != null)
            {
                frmSpatialReference reference = new frmSpatialReference {
                    SpatialRefrence = this.txtSpatialReference.Tag as ISpatialReference
                };
                if (reference.ShowDialog() == DialogResult.OK)
                {
                    this.txtSpatialReference.Tag = reference.SpatialRefrence;
                    this.txtSpatialReference.Text = reference.SpatialRefrence.Name;
                }
            }
        }

        public void DegreeToDMS(double double_0, out double double_1, out double double_2, out double double_3)
        {
            Math.Sign(double_0);
            double_0 = Math.Abs(double_0);
            double_1 = Math.Floor(double_0);
            double_0 = (double_0 - double_1) * 60.0;
            double_2 = Math.Floor(double_0);
            double_3 = (double_0 - double_2) * 60.0;
        }

 public bool Do()
        {
            double num6;
            double num7;
            if (this.imapGrid_0 is IIndexGrid)
            {
                try
                {
                    int num = int.Parse(this.txtRowCount.Text);
                    (this.imapGrid_0 as IIndexGrid).RowCount = num;
                }
                catch
                {
                }
                try
                {
                    int num2 = int.Parse(this.txtColumnCount.Text);
                    (this.imapGrid_0 as IIndexGrid).ColumnCount = num2;
                    goto Label_023F;
                }
                catch
                {
                    goto Label_023F;
                }
            }
            if (this.imapGrid_0 is IGraticule)
            {
                double num3;
                double num4;
                double num5;
                try
                {
                    num3 = double.Parse(this.txtHatchIntervalXDegree.Text);
                    num4 = double.Parse(this.txtHatchIntervalXMinute.Text);
                    num5 = double.Parse(this.txtHatchIntervalXSecond.Text);
                }
                catch
                {
                    MessageBox.Show("数据格式错误!");
                    return false;
                }
                if (num3 < 0.0)
                {
                    num6 = (num3 - (num4 / 60.0)) - (num5 / 3600.0);
                }
                else
                {
                    num6 = (num3 + (num4 / 60.0)) + (num5 / 3600.0);
                }
                try
                {
                    num3 = double.Parse(this.txtHatchIntervalYDegree.Text);
                    num4 = double.Parse(this.txtHatchIntervalYMinute.Text);
                    num5 = double.Parse(this.txtHatchIntervalYSecond.Text);
                }
                catch
                {
                    MessageBox.Show("数据格式错误!");
                    return false;
                }
                if (num3 < 0.0)
                {
                    num7 = (num3 - (num4 / 60.0)) - (num5 / 3600.0);
                }
                else
                {
                    num7 = (num3 + (num4 / 60.0)) + (num5 / 3600.0);
                }
                (this.imapGrid_0 as IMeasuredGrid).XIntervalSize = num6;
                (this.imapGrid_0 as IMeasuredGrid).YIntervalSize = num7;
            }
            else if (this.imapGrid_0 is IMeasuredGrid)
            {
                try
                {
                    num6 = double.Parse(this.txtMeasuredGridXSpace.Text);
                    num7 = double.Parse(this.txtMeasuredGridYSpace.Text);
                }
                catch
                {
                    MessageBox.Show("数据格式错误!");
                    return false;
                }
                (this.imapGrid_0 as IMeasuredGrid).XIntervalSize = num6;
                (this.imapGrid_0 as IMeasuredGrid).YIntervalSize = num7;
            }
        Label_023F:
            return true;
        }

        public void Init()
        {
            this.bool_0 = false;
            if (this.imapGrid_0 is IIndexGrid)
            {
                this.groupBoxIndexGridSpace.Visible = true;
                this.groupBoxIndexGridStyle.Visible = true;
                this.groupBoxGraticuleSpace.Visible = false;
                this.groupBoxGraticuleStyle.Visible = false;
                this.groupBoxMeasuredGridStyle.Visible = false;
                this.groupBoxMeasuredGridSpace.Visible = false;
                this.groupBoxCoordinate.Visible = false;
                this.txtRowCount.Text = (this.imapGrid_0 as IIndexGrid).RowCount.ToString();
                this.txtColumnCount.Text = (this.imapGrid_0 as IIndexGrid).ColumnCount.ToString();
                if (this.imapGrid_0.LineSymbol != null)
                {
                    this.rdoIndexGridStyle.SelectedIndex = 1;
                    this.btnIndexGridStyle.Style = this.imapGrid_0.LineSymbol;
                    this.btnIndexGridStyle.Enabled = true;
                }
                else
                {
                    this.rdoMeasuredGridStyle.SelectedIndex = 0;
                    this.btnIndexGridStyle.Style = null;
                    this.btnIndexGridStyle.Enabled = false;
                }
            }
            else if (this.imapGrid_0 is IGraticule)
            {
                double num4;
                double num5;
                double num6;
                this.groupBoxIndexGridSpace.Visible = false;
                this.groupBoxIndexGridStyle.Visible = false;
                this.groupBoxGraticuleSpace.Visible = true;
                this.groupBoxGraticuleStyle.Visible = true;
                this.groupBoxMeasuredGridStyle.Visible = false;
                this.groupBoxMeasuredGridSpace.Visible = false;
                this.groupBoxCoordinate.Visible = false;
                double xIntervalSize = (this.imapGrid_0 as IMeasuredGrid).XIntervalSize;
                double yIntervalSize = (this.imapGrid_0 as IMeasuredGrid).YIntervalSize;
                this.DegreeToDMS(xIntervalSize, out num4, out num5, out num6);
                this.txtHatchIntervalXDegree.Text = num4.ToString();
                this.txtHatchIntervalXMinute.Text = num5.ToString("00");
                this.txtHatchIntervalXSecond.Text = num6.ToString("00.##");
                this.DegreeToDMS(yIntervalSize, out num4, out num5, out num6);
                this.txtHatchIntervalYDegree.Text = num4.ToString();
                this.txtHatchIntervalYMinute.Text = num5.ToString("00");
                this.txtHatchIntervalYSecond.Text = num6.ToString("00.##");
                this.btnGraticulestyle.Enabled = true;
                if (this.imapGrid_0.LineSymbol != null)
                {
                    this.rdoGraticuleStyle.SelectedIndex = 2;
                    this.btnGraticulestyle.Style = this.imapGrid_0.LineSymbol;
                }
                else if (this.imapGrid_0.TickMarkSymbol != null)
                {
                    this.rdoGraticuleStyle.SelectedIndex = 1;
                    this.btnGraticulestyle.Style = this.imapGrid_0.TickMarkSymbol;
                }
                else
                {
                    this.rdoGraticuleStyle.SelectedIndex = 0;
                    this.btnGraticulestyle.Style = null;
                    this.btnGraticulestyle.Enabled = false;
                }
            }
            else if (this.imapGrid_0 is IMeasuredGrid)
            {
                this.groupBoxIndexGridSpace.Visible = false;
                this.groupBoxIndexGridStyle.Visible = false;
                this.groupBoxGraticuleSpace.Visible = false;
                this.groupBoxGraticuleStyle.Visible = false;
                this.groupBoxMeasuredGridStyle.Visible = true;
                this.groupBoxMeasuredGridSpace.Visible = true;
                this.groupBoxCoordinate.Visible = true;
                if ((this.imapGrid_0 as IProjectedGrid).SpatialReference != null)
                {
                    this.txtSpatialReference.Text = (this.imapGrid_0 as IProjectedGrid).SpatialReference.Name;
                    this.txtSpatialReference.Tag = (this.imapGrid_0 as IProjectedGrid).SpatialReference;
                }
                else if (this.imapFrame_0.Map.SpatialReference != null)
                {
                    this.txtSpatialReference.Tag = this.imapFrame_0.Map.SpatialReference;
                    this.txtSpatialReference.Text = "<与数据框架相同>\r\n" + this.imapFrame_0.Map.SpatialReference.Name;
                }
                else
                {
                    this.txtSpatialReference.Text = "<无坐标系>";
                    this.txtSpatialReference.Tag = null;
                }
                this.txtMeasuredGridXSpace.Text = (this.imapGrid_0 as IMeasuredGrid).XIntervalSize.ToString("0.###");
                this.txtMeasuredGridYSpace.Text = (this.imapGrid_0 as IMeasuredGrid).YIntervalSize.ToString("0.###");
                this.btnMeasuredGridStyle.Enabled = true;
                if (this.imapGrid_0.LineSymbol != null)
                {
                    this.rdoMeasuredGridStyle.SelectedIndex = 2;
                    this.btnMeasuredGridStyle.Style = this.imapGrid_0.LineSymbol;
                }
                else if (this.imapGrid_0.TickMarkSymbol != null)
                {
                    this.rdoMeasuredGridStyle.SelectedIndex = 1;
                    this.btnMeasuredGridStyle.Style = this.imapGrid_0.TickMarkSymbol;
                }
                else
                {
                    this.rdoMeasuredGridStyle.SelectedIndex = 0;
                    this.btnMeasuredGridStyle.Style = null;
                    this.btnMeasuredGridStyle.Enabled = false;
                }
            }
            this.bool_0 = true;
        }

 private void MapGridStylePropertyPage_Load(object sender, EventArgs e)
        {
            this.Init();
            this.bool_1 = true;
        }

        private void rdoGraticuleStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.btnGraticulestyle.Enabled = true;
                switch (this.rdoGraticuleStyle.SelectedIndex)
                {
                    case 0:
                        this.imapGrid_0.LineSymbol = null;
                        this.imapGrid_0.TickMarkSymbol = null;
                        this.btnGraticulestyle.Style = null;
                        this.btnGraticulestyle.Enabled = false;
                        break;

                    case 1:
                    {
                        this.imapGrid_0.LineSymbol = null;
                        ISimpleMarkerSymbol symbol2 = new SimpleMarkerSymbolClass {
                            Style = esriSimpleMarkerStyle.esriSMSCross
                        };
                        this.imapGrid_0.TickMarkSymbol = symbol2;
                        this.btnGraticulestyle.Style = this.imapGrid_0.TickMarkSymbol;
                        break;
                    }
                    default:
                    {
                        this.imapGrid_0.TickMarkSymbol = null;
                        ISimpleLineSymbol symbol = new SimpleLineSymbolClass {
                            Width = 1.0
                        };
                        this.imapGrid_0.LineSymbol = symbol;
                        this.btnGraticulestyle.Style = this.imapGrid_0.LineSymbol;
                        break;
                    }
                }
                this.btnGraticulestyle.Invalidate();
            }
        }

        private void rdoIndexGridStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.btnIndexGridStyle.Enabled = true;
                switch (this.rdoIndexGridStyle.SelectedIndex)
                {
                    case 0:
                        this.imapGrid_0.LineSymbol = null;
                        this.btnIndexGridStyle.Style = null;
                        this.btnIndexGridStyle.Enabled = false;
                        break;

                    case 1:
                    {
                        ISimpleLineSymbol symbol = new SimpleLineSymbolClass {
                            Width = 1.0
                        };
                        this.imapGrid_0.LineSymbol = symbol;
                        this.btnIndexGridStyle.Style = this.imapGrid_0.LineSymbol;
                        break;
                    }
                }
                this.btnIndexGridStyle.Invalidate();
            }
        }

        private void rdoMeasuredGridStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.btnMeasuredGridStyle.Enabled = true;
                switch (this.rdoMeasuredGridStyle.SelectedIndex)
                {
                    case 0:
                        this.imapGrid_0.LineSymbol = null;
                        this.imapGrid_0.TickMarkSymbol = null;
                        this.btnMeasuredGridStyle.Style = null;
                        this.btnMeasuredGridStyle.Enabled = false;
                        break;

                    case 1:
                    {
                        this.imapGrid_0.LineSymbol = null;
                        ISimpleMarkerSymbol symbol2 = new SimpleMarkerSymbolClass {
                            Style = esriSimpleMarkerStyle.esriSMSCross
                        };
                        this.imapGrid_0.TickMarkSymbol = symbol2;
                        this.btnMeasuredGridStyle.Style = this.imapGrid_0.TickMarkSymbol;
                        break;
                    }
                    default:
                    {
                        ISimpleLineSymbol symbol = new SimpleLineSymbolClass {
                            Width = 1.0
                        };
                        this.imapGrid_0.TickMarkSymbol = null;
                        this.imapGrid_0.LineSymbol = symbol;
                        this.btnMeasuredGridStyle.Style = this.imapGrid_0.LineSymbol;
                        break;
                    }
                }
                this.btnMeasuredGridStyle.Invalidate();
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
                if (this.bool_1)
                {
                    this.Init();
                }
            }
        }
    }
}

