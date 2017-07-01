using System.Runtime.InteropServices;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Common.ExtendClass
{
    [Guid("40C9ECA9-579C-44ac-9DE7-DB35AE79EC04")]
    public interface IJLKCodeValueDomain
    {
        string NameFieldName { get; set; }

        string TableName { get; set; }

        string ValueFieldName { get; set; }

        IWorkspace Workspace { get; set; }
    }
}