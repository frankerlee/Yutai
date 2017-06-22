using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class NewRelationClassSetClass
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
            this.label1 = new Label();
            this.groupBox1 = new GroupBox();
            this.treeViewDest = new TreeView();
            this.treeViewSource = new TreeView();
            this.label3 = new Label();
            this.label2 = new Label();
            this.txtRelationClassName = new TextEdit();
            this.groupBox1.SuspendLayout();
            this.txtRelationClassName.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 12);
            this.label1.Name = "label1";
            this.label1.Size = new Size(72, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "关系类名称:";
            this.groupBox1.Controls.Add(this.treeViewDest);
            this.groupBox1.Controls.Add(this.treeViewSource);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new Point(16, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(232, 264);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择表/要素类";
            this.treeViewDest.HideSelection = false;
            this.treeViewDest.ImageIndex = -1;
            this.treeViewDest.Location = new Point(16, 168);
            this.treeViewDest.Name = "treeViewDest";
            this.treeViewDest.SelectedImageIndex = -1;
            this.treeViewDest.Size = new Size(200, 80);
            this.treeViewDest.TabIndex = 4;
            this.treeViewDest.AfterSelect += new TreeViewEventHandler(this.treeViewDest_AfterSelect);
            this.treeViewSource.HideSelection = false;
            this.treeViewSource.ImageIndex = -1;
            this.treeViewSource.Location = new Point(16, 48);
            this.treeViewSource.Name = "treeViewSource";
            this.treeViewSource.SelectedImageIndex = -1;
            this.treeViewSource.Size = new Size(200, 80);
            this.treeViewSource.TabIndex = 3;
            this.treeViewSource.AfterSelect += new TreeViewEventHandler(this.treeViewSource_AfterSelect);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(16, 141);
            this.label3.Name = "label3";
            this.label3.Size = new Size(85, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "目标表/要素类";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 24);
            this.label2.Name = "label2";
            this.label2.Size = new Size(72, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "源表/要素类";
            this.txtRelationClassName.EditValue = "";
            this.txtRelationClassName.Location = new Point(24, 32);
            this.txtRelationClassName.Name = "txtRelationClassName";
            this.txtRelationClassName.Size = new Size(224, 23);
            this.txtRelationClassName.TabIndex = 2;
            this.txtRelationClassName.EditValueChanged += new EventHandler(this.txtRelationClassName_EditValueChanged);
            base.Controls.Add(this.txtRelationClassName);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.label1);
            base.Name = "NewRelationClassSetClass";
            base.Size = new Size(272, 344);
            base.Load += new EventHandler(this.NewRelationClassSetClass_Load);
            this.groupBox1.ResumeLayout(false);
            this.txtRelationClassName.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private TreeView treeViewDest;
        private TreeView treeViewSource;
        private TextEdit txtRelationClassName;
    }
}