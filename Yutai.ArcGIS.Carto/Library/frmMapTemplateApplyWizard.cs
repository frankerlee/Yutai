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
    public partial class frmMapTemplateApplyWizard : Form
    {
        private bool bool_0 = false;
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

