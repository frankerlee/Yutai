using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Carto.DesignLib;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class frmFenFuTKCreate : Form
    {
        public string connectStr = "";
        private FenFuMapClass fenFuMapClass_0 = new FenFuMapClass();
        private IActiveView iactiveView_0 = null;
        private IContainer icontainer_0 = null;
        public string styleFile = "";

        public frmFenFuTKCreate()
        {
            this.InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.fenFuMapClass_0 = null;
            base.Close();
        }

        private void btnCoord_Click(object sender, EventArgs e)
        {
            if (this.txtTH.Text.Trim() == "")
            {
                MessageBox.Show("请先输入图幅号");
            }
            else
            {
                IList<IPoint> list = null;
                IPoint point = null;
                THTools tools = null;
                if (this.txtTH.Text.Trim() != "")
                {
                    tools = new THTools();
                    bool flag = false;
                    list = tools.GetProjectCoord(this.txtTH.Text.Trim(), true, true, 0, ref flag);
                    if (flag)
                    {
                        if ((list != null) && (list.Count == 4))
                        {
                            point = list[0];
                            if (!point.IsEmpty)
                            {
                                this.txtLeftUpX.Text = point.Y.ToString();
                                this.txtLeftUpY.Text = point.X.ToString();
                            }
                            point = list[1];
                            if (!point.IsEmpty)
                            {
                                this.txtRightUpX.Text = point.Y.ToString();
                                this.txtRightUpY.Text = point.X.ToString();
                            }
                            point = list[2];
                            if (!point.IsEmpty)
                            {
                                this.txtRightLowX.Text = point.Y.ToString();
                                this.txtRightLowY.Text = point.X.ToString();
                            }
                            point = list[3];
                            if (!point.IsEmpty)
                            {
                                this.txtLeftLowX.Text = point.Y.ToString();
                                this.txtLeftLowY.Text = point.X.ToString();
                            }
                        }
                        else
                        {
                            MessageBox.Show("请检查图幅号是否正确。");
                        }
                    }
                    else
                    {
                        MessageBox.Show("请检查图幅号是否正确。");
                    }
                }
                if (list != null)
                {
                    tools = null;
                    point = null;
                    list.Clear();
                    list = null;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            double x = 0.0;
            double y = 0.0;
            double num3 = 0.0;
            double num4 = 0.0;
            double num5 = 0.0;
            double num6 = 0.0;
            double num7 = 0.0;
            double num8 = 0.0;
            IPoint point = new PointClass();
            IPoint point2 = new PointClass();
            IPoint point3 = new PointClass();
            IPoint point4 = new PointClass();
            if (this.txtTH.Text.Trim() == "")
            {
                MessageBox.Show("图号不能为空");
            }
            else
            {
                try
                {
                    x = double.Parse(this.txtLeftUpX.Text.Trim());
                    y = double.Parse(this.txtLeftUpY.Text.Trim());
                    num3 = double.Parse(this.txtRightUpX.Text.Trim());
                    num4 = double.Parse(this.txtRightUpY.Text.Trim());
                    num5 = double.Parse(this.txtRightLowX.Text.Trim());
                    num6 = double.Parse(this.txtRightLowY.Text.Trim());
                    num7 = double.Parse(this.txtLeftLowX.Text.Trim());
                    num8 = double.Parse(this.txtLeftLowY.Text.Trim());
                    point.PutCoords(x, y);
                    point2.PutCoords(num3, num4);
                    point3.PutCoords(num5, num6);
                    point4.PutCoords(num7, num8);
                }
                catch (Exception)
                {
                    MessageBox.Show("请检查坐标是否正确。");
                    return;
                }
                try
                {
                    this.fenFuMapClass_0.LeftUp = point;
                    this.fenFuMapClass_0.RightUp = point2;
                    this.fenFuMapClass_0.RightLow = point3;
                    this.fenFuMapClass_0.LeftLow = point4;
                    this.fenFuMapClass_0.MapTM = this.txtTM.Text.Trim();
                    this.fenFuMapClass_0.MapTH = this.txtTH.Text.Trim();
                    this.fenFuMapClass_0.MapRightUpText = this.txtRightUpTxt.Text.Trim();
                    this.fenFuMapClass_0.MapRightLowTex = this.txtRightLowTxt.Text.Trim();
                    this.fenFuMapClass_0.MapLeftLowText = this.txtLeftLowTxt.Text.Trim();
                    this.fenFuMapClass_0.MapLeftBorderOutText = this.txtZTDW.Text.Trim();
                    this.fenFuMapClass_0.MapScaleText = this.cmbScale.Text.Trim();
                    this.fenFuMapClass_0.MapRow1Col1Text = this.txtR1C1.Text.Trim();
                    this.fenFuMapClass_0.MapRow2Col1Text = this.txtR2C1.Text.Trim();
                    this.fenFuMapClass_0.MapRow3Col1Text = this.txtR3C1.Text.Trim();
                    this.fenFuMapClass_0.MapRow1Col2Text = this.txtR1C2.Text.Trim();
                    this.fenFuMapClass_0.MapRow3Col2Text = this.txtR3C2.Text.Trim();
                    this.fenFuMapClass_0.MapRow1Col3Text = this.txtR1C3.Text.Trim();
                    this.fenFuMapClass_0.MapRow2Col3Text = this.txtR2C3.Text.Trim();
                    this.fenFuMapClass_0.MapRow3Col3Text = this.txtR3C3.Text.Trim();
                    if (this.chkLenged.Checked)
                    {
                        this.fenFuMapClass_0.NeedLegend = true;
                    }
                    else
                    {
                        this.fenFuMapClass_0.NeedLegend = false;
                    }
                    this.fenFuMapClass_0.ActiveView = this.iactiveView_0;
                    this.fenFuMapClass_0.Draw();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private void btnSavee_Click(object sender, EventArgs e)
        {
            this.fenFuMapClass_0.Save();
        }

        private void cmbScale_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

 private void frmFenFuTKCreate_Load(object sender, EventArgs e)
        {
            this.cmbScale.Items.Add("1:5000");
            this.cmbScale.Items.Add("1:10000");
            this.cmbScale.SelectedIndex = 1;
        }

 public IActiveView ActiveView
        {
            set
            {
                this.iactiveView_0 = value;
            }
        }

        internal bool IsAdminSys
        {
            get
            {
                return false;
            }
            set
            {
            }
        }
    }
}

