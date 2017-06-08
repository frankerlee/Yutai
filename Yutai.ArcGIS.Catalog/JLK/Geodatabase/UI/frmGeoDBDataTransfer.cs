namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.Geodatabase;
    using JLK.Catalog;
    using JLK.ControlExtend;
    using JLK.Editors;
    using JLK.Editors.Controls;
    using JLK.Utility;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    public class frmGeoDBDataTransfer : Form
    {
        private SimpleButton btnOK;
        private Container container_0 = null;
        private IEnumNameMapping ienumNameMapping_0 = null;
        private IGeoDBDataTransfer igeoDBDataTransfer_0 = null;
        private IName iname_0 = null;
        private int int_0 = 0;
        private int int_1 = 0;
        private int int_2 = 0;
        private int int_3 = 0;
        private int int_4 = 0;
        private int int_5 = 0;
        private Label label2;
        private EditListView listView1;
        private LVColumnHeader lvcolumnHeader_0;
        private LVColumnHeader lvcolumnHeader_1;
        private LVColumnHeader lvcolumnHeader_2;
        private Panel panel1;
        private Panel panel2;
        private System.Windows.Forms.ProgressBar progressBarObject;
        private System.Windows.Forms.ProgressBar progressBarObjectClass;
        private SimpleButton simpleButton2;
        private string string_0 = "";
        private Thread thread_0;
        private TextEdit txtObject;
        private TextEdit txtObjectClass;

        public frmGeoDBDataTransfer()
        {
            if (CatalogLicenseProviderCheck.Check())
            {
                this.InitializeComponent();
            }
        }

        public void BeginTransfer()
        {
            try
            {
                this.igeoDBDataTransfer_0.Transfer(this.ienumNameMapping_0, this.iname_0);
                base.DialogResult = DialogResult.OK;
            }
            catch (Exception exception)
            {
                MessageBox.Show("该组对象中存在不能转入到目标对象中数据!", "数据转换", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                CErrorLog.writeErrorLog(this, exception, "");
            }
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.method_3();
            this.listView1.Hide();
            base.FormBorderStyle = FormBorderStyle.None;
            base.Size = new Size(this.panel2.Width + 8, this.panel2.Height + this.panel1.Height);
            this.btnOK.Hide();
            this.simpleButton2.Hide();
            this.method_2();
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmGeoDBDataTransfer_Load(object sender, EventArgs e)
        {
            this.method_1();
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmGeoDBDataTransfer));
            this.panel1 = new Panel();
            this.simpleButton2 = new SimpleButton();
            this.btnOK = new SimpleButton();
            this.panel2 = new Panel();
            this.txtObject = new TextEdit();
            this.txtObjectClass = new TextEdit();
            this.progressBarObject = new System.Windows.Forms.ProgressBar();
            this.progressBarObjectClass = new System.Windows.Forms.ProgressBar();
            this.label2 = new Label();
            this.listView1 = new EditListView();
            this.lvcolumnHeader_0 = new LVColumnHeader();
            this.lvcolumnHeader_1 = new LVColumnHeader();
            this.lvcolumnHeader_2 = new LVColumnHeader();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.txtObject.Properties.BeginInit();
            this.txtObjectClass.Properties.BeginInit();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.simpleButton2);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 0x10b);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x1a0, 0x1a);
            this.panel1.TabIndex = 2;
            this.simpleButton2.DialogResult = DialogResult.Cancel;
            this.simpleButton2.Location = new Point(0x120, 1);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new Size(0x38, 0x18);
            this.simpleButton2.TabIndex = 1;
            this.simpleButton2.Text = "取消";
            this.btnOK.Location = new Point(0xd8, 1);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x38, 0x18);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.panel2.Controls.Add(this.txtObject);
            this.panel2.Controls.Add(this.txtObjectClass);
            this.panel2.Controls.Add(this.progressBarObject);
            this.panel2.Controls.Add(this.progressBarObjectClass);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x1a0, 0x61);
            this.panel2.TabIndex = 4;
            this.txtObject.EditValue = "";
            this.txtObject.Location = new Point(0x10, 80);
            this.txtObject.Name = "txtObject";
            this.txtObject.Properties.AllowFocused = false;
            this.txtObject.Properties.Appearance.BackColor = SystemColors.InactiveBorder;
            this.txtObject.Properties.Appearance.Options.UseBackColor = true;
            this.txtObject.Properties.BorderStyle = BorderStyles.NoBorder;
            this.txtObject.Properties.ReadOnly = true;
            this.txtObject.ShowToolTips = false;
            this.txtObject.Size = new Size(0x180, 0x13);
            this.txtObject.TabIndex = 11;
            this.txtObjectClass.EditValue = "";
            this.txtObjectClass.Location = new Point(0x120, 0x10);
            this.txtObjectClass.Name = "txtObjectClass";
            this.txtObjectClass.Properties.AllowFocused = false;
            this.txtObjectClass.Properties.Appearance.BackColor = SystemColors.InactiveBorder;
            this.txtObjectClass.Properties.Appearance.Options.UseBackColor = true;
            this.txtObjectClass.Properties.BorderStyle = BorderStyles.NoBorder;
            this.txtObjectClass.Properties.ReadOnly = true;
            this.txtObjectClass.Size = new Size(0x70, 0x13);
            this.txtObjectClass.TabIndex = 10;
            this.progressBarObject.Location = new Point(8, 0x68);
            this.progressBarObject.Name = "progressBarObject";
            this.progressBarObject.Size = new Size(0x188, 0x18);
            this.progressBarObject.TabIndex = 9;
            this.progressBarObjectClass.Location = new Point(0x10, 0x30);
            this.progressBarObjectClass.Name = "progressBarObjectClass";
            this.progressBarObjectClass.Size = new Size(0x180, 0x18);
            this.progressBarObjectClass.TabIndex = 7;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x10, 0x10);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x8f, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "正在传送数据，请稍候...";
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.lvcolumnHeader_0, this.lvcolumnHeader_1, this.lvcolumnHeader_2 });
            this.listView1.ComboBoxBgColor = Color.LightBlue;
            this.listView1.ComboBoxFont = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.listView1.Dock = DockStyle.Fill;
            this.listView1.EditBgColor = Color.LightBlue;
            this.listView1.EditFont = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new Point(0, 0);
            this.listView1.LockRowCount = 0;
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0x1a0, 0x10b);
            this.listView1.TabIndex = 6;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.ValueChanged += new JLK.ControlExtend.ValueChangedHandler(this.method_13);
            this.lvcolumnHeader_0.ColumnStyle = ListViewColumnStyle.ReadOnly;
            this.lvcolumnHeader_0.Text = "类型";
            this.lvcolumnHeader_0.Width = 0x62;
            this.lvcolumnHeader_1.ColumnStyle = ListViewColumnStyle.ReadOnly;
            this.lvcolumnHeader_1.Text = "源名";
            this.lvcolumnHeader_1.Width = 0x88;
            this.lvcolumnHeader_2.ColumnStyle = ListViewColumnStyle.EditBox;
            this.lvcolumnHeader_2.Text = "目标名";
            this.lvcolumnHeader_2.Width = 0xa2;
            base.StartPosition = FormStartPosition.CenterParent;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x1a0, 0x125);
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Icon = (Icon) manager.GetObject("$Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmGeoDBDataTransfer";
            this.Text = "数据传送";
            base.Load += new EventHandler(this.frmGeoDBDataTransfer_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.txtObject.Properties.EndInit();
            this.txtObjectClass.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private string method_0(IName iname_1)
        {
            if (iname_1 is IFeatureClassName)
            {
                return "要素类";
            }
            if (iname_1 is IFeatureDatasetName)
            {
                return "要素集";
            }
            if (iname_1 is ITableName)
            {
                return "表";
            }
            if (iname_1 is IGeometricNetworkName)
            {
                return "几何网络";
            }
            if (iname_1 is IRelationshipClassName)
            {
                return "关系类";
            }
            if (iname_1 is ITopologyName)
            {
                return "拓扑";
            }
            return "";
        }

        private void method_1()
        {
            this.listView1.Items.Clear();
            this.ienumNameMapping_0.Reset();
            INameMapping mapping = this.ienumNameMapping_0.Next();
            string[] items = new string[3];
            while (mapping != null)
            {
                ListViewItem item;
                if (mapping.SourceObject is IDomain)
                {
                    IDomain sourceObject = mapping.SourceObject as IDomain;
                    if (sourceObject.Type == esriDomainType.esriDTCodedValue)
                    {
                        items[0] = "CV域";
                    }
                    else
                    {
                        items[0] = "范围域";
                    }
                    items[1] = sourceObject.Name;
                    if (mapping.NameConflicts)
                    {
                        mapping.TargetName = mapping.GetSuggestedName(this.iname_0);
                    }
                    items[2] = mapping.TargetName;
                    item = new ListViewItem(items) {
                        Tag = mapping
                    };
                    this.listView1.Items.Add(item);
                }
                else if (mapping.SourceObject is IName)
                {
                    IName name = mapping.SourceObject as IName;
                    items[0] = this.method_0(name);
                    items[1] = (name as IDatasetName).Name;
                    if (mapping.NameConflicts)
                    {
                        mapping.TargetName = mapping.GetSuggestedName(this.iname_0);
                    }
                    items[2] = mapping.TargetName;
                    item = new ListViewItem(items) {
                        Tag = mapping
                    };
                    this.listView1.Items.Add(item);
                    IEnumNameMapping children = mapping.Children;
                    if (children != null)
                    {
                        children.Reset();
                        for (INameMapping mapping3 = children.Next(); mapping3 != null; mapping3 = children.Next())
                        {
                            name = mapping3.SourceObject as IName;
                            items[0] = "  " + this.method_0(name);
                            items[1] = (name as IDatasetName).Name;
                            if (mapping3.NameConflicts)
                            {
                                mapping3.TargetName = mapping3.GetSuggestedName(this.iname_0);
                            }
                            items[2] = mapping3.TargetName;
                            item = new ListViewItem(items) {
                                Tag = mapping3
                            };
                            this.listView1.Items.Add(item);
                        }
                    }
                }
                mapping = this.ienumNameMapping_0.Next();
            }
        }

        private void method_10(int int_6)
        {
            this.progressBarObject.Value = int_6;
        }

        private void method_11(int int_6)
        {
            this.int_5 = int_6;
        }

        private void method_12()
        {
            this.progressBarObjectClass.Increment(1);
            if (this.progressBarObjectClass.Value == this.progressBarObjectClass.Maximum)
            {
                this.progressBarObjectClass.Value = this.progressBarObjectClass.Minimum;
            }
            Application.DoEvents();
        }

        private void method_13(object sender, ValueChangedEventArgs e)
        {
            ListViewItem item = this.listView1.Items[e.Row];
            if (item.Tag is INameMapping)
            {
                INameMapping tag = item.Tag as INameMapping;
                if (tag.TargetName != e.NewValue.ToString())
                {
                    tag.TargetName = e.NewValue.ToString();
                }
            }
        }

        private void method_2()
        {
            this.BeginTransfer();
        }

        private void method_3()
        {
            this.ienumNameMapping_0.Reset();
            for (INameMapping mapping = this.ienumNameMapping_0.Next(); mapping != null; mapping = this.ienumNameMapping_0.Next())
            {
                if (mapping.SourceObject is IName)
                {
                    IName sourceObject = mapping.SourceObject as IName;
                    if (sourceObject is IFeatureClassName)
                    {
                        this.int_0++;
                    }
                    else if (sourceObject is ITableName)
                    {
                        this.int_0++;
                    }
                    IEnumNameMapping children = mapping.Children;
                    if (children != null)
                    {
                        children.Reset();
                        for (INameMapping mapping3 = children.Next(); mapping3 != null; mapping3 = children.Next())
                        {
                            sourceObject = mapping3.SourceObject as IName;
                            if (sourceObject is IFeatureClassName)
                            {
                                this.int_0++;
                            }
                            else if (sourceObject is ITableName)
                            {
                                this.int_0++;
                            }
                        }
                    }
                }
            }
        }

        private void method_4()
        {
            try
            {
                this.txtObjectClass.Text = string.Concat(new object[] { "传送第 ", this.progressBarObjectClass.Value.ToString(), "个对象，共 ", this.int_0, "个对象" });
                this.thread_0.Join(3);
            }
            catch
            {
            }
        }

        private void method_5()
        {
            try
            {
                this.txtObject.Text = "传送" + this.string_0 + " ，第 " + this.progressBarObject.Value.ToString() + " 个对象, 共" + this.int_4.ToString() + " 个对象";
                this.thread_0.Join(3);
            }
            catch
            {
            }
        }

        private void method_6(string string_1)
        {
            this.int_1++;
            this.progressBarObjectClass.Increment(1);
            this.string_0 = string_1;
            this.int_2 = 0;
            this.progressBarObject.Minimum = 0;
            this.progressBarObject.Maximum = 100;
            this.txtObjectClass.Text = string.Concat(new object[] { "传送第 ", this.progressBarObjectClass.Value.ToString(), "个对象，共 ", this.int_0, "个对象" });
            Application.DoEvents();
        }

        private bool method_7()
        {
            return false;
        }

        private void method_8(int int_6)
        {
            this.progressBarObject.Minimum = int_6;
        }

        private void method_9(int int_6)
        {
            this.progressBarObject.Maximum = int_6;
            int_6 = int_6;
        }

        public IEnumNameMapping EnumNameMapping
        {
            set
            {
                this.ienumNameMapping_0 = value;
            }
        }

        public IGeoDBDataTransfer GeoDBTransfer
        {
            set
            {
                this.igeoDBDataTransfer_0 = value;
                (this.igeoDBDataTransfer_0 as IFeatureProgress_Event).add_Step(new IFeatureProgress_StepEventHandler(this.method_12));
            }
        }

        public IName ToName
        {
            set
            {
                this.iname_0 = value;
            }
        }
    }
}

