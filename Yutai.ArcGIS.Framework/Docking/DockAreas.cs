using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace Yutai.ArcGIS.Framework.Docking
{
    [Serializable, Flags, Editor(typeof(DockAreasEditor), typeof(UITypeEditor))]
    public enum DockAreas
    {
        DockBottom = 0x10,
        DockLeft = 2,
        DockRight = 4,
        DockTop = 8,
        Document = 0x20,
        Float = 1
    }
}

