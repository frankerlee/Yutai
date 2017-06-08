using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.UI
{
    public class NewMapGridWizard : Form
    {
        private SimpleButton btnCancel;
        private SimpleButton btnLast;
        private SimpleButton btnNext;
        private SimpleButton btnOK;
        private Container container_0 = null;
        private IGraphicsContainer igraphicsContainer_0 = null;
        private IMapFrame imapFrame_0 = null;
        private IndexGridLabelPropertyPage indexGridLabelPropertyPage_0 = new IndexGridLabelPropertyPage();
        protected IMapGrid m_pMapGrid = null;
        private MapGridBorderPropertyPage mapGridBorderPropertyPage_0 = new MapGridBorderPropertyPage();
        private MapGridCoordinatePropertyPage mapGridCoordinatePropertyPage_0 = new MapGridCoordinatePropertyPage();
        private MapGridStylePropertyPage mapGridStylePropertyPage_0 = new MapGridStylePropertyPage();
        private MapGridTypeProperty mapGridTypeProperty_0 = new MapGridTypeProperty();
        private Panel panel1;
        private short short_0 = 1;

        public NewMapGridWizard()
        {
            this.InitializeComponent();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            this.short_0 = (short) (this.short_0 - 1);
            switch (this.short_0)
            {
                case 1:
                    this.mapGridTypeProperty_0.Visible = true;
                    this.mapGridStylePropertyPage_0.Visible = false;
                    this.btnLast.Enabled = false;
                    return;

                case 2:
                    this.mapGridStylePropertyPage_0.Visible = true;
                    this.indexGridLabelPropertyPage_0.Visible = false;
                    this.mapGridCoordinatePropertyPage_0.Visible = false;
                    return;

                case 3:
                    this.mapGridBorderPropertyPage_0.Visible = false;
                    if (!(this.m_pMapGrid is IIndexGrid))
                    {
                        this.mapGridCoordinatePropertyPage_0.Visible = true;
                        break;
                    }
                    this.indexGridLabelPropertyPage_0.Visible = true;
                    break;

                default:
                    return;
            }
            this.btnNext.Visible = true;
            this.btnOK.Visible = false;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            switch (this.short_0)
            {
                case 1:
                    this.m_pMapGrid = this.mapGridTypeProperty_0.CreateMapGrid();
                    this.mapGridStylePropertyPage_0.MapGrid = this.m_pMapGrid;
                    if (!(this.m_pMapGrid is IIndexGrid))
                    {
                        this.mapGridCoordinatePropertyPage_0.MapGrid = this.m_pMapGrid;
                        break;
                    }
                    this.indexGridLabelPropertyPage_0.MapGrid = this.m_pMapGrid;
                    break;

                case 2:
                    if (!this.mapGridStylePropertyPage_0.Do())
                    {
                        return;
                    }
                    this.mapGridStylePropertyPage_0.Visible = false;
                    if (this.m_pMapGrid is IIndexGrid)
                    {
                        this.indexGridLabelPropertyPage_0.Visible = true;
                        this.indexGridLabelPropertyPage_0.Init();
                    }
                    else
                    {
                        this.mapGridCoordinatePropertyPage_0.Visible = true;
                        this.mapGridCoordinatePropertyPage_0.Init();
                    }
                    goto Label_0166;

                case 3:
                    this.indexGridLabelPropertyPage_0.Visible = false;
                    this.mapGridCoordinatePropertyPage_0.Visible = false;
                    this.mapGridBorderPropertyPage_0.Visible = true;
                    this.mapGridBorderPropertyPage_0.Init();
                    this.btnNext.Visible = false;
                    this.btnOK.Visible = true;
                    goto Label_0166;

                default:
                    goto Label_0166;
            }
            this.mapGridBorderPropertyPage_0.MapGrid = this.m_pMapGrid;
            this.mapGridCoordinatePropertyPage_0.Init();
            this.mapGridTypeProperty_0.Visible = false;
            this.mapGridStylePropertyPage_0.Visible = true;
            this.btnLast.Enabled = true;
        Label_0166:
            this.short_0 = (short) (this.short_0 + 1);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.mapGridBorderPropertyPage_0.OutlineSymbol != null)
            {
                IElement element = new RectangleElementClass();
                IFillSymbol symbol = new SimpleFillSymbolClass {
                    Outline = this.mapGridBorderPropertyPage_0.OutlineSymbol
                };
                (symbol as ISimpleFillSymbol).Style = esriSimpleFillStyle.esriSFSNull;
                (element as IFillShapeElement).Symbol = symbol;
                IEnvelope envelope = (this.imapFrame_0 as IElement).Geometry.Envelope;
                envelope.Expand(1.0, 1.0, false);
                element.Geometry = envelope;
                IFrameElement element2 = new FrameElementClass();
                (element2 as IElement).Geometry = envelope;
                IGroupElement group = new GroupElementClass();
                this.igraphicsContainer_0.MoveElementToGroup(element, group);
                this.igraphicsContainer_0.MoveElementToGroup(element2 as IElement, group);
                this.igraphicsContainer_0.AddElement(group as IElement, 0);
            }
            if (this.mapGridBorderPropertyPage_0.IsGenerateGraphics)
            {
                this.m_pMapGrid.GenerateGraphics(this.imapFrame_0, this.igraphicsContainer_0);
            }
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void InitializeComponent()
        {
            this.btnNext = new SimpleButton();
            this.btnLast = new SimpleButton();
            this.panel1 = new Panel();
            this.btnCancel = new SimpleButton();
            this.btnOK = new SimpleButton();
            base.SuspendLayout();
            this.btnNext.Location = new System.Drawing.Point(0xe0, 280);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(0x40, 0x18);
            this.btnNext.TabIndex = 7;
            this.btnNext.Text = "下一步>";
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.btnLast.Enabled = false;
            this.btnLast.Location = new System.Drawing.Point(0x98, 280);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new Size(0x40, 0x18);
            this.btnLast.TabIndex = 5;
            this.btnLast.Text = "<上一步";
            this.btnLast.Click += new EventHandler(this.btnLast_Click);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x178, 0x108);
            this.panel1.TabIndex = 4;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(0x128, 280);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x40, 0x18);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "取消";
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(0xe0, 280);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x40, 0x18);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "完成";
            this.btnOK.Visible = false;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.ClientSize = new Size(0x178, 0x14d);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnNext);
            base.Controls.Add(this.btnLast);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.btnCancel);
            base.Name = "NewMapGridWizard";
            this.Text = "地理网格向导";
            base.Load += new EventHandler(this.NewMapGridWizard_Load);
            base.ResumeLayout(false);
        }

        private void NewMapGridWizard_Load(object sender, EventArgs e)
        {
            this.mapGridTypeProperty_0.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(this.mapGridTypeProperty_0);
            this.mapGridStylePropertyPage_0.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(this.mapGridStylePropertyPage_0);
            this.mapGridStylePropertyPage_0.Visible = false;
            this.mapGridCoordinatePropertyPage_0.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(this.mapGridCoordinatePropertyPage_0);
            this.mapGridCoordinatePropertyPage_0.Visible = false;
            this.indexGridLabelPropertyPage_0.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(this.indexGridLabelPropertyPage_0);
            this.indexGridLabelPropertyPage_0.Visible = false;
            this.mapGridBorderPropertyPage_0.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(this.mapGridBorderPropertyPage_0);
            this.mapGridBorderPropertyPage_0.Visible = false;
        }

        public IGraphicsContainer GraphicsContainer
        {
            set
            {
                this.igraphicsContainer_0 = value;
            }
        }

        public IMapFrame MapFrame
        {
            set
            {
                this.mapGridTypeProperty_0.MapFrame = value;
                this.mapGridStylePropertyPage_0.MapFrame = value;
                this.imapFrame_0 = value;
            }
        }

        public IMapGrid MapGrid
        {
            get
            {
                return this.m_pMapGrid;
            }
        }
    }
}

