using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmPropertySheet : Form, IPropertySheet, IPropertySheetEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Container container_0 = null;
        private short short_0 = 0;

        public event OnApplyEventHandler OnApply;

        public frmPropertySheet()
        {
            this.InitializeComponent();
            this.btnApply.Enabled = false;
        }

        public void AddPage(object object_0)
        {
            IPropertyPage page = object_0 as IPropertyPage;
            if (page != null)
            {
                (page as IPropertyPageEvents).OnValueChange += new OnValueChangeEventHandler(this.method_1);
                TabPage page2 = new TabPage(page.Title);
                if (page2.Width < page.Width)
                {
                    page2.Width = page.Width;
                }
                if (page2.Height < page.Height)
                {
                    page2.Height = page.Height;
                }
                (object_0 as Control).Dock = DockStyle.Fill;
                page2.Controls.Add(object_0 as Control);
                if (this.tabControl1.Width < page.Width)
                {
                    base.Width = page.Width + 50;
                }
                if (base.Height < ((page.Height + this.panel1.Height) + 50))
                {
                    base.Height = (page.Height + this.panel1.Height) + 50;
                }
                this.tabControl1.TabPages.Add(page2);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.method_0();
            this.btnApply.Enabled = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.method_0();
        }

        public bool EditProperties(object object_0)
        {
            for (int i = 0; i < this.tabControl1.TabPages.Count; i++)
            {
                TabPage page = this.tabControl1.TabPages[i];
                (page.Controls[0] as IPropertyPage).SetObjects(object_0);
            }
            this.tabControl1.SelectedIndex = this.short_0;
            if (base.ShowDialog() == DialogResult.OK)
            {
                return this.bool_0;
            }
            return this.bool_0;
        }

        private void method_0()
        {
            this.bool_0 = true;
            for (int i = 0; i < this.tabControl1.TabPages.Count; i++)
            {
                TabPage page = this.tabControl1.TabPages[i];
                (page.Controls[0] as IPropertyPage).Apply();
            }
        }

        private void method_1()
        {
            this.btnApply.Enabled = true;
        }

        public short ActivePage
        {
            get { return this.short_0; }
            set { this.short_0 = value; }
        }

        public bool HideApplyButton
        {
            get { return this.bool_1; }
            set { this.bool_1 = value; }
        }

        public string Title
        {
            get { return this.Text; }
            set { this.Text = value; }
        }
    }
}