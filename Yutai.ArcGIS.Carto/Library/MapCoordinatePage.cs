using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Carto.MapCartoTemplateLib;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.Library
{
    public partial class MapCoordinatePage : UserControl, IPropertyPage
    {
        private bool bool_0 = false;
        private IContainer icontainer_0 = null;
        private MapTemplateApplyHelp mapTemplateApplyHelp_0 = null;

        public MapCoordinatePage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.rdoMapNo.Checked)
            {
                this.mapTemplateApplyHelp_0.MapNo = this.txtMapNo.Text;
            }
            else if (this.radioButton1.Checked)
            {
                this.mapTemplateApplyHelp_0.HasStrip = this.checkBox1.Checked;
                this.mapTemplateApplyHelp_0.XOffset = double.Parse(this.textBox1.Text);
                this.mapTemplateApplyHelp_0.SetJWD(double.Parse(this.txtJD.Text), double.Parse(this.txtWD.Text), double.Parse(this.txtJC.Text), double.Parse(this.txtWC.Text), double.Parse(this.txtScale.Text));
            }
            else
            {
                IPoint point = new PointClass();
                point.PutCoords(double.Parse(this.txtLeftUpperX.Text), double.Parse(this.txtLeftUpperY.Text));
                IPoint point2 = new PointClass();
                point2.PutCoords(double.Parse(this.txtRightUpperX.Text), double.Parse(this.txtRightUpperY.Text));
                IPoint point3 = new PointClass();
                point3.PutCoords(double.Parse(this.txtRightLowX.Text), double.Parse(this.txtRightLowY.Text));
                IPoint point4 = new PointClass();
                point4.PutCoords(double.Parse(this.txtLeftLowX.Text), double.Parse(this.txtLeftLowY.Text));
                this.mapTemplateApplyHelp_0.SetRouneCoordinate(point4, point, point2, point3, double.Parse(this.txtProjScale.Text));
            }
        }

        public bool CanApply()
        {
            if (this.rdoMapNo.Checked)
            {
                MapNoAssistant assistant = MapNoAssistantFactory.CreateMapNoAssistant(this.txtMapNo.Text);
                if (assistant == null)
                {
                    MessageBox.Show("图号输入不正确!");
                    return false;
                }
                if (!assistant.Validate())
                {
                    MessageBox.Show("图号输入不正确!");
                    return false;
                }
                return true;
            }
            if (this.radioButton1.Checked)
            {
                try
                {
                    double.Parse(this.txtJD.Text);
                    double.Parse(this.txtWD.Text);
                    double.Parse(this.txtJC.Text);
                    double.Parse(this.txtWC.Text);
                    double.Parse(this.txtScale.Text);
                    goto Label_0160;
                }
                catch
                {
                    return false;
                }
            }
            try
            {
                double.Parse(this.txtLeftUpperX.Text);
                double.Parse(this.txtLeftUpperY.Text);
                double.Parse(this.txtRightUpperX.Text);
                double.Parse(this.txtRightUpperY.Text);
                double.Parse(this.txtRightLowX.Text);
                double.Parse(this.txtRightLowY.Text);
                double.Parse(this.txtLeftLowX.Text);
                double.Parse(this.txtLeftLowY.Text);
            }
            catch
            {
                return false;
            }
        Label_0160:
            return true;
        }

        public void Cancel()
        {
        }

 private void MapCoordinatePage_Load(object sender, EventArgs e)
        {
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.panel2.Visible = this.radioButton1.Checked;
        }

        private void rdoCoordinate_CheckedChanged(object sender, EventArgs e)
        {
            this.panel3.Visible = this.rdoCoordinate.Checked;
        }

        private void rdoMapNo_CheckedChanged(object sender, EventArgs e)
        {
            this.panel1.Visible = this.rdoMapNo.Checked;
        }

        public void ResetControl()
        {
        }

        public void SetObjects(object object_0)
        {
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_0;
            }
        }

        int IPropertyPage.Height
        {
            get
            {
                return base.Height;
            }
        }

        int IPropertyPage.Width
        {
            get
            {
                return base.Width;
            }
        }

        public MapTemplateApplyHelp MapTemplateHelp
        {
            get
            {
                return this.mapTemplateApplyHelp_0;
            }
            set
            {
                this.mapTemplateApplyHelp_0 = value;
            }
        }

        public string Title
        {
            get
            {
                return "坐标";
            }
            set
            {
            }
        }
    }
}

