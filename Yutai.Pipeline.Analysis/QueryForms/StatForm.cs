using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraCharts;

namespace Yutai.Pipeline.Analysis.QueryForms
{
    public partial class StatForm : Form
    {
        public DataTable resultTable;
        public DataSet ds;
        private static bool frmIsResizing;
        private Rectangle frmRectangle = default(Rectangle);
        private DevExpress.XtraCharts.Series _series;

        /// <summary>
        /// 统计方式
        /// </summary>
        public string Form_StatWay
        {
            get { return this.statWay; }
            set { this.statWay = value; }
        }

        /// <summary>
        /// 统计字段
        /// </summary>
        public string Form_StatField
        {
            get { return this.statField; }
            set { this.statField = value; }
        }

        /// <summary>
        /// 计算字段
        /// </summary>
        public string Form_CalField
        {
            get { return this.calField; }
            set { this.calField = value; }
        }

        public StatForm()
        {
            this.InitializeComponent();
        }

        private void StatForm_Load(object sender, EventArgs e)
        {
            _series = ultraChart1.Series[0];
            _series.Name = statField;
            this.ChartKind.SelectedIndex = 0;
            this.ds = new DataSet("tableCol");
            this.MakeData();
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
                _series.ArgumentDataMember = statField;
                _series.ValueDataMembers[0] = "计算值";
                if (this.statWay == "计数")
                {
                    this.gridControl1.DataSource = this.dt;
                    this.sumRadio.Enabled = false;
                }
                else
                {
                    this.gridControl1.DataSource = (this.AllTable);
                    this.sumRadio.Text = this.statWay + "分布";
                }
                Splash.Close();
            }
            catch
            {
                Splash.Close();
            }
        }

        public void SelectDistinct(string tableName, DataTable sourceTable, string statFieldName)
        {
            this.dt.Columns.Add(statFieldName, typeof(string));
            this.dt.Columns.Add("计算值", typeof(double));
            this.CalTable.Columns.Add(statFieldName, typeof(string));
            this.CalTable.Columns.Add("计算值", typeof(double));
            this.AllTable.Columns.Add(statFieldName, typeof(string));
            this.AllTable.Columns.Add("分类值", typeof(double));
            this.AllTable.Columns.Add("计算值", typeof(double));
            object objValue = null;
            int num = 1;
            double num2 = 0.0;
            DataRow[] dataRows = sourceTable.Select("", statFieldName);
            for (int i = 0; i < dataRows.Length; i++)
            {
                DataRow dataRow = dataRows[i];
                if (objValue == null || !this.ColumnEqual(objValue, dataRow[statFieldName]))
                {
                    if (objValue == null)
                    {
                        objValue = dataRow[statFieldName];
                        if (this.statWay != "计数")
                        {
                            string a = dataRow[this.calField].ToString();
                            if (string.IsNullOrWhiteSpace(a) == false)
                            {
                                num2 += Convert.ToDouble(dataRow[this.calField]);
                            }
                        }
                    }
                    else
                    {
                        this.dt.Rows.Add(objValue, num);
                        if (this.statWay == "平均值")
                        {
                            num2 /= num;
                        }
                        num2 = Math.Round(num2, 3);
                        this.CalTable.Rows.Add(objValue, num2);
                        this.AllTable.Rows.Add(objValue, num, num2);
                        objValue = dataRow[statFieldName];
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
                num2 /= (double) num;
            }
            num2 = Math.Round(num2, 3);
            this.dt.Rows.Add(objValue, num);
            this.CalTable.Rows.Add(objValue, num2);
            this.AllTable.Rows.Add(objValue, num, num2);
        }

        private bool ColumnEqual(object A, object B)
        {
            return (A == DBNull.Value && B == DBNull.Value) || (A != DBNull.Value && B != DBNull.Value && A.Equals(B));
        }

        private void sumRadio_CheckedChanged(object sender, EventArgs e)
        {
            this.ultraChart1.DataSource = this.CalTable;
            _series.ArgumentDataMember = statField;
            _series.ValueDataMembers[0] = "计算值";
        }

        private void CountRadio_CheckedChanged(object sender, EventArgs e)
        {
            this.ultraChart1.DataSource = this.dt;
            _series.ArgumentDataMember = statField;
            _series.ValueDataMembers[0] = "计算值";
        }

        private void ChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.ChartKind.SelectedItem.ToString())
            {
                case "饼图":
                    _series.View = new PieSeriesView();
                    break;
                case "柱状图":
                default:
                    _series.View = (new Series()).View;
                    break;
            }
        }

        private void StatForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Splash.Close();
        }

        private void DoIt_Click(object sender, EventArgs e)
        {
            ultraChart1.ShowPrintPreview();
        }
    }
}