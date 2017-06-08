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
    internal class TextureFillSymbolCtrl : UserControl, CommonInterface
    {
        private AxSceneControl axSceneControl1;
        private SimpleButton btnSelectPicture;
        private ColorEdit colorEditForeColor;
        private ColorEdit colorEditTransColor;
        private Container components = null;
        private GroupBox groupBox2;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label6;
        private Label lblPathName;
        private bool m_CanDo = false;
        public IStyleGallery m_pSG;
        public ITextureFillSymbol m_pTextureFillSymbol = null;
        public double m_unit = 1.0;
        private SpinEdit txtAngle;
        private SpinEdit txtSize;

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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void GetRGB(uint rgb, out int r, out int g, out int b)
        {
            uint num = rgb & 0xff0000;
            b = (int) (num >> 0x10);
            num = rgb & 0xff00;
            g = (int) (num >> 8);
            num = rgb & 0xff;
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

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextureFillSymbolCtrl));
            this.groupBox2 = new GroupBox();
            this.axSceneControl1 = new AxSceneControl();
            this.btnSelectPicture = new SimpleButton();
            this.colorEditTransColor = new ColorEdit();
            this.colorEditForeColor = new ColorEdit();
            this.lblPathName = new Label();
            this.label6 = new Label();
            this.label4 = new Label();
            this.txtAngle = new SpinEdit();
            this.label2 = new Label();
            this.txtSize = new SpinEdit();
            this.label3 = new Label();
            this.groupBox2.SuspendLayout();
            this.axSceneControl1.BeginInit();
            this.colorEditTransColor.Properties.BeginInit();
            this.colorEditForeColor.Properties.BeginInit();
            this.txtAngle.Properties.BeginInit();
            this.txtSize.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox2.Controls.Add(this.axSceneControl1);
            this.groupBox2.Location = new System.Drawing.Point(0xe8, 0x40);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0xb0, 160);
            this.groupBox2.TabIndex = 90;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "3D预览";
            this.axSceneControl1.Location = new System.Drawing.Point(6, 20);
            this.axSceneControl1.Name = "axSceneControl1";
            this.axSceneControl1.OcxState = (AxHost.State) resources.GetObject("axSceneControl1.OcxState");
            this.axSceneControl1.Size = new Size(0x9a, 130);
            this.axSceneControl1.TabIndex = 0;
            this.btnSelectPicture.Location = new System.Drawing.Point(8, 0x10);
            this.btnSelectPicture.Name = "btnSelectPicture";
            this.btnSelectPicture.Size = new Size(0x40, 0x18);
            this.btnSelectPicture.TabIndex = 0x60;
            this.btnSelectPicture.Text = "纹理...";
            this.btnSelectPicture.Click += new EventHandler(this.btnSelectPicture_Click);
            this.colorEditTransColor.EditValue = Color.Empty;
            this.colorEditTransColor.Location = new System.Drawing.Point(0x40, 0x56);
            this.colorEditTransColor.Name = "colorEditTransColor";
            this.colorEditTransColor.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEditTransColor.Size = new Size(0x30, 0x15);
            this.colorEditTransColor.TabIndex = 0x5e;
            this.colorEditTransColor.EditValueChanged += new EventHandler(this.colorEditTransColor_EditValueChanged);
            this.colorEditForeColor.EditValue = Color.Empty;
            this.colorEditForeColor.Location = new System.Drawing.Point(0x40, 0x30);
            this.colorEditForeColor.Name = "colorEditForeColor";
            this.colorEditForeColor.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEditForeColor.Size = new Size(0x30, 0x15);
            this.colorEditForeColor.TabIndex = 0x5d;
            this.colorEditForeColor.EditValueChanged += new EventHandler(this.colorEditForeColor_EditValueChanged);
            this.lblPathName.Location = new System.Drawing.Point(0x58, 0x10);
            this.lblPathName.Name = "lblPathName";
            this.lblPathName.Size = new Size(280, 0x18);
            this.lblPathName.TabIndex = 0x5b;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 0x58);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x29, 12);
            this.label6.TabIndex = 0x62;
            this.label6.Text = "透明色";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0x10, 0x38);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x1d, 12);
            this.label4.TabIndex = 0x61;
            this.label4.Text = "颜色";
            int[] bits = new int[4];
            this.txtAngle.EditValue = new decimal(bits);
            this.txtAngle.Location = new System.Drawing.Point(0x38, 0x98);
            this.txtAngle.Name = "txtAngle";
            this.txtAngle.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtAngle.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.txtAngle.Properties.EditFormat.FormatType = FormatType.Numeric;
            bits = new int[4];
            bits[0] = 100;
            this.txtAngle.Properties.MaxValue = new decimal(bits);
            bits = new int[4];
            bits[0] = 100;
            bits[3] = -2147483648;
            this.txtAngle.Properties.MinValue = new decimal(bits);
            this.txtAngle.Size = new Size(0x48, 0x15);
            this.txtAngle.TabIndex = 0x66;
            this.txtAngle.EditValueChanged += new EventHandler(this.txtAngle_EditValueChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 160);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 12);
            this.label2.TabIndex = 0x65;
            this.label2.Text = "角度";
            bits = new int[4];
            this.txtSize.EditValue = new decimal(bits);
            this.txtSize.Location = new System.Drawing.Point(0x38, 120);
            this.txtSize.Name = "txtSize";
            this.txtSize.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtSize.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.txtSize.Properties.EditFormat.FormatType = FormatType.Numeric;
            bits = new int[4];
            bits[0] = 100;
            this.txtSize.Properties.MaxValue = new decimal(bits);
            this.txtSize.Size = new Size(0x40, 0x15);
            this.txtSize.TabIndex = 0x68;
            this.txtSize.EditValueChanged += new EventHandler(this.txtSize_EditValueChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0x10, 0x80);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x1d, 12);
            this.label3.TabIndex = 0x67;
            this.label3.Text = "大小";
            base.Controls.Add(this.txtSize);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.txtAngle);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.btnSelectPicture);
            base.Controls.Add(this.colorEditTransColor);
            base.Controls.Add(this.colorEditForeColor);
            base.Controls.Add(this.lblPathName);
            base.Controls.Add(this.groupBox2);
            base.Name = "TextureFillSymbolCtrl";
            base.Size = new Size(0x1a0, 0xf8);
            base.Load += new EventHandler(this.TextureLineSymbolCtrl_Load);
            this.groupBox2.ResumeLayout(false);
            this.axSceneControl1.EndInit();
            this.colorEditTransColor.Properties.EndInit();
            this.colorEditForeColor.Properties.EndInit();
            this.txtAngle.Properties.EndInit();
            this.txtSize.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
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

