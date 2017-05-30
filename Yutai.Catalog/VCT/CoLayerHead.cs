namespace Yutai.Catalog.VCT
{
    using System;

    public class CoLayerHead : ICoClone
    {
        private DateTime dateTime_0 = DateTime.Now;
        private ICoPoint icoPoint_0 = new CoPointClass(0.0, 0.0, 0.0);
        private ICoPoint icoPoint_1 = new CoPointClass(0.0, 0.0, 0.0);
        private int int_0 = 0;
        private int int_1 = 0;
        private int int_2 = 0;
        private int int_3 = 0;
        private string string_0 = string.Empty;
        private string string_1 = string.Empty;
        private string string_2 = string.Empty;
        private string string_3 = string.Empty;
        private string string_4 = string.Empty;
        private string string_5 = "1.0";
        private string string_6 = string.Empty;

        public virtual object Clone()
        {
            return new CoLayerHead { Coordinate = this.Coordinate, Datamark = this.Datamark, Date = this.Date, Dim = this.Dim, MaxPoint = this.MaxPoint, Meridian = this.Meridian, MinPoint = this.MinPoint, Parameters = this.Parameters, Projection = this.Projection, ScaleM = this.ScaleM, Spheroid = this.Spheroid, Topo = this.Topo, Unit = this.Unit, Version = this.Version };
        }

        public string Coordinate
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

        public string Datamark
        {
            get
            {
                return this.string_6;
            }
            set
            {
                this.string_6 = value;
            }
        }

        public DateTime Date
        {
            get
            {
                return this.dateTime_0;
            }
            set
            {
                this.dateTime_0 = value;
            }
        }

        public int Dim
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

        public ICoPoint MaxPoint
        {
            get
            {
                return this.icoPoint_1;
            }
            set
            {
                this.icoPoint_1 = value;
            }
        }

        public int Meridian
        {
            get
            {
                return this.int_2;
            }
            set
            {
                this.int_2 = value;
            }
        }

        public ICoPoint MinPoint
        {
            get
            {
                return this.icoPoint_0;
            }
            set
            {
                this.icoPoint_0 = value;
            }
        }

        public string Parameters
        {
            get
            {
                return this.string_4;
            }
            set
            {
                this.string_4 = value;
            }
        }

        public string Projection
        {
            get
            {
                return this.string_3;
            }
            set
            {
                this.string_3 = value;
            }
        }

        public int ScaleM
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

        public string Spheroid
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

        public int Topo
        {
            get
            {
                return this.int_3;
            }
            set
            {
                this.int_3 = value;
            }
        }

        public string Unit
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

        public string Version
        {
            get
            {
                return this.string_5;
            }
            set
            {
                this.string_5 = value;
            }
        }
    }
}

