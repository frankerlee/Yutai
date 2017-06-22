using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.GISClient;
using ESRI.ArcGIS.Server;

namespace Yutai.ArcGIS.Catalog.UI
{
    partial class ServerDirectoryPropertyPage
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
            this.btnEdit = new SimpleButton();
            this.btnDelete = new SimpleButton();
            this.btnAdd = new SimpleButton();
            this.lstDir = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.cboDirectoryType = new ComboBoxEdit();
            this.label4 = new Label();
            this.cboDirectoryType.Properties.BeginInit();
            base.SuspendLayout();
            this.btnEdit.Location = new Point(343, 132);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new Size(56, 24);
            this.btnEdit.TabIndex = 13;
            this.btnEdit.Text = "编辑...";
            this.btnDelete.Location = new Point(343, 100);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(56, 24);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.Text = "删除";
            this.btnAdd.Location = new Point(343, 68);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(56, 24);
            this.btnAdd.TabIndex = 11;
            this.btnAdd.Text = "添加...";
            this.lstDir.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1 });
            this.lstDir.Location = new Point(18, 68);
            this.lstDir.Name = "lstDir";
            this.lstDir.Size = new Size(319, 112);
            this.lstDir.TabIndex = 10;
            this.lstDir.UseCompatibleStateImageBehavior = false;
            this.lstDir.View = View.Details;
            this.columnHeader_0.Text = "名称";
            this.columnHeader_0.Width = 115;
            this.columnHeader_1.Text = "位置";
            this.columnHeader_1.Width = 160;
            this.cboDirectoryType.EditValue = "缓存目录";
            this.cboDirectoryType.Location = new Point(81, 40);
            this.cboDirectoryType.Name = "cboDirectoryType";
            this.cboDirectoryType.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboDirectoryType.Properties.Items.AddRange(new object[] { "缓存目录", "作业目录", "输出目录", "系统目录" });
            this.cboDirectoryType.Size = new Size(256, 21);
            this.cboDirectoryType.TabIndex = 15;
            this.cboDirectoryType.SelectedIndexChanged += new EventHandler(this.cboDirectoryType_SelectedIndexChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(16, 43);
            this.label4.Name = "label4";
            this.label4.Size = new Size(59, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "目录类型:";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.cboDirectoryType);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.btnEdit);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.btnAdd);
            base.Controls.Add(this.lstDir);
            base.Name = "ServerDirectoryPropertyPage";
            base.Size = new Size(415, 225);
            base.Load += new EventHandler(this.ServerDirectoryPropertyPage_Load);
            this.cboDirectoryType.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private SimpleButton btnAdd;
        private SimpleButton btnDelete;
        private SimpleButton btnEdit;
        private ComboBoxEdit cboDirectoryType;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private IAGSServerConnectionAdmin iagsserverConnectionAdmin_0;
        private Label label4;
        private ListView lstDir;
    }
}