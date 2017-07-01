using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.Library
{
    public partial class frmInputCoordinate : Form
    {
        private IContainer icontainer_0 = null;
        private IGeometry igeometry_0 = null;
        private IMap imap_0 = null;

        public frmInputCoordinate()
        {
            this.InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmInputXY txy = new frmInputXY();
            if (txy.ShowDialog() == DialogResult.OK)
            {
                ListViewItem item = new ListViewItem(new string[] {txy.X.ToString(), txy.Y.ToString()});
                this.listView1.Items.Add(item);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedIndices.Count == 1)
            {
                this.listView1.Items.RemoveAt(this.listView1.SelectedIndices[0]);
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedIndices.Count == 1)
            {
                ListViewItem item = this.listView1.SelectedItems[0];
                double num = double.Parse(item.Text);
                double num2 = double.Parse(item.SubItems[1].Text);
                frmInputXY txy = new frmInputXY
                {
                    X = num,
                    Y = num2
                };
                if (txy.ShowDialog() == DialogResult.OK)
                {
                    item.Text = num.ToString();
                    item.SubItems[1].Text = num2.ToString();
                }
            }
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedIndices.Count == 1)
            {
                int index = this.listView1.SelectedIndices[0];
                if (index != (this.listView1.Items.Count - 1))
                {
                    ListViewItem item = this.listView1.SelectedItems[0];
                    this.listView1.Items.RemoveAt(index);
                    this.listView1.Items.Insert(index + 1, item);
                }
            }
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedIndices.Count == 1)
            {
                int index = this.listView1.SelectedIndices[0];
                if (index != 0)
                {
                    ListViewItem item = this.listView1.SelectedItems[0];
                    this.listView1.Items.RemoveAt(index);
                    this.listView1.Items.Insert(index - 1, item);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            IPointCollection points = new PolygonClass();
            if (this.listView1.Items.Count < 3)
            {
                MessageBox.Show("点数必须大于或等于3个点");
            }
            else
            {
                for (int i = 0; i < this.listView1.Items.Count; i++)
                {
                    ListViewItem item = this.listView1.Items[i];
                    double num2 = double.Parse(item.Text);
                    double num3 = double.Parse(item.SubItems[1].Text);
                    if (this.imap_0.MapUnits != esriUnits.esriKilometers)
                    {
                        num2 *= 1000.0;
                        num3 *= 1000.0;
                    }
                    IPoint inPoint = new PointClass
                    {
                        X = num2,
                        Y = num3
                    };
                    object before = Missing.Value;
                    points.AddPoint(inPoint, ref before, ref before);
                }
                (points as IPolygon).Close();
                this.igeometry_0 = points as IGeometry;
                base.DialogResult = DialogResult.OK;
            }
        }

        private void frmInputCoordinate_Load(object sender, EventArgs e)
        {
        }

        public IGeometry Geometry
        {
            get { return this.igeometry_0; }
        }

        public IMap Map
        {
            set { this.imap_0 = value; }
        }
    }
}