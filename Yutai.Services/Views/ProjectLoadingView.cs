using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Yutai.Services.Views
{
    public partial class ProjectLoadingView : Yutai.UI.Forms.MapWindowView
    {
        public ProjectLoadingView()
        {
            InitializeComponent();
        }
        public ProjectLoadingView(string projectName)
        {
            InitializeComponent();

            Text = "引导项目: " + Path.GetFileNameWithoutExtension(projectName);
        }

        public override Plugins.Mvp.ViewStyle Style
        {
            get { return new Plugins.Mvp.ViewStyle() { Modal = false, Sizable = false }; }
        }

        public void ShowProgress(int percent, string message)
        {
            if (percent < 0) percent = 0;
            if (percent > 100) percent = 100;

            progressBarAdv1.EditValue = percent;
            progressBarAdv1.Refresh();

            lblMessage.Text = message;
            lblMessage.Refresh();

            Application.DoEvents();
        }
    }
}
