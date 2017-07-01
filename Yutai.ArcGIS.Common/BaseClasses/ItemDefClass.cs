using System.Runtime.CompilerServices;
using ESRI.ArcGIS.SystemUI;

namespace Yutai.ArcGIS.Common.BaseClasses
{
    public class ItemDefClass : IItemDef
    {
        public ItemDefClass()
        {
            this.Group = false;
            this.SubType = -1;
        }

        public bool Group { get; set; }

        public string ID { get; set; }

        public int SubType { get; set; }
    }
}