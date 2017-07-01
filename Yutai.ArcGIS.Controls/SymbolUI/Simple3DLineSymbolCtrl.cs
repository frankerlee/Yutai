using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
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
    internal partial class Simple3DLineSymbolCtrl : UserControl, CommonInterface
    {
        private bool m_CanDo = false;
        public ISimpleLine3DSymbol m_pSimpleLine3DSymbol = null;
        public double m_unit = 1.0;

        public event ValueChangedHandler ValueChanged;

        public Simple3DLineSymbolCtrl()
        {
            this.InitializeComponent();
        }

        private void cboStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_pSimpleLine3DSymbol.Style = (esriSimple3DLineStyle) this.cboStyle.SelectedIndex;
                this.refresh(e);
            }
        }

        public void ChangeUnit(double newunit)
        {
            this.m_CanDo = false;
            this.numericUpDownWidth.Value = (decimal) ((((double) this.numericUpDownWidth.Value)/this.m_unit)*newunit);
            this.m_unit = newunit;
            this.m_CanDo = true;
        }

        private void colorEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IColor pColor = (this.m_pSimpleLine3DSymbol as ILineSymbol).Color;
                this.UpdateColorFromColorEdit(this.colorEdit1, pColor);
                (this.m_pSimpleLine3DSymbol as ILineSymbol).Color = pColor;
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
            IGraphicsContainer3D containerd = layer as IGraphicsContainer3D;
            containerd.DeleteAllElements();
            if (this.m_pSimpleLine3DSymbol != null)
            {
                IPoint inPoint = new PointClass();
                IPointCollection points = new PolylineClass();
                (points as IZAware).ZAware = true;
                IZAware aware = inPoint as IZAware;
                aware.ZAware = true;
                inPoint.X = 0.0;
                inPoint.Y = 0.0;
                inPoint.Z = 0.0;
                object before = Missing.Value;
                points.AddPoint(inPoint, ref before, ref before);
                inPoint = new PointClass();
                aware = inPoint as IZAware;
                aware.ZAware = true;
                inPoint.X = 1.0;
                inPoint.Y = 0.0;
                inPoint.Z = 0.0;
                points.AddPoint(inPoint, ref before, ref before);
                IElement element = new LineElementClass();
                ILineElement element2 = element as ILineElement;
                element2.Symbol = this.m_pSimpleLine3DSymbol as ILineSymbol;
                element.Geometry = points as IGeometry;
                containerd.AddElement(element);
            }
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
            this.numericUpDownWidth.Value = (decimal) ((this.m_pSimpleLine3DSymbol as ILineSymbol).Width*this.m_unit);
            this.cboStyle.SelectedIndex = (int) this.m_pSimpleLine3DSymbol.Style;
            this.SetColorEdit(this.colorEdit1, (this.m_pSimpleLine3DSymbol as ILineSymbol).Color);
            this.DisplaySymbol();
            this.m_CanDo = true;
        }

        private void numericUpDownWidth_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if (this.numericUpDownWidth.Value < 0M)
                {
                    this.numericUpDownWidth.ForeColor = Color.Red;
                }
                else if (this.numericUpDownWidth.Value == 0M)
                {
                    this.numericUpDownWidth.ForeColor = Color.Red;
                }
                else
                {
                    this.numericUpDownWidth.ForeColor = SystemColors.WindowText;
                    (this.m_pSimpleLine3DSymbol as ILineSymbol).Width = ((double) this.numericUpDownWidth.Value)/
                                                                        this.m_unit;
                    this.refresh(e);
                }
            }
        }

        private void refresh(EventArgs e)
        {
            this.DisplaySymbol();
            if (this.ValueChanged != null)
            {
                this.ValueChanged(this, e);
            }
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

        private void Simple3DLineSymbolCtrl_Load(object sender, EventArgs e)
        {
            this.InitControl();
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