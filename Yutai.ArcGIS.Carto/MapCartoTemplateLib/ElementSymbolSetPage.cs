using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public class ElementSymbolSetPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private IContainer icontainer_0 = null;
        private Label label1;
        private MapCartoTemplateLib.MapTemplateElement mapTemplateElement_0;
        private StyleButton styleButton1;

        public event OnValueChangeEventHandler OnValueChange;

        public ElementSymbolSetPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                if (this.mapTemplateElement_0.Element is IFillShapeElement)
                {
                    (this.mapTemplateElement_0.Element as IFillShapeElement).Symbol = this.styleButton1.Style as IFillSymbol;
                }
                else if (this.mapTemplateElement_0.Element is ILineElement)
                {
                    (this.mapTemplateElement_0.Element as ILineElement).Symbol = this.styleButton1.Style as ILineSymbol;
                }
            }
        }

        public void Cancel()
        {
            this.bool_1 = false;
        }

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_2);
        }

        private void ElementSymbolSetPage_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

        private void InitializeComponent()
        {
            this.styleButton1 = new StyleButton();
            this.label1 = new Label();
            base.SuspendLayout();
            this.styleButton1.Location = new Point(12, 20);
            this.styleButton1.Name = "styleButton1";
            this.styleButton1.Size = new Size(0xc4, 0x49);
            this.styleButton1.Style = null;
            this.styleButton1.TabIndex = 0;
            this.styleButton1.Click += new EventHandler(this.styleButton1_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(10, 5);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "选择符号";
            base.Controls.Add(this.label1);
            base.Controls.Add(this.styleButton1);
            base.Name = "ElementSymbolSetPage";
            base.Size = new Size(0xd5, 0xa5);
            base.Load += new EventHandler(this.ElementSymbolSetPage_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

        private void method_0()
        {
            this.bool_0 = false;
            if (this.mapTemplateElement_0.Style != null)
            {
                this.styleButton1.Style = this.mapTemplateElement_0.Style;
            }
            else if (this.mapTemplateElement_0.Element is IFillShapeElement)
            {
                this.styleButton1.Style = (this.mapTemplateElement_0.Element as IFillShapeElement).Symbol;
            }
            else if (this.mapTemplateElement_0.Element is ILineElement)
            {
                this.styleButton1.Style = (this.mapTemplateElement_0.Element as ILineElement).Symbol;
            }
            this.bool_0 = true;
        }

        public void ResetControl()
        {
            this.bool_0 = false;
            this.method_0();
            this.bool_0 = true;
        }

        public void SetObjects(object object_0)
        {
            this.mapTemplateElement_0 = object_0 as MapCartoTemplateLib.MapTemplateElement;
            this.styleButton1.Style = this.mapTemplateElement_0.Style;
        }

        private void styleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(ApplicationBase.StyleGallery);
                    selector.SetSymbol(this.styleButton1.Style);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        this.styleButton1.Style = selector.GetSymbol();
                        this.mapTemplateElement_0.Style = this.styleButton1.Style;
                        this.bool_1 = true;
                        if (this.OnValueChange != null)
                        {
                            this.OnValueChange();
                        }
                    }
                }
            }
            catch
            {
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

        public MapCartoTemplateLib.MapTemplateElement MapTemplateElement
        {
            set
            {
                this.mapTemplateElement_0 = value;
                this.method_0();
            }
        }

        public string Title
        {
            get
            {
                return "符号";
            }
            set
            {
            }
        }
    }
}

