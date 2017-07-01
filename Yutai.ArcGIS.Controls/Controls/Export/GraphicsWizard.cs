using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.Controls.Export
{
    public partial class GraphicsWizard : Form
    {
        private object m_pDataSource = null;
        private GraphicsSelectData m_pGraphicsSelectData = new GraphicsSelectData();
        private GraphicsSet m_pGraphicsSet = new GraphicsSet();
        private SeriesChart m_pSeriesChart = new SeriesChart();
        private int m_Step = 0;

        public GraphicsWizard()
        {
            this.InitializeComponent();
            GraphicHelper.Init();
            this.m_pGraphicsSelectData.Dock = DockStyle.Fill;
            this.m_pGraphicsSelectData.Visible = false;
            this.panel1.Controls.Add(this.m_pGraphicsSelectData);
            this.m_pGraphicsSet.Dock = DockStyle.Fill;
            this.m_pGraphicsSet.Visible = false;
            this.panel1.Controls.Add(this.m_pGraphicsSet);
            this.m_pSeriesChart.Dock = DockStyle.Fill;
            this.m_pSeriesChart.Visible = false;
            this.panel1.Controls.Add(this.m_pSeriesChart);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            switch (this.m_Step)
            {
                case 0:
                    return;

                case 1:
                    if (this.m_pDataSource == null)
                    {
                        this.m_pGraphicsSelectData.Visible = true;
                        this.m_pGraphicsSet.Visible = false;
                    }
                    else
                    {
                        this.m_pGraphicsSet.Visible = true;
                        this.m_pSeriesChart.Visible = false;
                        this.btnNext.Text = "下一步";
                    }
                    this.btnLast.Enabled = false;
                    break;

                case 2:
                    this.m_pGraphicsSet.Visible = true;
                    this.m_pSeriesChart.Visible = false;
                    this.btnNext.Text = "下一步";
                    break;
            }
            this.m_Step--;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            switch (this.m_Step)
            {
                case 0:
                    if (this.m_pDataSource != null)
                    {
                        if (!this.m_pGraphicsSet.Do())
                        {
                            return;
                        }
                        this.btnNext.Text = "完成";
                        this.m_pGraphicsSet.Visible = false;
                        this.m_pSeriesChart.Visible = true;
                        break;
                    }
                    if (this.m_pGraphicsSelectData.Do())
                    {
                        this.m_pGraphicsSelectData.Visible = false;
                        this.m_pGraphicsSet.Visible = true;
                        break;
                    }
                    return;

                case 1:
                    if (this.m_pDataSource != null)
                    {
                        base.DialogResult = DialogResult.OK;
                        GraphicHelper.pGraphicHelper = null;
                    }
                    else
                    {
                        if (!this.m_pGraphicsSet.Do())
                        {
                            return;
                        }
                        this.btnNext.Text = "完成";
                        this.m_pGraphicsSet.Visible = false;
                        this.m_pSeriesChart.Visible = true;
                    }
                    goto Label_0123;

                case 2:
                    base.DialogResult = DialogResult.OK;
                    GraphicHelper.pGraphicHelper = null;
                    return;

                default:
                    goto Label_0123;
            }
            this.btnLast.Enabled = true;
            Label_0123:
            this.m_Step++;
        }

        private void ExportToExcelWizard_Load(object sender, EventArgs e)
        {
            if (this.m_pDataSource == null)
            {
                this.m_pGraphicsSelectData.Visible = true;
            }
            else
            {
                this.m_pGraphicsSet.Visible = true;
            }
        }

        public object DataSource
        {
            set
            {
                this.m_pDataSource = value;
                GraphicHelper.pGraphicHelper.DataSource = this.m_pDataSource;
            }
        }

        public IMap Map
        {
            set { this.m_pGraphicsSelectData.Map = value; }
        }
    }
}