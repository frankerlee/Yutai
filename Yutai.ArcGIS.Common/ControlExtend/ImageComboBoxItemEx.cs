using System.ComponentModel;
using DevExpress.XtraEditors.Controls;

namespace Yutai.ArcGIS.Common.ControlExtend
{
    [TypeConverter("JLK.Editors.Design.ImageComboBoxItemTypeConverter, JLK.Editors.Design")]
    public class ImageComboBoxItemEx : ImageComboBoxItem
    {
        private int int_0;
        private object object_0;

        public ImageComboBoxItemEx()
        {
            this.object_0 = null;
            this.int_0 = 0;
        }

        public ImageComboBoxItemEx(int int_1, int int_2) : base(int_1)
        {
            this.object_0 = null;
            this.int_0 = int_2;
        }

        public ImageComboBoxItemEx(object object_1, int int_1) : base(object_1, -1)
        {
            this.object_0 = null;
            this.int_0 = int_1;
        }

        public ImageComboBoxItemEx([Localizable(true)] string string_0, int int_1) : base(string_0)
        {
            this.object_0 = null;
            this.int_0 = int_1;
        }

        public ImageComboBoxItemEx(object object_1, int int_1, int int_2) : base(object_1, int_1)
        {
            this.object_0 = null;
            this.int_0 = int_2;
        }

        public ImageComboBoxItemEx([Localizable(true)] string string_0, int int_1, int int_2) : base(string_0, int_1)
        {
            this.object_0 = null;
            this.int_0 = int_2;
        }

        public ImageComboBoxItemEx([Localizable(true)] string string_0, object object_1, int int_1) : base(string_0, object_1)
        {
            this.object_0 = null;
            this.int_0 = int_1;
        }

        public ImageComboBoxItemEx([Localizable(true)] string string_0, object object_1, int int_1, int int_2) : base(string_0, object_1, int_1)
        {
            this.object_0 = null;
            this.int_0 = int_2;
        }

        public override void Assign(ImageComboBoxItem imageComboBoxItem_0)
        {
            if (imageComboBoxItem_0 != null)
            {
                base.Description = imageComboBoxItem_0.Description;
                this.Value = imageComboBoxItem_0.Value;
                base.ImageIndex = imageComboBoxItem_0.ImageIndex;
                if (imageComboBoxItem_0 is ImageComboBoxItemEx)
                {
                    this.int_0 = ((ImageComboBoxItemEx) imageComboBoxItem_0).Degree;
                    this.object_0 = ((ImageComboBoxItemEx) imageComboBoxItem_0).Tag;
                }
                else
                {
                    this.int_0 = 0;
                    this.object_0 = null;
                }
            }
        }

        public override string ToString()
        {
            return (base.Description + "_" + this.int_0.ToString());
        }

        public int Degree
        {
            get
            {
                return this.int_0;
            }
            set
            {
                this.int_0 = value;
            }
        }

        public object Tag
        {
            get
            {
                return this.object_0;
            }
            set
            {
                this.object_0 = value;
            }
        }
    }
}

