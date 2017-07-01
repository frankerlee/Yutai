namespace Yutai.ArcGIS.Common.Excel
{
    public interface IDraw
    {
        void Draw();

        System.Drawing.Graphics Graphics { get; set; }

        System.Drawing.Rectangle Rectangle { get; set; }
    }
}