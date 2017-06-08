using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Yutai.ArcGIS.Common.ControlExtend
{
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ContextMenuStrip | ToolStripItemDesignerAvailability.MenuStrip | ToolStripItemDesignerAvailability.ToolStrip), DefaultProperty("Items")]
    public class ToolStripComboBoxTreeView : ToolStripControlHost
    {
        public event EventHandler DropDown;

        public ToolStripComboBoxTreeView() : base(CreateControlInstance())
        {
            ToolStripComboBoxTreeViewControl control = base.Control as ToolStripComboBoxTreeViewControl;
            control.Owner = this;
        }

        private static Control CreateControlInstance()
        {
            return new ToolStripComboBoxTreeViewControl();
        }

        public void Select(int int_0, int int_1)
        {
            this.ComboBox.Select(int_0, int_1);
        }

        public void SelectAll()
        {
            this.ComboBox.SelectAll();
        }

        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), Localizable(true), EditorBrowsable(EditorBrowsableState.Always), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Browsable(true)]
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

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Windows.Forms.ComboBox ComboBox
        {
            get
            {
                return (base.Control as System.Windows.Forms.ComboBox);
            }
        }

        [DefaultValue(0x6a), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
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

        [DefaultValue(1), RefreshProperties(RefreshProperties.Repaint)]
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

        [Description("下拉框最大长度"), DefaultValue(0), Localizable(true)]
        public int MaxLength
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
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

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [Browsable(true)]
        public System.Windows.Forms.TreeView TreeView
        {
            get
            {
                ToolStripComboBoxTreeViewControl control = base.Control as ToolStripComboBoxTreeViewControl;
                return control.TreeView;
            }
        }

        internal class ToolStripComboBoxTreeViewControl : ComboBox
        {
            private ToolStripComboBoxTreeView toolStripComboBoxTreeView_0;
            private ToolStripControlHost toolStripControlHost_0;
            private ToolStripDropDown toolStripDropDown_0;
            private const int WM_LBUTTONDBLCLK = 0x203;
            private const int WM_LBUTTONDOWN = 0x201;

            public ToolStripComboBoxTreeViewControl()
            {
                System.Windows.Forms.TreeView c = new System.Windows.Forms.TreeView();
                c.AfterSelect += new TreeViewEventHandler(this.treeView_AfterSelect);
                c.BorderStyle = BorderStyle.None;
                this.toolStripControlHost_0 = new ToolStripControlHost(c);
                this.toolStripDropDown_0 = new ToolStripDropDown();
                this.toolStripDropDown_0.Width = base.Width;
                this.toolStripDropDown_0.Items.Add(this.toolStripControlHost_0);
            }

            protected override void Dispose(bool bool_0)
            {
                if (bool_0 && (this.toolStripDropDown_0 != null))
                {
                    this.toolStripDropDown_0.Dispose();
                    this.toolStripDropDown_0 = null;
                }
                base.Dispose(bool_0);
            }

            private void method_0()
            {
                if (this.toolStripDropDown_0 != null)
                {
                    this.toolStripControlHost_0.Size = new Size(base.DropDownWidth - 2, base.DropDownHeight);
                    this.toolStripDropDown_0.Show(this, 0, base.Height);
                }
            }

            public void treeView_AfterSelect(object sender, TreeViewEventArgs e)
            {
                this.Text = this.TreeView.SelectedNode.Text;
                if (this.Owner.DropDown != null)
                {
                    this.Owner.DropDown(this, e);
                }
                this.toolStripDropDown_0.Close();
            }

            protected override void WndProc(ref Message message_0)
            {
                if ((message_0.Msg == 0x203) || (message_0.Msg == 0x201))
                {
                    this.method_0();
                }
                else
                {
                    base.WndProc(ref message_0);
                }
            }

            public ToolStripComboBoxTreeView Owner
            {
                get
                {
                    return this.toolStripComboBoxTreeView_0;
                }
                set
                {
                    this.toolStripComboBoxTreeView_0 = value;
                }
            }

            public System.Windows.Forms.TreeView TreeView
            {
                get
                {
                    return (this.toolStripControlHost_0.Control as System.Windows.Forms.TreeView);
                }
            }
        }
    }
}

