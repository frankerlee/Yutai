using System;

namespace Yutai.ArcGIS.Framework
{
    public abstract class BarItemEx
    {
        protected BarEditStyle m_style = BarEditStyle.ComboBoxEdit;
        protected int m_width = 120;

        public event BarItemEditValueChangedHandler OnEditValueChanged;

        protected BarItemEx()
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

        public BarEditStyle Style
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