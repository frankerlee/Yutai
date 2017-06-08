using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public class TickSymbolPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private StyleButton btnStyle;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private ILineSymbol ilineSymbol_0 = null;
        private IMarkerSymbol imarkerSymbol_0 = null;
        protected IMapGrid m_pMapGrid = null;
        [CompilerGenerated]
        private MapCartoTemplateLib.MapTemplate mapTemplate_0;
        private RadioGroup radioGroup1;
        private string string_0 = "线";

        public event OnValueChangeEventHandler OnValueChange;

        public TickSymbolPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                if (this.radioGroup1.SelectedIndex == 2)
                {
                    this.m_pMapGrid.TickMarkSymbol = null;
                    this.m_pMapGrid.LineSymbol = null;
                }
                else if (this.radioGroup1.SelectedIndex == 0)
                {
                    this.m_pMapGrid.TickMarkSymbol = null;
                    this.m_pMapGrid.LineSymbol = this.ilineSymbol_0;
                }
                else
                {
                    this.m_pMapGrid.LineSymbol = null;
                    this.m_pMapGrid.TickMarkSymbol = this.imarkerSymbol_0;
                }
            }
        }

        private void btnStyle_Click(object sender, EventArgs e)
        {
            frmSymbolSelector selector = new frmSymbolSelector();
            if (selector != null)
            {
                selector.SetStyleGallery(ApplicationBase.StyleGallery);
                selector.SetSymbol(this.btnStyle.Style);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    this.btnStyle.Style = selector.GetSymbol();
                    if (this.radioGroup1.SelectedIndex == 0)
                    {
                        this.ilineSymbol_0 = this.btnStyle.Style as ILineSymbol;
                    }
                    else if (this.radioGroup1.SelectedIndex == 1)
                    {
                        this.imarkerSymbol_0 = this.btnStyle.Style as IMarkerSymbol;
                    }
                    this.bool_1 = true;
                    if (this.OnValueChange != null)
                    {
                        this.OnValueChange();
                    }
                }
            }
        }

        public void Cancel()
        {
        }

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_2);
        }

        public void Hide()
        {
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.btnStyle = new StyleButton();
            this.radioGroup1 = new RadioGroup();
            this.groupBox1.SuspendLayout();
            this.radioGroup1.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.btnStyle);
            this.groupBox1.Controls.Add(this.radioGroup1);
            this.groupBox1.Location = new Point(0x10, 0x10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(240, 0x88);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "显示属性";
            this.btnStyle.Location = new Point(0x18, 0x60);
            this.btnStyle.Name = "btnStyle";
            this.btnStyle.Size = new Size(80, 0x20);
            this.btnStyle.Style = null;
            this.btnStyle.TabIndex = 1;
            this.btnStyle.Click += new EventHandler(this.btnStyle_Click);
            this.radioGroup1.Location = new Point(0x10, 0x10);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup1.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "格网线"), new RadioGroupItem(null, "格网刻度"), new RadioGroupItem(null, "不显示线或刻度") });
            this.radioGroup1.Size = new Size(0x88, 0x48);
            this.radioGroup1.TabIndex = 0;
            this.radioGroup1.SelectedIndexChanged += new EventHandler(this.radioGroup1_SelectedIndexChanged);
            base.Controls.Add(this.groupBox1);
            base.Name = "TickSymbolPropertyPage";
            base.Size = new Size(280, 240);
            base.Load += new EventHandler(this.TickSymbolPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.radioGroup1.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.radioGroup1.SelectedIndex == 2)
            {
                this.btnStyle.Style = null;
                this.btnStyle.Enabled = false;
            }
            else if (this.radioGroup1.SelectedIndex == 0)
            {
                this.btnStyle.Style = this.ilineSymbol_0;
                this.btnStyle.Enabled = true;
            }
            else
            {
                this.btnStyle.Style = this.imarkerSymbol_0;
                this.btnStyle.Enabled = true;
            }
            this.btnStyle.Invalidate();
            this.bool_1 = true;
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
            }
        }

        public void ResetControl()
        {
        }

        public void SetObjects(object object_0)
        {
            this.MapTemplate = object_0 as MapCartoTemplateLib.MapTemplate;
            this.m_pMapGrid = this.MapTemplate.MapGrid;
        }

        private void TickSymbolPropertyPage_Load(object sender, EventArgs e)
        {
            this.m_pMapGrid = this.MapTemplate.MapGrid;
            if (this.m_pMapGrid != null)
            {
                if (this.m_pMapGrid.LineSymbol != null)
                {
                    this.radioGroup1.SelectedIndex = 0;
                    this.ilineSymbol_0 = this.m_pMapGrid.LineSymbol;
                    this.imarkerSymbol_0 = new SimpleMarkerSymbolClass();
                    (this.imarkerSymbol_0 as ISimpleMarkerSymbol).Style = esriSimpleMarkerStyle.esriSMSCross;
                    this.btnStyle.Style = this.ilineSymbol_0;
                }
                else if (this.m_pMapGrid.TickMarkSymbol != null)
                {
                    this.radioGroup1.SelectedIndex = 1;
                    this.imarkerSymbol_0 = this.m_pMapGrid.TickMarkSymbol;
                    this.ilineSymbol_0 = new SimpleLineSymbolClass();
                    this.btnStyle.Style = this.imarkerSymbol_0;
                }
                else
                {
                    this.radioGroup1.SelectedIndex = 2;
                    this.btnStyle.Enabled = false;
                }
                this.bool_0 = true;
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

        public MapCartoTemplateLib.MapTemplate MapTemplate
        {
            [CompilerGenerated]
            get
            {
                return this.mapTemplate_0;
            }
            [CompilerGenerated]
            set
            {
                this.mapTemplate_0 = value;
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

