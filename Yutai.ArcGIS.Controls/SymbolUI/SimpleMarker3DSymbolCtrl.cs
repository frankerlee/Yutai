using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.SymbolUI;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    internal partial class SimpleMarker3DSymbolCtrl : UserControl, CommonInterface
    {
        private bool m_CanDo = false;
        public ISimpleMarker3DSymbol m_pSimpleMarker3DSymbol = null;
        public double m_unit = 1.0;

        public event ValueChangedHandler ValueChanged;

        public SimpleMarker3DSymbolCtrl()
        {
            this.InitializeComponent();
            Marker3DEvent.Marker3DChanged += new Marker3DEvent.Marker3DChangedHandler(this.Marker3DEvent_Marker3DChanged);
        }

        private void cboStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_pSimpleMarker3DSymbol.Style = (esriSimple3DMarkerStyle) this.cboStyle.SelectedIndex;
                this.refresh(e);
            }
        }

        public void ChangeUnit(double newunit)
        {
            this.m_CanDo = false;
            this.txtSize.Value = (decimal) ((((double) this.txtSize.Value) / this.m_unit) * newunit);
            this.txtDepth1.Value = (decimal) ((((double) this.txtDepth1.Value) / this.m_unit) * newunit);
            this.txtWidth.Value = (decimal) ((((double) this.txtWidth.Value) / this.m_unit) * newunit);
            this.m_unit = newunit;
            this.m_CanDo = true;
        }

        private void chkMaintainAspectRatio_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                (this.m_pSimpleMarker3DSymbol as IMarker3DPlacement).MaintainAspectRatio = this.chkMaintainAspectRatio.Checked;
                this.refresh(e);
            }
        }

        private void colorEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IColor pColor = (this.m_pSimpleMarker3DSymbol as IMarkerSymbol).Color;
                this.UpdateColorFromColorEdit(this.colorEdit1, pColor);
                (this.m_pSimpleMarker3DSymbol as IMarkerSymbol).Color = pColor;
                this.axSceneControl1.SceneGraph.RefreshViewers();
                this.refresh(e);
            }
        }

        public void DisplaySymbol()
        {
            IGraphicsLayer layer;
            if (this.axSceneControl1.SceneGraph.Scene.LayerCount == 0)
            {
                layer = new GraphicsLayer3DClass();
                this.axSceneControl1.SceneGraph.Scene.AddLayer(layer as ILayer, false);
            }
            else
            {
                layer = this.axSceneControl1.SceneGraph.Scene.get_Layer(0) as IGraphicsLayer;
            }
            try
            {
            }
            catch
            {
            }
            IGraphicsContainer3D containerd = layer as IGraphicsContainer3D;
            containerd.DeleteAllElements();
            if (this.m_pSimpleMarker3DSymbol != null)
            {
                IPoint point = new PointClass();
                IZAware aware = point as IZAware;
                aware.ZAware = true;
                point.X = 0.0;
                point.Y = 0.0;
                point.Z = 0.0;
                IElement element = new MarkerElementClass();
                IMarkerElement element2 = element as IMarkerElement;
                element2.Symbol = this.m_pSimpleMarker3DSymbol as IMarkerSymbol;
                element.Geometry = point;
                containerd.AddElement(element);
            }
            this.axSceneControl1.Navigate = true;
            this.axSceneControl1.SceneGraph.RefreshViewers();
        }

 private void GetRGB(uint rgb, out int r, out int g, out int b)
        {
            uint num = rgb & 16711680;
            b = (int) (num >> 16);
            num = rgb & 65280;
            g = (int) (num >> 8);
            num = rgb & 255;
            r = (int) num;
        }

        public void InitControl()
        {
            this.m_CanDo = false;
            this.txtSize.Value = (decimal) ((this.m_pSimpleMarker3DSymbol as IMarkerSymbol).Size * this.m_unit);
            this.txtDepth1.Value = (decimal) ((this.m_pSimpleMarker3DSymbol as IMarker3DPlacement).Depth * this.m_unit);
            this.txtWidth.Value = (decimal) ((this.m_pSimpleMarker3DSymbol as IMarker3DPlacement).Width * this.m_unit);
            this.cboStyle.SelectedIndex = (int) this.m_pSimpleMarker3DSymbol.Style;
            this.chkMaintainAspectRatio.Checked = (this.m_pSimpleMarker3DSymbol as IMarker3DPlacement).MaintainAspectRatio;
            this.SetColorEdit(this.colorEdit1, (this.m_pSimpleMarker3DSymbol as IMarkerSymbol).Color);
            this.DisplaySymbol();
            this.m_CanDo = true;
        }

 private void Marker3DEvent_Marker3DChanged(object sender)
        {
            if (sender != this)
            {
                this.DisplaySymbol();
            }
        }

        private void refresh(EventArgs e)
        {
            this.DisplaySymbol();
            if (this.ValueChanged != null)
            {
                this.ValueChanged(this, e);
            }
            Marker3DEvent.Marker3DChangeH(this);
        }

        private int RGB(int r, int g, int b)
        {
            uint num = 0;
            num |= (uint) b;
            num = num << 8;
            num |= (uint) g;
            num = num << 8;
            num |= (uint) r;
            return (int) num;
        }

        private void SetColorEdit(ColorEdit colorEdit, IColor pColor)
        {
            if (pColor.NullColor)
            {
                colorEdit.Color = Color.Empty;
            }
            else
            {
                int num;
                int num2;
                int num3;
                int rGB = pColor.RGB;
                this.GetRGB((uint) rGB, out num, out num2, out num3);
                colorEdit.Color = Color.FromArgb(pColor.Transparency, num, num2, num3);
            }
        }

        private void SimpleMarker3DSymbolCtrl_Load(object sender, EventArgs e)
        {
        }

        private void txtDepth1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if (this.txtDepth1.Value <= 0M)
                {
                    this.txtDepth1.ForeColor = Color.Red;
                }
                else
                {
                    this.txtDepth1.ForeColor = SystemColors.WindowText;
                    (this.m_pSimpleMarker3DSymbol as IMarker3DPlacement).Depth = ((double) this.txtDepth1.Value) / this.m_unit;
                    this.refresh(e);
                }
            }
        }

        private void txtSize_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if (this.txtSize.Value <= 0M)
                {
                    this.txtSize.ForeColor = Color.Red;
                }
                else
                {
                    this.txtSize.ForeColor = SystemColors.WindowText;
                    (this.m_pSimpleMarker3DSymbol as IMarkerSymbol).Size = ((double) this.txtSize.Value) / this.m_unit;
                    this.refresh(e);
                }
            }
        }

        private void txtWidth_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if (this.txtWidth.Value <= 0M)
                {
                    this.txtWidth.ForeColor = Color.Red;
                }
                else
                {
                    this.txtWidth.ForeColor = SystemColors.WindowText;
                    (this.m_pSimpleMarker3DSymbol as IMarker3DPlacement).Width = ((double) this.txtWidth.Value) / this.m_unit;
                    this.refresh(e);
                }
            }
        }

        private void UpdateColorFromColorEdit(ColorEdit colorEdit, IColor pColor)
        {
            pColor.NullColor = colorEdit.Color.IsEmpty;
            if (!colorEdit.Color.IsEmpty)
            {
                pColor.Transparency = colorEdit.Color.A;
                pColor.RGB = this.RGB(colorEdit.Color.R, colorEdit.Color.G, colorEdit.Color.B);
            }
        }
    }
}

