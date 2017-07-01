using System.Data;

namespace Yutai.ArcGIS.Common.Data
{
    public interface ISQLTable
    {
        DataTable ExecuteDataTable(string string_0);

        DataSet ExecuteKDDataSet(string string_0);
    }
}