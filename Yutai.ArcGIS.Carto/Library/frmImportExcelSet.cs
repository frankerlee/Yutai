using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.Library
{
    public partial class frmImportExcelSet : Form
    {
        private IContainer icontainer_0 = null;
        private IGeometry igeometry_0 = null;

        public frmImportExcelSet()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DataRowCollection excelRows = ExcelTools.GetExcelRows(this.textBox1.Text, this.comboBox1.Text);
            IPointCollection points = new PolygonClass();
            foreach (DataRow row in excelRows)
            {
                try
                {
                    double num = double.Parse(row[this.cboX.Text].ToString());
                    double num2 = double.Parse(row[this.cboY.Text].ToString());
                    IPoint inPoint = new PointClass
                    {
                        X = num,
                        Y = num2
                    };
                    object before = Missing.Value;
                    points.AddPoint(inPoint, ref before, ref before);
                }
                catch
                {
                }
            }
            (points as IPolygon).Close();
            this.igeometry_0 = points as IGeometry;
            base.DialogResult = DialogResult.OK;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "*.xls|*.xls"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.comboBox1.Items.Clear();
                this.textBox1.Text = dialog.FileName;
                List<string> excelFileSheet = ExcelTools.GetExcelFileSheet(dialog.FileName);
                if (excelFileSheet != null)
                {
                    for (int i = 0; i < excelFileSheet.Count; i++)
                    {
                        this.comboBox1.Items.Add(excelFileSheet[i]);
                    }
                    if (this.comboBox1.Items.Count > 0)
                    {
                        this.comboBox1.SelectedIndex = 0;
                    }
                }
            }
        }

        private void cboX_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnOK.Enabled = ((this.comboBox1.SelectedIndex >= 0) && (this.cboX.SelectedIndex >= 0)) &&
                                 (this.cboY.SelectedIndex >= 0);
        }

        private void cboY_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnOK.Enabled = ((this.comboBox1.SelectedIndex >= 0) && (this.cboX.SelectedIndex >= 0)) &&
                                 (this.cboY.SelectedIndex >= 0);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnOK.Enabled = ((this.comboBox1.SelectedIndex >= 0) && (this.cboX.SelectedIndex >= 0)) &&
                                 (this.cboY.SelectedIndex >= 0);
            if (this.comboBox1.SelectedIndex >= 0)
            {
                List<string> excelColumns = ExcelTools.GetExcelColumns(this.textBox1.Text, this.comboBox1.Text);
                for (int i = 0; i < excelColumns.Count; i++)
                {
                    this.cboX.Items.Add(excelColumns[i]);
                    this.cboY.Items.Add(excelColumns[i]);
                }
                if (this.cboX.Items.Count > 0)
                {
                    this.cboX.SelectedIndex = 0;
                    this.cboY.SelectedIndex = 0;
                }
                if (this.cboX.Items.Count > 1)
                {
                    this.cboY.SelectedIndex = 1;
                }
            }
        }

        public IGeometry Geometry
        {
            get { return this.igeometry_0; }
            set { this.igeometry_0 = value; }
        }
    }
}