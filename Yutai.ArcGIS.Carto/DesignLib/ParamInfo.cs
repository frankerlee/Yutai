namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class ParamInfo
    {
        private int int_0 = -1;
        private string string_0 = "";
        private string string_1 = "";
        private string string_2 = "";

        public string Caption
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

        public string Description
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

        public string Name
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

        public int TemplateOID
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
    }
}

