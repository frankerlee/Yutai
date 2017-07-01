using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Controls.SymbolLib
{
    internal class BasicSymbolLayerBaseControl : UserControl
    {
        private List<Control> m_clist = new List<Control>();
        private esriGeometryType m_GeometryType = esriGeometryType.esriGeometryPoint;
        protected IBasicSymbol m_pBasicSymbol = null;

        public void AddControl(Control c)
        {
            for (int i = 0; i < this.m_clist.Count; i++)
            {
                Control control = this.m_clist[i];
                System.Drawing.Point location = control.Location;
                location.Y += c.Height;
                control.Location = location;
            }
            System.Drawing.Point point2 = new System.Drawing.Point(0, 0);
            if (base.Controls.Count > 0)
            {
                Control control2 = this.m_clist[0];
                point2 = control2.Location;
                point2.Y -= c.Height;
                this.m_clist.Insert(0, c);
            }
            else
            {
                this.m_clist.Add(c);
            }
            c.Location = point2;
            base.Controls.Add(c);
            base.AutoScrollMinSize = new Size(base.Width, this.m_clist[this.m_clist.Count - 1].Bottom);
        }

        public void AddControl(Control beforec, Control c)
        {
            int index = this.m_clist.IndexOf(beforec);
            for (int i = index; i < this.m_clist.Count; i++)
            {
                Control control = this.m_clist[i];
                System.Drawing.Point point = control.Location;
                point.Y += c.Height;
                control.Location = point;
            }
            System.Drawing.Point location = new System.Drawing.Point(0, 0);
            Control control2 = this.m_clist[index];
            location = control2.Location;
            location.Y -= c.Height;
            this.m_clist.Insert(index, c);
            c.Location = location;
            base.Controls.Add(c);
            if (c is GeometricEffectBaseControl)
            {
                (this.m_pBasicSymbol as IGeometricEffects).Insert(index,
                    (c as GeometricEffectBaseControl).GeometricEffect);
            }
            base.AutoScrollMinSize = new Size(base.Width, this.m_clist[this.m_clist.Count - 1].Bottom);
        }

        public Control CreateControlByGeometricEffect(IGeometricEffect pGE, BasicSymbolLayerBaseControl attControl)
        {
            if (pGE is IBasicMarkerSymbol)
            {
                return new GeometricEffectMarkerPage(attControl);
            }
            if (pGE is IBasicFillSymbol)
            {
                return new GeometricEffectFillPage(attControl);
            }
            if (pGE is IBasicLineSymbol)
            {
                return new GeometricEffectLinePage(attControl);
            }
            switch ((pGE as IGraphicAttributes).ClassName)
            {
                case "Cut curve":
                    return new GeometricCutCurverPage(attControl);

                case "Add control points":
                    return new GeometricEffectAddControlPointsPage(attControl);

                case "Buffer":
                    return new GeometricEffectBufferPage(attControl);

                case "Dashes":
                    return new GeometricEffectDashesPage(attControl);

                case "Donut":
                    return new GeometricEffectDonutPage(attControl);

                case "Enclosing Polygon":
                    return new GeometricEffectEnclosingPolygonPage(attControl);

                case "Fill":
                    return new GeometricEffectFillPage(attControl);

                case "Line":
                    return new GeometricEffectLinePage(attControl);

                case "Marker":
                    return new GeometricEffectMarkerPage(attControl);

                case "Offset curve":
                    return new GeometricEffectOffsetCurvePage(attControl);

                case "Radial from point":
                    return new GeometricEffectRadialfromPointPage(attControl);

                case "Reverse curver":
                    return new GeometricEffectReverseCurverPage(attControl);

                case "Simply":
                    return new GeometricEffectSimplyPage(attControl);

                case "Smooth curver":
                    return new GeometricSmoothCurverPage(attControl);
            }
            return null;
        }

        public Control CreateMarkerPlaceControl(IMarkerPlacement pGE, BasicSymbolLayerBaseControl attControl)
        {
            IGraphicAttributes attributes = new MarkerPlacementInsidePolygonClass();
            string className = attributes.ClassName;
            string str2 = (pGE as IGraphicAttributes).ClassName;
            switch ((pGE as IGraphicAttributes).ClassName)
            {
                case "Along line":
                    return new MarkerPlacementAlongLinePage(attControl);

                case "At extremities":
                    return new MarkerPlacementAtExtremitiesPage(attControl);

                case "Decorations":
                    return new MarkerPlacementDecorationPage(attControl);

                case "Inside polygon":
                    return new MarkerPlacementInsidePolygonPage(attControl);

                case "On line":
                    return new MarkerPlacementOnLinePage(attControl);

                case "On point":
                    return new MarkerPlacementOnPointPage(attControl);

                case "Polygon center":
                    return new MarkerPlacementPolygonCenterPage(attControl);

                case "Randomly along":
                    return new MarkerPlacementRandomAlongLinePage(attControl);

                case "Randomly inside polygon":
                    return new MarkerPlacementRandomInPolygonPage(attControl);

                case "Variable size":
                    return new MarkerPlacementVariableAlongLinePage(attControl);
            }
            return null;
        }

        public void RemoveControl(Control c)
        {
            int index = this.m_clist.IndexOf(c);
            if (index != -1)
            {
                for (int i = index + 1; i < this.m_clist.Count; i++)
                {
                    Control control = this.m_clist[i];
                    System.Drawing.Point location = control.Location;
                    location.Y -= c.Height;
                    control.Location = location;
                }
                if (c is GeometricEffectBaseControl)
                {
                    (this.m_pBasicSymbol as IGeometricEffects).Remove(index);
                }
                base.Controls.Remove(c);
                this.m_clist.Remove(c);
                if (this.m_clist.Count > 0)
                {
                    base.AutoScrollMinSize = new Size(base.Width, this.m_clist[this.m_clist.Count - 1].Bottom);
                }
                else
                {
                    base.AutoScrollMinSize = new Size(base.Width, base.Height);
                }
            }
        }

        public void ReplaceControl(Control oldc, Control newc)
        {
            int index = this.m_clist.IndexOf(oldc);
            if (index != -1)
            {
                for (int i = index + 1; i < this.m_clist.Count; i++)
                {
                    Control control = this.m_clist[i];
                    System.Drawing.Point point = control.Location;
                    point.Y = (point.Y - oldc.Height) + newc.Height;
                    control.Location = point;
                }
                System.Drawing.Point location = oldc.Location;
                newc.Location = location;
                if (newc is GeometricEffectBaseControl)
                {
                    (this.m_pBasicSymbol as IGeometricEffects).Insert(index,
                        (newc as GeometricEffectBaseControl).GeometricEffect);
                    (this.m_pBasicSymbol as IGeometricEffects).Remove(index + 1);
                }
                base.Controls.Remove(oldc);
                this.m_clist.Remove(oldc);
                base.Controls.Add(newc);
                if (this.m_clist.Count > 0)
                {
                    this.m_clist.Insert(index, newc);
                }
                else
                {
                    this.m_clist.Add(newc);
                }
                base.AutoScrollMinSize = new Size(base.Width, this.m_clist[this.m_clist.Count - 1].Bottom);
            }
        }

        public void ReplaceMarkerPlacementControl(Control oldc, Control newc)
        {
            int index = this.m_clist.IndexOf(oldc);
            if (index != -1)
            {
                for (int i = index + 1; i < this.m_clist.Count; i++)
                {
                    Control control = this.m_clist[i];
                    System.Drawing.Point point = control.Location;
                    point.Y = (point.Y - oldc.Height) + newc.Height;
                    control.Location = point;
                }
                System.Drawing.Point location = oldc.Location;
                newc.Location = location;
                base.Controls.Remove(oldc);
                this.m_clist.Remove(oldc);
                base.Controls.Add(newc);
                if (this.m_clist.Count > 0)
                {
                    this.m_clist.Insert(index, newc);
                }
                else
                {
                    this.m_clist.Add(newc);
                }
                base.AutoScrollMinSize = new Size(base.Width, this.m_clist[this.m_clist.Count - 1].Bottom);
            }
        }

        public IBasicSymbol BasicSymbol
        {
            get { return this.m_pBasicSymbol; }
            set { this.m_pBasicSymbol = value; }
        }

        public esriGeometryType GeometryType
        {
            get { return this.m_GeometryType; }
            set { this.m_GeometryType = value; }
        }
    }
}