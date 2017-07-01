using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Geodatabase;

namespace Yutai.ArcGIS.Catalog.UI
{
    public partial class frmDataLoader : Form
    {
        private FieldMatchCtrl fieldMatchCtrl_0 = new FieldMatchCtrl();
        private IContainer icontainer_0 = null;

        private int int_0 = 0;
        private SelectedDataLoaderCtrl selectedDataLoaderCtrl_0 = new SelectedDataLoaderCtrl();
        private SetImportRecordCtrl setImportRecordCtrl_0 = new SetImportRecordCtrl();

        public frmDataLoader()
        {
            this.InitializeComponent();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            switch (this.int_0)
            {
                case 0:
                    return;

                case 1:
                    this.btnLast.Enabled = false;
                    this.fieldMatchCtrl_0.Visible = false;
                    this.selectedDataLoaderCtrl_0.Visible = true;
                    break;

                case 2:
                    this.setImportRecordCtrl_0.Visible = false;
                    this.fieldMatchCtrl_0.Visible = true;
                    this.btnNext.Text = "下一步";
                    break;
            }
            this.int_0--;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            switch (this.int_0)
            {
                case 0:
                    if (this.selectedDataLoaderCtrl_0.SelectDataset.Count != 0)
                    {
                        IFields fields =
                            ((this.selectedDataLoaderCtrl_0.SelectDataset[0] as IGxDataset).Dataset as ITable).Fields;
                        IFields fields2 = (this.Table as ITable).Fields;
                        this.fieldMatchCtrl_0.TargertFields = fields2;
                        this.fieldMatchCtrl_0.SourceFields = fields;
                        this.fieldMatchCtrl_0.Init();
                        this.fieldMatchCtrl_0.Visible = true;
                        this.selectedDataLoaderCtrl_0.Visible = false;
                        this.btnLast.Enabled = true;
                        break;
                    }
                    MessageBox.Show("请选择要加载的数据!");
                    return;

                case 1:
                    this.setImportRecordCtrl_0.Table =
                        (this.selectedDataLoaderCtrl_0.SelectDataset[0] as IGxDataset).Dataset as ITable;
                    this.setImportRecordCtrl_0.Visible = true;
                    this.fieldMatchCtrl_0.Visible = false;
                    this.btnNext.Text = "完成";
                    break;

                case 2:
                    if (this.method_0())
                    {
                        base.DialogResult = DialogResult.OK;
                    }
                    return;
            }
            this.int_0++;
        }

        private void frmDataLoader_Load(object sender, EventArgs e)
        {
            this.selectedDataLoaderCtrl_0.Table = this.Table;
            this.selectedDataLoaderCtrl_0.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(this.selectedDataLoaderCtrl_0);
            this.fieldMatchCtrl_0.Visible = false;
            this.fieldMatchCtrl_0.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(this.fieldMatchCtrl_0);
            this.setImportRecordCtrl_0.Visible = false;
            this.setImportRecordCtrl_0.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(this.setImportRecordCtrl_0);
        }

        private bool method_0()
        {
            Dataloaders dataloaders = new Dataloaders();
            SortedList<string, string> fieldMaps = this.fieldMatchCtrl_0.FieldMaps;
            List<ITable> loadTables = this.selectedDataLoaderCtrl_0.LoadTables;
            string where = this.setImportRecordCtrl_0.Where;
            base.Enabled = false;
            dataloaders.LoadData(loadTables, where, this.Table as ITable, fieldMaps, 500);
            base.Enabled = true;
            MessageBox.Show("数据导入成功!");
            return true;
        }

        public IDataset Table { get; set; }
    }
}