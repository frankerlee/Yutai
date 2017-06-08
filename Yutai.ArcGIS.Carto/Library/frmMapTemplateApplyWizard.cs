using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Carto.MapCartoTemplateLib;

namespace Yutai.ArcGIS.Carto.Library
{
    public class frmMapTemplateApplyWizard : Form
    {
        private bool bool_0 = false;
        [CompilerGenerated]
        private bool bool_1;
        [CompilerGenerated]
        private bool bool_2;
        private Button btnLast;
        private Button btnNext;
        private Button button3;
        private double double_0 = 0.0;
        private double double_1 = 0.0;
        private IContainer icontainer_0 = null;
        private IEnvelope ienvelope_0 = null;
        private int int_0 = 0;
        private MapCoordinateInputPage mapCoordinateInputPage_0 = new MapCoordinateInputPage();
        private MapCoordinatePage mapCoordinatePage_0 = new MapCoordinatePage();
        private MapTemplateApplyHelp mapTemplateApplyHelp_0 = new MapTemplateApplyHelp();
        private MapTemplateList mapTemplateList_0 = new MapTemplateList();
        private MapTemplateParamPage mapTemplateParamPage_0 = new MapTemplateParamPage();
        private Panel panel1;
        private Panel panel2;

        public frmMapTemplateApplyWizard()
        {
            this.InitializeComponent();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            switch (this.int_0)
            {
                case 0:
                    return;

                case 1:
                    if (!this.IsInputTF)
                    {
                        this.mapTemplateList_0.Visible = true;
                        if (this.FixDataRange)
                        {
                            this.btnNext.Text = "下一步>";
                        }
                        else
                        {
                            this.mapTemplateParamPage_0.Visible = false;
                            this.btnNext.Text = "下一步>";
                        }
                        break;
                    }
                    this.mapCoordinatePage_0.Visible = true;
                    this.mapTemplateList_0.Visible = false;
                    break;

                case 2:
                    this.btnNext.Text = "下一步>";
                    if (!this.IsInputTF)
                    {
                        this.mapTemplateParamPage_0.Visible = true;
                        this.mapCoordinateInputPage_0.Visible = false;
                        break;
                    }
                    this.mapTemplateList_0.Visible = true;
                    this.mapTemplateParamPage_0.Visible = false;
                    break;

                case 3:
                    this.btnNext.Text = "下一步>";
                    this.mapTemplateParamPage_0.Visible = false;
                    this.mapCoordinateInputPage_0.Visible = true;
                    break;
            }
            this.int_0--;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            switch (this.int_0)
            {
                case 0:
                    if (!this.IsInputTF)
                    {
                        if (!this.mapTemplateList_0.Apply())
                        {
                            return;
                        }
                        this.mapTemplateList_0.Visible = false;
                        if (this.FixDataRange)
                        {
                            this.mapTemplateParamPage_0.CartoTemplateData = this.mapTemplateList_0.SelectCartoTemplateData;
                            this.mapTemplateParamPage_0.Visible = true;
                            this.mapTemplateParamPage_0.Init();
                            this.btnNext.Text = "完成";
                        }
                        else
                        {
                            this.mapTemplateParamPage_0.CartoTemplateData = this.mapTemplateList_0.SelectCartoTemplateData;
                            this.mapTemplateParamPage_0.Visible = true;
                            this.mapTemplateParamPage_0.Init();
                        }
                        break;
                    }
                    if (!this.mapCoordinatePage_0.CanApply())
                    {
                        return;
                    }
                    this.mapCoordinatePage_0.Visible = false;
                    this.mapCoordinatePage_0.Apply();
                    this.mapTemplateList_0.Visible = true;
                    break;

                case 1:
                    if (!this.FixDataRange)
                    {
                        if (this.IsInputTF)
                        {
                            if (!this.mapTemplateList_0.Apply())
                            {
                                return;
                            }
                            this.mapTemplateList_0.Visible = false;
                            MapTemplateParam param = this.mapTemplateList_0.SelectCartoTemplateData.FindParamByName("图号");
                            if (param != null)
                            {
                                param.Value = this.mapTemplateApplyHelp_0.MapNo;
                            }
                            this.mapTemplateParamPage_0.CartoTemplateData = this.mapTemplateList_0.SelectCartoTemplateData;
                            this.mapTemplateParamPage_0.Visible = true;
                            this.mapTemplateParamPage_0.Init();
                            this.btnNext.Text = "完成";
                        }
                        else
                        {
                            this.mapTemplateParamPage_0.Visible = false;
                            this.mapTemplateParamPage_0.Apply();
                            this.mapCoordinateInputPage_0.Visible = true;
                            this.btnNext.Text = "完成";
                        }
                        break;
                    }
                    this.mapTemplateParamPage_0.Visible = false;
                    this.mapTemplateParamPage_0.Apply();
                    base.DialogResult = DialogResult.OK;
                    break;

                case 2:
                    if (!this.IsInputTF)
                    {
                        this.mapCoordinateInputPage_0.Apply();
                        base.DialogResult = DialogResult.OK;
                        break;
                    }
                    this.mapTemplateParamPage_0.Apply();
                    base.DialogResult = DialogResult.OK;
                    break;

                case 3:
                    base.DialogResult = DialogResult.OK;
                    return;
            }
            this.int_0++;
        }

