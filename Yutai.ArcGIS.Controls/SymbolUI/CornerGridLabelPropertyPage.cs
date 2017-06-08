using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    public class CornerGridLabelPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private CheckEdit chkLowerLeft;
        private CheckEdit chkLowerRight;
        private CheckEdit chkUpperLeft;
        private CheckEdit chkUpperRight;
        private Container components = null;
        private Label label1;
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private ICornerGridLabel m_pCornerGridLabel = null;
        private string m_Title = "角标";

        public event OnValueChangeEventHandler OnValueChange;

        public CornerGridLabelPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
                this.m_pCornerGridLabel.set_CornerLabel(esriGridCornerEnum.esriGridCornerLowerLeft, this.chkLowerLeft.Checked);
                this.m_pCornerGridLabel.set_CornerLabel(esriGridCornerEnum.esriGridCornerLowerRight, this.chkLowerRight.Checked);
                this.m_pCornerGridLabel.set_CornerLabel(esriGridCornerEnum.esriGridCornerUpperLeft, this.chkUpperLeft.Checked);
                this.m_pCornerGridLabel.set_CornerLabel(esriGridCornerEnum.esriGridCornerUpperRight, this.chkUpperRight.Checked);
            }
        }

        public void Cancel()
        {
        }

        private void chkLowerLeft_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void chkLowerRight_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void chkUpperLeft_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void chkUpperRight_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void CornerGridLabelPropertyPage_Load(object sender, EventArgs e)
        {
            this.chkLowerLeft.Checked = this.m_pCornerGridLabel.get_CornerLabel(esriGridCornerEnum.esriGridCornerLowerLeft);
            this.chkLowerRight.Checked = this.m_pCornerGridLabel.get_CornerLabel(esriGridCornerEnum.esriGridCornerLowerRight);
            this.chkUpperLeft.Checked = this.m_pCornerGridLabel.get_CornerLabel(esriGridCornerEnum.esriGridCornerUpperLeft);
            this.chkUpperRight.Checked = this.m_pCornerGridLabel.get_CornerLabel(esriGridCornerEnum.esriGridCornerUpperRight);
            this.m_CanDo = true;
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
            this.label1 = new Label();
            this.chkUpperLeft = new CheckEdit();
            this.chkUpperRight = new CheckEdit();
            this.chkLowerRight = new CheckEdit();
            this.chkLowerLeft = new CheckEdit();
            this.chkUpperLeft.Properties.BeginInit();
            this.chkUpperRight.Properties.BeginInit();
            this.chkLowerRight.Properties.BeginInit();
            this.chkLowerLeft.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x99, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择哪个角显示完整的数字";
            this.chkUpperLeft.Location = new Point(0x20, 0x38);
            this.chkUpperLeft.Name = "chkUpperLeft";
            this.chkUpperLeft.Properties.Caption = "左上";
            this.chkUpperLeft.RightToLeft = RightToLeft.Yes;
            this.chkUpperLeft.Size = new Size(0x58, 0x13);
            this.chkUpperLeft.TabIndex = 1;
            this.chkUpperLeft.CheckedChanged += new EventHandler(this.chkUpperLeft_CheckedChanged);
            this.chkUpperRight.Location = new Point(0x80, 0x38);
            this.chkUpperRight.Name = "chkUpperRight";
            this.chkUpperRight.Properties.Caption = "右上";
            this.chkUpperRight.RightToLeft = RightToLeft.Yes;
            this.chkUpperRight.Size = new Size(0x58, 0x13);
            this.chkUpperRight.TabIndex = 2;
            this.chkUpperRight.CheckedChanged += new EventHandler(this.chkUpperRight_CheckedChanged);
            this.chkLowerRight.Location = new Point(0x84, 0x6b);
            this.chkLowerRight.Name = "chkLowerRight";
            this.chkLowerRight.Properties.Caption = "右下";
            this.chkLowerRight.RightToLeft = RightToLeft.Yes;
            this.chkLowerRight.Size = new Size(0x58, 0x13);
            this.chkLowerRight.TabIndex = 4;
            this.chkLowerRight.CheckedChanged += new EventHandler(this.chkLowerRight_CheckedChanged);
            this.chkLowerLeft.Location = new Point(0x24, 0x6b);
            this.chkLowerLeft.Name = "chkLowerLeft";
            this.chkLowerLeft.Properties.Caption = "左下";
            this.chkLowerLeft.RightToLeft = RightToLeft.Yes;
            this.chkLowerLeft.Size = new Size(0x58, 0x13);
            this.chkLowerLeft.TabIndex = 3;
            this.chkLowerLeft.CheckedChanged += new EventHandler(this.chkLowerLeft_CheckedChanged);
            base.Controls.Add(this.chkLowerRight);
            base.Controls.Add(this.chkLowerLeft);
            base.Controls.Add(this.chkUpperRight);
            base.Controls.Add(this.chkUpperLeft);
            base.Controls.Add(this.label1);
            base.Name = "CornerGridLabelPropertyPage";
            base.Size = new Size(0x100, 0xe8);
            base.Load += new EventHandler(this.CornerGridLabelPropertyPage_Load);
            this.chkUpperLeft.Properties.EndInit();
            this.chkUpperRight.Properties.EndInit();
            this.chkLowerRight.Properties.EndInit();
            this.chkLowerLeft.Properties.EndInit();
            base.ResumeLayout(false);
        }

        public void SetObjects(object @object)
        {
            this.m_pCornerGridLabel = @object as ICornerGridLabel;
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
            }
        }
    }
}

