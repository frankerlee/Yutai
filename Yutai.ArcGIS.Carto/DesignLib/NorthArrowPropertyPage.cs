using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class NorthArrowPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private SimpleButton btnNorthArrorSelector;
        private SimpleButton btnNorthMarkerSymbolSelector;
        private ColorEdit colorEdit1;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private IMapSurroundFrame imapSurroundFrame_0 = null;
        private INorthArrow inorthArrow_0 = null;
        private IStyleGallery istyleGallery_0 = ApplicationBase.StyleGallery;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private string string_0 = "指北针";
        private SymbolItem symbolItem1;
        private TextEdit txtAngle;
        private SpinEdit txtCalibrationAngle;
        private SpinEdit txtSize;

        public event OnValueChangeEventHandler OnValueChange;

        public NorthArrowPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.imapSurroundFrame_0.MapSurround = (this.inorthArrow_0 as IClone).Clone() as IMapSurround;
                this.bool_1 = false;
            }
        }

        private void btnNorthArrorSelector_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.istyleGallery_0);
                    selector.SetSymbol(this.inorthArrow_0);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        this.inorthArrow_0 = selector.GetSymbol() as INorthArrow;
                        this.bool_0 = false;
                        this.method_1();
                        this.bool_0 = true;
                        this.method_0();
                    }
                }
            }
            catch
            {
            }
        }

        private void btnNorthMarkerSymbolSelector_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.istyleGallery_0);
                    selector.SetSymbol((this.inorthArrow_0 as IMarkerNorthArrow).MarkerSymbol);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        (this.inorthArrow_0 as IMarkerNorthArrow).MarkerSymbol = selector.GetSymbol() as IMarkerSymbol;
                        this.bool_0 = false;
                        this.method_1();
                        this.bool_0 = true;
                        this.method_0();
                    }
                }
            }
            catch
            {
            }
        }

        public void Cancel()
        {
        }

        private void colorEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IColor color = this.inorthArrow_0.Color;
                this.method_2(this.colorEdit1, color);
                this.inorthArrow_0.Color = color;
                this.method_0();
            }
        }

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_2);
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.btnNorthArrorSelector = new SimpleButton();
            this.symbolItem1 = new SymbolItem();
            this.groupBox2 = new GroupBox();
            this.colorEdit1 = new ColorEdit();
            this.label4 = new Label();
            this.txtAngle = new TextEdit();
            this.label3 = new Label();
            this.txtCalibrationAngle = new SpinEdit();
            this.label2 = new Label();
            this.txtSize = new SpinEdit();
            this.label1 = new Label();
            this.groupBox3 = new GroupBox();
            this.btnNorthMarkerSymbolSelector = new SimpleButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.colorEdit1.Properties.BeginInit();
            this.txtAngle.Properties.BeginInit();
            this.txtCalibrationAngle.Properties.BeginInit();
            this.txtSize.Properties.BeginInit();
            this.groupBox3.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.btnNorthArrorSelector);
            this.groupBox1.Controls.Add(this.symbolItem1);
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(120, 0xc0);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "预览";
            this.btnNorthArrorSelector.Location = new Point(0x18, 160);
            this.btnNorthArrorSelector.Name = "btnNorthArrorSelector";
            this.btnNorthArrorSelector.Size = new Size(0x48, 0x18);
            this.btnNorthArrorSelector.TabIndex = 2;
            this.btnNorthArrorSelector.Text = "指北针";
            this.btnNorthArrorSelector.Click += new EventHandler(this.btnNorthArrorSelector_Click);
            this.symbolItem1.BackColor = SystemColors.ControlLight;
            this.symbolItem1.Location = new Point(0x10, 0x18);
            this.symbolItem1.Name = "symbolItem1";
            this.symbolItem1.Size = new Size(0x60, 0x80);
            this.symbolItem1.Symbol = null;
            this.symbolItem1.TabIndex = 1;
            this.groupBox2.Controls.Add(this.colorEdit1);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtAngle);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtCalibrationAngle);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtSize);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new Point(0x88, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0xa8, 120);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "常规";
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new Point(0x58, 40);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(0x30, 0x15);
            this.colorEdit1.TabIndex = 7;
            this.colorEdit1.EditValueChanged += new EventHandler(this.colorEdit1_EditValueChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x58, 0x10);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x23, 0x11);
            this.label4.TabIndex = 6;
            this.label4.Text = "颜色:";
            this.txtAngle.EditValue = "";
            this.txtAngle.Location = new Point(0x58, 0x58);
            this.txtAngle.Name = "txtAngle";
            this.txtAngle.Size = new Size(0x30, 0x15);
            this.txtAngle.TabIndex = 5;
            this.txtAngle.EditValueChanged += new EventHandler(this.txtAngle_EditValueChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x58, 0x40);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x23, 0x11);
            this.label3.TabIndex = 4;
            this.label3.Text = "角度:";
            this.txtCalibrationAngle.EditValue = 0;
            this.txtCalibrationAngle.Location = new Point(8, 0x58);
            this.txtCalibrationAngle.Name = "txtCalibrationAngle";
            this.txtCalibrationAngle.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtCalibrationAngle.Properties.UseCtrlIncrement = false;
            this.txtCalibrationAngle.Size = new Size(0x38, 0x15);
            this.txtCalibrationAngle.TabIndex = 3;
            this.txtCalibrationAngle.EditValueChanged += new EventHandler(this.txtCalibrationAngle_EditValueChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x10, 0x40);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x30, 0x11);
            this.label2.TabIndex = 2;
            this.label2.Text = "校准角:";
            this.txtSize.EditValue = 0;
            this.txtSize.Location = new Point(0x10, 40);
            this.txtSize.Name = "txtSize";
            this.txtSize.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtSize.Properties.UseCtrlIncrement = false;
            this.txtSize.Size = new Size(0x38, 0x15);
            this.txtSize.TabIndex = 1;
            this.txtSize.EditValueChanged += new EventHandler(this.txtSize_EditValueChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 0x18);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "大小:";
            this.groupBox3.Controls.Add(this.btnNorthMarkerSymbolSelector);
            this.groupBox3.Location = new Point(0x88, 0x80);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0xa8, 0x48);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "点符号";
            this.btnNorthMarkerSymbolSelector.Location = new Point(0x30, 40);
            this.btnNorthMarkerSymbolSelector.Name = "btnNorthMarkerSymbolSelector";
            this.btnNorthMarkerSymbolSelector.Size = new Size(0x48, 0x18);
            this.btnNorthMarkerSymbolSelector.TabIndex = 0;
            this.btnNorthMarkerSymbolSelector.Text = "点符号";
            this.btnNorthMarkerSymbolSelector.Click += new EventHandler(this.btnNorthMarkerSymbolSelector_Click);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "NorthArrowPropertyPage";
            base.Size = new Size(320, 0xd8);
            base.Load += new EventHandler(this.NorthArrowPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.colorEdit1.Properties.EndInit();
            this.txtAngle.Properties.EndInit();
            this.txtCalibrationAngle.Properties.EndInit();
            this.txtSize.Properties.EndInit();
            this.groupBox3.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

        private void method_0()
        {
            this.bool_1 = true;
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
            }
            this.symbolItem1.Invalidate();
        }

        private void method_1()
        {
            if (this.inorthArrow_0 != null)
            {
                this.symbolItem1.Symbol = this.inorthArrow_0;
                this.txtAngle.Text = this.inorthArrow_0.Angle.ToString();
                this.txtCalibrationAngle.Text = this.inorthArrow_0.CalibrationAngle.ToString();
                this.txtSize.Text = this.inorthArrow_0.Size.ToString();
                this.method_3(this.colorEdit1, this.inorthArrow_0.Color);
            }
        }

        private void method_2(ColorEdit colorEdit_0, IColor icolor_0)
        {
            icolor_0.NullColor = colorEdit_0.Color.IsEmpty;
            if (!colorEdit_0.Color.IsEmpty)
            {
                icolor_0.Transparency = colorEdit_0.Color.A;
                icolor_0.RGB = ColorManage.EsriRGB(colorEdit_0.Color.R, colorEdit_0.Color.G, colorEdit_0.Color.B);
            }
        }

        private void method_3(ColorEdit colorEdit_0, IColor icolor_0)
        {
            if (icolor_0.NullColor)
            {
                colorEdit_0.Color = Color.Empty;
            }
            else
            {
                int num2;
                int num3;
                int num4;
                ColorManage.GetEsriRGB((uint) icolor_0.RGB, out num2, out num3, out num4);
                colorEdit_0.Color = Color.FromArgb(icolor_0.Transparency, num2, num3, num4);
            }
        }

        private void NorthArrowPropertyPage_Load(object sender, EventArgs e)
        {
            this.method_1();
            this.bool_0 = true;
        }

        public void ResetControl()
        {
            this.bool_0 = false;
            this.method_1();
            this.bool_0 = true;
        }

        public void SetObjects(object object_0)
        {
            this.imapSurroundFrame_0 = object_0 as IMapSurroundFrame;
            if (this.imapSurroundFrame_0 != null)
            {
                this.inorthArrow_0 = (this.imapSurroundFrame_0.MapSurround as IClone).Clone() as INorthArrow;
            }
        }

        private void txtAngle_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void txtCalibrationAngle_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    this.inorthArrow_0.CalibrationAngle = double.Parse(this.txtCalibrationAngle.Text);
                    this.method_0();
                }
                catch
                {
                }
            }
        }

        private void txtSize_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    this.inorthArrow_0.Size = double.Parse(this.txtSize.Text);
                    this.method_0();
                }
                catch
                {
                }
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_1;
            }
        }

        int IPropertyPage.Height
        {
            get
            {
                return base.Height;
            }
        }

        int IPropertyPage.Width
        {
            get
            {
                return base.Width;
            }
        }

        public string Title
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }
    }
}

