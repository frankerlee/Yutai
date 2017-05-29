namespace Yutai.Controls
{
    public enum MapLegendCommand
    {
        ZoomToLayer = 0,
        SaveStyle = 1,
        LoadStyle = 2,
        RemoveLayer = 3,
        OpenFileLocation = 4,
        Properties = 5,
        GroupProperties = 6,
        RemoveGroup = 7,
        ZoomToGroup = 8,
        AddGroup = 9,
        AddLayer = 10,
        Labels = 11,
        TableEditor = 12,
    }

    public enum OverviewCommand
    {
        FullExtent,
        Current,
        Properties
    }
}