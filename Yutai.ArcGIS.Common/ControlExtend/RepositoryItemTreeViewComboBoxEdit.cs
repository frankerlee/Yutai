using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using DevExpress.Accessibility;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using Yutai.ArcGIS.Common.Data;

namespace Yutai.ArcGIS.Common.ControlExtend
{
    public class RepositoryItemTreeViewComboBoxEdit : RepositoryItemPopupContainerEdit
    {
        private TreeNode treeNode_0;

        internal bool collectionChanged = true;

        private bool bool_0 = true;

        private char char_0 = ',';

        private bool bool_1 = true;

        private Type type_0 = null;

        private bool bool_2 = false;

        private HighlightStyle highlightStyle_0;

        private string string_0;

        private EditorClassInfo editorClassInfo_0 = null;

        public string CodeFieldName
        {
            get;
            set;
        }

        public string Connection
        {
            get;
            set;
        }

        private int CustomItemsShift
        {
            get
            {
                return (this.ShowAllItemVisible ? 1 : 0);
            }
        }

        public DataAccessLayerBaseClass DataAccessLayerBaseClass
        {
            get;
            set;
        }

        protected override EditorClassInfo EditorClassInfo
        {
            get
            {
                if (this.editorClassInfo_0 == null)
                {
                    this.editorClassInfo_0 = new EditorClassInfo("TreeViewComboBoxEdit", typeof(TreeViewComboBoxEdit), typeof(RepositoryItemTreeViewComboBoxEdit), typeof(PopupContainerEditViewInfo), new ButtonEditPainter(), true, null, typeof(PopupEditAccessible));
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

        [Category("Appearance")]
        [DefaultValue(HighlightStyle.Default)]
        [Description("")]
        public virtual HighlightStyle HighlightedItemStyle
        {
            get
            {
                return this.highlightStyle_0;
            }
            set
            {
                if (this.HighlightedItemStyle != value)
                {
                    this.highlightStyle_0 = value;
                    this.OnPropertiesChanged();
                    this.SetCollectionChanged();
                }
            }
        }

        public string IDFieldName
        {
            get;
            set;
        }

        internal bool IsFlags
        {
            get
            {
                return this.type_0 != null;
            }
        }

        [Category("Data")]
        [Description("Provides access to the item collection, when the control is not bound to a data source.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor("System.Windows.Forms.Design.CollectionEditor, System.Design", typeof(UITypeEditor))]
        [Localizable(true)]
        public TreeNode Items
        {
            get
            {
                return this.treeNode_0;
            }
        }

        public string NameFieldName
        {
            get;
            set;
        }

        public string ParentIDFieldName
        {
            get;
            set;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override PopupContainerControl PopupControl
        {
            get
            {
                return base.PopupControl;
            }
            set
            {
                base.PopupControl = value;
            }
        }

        [Category("Format")]
        [DefaultValue(',')]
        [Description("Gets or sets the character separating checked items in the edit value, and the resultant text displayed in the edit box.")]
        public virtual char SeparatorChar
        {
            get
            {
                return this.char_0;
            }
            set
            {
                if (this.SeparatorChar != value)
                {
                    this.char_0 = value;
                    this.OnPropertiesChanged();
                    this.SynchronizeEditValue();
                }
            }
        }

        [Category("Appearance")]
        [Description("Gets or sets the 'Show All' check item's caption.")]
        [Localizable(true)]
        public virtual string ShowAllItemCaption
        {
            get
            {
                return this.string_0;
            }
            set
            {
                if (this.ShowAllItemCaption != value)
                {
                    this.string_0 = value;
                    this.OnPropertiesChanged();
                    this.SetCollectionChanged();
                }
            }
        }

        [Category("Appearance")]
        [DefaultValue(true)]
        [Description("Gets or sets whether the 'Show All' check item is visible.")]
        public virtual bool ShowAllItemVisible
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                if (this.ShowAllItemVisible != value)
                {
                    this.bool_0 = value;
                    this.OnPropertiesChanged();
                    this.SetCollectionChanged();
                }
            }
        }

        [Category("Behavior")]
        [DefaultValue(true)]
        [Description("Gets or sets whether changing a CheckedComboBoxEdit control's EditValue automatically updates the check states of items in the dropdown.")]
        public virtual bool SynchronizeEditValueWithCheckedItems
        {
            get
            {
                return this.bool_1;
            }
            set
            {
                if (this.SynchronizeEditValueWithCheckedItems != value)
                {
                    this.bool_1 = value;
                    this.OnPropertiesChanged();
                    this.SynchronizeEditValue();
                }
            }
        }

        public string TableName
        {
            get;
            set;
        }

        public RepositoryItemTreeViewComboBoxEdit()
        {
            this.string_0 = Localizer.Active.GetLocalizedString(StringId.FilterShowAll);
        }

        public override void Assign(RepositoryItem repositoryItem_0)
        {
            RepositoryItemTreeViewComboBoxEdit repositoryItem0 = repositoryItem_0 as RepositoryItemTreeViewComboBoxEdit;
            this.BeginUpdate();
            try
            {
                base.Assign(repositoryItem_0);
                if (repositoryItem0 != null)
                {
                    this.DataAccessLayerBaseClass = repositoryItem0.DataAccessLayerBaseClass;
                    this.IDFieldName = repositoryItem0.IDFieldName;
                    this.TableName = repositoryItem0.TableName;
                    this.ParentIDFieldName = repositoryItem0.ParentIDFieldName;
                    this.NameFieldName = repositoryItem0.NameFieldName;
                    this.CodeFieldName = repositoryItem0.CodeFieldName;
                    this.bool_0 = repositoryItem0.ShowAllItemVisible;
                    this.highlightStyle_0 = repositoryItem0.highlightStyle_0;
                    this.char_0 = repositoryItem0.SeparatorChar;
                    this.bool_1 = repositoryItem0.SynchronizeEditValueWithCheckedItems;
                    this.collectionChanged = true;
                    this.type_0 = repositoryItem0.type_0;
                    this.string_0 = repositoryItem0.ShowAllItemCaption;
                }
            }
            finally
            {
                this.EndUpdate();
            }
        }

        internal void ClearCollectionChanged()
        {
            this.collectionChanged = false;
        }

        protected override void Dispose(bool bool_3)
        {
            
            base.Dispose(bool_3);
        }

        private void method_0(object sender, ListChangedEventArgs e)
        {
            if (!this.bool_2)
            {
                this.SetCollectionChanged();
            }
        }

        private void method_1()
        {
            
        }

        protected override void OnLookAndFeelChanged(object sender, EventArgs e)
        {
            base.OnLookAndFeelChanged(sender, e);
            TreeViewComboBoxEdit ownerEdit = base.OwnerEdit as TreeViewComboBoxEdit;
            if (ownerEdit != null)
            {
                ownerEdit.SyncLookAndFeel();
            }
        }

        protected override void PreQueryDisplayText(QueryDisplayTextEventArgs queryDisplayTextEventArgs_0)
        {
            base.PreQueryDisplayText(queryDisplayTextEventArgs_0);
            if (this.IsFlags)
            {
                try
                {
                    if ((queryDisplayTextEventArgs_0.EditValue == null ? false : queryDisplayTextEventArgs_0.EditValue != DBNull.Value) && queryDisplayTextEventArgs_0.DisplayText == Convert.ToInt32(queryDisplayTextEventArgs_0.EditValue).ToString())
                    {
                        queryDisplayTextEventArgs_0.DisplayText = Enum.ToObject(this.type_0, queryDisplayTextEventArgs_0.EditValue).ToString();
                    }
                    if ("0".Equals(queryDisplayTextEventArgs_0.DisplayText))
                    {
                        queryDisplayTextEventArgs_0.DisplayText = string.Empty;
                    }
                }
                catch
                {
                    queryDisplayTextEventArgs_0.DisplayText = string.Empty;
                }
            }
        }

        protected override void PreQueryResultValue(QueryResultValueEventArgs queryResultValueEventArgs_0)
        {
            base.PreQueryResultValue(queryResultValueEventArgs_0);
            if (this.SynchronizeEditValueWithCheckedItems)
            {
                queryResultValueEventArgs_0.Value = this.treeNode_0.Text;
            }
        }

        protected virtual void ResetShowAllItemCaption()
        {
            this.ShowAllItemCaption = Localizer.Active.GetLocalizedString(StringId.FilterShowAll);
        }

        internal void SetCollectionChanged()
        {
            this.collectionChanged = true;
        }

        public void SetFlags(Type type_1)
        {
            this.type_0 = type_1;
            this.method_1();
        }

        protected virtual bool ShouldSerializeShowAllItemCaption()
        {
            return this.ShowAllItemCaption != Localizer.Active.GetLocalizedString(StringId.FilterShowAll);
        }

        internal void SynchronizeEditValue()
        {
           
        }

        internal void SynchronizeItemsWithEditValue()
        {
        }

        internal void SynchronizingSelectNode(TreeNode treeNode_1)
        {
            this.bool_2 = true;
            this.treeNode_0 = treeNode_1;
            this.bool_2 = false;
        }
    }
}

