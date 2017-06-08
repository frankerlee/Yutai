using System.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;

namespace Yutai.ArcGIS.Common.ControlExtend
{
    public class RepositoryTreeviewComboxEx : RepositoryItemComboBox
    {
        private int int_0 = 3;

        public RepositoryTreeviewComboxEx()
        {
            base.DrawItem += new ListBoxDrawItemEventHandler(this.RepositoryTreeviewComboxEx_DrawItem);
        }

        private void RepositoryTreeviewComboxEx_DrawItem(object sender, ListBoxDrawItemEventArgs e)
        {
            Brush brush = null;
            ImageComboBoxItemEx item = e.Item as ImageComboBoxItemEx;
            RectangleF layoutRectangle = RectangleF.Inflate(e.Bounds, -2f, -1f);
            layoutRectangle.X += item.Degree * this.int_0;
            Font font = new Font("宋体", 8f);
            e.Graphics.DrawString(e.Item.ToString() + "_MyTest", font, brush, layoutRectangle);
            e.Handled = true;
        }
    }
}

