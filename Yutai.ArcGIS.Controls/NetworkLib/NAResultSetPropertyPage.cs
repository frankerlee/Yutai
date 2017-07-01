using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace Yutai.ArcGIS.Controls.NetworkLib
{
    internal partial class NAResultSetPropertyPage : UserControl
    {
        private bool m_CanDo = false;
        private bool m_IsDirty = false;
        private bool m_TestOK = false;

        public NAResultSetPropertyPage()
        {
            this.InitializeComponent();
        }

        private void Init()
        {
            this.m_CanDo = false;
            this.m_CanDo = true;
        }

        private void NAResultSetPropertyPage_Load(object sender, EventArgs e)
        {
            this.Init();
        }
    }
}