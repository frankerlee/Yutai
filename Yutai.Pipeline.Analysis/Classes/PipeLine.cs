namespace Yutai.Pipeline.Analysis.Classes
{
    public class PipeLine : GPoints
    {
        private string string_0;

        private int int_0;

        private int int_1;

        private string string_1;

        private string string_2;

        public int Red;

        public int Green;

        public int Blue;

        public string DatasetName
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }

        public int ID
        {
            get
            {
                return this.int_0;
            }
            set
            {
                this.int_0 = value;
            }
        }

        public int Code
        {
            get
            {
                return this.int_1;
            }
            set
            {
                this.int_1 = value;
            }
        }

        public string Material
        {
            get
            {
                return this.string_1;
            }
            set
            {
                this.string_1 = value;
            }
        }

        public string PipeWidthHeight
        {
            get
            {
                return this.string_2;
            }
            set
            {
                this.string_2 = value;
            }
        }

        public new float Length
        {
            get
            {
                int count = this.m_vtPoints.Count;
                float num = 0f;
                for (int i = 0; i < count - 1; i++)
                {
                    GPoint gPoint = (GPoint)this.m_vtPoints[i];
                    GPoint toPoint = (GPoint)this.m_vtPoints[i + 1];
                    float num2 = (float)gPoint.DistanceToPt(toPoint);
                    num += num2;
                }
                return num;
            }
        }

        public new PipeLine GetDeepCopy()
        {
            return new PipeLine
            {
                m_vtPoints = base.GetDeepCopyOfVePoints(),
                string_0 = this.string_0,
                int_1 = this.int_1,
                int_0 = this.int_0,
                string_1 = this.string_1,
                string_2 = this.string_2,
                Red = this.Red,
                Green = this.Green,
                Blue = this.Blue
            };
        }
    }
}