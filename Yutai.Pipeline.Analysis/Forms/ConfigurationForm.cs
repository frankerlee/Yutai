using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.Pipeline.Analysis.Forms
{
	public class ConfigurationForm : XtraForm
	{
		private Configuration configuration_0;

		private IContainer icontainer_0 = null;

		private Label lbl_ZoomFactor;

		private CheckBox cb_Symmetry;

		private PictureBox pb_About;

		private Button btn_Close;

		private TrackBar tb_ZoomFactor;

		private GroupBox groupBox1;

		private CheckBox cb_CloseOnMouseUp;

		private CheckBox cb_DoubleBuffered;

		private CheckBox cb_RememberLastPoint;

		private CheckBox cb_ReturnToOrigin;

		private CheckBox cb_ShowInTaskbar;

		private CheckBox cb_HideMouseCursor;

		private CheckBox cb_TopMostWindow;

		private Label lbl_ZF;

		private Label lbl_SF;

		private Label lbl_MW;

		private Label lbl_MH;

		private TrackBar tb_SpeedFactor;

		private TrackBar tb_Width;

		private TrackBar tb_Height;

		private Label lbl_SpeedFactor;

		private Label lbl_Width;

		private Label lKgwAsmOjf;

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
			CheckBox checkBox = (CheckBox)obj;
			this.configuration_0.CloseOnMouseUp = checkBox.Checked;
		}

		private void cb_DoubleBuffered_CheckedChanged(object obj, EventArgs eventArg)
		{
			CheckBox checkBox = (CheckBox)obj;
			this.configuration_0.DoubleBuffered = checkBox.Checked;
		}

		private void cb_HideMouseCursor_CheckedChanged(object obj, EventArgs eventArg)
		{
			CheckBox checkBox = (CheckBox)obj;
			this.configuration_0.HideMouseCursor = checkBox.Checked;
		}

		private void cb_RememberLastPoint_CheckedChanged(object obj, EventArgs eventArg)
		{
			CheckBox checkBox = (CheckBox)obj;
			this.configuration_0.RememberLastPoint = checkBox.Checked;
		}

		private void cb_ReturnToOrigin_CheckedChanged(object obj, EventArgs eventArg)
		{
			CheckBox checkBox = (CheckBox)obj;
			this.configuration_0.ReturnToOrigin = checkBox.Checked;
		}

		private void cb_ShowInTaskbar_CheckedChanged(object obj, EventArgs eventArg)
		{
			CheckBox checkBox = (CheckBox)obj;
			this.configuration_0.ShowInTaskbar = checkBox.Checked;
		}

		private void cb_Symmetry_CheckedChanged(object obj, EventArgs eventArg)
		{
			if (!((CheckBox)obj).Checked)
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
			CheckBox checkBox = (CheckBox)obj;
			this.configuration_0.TopMostWindow = checkBox.Checked;
		}

		protected override void Dispose(bool disposing)
		{
			if ((!disposing ? false : this.icontainer_0 != null))
			{
				this.icontainer_0.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationForm));
			this.lbl_ZoomFactor = new Label();
			this.cb_Symmetry = new CheckBox();
			this.btn_Close = new Button();
			this.tb_ZoomFactor = new TrackBar();
			this.groupBox1 = new GroupBox();
			this.cb_CloseOnMouseUp = new CheckBox();
			this.cb_DoubleBuffered = new CheckBox();
			this.cb_RememberLastPoint = new CheckBox();
			this.cb_ReturnToOrigin = new CheckBox();
			this.cb_ShowInTaskbar = new CheckBox();
			this.cb_HideMouseCursor = new CheckBox();
			this.cb_TopMostWindow = new CheckBox();
			this.lbl_ZF = new Label();
			this.lbl_SF = new Label();
			this.lbl_MW = new Label();
			this.lbl_MH = new Label();
			this.tb_SpeedFactor = new TrackBar();
			this.tb_Width = new TrackBar();
			this.tb_Height = new TrackBar();
			this.lbl_SpeedFactor = new Label();
			this.lbl_Width = new Label();
			this.lKgwAsmOjf = new Label();
			this.pb_About = new PictureBox();
			((ISupportInitialize)this.tb_ZoomFactor).BeginInit();
			this.groupBox1.SuspendLayout();
			((ISupportInitialize)this.tb_SpeedFactor).BeginInit();
			((ISupportInitialize)this.tb_Width).BeginInit();
			((ISupportInitialize)this.tb_Height).BeginInit();
			((ISupportInitialize)this.pb_About).BeginInit();
			base.SuspendLayout();
			this.lbl_ZoomFactor.Location = new System.Drawing.Point(323, 18);
			this.lbl_ZoomFactor.Name = "lbl_ZoomFactor";
			this.lbl_ZoomFactor.Size = new System.Drawing.Size(60, 15);
			this.lbl_ZoomFactor.TabIndex = 21;
			this.lbl_ZoomFactor.Text = "?";
			this.cb_Symmetry.Checked = true;
			this.cb_Symmetry.CheckState = CheckState.Checked;
			this.cb_Symmetry.Location = new System.Drawing.Point(99, 192);
			this.cb_Symmetry.Name = "cb_Symmetry";
			this.cb_Symmetry.Size = new System.Drawing.Size(104, 18);
			this.cb_Symmetry.TabIndex = 18;
			this.cb_Symmetry.Text = "Keep symmetry";
			this.cb_Symmetry.CheckedChanged += new EventHandler(this.cb_Symmetry_CheckedChanged);
			this.btn_Close.Location = new System.Drawing.Point(467, 218);
			this.btn_Close.Name = "btn_Close";
			this.btn_Close.Size = new System.Drawing.Size(104, 26);
			this.btn_Close.TabIndex = 16;
			this.btn_Close.Text = "Close";
			this.btn_Close.Click += new EventHandler(this.btn_Close_Click);
			this.tb_ZoomFactor.Location = new System.Drawing.Point(99, 11);
			this.tb_ZoomFactor.Name = "tb_ZoomFactor";
			this.tb_ZoomFactor.Size = new System.Drawing.Size(220, 42);
			this.tb_ZoomFactor.TabIndex = 15;
			this.tb_ZoomFactor.Scroll += new EventHandler(this.tb_ZoomFactor_Scroll);
			this.groupBox1.Controls.Add(this.cb_CloseOnMouseUp);
			this.groupBox1.Controls.Add(this.cb_DoubleBuffered);
			this.groupBox1.Controls.Add(this.cb_RememberLastPoint);
			this.groupBox1.Controls.Add(this.cb_ReturnToOrigin);
			this.groupBox1.Controls.Add(this.cb_ShowInTaskbar);
			this.groupBox1.Controls.Add(this.cb_HideMouseCursor);
			this.groupBox1.Controls.Add(this.cb_TopMostWindow);
			this.groupBox1.Location = new System.Drawing.Point(391, 15);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(180, 174);
			this.groupBox1.TabIndex = 11;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = " Boolean Settings ";
			this.cb_CloseOnMouseUp.Location = new System.Drawing.Point(12, 22);
			this.cb_CloseOnMouseUp.Name = "cb_CloseOnMouseUp";
			this.cb_CloseOnMouseUp.Size = new System.Drawing.Size(148, 15);
			this.cb_CloseOnMouseUp.TabIndex = 1;
			this.cb_CloseOnMouseUp.Text = "Close On Mouse Up";
			this.cb_CloseOnMouseUp.CheckedChanged += new EventHandler(this.cb_CloseOnMouseUp_CheckedChanged);
			this.cb_DoubleBuffered.Location = new System.Drawing.Point(12, 41);
			this.cb_DoubleBuffered.Name = "cb_DoubleBuffered";
			this.cb_DoubleBuffered.Size = new System.Drawing.Size(148, 15);
			this.cb_DoubleBuffered.TabIndex = 1;
			this.cb_DoubleBuffered.Text = "Double Buffered";
			this.cb_DoubleBuffered.CheckedChanged += new EventHandler(this.cb_DoubleBuffered_CheckedChanged);
			this.cb_RememberLastPoint.Location = new System.Drawing.Point(12, 78);
			this.cb_RememberLastPoint.Name = "cb_RememberLastPoint";
			this.cb_RememberLastPoint.Size = new System.Drawing.Size(148, 15);
			this.cb_RememberLastPoint.TabIndex = 1;
			this.cb_RememberLastPoint.Text = "Remember Last Point";
			this.cb_RememberLastPoint.CheckedChanged += new EventHandler(this.cb_RememberLastPoint_CheckedChanged);
			this.cb_ReturnToOrigin.Location = new System.Drawing.Point(12, 96);
			this.cb_ReturnToOrigin.Name = "cb_ReturnToOrigin";
			this.cb_ReturnToOrigin.Size = new System.Drawing.Size(148, 15);
			this.cb_ReturnToOrigin.TabIndex = 1;
			this.cb_ReturnToOrigin.Text = "Return To Origin";
			this.cb_ReturnToOrigin.CheckedChanged += new EventHandler(this.cb_ReturnToOrigin_CheckedChanged);
			this.cb_ShowInTaskbar.Location = new System.Drawing.Point(12, 114);
			this.cb_ShowInTaskbar.Name = "cb_ShowInTaskbar";
			this.cb_ShowInTaskbar.Size = new System.Drawing.Size(148, 15);
			this.cb_ShowInTaskbar.TabIndex = 1;
			this.cb_ShowInTaskbar.Text = "Show In Taskbar";
			this.cb_ShowInTaskbar.CheckedChanged += new EventHandler(this.cb_ShowInTaskbar_CheckedChanged);
			this.cb_HideMouseCursor.Location = new System.Drawing.Point(12, 59);
			this.cb_HideMouseCursor.Name = "cb_HideMouseCursor";
			this.cb_HideMouseCursor.Size = new System.Drawing.Size(148, 15);
			this.cb_HideMouseCursor.TabIndex = 1;
			this.cb_HideMouseCursor.Text = "Hide Mouse Cursor";
			this.cb_HideMouseCursor.CheckedChanged += new EventHandler(this.cb_HideMouseCursor_CheckedChanged);
			this.cb_TopMostWindow.Location = new System.Drawing.Point(12, 133);
			this.cb_TopMostWindow.Name = "cb_TopMostWindow";
			this.cb_TopMostWindow.Size = new System.Drawing.Size(148, 15);
			this.cb_TopMostWindow.TabIndex = 1;
			this.cb_TopMostWindow.Text = "Top Most Window";
			this.cb_TopMostWindow.CheckedChanged += new EventHandler(this.cb_TopMostWindow_CheckedChanged);
			this.lbl_ZF.Location = new System.Drawing.Point(7, 18);
			this.lbl_ZF.Name = "lbl_ZF";
			this.lbl_ZF.Size = new System.Drawing.Size(88, 15);
			this.lbl_ZF.TabIndex = 8;
			this.lbl_ZF.Text = "放大比例";
			this.lbl_SF.Location = new System.Drawing.Point(7, 59);
			this.lbl_SF.Name = "lbl_SF";
			this.lbl_SF.Size = new System.Drawing.Size(88, 15);
			this.lbl_SF.TabIndex = 9;
			this.lbl_SF.Text = "Speed Factor";
			this.lbl_MW.Location = new System.Drawing.Point(7, 103);
			this.lbl_MW.Name = "lbl_MW";
			this.lbl_MW.Size = new System.Drawing.Size(88, 15);
			this.lbl_MW.TabIndex = 7;
			this.lbl_MW.Text = "Magnifier Width";
			this.lbl_MH.Location = new System.Drawing.Point(7, 148);
			this.lbl_MH.Name = "lbl_MH";
			this.lbl_MH.Size = new System.Drawing.Size(88, 15);
			this.lbl_MH.TabIndex = 10;
			this.lbl_MH.Text = "Magnifier Height";
			this.tb_SpeedFactor.Location = new System.Drawing.Point(99, 55);
			this.tb_SpeedFactor.Name = "tb_SpeedFactor";
			this.tb_SpeedFactor.Size = new System.Drawing.Size(220, 42);
			this.tb_SpeedFactor.TabIndex = 14;
			this.tb_SpeedFactor.Scroll += new EventHandler(this.tb_SpeedFactor_Scroll);
			this.tb_Width.Location = new System.Drawing.Point(99, 100);
			this.tb_Width.Name = "tb_Width";
			this.tb_Width.Size = new System.Drawing.Size(220, 42);
			this.tb_Width.TabIndex = 13;
			this.tb_Width.Scroll += new EventHandler(this.tb_Width_Scroll);
			this.tb_Height.Enabled = false;
			this.tb_Height.Location = new System.Drawing.Point(99, 144);
			this.tb_Height.Name = "tb_Height";
			this.tb_Height.Size = new System.Drawing.Size(220, 42);
			this.tb_Height.TabIndex = 12;
			this.tb_Height.Scroll += new EventHandler(this.tb_Height_Scroll);
			this.lbl_SpeedFactor.Location = new System.Drawing.Point(323, 59);
			this.lbl_SpeedFactor.Name = "lbl_SpeedFactor";
			this.lbl_SpeedFactor.Size = new System.Drawing.Size(60, 15);
			this.lbl_SpeedFactor.TabIndex = 22;
			this.lbl_SpeedFactor.Text = "?";
			this.lbl_Width.Location = new System.Drawing.Point(323, 103);
			this.lbl_Width.Name = "lbl_Width";
			this.lbl_Width.Size = new System.Drawing.Size(60, 15);
			this.lbl_Width.TabIndex = 19;
			this.lbl_Width.Text = "?";
			this.lKgwAsmOjf.Location = new System.Drawing.Point(323, 151);
			this.lKgwAsmOjf.Name = "lbl_Height";
			this.lKgwAsmOjf.Size = new System.Drawing.Size(60, 15);
			this.lKgwAsmOjf.TabIndex = 20;
			this.lKgwAsmOjf.Text = "?";
			this.pb_About.Cursor = Cursors.Hand;
			this.pb_About.Image = (Image)resources.GetObject("pb_About.Image");
			this.pb_About.Location = new System.Drawing.Point(267, 214);
			this.pb_About.Name = "pb_About";
			this.pb_About.Size = new System.Drawing.Size(35, 30);
			this.pb_About.TabIndex = 17;
			this.pb_About.TabStop = false;
			this.pb_About.MouseLeave += new EventHandler(this.pb_About_MouseLeave);
			this.pb_About.Click += new EventHandler(this.pb_About_Click);
			this.pb_About.MouseEnter += new EventHandler(this.pb_About_MouseEnter);
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(586, 254);
			base.Controls.Add(this.lbl_ZoomFactor);
			base.Controls.Add(this.cb_Symmetry);
			base.Controls.Add(this.pb_About);
			base.Controls.Add(this.btn_Close);
			base.Controls.Add(this.tb_ZoomFactor);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.lbl_ZF);
			base.Controls.Add(this.lbl_SF);
			base.Controls.Add(this.lbl_MW);
			base.Controls.Add(this.lbl_MH);
			base.Controls.Add(this.tb_SpeedFactor);
			base.Controls.Add(this.tb_Width);
			base.Controls.Add(this.tb_Height);
			base.Controls.Add(this.lbl_SpeedFactor);
			base.Controls.Add(this.lbl_Width);
			base.Controls.Add(this.lKgwAsmOjf);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Name = "ConfigurationForm";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Configuration";
			((ISupportInitialize)this.tb_ZoomFactor).EndInit();
			this.groupBox1.ResumeLayout(false);
			((ISupportInitialize)this.tb_SpeedFactor).EndInit();
			((ISupportInitialize)this.tb_Width).EndInit();
			((ISupportInitialize)this.tb_Height).EndInit();
			((ISupportInitialize)this.pb_About).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void method_0(Configuration configuration)
		{
			this.configuration_0 = configuration;
			this.tb_ZoomFactor.Maximum = (int)Configuration.ZOOM_FACTOR_MAX;
			this.tb_ZoomFactor.Minimum = (int)Configuration.ZOOM_FACTOR_MIN;
			this.tb_ZoomFactor.Value = (int)this.configuration_0.ZoomFactor;
			this.tb_SpeedFactor.Maximum = (int)(100f * Configuration.SPEED_FACTOR_MAX);
			this.tb_SpeedFactor.Minimum = (int)(100f * Configuration.SPEED_FACTOR_MIN);
			this.tb_SpeedFactor.Value = (int)(100f * this.configuration_0.SpeedFactor);
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
			(new AboutForm()).ShowDialog(this);
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
			TrackBar trackBar = (TrackBar)obj;
			this.configuration_0.MagnifierHeight = trackBar.Value;
			this.lKgwAsmOjf.Text = this.configuration_0.MagnifierHeight.ToString();
		}

		private void tb_SpeedFactor_Scroll(object obj, EventArgs eventArg)
		{
			TrackBar trackBar = (TrackBar)obj;
			this.configuration_0.SpeedFactor = (float)trackBar.Value / 100f;
			this.lbl_SpeedFactor.Text = this.configuration_0.SpeedFactor.ToString();
		}

		private void tb_Width_Scroll(object obj, EventArgs eventArg)
		{
			TrackBar trackBar = (TrackBar)obj;
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
			TrackBar trackBar = (TrackBar)obj;
			this.configuration_0.ZoomFactor = (float)trackBar.Value;
			this.lbl_ZoomFactor.Text = this.configuration_0.ZoomFactor.ToString();
		}
	}
}