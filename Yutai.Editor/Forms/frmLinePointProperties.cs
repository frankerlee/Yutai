using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Forms
{
    public partial class frmLinePointProperties : XtraForm
    {
        private IAppContext _context;
        public frmLinePointProperties()
        {
            InitializeComponent();
        }

        public frmLinePointProperties(IAppContext context)
        {
            InitializeComponent();
            _context = context;
        }

        private void frmLinePointProperties_Load(object sender, EventArgs e)
        {
            chkStart.Checked = _context.Config.IsAddStartPoint;
            chkEnd.Checked = _context.Config.IsAddEndPoint;
            chkVertex.Checked = _context.Config.IsAddVertexPoint;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _context.Config.IsAddStartPoint = chkStart.Checked;
            _context.Config.IsAddEndPoint = chkEnd.Checked;
            _context.Config.IsAddVertexPoint = chkVertex.Checked;
            DialogResult= DialogResult.OK;
        }
    }
}