        protected override void Dispose(bool bool_3)
        {
            if (bool_3 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_3);
        }

        private void frmMapTemplateApplyWizard_Load(object sender, EventArgs e)
        {
            this.mapTemplateApplyHelp_0.FixDataRange = this.FixDataRange;
            this.mapTemplateList_0.IsInputTF = this.IsInputTF;
            this.mapTemplateList_0.HasMosueClick = this.bool_0;
            this.mapTemplateList_0.MapTemplateHelp = this.mapTemplateApplyHelp_0;
            if (this.bool_0)
            {
                this.mapCoordinateInputPage_0.SetMouseClick(this.double_0, this.double_1);
            }
            this.mapTemplateList_0.Visible = !this.IsInputTF;
            this.mapTemplateList_0.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(this.mapTemplateList_0);
            this.mapCoordinatePage_0.Dock = DockStyle.Fill;
            this.mapCoordinatePage_0.Visible = this.IsInputTF;
            this.panel1.Controls.Add(this.mapCoordinatePage_0);
            this.mapCoordinatePage_0.MapTemplateHelp = this.mapTemplateApplyHelp_0;
            this.mapTemplateParamPage_0.Dock = DockStyle.Fill;
            this.mapTemplateParamPage_0.Visible = false;
            this.panel1.Controls.Add(this.mapTemplateParamPage_0);
            this.mapTemplateParamPage_0.MapTemplateHelp = this.mapTemplateApplyHelp_0;
            this.mapCoordinateInputPage_0.Dock = DockStyle.Fill;
            this.mapCoordinateInputPage_0.Visible = false;
            this.panel1.Controls.Add(this.mapCoordinateInputPage_0);
            this.mapCoordinateInputPage_0.MapTemplateHelp = this.mapTemplateApplyHelp_0;
        }

        private void InitializeComponent()
        {
            this.panel2 = new Panel();
            this.panel1 = new Panel();
            this.button3 = new Button();
            this.btnNext = new Button();
            this.btnLast = new Button();
            this.panel2.SuspendLayout();
            base.SuspendLayout();
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.btnNext);
            this.panel2.Controls.Add(this.btnLast);
            this.panel2.Dock = DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 0x12d);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x222, 0x2b);
            this.panel2.TabIndex = 1;
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x222, 0x12d);
            this.panel1.TabIndex = 2;
            this.button3.DialogResult = DialogResult.Cancel;
            this.button3.Location = new System.Drawing.Point(0x1c5, 8);
            this.button3.Name = "button3";
            this.button3.Size = new Size(0x4b, 0x17);
            this.button3.TabIndex = 13;
            this.button3.Text = "取消";
            this.button3.UseVisualStyleBackColor = true;
            this.btnNext.Location = new System.Drawing.Point(0x171, 8);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(0x4b, 0x17);
            this.btnNext.TabIndex = 12;
            this.btnNext.Text = "下一步>";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.btnLast.Location = new System.Drawing.Point(0x120, 8);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new Size(0x4b, 0x17);
            this.btnLast.TabIndex = 11;
            this.btnLast.Text = "<上一步";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new EventHandler(this.btnLast_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x222, 0x158);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.panel2);
            base.Name = "frmMapTemplateApplyWizard";
            this.Text = "地图模板向导";
            base.Load += new EventHandler(this.frmMapTemplateApplyWizard_Load);
            this.panel2.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public void SetMapEnv(IEnvelope ienvelope_1)
        {
            this.ienvelope_0 = ienvelope_1;
            this.double_0 = ienvelope_1.XMin;
            this.double_1 = ienvelope_1.YMin;
            this.FixDataRange = true;
        }

        public void SetMouseClick(double double_2, double double_3)
        {
            this.double_0 = double_2;
            this.double_1 = double_3;
            this.bool_0 = true;
            this.ienvelope_0 = null;
        }

        public bool FixDataRange
        {
            [CompilerGenerated]
            get
            {
                return this.bool_1;
            }
            [CompilerGenerated]
            set
            {
                this.bool_1 = value;
            }
        }

        public bool IsInputTF
        {
            [CompilerGenerated]
            get
            {
                return this.bool_2;
            }
            [CompilerGenerated]
            set
            {
                this.bool_2 = value;
            }
        }

        public MapTemplateApplyHelp MapTemplateHelp
        {
            get
            {
                return this.mapTemplateApplyHelp_0;
            }
            set
            {
                this.mapTemplateApplyHelp_0 = value;
            }
        }

        public IWorkspace Workspace
        {
            set
            {
                this.mapTemplateList_0.Workspace = value;
            }
        }
    }
}

