using System;
using Yutai.Plugins.Enums;

namespace Yutai.UI.Controls
{
    public abstract class RibbonItemEx
    {
        protected RibbonEditStyle m_style = RibbonEditStyle.ComboBoxEdit;
        protected int m_width = 120;

        public event RibbonItemEditValueChangedHandler OnEditValueChanged;

        protected RibbonItemEx()
        {
        }

        public void EditValueChanged(object sender, EventArgs e)
        {
            if (this.OnEditValueChanged != null)
            {
                this.OnEditValueChanged(sender, e);
            }
        }

        public abstract object EditValue { get; set; }

        public abstract bool Enabled { get; set; }

        public RibbonEditStyle Style
        {
            get { return this.m_style; }
        }

        public int Width
        {
            get { return this.m_width; }
            set { this.m_width = value; }
        }
    }
}