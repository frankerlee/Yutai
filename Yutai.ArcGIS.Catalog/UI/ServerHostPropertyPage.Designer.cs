using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.GISClient;
using ESRI.ArcGIS.Server;

namespace Yutai.ArcGIS.Catalog.UI
{
    partial class ServerHostPropertyPage
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
            this.btnStart = new SimpleButton();
            this.btnDelete = new SimpleButton();
            this.btnAdd = new SimpleButton();
            this.lstDir = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.btnStop = new SimpleButton();
            this.columnHeader_2 = new ColumnHeader();
            base.SuspendLayout();
            this.btnStart.Location = new Point(337, 108);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new Size(56, 24);
            this.btnStart.TabIndex = 17;
            this.btnStart.Text = "启动";
            this.btnDelete.Location = new Point(337, 76);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(56, 24);
            this.btnDelete.TabIndex = 16;
            this.btnDelete.Text = "删除";
            this.btnAdd.Location = new Point(337, 44);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(56, 24);
            this.btnAdd.TabIndex = 15;
            this.btnAdd.Text = "添加...";
            this.lstDir.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1, this.columnHeader_2 });
            this.lstDir.Location = new Point(12, 44);
            this.lstDir.Name = "lstDir";
            this.lstDir.Size = new Size(319, 112);
            this.lstDir.TabIndex = 14;
            this.lstDir.UseCompatibleStateImageBehavior = false;
            this.lstDir.View = View.Details;
            this.columnHeader_0.Text = "名称";
            this.columnHeader_0.Width = 115;
            this.columnHeader_1.Text = "管理URL";
            this.columnHeader_1.Width = 160;
            this.btnStop.Location = new Point(337, 138);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new Size(56, 24);
            this.btnStop.TabIndex = 18;
            this.btnStop.Text = "停止";
            this.columnHeader_2.Text = "状态";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.btnStop);
            base.Controls.Add(this.btnStart);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.btnAdd);
            base.Controls.Add(this.lstDir);
            base.Name = "ServerHostPropertyPage";
            base.Size = new Size(398, 244);
            base.Load += new EventHandler(this.ServerHostPropertyPage_Load);
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnAdd;
        private SimpleButton btnDelete;
        private SimpleButton btnStart;
        private SimpleButton btnStop;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ColumnHeader columnHeader_2;
        private IAGSServerConnectionAdmin iagsserverConnectionAdmin_0;
        private ListView lstDir;
    }
}