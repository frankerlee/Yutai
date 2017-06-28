namespace Yutai.Pipeline.Analysis.Classes
{
    public class PipeLine : GPoints
    {
        private string _datasetName;

        private int _id;

        private int _code;

        private string _material;

        private string _pipeWidthHeight;

        public int Red;

        public int Green;

        public int Blue;

        public string DatasetName
        {
            get
            {
                return this._datasetName;
            }
            set
            {
                this._datasetName = value;
            }
        }

        public int ID
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        public int Code
        {
            get
            {
                return this._code;
            }
            set
            {
                this._code = value;
            }
        }

        public string Material
        {
            get
            {
                return this._material;
            }
            set
            {
                this._material = value;
            }
        }

        public string PipeWidthHeight
        {
            get
            {
                return this._pipeWidthHeight;
            }
            set
            {
                this._pipeWidthHeight = value;
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
                _datasetName = this._datasetName,
                _code = this._code,
                _id = this._id,
                _material = this._material,
                _pipeWidthHeight = this._pipeWidthHeight,
                Red = this.Red,
                Green = this.Green,
                Blue = this.Blue
            };
        }
    }
}