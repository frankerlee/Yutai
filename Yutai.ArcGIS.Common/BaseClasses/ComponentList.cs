using System.Collections;
using System.Xml;

namespace Yutai.ArcGIS.Common.BaseClasses
{
    public class ComponentList
    {
        private ArrayList arrayList_0;
        private ArrayList arrayList_1;
        private ArrayList arrayList_2;
        private string string_0;
        private string string_1;
        private string string_2;
        private XmlTextReader xmlTextReader_0;

        public ComponentList()
        {
        }

        public ComponentList(string string_3)
        {
            this.string_0 = string_3;
            this.string_1 = "";
            this.string_2 = "";
            this.arrayList_0 = new ArrayList();
            this.arrayList_1 = new ArrayList();
            this.arrayList_2 = new ArrayList();
            this.xmlTextReader_0 = new XmlTextReader(string_3);
        }

        public void beginRead()
        {
            bool flag = false;
            bool flag2 = false;
            int num = -1;
            while (this.xmlTextReader_0.Read())
            {
                if (this.xmlTextReader_0.Name == "Component")
                {
                    this.xmlTextReader_0.MoveToFirstAttribute();
                    if (this.xmlTextReader_0.Name == "ComponentName")
                    {
                        this.string_2 = this.xmlTextReader_0.Value;
                        flag = true;
                    }
                    while (this.xmlTextReader_0.MoveToNextAttribute())
                    {
                        if (this.xmlTextReader_0.Name == "ComponentFileName")
                        {
                            this.string_1 = this.xmlTextReader_0.Value;
                            flag2 = true;
                        }
                        else if (this.xmlTextReader_0.Name == "CommandSubType")
                        {
                            num = int.Parse(this.xmlTextReader_0.Value);
                        }
                    }
                    if (flag && flag2)
                    {
                        this.arrayList_0.Add(this.string_2);
                        this.arrayList_1.Add(this.string_1);
                        this.arrayList_2.Add(num);
                    }
                    num = -1;
                    flag = false;
                    flag2 = false;
                }
            }
        }

        public string getClassName(int int_0)
        {
            return this.arrayList_0[int_0].ToString();
        }

        public int GetComponentCount()
        {
            return this.arrayList_1.Count;
        }

        public string getfilename(int int_0)
        {
            return this.arrayList_1[int_0].ToString();
        }

        public int getSubType(int int_0)
        {
            return (int) this.arrayList_2[int_0];
        }

        public string GfilenameFormClassName(string string_3)
        {
            string str = "";
            for (int i = 0; i < this.GetComponentCount(); i++)
            {
                if (string_3 == this.getClassName(i))
                {
                    str = this.getfilename(i);
                }
            }
            return str;
        }

        public string FileName
        {
            get
            {
                return this.string_0;
            }
        }
    }
}

