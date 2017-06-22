using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.SymbolUI;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    partial class TemplateControl
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
            this.TemplatelistView = new KDEditListView();
            this.header1 = new KDColumnHeader();
            this.header2 = new KDColumnHeader();
            this.label1 = new Label();
            this.numericUpDownInterval = new SpinEdit();
            this.label2 = new Label();
            this.btnAdd = new SimpleButton();
            this.btnDelete = new SimpleButton();
            this.btnClear = new SimpleButton();
            this.numericUpDownInterval.Properties.BeginInit();
            base.SuspendLayout();
            this.TemplatelistView.Columns.AddRange(new ColumnHeader[] { this.header1, this.header2 });
            this.TemplatelistView.ComboBoxBgColor = Color.LightBlue;
            this.TemplatelistView.ComboBoxFont = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
            this.TemplatelistView.EditBgColor = Color.LightBlue;
            this.TemplatelistView.EditFont = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
            this.TemplatelistView.FullRowSelect = true;
            this.TemplatelistView.GridLines = true;
            this.TemplatelistView.Location = new Point(24, 32);
            this.TemplatelistView.Name = "TemplatelistView";
            this.TemplatelistView.Size = new Size(240, 128);
            this.TemplatelistView.TabIndex = 3;
            this.TemplatelistView.View = View.Details;
            this.TemplatelistView.ValueChanged += new ValueChangedHandler(this.TemplatelistView_ValueChanged);
            this.header1.ColumnStyle = KDListViewColumnStyle.ReadOnly;
            this.header1.Text = "步长类型";
            this.header1.Width = 90;
            this.header2.ColumnStyle = KDListViewColumnStyle.EditBox;
            this.header2.Text = "步长值";
            this.header2.Width = 138;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(24, 184);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "间隔";
            int[] bits = new int[4];
            this.numericUpDownInterval.EditValue = new decimal(bits);
            this.numericUpDownInterval.Location = new Point(64, 176);
            this.numericUpDownInterval.Name = "numericUpDownInterval";
            this.numericUpDownInterval.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            bits = new int[4];
            bits[0] = 500;
            this.numericUpDownInterval.Properties.MaxValue = new decimal(bits);
            this.numericUpDownInterval.Properties.UseCtrlIncrement = false;
            this.numericUpDownInterval.Size = new Size(96, 23);
            this.numericUpDownInterval.TabIndex = 82;
            this.numericUpDownInterval.TextChanged += new EventHandler(this.numericUpDownInterval_ValueChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(24, 208);
            this.label2.Name = "label2";
            this.label2.Size = new Size(338, 17);
            this.label2.TabIndex = 83;
            this.label2.Text = "步长值的单位是单位组合框所选择项。间隔是步长值的倍数。";
            this.btnAdd.Location = new Point(272, 40);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(56, 24);
            this.btnAdd.TabIndex = 84;
            this.btnAdd.Text = "添加";
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.btnDelete.Location = new Point(272, 76);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(56, 24);
            this.btnDelete.TabIndex = 85;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.btnClear.Location = new Point(272, 112);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new Size(56, 24);
            this.btnClear.TabIndex = 86;
            this.btnClear.Text = "清除";
            this.btnClear.Click += new EventHandler(this.btnClear_Click);
            base.Controls.Add(this.btnClear);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.btnAdd);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.numericUpDownInterval);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.TemplatelistView);
            base.Name = "TemplateControl";
            base.Size = new Size(368, 256);
            base.Load += new EventHandler(this.TemplateControl_Load);
            this.numericUpDownInterval.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private SimpleButton btnAdd;
        private SimpleButton btnClear;
        private SimpleButton btnDelete;
        private KDColumnHeader header1;
        private KDColumnHeader header2;
        private Label label1;
        private Label label2;
        private SpinEdit numericUpDownInterval;
        private KDEditListView TemplatelistView;
    }
}