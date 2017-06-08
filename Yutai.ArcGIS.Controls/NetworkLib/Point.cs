namespace Yutai.ArcGIS.Controls.NetworkLib
{
    internal class Point
    {
        private double m_m;
        private double m_x;
        private double m_y;
        private double m_z;

        public Point()
        {
            this.m_x = double.NaN;
            this.m_y = double.NaN;
            this.m_z = double.NaN;
            this.m_m = double.NaN;
        }

        public Point(double x, double y)
        {
            this.m_x = double.NaN;
            this.m_y = double.NaN;
            this.m_z = double.NaN;
            this.m_m = double.NaN;
            this.X = x;
            this.Y = y;
        }

        public Point(double x, double y, double z, double m)
        {
            this.m_x = double.NaN;
            this.m_y = double.NaN;
            this.m_z = double.NaN;
            this.m_m = double.NaN;
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.M = m;
        }

        public double M
        {
            get
            {
                return this.m_m;
            }
            set
            {
                this.m_m = value;
            }
        }

        public double X
        {
            get
            {
                return this.m_x;
            }
            set
            {
                this.m_x = value;
            }
        }

        public double Y
        {
            get
            {
                return this.m_y;
            }
            set
            {
                this.m_y = value;
            }
        }

        public double Z
        {
            get
            {
                return this.m_z;
            }
            set
            {
                this.m_z = value;
            }
        }
    }
}

