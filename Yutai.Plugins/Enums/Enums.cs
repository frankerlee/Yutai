using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Plugins.Enums
{
    public enum RibbonItemType
    {
        TabItem = 0,
        ToolStrip = 1,
        Panel = 2,
        DropDown = 3,
        NormalItem=4,
        Tool=5
    }
    public enum MenuStyle
    {
        Ribbon = 0,
        Normal = 1
    }
    public enum CommandType
    {
        MapCommand = 0,
        SceneCommand = 1,
        AllCommand = 2,
        MapTool = 3,
        SceneTool = 4,
        OtherCommand = 5
    }
    public enum DataSourceType
    {
        Vector = 0,
        Raster = 1,
        All = 2,
        SpatiaLite = 3

    }
    public enum LayerType
    {
        Invalid = -1,
        Shapefile = 0,
        Image = 1,
        VectorLayer = 2,
        Grid = 3,
        WmsLayer = 4
    }

    public enum ProjectState
    {
        NotSaved = 0,
        HasChanges = 1,
        NoChanges = 2,
        Empty = 3,
    }

    public enum ConfigPageType
    {
        None = -1,
        General = 0,
        Map = 1,
        Plugins = 2,
        Projections = 3,
        Widgets = 4,
        Custom = 5,
        Raster = 6,
        Measuring = 7,
        Symbology = 8,
        Tiles = 9,
        Printing = 10,
        DataFormats = 11,
        Tools = 12,
        Identifier = 13,
        ShapeEditor = 14,
    }
    public enum MapViewStyle
    {
        View2D = 0,
        View3D = 1,
        ViewAll = 2
    }
    public enum DockPanelState
    {
        None = 0,
        Left = 1,
        Right = 2,
        Top = 3,
        Bottom = 4,
        Tabbed = 5,
        Fill = 6,
    }
    public enum MenuIndexType
    {
        MainMenu = 0,
        Toolbar = 1,
        StatusBar = 2,
    }
    public enum ToolbarDockState
    {
        //
        // Summary:
        //     The CommandBar is docked to the top border of the form.
        Top = 1,
        //
        // Summary:
        //     The CommandBar is docked to the bottom border of the form.
        Bottom = 2,
        //
        // Summary:
        //     The CommandBar is docked to the left border of the form.
        Left = 4,
        //
        // Summary:
        //     The CommandBar is docked to the right border of the form.
        Right = 8,
        //
        // Summary:
        //     The CommandBar is in a floating state.
        Float = 32,
    }

    public enum DefaultDockPanel
    {
        MapLegend = 0,
        Toolbox = 1,
        Locator = 2,

    }

    public enum DockPanels
    {
        MapLegend = 0,
        Preview = 1,
    }

}
