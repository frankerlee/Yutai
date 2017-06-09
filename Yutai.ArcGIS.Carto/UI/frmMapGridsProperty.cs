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
    public class frmMapGridsProperty : Form
    {
        private SimpleButton btnAdd;
        private SimpleButton btnClose;
        private SimpleButton btnDelete;
        private SimpleButton btnOK;
        private SimpleButton btnProperty;
        private CheckedListBox checkedListBox1;
        private Container container_0 = null;
        private IMapGrids imapGrids_0 = null;
        private IPageLayout ipageLayout_0 = null;
        private Label label1;

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

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmMapGridsProperty_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < this.imapGrids_0.MapGridCount; i++)
            {
                IMapGrid grid = this.imapGrids_0.get_MapGrid(i);
                this.checkedListBox1.Items.Add(new MapGridWrap(grid), grid.Visible);
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMapGridsProperty));
            this.label1 = new Label();
            this.checkedListBox1 = new CheckedListBox();
            this.btnClose = new SimpleButton();
            this.btnAdd = new SimpleButton();
            this.btnDelete = new SimpleButton();
            this.btnProperty = new SimpleButton();
            this.btnOK = new SimpleButton();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "格网";
            this.checkedListBox1.Location = new Point(0x10, 0x20);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new Size(0xe0, 0x94);
            this.checkedListBox1.TabIndex = 1;
            this.checkedListBox1.SelectedIndexChanged += new EventHandler(this.checkedListBox1_SelectedIndexChanged);
            this.checkedListBox1.ItemCheck += new ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            this.btnClose.DialogResult = DialogResult.Cancel;
            this.btnClose.Location = new Point(200, 0xc0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(0x38, 0x18);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "取消";
            this.btnAdd.Location = new Point(0xf8, 0x20);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(0x38, 0x18);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "添加";
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.btnDelete.Location = new Point(0xf8, 0x40);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(0x38, 0x18);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.btnProperty.Location = new Point(0xf8, 0x60);
            this.btnProperty.Name = "btnProperty";
            this.btnProperty.Size = new Size(0x38, 0x18);
            this.btnProperty.TabIndex = 5;
            this.btnProperty.Text = "样式";
            this.btnProperty.Click += new EventHandler(this.btnProperty_Click);
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(120, 0xc0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x38, 0x18);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x138, 0xe5);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnProperty);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.btnAdd);
            base.Controls.Add(this.btnClose);
            base.Controls.Add(this.checkedListBox1);
            base.Controls.Add(this.label1);
            base.Icon = ((System.Drawing.Icon)resources.GetObject("$Icon"));
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmMapGridsProperty";
            this.Text = "格网属性";
            base.Load += new EventHandler(this.frmMapGridsProperty_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        internal class MapGridWrap
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

