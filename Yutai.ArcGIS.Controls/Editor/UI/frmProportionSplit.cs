using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Geodatabase;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    public partial class frmProportionSplit : Form
    {
        private IFeature m_pFeature = null;
        private IPolyline m_pPolyline = null;
        private string m_preLabel = "";

        public frmProportionSplit()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Do();
        }

        private bool Do()
        {
            ListViewItem item;
            int num2;
            bool flag;
            int num6;
            int num7;
            IGeometryCollection geometrys;
            object obj2;
            int num8;
            IRow row;
            double distance = 0.0;
            for (num2 = 0; num2 < this.listView1.Items.Count; num2++)
            {
                item = this.listView1.Items[num2];
                try
                {
                    distance += (double) item.Tag;
                }
                catch
                {
                }
            }
            double num3 = distance - this.m_pPolyline.Length;
            double num4 = 1.0;
            if (num3 != 0.0)
            {
                num4 = this.m_pPolyline.Length/distance;
            }
            Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace.StartEditOperation();
            int count = this.listView1.Items.Count;
            item = this.listView1.Items[count - 1];
            try
            {
                if (((double) item.Tag) == 0.0)
                {
                    count--;
                }
            }
            catch
            {
                count--;
            }
            if (this.rdoStartType.SelectedIndex == 0)
            {
                for (num2 = 0; num2 < count; num2++)
                {
                    item = this.listView1.Items[num2];
                    distance = (double) item.Tag;
                    if (distance != 0.0)
                    {
                        distance *= num4;
                        this.m_pPolyline.SplitAtDistance(distance, false, true, out flag, out num6, out num7);
                        if (flag)
                        {
                            geometrys = new PolylineClass();
                            obj2 = Missing.Value;
                            num8 = 0;
                            while (num8 < num6)
                            {
                                geometrys.AddGeometry((this.m_pPolyline as IGeometryCollection).get_Geometry(num8),
                                    ref obj2, ref obj2);
                                num8++;
                            }
                            if (num2 == 0)
                            {
                                this.m_pFeature.Shape = geometrys as IGeometry;
                                this.m_pFeature.Store();
                            }
                            else
                            {
                                row = RowOperator.CreatRowByRow(this.m_pFeature);
                                (row as IFeature).Shape = geometrys as IGeometry;
                                row.Store();
                            }
                            (this.m_pPolyline as IGeometryCollection).RemoveGeometries(0, num6);
                        }
                    }
                }
                if (!this.m_pPolyline.IsEmpty)
                {
                    row = RowOperator.CreatRowByRow(this.m_pFeature);
                    (row as IFeature).Shape = this.m_pPolyline;
                    row.Store();
                }
            }
            else
            {
                for (num2 = count - 1; num2 >= 0; num2--)
                {
                    try
                    {
                        item = this.listView1.Items[num2];
                        distance = (double) item.Tag;
                        if (distance != 0.0)
                        {
                            distance *= num4;
                            this.m_pPolyline.SplitAtDistance(distance, false, true, out flag, out num6, out num7);
                            if (flag)
                            {
                                geometrys = new PolylineClass();
                                obj2 = Missing.Value;
                                for (num8 = 0; num8 < num6; num8++)
                                {
                                    geometrys.AddGeometry((this.m_pPolyline as IGeometryCollection).get_Geometry(num8),
                                        ref obj2, ref obj2);
                                }
                                if (num2 == (count - 1))
                                {
                                    this.m_pFeature.Shape = geometrys as IGeometry;
                                    this.m_pFeature.Store();
                                }
                                else
                                {
                                    row = RowOperator.CreatRowByRow(this.m_pFeature);
                                    (row as IFeature).Shape = geometrys as IGeometry;
                                    row.Store();
                                }
                                (this.m_pPolyline as IGeometryCollection).RemoveGeometries(0, num6);
                            }
                        }
                    }
                    catch
                    {
                    }
                }
                if (!this.m_pPolyline.IsEmpty)
                {
                    row = RowOperator.CreatRowByRow(this.m_pFeature);
                    (row as IFeature).Shape = this.m_pPolyline;
                    row.Store();
                }
            }
            Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace.StopEditOperation();
            return true;
        }

        private void frmPropertionSplit_Load(object sender, EventArgs e)
        {
            this.txtLength.Text = this.m_pPolyline.Length.ToString();
        }

        private void listView1_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            ListViewItem item;
            try
            {
                double num = 0.0;
                if ((e.Label == null) || (e.Label == ""))
                {
                    item = this.listView1.Items[e.Item];
                    num = double.Parse(item.Text);
                }
                else
                {
                    num = double.Parse(e.Label);
                }
                if (num > 0.0)
                {
                    int num3;
                    double num6;
                    double num2 = num;
                    for (num3 = 0; num3 < this.listView1.Items.Count; num3++)
                    {
                        item = this.listView1.Items[num3];
                        num2 += double.Parse(item.Tag.ToString());
                    }
                    item = this.listView1.Items[e.Item];
                    item.Tag = num;
                    this.txtInputLength.Text = num2.ToString();
                    double num4 = num2 - this.m_pPolyline.Length;
                    this.txtSurplus.Text = num4.ToString();
                    if (Math.Abs(num4) == 1E-05)
                    {
                        this.txtError.Text = "1:0";
                    }
                    else
                    {
                        num6 = this.m_pPolyline.Length/Math.Abs(num4);
                        this.txtError.Text = "1:" + num6.ToString("0");
                    }
                    double num5 = this.m_pPolyline.Length/num2;
                    for (num3 = 0; num3 < this.listView1.Items.Count; num3++)
                    {
                        item = this.listView1.Items[num3];
                        if (item.SubItems.Count == 1)
                        {
                            if (item.Tag.ToString() != "0")
                            {
                                num6 = ((double) item.Tag)*num5;
                                item.SubItems.Add(num6.ToString());
                            }
                        }
                        else
                        {
                            item.SubItems[1].Text = (((double) item.Tag)*num5).ToString();
                        }
                    }
                    if (e.Item == (this.listView1.Items.Count - 1))
                    {
                        item = new ListViewItem("<点击输入长度>")
                        {
                            Tag = 0
                        };
                        this.listView1.Items.Add(item);
                    }
                }
                else
                {
                    item = this.listView1.Items[e.Item];
                    item.Text = this.m_preLabel;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                item = this.listView1.Items[e.Item];
                item.SubItems[0].Text = this.m_preLabel;
            }
        }

        private void listView1_BeforeLabelEdit(object sender, LabelEditEventArgs e)
        {
            ListViewItem item = this.listView1.Items[e.Item];
            this.m_preLabel = item.Text;
        }

        public IFeature Feature
        {
            set
            {
                this.m_pFeature = value;
                this.m_pPolyline = this.m_pFeature.ShapeCopy as IPolyline;
            }
        }
    }
}