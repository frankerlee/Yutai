namespace Yutai.ArcGIS.Common.BaseClasses
{
    public interface IPropertyPage
    {
        // Methods
        void Apply();
        void Cancel();
        void Hide();
        void ResetControl();
        void SetObjects(object object_0);

        // Properties
        int Height { get; }
        bool IsPageDirty { get; }
        string Title { get; set; }
        int Width { get; }
    }
    public interface IPropertyPageEvents
    {
        // Events
        event OnValueChangeEventHandler OnValueChange;
    }

    public interface IPropertySheet
    {
        // Methods
        void AddPage(object object_0);
        bool EditProperties(object object_0);

        // Properties
        short ActivePage { get; set; }
        bool HideApplyButton { get; set; }
        string Title { get; set; }
    }

    public interface IPropertySheetEvents
    {
        // Events
        event OnApplyEventHandler OnApply;
    }

    public delegate void OnApplyEventHandler();
    public delegate void OnValueChangeEventHandler();
}