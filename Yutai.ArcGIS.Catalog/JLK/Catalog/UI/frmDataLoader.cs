namespace JLK.Catalog.UI
{
    using ESRI.ArcGIS.Geodatabase;
    using JLK.Catalog;
    using JLK.Editors;
    using JLK.Utility.Geodatabase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class frmDataLoader : Form
    {
        private SimpleButton btnCancel;
        private SimpleButton btnLast;
        private SimpleButton btnNext;
        private FieldMatchCtrl fieldMatchCtrl_0 = new FieldMatchCtrl();
        private IContainer icontainer_0 = null;
        [CompilerGenerated]
        private IDataset idataset_0;
        private int int_0 = 0;
        private Panel panel1;
        private Panel panel2;
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
                        IFields fields = ((this.selectedDataLoaderCtrl_0.SelectDataset[0] as IGxDataset).Dataset as ITable).Fields;
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
                    this.setImportRecordCtrl_0.Table = (this.selectedDataLoaderCtrl_0.SelectDataset[0] as IGxDataset).Dataset as ITable;
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

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
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

        private void InitializeComponent()
        {
            this.panel2 = new Panel();
            this.panel1 = new Panel();
            this.btnNext = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.btnLast = new SimpleButton();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x1da, 0x144);
            this.panel2.TabIndex = 3;
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnLast);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 0x144);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x1da, 0x1c);
            this.panel1.TabIndex = 2;
            this.btnNext.Location = new Point(0xa7, 2);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(0x40, 0x18);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "下一步";
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0x100, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x40, 0x18);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnLast.Enabled = false;
            this.btnLast.Location = new Point(0x53, 2);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new Size(0x40, 0x18);
            this.btnLast.TabIndex = 0;
            this.btnLast.Text = "上一步";
            this.btnLast.Click += new EventHandler(this.btnLast_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1da, 0x160);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Name = "frmDataLoader";
            this.Text = "装载数据";
            base.Load += new EventHandler(this.frmDataLoader_Load);
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
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

        public IDataset Table
        {
            [CompilerGenerated]
            get
            {
                return this.idataset_0;
            }
            [CompilerGenerated]
            set
            {
                this.idataset_0 = value;
            }
        }
    }
}

