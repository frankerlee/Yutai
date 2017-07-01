using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.ControlExtend;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmGeoDBDataTransfer : Form
    {
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
        private string string_0 = "";

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
                Logger.Current.Error("", exception, "");
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

        private void frmGeoDBDataTransfer_Load(object sender, EventArgs e)
        {
            this.method_1();
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
                    item = new ListViewItem(items)
                    {
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
                    item = new ListViewItem(items)
                    {
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
                            item = new ListViewItem(items)
                            {
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
            for (INameMapping mapping = this.ienumNameMapping_0.Next();
                mapping != null;
                mapping = this.ienumNameMapping_0.Next())
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
                this.txtObjectClass.Text =
                    string.Concat(new object[]
                        {"传送第 ", this.progressBarObjectClass.Value.ToString(), "个对象，共 ", this.int_0, "个对象"});
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
                this.txtObject.Text = "传送" + this.string_0 + " ，第 " + this.progressBarObject.Value.ToString() +
                                      " 个对象, 共" + this.int_4.ToString() + " 个对象";
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
            this.txtObjectClass.Text =
                string.Concat(new object[]
                    {"传送第 ", this.progressBarObjectClass.Value.ToString(), "个对象，共 ", this.int_0, "个对象"});
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
            set { this.ienumNameMapping_0 = value; }
        }

        public IGeoDBDataTransfer GeoDBTransfer
        {
            set
            {
                this.igeoDBDataTransfer_0 = value;
                (this.igeoDBDataTransfer_0 as IFeatureProgress_Event).Step +=
                    (new IFeatureProgress_StepEventHandler(this.method_12));
            }
        }

        public IName ToName
        {
            set { this.iname_0 = value; }
        }
    }
}