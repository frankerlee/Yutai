using System.Runtime.CompilerServices;
using ESRI.ArcGIS.SystemUI;

namespace Yutai.ArcGIS.Common.BaseClasses
{
    public class ItemDefClass : IItemDef
    {
        [CompilerGenerated]
        private bool bool_0;
        [CompilerGenerated]
        private int int_0;
        [CompilerGenerated]
        private string string_0;

        public ItemDefClass()
        {
            this.Group = false;
            this.SubType = -1;
        }

        public bool Group
        {
            [CompilerGenerated]
            get
            {
                return this.bool_0;
            }
            [CompilerGenerated]
            set
            {
                this.bool_0 = value;
            }
        }

        public string ID
        {
            [CompilerGenerated]
            get
            {
                return this.string_0;
            }
            [CompilerGenerated]
            set
            {
                this.string_0 = value;
            }
        }

        public int SubType
        {
            [CompilerGenerated]
            get
            {
                return this.int_0;
            }
            [CompilerGenerated]
            set
            {
                this.int_0 = value;
            }
        }
    }
}

