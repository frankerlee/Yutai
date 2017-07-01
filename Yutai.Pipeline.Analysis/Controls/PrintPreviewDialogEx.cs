using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Yutai.Pipeline.Analysis.Controls
{
    public partial class PrintPreviewDialogEx : PrintPreviewDialog
    {
        public PrintPreviewDialogEx()
        {
            InitializeComponent();
            foreach (Control ctrl in base.Controls)
            {
                if (ctrl.GetType() == typeof(ToolStrip))
                {
                    ToolStrip tools = ctrl as ToolStrip;
                    tools.Items.Insert(0, CreatePageSetupButton());
                    tools.Items.Insert(1, CreatePrintsetButton());
                }
            }
        }

        public PrintPreviewDialogEx(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            foreach (Control ctrl in base.Controls)
            {
                if (ctrl.GetType() == typeof(ToolStrip))
                {
                    ToolStrip tools = ctrl as ToolStrip;
                    tools.Items.Insert(0, CreatePageSetupButton());
                    tools.Items.Insert(1, CreatePrintsetButton());
                }
            }
        }

        ToolStripButton CreatePrintsetButton()
        {
            ToolStripButton Stripbutton = new ToolStripButton();
            Stripbutton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            Stripbutton.Name = "printsetStripButton";
            Stripbutton.Size = new System.Drawing.Size(23, 22);
            Stripbutton.Text = @"打印设置";
            Stripbutton.Click += new System.EventHandler(this.PrintsetButton_Click);
            return Stripbutton;
        }

        ToolStripButton CreatePageSetupButton()
        {
            ToolStripButton Stripbutton = new ToolStripButton();
            Stripbutton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            Stripbutton.Name = "PageSetupStripButton";
            Stripbutton.Size = new System.Drawing.Size(23, 22);
            Stripbutton.Text = @"页面设置";
            Stripbutton.Click += new System.EventHandler(this.PageSetupButton_Click);
            return Stripbutton;
        }

        private void PageSetupButton_Click(object sender, EventArgs e)
        {
            using (PageSetupDialog dialog = new PageSetupDialog())
            {
                dialog.Document = base.Document;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    base.Document.DefaultPageSettings = dialog.PageSettings;
                }
            }
        }

        private void PrintsetButton_Click(object sender, EventArgs e)
        {
            using (PrintDialog dialog = new PrintDialog())
            {
                dialog.Document = base.Document;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    base.Document.PrinterSettings = dialog.PrinterSettings;
                }
            }
        }
    }
}