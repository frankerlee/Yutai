using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common.Geodatabase;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmMapInfo2FeatureClass : Form
    {
        private Container container_0 = null;
        private IGxObject igxObject_0 = null;
        private IName iname_0 = null;
        private int int_0 = 0;
        private int int_1 = 0;
        private string string_0 = "";

        public frmMapInfo2FeatureClass()
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
                }
            }
        }

        private void btnSelectInputFeatures_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "MapInfo数据文件|*.tab;*.mif",
                Multiselect = true
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < dialog.FileNames.Length; i++)
                {
                    this.listView1.Items.Add(dialog.FileNames[i]);
                }
            }
        }

        private void btnSelectOutLocation_Click(object sender, EventArgs e)
        {
            frmOpenFile file = new frmOpenFile
            {
                Text = "保存位置"
            };
            file.RemoveAllFilters();
            file.AddFilter(new MyGxFilterWorkspaces(), true);
            if (file.DoModalSaveLocation() == DialogResult.OK)
            {
                IArray items = file.Items;
                if (items.Count != 0)
                {
                    this.igxObject_0 = items.get_Element(0) as IGxObject;
                    this.iname_0 = this.igxObject_0.InternalObjectName;
                    if (this.igxObject_0 is IGxDatabase)
                    {
                        this.iname_0 = this.igxObject_0.InternalObjectName;
                    }
                    else if (this.igxObject_0 is IGxFolder)
                    {
                        IWorkspaceName name = new WorkspaceNameClass
                        {
                            WorkspaceFactoryProgID = "esriDataSourcesFile.ShapefileWorkspaceFactory",
                            PathName = (this.igxObject_0.InternalObjectName as IFileName).Path
                        };
                        this.iname_0 = name as IName;
                    }
                    this.txtOutLocation.Text = this.igxObject_0.FullName;
                }
            }
        }

        public bool CanDo()
        {
            if (this.listView1.Items.Count == 0)
            {
                MessageBox.Show("请输入需要转换的MapInfo数据");
                return false;
            }
            if (this.iname_0 == null)
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
            Application.DoEvents();
            this.labelFN.Text = "处理:" + this.string_0 + "中第 " + this.int_0.ToString() + " 个对象, 共 " +
                                this.int_1.ToString() + " 个对象";
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (this.CanDo())
            {
                this.panel1.Visible = true;
                this.simpleButton1.Enabled = false;
                this.simpleButton2.Enabled = false;
                MiTab2DataConvert convert = new MiTab2DataConvert();
                convert.Step += (new IFeatureProgress_StepEventHandler(this.method_5));
                IFeatureWorkspace workspace = this.iname_0.Open() as IFeatureWorkspace;
                this.progressBar1.Minimum = 0;
                this.progressBar1.Maximum = this.listView1.Items.Count;
                int num = 0;
                while (true)
                {
                    if (num >= this.listView1.Items.Count)
                    {
                        break;
                    }
                    try
                    {
                        this.progressBar1.Increment(1);
                        string text = this.listView1.Items[num].Text;
                        this.labelFileName.Text = "导入:" + text;
                        Application.DoEvents();
                        convert._tab2(text, workspace);
                    }
                    catch (Exception exception)
                    {
                        Logger.Current.Error("", exception, "");
                        MessageBox.Show(exception.Message);
                    }
                    num++;
                }
                this.igxObject_0.Refresh();
                this.panel1.Visible = false;
                this.simpleButton1.Enabled = true;
                this.simpleButton2.Enabled = true;
                base.DialogResult = DialogResult.OK;
                base.Close();
            }
        }
    }
}