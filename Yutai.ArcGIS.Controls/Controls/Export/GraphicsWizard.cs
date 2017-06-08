using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.Controls.Export
{
    public class GraphicsWizard : Form
    {
        private SimpleButton btnCancel;
        private SimpleButton btnLast;
        private SimpleButton btnNext;
        private Container components = null;
        private GroupBox groupBox1;
        private object m_pDataSource = null;
        private GraphicsSelectData m_pGraphicsSelectData = new GraphicsSelectData();
        private GraphicsSet m_pGraphicsSet = new GraphicsSet();
        private SeriesChart m_pSeriesChart = new SeriesChart();
        private int m_Step = 0;
        private Panel panel1;

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

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraphicsWizard));
            this.panel1 = new Panel();
            this.groupBox1 = new GroupBox();
            this.btnLast = new SimpleButton();
            this.btnNext = new SimpleButton();
            this.btnCancel = new SimpleButton();
            base.SuspendLayout();
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x1a8, 0x108);
            this.panel1.TabIndex = 0;
            this.groupBox1.Location = new Point(8, 0x108);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x198, 8);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.btnLast.Location = new Point(200, 280);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new Size(0x40, 0x18);
            this.btnLast.TabIndex = 2;
            this.btnLast.Text = "上一步";
            this.btnLast.Click += new EventHandler(this.btnLast_Click);
            this.btnNext.Location = new Point(0x110, 280);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(0x40, 0x18);
            this.btnNext.TabIndex = 3;
            this.btnNext.Text = "下一步";
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0x158, 280);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x40, 0x18);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x1a8, 0x135);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnNext);
            base.Controls.Add(this.btnLast);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.panel1);
            base.Icon = (Icon) resources.GetObject("$this.Icon");
            base.Name = "GraphicsWizard";
            this.Text = "图表向导";
            base.Load += new EventHandler(this.ExportToExcelWizard_Load);
            base.ResumeLayout(false);
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
            set
            {
                this.m_pGraphicsSelectData.Map = value;
            }
        }
    }
}

