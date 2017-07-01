using System.Collections.Generic;

namespace Yutai.ArcGIS.Common.Editor
{
    public class EditTemplateSchems
    {
        private List<string> list_0 = new List<string>();

        private List<EditTemplateSchem> list_1 = new List<EditTemplateSchem>();

        public int Count
        {
            get { return this.list_1.Count; }
        }

        public EditTemplateSchem this[int int_0]
        {
            get
            {
                EditTemplateSchem item;
                if ((int_0 >= this.Count ? true : int_0 < 0))
                {
                    item = null;
                }
                else
                {
                    item = this.list_1[int_0];
                }
                return item;
            }
        }

        public EditTemplateSchems()
        {
        }

        public void Add(EditTemplateSchem editTemplateSchem_0)
        {
            this.list_1.Add(editTemplateSchem_0);
        }

        public void AddField(string string_0)
        {
            this.list_0.Add(string_0);
        }

        public bool HasField(string string_0)
        {
            bool flag;
            foreach (string list0 in this.list_0)
            {
                if (list0 != string_0)
                {
                    continue;
                }
                flag = true;
                return flag;
            }
            flag = false;
            return flag;
        }
    }
}