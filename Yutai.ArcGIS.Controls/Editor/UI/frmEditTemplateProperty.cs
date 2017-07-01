using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.Editor;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    public partial class frmEditTemplateProperty : Form
    {
        private PropertyControls m_property = new PropertyControls();

        public frmEditTemplateProperty()
        {
            this.InitializeComponent();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.EditTemplate.Name = this.txtName.Text;
            this.EditTemplate.Description = this.txtDescription.Text;
            this.EditTemplate.Label = this.txtLabel.Text;
            this.m_property.Apply();
        }

        private void frmEditTemplateProperty_Load(object sender, EventArgs e)
        {
            this.txtName.Text = this.EditTemplate.Name;
            this.txtDescription.Text = this.EditTemplate.Description;
            this.lblLayer.Text = this.EditTemplate.FeatureLayer.Name;
            this.txtLabel.Text = this.EditTemplate.Label;
            this.m_property.EditTemplate = this.EditTemplate;
            this.m_property.Dock = DockStyle.Fill;
            this.styleLable1.Style = this.EditTemplate.Symbol;
            this.panel1.Controls.Add(this.m_property);
        }

        private void label6_Paint(object sender, PaintEventArgs e)
        {
            if (this.EditTemplate.Symbol != null)
            {
                IStyleDraw draw = StyleDrawFactory.CreateStyleDraw(this.EditTemplate.Symbol);
                if (draw != null)
                {
                    draw.Draw(e.Graphics.GetHdc().ToInt32(), e.ClipRectangle, 96.0, 1.0);
                }
            }
        }

        public YTEditTemplate EditTemplate { get; set; }
    }
}