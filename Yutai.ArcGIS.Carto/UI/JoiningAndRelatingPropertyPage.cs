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
    public partial class JoiningAndRelatingPropertyPage : UserControl, ILayerAndStandaloneTablePropertyPage
    {
        private bool bool_0 = false;
        private Container container_0 = null;
        private IBasicMap ibasicMap_0 = null;
        private ITable itable_0 = null;

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
            frmJoining joining = new frmJoining
            {
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
            frmRelating relating = new frmRelating
            {
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
                MessageBox.Show(exception.Message, "COM Error: " + exception.ErrorCode.ToString(), MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
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
                MessageBox.Show(exception.Message, "COM Error: " + exception.ErrorCode.ToString(), MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            catch (Exception exception2)
            {
                MessageBox.Show(exception2.Message, ".NET Error: ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
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
                MessageBox.Show(exception.Message, "COM Error: " + exception.ErrorCode.ToString(), MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
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
                for (IRelationshipClass class3 = relationshipClasses.Next();
                    class3 != null;
                    class3 = relationshipClasses.Next())
                {
                    this.RelatingDataList.Items.Add(new ObjectWrap(class3));
                }
            }
            catch (COMException exception)
            {
                MessageBox.Show(exception.Message, "COM Error: " + exception.ErrorCode.ToString(), MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
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
            set { this.ibasicMap_0 = value; }
        }

        public bool IsPageDirty
        {
            get { return this.bool_0; }
        }

        public object SelectItem
        {
            set { this.itable_0 = value as ITable; }
        }

        internal partial class ObjectWrap
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
                get { return this.object_0; }
                set { this.object_0 = null; }
            }
        }
    }
}