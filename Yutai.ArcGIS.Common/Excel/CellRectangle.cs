using System.Runtime.InteropServices;

namespace Yutai.ArcGIS.Common.Excel
{
    [StructLayout(LayoutKind.Sequential)]
    public struct CellRectangle
    {
        private int int_0;
        private int int_1;
        private int int_2;
        private int int_3;

        public CellRectangle(int int_4, int int_5, int int_6, int int_7)
        {
            this.int_0 = int_4;
            this.int_1 = int_5;
            this.int_2 = int_6;
            this.int_3 = int_7;
        }

        public int Left
        {
            get { return this.int_0; }
            set { this.int_0 = value; }
        }

        public int Top
        {
            get { return this.int_1; }
            set { this.int_1 = value; }
        }

        public int Width
        {
            get { return this.int_2; }
            set { this.int_2 = value; }
        }

        public int Height
        {
            get { return this.int_3; }
            set { this.int_3 = value; }
        }
    }
}