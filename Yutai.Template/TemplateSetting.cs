using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Plugins.Template
{
    public class TemplateSetting
    {
        private string _templateDatabaseName;

        public string TemplateDatabaseName
        {
            get
            {
                return _templateDatabaseName;
            }

            set
            {
                _templateDatabaseName = value;
            }
        }
    }
}
