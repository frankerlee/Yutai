using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.ControlExtend;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class LayerFieldsPageExtend
    {
        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_2);
        }

       
 private void InitializeComponent()
        {
            this.label1 = new Label();
            this.listView1 = new EditListView();
            this.lvcolumnHeader_0 = new LVColumnHeader();
            this.lvcolumnHeader_1 = new LVColumnHeader();
            this.lvcolumnHeader_2 = new LVColumnHeader();
            this.lvcolumnHeader_3 = new LVColumnHeader();
            this.lvcolumnHeader_4 = new LVColumnHeader();
            this.cboFields = new ComboBoxEdit();
            this.label2 = new Label();
            this.cboFields.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "字段列表";
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.lvcolumnHeader_0, this.lvcolumnHeader_1, this.lvcolumnHeader_2, this.lvcolumnHeader_3, this.lvcolumnHeader_4 });
            this.listView1.ComboBoxBgColor = Color.LightBlue;
            this.listView1.ComboBoxFont = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
            this.listView1.EditBgColor = Color.LightBlue;
            this.listView1.EditFont = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new Point(3, 35);
            this.listView1.LockRowCount = 0;
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(455, 207);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.lvcolumnHeader_0.ColumnStyle = ListViewColumnStyle.ReadOnly;
            this.lvcolumnHeader_0.Text = "字段名称";
            this.lvcolumnHeader_0.Width = 149;
            this.lvcolumnHeader_1.ColumnStyle = ListViewColumnStyle.EditBox;
            this.lvcolumnHeader_1.Text = "别名";
            this.lvcolumnHeader_1.Width = 108;
            this.lvcolumnHeader_2.ColumnStyle = ListViewColumnStyle.ReadOnly;
            this.lvcolumnHeader_2.Text = "类型";
            this.lvcolumnHeader_3.ColumnStyle = ListViewColumnStyle.ReadOnly;
            this.lvcolumnHeader_3.Text = "长度";
            this.lvcolumnHeader_4.ColumnStyle = ListViewColumnStyle.ReadOnly;
            this.lvcolumnHeader_4.Text = "精度";
            this.cboFields.EditValue = "";
            this.cboFields.Location = new Point(87, -24);
            this.cboFields.Name = "cboFields";
            this.cboFields.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFields.Size = new Size(144, 21);
            this.cboFields.TabIndex = 3;
            this.cboFields.SelectedIndexChanged += new EventHandler(this.cboFields_SelectedIndexChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(16, -19);
            this.label2.Name = "label2";
            this.label2.Size = new Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "主显示字段";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.cboFields);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.label1);
            base.Name = "LayerFieldsPageExtend";
            base.Size = new Size(472, 258);
            base.Load += new EventHandler(this.LayerFieldsPageExtend_Load);
            this.cboFields.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private ComboBoxEdit cboFields;
        private ILayer ilayer_0;
        private Label label1;
        private Label label2;
        private EditListView listView1;
        private LVColumnHeader lvcolumnHeader_0;
        private LVColumnHeader lvcolumnHeader_1;
        private LVColumnHeader lvcolumnHeader_2;
        private LVColumnHeader lvcolumnHeader_3;
        private LVColumnHeader lvcolumnHeader_4;
    }
}