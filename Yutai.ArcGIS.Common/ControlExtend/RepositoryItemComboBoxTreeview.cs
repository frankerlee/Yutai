using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;

namespace Yutai.ArcGIS.Common.ControlExtend
{
    public class RepositoryItemComboBoxTreeview : RepositoryItemPopupBaseAutoSearchEdit
    {
        [CompilerGenerated]
        private ComboBoxTreeView comboBoxTreeView_0;

        public event EventHandler DropDown;

        public RepositoryItemComboBoxTreeview()
        {
            this.ComboBoxTreeView = new ComboBoxTreeView();
        }

        private static Control CreateControlInstance()
        {
            return new ComboBoxTreeView();
        }

        public override BaseEdit CreateEditor()
        {
            return base.CreateEditor();
        }

        public void Select(int int_0, int int_1)
        {
            this.ComboBox.Select(int_0, int_1);
        }

        public void SelectAll()
        {
            this.ComboBox.SelectAll();
        }

        public AutoCompleteStringCollection AutoCompleteCustomSource
        {
            get
            {
                return this.ComboBox.AutoCompleteCustomSource;
            }
            set
            {
                this.ComboBox.AutoCompleteCustomSource = value;
            }
        }

        public System.Windows.Forms.ComboBox ComboBox
        {
            get
            {
                return this.ComboBoxTreeView;
            }
        }

        protected ComboBoxTreeView ComboBoxTreeView
        {
            [CompilerGenerated]
            get
            {
                return this.comboBoxTreeView_0;
            }
            [CompilerGenerated]
            set
            {
                this.comboBoxTreeView_0 = value;
            }
        }

        public int DropDownHeight
        {
            get
            {
                return this.ComboBox.DropDownHeight;
            }
            set
            {
                this.ComboBox.DropDownHeight = value;
            }
        }

        public ComboBoxStyle DropDownStyle
        {
            get
            {
                return this.ComboBox.DropDownStyle;
            }
            set
            {
                this.ComboBox.DropDownStyle = value;
            }
        }

        public int DropDownWidth
        {
            get
            {
                return this.ComboBox.DropDownWidth;
            }
            set
            {
                this.ComboBox.DropDownWidth = value;
            }
        }

        public override int MaxLength
        {
            get
            {
                return this.ComboBox.MaxLength;
            }
            set
            {
                this.ComboBox.MaxLength = value;
            }
        }

        public int SelectionLength
        {
            get
            {
                return this.ComboBox.SelectionLength;
            }
            set
            {
                this.ComboBox.SelectionLength = value;
            }
        }

        public int SelectionStart
        {
            get
            {
                return this.ComboBox.SelectionStart;
            }
            set
            {
                this.ComboBox.SelectionStart = value;
            }
        }

        public override TextEditStyles TextEditStyle
        {
            get
            {
                return base.TextEditStyle;
            }
            set
            {
                base.TextEditStyle = value;
            }
        }

        public System.Windows.Forms.TreeView TreeView
        {
            get
            {
                return this.ComboBoxTreeView.TreeView;
            }
        }
    }
}

