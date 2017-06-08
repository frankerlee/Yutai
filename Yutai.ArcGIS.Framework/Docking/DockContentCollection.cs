using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Yutai.ArcGIS.Framework.Docking
{
    public class DockContentCollection : ReadOnlyCollection<IDockContent>
    {
        private static List<IDockContent> _emptyList = new List<IDockContent>(0);
        private DockPane m_dockPane;

        internal DockContentCollection() : base(new List<IDockContent>())
        {
            this.m_dockPane = null;
        }

        internal DockContentCollection(DockPane pane) : base(_emptyList)
        {
            this.m_dockPane = null;
            this.m_dockPane = pane;
        }

        internal int Add(IDockContent content)
        {
            if (this.DockPane != null)
            {
                throw new InvalidOperationException();
            }
            if (this.Contains(content))
            {
                return this.IndexOf(content);
            }
            base.Items.Add(content);
            return (this.Count - 1);
        }

        internal void AddAt(IDockContent content, int index)
        {
            if (this.DockPane != null)
            {
                throw new InvalidOperationException();
            }
            if (((index >= 0) && (index <= (base.Items.Count - 1))) && !this.Contains(content))
            {
                base.Items.Insert(index, content);
            }
        }

        public bool Contains(IDockContent content)
        {
            if (this.DockPane == null)
            {
                return base.Items.Contains(content);
            }
            return (this.GetIndexOfVisibleContents(content) != -1);
        }

        private int GetIndexOfVisibleContents(IDockContent content)
        {
            if (this.DockPane == null)
            {
                throw new InvalidOperationException();
            }
            if (content != null)
            {
                int num = -1;
                foreach (IDockContent content2 in this.DockPane.Contents)
                {
                    if (content2.DockHandler.DockState == this.DockPane.DockState)
                    {
                        num++;
                        if (content2 == content)
                        {
                            return num;
                        }
                    }
                }
            }
            return -1;
        }

        private IDockContent GetVisibleContent(int index)
        {
            if (this.DockPane == null)
            {
                throw new InvalidOperationException();
            }
            int num = -1;
            foreach (IDockContent content in this.DockPane.Contents)
            {
                if (content.DockHandler.DockState == this.DockPane.DockState)
                {
                    num++;
                }
                if (num == index)
                {
                    return content;
                }
            }
            throw new ArgumentOutOfRangeException();
        }

        public int IndexOf(IDockContent content)
        {
            if (this.DockPane == null)
            {
                if (!this.Contains(content))
                {
                    return -1;
                }
                return base.Items.IndexOf(content);
            }
            return this.GetIndexOfVisibleContents(content);
        }

        internal void Remove(IDockContent content)
        {
            if (this.DockPane != null)
            {
                throw new InvalidOperationException();
            }
            if (this.Contains(content))
            {
                base.Items.Remove(content);
            }
        }

        public int Count
        {
            get
            {
                if (this.DockPane == null)
                {
                    return base.Count;
                }
                return this.CountOfVisibleContents;
            }
        }

        private int CountOfVisibleContents
        {
            get
            {
                if (this.DockPane == null)
                {
                    throw new InvalidOperationException();
                }
                int num = 0;
                foreach (IDockContent content in this.DockPane.Contents)
                {
                    if (content.DockHandler.DockState == this.DockPane.DockState)
                    {
                        num++;
                    }
                }
                return num;
            }
        }

        private DockPane DockPane
        {
            get
            {
                return this.m_dockPane;
            }
        }

        public IDockContent this[int index]
        {
            get
            {
                if (this.DockPane == null)
                {
                    return base.Items[index];
                }
                return this.GetVisibleContent(index);
            }
        }
    }
}

