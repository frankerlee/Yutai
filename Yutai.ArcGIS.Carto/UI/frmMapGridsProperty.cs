using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using Yutai.ArcGIS.Controls.SymbolUI;
using IPropertyPage = Yutai.ArcGIS.Common.BaseClasses.IPropertyPage;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class frmMapGridsProperty : Form
    {
        private Container container_0 = null;
        private IMapGrids imapGrids_0 = null;
        private IPageLayout ipageLayout_0 = null;

        public frmMapGridsProperty(IPageLayout ipageLayout_1, IMapGrids imapGrids_1)
        {
            this.InitializeComponent();
            this.imapGrids_0 = imapGrids_1;
            this.ipageLayout_0 = ipageLayout_1;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            IGraphicsContainer container = this.ipageLayout_0 as IGraphicsContainer;
            NewMapGridWizard wizard = new NewMapGridWizard {
                MapFrame = this.imapGrids_0 as IMapFrame,
                GraphicsContainer = container
            };
            if (wizard.ShowDialog() == DialogResult.OK)
            {
                this.imapGrids_0.AddMapGrid(wizard.MapGrid);
                this.checkedListBox1.Items.Add(new MapGridWrap(wizard.MapGrid), wizard.MapGrid.Visible);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.checkedListBox1.SelectedIndex != -1)
            {
                IMapGrid mapGrid = (this.checkedListBox1.SelectedItem as MapGridWrap).MapGrid;
                this.imapGrids_0.DeleteMapGrid(mapGrid);
                this.checkedListBox1.Items.RemoveAt(this.checkedListBox1.SelectedIndex);
                (this.ipageLayout_0 as IActiveView).Refresh();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
        }

        private void btnProperty_Click(object sender, EventArgs e)
        {
            if (this.checkedListBox1.SelectedIndex != -1)
            {
                int selectedIndex = this.checkedListBox1.SelectedIndex;
                IMapGrid mapGrid = (this.checkedListBox1.SelectedItem as MapGridWrap).MapGrid;
                frmElementProperty property = new frmElementProperty {
                    Text = "参考系"
                };
                IPropertyPage page = null;
                if (mapGrid is IProjectedGrid)
                {
                    page = new GridAxisPropertyPage() as IPropertyPage;
                    property.AddPage(page);
                    page = new LabelFormatPropertyPage() as IPropertyPage;
                    property.AddPage(page);
                    page = new TickSymbolPropertyPage() as IPropertyPage;
                    property.AddPage(page);
                    page = new MeasureCoordinatePropertyPage() as IPropertyPage;
                    property.AddPage(page);
                    page = new MeasuredGridPropertyPage() as IPropertyPage;
                    property.AddPage(page);
                }
                else if (mapGrid is IMgrsGrid)
                {
                    page = new MGRSPropertyPage() as IPropertyPage;
                    property.AddPage(page);
                    page = new GridAxisPropertyPage() as IPropertyPage;
                    property.AddPage(page);
                    page = new LabelFormatPropertyPage() as IPropertyPage;
                    property.AddPage(page);
                    page = new TickSymbolPropertyPage() as IPropertyPage;
                    property.AddPage(page);
                }
                else if (mapGrid is IGraticule)
                {
                    page = new GridAxisPropertyPage() as IPropertyPage;
                    property.AddPage(page);
                    page = new GridInteriorLabelsPropertyPage() as IPropertyPage;
                    property.AddPage(page);
                    page = new LabelFormatPropertyPage() as IPropertyPage;
                    property.AddPage(page);
                    page = new TickSymbolPropertyPage() as IPropertyPage;
                    property.AddPage(page);
                    page = new GridHatchPropertyPage() as IPropertyPage;
                    property.AddPage(page);
                    page = new MeasuredGridPropertyPage() as IPropertyPage;
                    property.AddPage(page);
                }
                else if (mapGrid is ICustomOverlayGrid)
                {
                    page = new CustomOverlayGridPropertyPage() as IPropertyPage;
                    property.AddPage(page);
                    page = new GridAxisPropertyPage() as IPropertyPage;
                    property.AddPage(page);
                    page = new LabelFormatPropertyPage() as IPropertyPage;
                    property.AddPage(page);
                    page = new TickSymbolPropertyPage() as IPropertyPage;
                    property.AddPage(page);
                }
                else if (mapGrid is IIndexGrid)
                {
                    page = new GridAxisPropertyPage() as IPropertyPage;
                    property.AddPage(page);
                    page = new IndexGridProperyPage() as IPropertyPage;
                    property.AddPage(page);
                    page = new LabelFormatPropertyPage() as IPropertyPage;
                    property.AddPage(page);
                    page = new TickSymbolPropertyPage() as IPropertyPage;
                    property.AddPage(page);
                }
                if (property.EditProperties(mapGrid))
                {
                    this.imapGrids_0.set_MapGrid(selectedIndex, mapGrid);
                }
            }
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            (this.checkedListBox1.Items[e.Index] as MapGridWrap).MapGrid.Visible = e.NewValue == CheckState.Checked;
            (this.ipageLayout_0 as IActiveView).Refresh();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.checkedListBox1.SelectedIndex != -1)
            {
                this.btnDelete.Enabled = true;
                this.btnProperty.Enabled = true;
            }
            else
            {
                this.btnDelete.Enabled = false;
                this.btnProperty.Enabled = false;
            }
        }

 private void frmMapGridsProperty_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < this.imapGrids_0.MapGridCount; i++)
            {
                IMapGrid grid = this.imapGrids_0.get_MapGrid(i);
                this.checkedListBox1.Items.Add(new MapGridWrap(grid), grid.Visible);
            }
        }

 internal partial class MapGridWrap
        {
            private IMapGrid imapGrid_0 = null;

            internal MapGridWrap(IMapGrid imapGrid_1)
            {
                this.imapGrid_0 = imapGrid_1;
            }

            public override string ToString()
            {
                return this.imapGrid_0.Name;
            }

            internal IMapGrid MapGrid
            {
                get
                {
                    return this.imapGrid_0;
                }
            }
        }
    }
}

