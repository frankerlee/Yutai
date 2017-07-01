using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Common.ControlExtend
{
    public class EXBoolListViewSubItem : EXListViewSubItemAB
    {
        private bool bool_0;

        public EXBoolListViewSubItem()
        {
        }

        public EXBoolListViewSubItem(bool bool_1)
        {
            this.bool_0 = bool_1;
            base.MyValue = bool_1.ToString();
        }

        public override int DoDraw(DrawListViewSubItemEventArgs drawListViewSubItemEventArgs_0, int int_0,
            EXColumnHeader excolumnHeader_0)
        {
            Image trueImage;
            EXBoolColumnHeader header = (EXBoolColumnHeader) excolumnHeader_0;
            if (this.BoolValue)
            {
                trueImage = header.TrueImage;
            }
            else
            {
                trueImage = header.FalseImage;
            }
            int y = (drawListViewSubItemEventArgs_0.Bounds.Y + (drawListViewSubItemEventArgs_0.Bounds.Height/2)) -
                    (trueImage.Height/2);
            drawListViewSubItemEventArgs_0.Graphics.DrawImage(trueImage, int_0, y, trueImage.Width, trueImage.Height);
            int_0 += trueImage.Width + 2;
            return int_0;
        }

        public bool BoolValue
        {
            get { return this.bool_0; }
            set
            {
                this.bool_0 = value;
                base.MyValue = value.ToString();
            }
        }
    }
}