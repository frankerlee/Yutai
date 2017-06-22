using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmFeatureClass2VCT : Form
    {
        private Container container_0 = null;
        private IGxObject igxObject_0 = null;
        private IList ilist_0 = new ArrayList();
        private IName iname_0 = null;
        private int int_0 = 0;
        private int int_1 = 0;
        private ISpatialReference ispatialReference_0 = null;
        private string string_0 = "";

        public frmFeatureClass2VCT()
        {
            this.InitializeComponent();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            for (int i = this.listView1.Items.Count - 1; i >= 0; i--)
            {
                if (this.listView1.Items[i].Selected)
                {
                    this.listView1.Items.RemoveAt(i);
                    this.ilist_0.RemoveAt(i);
                }
            }
            if (this.ilist_0.Count == 0)
            {
                this.ispatialReference_0 = null;
            }
        }

        private void btnSelectInputFeatures_Click(object sender, EventArgs e)
        {
            frmOpenFile file = new frmOpenFile {
                Text = "添加数据"
            };
            file.RemoveAllFilters();
            file.AllowMultiSelect = true;
            file.AddFilter(new MyGxFilterDatasets(), true);
            if (file.DoModalOpen() == DialogResult.OK)
            {
                IArray items = file.Items;
                if (items.Count != 0)
                {
                    for (int i = 0; i < items.Count; i++)
                    {
                        IGxObject obj2 = items.get_Element(i) as IGxObject;
                        if (obj2 is IGxDataset)
                        {
                            IDataset dataset = (obj2 as IGxDataset).Dataset;
                            if (this.ispatialReference_0 == null)
                            {
                                this.ispatialReference_0 = (dataset as IGeoDataset).SpatialReference;
                            }
                            else
                            {
                                ISpatialReference spatialReference = (dataset as IGeoDataset).SpatialReference;
                                if (!(!(spatialReference is IUnknownCoordinateSystem) || (this.ispatialReference_0 is IUnknownCoordinateSystem)))
                                {
                                    MessageBox.Show("空间参考不一致，" + dataset.Name + "不能添加");
                                    continue;
                                }
                                if (!(!(spatialReference is IProjectedCoordinateSystem) || (this.ispatialReference_0 is IProjectedCoordinateSystem)))
                                {
                                    MessageBox.Show("空间参考不一致，" + dataset.Name + "不能添加");
                                    continue;
                                }
                                if (!(!(spatialReference is IGeographicCoordinateSystem) || (this.ispatialReference_0 is IGeographicCoordinateSystem)))
                                {
                                    MessageBox.Show("空间参考不一致，" + dataset.Name + "不能添加");
                                    continue;
                                }
                                if (((spatialReference is IProjectedCoordinateSystem) && (this.ispatialReference_0 is IProjectedCoordinateSystem)) && !(spatialReference as IClone).IsEqual(this.ispatialReference_0 as IClone))
                                {
                                    MessageBox.Show("空间参考不一致，" + dataset.Name + "不能添加");
                                    continue;
                                }
                            }
                            this.ilist_0.Add(dataset);
                            this.listView1.Items.Add(obj2.FullName);
                        }
                    }
                }
            }
        }

        private void btnSelectOutLocation_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog {
                Filter = "VCT文件(*.vct)|*.vct"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txtOutLocation.Text = dialog.FileName;
            }
        }

        public bool CanDo()
        {
            if (this.listView1.Items.Count == 0)
            {
                MessageBox.Show("请输入需要转换的数据");
                return false;
            }
            if (this.txtOutLocation.Text.Length == 0)
            {
                MessageBox.Show("请选择输出位置");
                return false;
            }
            return true;
        }

 private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                this.btnDelete.Enabled = true;
            }
            else
            {
                this.btnDelete.Enabled = false;
            }
        }

        private void method_0(string string_1)
        {
            this.string_0 = string_1;
            this.labelFN.Text = "处理:" + string_1;
        }

        private void method_1(int int_2)
        {
            this.int_0 = int_2;
            this.progressBar2.Minimum = int_2;
        }

        private void method_2(int int_2)
        {
            this.int_1 = int_2;
            this.progressBar2.Maximum = int_2;
        }

        private void method_3(int int_2)
        {
            this.progressBar2.Value = int_2;
        }

        private void method_4(int int_2)
        {
            this.progressBar2.Step = int_2;
        }

        private void method_5()
        {
            this.int_0++;
            this.progressBar2.Increment(1);
            this.labelFN.Text = "处理图层<" + this.string_0 + "> ,转换第" + this.int_0.ToString() + " 个对象, 共 " + this.int_1.ToString() + " 个对象";
            Application.DoEvents();
        }

        private void method_6(object object_0, string string_1)
        {
            this.labelFileName.Text = string_1;
            Application.DoEvents();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (this.CanDo())
            {
                this.panel1.Visible = true;
                this.simpleButton1.Enabled = false;
                this.simpleButton2.Enabled = false;
                VCTWrite write = new VCTWrite();
                write.ProgressMessage += new ProgressMessageHandle(this.method_6);
                write.Step+=(new IFeatureProgress_StepEventHandler(this.method_5));
                for (int i = 0; i < this.ilist_0.Count; i++)
                {
                    write.AddDataset(this.ilist_0[i] as IDataset);
                }
                write.Write(this.txtOutLocation.Text);
                this.panel1.Visible = false;
                this.simpleButton1.Enabled = true;
                this.simpleButton2.Enabled = true;
                base.DialogResult = DialogResult.OK;
                base.Close();
            }
        }
    }
}

