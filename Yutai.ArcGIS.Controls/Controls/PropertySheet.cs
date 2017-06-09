using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Controls.Controls
{
    public class PropertySheet : Form, IPropertySheet, IPropertySheetEvents
    {
        private SimpleButton btnApply;
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private IContainer components = null;
        private short m_ActivePage = 0;
        private bool m_bChanged = false;
        private bool m_HideApplyButton = false;
        private Panel panel1;
        private TabControl tabControl1;

        public event OnApplyEventHandler OnApply;

        public PropertySheet()
        {
            this.InitializeComponent();
        }

        public void AddPage(object Page)
        {
            IPropertyPage page = Page as IPropertyPage;
            if (page != null)
            {
                (page as IPropertyPageEvents).OnValueChange += new OnValueChangeEventHandler(this.Property_OnValueChange);
                TabPage page2 = new TabPage(page.Title);
                if (page2.Width < page.Width)
                {
                    page2.Width = page.Width;
                }
                if (page2.Height < page.Height)
                {
                    page2.Height = page.Height;
                }
                (Page as Control).Dock = DockStyle.Fill;
                page2.Controls.Add(Page as Control);
                if (this.tabControl1.Width < page.Width)
                {
                    base.Width = page.Width;
                }
                if (base.Height < ((page.Height + this.panel1.Height) + 50))
                {
                    base.Height = (page.Height + this.panel1.Height) + 50;
                }
                this.tabControl1.TabPages.Add(page2);
            }
        }

        private void Apply()
        {
            this.m_bChanged = true;
            for (int i = 0; i < this.tabControl1.TabPages.Count; i++)
            {
                TabPage page = this.tabControl1.TabPages[i];
                (page.Controls[0] as IPropertyPage).Apply();
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.Apply();
            this.btnApply.Enabled = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.btnApply.Enabled)
            {
                this.Apply();
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

        public bool EditProperties(object @object)
        {
            for (int i = 0; i < this.tabControl1.TabPages.Count; i++)
            {
                TabPage page = this.tabControl1.TabPages[i];
                (page.Controls[0] as IPropertyPage).SetObjects(@object);
            }
            this.tabControl1.SelectedIndex = this.m_ActivePage;
            try
            {
                if (base.ShowDialog() == DialogResult.OK)
                {
                    return this.m_bChanged;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
            return this.m_bChanged;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PropertySheet));
            this.tabControl1 = new TabControl();
            this.panel1 = new Panel();
            this.btnApply = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.btnOK = new SimpleButton();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.tabControl1.Dock = DockStyle.Fill;
            this.tabControl1.Location = new Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(0x19c, 0xf1);
            this.tabControl1.TabIndex = 7;
            this.tabControl1.SelectedIndexChanged += new EventHandler(this.tabControl1_SelectedIndexChanged);
            this.panel1.Controls.Add(this.btnApply);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 0xf1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x19c, 0x20);
            this.panel1.TabIndex = 6;
            this.btnApply.Location = new Point(0x158, 4);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new Size(0x38, 0x18);
            this.btnApply.TabIndex = 6;
            this.btnApply.Text = "应用";
            this.btnApply.Click += new EventHandler(this.btnApply_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(280, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x38, 0x18);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0xd8, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x38, 0x18);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x19c, 0x111);
            base.Controls.Add(this.tabControl1);
            base.Controls.Add(this.panel1);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.Icon = (Icon) resources.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "PropertySheet";
            base.StartPosition = FormStartPosition.CenterParent;
            base.Load += new EventHandler(this.PropertySheet_Load);
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void Property_OnValueChange()
        {
            this.btnApply.Enabled = true;
        }

        private void PropertySheet_Load(object sender, EventArgs e)
        {
            this.btnApply.Enabled = false;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.btnApply.Enabled)
            {
                for (int i = 0; i < this.tabControl1.TabPages.Count; i++)
                {
                    if (i > this.m_ActivePage)
                    {
                        TabPage page = this.tabControl1.TabPages[i];
                        (page.Controls[0] as IPropertyPage).ResetControl();
                    }
                }
            }
            this.m_ActivePage = (short) this.tabControl1.SelectedIndex;
        }

        public short ActivePage
        {
            get
            {
                return this.m_ActivePage;
            }
            set
            {
                this.m_ActivePage = value;
            }
        }

        public bool HideApplyButton
        {
            get
            {
                return this.m_HideApplyButton;
            }
            set
            {
                this.m_HideApplyButton = value;
            }
        }

        public string Title
        {
            get
            {
                return this.Text;
            }
            set
            {
                this.Text = value;
            }
        }
    }
}

