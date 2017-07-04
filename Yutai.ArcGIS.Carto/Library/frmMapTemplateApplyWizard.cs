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
        private bool _hasClick = false;
        private double _X = 0.0;
        private double _Y = 0.0;
        private IContainer icontainer = null;
        private IEnvelope ienvelope = null;
        private int _wizardStep = 0;
        //private MapCoordinateInputPage mapCoordinateInputPage = new MapCoordinateInputPage();
        //private MapCoordinatePage mapCoordinatePage = new MapCoordinatePage();
        //private MapTemplateApplyHelp mapTemplateApplyHelp = new MapTemplateApplyHelp();
        //private MapTemplateList mapTemplateList = new MapTemplateList();
        //private MapTemplateParamPage mapTemplateParamPage = new MapTemplateParamPage();
        private MapCoordinateInputPage mapCoordinateInputPage;
        private MapCoordinatePage mapCoordinatePage;
        private MapTemplateApplyHelp mapTemplateApplyHelp;
        private MapTemplateList mapTemplateList;
        private MapTemplateParamPage mapTemplateParamPage;
        private MapTemplateGallery _mapTemplateGallery = null;

        public frmMapTemplateApplyWizard()
        {
            this.InitializeComponent();
        }

        public frmMapTemplateApplyWizard(MapTemplateGallery templateGallery)
        {
            this.InitializeComponent();
            _mapTemplateGallery = templateGallery;
        }

        public MapTemplateGallery MapTemplateGallery
        {
            get { return _mapTemplateGallery; }
            set { _mapTemplateGallery = value; }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            switch (this._wizardStep)
            {
                case 0:
                    return;

                case 1:
                    if (!this.IsInputTF)
                    {
                        this.mapTemplateList.Visible = true;
                        if (this.FixDataRange)
                        {
                            this.btnNext.Text = "下一步>";
                        }
                        else
                        {
                            this.mapTemplateParamPage.Visible = false;
                            this.btnNext.Text = "下一步>";
                        }
                        break;
                    }
                    this.mapCoordinatePage.Visible = true;
                    this.mapTemplateList.Visible = false;
                    break;

                case 2:
                    this.btnNext.Text = "下一步>";
                    if (!this.IsInputTF)
                    {
                        this.mapTemplateParamPage.Visible = true;
                        this.mapCoordinateInputPage.Visible = false;
                        break;
                    }
                    this.mapTemplateList.Visible = true;
                    this.mapTemplateParamPage.Visible = false;
                    break;

                case 3:
                    this.btnNext.Text = "下一步>";
                    this.mapTemplateParamPage.Visible = false;
                    this.mapCoordinateInputPage.Visible = true;
                    break;
            }
            this._wizardStep--;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            switch (this._wizardStep)
            {
                case 0:
                    if (!this.IsInputTF)
                    {
                        if (!this.mapTemplateList.Apply())
                        {
                            return;
                        }
                        this.mapTemplateList.Visible = false;
                        if (this.FixDataRange)
                        {
                            this.mapTemplateParamPage.CartoTemplateData =
                                this.mapTemplateList.SelectCartoTemplateData;
                            this.mapTemplateParamPage.Visible = true;
                            this.mapTemplateParamPage.Init();
                            this.btnNext.Text = "完成";
                        }
                        else
                        {
                            this.mapTemplateParamPage.CartoTemplateData =
                                this.mapTemplateList.SelectCartoTemplateData;
                            this.mapTemplateParamPage.Visible = true;
                            this.mapTemplateParamPage.Init();
                        }
                        break;
                    }
                    if (!this.mapCoordinatePage.CanApply())
                    {
                        return;
                    }
                    this.mapCoordinatePage.Visible = false;
                    this.mapCoordinatePage.Apply();
                    this.mapTemplateList.Visible = true;
                    break;

                case 1:
                    if (!this.FixDataRange)
                    {
                        if (this.IsInputTF)
                        {
                            if (!this.mapTemplateList.Apply())
                            {
                                return;
                            }
                            this.mapTemplateList.Visible = false;
                            MapTemplateParam param = this.mapTemplateList.SelectCartoTemplateData.FindParamByName("图号");
                            if (param != null)
                            {
                                param.Value = this.mapTemplateApplyHelp.MapNo;
                            }
                            this.mapTemplateParamPage.CartoTemplateData =
                                this.mapTemplateList.SelectCartoTemplateData;
                            this.mapTemplateParamPage.Visible = true;
                            this.mapTemplateParamPage.Init();
                            this.btnNext.Text = "完成";
                        }
                        else
                        {
                            this.mapTemplateParamPage.Visible = false;
                            this.mapTemplateParamPage.Apply();
                            this.mapCoordinateInputPage.Visible = true;
                            this.btnNext.Text = "完成";
                        }
                        break;
                    }
                    this.mapTemplateParamPage.Visible = false;
                    this.mapTemplateParamPage.Apply();
                    base.DialogResult = DialogResult.OK;
                    break;

                case 2:
                    if (!this.IsInputTF)
                    {
                        this.mapCoordinateInputPage.Apply();
                        base.DialogResult = DialogResult.OK;
                        break;
                    }
                    this.mapTemplateParamPage.Apply();
                    base.DialogResult = DialogResult.OK;
                    break;

                case 3:
                    base.DialogResult = DialogResult.OK;
                    return;
            }
            this._wizardStep++;
        }

        private void frmMapTemplateApplyWizard_Load(object sender, EventArgs e)
        {
            mapCoordinateInputPage = new MapCoordinateInputPage();
            mapCoordinatePage = new MapCoordinatePage();
            mapTemplateApplyHelp = new MapTemplateApplyHelp();
            mapTemplateList = new MapTemplateList(_mapTemplateGallery);
            mapTemplateParamPage = new MapTemplateParamPage();
            this.mapTemplateApplyHelp.FixDataRange = this.FixDataRange;
            this.mapTemplateList.IsInputTF = this.IsInputTF;
            this.mapTemplateList.HasMosueClick = this._hasClick;
            this.mapTemplateList.MapTemplateHelp = this.mapTemplateApplyHelp;
            if (this._hasClick)
            {
                this.mapCoordinateInputPage.SetMouseClick(this._X, this._Y);
            }
            this.mapTemplateList.Visible = !this.IsInputTF;
            this.mapTemplateList.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(this.mapTemplateList);
            this.mapCoordinatePage.Dock = DockStyle.Fill;
            this.mapCoordinatePage.Visible = this.IsInputTF;
            this.panel1.Controls.Add(this.mapCoordinatePage);
            this.mapCoordinatePage.MapTemplateHelp = this.mapTemplateApplyHelp;
            this.mapTemplateParamPage.Dock = DockStyle.Fill;
            this.mapTemplateParamPage.Visible = false;
            this.panel1.Controls.Add(this.mapTemplateParamPage);
            this.mapTemplateParamPage.MapTemplateHelp = this.mapTemplateApplyHelp;
            this.mapCoordinateInputPage.Dock = DockStyle.Fill;
            this.mapCoordinateInputPage.Visible = false;
            this.panel1.Controls.Add(this.mapCoordinateInputPage);
            this.mapCoordinateInputPage.MapTemplateHelp = this.mapTemplateApplyHelp;
        }

        public void SetMapEnv(IEnvelope envelope)
        {
            this.ienvelope = envelope;
            this._X = envelope.XMin;
            this._Y = envelope.YMin;
            this.FixDataRange = true;
        }

        public void SetMouseClick(double x, double y)
        {
            this._X = x;
            this._Y = y;
            this._hasClick = true;
            this.ienvelope = null;
        }

        public bool FixDataRange { get; set; }

        public bool IsInputTF { get; set; }

        

        public MapTemplateApplyHelp MapTemplateHelp
        {
            get { return this.mapTemplateApplyHelp; }
            set { this.mapTemplateApplyHelp = value; }
        }

        public IWorkspace Workspace
        {
            set { this.mapTemplateList.Workspace = value; }
        }
    }
}