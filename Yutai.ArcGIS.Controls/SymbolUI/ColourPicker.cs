using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    internal class ColourPicker : Button
    {
        private Container components = null;
        protected Rectangle m_ArrowRect;
        protected bool m_bActive;
        protected bool m_bTrackSelection;
        protected Color m_crColourBk;
        protected Color m_crColourText;
        protected int m_nSelectionMode;
        protected string m_strCustomText;
        protected string m_strDefaultText;

        public ColourPicker()
        {
            this.InitializeComponent();
            this.m_bTrackSelection = false;
            this.m_nSelectionMode = 2;
            this.m_bActive = false;
            this.m_strDefaultText = "无色";
            this.m_strCustomText = "更多颜色……";
        }

        private void ColourPicker_Paint(object sender, PaintEventArgs e)
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

        private void InitializeComponent()
        {
            base.Paint += new PaintEventHandler(this.ColourPicker_Paint);
        }
    }
}

