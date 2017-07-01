namespace Yutai.ArcGIS.Common
{
    public class Staff
    {
        private string string_0 = "";
        private string string_1 = "";
        private string string_2 = "";
        private string string_3 = "";

        public override string ToString()
        {
            return this.RealName;
        }

        public string LoginName
        {
            get { return this.string_1; }
            set { this.string_1 = value; }
        }

        public string Password
        {
            get { return this.string_3; }
            set { this.string_3 = value; }
        }

        public string RealName
        {
            get { return this.string_2; }
            set { this.string_2 = value; }
        }

        public string StaffID
        {
            get { return this.string_0; }
            set { this.string_0 = value; }
        }
    }
}