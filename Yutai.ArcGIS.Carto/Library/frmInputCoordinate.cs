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
    public class frmInputCoordinate : Form
    {
        private Button btnAdd;
        private Button btnCancel;
        private Button btnDelete;
        private Button btnModify;
        private Button btnMoveDown;
        private Button btnMoveUp;
        private Button btnOK;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private IContainer icontainer_0 = null;
        private IGeometry igeometry_0 = null;
        private IMap imap_0 = null;
        private Label label1;
        private Label label3;
        private Label label4;
        private ListView listView1;

        public frmInputCoordinate()
        {
            this.InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmInputXY txy = new frmInputXY();
            if (txy.ShowDialog() == DialogResult.OK)
            {
                ListViewItem item = new ListViewItem(new string[] { txy.X.ToString(), txy.Y.ToString() });
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
                frmInputXY txy = new frmInputXY {
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
                    IPoint inPoint = new PointClass {
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

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmInputCoordinate_Load(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            this.listView1 = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.label1 = new Label();
            this.btnAdd = new Button();
            this.btnModify = new Button();
            this.btnDelete = new Button();
            this.btnMoveUp = new Button();
            this.btnMoveDown = new Button();
            this.btnCancel = new Button();
            this.btnOK = new Button();
            this.label3 = new Label();
            this.label4 = new Label();
            base.SuspendLayout();
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1 });
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(14, 0x2f);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0x103, 0xac);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.columnHeader_0.Text = "横坐标";
            this.columnHeader_0.Width = 0x6c;
            this.columnHeader_1.Text = "纵坐标";
            this.columnHeader_1.Width = 0x71;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 0x18);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "坐标列表";
            this.btnAdd.Location = new System.Drawing.Point(0x125, 0x2f);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(0x4b, 0x17);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.btnModify.Location = new System.Drawing.Point(0x125, 0x4d);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new Size(0x4b, 0x17);
            this.btnModify.TabIndex = 3;
            this.btnModify.Text = "修改";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new EventHandler(this.btnModify_Click);
            this.btnDelete.Location = new System.Drawing.Point(0x125, 0x6a);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(0x4b, 0x17);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.btnMoveUp.Location = new System.Drawing.Point(0x125, 0x93);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new Size(0x4b, 0x17);
            this.btnMoveUp.TabIndex = 5;
            this.btnMoveUp.Text = "上移";
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Click += new EventHandler(this.btnMoveUp_Click);
            this.btnMoveDown.Location = new System.Drawing.Point(0x125, 0xb0);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new Size(0x4b, 0x17);
            this.btnMoveDown.TabIndex = 6;
            this.btnMoveDown.Text = "下移";
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Click += new EventHandler(this.btnMoveDown_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(0x112, 0xee);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x17);
            this.btnCancel.TabIndex = 0x12;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnOK.Location = new System.Drawing.Point(0xb3, 0xee);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 0x11;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0xe1, 0x18);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x1d, 12);
            this.label3.TabIndex = 0x17;
            this.label3.Text = "公里";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0x94, 0x18);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x3b, 12);
            this.label4.TabIndex = 0x18;
            this.label4.Text = "坐标单位:";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x18a, 0x111);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnMoveDown);
            base.Controls.Add(this.btnMoveUp);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.btnModify);
            base.Controls.Add(this.btnAdd);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.listView1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmInputCoordinate";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "输入坐标";
            base.Load += new EventHandler(this.frmInputCoordinate_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public IGeometry Geometry
        {
            get
            {
                return this.igeometry_0;
            }
        }

        public IMap Map
        {
            set
            {
                this.imap_0 = value;
            }
        }
    }
}

