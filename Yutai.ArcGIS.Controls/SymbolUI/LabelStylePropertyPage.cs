using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.Plugins.Interfaces;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    internal class LabelStylePropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private SimpleButton btnLabelPlacement;
        private SimpleButton btnLabelSymbol;
        private Container components = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private bool m_IsPageDirty = false;
        private ILabelStyle m_pLabelStyle = null;
        private IBasicOverposterLayerProperties m_pOverposterLayerProperties = null;
        private ITextSymbol m_pTextSymbol = null;
        private SymbolItem symbolItem1;
        private IAppContext _context;

        public event OnValueChangeEventHandler OnValueChange;

        public LabelStylePropertyPage()
        {
            this.InitializeComponent();
        }

        public LabelStylePropertyPage(IAppContext context)
        {
            this.InitializeComponent();
            _context =context;
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
            }
        }

        private void btnLabelPlacement_Click(object sender, EventArgs e)
        {
            frmElementProperty property = new frmElementProperty {
                Text = "放值属性"
            };
            LabelPlacementPropertyPage page = new LabelPlacementPropertyPage();
            property.AddPage(page);
            LabelConficPropertyPage page2 = new LabelConficPropertyPage();
            property.AddPage(page2);
            if (property.EditProperties(this.m_pOverposterLayerProperties))
            {
            }
        }

        private void btnLabelSymbol_Click(object sender, EventArgs e)
        {
            if (this.m_pTextSymbol != null)
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                selector.SetStyleGallery(_context.StyleGallery);
                selector.SetSymbol(this.m_pTextSymbol);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    this.m_pTextSymbol = selector.GetSymbol() as ITextSymbol;
                    this.symbolItem1.Symbol = this.m_pTextSymbol;
                    this.symbolItem1.Invalidate();
                }
            }
        }

        public void Cancel()
        {
            this.m_IsPageDirty = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.btnLabelSymbol = new SimpleButton();
            this.symbolItem1 = new SymbolItem();
            this.groupBox2 = new GroupBox();
            this.btnLabelPlacement = new SimpleButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.btnLabelSymbol);
            this.groupBox1.Controls.Add(this.symbolItem1);
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x108, 0x60);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "文本符号属性";
            this.btnLabelSymbol.Location = new Point(0xa8, 0x20);
            this.btnLabelSymbol.Name = "btnLabelSymbol";
            this.btnLabelSymbol.Size = new Size(80, 0x18);
            this.btnLabelSymbol.TabIndex = 3;
            this.btnLabelSymbol.Text = "符号属性...";
            this.btnLabelSymbol.Click += new EventHandler(this.btnLabelSymbol_Click);
            this.symbolItem1.BackColor = SystemColors.ControlLight;
            this.symbolItem1.Location = new Point(0x10, 0x20);
            this.symbolItem1.Name = "symbolItem1";
            this.symbolItem1.Size = new Size(0x80, 0x20);
            this.symbolItem1.Symbol = null;
            this.symbolItem1.TabIndex = 2;
            this.groupBox2.Controls.Add(this.btnLabelPlacement);
            this.groupBox2.Location = new Point(8, 0x70);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x108, 0x48);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "放置";
            this.btnLabelPlacement.Location = new Point(0x10, 0x18);
            this.btnLabelPlacement.Name = "btnLabelPlacement";
            this.btnLabelPlacement.Size = new Size(0x80, 0x18);
            this.btnLabelPlacement.TabIndex = 4;
            this.btnLabelPlacement.Text = "标注放置选项...";
            this.btnLabelPlacement.Click += new EventHandler(this.btnLabelPlacement_Click);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "LabelStylePropertyPage";
            base.Size = new Size(0x128, 0xd8);
            base.Load += new EventHandler(this.LabelStylePropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

        private void LabelStylePropertyPage_Load(object sender, EventArgs e)
        {
            this.symbolItem1.Symbol = this.m_pTextSymbol;
        }

        public void SetObjects(object @object)
        {
            ILabelStyle style = @object as ILabelStyle;
            if (style != null)
            {
                this.m_pTextSymbol = (style.Symbol as IClone).Clone() as ITextSymbol;
                this.m_pOverposterLayerProperties = (style.BasicOverposterLayerProperties as IClone).Clone() as IBasicOverposterLayerProperties;
            }
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
                return "标注样式";
            }
            set
            {
            }
        }
    }
}

