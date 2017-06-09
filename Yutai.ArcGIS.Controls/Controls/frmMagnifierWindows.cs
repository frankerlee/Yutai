using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using Yutai.ArcGIS.Common;

namespace Yutai.ArcGIS.Controls.Controls
{
    public class frmMagnifierWindows : Form
    {
        private AxMapControl axMapControl1;
        private ToolStripButton btnFixedZoomIn;
        private ToolStripButton btnFixedZoomOut;
        private ToolStripButton btnFullExtend;
        private ToolStripButton btnLastExtend;
        private ToolStripButton btnNextExtend;
        private ToolStripComboBox cboMapScale;
        private IContainer components = null;
        private bool m_CanDo = true;
        private IPoint m_CenterPoint = null;
        private AxMapControl m_MainMapControl = null;
        private IMap m_pMap = null;
        private MagnifierOrViewerProperty m_proprty = new MagnifierOrViewerProperty();
        private ToolStripMenuItem mnuProperty;
        private ToolStrip toolStrip1;
        private ToolStripDropDownButton toolStripDropDownButton1;

        public frmMagnifierWindows()
        {
            this.InitializeComponent();
        }

        private void AdjustByMagnify(double s)
        {
            IEnvelope envelope = this.GetEnvelope(1.0 / s);
            this.axMapControl1.ActiveView.Extent = envelope;
            if (this.m_CenterPoint != null)
            {
                this.axMapControl1.CenterAt(this.m_CenterPoint);
            }
            this.axMapControl1.ActiveView.Refresh();
        }

        private void AdjustByScale(double s)
        {
            if (this.axMapControl1.Map.MapUnits != esriUnits.esriUnknownUnits)
            {
                this.axMapControl1.Map.MapScale = s;
                this.axMapControl1.ActiveView.Refresh();
            }
        }

        private void axMapControl1_MouseCaptureChanged(object sender, EventArgs e)
        {
        }

        private void axMapControl1_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            if (this.m_proprty.m_Formtype == FormType.Viewer)
            {
                if (this.m_MainMapControl.CurrentTool == null)
                {
                    this.axMapControl1.CurrentTool = null;
                }
                else
                {
                    ICommand command;
                    if (this.axMapControl1.CurrentTool == null)
                    {
                        command = Activator.CreateInstance(this.m_MainMapControl.CurrentTool.GetType()) as ICommand;
                        command.OnCreate(this.axMapControl1.Object);
                        this.axMapControl1.CurrentTool = command as ITool;
                    }
                    else if ((this.axMapControl1.CurrentTool != null) && (this.axMapControl1.CurrentTool.GetType().FullName != this.m_MainMapControl.CurrentTool.GetType().FullName))
                    {
                        command = Activator.CreateInstance(this.m_MainMapControl.CurrentTool.GetType()) as ICommand;
                        command.OnCreate(this.axMapControl1.Object);
                        this.axMapControl1.CurrentTool = command as ITool;
                    }
                }
            }
        }

