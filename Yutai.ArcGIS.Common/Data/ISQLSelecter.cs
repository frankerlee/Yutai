namespace Yutai.ArcGIS.Common.Data
{
    public interface ISQLSelecter
    {
        string SQL { get; set; }

        void Dispose();

        void Execute();

        string getFieldValue(string string_0);
    }
}