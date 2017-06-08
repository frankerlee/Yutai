using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Carto.UI
{
    public class JoiningAndRelatingPropertyPage : UserControl, ILayerAndStandaloneTablePropertyPage
    {
        private bool bool_0 = false;
        private SimpleButton btnAddJoining;
        private SimpleButton btnAddRelating;
        private SimpleButton btnRemoveAllJoining;
        private SimpleButton btnRemoveAllRelating;
        private SimpleButton btnRemoveJoining;
        private SimpleButton btnRemoveRelating;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private IBasicMap ibasicMap_0 = null;
        private ITable itable_0 = null;
        private ListBoxControl JoiningDataList;
        private Label label1;
        private Label label2;
        private ListBoxControl RelatingDataList;

        public event OnJoinAndRelationChangeHandler OnJoinAndRelationChange;

        public JoiningAndRelatingPropertyPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            return true;
        }

        private void btnAddJoining_Click(object sender, EventArgs e)
        {
            frmJoining joining = new frmJoining {
                FocusMap = this.ibasicMap_0,
                CurrentSelectItem = this.itable_0
            };
            if (joining.ShowDialog() == DialogResult.OK)
            {
                this.JoiningDataList.Items.Clear();
                if (this.OnJoinAndRelationChange != null)
                {
                    this.OnJoinAndRelationChange();
                }
                this.method_1(this.itable_0);
                if (this.JoiningDataList.Items.Count > 0)
                {
                    this.btnRemoveAllJoining.Enabled = true;
                }
            }
        }

        private void btnAddRelating_Click(object sender, EventArgs e)
        {
            frmRelating relating = new frmRelating {
                FocusMap = this.ibasicMap_0,
                CurrentSelectItem = this.itable_0
            };
            if (relating.ShowDialog() == DialogResult.OK)
            {
                this.RelatingDataList.Items.Clear();
                this.method_2(this.itable_0);
                if (this.OnJoinAndRelationChange != null)
                {
                    this.OnJoinAndRelationChange();
                }
                if (this.RelatingDataList.Items.Count > 0)
                {
                    this.btnRemoveAllRelating.Enabled = true;
                }
            }
        }

        private void btnRemoveAllJoining_Click(object sender, EventArgs e)
        {
            IDisplayRelationshipClass class2 = this.itable_0 as IDisplayRelationshipClass;
            if (class2 != null)
            {
                class2.DisplayRelationshipClass(null, esriJoinType.esriLeftInnerJoin);
                this.JoiningDataList.Items.Clear();
                if (this.OnJoinAndRelationChange != null)
                {
                    this.OnJoinAndRelationChange();
                }
                this.btnRemoveAllJoining.Enabled = false;
            }
        }

        private void btnRemoveAllRelating_Click(object sender, EventArgs e)
        {
            try
            {
                (this.itable_0 as IRelationshipClassCollectionEdit).RemoveAllRelationshipClasses();
                this.RelatingDataList.Items.Clear();
                this.btnRemoveAllRelating.Enabled = false;
                if (this.OnJoinAndRelationChange != null)
                {
                    this.OnJoinAndRelationChange();
                }
            }
            catch (COMException exception)
            {
                MessageBox.Show(exception.Message, "COM Error: " + exception.ErrorCode.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception exception2)
            {
                MessageBox.Show(exception2.Message, ".NET Error: ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnRemoveJoining_Click(object sender, EventArgs e)
        {
            IDisplayTable table = this.itable_0 as IDisplayTable;
            ITable displayTable = table.DisplayTable;
            if (displayTable is IRelQueryTable)
            {
                string str = this.JoiningDataList.SelectedItem.ToString();
                while (displayTable is IRelQueryTable)
                {
                    IRelQueryTable table3 = displayTable as IRelQueryTable;
                    if ((table3.DestinationTable as IDataset).Name == str)
                    {
                        break;
                    }
                    displayTable = table3.SourceTable;
                }
                if (displayTable is IRelQueryTable)
                {
                    IRelQueryTable table5 = displayTable as IRelQueryTable;
                    displayTable = table5.SourceTable;
                    IDisplayRelationshipClass class2 = this.itable_0 as IDisplayRelationshipClass;
                    if (displayTable is IRelQueryTable)
                    {
                        table5 = displayTable as IRelQueryTable;
                        IRelQueryTableInfo info = table5 as IRelQueryTableInfo;
                        class2.DisplayRelationshipClass(table5.RelationshipClass, info.JoinType);
                    }
                    else
                    {
                        class2.DisplayRelationshipClass(null, esriJoinType.esriLeftInnerJoin);
                    }
                    this.JoiningDataList.Items.Clear();
                    if (this.OnJoinAndRelationChange != null)
                    {
                        this.OnJoinAndRelationChange();
                    }
                    this.method_1(this.itable_0);
                    if (this.JoiningDataList.Items.Count > 0)
                    {
                        this.btnRemoveAllJoining.Enabled = true;
                    }
                    else
                    {
                        this.btnRemoveAllJoining.Enabled = false;
                    }
                }
            }
        }

        private void btnRemoveRelating_Click(object sender, EventArgs e)
        {
            try
            {
                IRelationshipClassCollection classs = this.itable_0 as IRelationshipClassCollection;
                IEnumRelationshipClass relationshipClasses = classs.RelationshipClasses;
                relationshipClasses.Reset();
                string str = this.RelatingDataList.SelectedItem.ToString();
                IRelationshipClass relationshipClass = relationshipClasses.Next();
                while (relationshipClass != null)
                {
                    if (str == (relationshipClass as IDataset).Name)
                    {
                        goto Label_0075;
                    }
                    relationshipClass = relationshipClasses.Next();
                }
                if (this.OnJoinAndRelationChange != null)
                {
                    this.OnJoinAndRelationChange();
                }
                return;
            Label_0075:
                (classs as IRelationshipClassCollectionEdit).RemoveRelationshipClass(relationshipClass);
                this.RelatingDataList.Items.Clear();
                this.method_2(this.itable_0);
                if (this.RelatingDataList.Items.Count > 0)
                {
                    this.btnRemoveAllRelating.Enabled = true;
                }
                else
                {
                    this.btnRemoveAllRelating.Enabled = false;
                }
            }
            catch (COMException exception)
            {
                MessageBox.Show(exception.Message, "COM Error: " + exception.ErrorCode.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception exception2)
            {
                MessageBox.Show(exception2.Message, ".NET Error: ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.label1 = new Label();
            this.groupBox2 = new GroupBox();
            this.label2 = new Label();
            this.JoiningDataList = new ListBoxControl();
            this.btnAddJoining = new SimpleButton();
            this.btnRemoveJoining = new SimpleButton();
            this.btnRemoveAllJoining = new SimpleButton();
            this.btnRemoveAllRelating = new SimpleButton();
            this.btnRemoveRelating = new SimpleButton();
            this.btnAddRelating = new SimpleButton();
            this.RelatingDataList = new ListBoxControl();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((ISupportInitialize) this.JoiningDataList).BeginInit();
            ((ISupportInitialize) this.RelatingDataList).BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.btnRemoveAllJoining);
            this.groupBox1.Controls.Add(this.btnRemoveJoining);
            this.groupBox1.Controls.Add(this.btnAddJoining);
            this.groupBox1.Controls.Add(this.JoiningDataList);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(0x10, 0x10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xd8, 200);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "连接";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0x18);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0xc4, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "显示添加到当前表/图层中的属性表";
            this.groupBox2.Controls.Add(this.btnRemoveAllRelating);
            this.groupBox2.Controls.Add(this.btnRemoveRelating);
            this.groupBox2.Controls.Add(this.btnAddRelating);
            this.groupBox2.Controls.Add(this.RelatingDataList);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new Point(240, 0x10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0xd8, 200);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "关联";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 0x18);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0xc4, 0x11);
            this.label2.TabIndex = 0;
            this.label2.Text = "显示添加到当前表/图层中的属性表";
            this.JoiningDataList.ItemHeight = 0x11;
            this.JoiningDataList.Location = new Point(8, 0x30);
            this.JoiningDataList.Name = "JoiningDataList";
            this.JoiningDataList.Size = new Size(0x80, 0x88);
            this.JoiningDataList.TabIndex = 1;
            this.JoiningDataList.SelectedIndexChanged += new EventHandler(this.JoiningDataList_SelectedIndexChanged);
            this.btnAddJoining.Location = new Point(0x90, 0x30);
            this.btnAddJoining.Name = "btnAddJoining";
            this.btnAddJoining.Size = new Size(0x40, 0x18);
            this.btnAddJoining.TabIndex = 2;
            this.btnAddJoining.Text = "添加";
            this.btnAddJoining.Click += new EventHandler(this.btnAddJoining_Click);
            this.btnRemoveJoining.Enabled = false;
            this.btnRemoveJoining.Location = new Point(0x90, 80);
            this.btnRemoveJoining.Name = "btnRemoveJoining";
            this.btnRemoveJoining.Size = new Size(0x40, 0x18);
            this.btnRemoveJoining.TabIndex = 3;
            this.btnRemoveJoining.Text = "移除";
            this.btnRemoveJoining.Click += new EventHandler(this.btnRemoveJoining_Click);
            this.btnRemoveAllJoining.Enabled = false;
            this.btnRemoveAllJoining.Location = new Point(0x90, 0x70);
            this.btnRemoveAllJoining.Name = "btnRemoveAllJoining";
            this.btnRemoveAllJoining.Size = new Size(0x40, 0x18);
            this.btnRemoveAllJoining.TabIndex = 4;
            this.btnRemoveAllJoining.Text = "移除所有";
            this.btnRemoveAllJoining.Click += new EventHandler(this.btnRemoveAllJoining_Click);
            this.btnRemoveAllRelating.Enabled = false;
            this.btnRemoveAllRelating.Location = new Point(0x90, 0x70);
            this.btnRemoveAllRelating.Name = "btnRemoveAllRelating";
            this.btnRemoveAllRelating.Size = new Size(0x40, 0x18);
            this.btnRemoveAllRelating.TabIndex = 8;
            this.btnRemoveAllRelating.Text = "移除所有";
            this.btnRemoveAllRelating.Click += new EventHandler(this.btnRemoveAllRelating_Click);
            this.btnRemoveRelating.Enabled = false;
            this.btnRemoveRelating.Location = new Point(0x90, 80);
            this.btnRemoveRelating.Name = "btnRemoveRelating";
            this.btnRemoveRelating.Size = new Size(0x40, 0x18);
            this.btnRemoveRelating.TabIndex = 7;
            this.btnRemoveRelating.Text = "移除";
            this.btnRemoveRelating.Click += new EventHandler(this.btnRemoveRelating_Click);
            this.btnAddRelating.Location = new Point(0x90, 0x30);
            this.btnAddRelating.Name = "btnAddRelating";
            this.btnAddRelating.Size = new Size(0x40, 0x18);
            this.btnAddRelating.TabIndex = 6;
            this.btnAddRelating.Text = "添加";
            this.btnAddRelating.Click += new EventHandler(this.btnAddRelating_Click);
            this.RelatingDataList.ItemHeight = 0x11;
            this.RelatingDataList.Location = new Point(8, 0x30);
            this.RelatingDataList.Name = "RelatingDataList";
            this.RelatingDataList.Size = new Size(0x80, 0x88);
            this.RelatingDataList.TabIndex = 5;
            this.RelatingDataList.SelectedIndexChanged += new EventHandler(this.RelatingDataList_SelectedIndexChanged);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "JoiningAndRelatingPropertyPage";
            base.Size = new Size(480, 0x130);
            base.Load += new EventHandler(this.JoiningAndRelatingPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((ISupportInitialize) this.JoiningDataList).EndInit();
            ((ISupportInitialize) this.RelatingDataList).EndInit();
            base.ResumeLayout(false);
        }

        private void JoiningAndRelatingPropertyPage_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

        private void JoiningDataList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.JoiningDataList.SelectedItem != null)
            {
                this.btnRemoveJoining.Enabled = true;
            }
            else
            {
                this.btnRemoveJoining.Enabled = false;
            }
        }

        private void method_0()
        {
            this.method_1(this.itable_0);
            if (this.JoiningDataList.Items.Count > 0)
            {
                this.btnRemoveAllJoining.Enabled = true;
            }
            if (this.itable_0 is IStandaloneTable)
            {
                this.btnAddRelating.Enabled = false;
            }
            else
            {
                this.method_2(this.itable_0);
                if (this.RelatingDataList.Items.Count > 0)
                {
                    this.btnRemoveAllRelating.Enabled = true;
                }
            }
        }

        private void method_1(ITable itable_1)
        {
            try
            {
                IDisplayTable table = itable_1 as IDisplayTable;
                if (table != null)
                {
                    IRelQueryTable table3;
                    for (ITable table2 = table.DisplayTable; table2 is IRelQueryTable; table2 = table3.SourceTable)
                    {
                        table3 = table2 as IRelQueryTable;
                        ITable destinationTable = table3.DestinationTable;
                        this.JoiningDataList.Items.Add(new ObjectWrap(destinationTable));
                    }
                }
            }
            catch (COMException exception)
            {
                MessageBox.Show(exception.Message, "COM Error: " + exception.ErrorCode.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception exception2)
            {
                MessageBox.Show(exception2.Message, ".NET Error: ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void method_2(ITable itable_1)
        {
            try
            {
                IRelationshipClassCollection classs = itable_1 as IRelationshipClassCollection;
                IEnumRelationshipClass relationshipClasses = classs.RelationshipClasses;
                relationshipClasses.Reset();
                for (IRelationshipClass class3 = relationshipClasses.Next(); class3 != null; class3 = relationshipClasses.Next())
                {
                    this.RelatingDataList.Items.Add(new ObjectWrap(class3));
                }
            }
            catch (COMException exception)
            {
                MessageBox.Show(exception.Message, "COM Error: " + exception.ErrorCode.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception exception2)
            {
                MessageBox.Show(exception2.Message, ".NET Error: ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void RelatingDataList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.RelatingDataList.SelectedItem != null)
            {
                this.btnRemoveRelating.Enabled = true;
            }
            else
            {
                this.btnRemoveRelating.Enabled = false;
            }
        }

        public IBasicMap FocusMap
        {
            set
            {
                this.ibasicMap_0 = value;
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_0;
            }
        }

        public object SelectItem
        {
            set
            {
                this.itable_0 = value as ITable;
            }
        }

        internal class ObjectWrap
        {
            private object object_0 = null;

            public ObjectWrap(object object_1)
            {
                this.object_0 = object_1;
            }

            public override string ToString()
            {
                if (this.object_0 is ILayer)
                {
                    return (this.object_0 as ILayer).Name;
                }
                if (this.object_0 is IDataset)
                {
                    return (this.object_0 as IDataset).Name;
                }
                return "";
            }

            public object Object
            {
                get
                {
                    return this.object_0;
                }
                set
                {
                    this.object_0 = null;
                }
            }
        }
    }
}

