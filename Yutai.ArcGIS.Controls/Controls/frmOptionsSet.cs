using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Yutai.ArcGIS.Common;
using Yutai.Plugins.Enums;

namespace Yutai.ArcGIS.Controls.Controls
{
    public partial class frmOptionsSet : Form
    {

        public frmOptionsSet()
        {
            this.InitializeComponent();
            string path = Application.StartupPath + @"\sysconfig.cfg";
            if (File.Exists(path))
            {
                using (TextReader reader = new StreamReader(path, Encoding.Default))
                {
                    string str2 = reader.ReadLine();
                    if (str2.Length > 0)
                    {
                        string[] strArray = str2.Split(new char[] { '=' });
                        if ((strArray.Length > 1) && (strArray[0].ToLower() == "pyramiddialogset"))
                        {
                            try
                            {
                                int num = Convert.ToInt32(strArray[1]);
                                if (num == 0)
                                {
                                    this.rdoAlwaysPrompt.Checked = true;
                                }
                                else if (num == 1)
                                {
                                    this.rdoAlwaysBuild.Checked = true;
                                }
                                else
                                {
                                    this.rdoNeverBuild.Checked = true;
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + @"\sysconfig.cfg";
            using (TextWriter writer = new StreamWriter(path, false, Encoding.Default))
            {
                this.WriteRasterCfg(writer);
            }
            base.DialogResult = DialogResult.OK;
        }

 private void frmOptionsSet_Load(object sender, EventArgs e)
        {
        }

 private void WriteRasterCfg(TextWriter reader)
        {
            PyramidPromptType alwaysPrompt = PyramidPromptType.AlwaysPrompt;
            if (this.rdoAlwaysBuild.Checked)
            {
                alwaysPrompt = PyramidPromptType.AlwaysBuildNoPrompt;
            }
            else if (this.rdoNeverBuild.Checked)
            {
                alwaysPrompt = PyramidPromptType.NeverBuildNoPrompt;
            }
            ApplicationRef.Application.PyramidPromptType = alwaysPrompt;
            string str = string.Format("PyramidDialogSet={0}", (int) alwaysPrompt);
            reader.Write(str);
        }
    }
}

