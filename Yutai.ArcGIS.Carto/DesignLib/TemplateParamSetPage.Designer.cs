using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.ControlExtend;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    partial class TemplateParamSetPage
    {
        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

       
 private void InitializeComponent()
        {
            this.listView1 = new EditListView();
            this.lvcolumnHeader_0 = new LVColumnHeader();
            this.lvcolumnHeader_1 = new LVColumnHeader();
            this.lbl1 = new Label();
            base.SuspendLayout();
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.lvcolumnHeader_0, this.lvcolumnHeader_1 });
            this.listView1.ComboBoxBgColor = Color.LightBlue;
            this.listView1.ComboBoxFont = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
            this.listView1.EditBgColor = Color.LightBlue;
            this.listView1.EditFont = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new Point(16, 37);
            this.listView1.LockRowCount = 0;
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(272, 96);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.lvcolumnHeader_0.ColumnStyle = ListViewColumnStyle.ReadOnly;
            this.lvcolumnHeader_0.Text = "参数";
            this.lvcolumnHeader_0.Width = 144;
            this.lvcolumnHeader_1.ColumnStyle = ListViewColumnStyle.EditBox;
            this.lvcolumnHeader_1.Text = "值";
            this.lvcolumnHeader_1.Width = 122;
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new Point(14, 9);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new Size(53, 12);
            this.lbl1.TabIndex = 3;
            this.lbl1.Text = "参数列表";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.lbl1);
            base.Controls.Add(this.listView1);
            base.Name = "TemplateParamSetPage";
            base.Size = new Size(313, 159);
            base.Load += new EventHandler(this.TemplateParamSetPage_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private Label lbl1;
        private EditListView listView1;
        private LVColumnHeader lvcolumnHeader_0;
        private LVColumnHeader lvcolumnHeader_1;
    }
}