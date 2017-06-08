namespace JLK.Catalog
{
    using System;

    public class GxObjectContainer : GxObject, IGxObjectContainer
    {
        protected IGxObjectArray m_GxObjectContainer = new GxObjectArray();

        public IGxObject AddChild(IGxObject igxObject_0)
        {
            bool flag = igxObject_0 is IGxDiskConnection;
            string strB = igxObject_0.Name.ToUpper();
            if (flag && (strB[0] == '\\'))
            {
                this.m_GxObjectContainer.Insert(-1, igxObject_0);
                return igxObject_0;
            }
            int num = 0;
            if (flag)
            {
                for (int i = 0; i < this.m_GxObjectContainer.Count; i++)
                {
                    IGxObject obj3 = this.m_GxObjectContainer.Item(i);
                    if (!(obj3 is IGxDiskConnection))
                    {
                        this.m_GxObjectContainer.Insert(i, igxObject_0);
                        return igxObject_0;
                    }
                    num = obj3.Name.ToUpper().CompareTo(strB);
                    if (num > 0)
                    {
                        this.m_GxObjectContainer.Insert(i, igxObject_0);
                        return igxObject_0;
                    }
                    if (num == 0)
                    {
                        return obj3;
                    }
                }
            }
            this.m_GxObjectContainer.Insert(-1, igxObject_0);
            return igxObject_0;
        }

        public void DeleteChild(IGxObject igxObject_0)
        {
            for (int i = 0; i < this.m_GxObjectContainer.Count; i++)
            {
                if (this.m_GxObjectContainer.Item(i) == igxObject_0)
                {
                    this.m_GxObjectContainer.Remove(i);
                    break;
                }
            }
        }

        protected virtual void OpenChild()
        {
        }

        public override void Refresh()
        {
            this.m_GxObjectContainer.Empty();
            base.Refresh();
        }

        public void SearchChildren(string string_0, IGxObjectArray igxObjectArray_0)
        {
        }

        public bool AreChildrenViewable
        {
            get
            {
                return true;
            }
        }

        public IEnumGxObject Children
        {
            get
            {
                if (this.m_GxObjectContainer.Count == 0)
                {
                    this.OpenChild();
                }
                return (this.m_GxObjectContainer as IEnumGxObject);
            }
        }

        public bool HasChildren
        {
            get
            {
                return true;
            }
        }
    }
}

