using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.Symbol;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;

namespace Yutai.ArcGIS.Carto.UI
{
    public class FeatureSelectionSetCtrl : UserControl, ILayerAndStandaloneTablePropertyPage
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private StyleButton btnSymbol;
        private ColorEdit colorEdit1;
        private Container container_0 = null;
        private IFeatureSelection ifeatureSelection_0 = null;
        private Label label1;
        public IStyleGallery m_pSG = null;
        private RadioGroup radioGroup1;

        public FeatureSelectionSetCtrl()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            if (this.bool_0)
            {
                this.bool_0 = false;
                if (this.radioGroup1.SelectedIndex == 0)
                {
                    this.ifeatureSelection_0.SetSelectionSymbol = false;
                    this.ifeatureSelection_0.SelectionColor = null;
                }
                else if (this.radioGroup1.SelectedIndex == 1)
                {
                    this.ifeatureSelection_0.SetSelectionSymbol = true;
                    this.ifeatureSelection_0.SelectionColor = null;
                    this.ifeatureSelection_0.SelectionSymbol = this.btnSymbol.Style as ISymbol;
                }
                else if (this.radioGroup1.SelectedIndex == 2)
                {
                    this.ifeatureSelection_0.SetSelectionSymbol = false;
                    IColor color = new RgbColorClass();
                    this.method_2(this.colorEdit1, color);
                    this.ifeatureSelection_0.SelectionColor = color;
                }
            }
            return true;
        }

        private void btnSymbol_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.m_pSG);
                    selector.SetSymbol(this.btnSymbol.Style);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        this.btnSymbol.Style = selector.GetSymbol();
                        this.bool_0 = true;
                    }
                }
            }
            catch
            {
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

        private void FeatureSelectionSetCtrl_Load(object sender, EventArgs e)
        {
            if (!this.ifeatureSelection_0.SetSelectionSymbol)
            {
                if (this.ifeatureSelection_0.SelectionColor == null)
                {
                    this.radioGroup1.SelectedIndex = 0;
                }
                else
                {
                    this.radioGroup1.SelectedIndex = 2;
                }
            }
            else
            {
                this.radioGroup1.SelectedIndex = 1;
            }
            if (this.ifeatureSelection_0.SelectionSymbol == null)
            {
                ILineSymbol symbol = new SimpleLineSymbolClass {
                    Width = 2.0,
                    Color = ColorManage.CreatColor(0, 0xff, 0xff)
                };
                this.btnSymbol.Style = symbol;
            }
            else
            {
                this.btnSymbol.Style = this.ifeatureSelection_0.SelectionSymbol;
            }
            this.method_3(this.colorEdit1, this.ifeatureSelection_0.SelectionColor);
            this.bool_1 = true;
        }

        private void InitializeComponent()
        {
            this.radioGroup1 = new RadioGroup();
            this.label1 = new Label();
            this.btnSymbol = new StyleButton();
            this.colorEdit1 = new ColorEdit();
            this.radioGroup1.Properties.BeginInit();
            this.colorEdit1.Properties.BeginInit();
            base.SuspendLayout();
            this.radioGroup1.Location = new Point(8, 8);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = Color.Transparent;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup1.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "使用选择集中的符号"), new RadioGroupItem(null, "使用指定符号"), new RadioGroupItem(null, "使用指定颜色") });
            this.radioGroup1.Size = new Size(0xb8, 0xb0);
            this.radioGroup1.TabIndex = 1;
            this.radioGroup1.SelectedIndexChanged += new EventHandler(this.radioGroup1_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x4f, 0x11);
            this.label1.TabIndex = 2;
            this.label1.Text = "显示选择要素";
            this.btnSymbol.Location = new Point(40, 0x68);
            this.btnSymbol.Name = "btnSymbol";
            this.btnSymbol.Size = new Size(0x60, 0x20);
            this.btnSymbol.Style = null;
            this.btnSymbol.TabIndex = 4;
            this.btnSymbol.Click += new EventHandler(this.btnSymbol_Click);
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new Point(40, 0xa8);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(0x38, 0x17);
            this.colorEdit1.TabIndex = 5;
            base.Controls.Add(this.colorEdit1);
            base.Controls.Add(this.btnSymbol);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.radioGroup1);
            base.Name = "FeatureSelectionSetCtrl";
            base.Size = new Size(0x160, 0x108);
            base.Load += new EventHandler(this.FeatureSelectionSetCtrl_Load);
            this.radioGroup1.Properties.EndInit();
            this.colorEdit1.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void method_0(uint uint_0, out int int_0, out int int_1, out int int_2)
        {
            uint num = uint_0 & 0xff0000;
             int_2 = (int) (num >> 0x10);
            num = uint_0 & 0xff00;
            int_1 = (int) (num >> 8);
            num = uint_0 & 0xff;
            int_0 = (int) num;
        }

        private int method_1(int int_0, int int_1, int int_2)
        {
            uint num = 0;
            num = (uint) (0 | int_2);
            num = num << 8;
            num |=(uint) int_1;
            num = num << 8;
            return (int) (num | int_0);
        }

        private void method_2(ColorEdit colorEdit_0, IColor icolor_0)
        {
            if (icolor_0 != null)
            {
                icolor_0.NullColor = colorEdit_0.Color.IsEmpty;
                if (!colorEdit_0.Color.IsEmpty)
                {
                    icolor_0.Transparency = colorEdit_0.Color.A;
                    icolor_0.RGB = this.method_1(colorEdit_0.Color.R, colorEdit_0.Color.G, colorEdit_0.Color.B);
                }
            }
        }

        private void method_3(ColorEdit colorEdit_0, IColor icolor_0)
        {
            if (icolor_0 == null)
            {
                colorEdit_0.Color = Color.Empty;
            }
            else if (icolor_0.NullColor)
            {
                colorEdit_0.Color = Color.Empty;
            }
            else
            {
                int num2;
                int num3;
                int num4;
                int rGB = icolor_0.RGB;
               this.method_0((uint) rGB, out num2, out num3, out num4);
                colorEdit_0.Color = Color.FromArgb(icolor_0.Transparency, num2, num3, num4);
            }
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_1)
            {
                this.bool_0 = true;
            }
        }

        public IBasicMap FocusMap
        {
            set
            {
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_0;
            }
        }

        public object SelectItem
        {
            set
            {
                this.ifeatureSelection_0 = value as IFeatureSelection;
            }
        }

        public IStyleGallery StyleGallery
        {
            set
            {
                this.m_pSG = value;
            }
        }
    }
}

