using System.ComponentModel;
using System.Drawing;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Common.SymbolLib
{
    public class StyleComboBox : System.Windows.Forms.ComboBox
    {
        private IArray iarray_0 = new Array();

        private Container container_0 = null;

        public StyleComboBox(IContainer icontainer_0)
        {
            icontainer_0.Add(this);
            this.method_0();
        }

        public StyleComboBox()
        {
            this.method_0();
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && this.container_0 != null)
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void method_0()
        {
            base.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            base.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            base.DropDownWidth = 160;
            base.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.StyleComboBox_MeasureItem);
            base.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.StyleComboBox_DrawItem);
        }

        private void StyleComboBox_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            if (this.iarray_0.Count != 0)
            {
                e.DrawBackground();
                try
                {
                    IStyleGalleryItem styleGalleryItem = this.iarray_0.get_Element(e.Index) as IStyleGalleryItem;
                    if (styleGalleryItem != null)
                    {
                        System.IntPtr hdc = e.Graphics.GetHdc();
                        IStyleDraw styleDraw = StyleDrawFactory.CreateStyleDraw(styleGalleryItem.Item);
                        if (styleDraw != null)
                        {
                            e.Bounds.Inflate(-2, -4);
                            styleDraw.Draw(hdc.ToInt32(), e.Bounds, 72.0, 1.0);
                        }
                        e.Graphics.ReleaseHdc(hdc);
                    }
                    else
                    {
                        Brush brush = new SolidBrush(e.ForeColor);
                        e.Graphics.DrawString(base.Items[e.Index].ToString(), e.Font, brush, (float) (e.Bounds.Left + 3),
                            (float) (e.Bounds.Top + 3));
                    }
                }
                catch
                {
                }
            }
        }

        public void Add(IStyleGalleryItem istyleGalleryItem_0)
        {
            this.iarray_0.Add(istyleGalleryItem_0);
            if (istyleGalleryItem_0 != null)
            {
                base.Items.Add(istyleGalleryItem_0.Name);
            }
            else
            {
                base.Items.Add("<无>");
            }
        }

        public void Clear()
        {
            this.iarray_0.RemoveAll();
            base.Items.Clear();
        }

        public IStyleGalleryItem GetSelectStyleGalleryItem()
        {
            IStyleGalleryItem result;
            if (this.SelectedIndex < 0)
            {
                result = null;
            }
            else
            {
                result = (this.iarray_0.get_Element(this.SelectedIndex) as IStyleGalleryItem);
            }
            return result;
        }

        public void RemoveAt(int int_0)
        {
            if (int_0 >= 0 || int_0 < this.iarray_0.Count)
            {
                this.iarray_0.Remove(int_0);
                base.Items.RemoveAt(int_0);
            }
        }

        public IStyleGalleryItem GetStyleGalleryItemAt(int int_0)
        {
            IStyleGalleryItem result;
            if (int_0 < 0 && int_0 >= this.iarray_0.Count)
            {
                result = null;
            }
            else
            {
                result = (this.iarray_0.get_Element(int_0) as IStyleGalleryItem);
            }
            return result;
        }

        public int SelectStyleGalleryItem(IStyleGalleryItem istyleGalleryItem_0)
        {
            int result;
            for (int i = 0; i < this.iarray_0.Count; i++)
            {
                IStyleGalleryItem styleGalleryItem = this.iarray_0.get_Element(i) as IStyleGalleryItem;
                if (styleGalleryItem != null && istyleGalleryItem_0 != null)
                {
                    if (istyleGalleryItem_0.Name == (this.iarray_0.get_Element(i) as IStyleGalleryItem).Name)
                    {
                        this.SelectedIndex = i;
                        result = i;
                        return result;
                    }
                }
                else if (styleGalleryItem == istyleGalleryItem_0)
                {
                    this.SelectedIndex = i;
                    result = i;
                    return result;
                }
            }
            this.Add(istyleGalleryItem_0);
            this.SelectedIndex = this.iarray_0.Count - 1;
            result = this.SelectedIndex;
            return result;
        }

        private void StyleComboBox_MeasureItem(object sender, System.Windows.Forms.MeasureItemEventArgs e)
        {
        }
    }
}