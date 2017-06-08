namespace Yutai.ArcGIS.Controls.Controls.TOCDisplay
{
    internal class TOCTreeNodeEx : TOCTreeNode
    {
        public int OID;

        public TOCTreeNodeEx()
        {
            this.OID = 0;
        }

        public TOCTreeNodeEx(string name) : base(name)
        {
            this.OID = 0;
        }

        public TOCTreeNodeEx(string name, bool HasCheck, bool HasIamge) : base(name, HasCheck, HasIamge)
        {
            this.OID = 0;
        }
    }
}

