namespace JLK.Catalog.VCT
{
    using System;

    public class CoPointClass : ICoPoint
    {
        private double double_0;
        private double double_1;
        private double double_2;

        public CoPointClass()
        {
            this.double_0 = 0.0;
            this.double_1 = 0.0;
            this.double_2 = 0.0;
        }

        public CoPointClass(double double_3, double double_4)
        {
            this.double_0 = 0.0;
            this.double_1 = 0.0;
            this.double_2 = 0.0;
            this.double_0 = double_3;
            this.double_1 = double_4;
        }

        public CoPointClass(double double_3, double double_4, double double_5) : this(double_3, double_4)
        {
            this.double_2 = double_5;
        }

        ~CoPointClass()
        {
        }

        public override string ToString()
        {
            return string.Format("{0},{1},{2}", this.double_0, this.double_1, this.double_2);
        }

        public double X
        {
            get
            {
                return this.double_0;
            }
            set
            {
                this.double_0 = value;
            }
        }

        public double Y
        {
            get
            {
                return this.double_1;
            }
            set
            {
                this.double_1 = value;
            }
        }

        public double Z
        {
            get
            {
                return this.double_2;
            }
            set
            {
                this.double_2 = value;
            }
        }
    }
}

