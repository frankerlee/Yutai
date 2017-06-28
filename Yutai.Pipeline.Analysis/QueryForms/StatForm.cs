using Infragistics.UltraChart.Resources.Appearance;
using Infragistics.UltraChart.Shared.Styles;
using Infragistics.Win;
using Infragistics.Win.Printing;
using Infragistics.Win.UltraWinChart;
using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.Pipeline.Analysis.QueryForms
{
    public partial class StatForm : Form
    {
        public DataTable resultTable;
        public DataSet ds;
        private static bool frmIsResizing;
        private Rectangle frmRectangle = default(Rectangle);
        private Dictionary<string, string> PosDic = new Dictionary<string, string>();
        public string Form_StatWay
        {
            get
            {
                return this.statWay;
            }
            set
            {
                this.statWay = value;
            }
        }

        public string Form_StatField
        {
            get
            {
                return this.statField;
            }
            set
            {
                this.statField = value;
            }
        }

        public string Form_CalField
        {
            get
            {
                return this.calField;
            }
            set
            {
                this.calField = value;
            }
        }

        public StatForm()
        {
            this.InitializeComponent();
            this.PosDic.Add("Top", "上部");
            this.PosDic.Add("Bottom", "下部");
            this.PosDic.Add("Left", "左部");
            this.PosDic.Add("Right", "右部");
        }

        private void StatForm_Load(object sender, EventArgs e)
        {
            this.ChangeSize();
            this.LegendBox.Items.Clear();
            string[] names = Enum.GetNames(typeof(LegendLocation));
            string[] array = names;
            for (int i = 0; i < array.Length; i++)
            {
                string key = array[i];
                this.LegendBox.Items.Add(this.PosDic[key]);
            }
            this.LegendBox.Text = this.PosDic[this.ultraChart1.Legend.Location.ToString()];
            this.ShowLegendBox.Checked = this.ultraChart1.Legend.Visible;
            this.ChartKind.SelectedIndex = 0;
            this.ds = new DataSet("tableCol");
            this.MakeData();
            this.trackBar1.Value = (int)this.ultraChart1.Transform3D.XRotation;
            this.trackBar2.Value = (int)this.ultraChart1.Transform3D.YRotation;
            this.trackBar3.Value = (int)this.ultraChart1.Transform3D.ZRotation;
            this.SizeBar.Value = (int)this.ultraChart1.Transform3D.Scale;
            Splash.Close();
        }

        private void MakeData()
        {
            try
            {
                Splash.Show();
                Splash.Status = "状态:创建临时表,请稍候...";
                this.dt = new DataTable("table1");
                this.CalTable = new DataTable("table2");
                this.AllTable = new DataTable("AllTable");
                if (this.statWay == "统计长度")
                {
                    this.calField = "GD管线长度";
                }
                Splash.Status = "状态:绘制图表,请稍候...";
                this.SelectDistinct("分组", this.resultTable, this.statField);
                if (this.ds != null)
                {
                    this.ds.Tables.Add(this.dt);
                    this.ds.Tables.Add(this.CalTable);
                }
                this.ultraChart1.DataSource = this.dt;
                this.ultraChart1.DataBind();
                if (this.statWay == "计数")
                {
                    this.ultraGrid1.DataSource = (this.dt);
                    this.sumRadio.Enabled = false;
                }
                else
                {
                    this.ultraGrid1.DataSource = (this.AllTable);
                    this.sumRadio.Text = this.statWay + "分布";
                }
                this.ultraGrid1.DisplayLayout.Bands[0].CardSettings.Width = (50);
                Splash.Close();
            }
            catch
            {
                Splash.Close();
            }
        }

        public void SelectDistinct(string TableName, DataTable SourceTable, string FieldName)
        {
            this.dt.Columns.Add(FieldName, typeof(string));
            this.dt.Columns.Add("值", typeof(double));
            this.CalTable.Columns.Add(FieldName, typeof(string));
            this.CalTable.Columns.Add("计算值", typeof(double));
            this.AllTable.Columns.Add(FieldName, typeof(string));
            this.AllTable.Columns.Add("分类值", typeof(double));
            this.AllTable.Columns.Add("计算值", typeof(double));
            object obj = null;
            int num = 1;
            double num2 = 0.0;
            DataRow[] array = SourceTable.Select("", FieldName);
            for (int i = 0; i < array.Length; i++)
            {
                DataRow dataRow = array[i];
                if (obj == null || !this.ColumnEqual(obj, dataRow[FieldName]))
                {
                    if (obj == null)
                    {
                        obj = dataRow[FieldName];
                        if (this.statWay != "计数")
                        {
                            string a = dataRow[this.calField].ToString();
                            if (a != "")
                            {
                                num2 += Convert.ToDouble(dataRow[this.calField]);
                            }
                        }
                    }
                    else
                    {
                        this.dt.Rows.Add(new object[]
                        {
                            obj,
                            num
                        });
                        if (this.statWay == "平均值")
                        {
                            num2 /= (double)num;
                        }
                        num2 = Math.Round(num2, 3);
                        this.CalTable.Rows.Add(new object[]
                        {
                            obj,
                            num2
                        });
                        this.AllTable.Rows.Add(new object[]
                        {
                            obj,
                            num,
                            num2
                        });
                        obj = dataRow[FieldName];
                        num = 1;
                        if (this.statWay != "计数")
                        {
                            string a = dataRow[this.calField].ToString();
                            if (a != "")
                            {
                                num2 = Convert.ToDouble(dataRow[this.calField]);
                            }
                            else
                            {
                                num2 = 0.0;
                            }
                        }
                    }
                }
                else
                {
                    num++;
                    if (this.statWay != "计数")
                    {
                        string a = dataRow[this.calField].ToString();
                        if (a != "")
                        {
                            num2 += Convert.ToDouble(dataRow[this.calField]);
                        }
                    }
                }
            }
            if (this.statWay == "平均值")
            {
                num2 /= (double)num;
            }
            num2 = Math.Round(num2, 3);
            this.dt.Rows.Add(new object[]
            {
                obj,
                num
            });
            this.CalTable.Rows.Add(new object[]
            {
                obj,
                num2
            });
            this.AllTable.Rows.Add(new object[]
            {
                obj,
                num,
                num2
            });
        }

        private bool ColumnEqual(object A, object B)
        {
            return (A == DBNull.Value && B == DBNull.Value) || (A != DBNull.Value && B != DBNull.Value && A.Equals(B));
        }

        private void sumRadio_CheckedChanged(object sender, EventArgs e)
        {
            this.ultraChart1.DataSource = this.CalTable;
        }

        private void CountRadio_CheckedChanged(object sender, EventArgs e)
        {
            this.ultraChart1.DataSource = this.dt;
        }

        private void ChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ChartKind.SelectedIndex == 0)
            {
                this.ultraChart1.ChartType = ChartType.CylinderColumnChart3D;
            }
            else
            {
                this.ultraChart1.ChartType = ChartType.PieChart3D;
                this.ultraChart1.PieChart3D.OthersCategoryPercent = 0.0;
                this.ultraChart1.PieChart3D.OthersCategoryText = "其它";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.bExtent = !this.bExtent;
            this.ChangeSize();
        }

        private void ChangeSize()
        {
            int height = base.Size.Height;
            int width;
            if (!this.bExtent)
            {
                width = this.SizeBut.Right + 10;
                this.SizeBut.BackgroundImage = this.imageList1.Images[0];
            }
            else
            {
                width = this.SizeBut.Right + 157;
                this.SizeBut.BackgroundImage = this.imageList1.Images[1];
            }
            Size size = base.Size;
            size.Width = width;
            size.Height = height;
            base.Size = size;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            this.ultraChart1.Transform3D.XRotation = (float)this.trackBar1.Value;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            this.ultraChart1.Transform3D.YRotation = (float)this.trackBar2.Value;
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            this.ultraChart1.Transform3D.ZRotation = (float)this.trackBar3.Value;
        }

        private void SizeBar_Scroll(object sender, EventArgs e)
        {
            this.ultraChart1.Transform3D.Scale = (float)this.SizeBar.Value;
        }

        private void DoIt_Click(object sender, EventArgs e)
        {
            this.ultraChart1.AutoSize = true;
            this.ultraPrintPreviewDialog1.Document = this.ultraChart1.PrintDocument;
            this.ultraPrintPreviewDialog1.ShowDialog();
        }

        private void ShowLegendBox_CheckedChanged(object sender, EventArgs e)
        {
            this.ultraChart1.Legend.Visible = this.ShowLegendBox.Checked;
        }

        private void LegendBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string a = this.LegendBox.SelectedItem.ToString();
            string value = "";
            foreach (KeyValuePair<string, string> current in this.PosDic)
            {
                if (a == current.Value)
                {
                    value = current.Key;
                    break;
                }
            }
            this.ultraChart1.Legend.Location = (LegendLocation)Enum.Parse(typeof(LegendLocation), value);
        }

        private void StatForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Splash.Close();
        }
    }
}
