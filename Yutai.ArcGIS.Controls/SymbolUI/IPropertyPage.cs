namespace Yutai.ArcGIS.Controls.SymbolUI
{
    public interface IPropertyPage
    {
        void Apply();
        void Cancel();
        void Hide();
        void SetObjects(object @object);

        int Height { get; }

        bool IsPageDirty { get; }

        string Title { get; set; }

        int Width { get; }
    }
}