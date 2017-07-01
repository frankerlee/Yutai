namespace Yutai.ArcGIS.Common.Data
{
    internal struct DatabaseProperty
    {
        private string string_0;

        private DatabaseType databaseType_0;

        private string string_1;

        private string string_2;

        private string string_3;

        private string string_4;

        public string Database
        {
            get { return this.string_4; }
            set { this.string_4 = value; }
        }

        public DatabaseType Instance
        {
            get { return this.databaseType_0; }
            set { this.databaseType_0 = value; }
        }

        public string Password
        {
            get { return this.string_2; }
            set { this.string_2 = value; }
        }

        public string Server
        {
            get { return this.string_0; }
            set { this.string_0 = value; }
        }

        public string User
        {
            get { return this.string_1; }
            set { this.string_1 = value; }
        }

        public string Version
        {
            get { return this.string_3; }
            set { this.string_3 = value; }
        }
    }
}