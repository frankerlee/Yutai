using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Catalog
{
    public partial class frmSetGeometryType : Form
    {
        [CompilerGenerated]
        private IContainer icontainer_0 = null;

        public frmSetGeometryType()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedIndex != -1)
            {
                if (this.comboBox1.SelectedIndex == 0)
                {
                    this.ShapeType = esriGeometryType.esriGeometryPoint;
                }
                else if (this.comboBox1.SelectedIndex == 1)
                {
                    this.ShapeType = esriGeometryType.esriGeometryMultipoint;
                }
                else if (this.comboBox1.SelectedIndex == 2)
                {
                    this.ShapeType = esriGeometryType.esriGeometryPolyline;
                }
                else if (this.comboBox1.SelectedIndex == 3)
                {
                    this.ShapeType = esriGeometryType.esriGeometryPolygon;
                }
                base.DialogResult = DialogResult.OK;
            }
        }

 private void frmSetGeometryType_Load(object sender, EventArgs e)
        {
            this.comboBox1.SelectedIndex = 0;
        }

 public esriGeometryType ShapeType
        {
            [CompilerGenerated]
            get
            {
                return this.esriGeometryType_0;
            }
            [CompilerGenerated]
            protected set
            {
                this.esriGeometryType_0 = value;
            }
        }
    }
}

