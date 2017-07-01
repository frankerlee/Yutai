using System.Windows.Forms;

namespace Yutai.ArcGIS.Framework.Docking
{
    internal interface IDragSource
    {
        Control DragControl { get; }
    }
}