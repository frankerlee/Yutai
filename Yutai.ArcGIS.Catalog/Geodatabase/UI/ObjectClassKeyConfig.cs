using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal partial class ObjectClassKeyConfig : UserControl
    {
        private Container container_0 = null;
        private string string_0 = "";

        public ObjectClassKeyConfig()
        {
            this.InitializeComponent();
        }

        public string ConfigKeyword
        {
            get { return this.string_0; }
            set { this.string_0 = value; }
        }
    }
}