using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using Yutai.ArcGIS.Controls.SymbolUI;
using IPropertyPage = Yutai.ArcGIS.Common.BaseClasses.IPropertyPage;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class frmMapGridsProperty
    {
        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

       
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMapGridsProperty));
            this.label1 = new Label();
            this.checkedListBox1 = new CheckedListBox();
            this.btnClose = new SimpleButton();
            this.btnAdd = new SimpleButton();
            this.btnDelete = new SimpleButton();
            this.btnProperty = new SimpleButton();
            this.btnOK = new SimpleButton();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "格网";
            this.checkedListBox1.Location = new Point(16, 32);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new Size(224, 148);
            this.checkedListBox1.TabIndex = 1;
            this.checkedListBox1.SelectedIndexChanged += new EventHandler(this.checkedListBox1_SelectedIndexChanged);
            this.checkedListBox1.ItemCheck += new ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new Point(200, 192);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(56, 24);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "取消";
            this.btnAdd.Location = new Point(248, 32);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(56, 24);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "添加";
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.btnDelete.Location = new Point(248, 64);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(56, 24);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.btnProperty.Location = new Point(248, 96);
            this.btnProperty.Name = "btnProperty";
            this.btnProperty.Size = new Size(56, 24);
            this.btnProperty.TabIndex = 5;
            this.btnProperty.Text = "样式";
            this.btnProperty.Click += new EventHandler(this.btnProperty_Click);
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new Point(120, 192);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(56, 24);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(312, 229);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnProperty);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.btnAdd);
            base.Controls.Add(this.btnClose);
            base.Controls.Add(this.checkedListBox1);
            base.Controls.Add(this.label1);
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmMapGridsProperty";
            this.Text = "格网属性";
            base.Load += new EventHandler(this.frmMapGridsProperty_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private SimpleButton btnAdd;
        private SimpleButton btnClose;
        private SimpleButton btnDelete;
        private SimpleButton btnOK;
        private SimpleButton btnProperty;
        private CheckedListBox checkedListBox1;
        private Label label1;
    }
}