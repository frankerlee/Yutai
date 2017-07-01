using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public partial class JLKMapTemplateWizard : Form
    {
        private GridAxisPropertyPage gridAxisPropertyPage_0 = new GridAxisPropertyPage();
        private IContainer icontainer_0 = null;
        private IMapFrame imapFrame_0 = null;
        private int int_0 = 0;
        private LabelFormatPropertyPage labelFormatPropertyPage_0 = new LabelFormatPropertyPage();
        internal IMapGrid m_pMapGrid = null;
        private MapTemplate mapTemplate_0 = new MapTemplate();
        private MapTemplateGeneralPage mapTemplateGeneralPage_0 = new MapTemplateGeneralPage();
        private MapTemplateTypePage mapTemplateTypePage_0 = new MapTemplateTypePage();
        private OtherGridPropertyPage otherGridPropertyPage_0 = new OtherGridPropertyPage();
        private StandardTickSymbolPropertyPage standardTickSymbolPropertyPage_0 = new StandardTickSymbolPropertyPage();
        private StarndFenFuMapTemplatePage starndFenFuMapTemplatePage_0 = new StarndFenFuMapTemplatePage();
        private TickSymbolPropertyPage tickSymbolPropertyPage_0 = new TickSymbolPropertyPage();

        public JLKMapTemplateWizard()
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
                    this.mapTemplateTypePage_0.Visible = true;
                    if (this.mapTemplateTypePage_0.m_MapTemplateType != 0)
                    {
                        this.starndFenFuMapTemplatePage_0.Visible = false;
                        break;
                    }
                    this.mapTemplateGeneralPage_0.Visible = false;
                    break;

                case 2:
                    if (this.mapTemplateTypePage_0.m_MapTemplateType != 0)
                    {
                        this.starndFenFuMapTemplatePage_0.Visible = true;
                        this.standardTickSymbolPropertyPage_0.Visible = false;
                        this.btnNext.Text = "下一步>";
                        break;
                    }
                    if (!this.mapTemplateGeneralPage_0.UseMapGrid)
                    {
                        this.mapTemplateGeneralPage_0.Visible = true;
                        this.otherGridPropertyPage_0.Visible = false;
                        this.btnNext.Text = "下一步>";
                        break;
                    }
                    this.mapTemplateGeneralPage_0.Visible = true;
                    this.labelFormatPropertyPage_0.Visible = false;
                    break;

                case 3:
                    this.labelFormatPropertyPage_0.Visible = true;
                    this.gridAxisPropertyPage_0.Visible = false;
                    break;

                case 4:
                    this.btnNext.Text = "下一步>";
                    this.gridAxisPropertyPage_0.Visible = true;
                    this.tickSymbolPropertyPage_0.Visible = false;
                    break;
            }
            this.int_0--;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            switch (this.int_0)
            {
                case 0:
                    this.mapTemplateTypePage_0.Visible = false;
                    if (this.mapTemplateTypePage_0.m_MapTemplateType != 0)
                    {
                        this.starndFenFuMapTemplatePage_0.Visible = true;
                        this.mapTemplate_0.TKType = 1;
                        break;
                    }
                    this.mapTemplate_0.TKType = 0;
                    this.mapTemplateGeneralPage_0.Visible = true;
                    break;

                case 1:
                    if (this.mapTemplateTypePage_0.m_MapTemplateType != 0)
                    {
                        if (!this.starndFenFuMapTemplatePage_0.CanApply())
                        {
                            return;
                        }
                        this.starndFenFuMapTemplatePage_0.Apply();
                        this.starndFenFuMapTemplatePage_0.Visible = false;
                        this.standardTickSymbolPropertyPage_0.Visible = true;
                        this.btnNext.Text = "完成";
                        break;
                    }
                    if (this.mapTemplateGeneralPage_0.CanApply())
                    {
                        this.mapTemplateGeneralPage_0.Apply();
                        if (this.mapTemplateGeneralPage_0.UseMapGrid)
                        {
                            if (this.m_pMapGrid == null)
                            {
                                this.method_0();
                                this.labelFormatPropertyPage_0.SetObjects(this.m_pMapGrid);
                                this.gridAxisPropertyPage_0.SetObjects(this.m_pMapGrid);
                                this.tickSymbolPropertyPage_0.SetObjects(this.m_pMapGrid);
                            }
                            this.mapTemplateGeneralPage_0.Visible = false;
                            this.labelFormatPropertyPage_0.Visible = true;
                        }
                        else
                        {
                            this.mapTemplateGeneralPage_0.Visible = false;
                            this.otherGridPropertyPage_0.Visible = true;
                            this.btnNext.Text = "完成";
                        }
                        break;
                    }
                    MessageBox.Show("请检查输入是否正确!");
                    return;

                case 2:
                    if (this.mapTemplateTypePage_0.m_MapTemplateType != 0)
                    {
                        this.standardTickSymbolPropertyPage_0.Apply();
                        base.DialogResult = DialogResult.OK;
                        break;
                    }
                    if (!this.mapTemplateGeneralPage_0.UseMapGrid)
                    {
                        this.otherGridPropertyPage_0.Apply();
                        base.DialogResult = DialogResult.OK;
                        break;
                    }
                    this.labelFormatPropertyPage_0.Apply();
                    this.labelFormatPropertyPage_0.Visible = false;
                    this.gridAxisPropertyPage_0.Visible = true;
                    break;

                case 3:
                    this.gridAxisPropertyPage_0.Apply();
                    this.gridAxisPropertyPage_0.Visible = false;
                    this.tickSymbolPropertyPage_0.Visible = true;
                    this.btnNext.Text = "完成";
                    break;

                case 4:
                    this.tickSymbolPropertyPage_0.Apply();
                    base.DialogResult = DialogResult.OK;
                    return;
            }
            this.int_0++;
        }

        private void JLKMapTemplateWizard_Load(object sender, EventArgs e)
        {
            this.mapTemplateTypePage_0.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(this.mapTemplateTypePage_0);
            this.mapTemplateGeneralPage_0.MapTemplate = this.mapTemplate_0;
            this.mapTemplateGeneralPage_0.Dock = DockStyle.Fill;
            this.mapTemplateGeneralPage_0.Visible = false;
            this.panel1.Controls.Add(this.mapTemplateGeneralPage_0);
            this.otherGridPropertyPage_0.MapTemplate = this.mapTemplate_0;
            this.otherGridPropertyPage_0.Dock = DockStyle.Fill;
            this.otherGridPropertyPage_0.Visible = false;
            this.panel1.Controls.Add(this.otherGridPropertyPage_0);
            this.starndFenFuMapTemplatePage_0.MapTemplate = this.mapTemplate_0;
            this.starndFenFuMapTemplatePage_0.Dock = DockStyle.Fill;
            this.starndFenFuMapTemplatePage_0.Visible = false;
            this.panel1.Controls.Add(this.starndFenFuMapTemplatePage_0);
            this.standardTickSymbolPropertyPage_0.MapTemplate = this.mapTemplate_0;
            this.standardTickSymbolPropertyPage_0.Dock = DockStyle.Fill;
            this.standardTickSymbolPropertyPage_0.Visible = false;
            this.panel1.Controls.Add(this.standardTickSymbolPropertyPage_0);
            this.labelFormatPropertyPage_0.Dock = DockStyle.Fill;
            this.labelFormatPropertyPage_0.Visible = false;
            this.panel1.Controls.Add(this.labelFormatPropertyPage_0);
            this.gridAxisPropertyPage_0.Dock = DockStyle.Fill;
            this.gridAxisPropertyPage_0.Visible = false;
            this.panel1.Controls.Add(this.gridAxisPropertyPage_0);
            this.tickSymbolPropertyPage_0.Dock = DockStyle.Fill;
            this.tickSymbolPropertyPage_0.Visible = false;
            this.panel1.Controls.Add(this.tickSymbolPropertyPage_0);
        }

        private void method_0()
        {
            this.m_pMapGrid = new MeasuredGridClass();
            if (this.imapFrame_0 != null)
            {
                this.m_pMapGrid.SetDefaults(this.imapFrame_0);
            }
            (this.m_pMapGrid as IMeasuredGrid).XOrigin = 0.0;
            (this.m_pMapGrid as IMeasuredGrid).Units = esriUnits.esriMeters;
            (this.m_pMapGrid as IMeasuredGrid).YOrigin = 0.0;
            (this.m_pMapGrid as IMeasuredGrid).XIntervalSize = 200.0;
            (this.m_pMapGrid as IMeasuredGrid).YIntervalSize = 200.0;
            (this.m_pMapGrid as IMeasuredGrid).FixedOrigin = true;
            IGridLabel labelFormat = this.m_pMapGrid.LabelFormat;
            ITextSymbol symbol = new TextSymbolClass
            {
                Font = labelFormat.Font,
                Color = labelFormat.Color,
                Text = labelFormat.DisplayName,
                VerticalAlignment = esriTextVerticalAlignment.esriTVABottom
            };
            labelFormat.Font = symbol.Font;
            labelFormat.Color = symbol.Color;
            labelFormat.LabelOffset = 6.0;
            this.m_pMapGrid.LabelFormat = labelFormat;
            if (labelFormat is IMixedFontGridLabel2)
            {
                (labelFormat as IMixedFontGridLabel2).NumGroupedDigits = 0;
            }
        }

        public IMapFrame MapFrame
        {
            get { return this.imapFrame_0; }
            set { this.imapFrame_0 = value; }
        }

        public IMapGrid MapGrid
        {
            get { return this.m_pMapGrid; }
        }

        public MapTemplate MapTemplate
        {
            get { return this.mapTemplate_0; }
        }
    }
}