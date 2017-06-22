using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class frmNewRelationClass
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewRelationClass));
            this.panel1 = new Panel();
            this.btnNext = new SimpleButton();
            this.btnLast = new SimpleButton();
            this.simpleButton3 = new SimpleButton();
            this.panel2 = new Panel();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.btnLast);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 460);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(378, 40);
            this.panel1.TabIndex = 0;
            this.btnNext.Location = new Point(248, 8);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(56, 24);
            this.btnNext.TabIndex = 4;
            this.btnNext.Text = "下一步";
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.btnLast.Enabled = false;
            this.btnLast.Location = new Point(184, 8);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new Size(56, 24);
            this.btnLast.TabIndex = 3;
            this.btnLast.Text = "上一步";
            this.btnLast.Click += new EventHandler(this.btnLast_Click);
            this.simpleButton3.Location = new Point(312, 8);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new Size(56, 24);
            this.simpleButton3.TabIndex = 5;
            this.simpleButton3.Text = "取消";
            this.simpleButton3.Click += new EventHandler(this.simpleButton3_Click);
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(378, 460);
            this.panel2.TabIndex = 1;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(378, 500);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmNewRelationClass";
            this.Text = "新建关系类";
            base.Load += new EventHandler(this.frmNewRelationClass_Load);
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnLast;
        private SimpleButton btnNext;
        private NewRelationClass_LabelAndNotification newRelationClass_LabelAndNotification_0;
        private NewRelationClass_RelationType newRelationClass_RelationType_0;
        private NewRelationClass_SetCardinality newRelationClass_SetCardinality_0;
        private NewRelationClass_SetKey newRelationClass_SetKey_0;
        private NewRelationClassSetClass newRelationClassSetClass_0;
        private ObjectFieldsPage objectFieldsPage_0;
        private Panel panel1;
        private Panel panel2;
        private SimpleButton simpleButton3;
    }
}