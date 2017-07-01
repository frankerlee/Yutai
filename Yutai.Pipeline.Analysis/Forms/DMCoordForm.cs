using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geometry;

namespace Yutai.Pipeline.Analysis.Forms
{
    public partial class DMCoordForm : XtraForm
    {
        public IPolyline pPolyline;

        private IContainer icontainer_0 = null;


        public DMCoordForm()
        {
            this.InitializeComponent();
        }

        private void DMCoordForm_Load(object obj, EventArgs eventArg)
        {
            if (this.pPolyline != null)
            {
                TextBox str = this.textBox1;
                double x = this.pPolyline.FromPoint.X;
                str.Text = x.ToString();
                TextBox textBox = this.textBox2;
                x = this.pPolyline.FromPoint.Y;
                textBox.Text = x.ToString();
                TextBox str1 = this.textBox3;
                x = this.pPolyline.ToPoint.X;
                str1.Text = x.ToString();
                TextBox textBox1 = this.textBox4;
                x = this.pPolyline.ToPoint.Y;
                textBox1.Text = x.ToString();
            }
        }

        private void ModifyBut_Click(object obj, EventArgs eventArg)
        {
            IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
            pointClass.X = (Convert.ToDouble(this.textBox1.Text));
            pointClass.Y = (Convert.ToDouble(this.textBox2.Text));
            IPoint point = new ESRI.ArcGIS.Geometry.Point();
            point.X = (Convert.ToDouble(this.textBox3.Text));
            point.Y = (Convert.ToDouble(this.textBox4.Text));
            this.pPolyline.FromPoint = (pointClass);
            this.pPolyline.ToPoint = (point);
        }
    }
}