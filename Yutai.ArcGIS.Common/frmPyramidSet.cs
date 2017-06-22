using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.Plugins.Enums;

namespace Yutai.ArcGIS.Common
{
    public partial class frmPyramidSet : Form
    {
        private IContainer icontainer_0 = null;

        public frmPyramidSet()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ApplicationRef.Application.PyramidPromptType = this.checkBox1.Checked ? PyramidPromptType.AlwaysBuildNoPrompt : PyramidPromptType.AlwaysPrompt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ApplicationRef.Application.PyramidPromptType = this.checkBox1.Checked ? PyramidPromptType.NeverBuildNoPrompt : PyramidPromptType.AlwaysPrompt;
        }


    }
}