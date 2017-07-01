using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace Yutai.ArcGIS.Framework.Docking
{
    [Serializable, Flags, Editor(typeof(DockAreasEditor), typeof(UITypeEditor))]
    public enum DockAreas
    {
        DockBottom = 16,
        DockLeft = 2,
        DockRight = 4,
        DockTop = 8,
        Document = 32,
        Float = 1
    }
}