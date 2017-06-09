using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.TableEditor.Menu;
using Yutai.Plugins.TableEditor.Views;

namespace Yutai.Plugins.TableEditor.Editor
{
    public partial class Header : UserControl
    {
        public Header()
        {
            InitializeComponent();
        }

        [Browsable(true)]
        [Description("标题"), Category("扩展"), DefaultValue(null)]
        public string Value
        {
            get { return this.lblValue.Text; }
            set { this.lblValue.Text = value; }
        }

        public int Handler { get; set; }

        public IAppContext AppContext { get; set; }
        public ITableEditorView View { get; set; }
        private void btnClose_Click(object sender, EventArgs e)
        {
            View.ClosePage();
        }
        
        private void btnClose_MouseHover(object sender, EventArgs e)
        {
            PictureBox button = sender as PictureBox;
            if (button == null)
                return;
            button.BorderStyle = BorderStyle.Fixed3D;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            PictureBox button = sender as PictureBox;
            if (button == null)
                return;
            button.BorderStyle = BorderStyle.None;
        }
    }
}