        private void cboMapScale_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                string text;
                int num;
                double num2;
                if (this.m_proprty.m_ZoomType != ZoomType.MagnifyBy)
                {
                    text = "";
                    if (this.cboMapScale.SelectedIndex != -1)
                    {
                        text = this.cboMapScale.Text;
                    }
                    else
                    {
                        text = this.cboMapScale.Text;
                        if (text.Length == 0)
                        {
                            text = "1:1000";
                        }
                        else
                        {
                            string[] strArray = text.Split(new char[] { ':' });
                            if (strArray.Length >= 2)
                            {
                                text = strArray[1];
                            }
                            string str3 = text;
                            for (num = 0; num < text.Length; num++)
                            {
                                if ((text[num] < '0') || (text[num] > '9'))
                                {
                                    str3 = text.Substring(0, num + 1);
                                    break;
                                }
                            }
                            if (str3.Length > 0)
                            {
                                text = "1:" + str3;
                            }
                            else
                            {
                                text = "1:1000";
                            }
                        }
                    }
                    this.m_proprty.m_scale = text;
                    num2 = double.Parse(text.Split(new char[] { ':' })[1]);
                    this.AdjustByScale(num2);
                }
                else
                {
                    text = "";
                    if (this.cboMapScale.SelectedIndex != -1)
                    {
                        text = this.cboMapScale.Text;
                    }
                    else
                    {
                        text = this.cboMapScale.Text;
                        if (text.Length == 0)
                        {
                            text = "200%";
                        }
                        else if ((text[0] < '0') || (text[0] > '9'))
                        {
                            text = "200%";
                        }
                        else
                        {
                            if (text[text.Length - 1] == '%')
                            {
                                text = text.Substring(0, text.Length - 1);
                            }
                            string str2 = "";
                            for (num = 0; num < text.Length; num++)
                            {
                                if ((text[num] < '0') || (text[num] > '9'))
                                {
                                    str2 = text.Substring(0, num + 1);
                                    break;
                                }
                            }
                            if (str2.Length > 0)
                            {
                                text = str2 + "%";
                            }
                            else
                            {
                                text = "200%";
                            }
                        }
                    }
                    this.m_proprty.m_magnify = text;
                    num2 = double.Parse(text.Substring(0, text.Length - 1)) / 100.0;
                    this.AdjustByMagnify(num2);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmMagnifierWindows_ItemAdded(object Item)
        {
            MapHelper.CopyMap(this.m_pMap, this.axMapControl1.Map, false, false);
        }

        private void frmMagnifierWindows_Load(object sender, EventArgs e)
        {
            this.cboMapScale.SelectedIndexChanged += new EventHandler(this.cboMapScale_SelectedIndexChanged);
            ICommand command = new ControlsMapZoomInFixedCommandClass();
            command.OnCreate(this.axMapControl1.Object);
            this.btnFixedZoomIn.Tag = command;
            command = new ControlsMapZoomOutFixedCommandClass();
            command.OnCreate(this.axMapControl1.Object);
            this.btnFixedZoomOut.Tag = command;
            command = new ControlsMapFullExtentCommandClass();
            command.OnCreate(this.axMapControl1.Object);
            this.btnFullExtend.Tag = command;
            command = new ControlsMapZoomToLastExtentBackCommandClass();
            command.OnCreate(this.axMapControl1.Object);
            this.btnLastExtend.Tag = command;
            command = new ControlsMapZoomToLastExtentForwardCommandClass();
            command.OnCreate(this.axMapControl1.Object);
            this.btnNextExtend.Tag = command;
            base.MouseUp += new MouseEventHandler(this.frmMagnifierWindows_MouseUp);
            base.Move += new EventHandler(this.frmMagnifierWindows_Move);
            MapHelper.CopyMap(this.m_pMap, this.axMapControl1.Map, false, false);
            if (this.m_proprty.m_ZoomType == ZoomType.FixedScale)
            {
                this.cboMapScale.Enabled = this.m_pMap.MapUnits != esriUnits.esriUnknownUnits;
            }
            else
            {
                this.cboMapScale.Enabled = true;
            }
        }

        private void frmMagnifierWindows_MouseUp(object sender, MouseEventArgs e)
        {
            this.SetExtend();
        }

        private void frmMagnifierWindows_Move(object sender, EventArgs e)
        {
            if (this.m_proprty.m_Formtype == FormType.Magnifier)
            {
                this.SetExtend();
            }
        }

        private void frmMagnifierWindows_Shown(object sender, EventArgs e)
        {
            this.SetProperty();
            this.Init();
            this.SetExtend();
        }

        private void frmMagnifierWindows_SizeChanged(object sender, EventArgs e)
        {
            this.cboMapScale_SelectedIndexChanged(this, e);
            this.SetExtend();
        }

        private void frmMagnifierWindows_VisibleChanged(object sender, EventArgs e)
        {
        }

        private IEnvelope GetEnvelope(double s)
        {
            IEnvelope extent = (this.m_pMap as IActiveView).Extent;
            double width = this.m_MainMapControl.Width;
            double height = this.m_MainMapControl.Height;
            double num3 = this.axMapControl1.Width;
            double num4 = this.axMapControl1.Height;
            extent.Width = (extent.Width * num3) / width;
            extent.Height = (extent.Height * num4) / height;
            extent.Expand(s, s, true);
            return extent;
        }

        private void Init()
        {
            IEnvelope envelope = this.GetEnvelope(0.5);
            this.axMapControl1.ActiveView.Extent = envelope;
            this.axMapControl1.ActiveView.Refresh();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMagnifierWindows));
            this.toolStrip1 = new ToolStrip();
            this.btnFixedZoomIn = new ToolStripButton();
            this.btnFixedZoomOut = new ToolStripButton();
            this.btnFullExtend = new ToolStripButton();
            this.btnLastExtend = new ToolStripButton();
            this.btnNextExtend = new ToolStripButton();
            this.cboMapScale = new ToolStripComboBox();
            this.toolStripDropDownButton1 = new ToolStripDropDownButton();
            this.mnuProperty = new ToolStripMenuItem();
            this.axMapControl1 = new AxMapControl();
            this.toolStrip1.SuspendLayout();
            this.axMapControl1.BeginInit();
            base.SuspendLayout();
            this.toolStrip1.Items.AddRange(new ToolStripItem[] { this.btnFixedZoomIn, this.btnFixedZoomOut, this.btnFullExtend, this.btnLastExtend, this.btnNextExtend, this.cboMapScale, this.toolStripDropDownButton1 });
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new Size(0x124, 0x19);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            this.btnFixedZoomIn.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.btnFixedZoomIn.Image = (Image) resources.GetObject("btnFixedZoomIn.Image");
            this.btnFixedZoomIn.ImageTransparentColor = Color.Magenta;
            this.btnFixedZoomIn.Name = "btnFixedZoomIn";
            this.btnFixedZoomIn.Size = new Size(0x17, 0x16);
            this.btnFixedZoomIn.Text = "toolStripButton1";
            this.btnFixedZoomOut.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.btnFixedZoomOut.Image = (Image) resources.GetObject("btnFixedZoomOut.Image");
            this.btnFixedZoomOut.ImageTransparentColor = Color.Magenta;
            this.btnFixedZoomOut.Name = "btnFixedZoomOut";
            this.btnFixedZoomOut.Size = new Size(0x17, 0x16);
            this.btnFixedZoomOut.Text = "toolStripButton2";
            this.btnFullExtend.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.btnFullExtend.Image = (Image) resources.GetObject("btnFullExtend.Image");
            this.btnFullExtend.ImageTransparentColor = Color.Magenta;
            this.btnFullExtend.Name = "btnFullExtend";
            this.btnFullExtend.Size = new Size(0x17, 0x16);
            this.btnFullExtend.Text = "toolStripButton3";
            this.btnLastExtend.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.btnLastExtend.Image = (Image) resources.GetObject("btnLastExtend.Image");
            this.btnLastExtend.ImageTransparentColor = Color.Magenta;
            this.btnLastExtend.Name = "btnLastExtend";
            this.btnLastExtend.Size = new Size(0x17, 0x16);
            this.btnLastExtend.Text = "toolStripButton4";
            this.btnNextExtend.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.btnNextExtend.Image = (Image) resources.GetObject("btnNextExtend.Image");
            this.btnNextExtend.ImageTransparentColor = Color.Magenta;
            this.btnNextExtend.Name = "btnNextExtend";
            this.btnNextExtend.Size = new Size(0x17, 0x16);
            this.btnNextExtend.Text = "toolStripButton5";
            this.cboMapScale.Name = "cboMapScale";
            this.cboMapScale.Size = new Size(0x79, 0x19);
            this.toolStripDropDownButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new ToolStripItem[] { this.mnuProperty });
            this.toolStripDropDownButton1.Image = (Image) resources.GetObject("toolStripDropDownButton1.Image");
            this.toolStripDropDownButton1.ImageTransparentColor = Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new Size(0x1d, 0x16);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            this.mnuProperty.Name = "mnuProperty";
            this.mnuProperty.Size = new Size(0x5e, 0x16);
            this.mnuProperty.Text = "属性";
            this.mnuProperty.Click += new EventHandler(this.mnuProperty_Click);
            this.axMapControl1.Dock = DockStyle.Fill;
            this.axMapControl1.Location = new System.Drawing.Point(0, 0x19);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = (AxHost.State) resources.GetObject("axMapControl1.OcxState");
            this.axMapControl1.Size = new Size(0x124, 0xf8);
            this.axMapControl1.TabIndex = 2;
            this.axMapControl1.MouseCaptureChanged += new EventHandler(this.axMapControl1_MouseCaptureChanged);
            this.axMapControl1.OnMouseMove += new IMapControlEvents2_Ax_OnMouseMoveEventHandler(this.axMapControl1_OnMouseMove);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x124, 0x111);
            base.Controls.Add(this.axMapControl1);
            base.Controls.Add(this.toolStrip1);
            base.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            base.Name = "frmMagnifierWindows";
            this.Text = "frmMagnifierWindows";
            base.TopMost = true;
            base.Load += new EventHandler(this.frmMagnifierWindows_Load);
            base.SizeChanged += new EventHandler(this.frmMagnifierWindows_SizeChanged);
            base.Shown += new EventHandler(this.frmMagnifierWindows_Shown);
            base.VisibleChanged += new EventHandler(this.frmMagnifierWindows_VisibleChanged);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.axMapControl1.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void m_MainMapControl_OnExtentUpdated(object sender, IMapControlEvents2_OnExtentUpdatedEvent e)
        {
            this.cboMapScale_SelectedIndexChanged(this, new EventArgs());
            this.SetExtend();
        }

        private void mnuProperty_Click(object sender, EventArgs e)
        {
            frmViewerOrMagnifierPropertyPage ownedForm = new frmViewerOrMagnifierPropertyPage();
            base.AddOwnedForm(ownedForm);
            ownedForm.TopMost = true;
            ownedForm.Property = this.m_proprty;
            if (ownedForm.ShowDialog() == DialogResult.OK)
            {
                this.SetProperty();
            }
        }

        private void SetExtend()
        {
            Rectangle rectangle = new Rectangle(this.axMapControl1.Location, this.axMapControl1.Size);
            System.Drawing.Point p = new System.Drawing.Point((rectangle.Left + rectangle.Right) / 2, (rectangle.Top + rectangle.Bottom) / 2);
            p = this.axMapControl1.PointToScreen(p);
            p = this.m_MainMapControl.PointToClient(p);
            this.m_CenterPoint = this.m_MainMapControl.ToMapPoint(p.X, p.Y);
            this.axMapControl1.CenterAt(this.m_CenterPoint);
            this.axMapControl1.ActiveView.Refresh();
        }

        private void SetProperty()
        {
            if (this.m_proprty.m_Formtype == FormType.Magnifier)
            {
                this.btnFixedZoomIn.Enabled = false;
                this.btnFixedZoomOut.Enabled = false;
                this.btnFullExtend.Enabled = false;
                this.btnLastExtend.Enabled = false;
                this.btnNextExtend.Enabled = false;
                this.m_CanDo = false;
                this.cboMapScale.Items.Clear();
                string magnify = "";
                if (this.m_proprty.m_ZoomType == ZoomType.MagnifyBy)
                {
                    this.cboMapScale.Items.Add("200%");
                    this.cboMapScale.Items.Add("400%");
                    this.cboMapScale.Items.Add("600%");
                    this.cboMapScale.Items.Add("800%");
                    magnify = this.m_proprty.m_magnify;
                }
                else
                {
                    this.cboMapScale.Items.AddRange(new object[] { "1:1000", "1:10000", "1:24000", "1:100000", "1:250000", "1:500000", "1:750000", "1:1000000", "1:5000000", "1:10000000" });
                    magnify = this.m_proprty.m_scale;
                }
                this.m_CanDo = true;
                this.cboMapScale.Text = magnify;
                this.Text = "放大镜";
                if (this.m_proprty.m_ZoomType == ZoomType.FixedScale)
                {
                    this.cboMapScale.Enabled = this.m_pMap.MapUnits != esriUnits.esriUnknownUnits;
                }
                else
                {
                    this.cboMapScale.Enabled = true;
                }
            }
            else
            {
                this.axMapControl1.CurrentTool = null;
                this.btnFixedZoomIn.Enabled = true;
                this.btnFixedZoomOut.Enabled = true;
                this.btnFullExtend.Enabled = true;
                this.btnLastExtend.Enabled = true;
                this.btnNextExtend.Enabled = true;
                this.m_CanDo = false;
                this.cboMapScale.Items.Clear();
                this.cboMapScale.Items.AddRange(new object[] { "1:1000", "1:10000", "1:24000", "1:100000", "1:250000", "1:500000", "1:750000", "1:1000000", "1:5000000", "1:10000000" });
                this.m_CanDo = true;
                this.cboMapScale.Text = this.m_proprty.m_scale;
                this.Text = "视图";
                this.cboMapScale.Enabled = this.m_pMap.MapUnits != esriUnits.esriUnknownUnits;
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Tag is ICommand)
            {
                (e.ClickedItem.Tag as ICommand).OnClick();
                this.m_CanDo = false;
                this.cboMapScale.Text = "1:" + MapHelper.GetMapScale(this.axMapControl1.Map).ToString();
                this.m_CanDo = true;
            }
        }

        public IMap FocusMap
        {
            set
            {
                this.m_pMap = value;
                (this.m_pMap as IActiveViewEvents_Event).ItemAdded+=(new IActiveViewEvents_ItemAddedEventHandler(this.frmMagnifierWindows_ItemAdded));
            }
        }

        public AxMapControl MainMapControl
        {
            set
            {
                this.m_MainMapControl = value;
                this.FocusMap = this.m_MainMapControl.Map;
                this.m_MainMapControl.OnExtentUpdated += new IMapControlEvents2_Ax_OnExtentUpdatedEventHandler(this.m_MainMapControl_OnExtentUpdated);
            }
        }
    }
}

