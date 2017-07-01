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
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.SymbolUI;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    internal partial class Marker3DPlacementCtrl : UserControl, CommonInterface
    {
        private bool m_CanDo = false;
        public IMarker3DPlacement m_pMarker3DSymbol = null;
        public double m_unit = 1.0;

        public event ValueChangedHandler ValueChanged;

        public Marker3DPlacementCtrl()
        {
            this.InitializeComponent();
            Marker3DEvent.Marker3DChanged += new Marker3DEvent.Marker3DChangedHandler(this.Marker3DEvent_Marker3DChanged);
        }

        public void ChangeUnit(double newunit)
        {
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
            IGraphicsContainer3D containerd = layer as IGraphicsContainer3D;
            containerd.DeleteAllElements();
            if (this.m_pMarker3DSymbol != null)
            {
                IPoint point = new PointClass();
                IZAware aware = point as IZAware;
                aware.ZAware = true;
                point.X = 0.0;
                point.Y = 0.0;
                point.Z = 0.0;
                IElement element = new MarkerElementClass();
                IMarkerElement element2 = element as IMarkerElement;
                element2.Symbol = this.m_pMarker3DSymbol;
                element.Geometry = point;
                containerd.AddElement(element);
            }
            this.axSceneControl1.SceneGraph.RefreshViewers();
        }

        public void InitControl()
        {
            double num;
            double num2;
            double num3;
            this.m_CanDo = false;
            this.txtOffsetX.Value = (decimal) (this.m_pMarker3DSymbol.XOffset*this.m_unit);
            this.txtOffsetY.Value = (decimal) (this.m_pMarker3DSymbol.YOffset*this.m_unit);
            this.txtOffsetZ.Value = (decimal) (this.m_pMarker3DSymbol.ZOffset*this.m_unit);
            this.m_pMarker3DSymbol.QueryRotationAngles(out num, out num2, out num3);
            this.txtRotateX.Value = (decimal) num;
            this.txtRotateY.Value = (decimal) num2;
            this.txtRotateZ.Value = (decimal) num3;
            IVector3D normalizedOriginOffset = this.m_pMarker3DSymbol.NormalizedOriginOffset;
            this.txtDX.Value = (decimal) normalizedOriginOffset.XComponent;
            this.txtDY.Value = (decimal) normalizedOriginOffset.YComponent;
            this.txtDZ.Value = (decimal) normalizedOriginOffset.ZComponent;
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

        private void Marker3DPlacementCtrl_Load(object sender, EventArgs e)
        {
            this.InitControl();
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

        private void txtDX_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IVector3D normalizedOriginOffset = this.m_pMarker3DSymbol.NormalizedOriginOffset;
                normalizedOriginOffset.XComponent = (double) this.txtDX.Value;
                this.m_pMarker3DSymbol.NormalizedOriginOffset = normalizedOriginOffset;
                this.refresh(e);
            }
        }

        private void txtDY_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IVector3D normalizedOriginOffset = this.m_pMarker3DSymbol.NormalizedOriginOffset;
                normalizedOriginOffset.YComponent = (double) this.txtDY.Value;
                this.m_pMarker3DSymbol.NormalizedOriginOffset = normalizedOriginOffset;
                this.refresh(e);
            }
        }

        private void txtDZ_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IVector3D normalizedOriginOffset = this.m_pMarker3DSymbol.NormalizedOriginOffset;
                normalizedOriginOffset.ZComponent = (double) this.txtDZ.Value;
                this.m_pMarker3DSymbol.NormalizedOriginOffset = normalizedOriginOffset;
                this.refresh(e);
            }
        }

        private void txtOffsetX_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_pMarker3DSymbol.XOffset = ((double) this.txtOffsetX.Value)/this.m_unit;
                this.refresh(e);
            }
        }

        private void txtOffsetY_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_pMarker3DSymbol.YOffset = ((double) this.txtOffsetY.Value)/this.m_unit;
                this.refresh(e);
            }
        }

        private void txtOffsetZ_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_pMarker3DSymbol.ZOffset = ((double) this.txtOffsetZ.Value)/this.m_unit;
                this.refresh(e);
            }
        }

        private void txtRotateX_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                double num;
                double num2;
                double num3;
                this.m_pMarker3DSymbol.QueryRotationAngles(out num, out num2, out num3);
                num = (double) this.txtRotateX.Value;
                this.m_pMarker3DSymbol.SetRotationAngles(num, num2, num3);
                this.refresh(e);
            }
        }

        private void txtRotateY_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                double num;
                double num2;
                double num3;
                this.m_pMarker3DSymbol.QueryRotationAngles(out num, out num2, out num3);
                num2 = (double) this.txtRotateY.Value;
                this.m_pMarker3DSymbol.SetRotationAngles(num, num2, num3);
                this.refresh(e);
            }
        }

        private void txtRotateZ_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                double num;
                double num2;
                double num3;
                this.m_pMarker3DSymbol.QueryRotationAngles(out num, out num2, out num3);
                num3 = (double) this.txtRotateZ.Value;
                this.m_pMarker3DSymbol.SetRotationAngles(num, num2, num3);
                this.refresh(e);
            }
        }
    }
}