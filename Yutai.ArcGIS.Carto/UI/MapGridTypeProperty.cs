using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class MapGridTypeProperty : UserControl
    {
        private Container container_0 = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        protected IMapFrame m_pMapFrame = null;
        private RadioGroup radioMapGridType;
        private string[] string_0 = new string[] { "经纬网", "方里网", "索引格网" };
        private TextEdit txtMapGridName;

        public MapGridTypeProperty()
        {
            this.InitializeComponent();
            this.txtMapGridName.Text = "经纬网";
        }

        public IMapGrid CreateMapGrid()
        {
            int selectedIndex = this.radioMapGridType.SelectedIndex;
            IMapGrid grid = null;
            switch (selectedIndex)
            {
                case 0:
                    grid = new GraticuleClass();
                    break;

                case 1:
                    grid = new MeasuredGridClass();
                    break;

                case 2:
                    grid = new IndexGridClass();
                    break;
            }
            if (grid == null)
            {
                return null;
            }
            grid.SetDefaults(this.m_pMapFrame);
            grid.Name = this.txtMapGridName.Text;
            if (grid is IMeasuredGrid)
            {
                try
                {
                    double mapScale = this.m_pMapFrame.Map.MapScale;
                }
                catch
                {
                }
                IEnvelope extent = (this.m_pMapFrame.Map as IActiveView).Extent;
                (grid as IMeasuredGrid).XOrigin = extent.XMin;
                (grid as IMeasuredGrid).YOrigin = extent.YMin;
                (grid as IMeasuredGrid).FixedOrigin = true;
            }
            return grid;
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
            this.groupBox2 = new GroupBox();
            this.txtMapGridName = new TextEdit();
            this.label1 = new Label();
            this.groupBox1 = new GroupBox();
            this.radioMapGridType = new RadioGroup();
            this.groupBox2.SuspendLayout();
            this.txtMapGridName.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            this.radioMapGridType.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox2.Controls.Add(this.txtMapGridName);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(8, 0x70);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0xd8, 0x48);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.txtMapGridName.EditValue = "";
            this.txtMapGridName.Location = new System.Drawing.Point(0x48, 30);
            this.txtMapGridName.Name = "txtMapGridName";
            this.txtMapGridName.Size = new Size(0x80, 0x15);
            this.txtMapGridName.TabIndex = 1;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 0x20);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x36, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "格网名称";
            this.groupBox1.Controls.Add(this.radioMapGridType);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xd8, 0x60);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "格网类型";
            this.radioMapGridType.Location = new System.Drawing.Point(0x10, 0x10);
            this.radioMapGridType.Name = "radioMapGridType";
            this.radioMapGridType.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioMapGridType.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "经纬网"), new RadioGroupItem(null, "方里网"), new RadioGroupItem(null, "参考格网") });
            this.radioMapGridType.Size = new Size(160, 0x48);
            this.radioMapGridType.TabIndex = 1;
            this.radioMapGridType.SelectedIndexChanged += new EventHandler(this.radioMapGridType_SelectedIndexChanged);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "MapGridTypeProperty";
            base.Size = new Size(0xf8, 200);
            this.groupBox2.ResumeLayout(false);
            this.txtMapGridName.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.radioMapGridType.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void radioMapGridType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.radioMapGridType.SelectedIndex != -1)
            {
                this.txtMapGridName.Text = this.string_0[this.radioMapGridType.SelectedIndex];
            }
        }

        public IMapFrame MapFrame
        {
            set
            {
                this.m_pMapFrame = value;
            }
        }
    }
}

