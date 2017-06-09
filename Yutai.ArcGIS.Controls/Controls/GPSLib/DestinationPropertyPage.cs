using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.Historical;
using Yutai.ArcGIS.Controls.SymbolUI;
using IPropertyPage = Yutai.ArcGIS.Common.BaseClasses.IPropertyPage;
using IPropertyPageEvents = Yutai.ArcGIS.Common.BaseClasses.IPropertyPageEvents;
using OnValueChangeEventHandler = Yutai.ArcGIS.Common.BaseClasses.OnValueChangeEventHandler;


namespace Yutai.ArcGIS.Controls.Controls.GPSLib
{
    public class DestinationPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private NewSymbolButton btnBearingSymbol;
        private NewSymbolButton btnLabelSymbol;
        private NewSymbolButton btnSymbol;
        private CheckBox chkShowBearingSymbol;
        private IContainer components = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label lbl;
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private IStyleGallery m_pSG = null;
        private IRealTimeDestination m_RealTimeDestination = null;
        private string m_Title = "常规";
        private TextBox txtLabel;

        public event OnValueChangeEventHandler OnValueChange;

        public DestinationPropertyPage()
        {
            this.InitializeComponent();
        }

        event Common.BaseClasses.OnValueChangeEventHandler IPropertyPageEvents.OnValueChange
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
                this.m_RealTimeDestination.BearingToDestinationSymbol = (this.btnBearingSymbol.Style as IClone).Clone() as IMarkerSymbol;
                this.m_RealTimeDestination.DestinationTextSymbol = (this.btnLabelSymbol.Style as IClone).Clone() as ITextSymbol;
                this.m_RealTimeDestination.DestinationSymbol = (this.btnSymbol.Style as IClone).Clone() as IMarkerSymbol;
                this.m_RealTimeDestination.DestinationLabel = this.txtLabel.Text;
                this.m_RealTimeDestination.ShowBearingToDestination = this.chkShowBearingSymbol.Checked;
            }
        }

        private void btnBearingSymbol_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.m_pSG);
                    selector.SetSymbol(this.m_RealTimeDestination.BearingToDestinationSymbol);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        this.btnBearingSymbol.Style = selector.GetSymbol();
                        this.ValueChange();
                    }
                }
            }
            catch
            {
            }
        }

        private void btnLabelSymbol_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.m_pSG);
                    selector.SetSymbol(this.m_RealTimeDestination.DestinationTextSymbol);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        this.btnLabelSymbol.Style = selector.GetSymbol();
                        this.ValueChange();
                    }
                }
            }
            catch
            {
            }
        }

        private void btnSymbol_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.m_pSG);
                    selector.SetSymbol(this.m_RealTimeDestination.DestinationSymbol);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        this.btnSymbol.Style = selector.GetSymbol();
                        this.ValueChange();
                    }
                }
            }
            catch
            {
            }
        }

        public void Cancel()
        {
            this.m_IsPageDirty = false;
        }

        private void chkShowBearingSymbol_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.ValueChange();
            }
        }

        private void DestinationPropertyPage_Load(object sender, EventArgs e)
        {
            if (this.m_RealTimeDestination != null)
            {
                this.btnBearingSymbol.Style = this.m_RealTimeDestination.BearingToDestinationSymbol;
                this.btnLabelSymbol.Style = this.m_RealTimeDestination.DestinationTextSymbol;
                this.btnSymbol.Style = this.m_RealTimeDestination.DestinationSymbol;
                this.txtLabel.Text = this.m_RealTimeDestination.DestinationLabel;
                this.chkShowBearingSymbol.Checked = this.m_RealTimeDestination.ShowBearingToDestination;
                this.m_CanDo = true;
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

        private void InitializeComponent()
        {
            this.lbl = new Label();
            this.btnSymbol = new NewSymbolButton();
            this.label1 = new Label();
            this.txtLabel = new TextBox();
            this.label2 = new Label();
            this.btnLabelSymbol = new NewSymbolButton();
            this.btnBearingSymbol = new NewSymbolButton();
            this.label3 = new Label();
            this.chkShowBearingSymbol = new CheckBox();
            base.SuspendLayout();
            this.lbl.AutoSize = true;
            this.lbl.Location = new Point(20, 14);
            this.lbl.Name = "lbl";
            this.lbl.Size = new Size(0x1d, 12);
            this.lbl.TabIndex = 0;
            this.lbl.Text = "符号";
            this.btnSymbol.Location = new Point(0x5d, 14);
            this.btnSymbol.Name = "btnSymbol";
            this.btnSymbol.Size = new Size(0x4b, 0x25);
            this.btnSymbol.Style = null;
            this.btnSymbol.TabIndex = 1;
            this.btnSymbol.Click += new EventHandler(this.btnSymbol_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(20, 0x44);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "标记";
            this.txtLabel.Location = new Point(0x5d, 0x44);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.Size = new Size(0x86, 0x15);
            this.txtLabel.TabIndex = 3;
            this.txtLabel.TextChanged += new EventHandler(this.txtLabel_TextChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(20, 0x75);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "标记符号";
            this.btnLabelSymbol.Location = new Point(0x5d, 0x69);
            this.btnLabelSymbol.Name = "btnLabelSymbol";
            this.btnLabelSymbol.Size = new Size(120, 0x25);
            this.btnLabelSymbol.Style = null;
            this.btnLabelSymbol.TabIndex = 5;
            this.btnLabelSymbol.Click += new EventHandler(this.btnLabelSymbol_Click);
            this.btnBearingSymbol.Location = new Point(0x5d, 0xca);
            this.btnBearingSymbol.Name = "btnBearingSymbol";
            this.btnBearingSymbol.Size = new Size(0x4b, 0x25);
            this.btnBearingSymbol.Style = null;
            this.btnBearingSymbol.TabIndex = 7;
            this.btnBearingSymbol.Click += new EventHandler(this.btnBearingSymbol_Click);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(20, 0xd6);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x35, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "方向符号";
            this.chkShowBearingSymbol.AutoSize = true;
            this.chkShowBearingSymbol.Location = new Point(0x18, 0x9f);
            this.chkShowBearingSymbol.Name = "chkShowBearingSymbol";
            this.chkShowBearingSymbol.Size = new Size(0x90, 0x10);
            this.chkShowBearingSymbol.TabIndex = 8;
            this.chkShowBearingSymbol.Text = "显示到目标的方向符号";
            this.chkShowBearingSymbol.UseVisualStyleBackColor = true;
            this.chkShowBearingSymbol.CheckedChanged += new EventHandler(this.chkShowBearingSymbol_CheckedChanged);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.chkShowBearingSymbol);
            base.Controls.Add(this.btnBearingSymbol);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.btnLabelSymbol);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.txtLabel);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.btnSymbol);
            base.Controls.Add(this.lbl);
            base.Name = "DestinationPropertyPage";
            base.Size = new Size(0x112, 0x107);
            base.Load += new EventHandler(this.DestinationPropertyPage_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

        public void ResetControl()
        {
        }

        public void SetObjects(object @object)
        {
            this.m_RealTimeDestination = @object as IRealTimeDestination;
        }

        private void txtLabel_TextChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.ValueChange();
            }
        }

        private void ValueChange()
        {
            this.m_IsPageDirty = true;
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
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

        public IRealTimeDestination RealTimeDestination
        {
            set
            {
                this.m_RealTimeDestination = value;
            }
        }

        public IStyleGallery StyleGallery
        {
            set
            {
                this.m_pSG = value;
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

