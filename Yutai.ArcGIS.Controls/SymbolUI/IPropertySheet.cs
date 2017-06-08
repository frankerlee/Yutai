namespace Yutai.ArcGIS.Controls.SymbolUI
{
    public interface IPropertySheet
    {
        void AddPage(object Page);
        bool EditProperties(object @object);

        short ActivePage { get; set; }

        bool HideApplyButton { get; set; }

        string Title { get; set; }
    }
}

