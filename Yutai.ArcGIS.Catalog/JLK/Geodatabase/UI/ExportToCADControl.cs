namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.Geodatabase;
    using JLK.Catalog;
    using JLK.Catalog.UI;
    using JLK.Editors;
    using JLK.Editors.Controls;
    using JLK.Utility;
    using JLK.Utility.Geodatabase;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Resources;
    using System.Windows.Forms;

    internal class ExportToCADControl : UserControl
    {
        private SimpleButton btnDelete;
        private SimpleButton btnSelectInputFeatures;
        private SimpleButton btnSelectOutLocation;
        private ComboBoxEdit cboType;
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
        private Label label1;
        private Label label2;
        private Label labelFeatureClass;
        private Label lblObj;
        private Label lblSelectObjects;
        private ListView listView1;
        private Panel panel1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ProgressBar progressBar2;
        private TextEdit txtOutLocation;

        public ExportToCADControl()
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
            frmOpenFile file = new frmOpenFile {
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
            SaveFileDialog dialog = new SaveFileDialog {
                Filter = "*.dxf|*.dxf"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txtOutLocation.Text = dialog.FileName;
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

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        public void Do()
        {
            try
            {
                this.panel1.Visible = true;
                ICADConvert convert = new CADConvert();
                (convert as IFeatureProgress_Event).add_Step(new IFeatureProgress_StepEventHandler(this.method_7));
                (convert as IConvertEvent).SetFeatureClassNameEnvent += new SetFeatureClassNameEnventHandler(this.method_2);
                (convert as IConvertEvent).SetFeatureCountEnvent += new SetFeatureCountEnventHandler(this.method_3);
                this.progressBar1.Minimum = 0;
                this.progressBar1.Maximum = this.ilist_0.Count;
                convert.InputFeatureClasses = this.ilist_0;
                if (this.cboType.SelectedIndex == 0)
                {
                    convert.AutoCADVersion = "R14";
                }
                else
                {
                    convert.AutoCADVersion = "R2000";
                }
                convert.OutPath = this.txtOutLocation.Text;
                convert.Export();
            }
            catch (Exception exception)
            {
                CErrorLog.writeErrorLog(this, exception, "");
                MessageBox.Show(exception.Message);
            }
        }

        private void ExportToCADControl_Load(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            ResourceManager manager = new ResourceManager(typeof(ExportToCADControl));
            this.lblSelectObjects = new Label();
            this.btnSelectInputFeatures = new SimpleButton();
            this.txtOutLocation = new TextEdit();
            this.btnSelectOutLocation = new SimpleButton();
            this.label2 = new Label();
            this.listView1 = new ListView();
            this.btnDelete = new SimpleButton();
            this.cboType = new ComboBoxEdit();
            this.label1 = new Label();
            this.panel1 = new Panel();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblObj = new Label();
            this.labelFeatureClass = new Label();
            this.txtOutLocation.Properties.BeginInit();
            this.cboType.Properties.BeginInit();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.lblSelectObjects.AutoSize = true;
            this.lblSelectObjects.Location = new Point(8, 8);
            this.lblSelectObjects.Name = "lblSelectObjects";
            this.lblSelectObjects.Size = new Size(0x42, 0x11);
            this.lblSelectObjects.TabIndex = 0;
            this.lblSelectObjects.Text = "输入要素类";
            this.btnSelectInputFeatures.Image = (Image) manager.GetObject("btnSelectInputFeatures.Image");
            this.btnSelectInputFeatures.Location = new Point(0x110, 0x18);
            this.btnSelectInputFeatures.Name = "btnSelectInputFeatures";
            this.btnSelectInputFeatures.Size = new Size(0x18, 0x18);
            this.btnSelectInputFeatures.TabIndex = 2;
            this.btnSelectInputFeatures.Click += new EventHandler(this.btnSelectInputFeatures_Click);
            this.txtOutLocation.EditValue = @"c:\tmp.dxf";
            this.txtOutLocation.Location = new Point(8, 0xd8);
            this.txtOutLocation.Name = "txtOutLocation";
            this.txtOutLocation.Properties.Appearance.BackColor = SystemColors.InactiveCaptionText;
            this.txtOutLocation.Properties.Appearance.Options.UseBackColor = true;
            this.txtOutLocation.Properties.ReadOnly = true;
            this.txtOutLocation.Size = new Size(0xf8, 0x17);
            this.txtOutLocation.TabIndex = 3;
            this.btnSelectOutLocation.Image = (Image) manager.GetObject("btnSelectOutLocation.Image");
            this.btnSelectOutLocation.Location = new Point(0x108, 0xd8);
            this.btnSelectOutLocation.Name = "btnSelectOutLocation";
            this.btnSelectOutLocation.Size = new Size(0x18, 0x18);
            this.btnSelectOutLocation.TabIndex = 4;
            this.btnSelectOutLocation.Click += new EventHandler(this.btnSelectOutLocation_Click);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 0xc0);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x36, 0x11);
            this.label2.TabIndex = 5;
            this.label2.Text = "输出位置";
            this.listView1.GridLines = true;
            this.listView1.Location = new Point(8, 0x20);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0x100, 120);
            this.listView1.TabIndex = 6;
            this.listView1.View = View.List;
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.btnDelete.Enabled = false;
            this.btnDelete.Image = (Image) manager.GetObject("btnDelete.Image");
            this.btnDelete.Location = new Point(0x110, 0x40);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(0x18, 0x18);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.cboType.EditValue = "DXF-R14";
            this.cboType.Location = new Point(0x48, 160);
            this.cboType.Name = "cboType";
            this.cboType.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboType.Properties.Items.AddRange(new object[] { "DXF-R14", "DXF-R2000" });
            this.cboType.Size = new Size(0xc0, 0x17);
            this.cboType.TabIndex = 9;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0xa2);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x36, 0x11);
            this.label1.TabIndex = 10;
            this.label1.Text = "输出类型";
            this.panel1.Controls.Add(this.progressBar2);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.lblObj);
            this.panel1.Controls.Add(this.labelFeatureClass);
            this.panel1.Location = new Point(0, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x130, 240);
            this.panel1.TabIndex = 11;
            this.panel1.Visible = false;
            this.progressBar2.Location = new Point(8, 0x80);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new Size(0x100, 0x18);
            this.progressBar2.TabIndex = 3;
            this.progressBar1.Location = new Point(8, 40);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new Size(0x100, 0x18);
            this.progressBar1.TabIndex = 2;
            this.lblObj.AutoSize = true;
            this.lblObj.Location = new Point(8, 0x55);
            this.lblObj.Name = "lblObj";
            this.lblObj.Size = new Size(0, 0x11);
            this.lblObj.TabIndex = 1;
            this.labelFeatureClass.AutoSize = true;
            this.labelFeatureClass.Location = new Point(8, 8);
            this.labelFeatureClass.Name = "labelFeatureClass";
            this.labelFeatureClass.Size = new Size(0, 0x11);
            this.labelFeatureClass.TabIndex = 0;
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.cboType);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.btnSelectOutLocation);
            base.Controls.Add(this.txtOutLocation);
            base.Controls.Add(this.btnSelectInputFeatures);
            base.Controls.Add(this.lblSelectObjects);
            base.Name = "ExportToCADControl";
            base.Size = new Size(0x130, 0x108);
            base.Load += new EventHandler(this.ExportToCADControl_Load);
            this.txtOutLocation.Properties.EndInit();
            this.cboType.Properties.EndInit();
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
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

        private bool method_1()
        {
            return false;
        }

        private void method_2(string string_0)
        {
            this.progressBar2.Value = 0;
            this.progressBar1.Increment(1);
            this.labelFeatureClass.Text = "转换:" + string_0;
            Application.DoEvents();
        }

        private void method_3(int int_3)
        {
            if (int_3 > 0)
            {
                this.int_0 = int_3;
                this.progressBar2.Maximum = int_3;
            }
        }

        private void method_4(int int_3)
        {
            this.int_1 = int_3;
            this.progressBar2.Minimum = int_3;
        }

        private void method_5(int int_3)
        {
        }

        private void method_6(int int_3)
        {
            this.int_2 = int_3;
        }

        private void method_7()
        {
            this.int_1++;
            this.lblObj.Text = "共有 " + this.int_0.ToString() + "个对象, 处理第 " + this.int_1.ToString() + "个对象";
            this.progressBar2.Increment(this.int_2);
            Application.DoEvents();
        }

        public IGxObject InGxObject
        {
            set
            {
                this.igxObject_0 = value;
            }
        }

        public IGxObject OutGxObject
        {
            set
            {
                this.igxObject_1 = value;
            }
        }
    }
}

