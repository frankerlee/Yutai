using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Accessibility;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;

namespace Yutai.ArcGIS.Common.ControlExtend
{
    public class TreeViewComboBoxEdit : PopupContainerEdit
    {
        private bool bool_0 = false;
        private CheckedListBoxItem checkedListBoxItem_0 = null;
        private EditorClassInfo editorClassInfo_0 = null;
        private PanelControl panelControl_0;
        private PanelControl panelControl_1;
        private SimpleButton simpleButton_0 = null;
        private SimpleButton simpleButton_1 = null;
        private TreeView treeView_0 = null;

        protected virtual PopupContainerControl CreatePopupCheckListControl()
        {
            this.treeView_0 = new TreeView();
            this.treeView_0.BeforeExpand += new TreeViewCancelEventHandler(this.treeView_0_BeforeExpand);
            this.simpleButton_0 = new SimpleButton();
            this.simpleButton_1 = new SimpleButton();
            PopupContainerControl control = new PopupContainerControl();
            this.simpleButton_1.Text = Localizer.Active.GetLocalizedString(StringId.OK);
            this.simpleButton_0.Text = Localizer.Active.GetLocalizedString(StringId.Cancel);
            this.panelControl_0 = new PanelControl();
            this.panelControl_1 = new PanelControl();
            this.panelControl_0.Padding = new Padding(4);
            this.panelControl_0.BorderStyle = BorderStyles.NoBorder;
            this.panelControl_0.Parent = control;
            this.treeView_0.Parent = control;
            this.simpleButton_0.Parent = this.panelControl_0;
            this.simpleButton_1.Parent = this.panelControl_0;
            this.panelControl_1.BorderStyle = BorderStyles.NoBorder;
            this.panelControl_1.Width = 4;
            this.panelControl_1.Parent = this.panelControl_0;
            this.panelControl_0.Dock = DockStyle.Bottom;
            this.treeView_0.Dock = DockStyle.Fill;
            this.simpleButton_0.Dock = DockStyle.Right;
            this.simpleButton_1.Dock = DockStyle.Right;
            this.panelControl_1.Dock = DockStyle.Right;
            this.treeView_0.BringToFront();
            this.treeView_0.TabIndex = 0;
            this.simpleButton_1.TabIndex = 0;
            this.panelControl_1.TabIndex = 1;
            this.simpleButton_0.TabIndex = 2;
            this.panelControl_1.SendToBack();
            this.simpleButton_0.SendToBack();
            this.method_5(this.simpleButton_1, this.simpleButton_0, this.panelControl_0, 4);
            this.simpleButton_1.Click += new EventHandler(this.simpleButton_1_Click);
            this.simpleButton_0.Click += new EventHandler(this.simpleButton_0_Click);
            this.SyncLookAndFeel();
            return control;
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1)
            {
                if (this.simpleButton_1 != null)
                {
                    this.simpleButton_1.Click -= new EventHandler(this.simpleButton_1_Click);
                }
                if (this.simpleButton_0 != null)
                {
                    this.simpleButton_0.Click -= new EventHandler(this.simpleButton_0_Click);
                }
            }
            base.Dispose(bool_1);
        }

        protected override void DoClosePopup(PopupCloseMode popupCloseMode_0)
        {
            if (this.IsAcceptCloseMode(popupCloseMode_0))
            {
                if (this.treeView_0 != null)
                {
                    this.Properties.SynchronizingSelectNode(this.treeView_0.SelectedNode);
                }
            }
            else
            {
                this.Properties.SetCollectionChanged();
            }
            base.DoClosePopup(popupCloseMode_0);
        }

        protected override void DoShowPopup()
        {
            if (this.Properties.PopupControl == null)
            {
                this.Properties.PopupControl = this.CreatePopupCheckListControl();
                if (this.Properties.PopupFormMinSize == Size.Empty)
                {
                    this.Properties.PopupControl.Width = Math.Max(this.Properties.PopupControl.Width, base.Width);
                    this.Properties.PopupControl.Height = this.DefaultPopupHeight;
                }
            }
            this.method_2();
            if (this.Properties.SynchronizeEditValueWithCheckedItems)
            {
                this.method_1();
            }
            this.method_0();
            base.DoShowPopup();
        }

        internal static bool DoSynchronizeEditValueWithCheckedItems(TreeNode treeNode_0, RepositoryItemCheckedComboBoxEdit repositoryItemCheckedComboBoxEdit_0, object object_0)
        {
            return false;
        }

        public override bool DoValidate(PopupCloseMode popupCloseMode_0)
        {
            this.DoClosePopup(popupCloseMode_0);
            return base.DoValidate(popupCloseMode_0);
        }

        private static bool HasValue(string[] string_0, object object_0)
        {
            string str = string.Format("{0}", object_0);
            foreach (string str2 in string_0)
            {
                if (str2.Equals(str) || str2.TrimStart(new char[0]).Equals(str))
                {
                    return true;
                }
            }
            return false;
        }

        private void method_0()
        {
            int num = 80;
            num = 80 + this.simpleButton_1.Parent.Height;
            if (this.Properties.PopupControl.Height > num)
            {
                this.Properties.PopupControl.Height = num;
            }
        }

        private void method_1()
        {
        }

