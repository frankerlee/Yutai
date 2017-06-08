using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;

namespace Yutai.ArcGIS.Controls.Controls.TOCDisplay
{
    public abstract class TOCNodePopmenuItemOld : ESRI.ArcGIS.ADF.BaseClasses.BaseCommand, ITOCNodePopmenuItemEx
    {
        protected IApplication m_pApp = null;

        protected TOCNodePopmenuItemOld()
        {
        }

        protected void DocumentChanged(object hook)
        {
            if (hook != null)
            {
                DocumentManager.DocumentChanged(hook);
            }
        }

        protected TOCTreeNode FindMapNodeByNode(TOCTreeNode pTOCNode)
        {
            if ((pTOCNode.TOCNodeType == NodeType.Map) || (pTOCNode.TOCNodeType == NodeType.MapFrame))
            {
                return pTOCNode;
            }
            if (pTOCNode.Parent != null)
            {
                if ((pTOCNode.Parent.TOCNodeType == NodeType.Map) || (pTOCNode.Parent.TOCNodeType == NodeType.MapFrame))
                {
                    return pTOCNode.Parent;
                }
                return this.FindMapNodeByNode(pTOCNode.Parent);
            }
            return null;
        }

        public override void OnCreate(object hook)
        {
            this.m_pApp = hook as IApplication;
        }

        protected IBasicMap FocusMap
        {
            get
            {
                if (this.TreeBase != null)
                {
                    return this.TreeBase.m_FocusMap;
                }
                if (ApplicationRef.Application != null)
                {
                    return (ApplicationRef.Application.FocusMap as IBasicMap);
                }
                return null;
            }
        }

        public IMapControl2 InMapCtrl { get; set; }

        public virtual bool IsShow
        {
            get
            {
                if (this.TreeView != null)
                {
                    if (this.TreeView.SelectedNodes.Count > 1)
                    {
                        return false;
                    }
                    if (this.TreeView.SelectedNode == null)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public IPageLayoutControl2 PageLayoutControl { get; set; }

        public TreeViewWrapBase TreeBase { get; set; }

        public TOCTreeView TreeView { get; set; }
    }
}

