using System.Drawing;

namespace Yutai.ArcGIS.Common.Excel
{
    public class Body : Outer
    {
        public Body()
        {
            this.IsDrawAllPage = false;
            base.mdrawGrid.AlignMent = AlignFlag.Left;
            base.mdrawGrid.Border = GridBorderFlag.Single;
            base.mdrawGrid.Line = GridLineFlag.Both;
            base.IsAverageColsWidth = false;
            base.mdrawGrid.Merge = GridMergeFlag.None;
            base.mdrawGrid.Font = new System.Drawing.Font("宋体", 12f);
            base.mdrawGrid.PreferredRowHeight = base.mdrawGrid.Font.Height + 2;
        }

        public Body(int int_9, int int_10) : this()
        {
            base.Initialize(int_9, int_10);
        }

        public string ColsAlignString
        {
            get { return base.mdrawGrid.ColsAlignString; }
            set { base.mdrawGrid.ColsAlignString = value; }
        }

        public override Font Font
        {
            get { return base.mdrawGrid.Font; }
            set { base.mdrawGrid.Font = value; }
        }

        public string[,] GridText
        {
            get { return base.mdrawGrid.GridText; }
            set
            {
                base.mblnHadInitialized = true;
                base.mdrawGrid.GridText = value;
            }
        }
    }
}