        private void method_2()
        {
            if (this.Properties.collectionChanged)
            {
                this.treeView_0.Nodes.Clear();
                if (this.Properties.DataAccessLayerBaseClass != null)
                {
                    string str = string.Format("select * from {0} where {1}={2}", this.Properties.TableName, this.Properties.IDFieldName, this.Properties.ParentIDFieldName);
                    this.Properties.DataAccessLayerBaseClass.Open();
                    IDataReader reader = this.Properties.DataAccessLayerBaseClass.ExecuteDataReader(str);
                    while (reader.Read())
                    {
                        NameValueCollection values = new NameValueCollection();
                        string text = reader[this.Properties.NameFieldName].ToString();
                        TreeNode node = new TreeNode(text) {
                            Tag = values
                        };
                        values.Add(this.Properties.NameFieldName, text);
                        values.Add(this.Properties.IDFieldName, reader[this.Properties.IDFieldName].ToString());
                        values.Add(this.Properties.CodeFieldName, reader[this.Properties.CodeFieldName].ToString());
                        this.treeView_0.Nodes.Add(node);
                        node.Nodes.Add("tmp");
                    }
                }
                this.method_3();
            }
        }

        private void method_3()
        {
            this.method_4(-1);
        }

        private void method_4(int int_0)
        {
            if (this.checkedListBoxItem_0 != null)
            {
                this.bool_0 = true;
                try
                {
                }
                finally
                {
                    this.bool_0 = false;
                }
            }
        }

        private void method_5(SimpleButton simpleButton_2, SimpleButton simpleButton_3, PanelControl panelControl_2, int int_0)
        {
            Size size = simpleButton_2.CalcBestSize();
            Size size2 = simpleButton_3.CalcBestSize();
            size.Height = Math.Max(size.Height, size2.Height);
            size.Width = Math.Max(size.Width, size2.Width) + (int_0 * 4);
            panelControl_2.Height = size.Height + (int_0 * 2);
            simpleButton_2.Size = size;
            simpleButton_3.Size = size;
        }

        protected override void OnHandleCreated(EventArgs eventArgs_0)
        {
            base.OnHandleCreated(eventArgs_0);
            if (!(this.IsDesignMode || (this.EditValue != null)))
            {
                this.Properties.SynchronizeEditValue();
            }
        }

        public virtual void SetEditValue(object object_0)
        {
            this.EditValue = object_0;
            this.Properties.SynchronizeItemsWithEditValue();
        }

        private void simpleButton_0_Click(object sender, EventArgs e)
        {
            this.ClosePopup(PopupCloseMode.Cancel);
        }

        private void simpleButton_1_Click(object sender, EventArgs e)
        {
            this.ClosePopup(PopupCloseMode.ButtonClick);
        }

        internal void SyncLookAndFeel()
        {
            if (this.treeView_0 != null)
            {
                this.simpleButton_0.LookAndFeel.Assign(this.Properties.LookAndFeel);
                this.simpleButton_1.LookAndFeel.Assign(this.Properties.LookAndFeel);
                this.panelControl_0.LookAndFeel.Assign(this.Properties.LookAndFeel);
                this.panelControl_1.LookAndFeel.Assign(this.Properties.LookAndFeel);
            }
        }

        private void treeView_0_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if ((e.Node.Nodes.Count == 1) && (e.Node.Nodes[0].Tag == null))
            {
                e.Node.Nodes.Clear();
                NameValueCollection tag = e.Node.Tag as NameValueCollection;
                string str = tag[this.Properties.IDFieldName];
                string str2 = string.Format("select * from {0} where {1}<>{2} and {2}='{3}'", new object[] { this.Properties.TableName, this.Properties.IDFieldName, this.Properties.ParentIDFieldName, str });
                this.Properties.DataAccessLayerBaseClass.Open();
                IDataReader reader = this.Properties.DataAccessLayerBaseClass.ExecuteDataReader(str2);
                while (reader.Read())
                {
                    NameValueCollection values2 = new NameValueCollection();
                    string text = reader[this.Properties.NameFieldName].ToString();
                    TreeNode node = new TreeNode(text) {
                        Tag = values2
                    };
                    values2.Add(this.Properties.NameFieldName, text);
                    values2.Add(this.Properties.IDFieldName, reader[this.Properties.IDFieldName].ToString());
                    values2.Add(this.Properties.CodeFieldName, reader[this.Properties.CodeFieldName].ToString());
                    e.Node.Nodes.Add(node);
                    node.Nodes.Add("tmp");
                }
            }
        }

        private static bool UpdateCheckItem(CheckedListBoxItem checkedListBoxItem_1, bool bool_1)
        {
            bool flag = false;
            if (bool_1 && (checkedListBoxItem_1.CheckState != CheckState.Checked))
            {
                checkedListBoxItem_1.CheckState = CheckState.Checked;
                flag = true;
            }
            if (!(bool_1 || (checkedListBoxItem_1.CheckState == CheckState.Unchecked)))
            {
                checkedListBoxItem_1.CheckState = CheckState.Unchecked;
                flag = true;
            }
            return flag;
        }

        protected virtual int DefaultPopupHeight
        {
            get
            {
                return 0x97;
            }
        }

        protected override  EditorClassInfo EditorClassInfo
        {
            get
            {
                if (this.editorClassInfo_0 == null)
                {
                    this.editorClassInfo_0 = new  EditorClassInfo("TreeViewComboBoxEdit", typeof(TreeViewComboBoxEdit), typeof(RepositoryItemTreeViewComboBoxEdit), typeof(PopupContainerEditViewInfo), new ButtonEditPainter(), true, null, typeof(PopupEditAccessible));
                }
                return this.editorClassInfo_0;
            }
        }

        public override string EditorTypeName
        {
            get
            {
                return "TreeViewComboBoxEdit";
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public RepositoryItemTreeViewComboBoxEdit Properties
        {
            get
            {
                return (base.Properties as RepositoryItemTreeViewComboBoxEdit);
            }
        }
    }
}

