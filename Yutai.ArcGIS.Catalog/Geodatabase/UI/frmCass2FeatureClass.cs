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
    public class frmCass2FeatureClass : Form
    {
        private SimpleButton btnDelete;
        private SimpleButton btnSelectInputFeatures;
        private SimpleButton btnSelectOutLocation;
        private Container container_0 = null;
        private IGxObject igxObject_0 = null;
        private IName iname_0 = null;
        private int int_0 = 0;
        private int int_1 = 0;
        private Label label2;
        private Label labelFileName;
        private Label labelFN;
        private Label lblSelectObjects;
        private ListView listView1;
        private Panel panel1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ProgressBar progressBar2;
        private SimpleButton simpleButton1;
        private SimpleButton simpleButton2;
        private string string_0 = "";
        private TextEdit txtOutLocation;

        public frmCass2FeatureClass()
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
            OpenFileDialog dialog = new OpenFileDialog {
                Filter = "CASS交换文件(*.cas)|*.cas",
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
            frmOpenFile file = new frmOpenFile {
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
                        IWorkspaceName name = new WorkspaceNameClass {
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
                MessageBox.Show("请输入需要转换的数据");
                return false;
            }
            if (this.iname_0 == null)
            {
                MessageBox.Show("请选择输出位置");
                return false;
            }
            return true;
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCass2FeatureClass));
            this.btnDelete = new SimpleButton();
            this.listView1 = new ListView();
            this.label2 = new Label();
            this.txtOutLocation = new TextEdit();
            this.btnSelectInputFeatures = new SimpleButton();
            this.lblSelectObjects = new Label();
            this.btnSelectOutLocation = new SimpleButton();
            this.panel1 = new Panel();
            this.labelFN = new Label();
            this.labelFileName = new Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.simpleButton1 = new SimpleButton();
            this.simpleButton2 = new SimpleButton();
            this.txtOutLocation.Properties.BeginInit();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.btnDelete.Enabled = false;
            this.btnDelete.Image = (Image) resources.GetObject("btnDelete.Image");
            this.btnDelete.Location = new Point(0x110, 0x40);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(0x18, 0x18);
            this.btnDelete.TabIndex = 13;
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.listView1.GridLines = true;
            this.listView1.Location = new Point(8, 0x20);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0x100, 0x98);
            this.listView1.TabIndex = 12;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.List;
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 0xc0);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "输出位置";
            this.txtOutLocation.EditValue = "";
            this.txtOutLocation.Location = new Point(8, 0xd8);
            this.txtOutLocation.Name = "txtOutLocation";
            this.txtOutLocation.Properties.Appearance.BackColor = SystemColors.InactiveCaptionText;
            this.txtOutLocation.Properties.Appearance.Options.UseBackColor = true;
            this.txtOutLocation.Properties.ReadOnly = true;
            this.txtOutLocation.Size = new Size(0xf8, 0x15);
            this.txtOutLocation.TabIndex = 10;
            this.btnSelectInputFeatures.Image = (Image) resources.GetObject("btnSelectInputFeatures.Image");
            this.btnSelectInputFeatures.Location = new Point(0x110, 0x18);
            this.btnSelectInputFeatures.Name = "btnSelectInputFeatures";
            this.btnSelectInputFeatures.Size = new Size(0x18, 0x18);
            this.btnSelectInputFeatures.TabIndex = 9;
            this.btnSelectInputFeatures.Click += new EventHandler(this.btnSelectInputFeatures_Click);
            this.lblSelectObjects.AutoSize = true;
            this.lblSelectObjects.Location = new Point(8, 8);
            this.lblSelectObjects.Name = "lblSelectObjects";
            this.lblSelectObjects.Size = new Size(0x4d, 12);
            this.lblSelectObjects.TabIndex = 8;
            this.lblSelectObjects.Text = "CASS数据文件";
            this.btnSelectOutLocation.Image = (Image) resources.GetObject("btnSelectOutLocation.Image");
            this.btnSelectOutLocation.Location = new Point(0x110, 0xd8);
            this.btnSelectOutLocation.Name = "btnSelectOutLocation";
            this.btnSelectOutLocation.Size = new Size(0x18, 0x18);
            this.btnSelectOutLocation.TabIndex = 14;
            this.btnSelectOutLocation.Click += new EventHandler(this.btnSelectOutLocation_Click);
            this.panel1.Controls.Add(this.labelFN);
            this.panel1.Controls.Add(this.labelFileName);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.progressBar2);
            this.panel1.Location = new Point(8, 0x2b);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x160, 0x37);
            this.panel1.TabIndex = 0x10;
            this.panel1.Visible = false;
            this.labelFN.AutoSize = true;
            this.labelFN.Location = new Point(0x18, 0x68);
            this.labelFN.Name = "labelFN";
            this.labelFN.Size = new Size(0, 12);
            this.labelFN.TabIndex = 0x13;
            this.labelFileName.AutoSize = true;
            this.labelFileName.Location = new Point(0x18, 0x18);
            this.labelFileName.Name = "labelFileName";
            this.labelFileName.Size = new Size(0, 12);
            this.labelFileName.TabIndex = 0x12;
            this.progressBar1.Location = new Point(0x18, 0x30);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new Size(0x100, 0x18);
            this.progressBar1.TabIndex = 0x11;
            this.progressBar2.Location = new Point(0x18, 0x80);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new Size(0x100, 0x18);
            this.progressBar2.TabIndex = 0x10;
            this.simpleButton1.Location = new Point(0xa8, 0x100);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new Size(0x48, 0x18);
            this.simpleButton1.TabIndex = 0x11;
            this.simpleButton1.Text = "转换";
            this.simpleButton1.Click += new EventHandler(this.simpleButton1_Click);
            this.simpleButton2.DialogResult = DialogResult.Cancel;
            this.simpleButton2.Location = new Point(0xf8, 0x100);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new Size(80, 0x18);
            this.simpleButton2.TabIndex = 0x12;
            this.simpleButton2.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x170, 0x125);
            base.Controls.Add(this.simpleButton2);
            base.Controls.Add(this.simpleButton1);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.btnSelectOutLocation);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.lblSelectObjects);
            base.Controls.Add(this.txtOutLocation);
            base.Controls.Add(this.btnSelectInputFeatures);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) resources.GetObject("$Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmCass2FeatureClass";
            this.Text = "导入CASS数据";
            this.txtOutLocation.Properties.EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
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
            this.labelFN.Text = "处理图层<" + this.string_0 + "> ,转换第" + this.int_0.ToString() + " 个对象, 共 " + this.int_1.ToString() + " 个对象";
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (this.CanDo())
            {
                this.panel1.Visible = true;
                this.simpleButton1.Enabled = false;
                this.simpleButton2.Enabled = false;
                CASSReader reader = new CASSReader();
                reader.Step+=(new IFeatureProgress_StepEventHandler(this.method_5));
                IFeatureWorkspace workspace = this.iname_0.Open() as IFeatureWorkspace;
                reader.Workspace = workspace as IWorkspace;
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
                        reader.Read(text);
                    }
                    catch (Exception exception)
                    {
                        Logger.Current.Error("",exception, "");
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

