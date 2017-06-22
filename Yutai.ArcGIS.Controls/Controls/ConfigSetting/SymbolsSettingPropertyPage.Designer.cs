using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace Yutai.ArcGIS.Controls.Controls.ConfigSetting
{
    partial class SymbolsSettingPropertyPage
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

       
        private void InitializeComponent()
        {
            this.btnAddSymbol = new SimpleButton();
            this.btnDelete = new SimpleButton();
            this.label1 = new Label();
            this.listBox1 = new ListBox();
            this.radioGroup1 = new RadioGroup();
            this.listBox2 = new ListBox();
            this.radioGroup1.Properties.BeginInit();
            base.SuspendLayout();
            this.btnAddSymbol.Location = new Point(334, 32);
            this.btnAddSymbol.Name = "btnAddSymbol";
            this.btnAddSymbol.Size = new Size(48, 24);
            this.btnAddSymbol.TabIndex = 2;
            this.btnAddSymbol.Text = "添加...";
            this.btnAddSymbol.Click += new EventHandler(this.btnAddSymbol_Click);
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new Point(334, 64);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(48, 24);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(89, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "符号库文件列表";
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new Point(8, 32);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new Size(312, 160);
            this.listBox1.TabIndex = 6;
            this.listBox1.SelectedIndexChanged += new EventHandler(this.listBox1_SelectedIndexChanged);
            this.radioGroup1.Location = new Point(8, 203);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = Color.Transparent;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup1.Properties.Columns = 2;
            this.radioGroup1.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "style样式库文件"), new RadioGroupItem(null, "serverstyle样式库文件") });
            this.radioGroup1.Size = new Size(312, 24);
            this.radioGroup1.TabIndex = 7;
            this.radioGroup1.SelectedIndexChanged += new EventHandler(this.radioGroup1_SelectedIndexChanged);
            this.listBox2.HorizontalScrollbar = true;
            this.listBox2.ItemHeight = 12;
            this.listBox2.Location = new Point(8, 32);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new Size(312, 160);
            this.listBox2.TabIndex = 8;
            this.listBox2.Visible = false;
            this.listBox2.SelectedIndexChanged += new EventHandler(this.listBox2_SelectedIndexChanged);
            base.Controls.Add(this.listBox2);
            base.Controls.Add(this.radioGroup1);
            base.Controls.Add(this.listBox1);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.btnAddSymbol);
            base.Name = "SymbolsSettingPropertyPage";
            base.Size = new Size(400, 265);
            base.Load += new EventHandler(this.SymbolsSettingPropertyPage_Load);
            this.radioGroup1.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private Container components = null;
        private SimpleButton btnAddSymbol;
        private SimpleButton btnDelete;
        private Label label1;
        private ListBox listBox1;
        private ListBox listBox2;
        private RadioGroup radioGroup1;
    }
}