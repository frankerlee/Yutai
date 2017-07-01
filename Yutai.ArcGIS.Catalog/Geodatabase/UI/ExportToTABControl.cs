using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common.Geodatabase;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal partial class ExportToTABControl : UserControl
    {
        private Container container_0 = null;
        private IArray iarray_0 = new ArrayClass();
        private IGxObject igxObject_0 = null;
        private IGxObject igxObject_1 = null;
        private IList ilist_0 = new ArrayList();
        private IName iname_0 = null;
        private IName iname_1 = null;
        private int int_0 = 0;
        private int int_1 = 0;
        private int int_2 = 1;

        public ExportToTABControl()
        {
            if (CatalogLicenseProviderCheck.Check())
            {
                this.InitializeComponent();
            }
        }

        public void Add(IGxObjectFilter igxObjectFilter_0)
        {
            if (igxObjectFilter_0 != null)
            {
                this.iarray_0.Add(igxObjectFilter_0);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            for (int i = this.listView1.Items.Count - 1; i >= 0; i--)
            {
                if (this.listView1.Items[i].Selected)
                {
                    this.ilist_0.Remove(i);
                    this.listView1.Items.RemoveAt(i);
                }
            }
        }

        private void btnSelectInputFeatures_Click(object sender, EventArgs e)
        {
            int num;
            frmOpenFile file = new frmOpenFile
            {
                Text = "添加数据"
            };
            file.RemoveAllFilters();
            file.AllowMultiSelect = true;
            if (this.iarray_0.Count != 0)
            {
                for (num = 0; num < this.iarray_0.Count; num++)
                {
                    if (num == 0)
                    {
                        file.AddFilter(this.iarray_0.get_Element(num) as IGxObjectFilter, true);
                    }
                    else
                    {
                        file.AddFilter(this.iarray_0.get_Element(num) as IGxObjectFilter, false);
                    }
                }
            }
            else
            {
                file.AddFilter(new MyGxFilterDatasets(), true);
            }
            if (file.DoModalOpen() == DialogResult.OK)
            {
                IArray items = file.Items;
                if (items.Count != 0)
                {
                    for (num = 0; num < items.Count; num++)
                    {
                        IGxObject obj2 = items.get_Element(num) as IGxObject;
                        this.ilist_0.Add(obj2.InternalObjectName);
                        this.listView1.Items.Add(obj2.FullName);
                    }
                }
            }
        }

        private void btnSelectOutLocation_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txtOutLocation.Text = dialog.SelectedPath;
            }
        }

        public bool CanDo()
        {
            if (this.ilist_0.Count == 0)
            {
                MessageBox.Show("请输入需要转换的要素类或表");
                return false;
            }
            if (this.txtOutLocation.Text.Trim().Length == 0)
            {
                MessageBox.Show("请选择输出位置");
                return false;
            }
            return true;
        }

        public void Clear()
        {
            this.iarray_0.RemoveAll();
        }

        public void Do()
        {
            try
            {
                this.panel1.Visible = true;
                ITABConvert convert = new ExportToMiTab();
                (convert as IFeatureProgress_Event).Step += (new IFeatureProgress_StepEventHandler(this.method_9));
                (convert as IConvertEvent).SetFeatureClassNameEnvent +=
                    new SetFeatureClassNameEnventHandler(this.method_2);
                (convert as IConvertEvent).SetFeatureCountEnvent += new SetFeatureCountEnventHandler(this.method_1);
                this.progressBar1.Minimum = 0;
                this.progressBar1.Maximum = this.ilist_0.Count;
                convert.InputFeatureClasses = this.ilist_0;
                if (this.cboType.SelectedIndex == 0)
                {
                    convert.Type = "tab";
                }
                else
                {
                    convert.Type = "mif";
                }
                convert.OutPath = this.txtOutLocation.Text;
                convert.Export();
            }
            catch (Exception exception)
            {
                Logger.Current.Error("", exception, "");
                MessageBox.Show(exception.Message);
            }
        }

        private void ExportToTABControl_Load(object sender, EventArgs e)
        {
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnDelete.Enabled = this.listView1.SelectedItems.Count > 0;
        }

        private IGxCatalog method_0(IGxObject igxObject_2)
        {
            if (igxObject_2 is IGxCatalog)
            {
                return (igxObject_2 as IGxCatalog);
            }
            for (IGxObject obj2 = igxObject_2.Parent; obj2 != null; obj2 = obj2.Parent)
            {
                if (obj2 is IGxCatalog)
                {
                    return (obj2 as IGxCatalog);
                }
            }
            return null;
        }

        private void method_1(int int_3)
        {
            if (int_3 > 0)
            {
                this.int_0 = int_3;
                this.progressBar2.Maximum = int_3;
                Application.DoEvents();
            }
        }

        private void method_2(string string_0)
        {
            this.int_1 = 0;
            this.progressBar2.Value = 0;
            this.progressBar1.Increment(1);
            this.labelFeatureClass.Text = "转换:" + string_0;
            Application.DoEvents();
        }

        private bool method_3()
        {
            return false;
        }

        private void method_4(string string_0)
        {
            this.progressBar2.Value = 0;
            this.progressBar1.Increment(1);
            this.labelFeatureClass.Text = "转换:" + string_0;
            Application.DoEvents();
        }

        private void method_5(int int_3)
        {
            if (int_3 > 0)
            {
                this.int_0 = int_3;
                this.progressBar2.Maximum = int_3;
            }
        }

        private void method_6(int int_3)
        {
            this.int_1 = int_3;
            this.progressBar2.Minimum = int_3;
        }

        private void method_7(int int_3)
        {
        }

        private void method_8(int int_3)
        {
            this.int_2 = int_3;
        }

        private void method_9()
        {
            this.int_1++;
            this.lblObj.Text = "共有 " + this.int_0.ToString() + "个对象, 处理第 " + this.int_1.ToString() + "个对象";
            this.progressBar2.Increment(this.int_2);
            Application.DoEvents();
        }

        public IGxObject InGxObject
        {
            set { this.igxObject_0 = value; }
        }

        public IGxObject OutGxObject
        {
            set { this.igxObject_1 = value; }
        }
    }
}