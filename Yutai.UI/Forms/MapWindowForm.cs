using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mvp;
using Yutai.UI.Properties;

namespace Yutai.UI.Forms
{
#if STYLE2010
    public partial class MapWindowForm : Office2010Form
#else
    public class MapWindowForm : XtraForm
#endif
    {
        protected readonly IAppContext _context;

        public MapWindowForm()
        {
            Icon = Resources.MapWindow;
        }

        public MapWindowForm(IAppContext context)
            : this()
        {
            _context = context;
        }

        public IAppContext AppContext
        {
            get { return _context; }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MapWindowForm
            // 

            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "MapWindowForm";
            this.Load += new System.EventHandler(this.MapWindowForm_Load);
            this.ResumeLayout(false);
        }

        private void MapWindowForm_Load(object sender, EventArgs e)
        {
            // Fixing CORE-160
        }
    }
}