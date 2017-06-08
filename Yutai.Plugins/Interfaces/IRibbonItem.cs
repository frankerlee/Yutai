using System.Drawing;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;

namespace Yutai.Plugins.Interfaces
{
    public interface IRibbonItem
    {
        int Position { get; set; }
        string ParentName { get; set; }
        string Name { get;  }
        string Key { get;  }
        string Caption { get;  }
        Bitmap Image { get;  }
        string Tooltip { get; }
        string Category { get; }
        RibbonItemType ItemType { get; set; }
        bool Checked { get; }
        bool Enabled { get; }
        PluginIdentity PluginIdentity { get; }
     
        TextImageRelationYT TextImageRelationYT { get; set; }
        DisplayStyleYT DisplayStyleYT { get; set; }

        ToolStripItemImageScalingYT ToolStripItemImageScalingYT { get; set; }

        ToolStripLayoutStyleYT ToolStripLayoutStyleYT { get; set; }

        int PanelRowCount { get; set; }
        //对ToolStripEx起作用
        bool IsGroup { get; set; }
        bool NeedUpdateEvent { get; set; }
        string Message { get;  }
    }
}