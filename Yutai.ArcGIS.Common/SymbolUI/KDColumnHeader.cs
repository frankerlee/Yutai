using System.Windows.Forms;

namespace Yutai.ArcGIS.Common.SymbolUI
{
    public class KDColumnHeader : ColumnHeader
    {
        // Fields
        private KDListViewColumnStyle kdlistViewColumnStyle_0;

        // Methods
        public KDColumnHeader()
        {
            this.kdlistViewColumnStyle_0 = KDListViewColumnStyle.ReadOnly;
        }

        public KDColumnHeader(KDListViewColumnStyle kdlistViewColumnStyle_1)
        {
            this.kdlistViewColumnStyle_0 = kdlistViewColumnStyle_1;
        }

        // Properties
        public KDListViewColumnStyle ColumnStyle
        {
            get
            {
                return this.kdlistViewColumnStyle_0;
            }
            set
            {
                this.kdlistViewColumnStyle_0 = value;
            }
        }
    }
}