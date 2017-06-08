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
   
    internal class TextureLineSymbolCtrl : UserControl, CommonInterface
    {
        private AxSceneControl axSceneControl1;
        private SimpleButton btnSelectPicture;
        private CheckEdit checkEdit1;
        private ColorEdit colorEditForeColor;
        private ColorEdit colorEditTransColor;
        private Container components = null;
        private GroupBox groupBox2;
        private Label label1;
        private Label label4;
        private Label label6;
        private Label lblPathName;
        private bool m_CanDo = false;
        public ITextureLineSymbol m_pTextureLineSymbol = null;
        public double m_unit = 1.0;
        private SpinEdit numericUpDownWidth;

        public event ValueChangedHandler ValueChanged;

        public TextureLineSymbolCtrl()
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
                this.m_pTextureLineSymbol.CreateLineSymbolFromFile(dialog.FileName);
                this.lblPathName.Text = dialog.FileName;
            }
        }

        public void ChangeUnit(double newunit)
        {
            this.m_CanDo = false;
            this.numericUpDownWidth.Value = (decimal) ((((double) this.numericUpDownWidth.Value) / this.m_unit) * newunit);
            this.m_unit = newunit;
            this.m_CanDo = true;
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_pTextureLineSymbol.VerticalOrientation = this.checkEdit1.Checked;
                this.refresh(e);
            }
        }

        private void colorEditForeColor_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IColor pColor = this.m_pTextureLineSymbol.Color;
                this.UpdateColorFromColorEdit(this.colorEditForeColor, pColor);
                this.m_pTextureLineSymbol.Color = pColor;
                this.refresh(e);
            }
        }

        private void colorEditTransColor_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IColor bitmapTransparencyColor = this.m_pTextureLineSymbol.BitmapTransparencyColor;
                if (bitmapTransparencyColor == null)
                {
                    bitmapTransparencyColor = new RgbColorClass();
                }
                this.UpdateColorFromColorEdit(this.colorEditTransColor, bitmapTransparencyColor);
                this.m_pTextureLineSymbol.BitmapTransparencyColor = bitmapTransparencyColor;
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
            if (this.m_pTextureLineSymbol != null)
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
                element2.Symbol = this.m_pTextureLineSymbol;
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
            this.numericUpDownWidth.Value = (decimal) (this.m_pTextureLineSymbol.Width * this.m_unit);
            this.SetColorEdit(this.colorEditForeColor, this.m_pTextureLineSymbol.Color);
            if (this.m_pTextureLineSymbol.BitmapTransparencyColor != null)
            {
                this.SetColorEdit(this.colorEditTransColor, this.m_pTextureLineSymbol.BitmapTransparencyColor);
            }
            this.checkEdit1.Checked = this.m_pTextureLineSymbol.VerticalOrientation;
            this.DisplaySymbol();
            this.m_CanDo = true;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextureLineSymbolCtrl));
            this.groupBox2 = new GroupBox();
            this.axSceneControl1 = new AxSceneControl();
            this.btnSelectPicture = new SimpleButton();
            this.numericUpDownWidth = new SpinEdit();
            this.colorEditTransColor = new ColorEdit();
            this.colorEditForeColor = new ColorEdit();
            this.label1 = new Label();
            this.lblPathName = new Label();
            this.label6 = new Label();
            this.label4 = new Label();
            this.checkEdit1 = new CheckEdit();
            this.groupBox2.SuspendLayout();
            this.axSceneControl1.BeginInit();
            this.numericUpDownWidth.Properties.BeginInit();
            this.colorEditTransColor.Properties.BeginInit();
            this.colorEditForeColor.Properties.BeginInit();
            this.checkEdit1.Properties.BeginInit();
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
            this.axSceneControl1.Size = new Size(0xa4, 130);
            this.axSceneControl1.TabIndex = 0;
            this.btnSelectPicture.Location = new System.Drawing.Point(8, 0x10);
            this.btnSelectPicture.Name = "btnSelectPicture";
            this.btnSelectPicture.Size = new Size(0x40, 0x18);
            this.btnSelectPicture.TabIndex = 0x60;
            this.btnSelectPicture.Text = "纹理...";
            this.btnSelectPicture.Click += new EventHandler(this.btnSelectPicture_Click);
            int[] bits = new int[4];
            this.numericUpDownWidth.EditValue = new decimal(bits);
            this.numericUpDownWidth.Location = new System.Drawing.Point(0x40, 0x88);
            this.numericUpDownWidth.Name = "numericUpDownWidth";
            this.numericUpDownWidth.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.numericUpDownWidth.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.numericUpDownWidth.Properties.EditFormat.FormatType = FormatType.Numeric;
            bits = new int[4];
            bits[0] = 100;
            this.numericUpDownWidth.Properties.MaxValue = new decimal(bits);
            this.numericUpDownWidth.Size = new Size(0x40, 0x15);
            this.numericUpDownWidth.TabIndex = 0x5f;
            this.numericUpDownWidth.EditValueChanged += new EventHandler(this.numericUpDownWidth_EditValueChanged);
            this.colorEditTransColor.EditValue = Color.Empty;
            this.colorEditTransColor.Location = new System.Drawing.Point(0x40, 0x68);
            this.colorEditTransColor.Name = "colorEditTransColor";
            this.colorEditTransColor.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEditTransColor.Size = new Size(0x30, 0x15);
            this.colorEditTransColor.TabIndex = 0x5e;
            this.colorEditTransColor.EditValueChanged += new EventHandler(this.colorEditTransColor_EditValueChanged);
            this.colorEditForeColor.EditValue = Color.Empty;
            this.colorEditForeColor.Location = new System.Drawing.Point(0x40, 0x40);
            this.colorEditForeColor.Name = "colorEditForeColor";
            this.colorEditForeColor.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEditForeColor.Size = new Size(0x30, 0x15);
            this.colorEditForeColor.TabIndex = 0x5d;
            this.colorEditForeColor.EditValueChanged += new EventHandler(this.colorEditForeColor_EditValueChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 0x90);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 0x5c;
            this.label1.Text = "宽度";
            this.lblPathName.Location = new System.Drawing.Point(0x58, 0x10);
            this.lblPathName.Name = "lblPathName";
            this.lblPathName.Size = new Size(280, 0x18);
            this.lblPathName.TabIndex = 0x5b;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 0x68);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x29, 12);
            this.label6.TabIndex = 0x62;
            this.label6.Text = "透明色";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0x10, 0x40);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x1d, 12);
            this.label4.TabIndex = 0x61;
            this.label4.Text = "颜色";
            this.checkEdit1.Location = new System.Drawing.Point(0x90, 0x90);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "垂直方向";
            this.checkEdit1.Size = new Size(0x48, 0x13);
            this.checkEdit1.TabIndex = 0x63;
            this.checkEdit1.CheckedChanged += new EventHandler(this.checkEdit1_CheckedChanged);
            base.Controls.Add(this.checkEdit1);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.btnSelectPicture);
            base.Controls.Add(this.numericUpDownWidth);
            base.Controls.Add(this.colorEditTransColor);
            base.Controls.Add(this.colorEditForeColor);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.lblPathName);
            base.Controls.Add(this.groupBox2);
            base.Name = "TextureLineSymbolCtrl";
            base.Size = new Size(0x1b0, 280);
            base.Load += new EventHandler(this.TextureLineSymbolCtrl_Load);
            this.groupBox2.ResumeLayout(false);
            this.axSceneControl1.EndInit();
            this.numericUpDownWidth.Properties.EndInit();
            this.colorEditTransColor.Properties.EndInit();
            this.colorEditForeColor.Properties.EndInit();
            this.checkEdit1.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
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
                    this.m_pTextureLineSymbol.Width = ((double) this.numericUpDownWidth.Value) / this.m_unit;
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

        private void TextureLineSymbolCtrl_Load(object sender, EventArgs e)
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

