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
    public partial class frmMagnifierWindows : Form
    {
        private bool m_CanDo = true;
        private IPoint m_CenterPoint = null;
        private AxMapControl m_MainMapControl = null;
        private IMap m_pMap = null;
        private MagnifierOrViewerProperty m_proprty = new MagnifierOrViewerProperty();

        public frmMagnifierWindows()
        {
            this.InitializeComponent();
        }

        private void AdjustByMagnify(double s)
        {
            IEnvelope envelope = this.GetEnvelope(1.0/s);
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
                    else if ((this.axMapControl1.CurrentTool != null) &&
                             (this.axMapControl1.CurrentTool.GetType().FullName !=
                              this.m_MainMapControl.CurrentTool.GetType().FullName))
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
                            string[] strArray = text.Split(new char[] {':'});
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
                    num2 = double.Parse(text.Split(new char[] {':'})[1]);
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
                    num2 = double.Parse(text.Substring(0, text.Length - 1))/100.0;
                    this.AdjustByMagnify(num2);
                }
            }
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
            extent.Width = (extent.Width*num3)/width;
            extent.Height = (extent.Height*num4)/height;
            extent.Expand(s, s, true);
            return extent;
        }

        private void Init()
        {
            IEnvelope envelope = this.GetEnvelope(0.5);
            this.axMapControl1.ActiveView.Extent = envelope;
            this.axMapControl1.ActiveView.Refresh();
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
            System.Drawing.Point p = new System.Drawing.Point((rectangle.Left + rectangle.Right)/2,
                (rectangle.Top + rectangle.Bottom)/2);
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
                    this.cboMapScale.Items.AddRange(new object[]
                    {
                        "1:1000", "1:10000", "1:24000", "1:100000", "1:250000", "1:500000", "1:750000", "1:1000000",
                        "1:5000000", "1:10000000"
                    });
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
                this.cboMapScale.Items.AddRange(new object[]
                {
                    "1:1000", "1:10000", "1:24000", "1:100000", "1:250000", "1:500000", "1:750000", "1:1000000",
                    "1:5000000", "1:10000000"
                });
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
                (this.m_pMap as IActiveViewEvents_Event).ItemAdded +=
                    (new IActiveViewEvents_ItemAddedEventHandler(this.frmMagnifierWindows_ItemAdded));
            }
        }

        public AxMapControl MainMapControl
        {
            set
            {
                this.m_MainMapControl = value;
                this.FocusMap = this.m_MainMapControl.Map;
                this.m_MainMapControl.OnExtentUpdated +=
                    new IMapControlEvents2_Ax_OnExtentUpdatedEventHandler(this.m_MainMapControl_OnExtentUpdated);
            }
        }
    }
}