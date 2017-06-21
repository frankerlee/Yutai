namespace Yutai.Pipeline.Analysis.Classes
{
    public class SortInfo
    {
        public int SmID;

        public int SmFNode;

        public int SmTNode;

        public bool bRightDirection;

        public SortInfo()
        {
        }

        public int GetPointInfoRleation(SortInfo psifSecond)
        {
            int num;
            if (this.SmFNode == psifSecond.SmFNode)
            {
                num = 11;
            }
            else if (this.SmFNode == psifSecond.SmTNode)
            {
                num = 12;
            }
            else if (this.SmTNode != psifSecond.SmTNode)
            {
                num = (this.SmTNode != psifSecond.SmFNode ? -1 : 21);
            }
            else
            {
                num = 22;
            }
            return num;
        }

        public void SwapFromWithTo()
        {
            int smFNode = this.SmFNode;
            this.SmFNode = this.SmTNode;
            this.SmTNode = smFNode;
        }
    }
}