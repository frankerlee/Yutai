using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    public class TickSymbolPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private StyleButton btnStyle;
        private Container components = null;
        private GroupBox groupBox1;
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private ILineSymbol m_TickLineSymbol = null;
        private IMarkerSymbol m_TickMarkSymbol = null;
        private string m_Title = "线";
        private RadioGroup radioGroup1;

        public event OnValueChangeEventHandler OnValueChange;

        public TickSymbolPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
                if (this.radioGroup1.SelectedIndex == 2)
                {
                    GridAxisPropertyPage.m_pMapGrid.TickMarkSymbol = null;
                    GridAxisPropertyPage.m_pMapGrid.LineSymbol = null;
                }
                else if (this.radioGroup1.SelectedIndex == 0)
                {
                    GridAxisPropertyPage.m_pMapGrid.TickMarkSymbol = null;
                    GridAxisPropertyPage.m_pMapGrid.LineSymbol = this.m_TickLineSymbol;
                }
                else
                {
                    GridAxisPropertyPage.m_pMapGrid.LineSymbol = null;
                    GridAxisPropertyPage.m_pMapGrid.TickMarkSymbol = this.m_TickMarkSymbol;
                }
            }
        }

        private void btnStyle_Click(object sender, EventArgs e)
        {
            frmSymbolSelector selector = new frmSymbolSelector();
            if (selector != null)
            {
                selector.SetStyleGallery(GridAxisPropertyPage.m_pSG);
                selector.SetSymbol(this.btnStyle.Style);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    this.btnStyle.Style = selector.GetSymbol();
                    if (this.radioGroup1.SelectedIndex == 0)
                    {
                        this.m_TickLineSymbol = this.btnStyle.Style as ILineSymbol;
                    }
                    else if (this.radioGroup1.SelectedIndex == 1)
                    {
                        this.m_TickMarkSymbol = this.btnStyle.Style as IMarkerSymbol;
                    }
                    this.m_IsPageDirty = true;
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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
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
                this.btnStyle.Style = this.m_TickLineSymbol;
                this.btnStyle.Enabled = true;
            }
            else
            {
                this.btnStyle.Style = this.m_TickMarkSymbol;
                this.btnStyle.Enabled = true;
            }
            this.btnStyle.Invalidate();
            this.m_IsPageDirty = true;
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
            }
        }

        public void SetObjects(object @object)
        {
        }

        private void TickSymbolPropertyPage_Load(object sender, EventArgs e)
        {
            if (GridAxisPropertyPage.m_pMapGrid != null)
            {
                if (GridAxisPropertyPage.m_pMapGrid.LineSymbol != null)
                {
                    this.radioGroup1.SelectedIndex = 0;
                    this.m_TickLineSymbol = GridAxisPropertyPage.m_pMapGrid.LineSymbol;
                    this.m_TickMarkSymbol = new SimpleMarkerSymbolClass();
                    (this.m_TickMarkSymbol as ISimpleMarkerSymbol).Style = esriSimpleMarkerStyle.esriSMSCross;
                    this.btnStyle.Style = this.m_TickLineSymbol;
                }
                else if (GridAxisPropertyPage.m_pMapGrid.TickMarkSymbol != null)
                {
                    this.radioGroup1.SelectedIndex = 1;
                    this.m_TickMarkSymbol = GridAxisPropertyPage.m_pMapGrid.TickMarkSymbol;
                    this.m_TickLineSymbol = new SimpleLineSymbolClass();
                    this.btnStyle.Style = this.m_TickMarkSymbol;
                }
                else
                {
                    this.radioGroup1.SelectedIndex = 2;
                    this.btnStyle.Enabled = false;
                }
                this.m_CanDo = true;
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
                return this.m_Title;
            }
            set
            {
                this.m_Title = value;
            }
        }
    }
}

