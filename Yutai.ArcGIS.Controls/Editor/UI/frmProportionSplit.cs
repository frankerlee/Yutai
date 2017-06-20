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
    public class frmProportionSplit : Form
    {
        private SimpleButton btnOK;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private Container components = null;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private ListView listView1;
        private IFeature m_pFeature = null;
        private IPolyline m_pPolyline = null;
        private string m_preLabel = "";
        private RadioGroup rdoStartType;
        private SimpleButton simpleButton2;
        private TextEdit txtError;
        private TextEdit txtInputLength;
        private TextEdit txtLength;
        private TextEdit txtSurplus;

        public frmProportionSplit()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Do();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
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
                num4 = this.m_pPolyline.Length / distance;
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
                                geometrys.AddGeometry((this.m_pPolyline as IGeometryCollection).get_Geometry(num8), ref obj2, ref obj2);
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
                                    geometrys.AddGeometry((this.m_pPolyline as IGeometryCollection).get_Geometry(num8), ref obj2, ref obj2);
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

        private void InitializeComponent()
        {
            ListViewItem item = new ListViewItem("<点击输入长度>");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProportionSplit));
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.txtLength = new TextEdit();
            this.txtInputLength = new TextEdit();
            this.txtSurplus = new TextEdit();
            this.txtError = new TextEdit();
            this.listView1 = new ListView();
            this.columnHeader1 = new ColumnHeader();
            this.columnHeader2 = new ColumnHeader();
            this.groupBox1 = new GroupBox();
            this.rdoStartType = new RadioGroup();
            this.btnOK = new SimpleButton();
            this.simpleButton2 = new SimpleButton();
            this.txtLength.Properties.BeginInit();
            this.txtInputLength.Properties.BeginInit();
            this.txtSurplus.Properties.BeginInit();
            this.txtError.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            this.rdoStartType.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x3b, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "对象长度:";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 0x2d);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x3b, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "输入长度:";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(160, 0x2d);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x3b, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "相对误差:";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(160, 10);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x23, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "剩余:";
            this.txtLength.EditValue = "";
            this.txtLength.Location = new System.Drawing.Point(0x48, 8);
            this.txtLength.Name = "txtLength";
            this.txtLength.Properties.ReadOnly = true;
            this.txtLength.Size = new Size(80, 0x15);
            this.txtLength.TabIndex = 4;
            this.txtInputLength.EditValue = "";
            this.txtInputLength.Location = new System.Drawing.Point(0x48, 40);
            this.txtInputLength.Name = "txtInputLength";
            this.txtInputLength.Properties.ReadOnly = true;
            this.txtInputLength.Size = new Size(80, 0x15);
            this.txtInputLength.TabIndex = 5;
            this.txtSurplus.EditValue = "";
            this.txtSurplus.Location = new System.Drawing.Point(0xe8, 8);
            this.txtSurplus.Name = "txtSurplus";
            this.txtSurplus.Properties.ReadOnly = true;
            this.txtSurplus.Size = new Size(80, 0x15);
            this.txtSurplus.TabIndex = 6;
            this.txtError.EditValue = "";
            this.txtError.Location = new System.Drawing.Point(0xe8, 40);
            this.txtError.Name = "txtError";
            this.txtError.Properties.ReadOnly = true;
            this.txtError.Size = new Size(80, 0x15);
            this.txtError.TabIndex = 7;
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader1, this.columnHeader2 });
            this.listView1.FullRowSelect = true;
            item.Tag = "0";
            this.listView1.Items.AddRange(new ListViewItem[] { item });
            this.listView1.LabelEdit = true;
            this.listView1.Location = new System.Drawing.Point(0x10, 0x48);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0x150, 120);
            this.listView1.TabIndex = 8;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.listView1.AfterLabelEdit += new LabelEditEventHandler(this.listView1_AfterLabelEdit);
            this.listView1.BeforeLabelEdit += new LabelEditEventHandler(this.listView1_BeforeLabelEdit);
            this.columnHeader1.Text = "长度";
            this.columnHeader1.Width = 130;
            this.columnHeader2.Text = "比例细分";
            this.columnHeader2.Width = 0xa9;
            this.groupBox1.Controls.Add(this.rdoStartType);
            this.groupBox1.Location = new System.Drawing.Point(0x18, 200);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x138, 0x48);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "方向";
            this.rdoStartType.Location = new System.Drawing.Point(0x10, 0x11);
            this.rdoStartType.Name = "rdoStartType";
            this.rdoStartType.Properties.Appearance.BackColor = SystemColors.ControlLight;
            this.rdoStartType.Properties.Appearance.Options.UseBackColor = true;
            this.rdoStartType.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoStartType.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "从线起点开始"), new RadioGroupItem(null, "从线终点开始") });
            this.rdoStartType.Size = new Size(0x70, 0x2f);
            this.rdoStartType.TabIndex = 0;
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(0xc0, 280);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x40, 0x18);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.simpleButton2.DialogResult = DialogResult.Cancel;
            this.simpleButton2.Location = new System.Drawing.Point(0x110, 280);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new Size(0x40, 0x18);
            this.simpleButton2.TabIndex = 11;
            this.simpleButton2.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(360, 0x13d);
            base.Controls.Add(this.simpleButton2);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.txtError);
            base.Controls.Add(this.txtSurplus);
            base.Controls.Add(this.txtInputLength);
            base.Controls.Add(this.txtLength);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmProportionSplit";
            this.Text = "分割";
            base.Load += new EventHandler(this.frmPropertionSplit_Load);
            this.txtLength.Properties.EndInit();
            this.txtInputLength.Properties.EndInit();
            this.txtSurplus.Properties.EndInit();
            this.txtError.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.rdoStartType.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
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
                        num6 = this.m_pPolyline.Length / Math.Abs(num4);
                        this.txtError.Text = "1:" + num6.ToString("0");
                    }
                    double num5 = this.m_pPolyline.Length / num2;
                    for (num3 = 0; num3 < this.listView1.Items.Count; num3++)
                    {
                        item = this.listView1.Items[num3];
                        if (item.SubItems.Count == 1)
                        {
                            if (item.Tag.ToString() != "0")
                            {
                                num6 = ((double) item.Tag) * num5;
                                item.SubItems.Add(num6.ToString());
                            }
                        }
                        else
                        {
                            item.SubItems[1].Text = (((double) item.Tag) * num5).ToString();
                        }
                    }
                    if (e.Item == (this.listView1.Items.Count - 1))
                    {
                        item = new ListViewItem("<点击输入长度>") {
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

