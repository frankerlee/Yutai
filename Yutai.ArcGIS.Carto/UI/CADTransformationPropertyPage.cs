using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class CADTransformationPropertyPage : UserControl, ILayerAndStandaloneTablePropertyPage
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Container container_0 = null;
        private ICadTransformations icadTransformations_0 = null;

        public CADTransformationPropertyPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            if (this.bool_0)
            {
                this.bool_0 = false;
                double angle = 0.0;
                double scale = 0.0;
                try
                {
                    angle = double.Parse(this.txtRotate.Text);
                }
                catch
                {
                }
                try
                {
                    scale = double.Parse(this.txtScale.Text);
                }
                catch
                {
                }
                WKSPoint point = new WKSPoint();
                try
                {
                    point.X = double.Parse(this.txtFromXCoord1.Text);
                }
                catch
                {
                }
                try
                {
                    point.Y = double.Parse(this.txtFromYCoord1.Text);
                }
                catch
                {
                }
                WKSPoint point2 = new WKSPoint();
                try
                {
                    point2.X = double.Parse(this.txtToXCoord1.Text);
                }
                catch
                {
                }
                try
                {
                    point2.Y = double.Parse(this.txtToYCoord1.Text);
                }
                catch
                {
                }
                WKSPoint point3 = new WKSPoint();
                try
                {
                    point3.X = double.Parse(this.txtFromXCoord1.Text);
                }
                catch
                {
                }
                try
                {
                    point3.Y = double.Parse(this.txtFromYCoord1.Text);
                }
                catch
                {
                }
                WKSPoint point4 = new WKSPoint();
                try
                {
                    point4.X = double.Parse(this.txtToXCoord1.Text);
                }
                catch
                {
                }
                try
                {
                    point4.Y = double.Parse(this.txtToYCoord1.Text);
                }
                catch
                {
                }
                this.icadTransformations_0.EnableTransformations = this.checkEdit1.Checked;
                switch (this.radioGroup1.SelectedIndex)
                {
                    case 0:
                        this.icadTransformations_0.TransformMode = esriCadTransform.esriCadTransformByWorldFile;
                        this.icadTransformations_0.WorldFileName = this.txtWordFileName.Text;
                        break;

                    case 1:
                        this.icadTransformations_0.TransformMode = esriCadTransform.esriCadTransformByPoints;
                        this.icadTransformations_0.SetFromToTransform(ref point, ref point3, ref point2, ref point4);
                        break;

                    case 2:
                        this.icadTransformations_0.TransformMode = esriCadTransform.esriCadTransformByRst;
                        this.icadTransformations_0.SetTransformation(ref point, ref point3, angle, scale);
                        break;
                }
            }
            return true;
        }

        private void btnOpenWFName_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                Filter = "CAD World Files(*.wld)|*.wld",
                Multiselect = false
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txtWordFileName.Text = dialog.FileName;
                this.bool_0 = true;
            }
        }

        private void CADTransformationPropertyPage_Load(object sender, EventArgs e)
        {
            this.method_0();
            this.bool_1 = true;
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkEdit1.Checked)
            {
                this.groupBox1.Visible = true;
                this.panel1.Visible = true;
                this.panel2.Visible = true;
            }
            else
            {
                this.groupBox1.Visible = false;
                this.panel1.Visible = false;
                this.panel2.Visible = false;
            }
        }

 private void method_0()
        {
            WKSPoint point;
            WKSPoint point3;
            this.checkEdit1.Checked = this.icadTransformations_0.EnableTransformations;
            if (this.checkEdit1.Checked)
            {
                this.groupBox1.Visible = true;
                this.panel1.Visible = true;
                this.panel2.Visible = true;
            }
            else
            {
                this.groupBox1.Visible = false;
                this.panel1.Visible = false;
                this.panel2.Visible = false;
            }
            this.radioGroup1.SelectedIndex = (int) this.icadTransformations_0.TransformMode;
            switch (this.icadTransformations_0.TransformMode)
            {
                case esriCadTransform.esriCadTransformByWorldFile:
                    this.txtWordFileName.Text = this.icadTransformations_0.WorldFileName;
                    break;

                case esriCadTransform.esriCadTransformByPoints:
                    WKSPoint point2;
                    WKSPoint point4;
                    this.icadTransformations_0.GetFromToTransform(out point, out point2, out point3, out point4);
                    this.txtFromXCoord1.Text = point.X.ToString();
                    this.txtFromYCoord1.Text = point.Y.ToString();
                    this.txtFromXCoord2.Text = point2.X.ToString();
                    this.txtFromYCoord2.Text = point2.Y.ToString();
                    this.txtToXCoord1.Text = point3.X.ToString();
                    this.txtToYCoord1.Text = point3.Y.ToString();
                    this.txtToXCoord2.Text = point4.X.ToString();
                    this.txtToYCoord2.Text = point4.Y.ToString();
                    break;

                case esriCadTransform.esriCadTransformByRst:
                    double num;
                    double num2;
                    this.icadTransformations_0.GetTransformation(out point, out point3, out num, out num2);
                    this.txtFromXCoord1.Text = point.X.ToString();
                    this.txtFromYCoord1.Text = point.Y.ToString();
                    this.txtToXCoord1.Text = point3.X.ToString();
                    this.txtToYCoord1.Text = point3.Y.ToString();
                    this.txtScale.Text = num2.ToString();
                    this.txtRotate.Text = num.ToString();
                    break;
            }
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.radioGroup1.SelectedIndex == 0)
            {
                this.panel1.Visible = true;
                this.panel2.Visible = false;
            }
            else if (this.radioGroup1.SelectedIndex == 1)
            {
                this.panel1.Visible = false;
                this.panel2.Visible = true;
                this.panel3.Visible = true;
                this.panel4.Visible = false;
            }
            else if (this.radioGroup1.SelectedIndex == 2)
            {
                this.panel1.Visible = false;
                this.panel2.Visible = true;
                this.panel3.Visible = false;
                this.panel4.Visible = true;
            }
            if (this.bool_1)
            {
                this.bool_0 = true;
            }
        }

        private void txtFromXCoord1_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                double.Parse(this.txtFromXCoord1.Text);
                this.txtFromXCoord1.ForeColor = SystemColors.WindowText;
                if (this.bool_1)
                {
                    this.bool_0 = true;
                }
            }
            catch
            {
                this.txtFromXCoord1.ForeColor = Color.Red;
            }
        }

        private void txtFromXCoord2_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                double.Parse(this.txtFromXCoord2.Text);
                this.txtFromXCoord2.ForeColor = SystemColors.WindowText;
                if (this.bool_1)
                {
                    this.bool_0 = true;
                }
            }
            catch
            {
                this.txtFromXCoord2.ForeColor = Color.Red;
            }
        }

        private void txtFromYCoord1_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                double.Parse(this.txtFromYCoord1.Text);
                this.txtFromYCoord1.ForeColor = SystemColors.WindowText;
                if (this.bool_1)
                {
                    this.bool_0 = true;
                }
            }
            catch
            {
                this.txtFromYCoord1.ForeColor = Color.Red;
            }
        }

        private void txtFromYCoord2_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                double.Parse(this.txtFromYCoord2.Text);
                this.txtFromYCoord2.ForeColor = SystemColors.WindowText;
                if (this.bool_1)
                {
                    this.bool_0 = true;
                }
            }
            catch
            {
                this.txtFromYCoord2.ForeColor = Color.Red;
            }
        }

        private void txtRotate_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                double.Parse(this.txtRotate.Text);
                this.txtRotate.ForeColor = SystemColors.WindowText;
                if (this.bool_1)
                {
                    this.bool_0 = true;
                }
            }
            catch
            {
                this.txtRotate.ForeColor = Color.Red;
            }
        }

        private void txtScale_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                double.Parse(this.txtScale.Text);
                this.txtScale.ForeColor = SystemColors.WindowText;
                if (this.bool_1)
                {
                    this.bool_0 = true;
                }
            }
            catch
            {
                this.txtScale.ForeColor = Color.Red;
            }
        }

        private void txtToXCoord1_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                double.Parse(this.txtToXCoord1.Text);
                this.txtToXCoord1.ForeColor = SystemColors.WindowText;
                if (this.bool_1)
                {
                    this.bool_0 = true;
                }
            }
            catch
            {
                this.txtToXCoord1.ForeColor = Color.Red;
            }
        }

        private void txtToXCoord2_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                double.Parse(this.txtToXCoord2.Text);
                this.txtToXCoord2.ForeColor = SystemColors.WindowText;
                if (this.bool_1)
                {
                    this.bool_0 = true;
                }
            }
            catch
            {
                this.txtToXCoord2.ForeColor = Color.Red;
            }
        }

        private void txtToYCoord1_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                double.Parse(this.txtToYCoord1.Text);
                this.txtToYCoord1.ForeColor = SystemColors.WindowText;
                if (this.bool_1)
                {
                    this.bool_0 = true;
                }
            }
            catch
            {
                this.txtToYCoord1.ForeColor = Color.Red;
            }
        }

        private void txtToYCoord2_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                double.Parse(this.txtToYCoord2.Text);
                this.txtToYCoord2.ForeColor = SystemColors.WindowText;
                if (this.bool_1)
                {
                    this.bool_0 = true;
                }
            }
            catch
            {
                this.txtToYCoord2.ForeColor = Color.Red;
            }
        }

        public IBasicMap FocusMap
        {
            set
            {
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_0;
            }
        }

        public object SelectItem
        {
            set
            {
                this.icadTransformations_0 = value as ICadTransformations;
            }
        }
    }
}

