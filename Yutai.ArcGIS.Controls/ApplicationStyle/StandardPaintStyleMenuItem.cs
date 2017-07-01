using System;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.ControlExtenders;
using Yutai.ArcGIS.Common.Renderer;

namespace Yutai.ArcGIS.Controls.ApplicationStyle
{
    public class StandardPaintStyleMenuItem
    {
        private ToolStripMenuItem iPaintStyle;
        private ToolStripMenuItem ipsDefault;
        private ToolStripMenuItem ipsO7;
        private StandardBarManager m_pBarManager = null;

        private void Init()
        {
            this.iPaintStyle = new ToolStripMenuItem();
            this.ipsDefault = new ToolStripMenuItem();
            this.ipsO7 = new ToolStripMenuItem();
            this.iPaintStyle.Text = "界面风格";
            this.iPaintStyle.Name = "iPaintStyle";
            this.ipsDefault.Text = "默认风格";
            this.ipsDefault.Name = "ipsDefault";
            this.ipsDefault.Click += new EventHandler(this.ipsDefault_Click);
            this.ipsO7.Text = "Office 2007";
            this.ipsO7.Name = "ipsO7";
            this.ipsO7.Click += new EventHandler(this.ipsO7_Click);
            if (this.m_pBarManager.MainMenu != null)
            {
                this.m_pBarManager.MainMenu.Items.Add(this.iPaintStyle);
            }
            else if (this.m_pBarManager.BarCount > 0)
            {
                this.m_pBarManager.GetBar(0).Items.Add(this.iPaintStyle);
            }
            this.iPaintStyle.DropDownItems.Add(this.ipsDefault);
            this.iPaintStyle.DropDownItems.Add(this.ipsO7);
        }

        private void ipsDefault_Click(object sender, EventArgs e)
        {
            ToolStripManager.Renderer = null;
        }

        private void ipsO7_Click(object sender, EventArgs e)
        {
            ToolStripManager.Renderer = new Office2007Renderer();
        }

        public StandardBarManager BarManager
        {
            set
            {
                this.m_pBarManager = value;
                this.Init();
            }
        }
    }
}