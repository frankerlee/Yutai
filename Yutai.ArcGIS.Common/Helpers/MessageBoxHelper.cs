using System;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Common.Helpers
{
    public class MessageBoxHelper
    {
        public MessageBoxHelper()
        {
        }

        public static void ShowErrorMessageBox(string sErr)
        {
            MessageBox.Show(sErr, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public static void ShowErrorMessageBox(Exception ex, string sErr)
        {
            MessageBox.Show(ex.Message, sErr, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public static void ShowMessageBox(string sInfo)
        {
            MessageBox.Show(sInfo, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
    }
}