using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Yutai.Plugins.Interfaces;

namespace Yutai.Forms
{
    public partial class SplashView : XtraForm, ISplashView
    {
        private static SplashView _instance;

        public static SplashView Instance
        {
            get { return _instance ?? (_instance = new SplashView()); }
        }

        public SplashView()
        {
            InitializeComponent();

            ShowVersion();
        }

        private void ShowVersion()
        {
            lblVersion.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        public void ShowStatus(string message)
        {
            lblStatus.Text = message + "...";
            lblStatus.Refresh();
        }
    }
}