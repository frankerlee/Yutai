using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Yutai.Pipeline.Analysis.Classes;

namespace Yutai.Pipeline.Analysis.Forms
{
    public partial class ConfigurationForm : XtraForm
    {
        private IContainer icontainer_0 = null;


        public ConfigurationForm(Configuration configuration)
        {
            this.InitializeComponent();
            this.method_0(configuration);
        }

        private void btn_Close_Click(object obj, EventArgs eventArg)
        {
            base.Close();
        }

        private void cb_CloseOnMouseUp_CheckedChanged(object obj, EventArgs eventArg)
        {
            CheckBox checkBox = (CheckBox) obj;
            this.configuration_0.CloseOnMouseUp = checkBox.Checked;
        }

        private void cb_DoubleBuffered_CheckedChanged(object obj, EventArgs eventArg)
        {
            CheckBox checkBox = (CheckBox) obj;
            this.configuration_0.DoubleBuffered = checkBox.Checked;
        }

        private void cb_HideMouseCursor_CheckedChanged(object obj, EventArgs eventArg)
        {
            CheckBox checkBox = (CheckBox) obj;
            this.configuration_0.HideMouseCursor = checkBox.Checked;
        }

        private void cb_RememberLastPoint_CheckedChanged(object obj, EventArgs eventArg)
        {
            CheckBox checkBox = (CheckBox) obj;
            this.configuration_0.RememberLastPoint = checkBox.Checked;
        }

        private void cb_ReturnToOrigin_CheckedChanged(object obj, EventArgs eventArg)
        {
            CheckBox checkBox = (CheckBox) obj;
            this.configuration_0.ReturnToOrigin = checkBox.Checked;
        }

        private void cb_ShowInTaskbar_CheckedChanged(object obj, EventArgs eventArg)
        {
            CheckBox checkBox = (CheckBox) obj;
            this.configuration_0.ShowInTaskbar = checkBox.Checked;
        }

        private void cb_Symmetry_CheckedChanged(object obj, EventArgs eventArg)
        {
            if (!((CheckBox) obj).Checked)
            {
                this.tb_Height.Enabled = true;
            }
            else
            {
                this.tb_Height.Enabled = false;
            }
        }

        private void cb_TopMostWindow_CheckedChanged(object obj, EventArgs eventArg)
        {
            CheckBox checkBox = (CheckBox) obj;
            this.configuration_0.TopMostWindow = checkBox.Checked;
        }

        private void method_0(Configuration configuration)
        {
            this.configuration_0 = configuration;
            this.tb_ZoomFactor.Maximum = (int) Configuration.ZOOM_FACTOR_MAX;
            this.tb_ZoomFactor.Minimum = (int) Configuration.ZOOM_FACTOR_MIN;
            this.tb_ZoomFactor.Value = (int) this.configuration_0.ZoomFactor;
            this.tb_SpeedFactor.Maximum = (int) (100f*Configuration.SPEED_FACTOR_MAX);
            this.tb_SpeedFactor.Minimum = (int) (100f*Configuration.SPEED_FACTOR_MIN);
            this.tb_SpeedFactor.Value = (int) (100f*this.configuration_0.SpeedFactor);
            this.tb_Width.Maximum = 1000;
            this.tb_Width.Minimum = 100;
            this.tb_Width.Value = this.configuration_0.MagnifierWidth;
            this.tb_Height.Maximum = 1000;
            this.tb_Height.Minimum = 100;
            this.tb_Height.Value = this.configuration_0.MagnifierHeight;
            Label lblZoomFactor = this.lbl_ZoomFactor;
            float zoomFactor = this.configuration_0.ZoomFactor;
            lblZoomFactor.Text = zoomFactor.ToString();
            Label lblSpeedFactor = this.lbl_SpeedFactor;
            zoomFactor = this.configuration_0.SpeedFactor;
            lblSpeedFactor.Text = zoomFactor.ToString();
            this.lbl_Width.Text = this.configuration_0.MagnifierWidth.ToString();
            this.lKgwAsmOjf.Text = this.configuration_0.MagnifierHeight.ToString();
            this.cb_CloseOnMouseUp.Checked = this.configuration_0.CloseOnMouseUp;
            this.cb_DoubleBuffered.Checked = this.configuration_0.DoubleBuffered;
            this.cb_HideMouseCursor.Checked = this.configuration_0.HideMouseCursor;
            this.cb_RememberLastPoint.Checked = this.configuration_0.RememberLastPoint;
            this.cb_ReturnToOrigin.Checked = this.configuration_0.ReturnToOrigin;
            this.cb_ShowInTaskbar.Checked = this.configuration_0.ShowInTaskbar;
            this.cb_TopMostWindow.Checked = this.configuration_0.TopMostWindow;
            base.ShowInTaskbar = false;
        }

        private void pb_About_Click(object obj, EventArgs eventArg)
        {
            //(new AboutForm()).ShowDialog(this);
        }

        private void pb_About_MouseEnter(object obj, EventArgs eventArg)
        {
            this.Cursor = Cursors.Hand;
        }

        private void pb_About_MouseLeave(object obj, EventArgs eventArg)
        {
            this.Cursor = Cursors.Default;
        }

        private void tb_Height_Scroll(object obj, EventArgs eventArg)
        {
            TrackBar trackBar = (TrackBar) obj;
            this.configuration_0.MagnifierHeight = trackBar.Value;
            this.lKgwAsmOjf.Text = this.configuration_0.MagnifierHeight.ToString();
        }

        private void tb_SpeedFactor_Scroll(object obj, EventArgs eventArg)
        {
            TrackBar trackBar = (TrackBar) obj;
            this.configuration_0.SpeedFactor = (float) trackBar.Value/100f;
            this.lbl_SpeedFactor.Text = this.configuration_0.SpeedFactor.ToString();
        }

        private void tb_Width_Scroll(object obj, EventArgs eventArg)
        {
            TrackBar trackBar = (TrackBar) obj;
            this.configuration_0.MagnifierWidth = trackBar.Value;
            this.lbl_Width.Text = this.configuration_0.MagnifierWidth.ToString();
            if (this.cb_Symmetry.Checked)
            {
                this.tb_Height.Value = trackBar.Value;
                this.configuration_0.MagnifierHeight = trackBar.Value;
                this.lKgwAsmOjf.Text = this.configuration_0.MagnifierHeight.ToString();
            }
        }

        private void tb_ZoomFactor_Scroll(object obj, EventArgs eventArg)
        {
            TrackBar trackBar = (TrackBar) obj;
            this.configuration_0.ZoomFactor = (float) trackBar.Value;
            this.lbl_ZoomFactor.Text = this.configuration_0.ZoomFactor.ToString();
        }
    }
}