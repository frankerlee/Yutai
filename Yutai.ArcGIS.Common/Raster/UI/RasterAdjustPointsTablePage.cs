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
    public class RasterAdjustPointsTablePage : UserControl, IDockContent
    {
        private bool bool_0 = false;
        private bool bool_1 = true;
        private SimpleButton btnDelete;
        private SimpleButton btnLoadLinkPoint;
        private SimpleButton btnSave;
        private SimpleButton button1;
        private CheckEdit checkEditAutoAdjust;
        private Container container_0 = null;
        private IArray iarray_0 = new ESRI.ArcGIS.esriSystem.Array();
        private EditListView LinkPointlist;
        private LVColumnHeader lvcolumnHeader_0;
        private LVColumnHeader lvcolumnHeader_1;
        private LVColumnHeader lvcolumnHeader_2;
        private LVColumnHeader lvcolumnHeader_3;
        private LVColumnHeader lvcolumnHeader_4;

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

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_2);
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

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources= new  System.ComponentModel.ComponentResourceManager(typeof(RasterAdjustPointsTablePage));
            this.LinkPointlist = new EditListView();
            this.lvcolumnHeader_0 = new LVColumnHeader();
            this.lvcolumnHeader_1 = new LVColumnHeader();
            this.lvcolumnHeader_2 = new LVColumnHeader();
            this.lvcolumnHeader_3 = new LVColumnHeader();
            this.lvcolumnHeader_4 = new LVColumnHeader();
            this.checkEditAutoAdjust = new CheckEdit();
            this.btnDelete = new SimpleButton();
            this.btnLoadLinkPoint = new SimpleButton();
            this.button1 = new SimpleButton();
            this.btnSave = new SimpleButton();
            this.checkEditAutoAdjust.Properties.BeginInit();
            base.SuspendLayout();
            this.LinkPointlist.Columns.AddRange(new ColumnHeader[] { this.lvcolumnHeader_0, this.lvcolumnHeader_1, this.lvcolumnHeader_2, this.lvcolumnHeader_3, this.lvcolumnHeader_4 });
            this.LinkPointlist.ComboBoxBgColor = Color.LightBlue;
            this.LinkPointlist.ComboBoxFont = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.LinkPointlist.EditBgColor = Color.White;
            this.LinkPointlist.EditFont = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.LinkPointlist.FullRowSelect = true;
            this.LinkPointlist.GridLines = true;
            this.LinkPointlist.Location = new System.Drawing.Point(8, 8);
            this.LinkPointlist.LockRowCount = 0;
            this.LinkPointlist.Name = "LinkPointlist";
            this.LinkPointlist.Size = new Size(0x1a8, 0x88);
            this.LinkPointlist.TabIndex = 5;
            this.LinkPointlist.View = View.Details;
            this.LinkPointlist.KeyPress += new KeyPressEventHandler(this.LinkPointlist_KeyPress);
            this.LinkPointlist.ValueChanged += new ValueChangedHandler(this.method_3);
            this.LinkPointlist.SelectedIndexChanged += new EventHandler(this.LinkPointlist_SelectedIndexChanged);
            this.lvcolumnHeader_0.ColumnStyle = ListViewColumnStyle.ReadOnly;
            this.lvcolumnHeader_0.Text = "链接";
            this.lvcolumnHeader_0.Width = 0x27;
            this.lvcolumnHeader_1.ColumnStyle = ListViewColumnStyle.EditBox;
            this.lvcolumnHeader_1.Text = "影像源X";
            this.lvcolumnHeader_1.Width = 0x52;
            this.lvcolumnHeader_2.ColumnStyle = ListViewColumnStyle.EditBox;
            this.lvcolumnHeader_2.Text = "影像源Y";
            this.lvcolumnHeader_2.Width = 0x56;
            this.lvcolumnHeader_3.ColumnStyle = ListViewColumnStyle.EditBox;
            this.lvcolumnHeader_3.Text = "地图X";
            this.lvcolumnHeader_3.Width = 0x6a;
            this.lvcolumnHeader_4.ColumnStyle = ListViewColumnStyle.EditBox;
            this.lvcolumnHeader_4.Text = "地图Y";
            this.lvcolumnHeader_4.Width = 0x69;
            this.checkEditAutoAdjust.Location = new System.Drawing.Point(8, 0x98);
            this.checkEditAutoAdjust.Name = "checkEditAutoAdjust";
            this.checkEditAutoAdjust.Properties.Caption = "自动调整";
            this.checkEditAutoAdjust.Size = new Size(0x58, 0x13);
            this.checkEditAutoAdjust.TabIndex = 7;
            this.checkEditAutoAdjust.Click += new EventHandler(this.checkEditAutoAdjust_Click);
            this.checkEditAutoAdjust.CheckedChanged += new EventHandler(this.checkEditAutoAdjust_CheckedChanged);
            this.btnDelete.Image = (Image) resources.GetObject("btnDelete.Image");
            this.btnDelete.Location = new System.Drawing.Point(440, 8);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(0x18, 0x18);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.btnLoadLinkPoint.Location = new System.Drawing.Point(0x70, 0x98);
            this.btnLoadLinkPoint.Name = "btnLoadLinkPoint";
            this.btnLoadLinkPoint.Size = new Size(0x30, 0x18);
            this.btnLoadLinkPoint.TabIndex = 10;
            this.btnLoadLinkPoint.Text = "装入";
            this.btnLoadLinkPoint.Click += new EventHandler(this.btnLoadLinkPoint_Click);
            this.button1.Location = new System.Drawing.Point(0xb0, 0x98);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x30, 0x18);
            this.button1.TabIndex = 11;
            this.button1.Text = "配准";
            this.button1.Click += new EventHandler(this.button1_Click);
            this.btnSave.Location = new System.Drawing.Point(240, 0x98);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(0x38, 0x18);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "保存变化";
            this.btnSave.Click += new EventHandler(this.btnSave_Click);
            base.Controls.Add(this.btnSave);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.btnLoadLinkPoint);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.checkEditAutoAdjust);
            base.Controls.Add(this.LinkPointlist);
            base.Name = "RasterAdjustPointsTablePage";
            base.Size = new Size(0x1d8, 0xc0);
            base.Load += new EventHandler(this.RasterAdjustPointsTablePage_Load);
            this.checkEditAutoAdjust.Properties.EndInit();
            base.ResumeLayout(false);
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
            string[] items = new string[] { (this.LinkPointlist.Items.Count + 1).ToString(), ipoint_0.X.ToString(), ipoint_0.Y.ToString(), ipoint_1.X.ToString(), ipoint_1.Y.ToString() };
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
            OpenFileDialog dialog = new OpenFileDialog {
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
            get
            {
                return DockingStyle.Bottom;
            }
        }

        string IDockContent.Name
        {
            get
            {
                return base.Name;
            }
        }

        int IDockContent.Width
        {
            get
            {
                return base.Width;
            }
        }
    }
}

