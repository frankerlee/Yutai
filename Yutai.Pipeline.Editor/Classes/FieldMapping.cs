using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.Pipeline.Editor.Classes
{
    public class FieldMapping
    {
        public string Name { get; set; }
        public string AliasName { get; set; }
        public esriFieldType Type { get; set; }
        public int Length { get; set; }
        public int Precision { get; set; }
        public string DomainName { get; set; }

        public string OriginFieldName { get; set; }

        public int IndexOriginField { get; set; }
        public int IndexTargetField { get; set; }

        public int TargetFieldIndex { get; set; }
        public int SourceFieldIndex { get; set; }
        public string TargetFieldName { get; set; }
        public string SourceFieldName { get; set; }
        public IField TargetField { get; set; }
        public IField SourceField { get; set; }
    }
}
