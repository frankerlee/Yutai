using System;
using System.Collections;

namespace Yutai.ArcGIS.Controls.Controls.TOCDisplay
{
    public class TOCTreeNodeCollection : IList, ICollection, IEnumerable
    {
        private bool m_CanDrag = true;
        private object m_Owner = null;
        private TOCTreeNode m_pNode = null;
        private IList m_pTOCTreeNodeCollection = new ArrayList();
        private TocTreeViewBase m_pTreeView = null;

        public virtual int Add(TOCTreeNode pNode)
        {
            if (this.m_pNode != null)
            {
                pNode.SetParent(this.m_pNode);
                pNode.SetTreeView(this.m_pNode.TreeView);
            }
            else
            {
                pNode.SetTreeView(this.m_pTreeView);
            }
            return this.m_pTOCTreeNodeCollection.Add(pNode);
        }

        public int Add(object value)
        {
            return this.m_pTOCTreeNodeCollection.Add(value);
        }

        public virtual TOCTreeNode Add(string name)
        {
            TOCTreeNode node = new TOCTreeNode(name);
            if (this.m_pNode != null)
            {
                node.SetParent(this.m_pNode);
                node.SetTreeView(this.m_pNode.TreeView);
            }
            else
            {
                node.SetTreeView(this.m_pTreeView);
            }
            this.m_pTOCTreeNodeCollection.Add(node);
            return node;
        }

        public void Clear()
        {
            this.m_pTOCTreeNodeCollection.Clear();
        }

        public bool Contains(object value)
        {
            return this.m_pTOCTreeNodeCollection.Contains(value);
        }

        public void CopyTo(Array array, int index)
        {
            this.m_pTOCTreeNodeCollection.CopyTo(array, index);
        }

        public IEnumerator GetEnumerator()
        {
            return this.m_pTOCTreeNodeCollection.GetEnumerator();
        }

        public int IndexOf(object value)
        {
            return this.m_pTOCTreeNodeCollection.IndexOf(value);
        }

        public virtual void Insert(int index, TOCTreeNode node)
        {
            if (this.m_pNode != null)
            {
                node.SetParent(this.m_pNode);
                node.SetTreeView(this.m_pNode.TreeView);
            }
            else
            {
                node.SetTreeView(this.m_pTreeView);
            }
            this.m_pTOCTreeNodeCollection.Insert(index, node);
        }

        public void Insert(int index, object value)
        {
            (value as TOCTreeNode).SetParent(this.m_pNode);
            (value as TOCTreeNode).SetTreeView(this.m_pTreeView);
            this.m_pTOCTreeNodeCollection.Insert(index, value);
        }

        internal void InteralInsert(int index, TOCTreeNode node)
        {
            this.m_pTOCTreeNodeCollection.Insert(index, node);
        }

        internal virtual int InternalAdd(TOCTreeNode pNode)
        {
            return this.m_pTOCTreeNodeCollection.Add(pNode);
        }

        public void Remove(TOCTreeNode pNode)
        {
            this.m_pTOCTreeNodeCollection.Remove(pNode);
        }

        public void Remove(object value)
        {
            (value as TOCTreeNode).SetParent(null);
            (value as TOCTreeNode).SetTreeView(null);
            this.m_pTOCTreeNodeCollection.Remove(value);
        }

        public void RemoveAt(int index)
        {
            this.m_pTOCTreeNodeCollection.RemoveAt(index);
        }

        public bool CanDrag
        {
            get
            {
                return this.m_CanDrag;
            }
            set
            {
                this.m_CanDrag = value;
            }
        }

        public int Count
        {
            get
            {
                return this.m_pTOCTreeNodeCollection.Count;
            }
        }

        public bool IsFixedSize
        {
            get
            {
                return this.m_pTOCTreeNodeCollection.IsFixedSize;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return this.m_pTOCTreeNodeCollection.IsReadOnly;
            }
        }

        public bool IsSynchronized
        {
            get
            {
                return this.m_pTOCTreeNodeCollection.IsSynchronized;
            }
        }

        public object this[int index]
        {
            get
            {
                return this.m_pTOCTreeNodeCollection[index];
            }
            set
            {
                this.m_pTOCTreeNodeCollection[index] = value;
            }
        }

        internal object Owner
        {
            set
            {
                this.m_Owner = value;
                if (this.m_Owner is TocTreeViewBase)
                {
                    this.m_pTreeView = this.m_Owner as TocTreeViewBase;
                }
                else if (this.m_Owner is TOCTreeNode)
                {
                    this.m_pNode = this.m_Owner as TOCTreeNode;
                    this.m_pTreeView = this.m_pNode.TreeView;
                }
            }
        }

        public object SyncRoot
        {
            get
            {
                return this.m_pTOCTreeNodeCollection.SyncRoot;
            }
        }

        internal TocTreeViewBase TreeView
        {
            set
            {
                this.m_pTreeView = value;
            }
        }
    }
}

