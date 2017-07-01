namespace Yutai.ArcGIS.Common.Geodatabase
{
    public interface IObjectProvider
    {
        int Count { get; }

        object GetObj(int int_0);
    }
}