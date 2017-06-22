using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class frmAddTinRender
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddTinRender));
            this.listBoxControl1 = new ListBoxControl();
            this.btnAdd = new SimpleButton();
            this.btnClose = new SimpleButton();
            ((ISupportInitialize) this.listBoxControl1).BeginInit();
            base.SuspendLayout();
            this.listBoxControl1.Items.AddRange(new object[] { "唯一值渲染边类型", "简单渲染边", "分级色彩渲染表面坡向", "分级色彩渲染表面高程", "分级色彩渲染表面坡度", "唯一值渲染面标签值", "同一符号渲染表面", "分级色彩渲染节点高程", "唯一值渲染节点标签值", "同一符号渲染节点" });
            this.listBoxControl1.Location = new Point(3, 2);
            this.listBoxControl1.Name = "listBoxControl1";
            this.listBoxControl1.Size = new Size(277, 190);
            this.listBoxControl1.TabIndex = 0;
            this.listBoxControl1.MouseDoubleClick += new MouseEventHandler(this.listBoxControl1_MouseDoubleClick);
            this.listBoxControl1.SelectedIndexChanged += new EventHandler(this.listBoxControl1_SelectedIndexChanged);
            this.btnAdd.Enabled = false;
            this.btnAdd.Location = new Point(115, 198);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(75, 23);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "添加";
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new Point(196, 198);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "关闭";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(292, 239);
            base.Controls.Add(this.btnClose);
            base.Controls.Add(this.btnAdd);
            base.Controls.Add(this.listBoxControl1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmAddTinRender";
            this.Text = "添加渲染";
            base.Load += new EventHandler(this.frmAddTinRender_Load);
            ((ISupportInitialize) this.listBoxControl1).EndInit();
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnAdd;
        private SimpleButton btnClose;
        private ListBoxControl listBoxControl1;
    }
}