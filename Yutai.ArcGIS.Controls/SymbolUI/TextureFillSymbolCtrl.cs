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
    internal partial class TextureFillSymbolCtrl : UserControl, CommonInterface
    {
        private bool m_CanDo = false;
        public IStyleGallery m_pSG;
        public ITextureFillSymbol m_pTextureFillSymbol = null;
        public double m_unit = 1.0;

        public event ValueChangedHandler ValueChanged;

        public TextureFillSymbolCtrl()
        {
            this.InitializeComponent();
        }

        private void btnSelectPicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                Filter = "JPEG文件交换格式(*.jpg;*jpeg)|*.jpg;*jpeg|移动网络图形(*.png)|*.png|Windows位图(*.bmp)|*.bmp|tiff(*.tif)|*.tif|GIF(*.gif)|*.gif|动画文件(*.cel)|*.cel|Targa文件(*.tga)|*.tga|SGI图像文件格式(*.rgb;*.rgba)|*.rgb;*.rgba|INT(*.int;*.inta)|*.int;*.inta"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.m_pTextureFillSymbol.CreateFillSymbolFromFile(dialog.FileName);
                this.lblPathName.Text = dialog.FileName;
            }
        }

        public void ChangeUnit(double newunit)
        {
            this.m_CanDo = false;
            this.txtSize.Value = (decimal) ((((double) this.txtSize.Value) / this.m_unit) * newunit);
            this.m_unit = newunit;
            this.m_CanDo = true;
        }

        private void colorEditForeColor_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IColor pColor = this.m_pTextureFillSymbol.Color;
                this.UpdateColorFromColorEdit(this.colorEditForeColor, pColor);
                this.m_pTextureFillSymbol.Color = pColor;
                this.refresh(e);
            }
        }

        private void colorEditTransColor_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IColor bitmapTransparencyColor = this.m_pTextureFillSymbol.BitmapTransparencyColor;
                if (bitmapTransparencyColor == null)
                {
                    bitmapTransparencyColor = new RgbColorClass();
                }
                this.UpdateColorFromColorEdit(this.colorEditTransColor, bitmapTransparencyColor);
                this.m_pTextureFillSymbol.BitmapTransparencyColor = bitmapTransparencyColor;
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
            if (this.m_pTextureFillSymbol != null)
            {
                IPoint inPoint = new PointClass();
                IPointCollection points = new PolygonClass();
                (points as IZAware).ZAware = true;
                object before = Missing.Value;
                IZAware aware = inPoint as IZAware;
                aware.ZAware = true;
                inPoint.X = 0.0;
                inPoint.Y = 0.0;
                inPoint.Z = 0.0;
                points.AddPoint(inPoint, ref before, ref before);
                inPoint = new PointClass();
                aware = inPoint as IZAware;
                aware.ZAware = true;
                inPoint.X = 1.0;
                inPoint.Y = 0.0;
                inPoint.Z = 0.0;
                points.AddPoint(inPoint, ref before, ref before);
                inPoint = new PointClass();
                aware = inPoint as IZAware;
                aware.ZAware = true;
                inPoint.X = 1.0;
                inPoint.Y = 1.0;
                inPoint.Z = 0.0;
                points.AddPoint(inPoint, ref before, ref before);
                inPoint = new PointClass();
                aware = inPoint as IZAware;
                aware.ZAware = true;
                inPoint.X = 0.0;
                inPoint.Y = 1.0;
                inPoint.Z = 0.0;
                points.AddPoint(inPoint, ref before, ref before);
                inPoint = new PointClass();
                aware = inPoint as IZAware;
                aware.ZAware = true;
                inPoint.X = 0.0;
                inPoint.Y = 0.0;
                inPoint.Z = 0.0;
                points.AddPoint(inPoint, ref before, ref before);
                IElement element = new PolygonElementClass();
                IFillShapeElement element2 = element as IFillShapeElement;
                element2.Symbol = this.m_pTextureFillSymbol;
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
            this.txtSize.Value = (decimal) (this.m_pTextureFillSymbol.Size * this.m_unit);
            this.txtAngle.Value = (decimal) this.m_pTextureFillSymbol.Angle;
            this.SetColorEdit(this.colorEditForeColor, this.m_pTextureFillSymbol.Color);
            if (this.m_pTextureFillSymbol.BitmapTransparencyColor != null)
            {
                this.SetColorEdit(this.colorEditTransColor, this.m_pTextureFillSymbol.BitmapTransparencyColor);
            }
            this.DisplaySymbol();
            this.m_CanDo = true;
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

        private void TextureLineSymbolCtrl_Load(object sender, EventArgs e)
        {
            this.InitControl();
        }

        private void txtAngle_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if (this.txtAngle.Value < 0M)
                {
                    this.txtAngle.ForeColor = Color.Red;
                }
                else if (this.txtAngle.Value == 0M)
                {
                    this.txtAngle.ForeColor = Color.Red;
                }
                else
                {
                    this.txtAngle.ForeColor = SystemColors.WindowText;
                    this.m_pTextureFillSymbol.Angle = (double) this.txtAngle.Value;
                    this.refresh(e);
                }
            }
        }

        private void txtSize_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if (this.txtSize.Value < 0M)
                {
                    this.txtSize.ForeColor = Color.Red;
                }
                else if (this.txtSize.Value == 0M)
                {
                    this.txtSize.ForeColor = Color.Red;
                }
                else
                {
                    this.txtSize.ForeColor = SystemColors.WindowText;
                    this.m_pTextureFillSymbol.Size = ((double) this.txtSize.Value) / this.m_unit;
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

