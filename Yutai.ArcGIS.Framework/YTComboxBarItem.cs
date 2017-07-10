using System;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Repository;

namespace Yutai.ArcGIS.Framework
{
    public class YTComboxBarItem : ComboxBarItem
    {
        private BarEditItem barEditItem_0 = null;
        private RepositoryItemComboBox repositoryItemComboBox_0 = null;

        public YTComboxBarItem(object object_0)
        {
            this.barEditItem_0 = object_0 as BarEditItem;
            if (this.barEditItem_0 == null)
            {
                throw new Exception("该工具栏按钮不是组合框按钮");
            }
            this.repositoryItemComboBox_0 = this.barEditItem_0.Edit as RepositoryItemComboBox;
            if (this.repositoryItemComboBox_0 == null)
            {
                throw new Exception("该工具栏按钮不是组合框按钮");
            }
            this.barEditItem_0.EditValueChanged += new EventHandler(this.barEditItem_0_EditValueChanged);
        }

        public override void Add(object object_0)
        {
            if (this.repositoryItemComboBox_0 != null)
            {
                this.repositoryItemComboBox_0.Items.Add(object_0);
            }
        }

        private void barEditItem_0_EditValueChanged(object sender, EventArgs e)
        {
            base.EditValueChanged(sender, e);
        }

        public override void Clear()
        {
            if (this.repositoryItemComboBox_0 != null)
            {
                this.repositoryItemComboBox_0.Items.Clear();
            }
        }

        public override void RemoveAt(int int_0)
        {
            if (this.barEditItem_0 != null)
            {
                this.repositoryItemComboBox_0.Items.RemoveAt(int_0);
            }
        }

        public override int Count
        {
            get { return this.repositoryItemComboBox_0.Items.Count; }
        }

        public override object EditValue
        {
            get { return this.barEditItem_0.EditValue; }
            set { this.barEditItem_0.EditValue = value; }
        }

        public override bool Enabled
        {
            get
            {
                if (this.barEditItem_0 == null)
                {
                    return false;
                }
                return this.barEditItem_0.Enabled;
            }
            set
            {
                if (this.barEditItem_0 != null)
                {
                    this.barEditItem_0.Enabled = value;
                }
            }
        }

        public override object this[int int_0]
        {
            get { return this.repositoryItemComboBox_0.Items[int_0]; }
        }

        public override string Text
        {
            get { return this.barEditItem_0.Caption; }
            set { this.barEditItem_0.Caption = value; }
        }
    }
}