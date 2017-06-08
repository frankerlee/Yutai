using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    public class NorthArrowPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private SimpleButton btnNorthArrorSelector;
        private SimpleButton btnNorthMarkerSymbolSelector;
        private ColorEdit colorEdit1;
        private Container components = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private INorthArrow m_pNorthArrow = null;
        private INorthArrow m_pOldNorthArrow = null;
        private IStyleGallery m_pSG;
        private string m_Title = "指北针";
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
            if (this.m_IsPageDirty)
            {
                (this.m_pOldNorthArrow as IClone).Assign(this.m_pNorthArrow as IClone);
                this.m_IsPageDirty = false;
            }
        }

        private void btnNorthArrorSelector_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.m_pSG);
                    selector.SetSymbol(this.m_pNorthArrow);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        this.m_pNorthArrow = selector.GetSymbol() as INorthArrow;
                        this.m_CanDo = false;
                        this.Init();
                        this.m_CanDo = true;
                        this.ValueChanged();
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
                    selector.SetStyleGallery(this.m_pSG);
                    selector.SetSymbol((this.m_pNorthArrow as IMarkerNorthArrow).MarkerSymbol);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        (this.m_pNorthArrow as IMarkerNorthArrow).MarkerSymbol = selector.GetSymbol() as IMarkerSymbol;
                        this.m_CanDo = false;
                        this.Init();
                        this.m_CanDo = true;
                        this.ValueChanged();
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
            if (this.m_CanDo)
            {
                IColor pColor = this.m_pNorthArrow.Color;
                this.UpdateColorFromColorEdit(this.colorEdit1, pColor);
                this.m_pNorthArrow.Color = pColor;
                this.ValueChanged();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public static int EsriRGB(int r, int g, int b)
        {
            uint num = 0;
            num |= (uint) b;
            num = num << 8;
            num |= (uint) g;
            num = num << 8;
            num |= (uint) r;
            return (int) num;
        }

        public static void GetEsriRGB(uint rgb, out int r, out int g, out int b)
        {
            uint num = rgb & 0xff0000;
            b = (int) (num >> 0x10);
            num = rgb & 0xff00;
            g = (int) (num >> 8);
            num = rgb & 0xff;
            r = (int) num;
        }

        public void Hide()
        {
        }

        private void Init()
        {
            if (this.m_pNorthArrow != null)
            {
                this.symbolItem1.Symbol = this.m_pNorthArrow;
                this.txtAngle.Text = this.m_pNorthArrow.Angle.ToString();
                this.txtCalibrationAngle.Text = this.m_pNorthArrow.CalibrationAngle.ToString();
                this.txtSize.Text = this.m_pNorthArrow.Size.ToString();
                this.SetColorEdit(this.colorEdit1, this.m_pNorthArrow.Color);
            }
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

        private void NorthArrowPropertyPage_Load(object sender, EventArgs e)
        {
            this.Init();
            this.m_CanDo = true;
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
                GetEsriRGB((uint) pColor.RGB, out num, out num2, out num3);
                colorEdit.Color = Color.FromArgb(pColor.Transparency, num, num2, num3);
            }
        }

        public void SetObjects(object @object)
        {
            this.m_pOldNorthArrow = @object as INorthArrow;
            if (this.m_pOldNorthArrow != null)
            {
                this.m_pNorthArrow = (this.m_pOldNorthArrow as IClone).Clone() as INorthArrow;
            }
        }

        private void txtAngle_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void txtCalibrationAngle_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                try
                {
                    this.m_pNorthArrow.CalibrationAngle = double.Parse(this.txtCalibrationAngle.Text);
                    this.ValueChanged();
                }
                catch
                {
                }
            }
        }

        private void txtSize_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                try
                {
                    this.m_pNorthArrow.Size = double.Parse(this.txtSize.Text);
                    this.ValueChanged();
                }
                catch
                {
                }
            }
        }

        private void UpdateColorFromColorEdit(ColorEdit colorEdit, IColor pColor)
        {
            pColor.NullColor = colorEdit.Color.IsEmpty;
            if (!colorEdit.Color.IsEmpty)
            {
                pColor.Transparency = colorEdit.Color.A;
                pColor.RGB = EsriRGB(colorEdit.Color.R, colorEdit.Color.G, colorEdit.Color.B);
            }
        }

        private void ValueChanged()
        {
            this.m_IsPageDirty = true;
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
            }
            this.symbolItem1.Invalidate();
        }

        public bool IsPageDirty
        {
            get
            {
                return this.m_IsPageDirty;
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
                return this.m_Title;
            }
            set
            {
                this.m_Title = value;
            }
        }
    }
}

