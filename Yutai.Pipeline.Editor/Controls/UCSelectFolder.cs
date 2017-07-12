using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.Pipeline.Editor.Controls
{
    public partial class UCSelectFolder : UserControl
    {
        public event SelectComplateHandler SelectComplateEvent;

        private string _selectedPath;

        public UCSelectFolder()
        {
            InitializeComponent();
        }

        public string SelectedPath
        {
            get { return _selectedPath; }
            private set
            {
                _selectedPath = value;
                OnSelectComplateEvent();
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowNewFolderButton = false;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txtFolderPath.Text = dialog.SelectedPath;
            }
        }

        private void txtFolderPath_TextChanged(object sender, EventArgs e)
        {
            SelectedPath = txtFolderPath.Text.Trim();
        }

        protected virtual void OnSelectComplateEvent()
        {
            var handler = SelectComplateEvent;
            if (handler != null) handler();
        }

        [Browsable(true)]
        [Description("是否显示文本"), Category("扩展"), DefaultValue(false)]
        public bool VisibleLabel
        {
            get { return !this.splitContainer2.Panel1Collapsed; }
            set { this.splitContainer2.Panel1Collapsed = !value; }
        }

        [Browsable(true)]
        [Description("与控件关联的文本"), Category("扩展"), DefaultValue(null)]
        public string Label
        {
            get { return this.label1.Text; }
            set
            {
                this.label1.Text = value;
                this.splitContainer2.SplitterDistance = this.label1.Width;
            }
        }

        [Browsable(true)]
        [Description("与控件关联的文本"), Category("扩展"), DefaultValue(0)]
        public int LabelWidth
        {
            get { return this.splitContainer2.SplitterDistance; }
            set { this.splitContainer2.SplitterDistance = value; }
        }

        [Browsable(true)]
        [Description("文本的对齐方式"), Category("扩展"), DefaultValue(ContentAlignment.MiddleLeft)]
        public ContentAlignment LabelAlign
        {
            get { return this.label1.TextAlign; }
            set { this.label1.TextAlign = value; }
        }
    }
}
