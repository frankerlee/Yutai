using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.ControlExtend;
using FileStream = System.IO.FileStream;

namespace Yutai.ArcGIS.Common.Raster.UI
{
    [ToolboxItem(false)]
    public partial class RasterAdjustPointsTablePage : UserControl, IDockContent
    {
        private bool bool_0 = false;
        private bool bool_1 = true;
        private Container container_0 = null;
        private IArray iarray_0 = new ESRI.ArcGIS.esriSystem.Array();

        public RasterAdjustPointsTablePage()
        {
            this.InitializeComponent();
            if (RasterAdjustHelper.RasterAdjust == null)
            {
                RasterAdjustHelper.Init();
            }
            RasterAdjustHelper.OnAddPoints += new RasterAdjustHelper.OnAddPointsHandler(this.method_4);
            this.Text = "配准";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.LinkPointlist.SelectedItems.Count != 0)
            {
                int num;
                ArrayList list = new ArrayList();
                for (num = 0; num < this.LinkPointlist.SelectedItems.Count; num++)
                {
                    int index = this.LinkPointlist.Items.IndexOf(this.LinkPointlist.SelectedItems[num]);
                    list.Add(index);
                    this.LinkPointlist.Items.Remove(this.LinkPointlist.SelectedItems[num]);
                }
                list.Sort();
                for (num = list.Count - 1; num >= 0; num--)
                {
                    RasterAdjustHelper.RasterAdjust.DeleteControlPointPair((int) list[num]);
                }
            }
        }

        private void btnLoadLinkPoint_Click(object sender, EventArgs e)
        {
            this.method_2();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            RasterAdjustHelper.RasterAdjust.Save();
            this.Reset();
            MessageBox.Show("保存成功", "配准");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RasterAdjustHelper.RasterAdjust.Adjust();
        }

        private void checkEditAutoAdjust_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                RasterAdjustHelper.RasterAdjust.AutoAdjust = this.checkEditAutoAdjust.Checked;
            }
        }

        private void checkEditAutoAdjust_Click(object sender, EventArgs e)
        {
        }

        internal void InitControl()
        {
            this.LinkPointlist.Items.Clear();
            this.iarray_0.RemoveAll();
            if (RasterAdjustHelper.RasterAdjust != null)
            {
                if (RasterAdjustHelper.RasterAdjust.OperatorLayer != null)
                {
                    string name = RasterAdjustHelper.RasterAdjust.OperatorLayer.Name;
                }
                string[] items = new string[5];
                for (int i = 0; i < RasterAdjustHelper.RasterAdjust.SourcePointCollection.PointCount; i++)
                {
                    items[0] = (i + 1).ToString();
                    IPoint point = RasterAdjustHelper.RasterAdjust.SourcePointCollection.get_Point(i);
                    items[1] = point.X.ToString();
                    items[2] = point.Y.ToString();
                    point = RasterAdjustHelper.RasterAdjust.DestPointCollection.get_Point(i);
                    items[3] = point.X.ToString();
                    items[4] = point.Y.ToString();
                    this.LinkPointlist.Items.Add(new ListViewItem(items));
                }
                this.checkEditAutoAdjust.Checked = RasterAdjustHelper.RasterAdjust.AutoAdjust;
            }
        }

        private void LinkPointlist_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void LinkPointlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.LinkPointlist.SelectedItems.Count > 0)
            {
                this.btnDelete.Enabled = true;
            }
            else
            {
                this.btnDelete.Enabled = false;
            }
        }

        private void method_0(IPoint ipoint_0, IPoint ipoint_1)
        {
            string[] items = new string[]
            {
                (this.LinkPointlist.Items.Count + 1).ToString(), ipoint_0.X.ToString(), ipoint_0.Y.ToString(),
                ipoint_1.X.ToString(), ipoint_1.Y.ToString()
            };
            this.LinkPointlist.Items.Add(new ListViewItem(items));
        }

        private void method_1(string string_0)
        {
            IPoint point = new ESRI.ArcGIS.Geometry.Point();
            IPoint point2 = new ESRI.ArcGIS.Geometry.Point();
            string_0 = string_0.Trim();
            int index = string_0.IndexOf(" ");
            double x = Convert.ToDouble(string_0.Substring(0, index));
            string_0 = string_0.Substring(index);
            string_0 = string_0.Trim();
            index = string_0.IndexOf(" ");
            double y = Convert.ToDouble(string_0.Substring(0, index));
            point.PutCoords(x, y);
            string_0 = string_0.Substring(index);
            string_0 = string_0.Trim();
            index = string_0.IndexOf(" ");
            x = Convert.ToDouble(string_0.Substring(0, index));
            string_0 = string_0.Substring(index);
            string_0 = string_0.Trim();
            string str = string_0;
            y = Convert.ToDouble(str);
            point2.PutCoords(x, y);
            RasterAdjustHelper.RasterAdjust.AddPointPair(point, point2);
        }

        private void method_2()
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "文本文件|*.txt",
                RestoreDirectory = true
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                RasterAdjustHelper.RasterAdjust.Clear();
                this.LinkPointlist.Items.Clear();
                FileStream stream = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                StreamReader reader = new StreamReader(stream);
                while (reader.Peek() > -1)
                {
                    string str = reader.ReadLine();
                    this.method_1(str);
                }
                reader.Close();
            }
        }

        private void method_3(object sender, ValueChangedEventArgs e)
        {
            IPoint inPoint = RasterAdjustHelper.RasterAdjust.DestPointCollection.get_Point(e.Row);
            try
            {
                if (e.Column == 3)
                {
                    inPoint.X = Convert.ToDouble(e.NewValue);
                }
                else if (e.Column == 4)
                {
                    inPoint.Y = Convert.ToDouble(e.NewValue);
                }
            }
            catch
            {
                return;
            }
            object before = Missing.Value;
            IPointCollection newPoints = new Multipoint();
            newPoints.AddPoint(inPoint, ref before, ref before);
            RasterAdjustHelper.RasterAdjust.DestPointCollection.ReplacePointCollection(e.Row, 1, newPoints);
        }

        private void method_4(IPoint ipoint_0, IPoint ipoint_1)
        {
            this.method_0(ipoint_0, ipoint_1);
        }

        private void RasterAdjustPointsTablePage_Load(object sender, EventArgs e)
        {
            this.InitControl();
            this.btnDelete.Enabled = false;
            this.bool_0 = true;
        }

        public void Reset()
        {
            this.InitControl();
        }

        public DockingStyle DefaultDockingStyle
        {
            get { return DockingStyle.Bottom; }
        }

        string IDockContent.Name
        {
            get { return base.Name; }
        }

        int IDockContent.Width
        {
            get { return base.Width; }
        }
    }
}