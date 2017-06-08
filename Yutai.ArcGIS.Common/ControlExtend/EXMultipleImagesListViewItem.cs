using System.Collections;

namespace Yutai.ArcGIS.Common.ControlExtend
{
    public class EXMultipleImagesListViewItem : EXListViewItem
    {
        private ArrayList arrayList_0;

        public EXMultipleImagesListViewItem()
        {
        }

        public EXMultipleImagesListViewItem(ArrayList arrayList_1)
        {
            this.arrayList_0 = arrayList_1;
        }

        public EXMultipleImagesListViewItem(string string_1)
        {
            base.Text = string_1;
        }

        public EXMultipleImagesListViewItem(string string_1, ArrayList arrayList_1)
        {
            base.Text = string_1;
            this.arrayList_0 = arrayList_1;
        }

        public EXMultipleImagesListViewItem(string string_1, ArrayList arrayList_1, string string_2)
        {
            base.Text = string_1;
            this.arrayList_0 = arrayList_1;
            base.MyValue = string_2;
        }

        public ArrayList MyImages
        {
            get
            {
                return this.arrayList_0;
            }
            set
            {
                this.arrayList_0 = value;
            }
        }
    }
}

