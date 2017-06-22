using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Controls.Controls
{
    [ToolboxItem(false)]
    public partial class frmElementProperty : Form, IPropertySheet, IPropertySheetEvents
    {
        private short m_ActivePage = 0;
        private bool m_bChanged = false;
        private bool m_HideApplyButton = false;

        public event OnApplyEventHandler OnApply;

        public frmElementProperty()
        {
            this.InitializeComponent();
            this.btnApply.Enabled = false;
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

 public bool EditProperties(object @object)
        {
            for (int i = 0; i < this.tabControl1.TabPages.Count; i++)
            {
                TabPage page = this.tabControl1.TabPages[i];
                (page.Controls[0] as IPropertyPage).SetObjects(@object);
            }
            this.tabControl1.SelectedIndex = this.m_ActivePage;
            if (base.ShowDialog() == DialogResult.OK)
            {
                return this.m_bChanged;
            }
            return this.m_bChanged;
        }

 private void Property_OnValueChange()
        {
            this.btnApply.Enabled = true;
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

