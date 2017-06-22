using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Location;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class HatchLayerExtensionPropertyPage
    {
        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_2);
        }

       
        private void InitializeComponent()
        {
            this.chkShowHatches = new CheckEdit();
            this.treeView1 = new TreeView();
            this.panel1 = new Panel();
            this.hatchClassCtrl1 = new HatchClassCtrl();
            this.panel2 = new Panel();
            this.txtHatchInt = new TextEdit();
            this.label2 = new Label();
            this.label1 = new Label();
            this.hatchDefinitionCtrl1 = new HatchDefinitionCtrl();
            this.btnAddHatchClass = new SimpleButton();
            this.btnDeleteHatchClass = new SimpleButton();
            this.btnDeleteAll = new SimpleButton();
            this.chkShowHatches.Properties.BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.txtHatchInt.Properties.BeginInit();
            base.SuspendLayout();
            this.chkShowHatches.Location = new Point(0, 8);
            this.chkShowHatches.Name = "chkShowHatches";
            this.chkShowHatches.Properties.Caption = "在此层中用刻度标注要素";
            this.chkShowHatches.Size = new Size(152, 19);
            this.chkShowHatches.TabIndex = 0;
            this.treeView1.HideSelection = false;
            this.treeView1.ImageIndex = -1;
            this.treeView1.Location = new Point(8, 40);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = -1;
            this.treeView1.Size = new Size(112, 144);
            this.treeView1.TabIndex = 1;
            this.treeView1.AfterSelect += new TreeViewEventHandler(this.treeView1_AfterSelect);
            this.panel1.Controls.Add(this.hatchClassCtrl1);
            this.panel1.Location = new Point(136, 56);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(320, 240);
            this.panel1.TabIndex = 2;
            this.hatchClassCtrl1.HatchClass = null;
            this.hatchClassCtrl1.Location = new Point(0, 0);
            this.hatchClassCtrl1.Name = "hatchClassCtrl1";
            this.hatchClassCtrl1.Size = new Size(320, 240);
            this.hatchClassCtrl1.TabIndex = 0;
            this.panel2.Controls.Add(this.txtHatchInt);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.hatchDefinitionCtrl1);
            this.panel2.Location = new Point(128, 48);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(320, 240);
            this.panel2.TabIndex = 3;
            this.txtHatchInt.EditValue = "1";
            this.txtHatchInt.Location = new Point(112, 8);
            this.txtHatchInt.Name = "txtHatchInt";
            this.txtHatchInt.Size = new Size(72, 23);
            this.txtHatchInt.TabIndex = 5;
            this.txtHatchInt.EditValueChanged += new EventHandler(this.txtHatchInt_EditValueChanged);
            this.txtHatchInt.Leave += new EventHandler(this.txtHatchInt_Leave);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(208, 8);
            this.label2.Name = "label2";
            this.label2.Size = new Size(66, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "个刻度间隔";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(91, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "放置刻度线，每";
            this.hatchDefinitionCtrl1.HatchDefinition = null;
            this.hatchDefinitionCtrl1.HatchInterval = 1;
            this.hatchDefinitionCtrl1.Location = new Point(0, 40);
            this.hatchDefinitionCtrl1.Name = "hatchDefinitionCtrl1";
            this.hatchDefinitionCtrl1.Size = new Size(320, 240);
            this.hatchDefinitionCtrl1.TabIndex = 2;
            this.btnAddHatchClass.Location = new Point(16, 200);
            this.btnAddHatchClass.Name = "btnAddHatchClass";
            this.btnAddHatchClass.Size = new Size(80, 24);
            this.btnAddHatchClass.TabIndex = 4;
            this.btnAddHatchClass.Text = "增加类...";
            this.btnAddHatchClass.Click += new EventHandler(this.btnAddHatchClass_Click);
            this.btnDeleteHatchClass.Location = new Point(16, 232);
            this.btnDeleteHatchClass.Name = "btnDeleteHatchClass";
            this.btnDeleteHatchClass.Size = new Size(80, 24);
            this.btnDeleteHatchClass.TabIndex = 5;
            this.btnDeleteHatchClass.Text = "删除类";
            this.btnDeleteHatchClass.Click += new EventHandler(this.btnDeleteHatchClass_Click);
            this.btnDeleteAll.Location = new Point(16, 264);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new Size(80, 24);
            this.btnDeleteAll.TabIndex = 6;
            this.btnDeleteAll.Text = "删除所有";
            this.btnDeleteAll.Click += new EventHandler(this.btnDeleteAll_Click);
            base.Controls.Add(this.btnDeleteAll);
            base.Controls.Add(this.btnDeleteHatchClass);
            base.Controls.Add(this.btnAddHatchClass);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.treeView1);
            base.Controls.Add(this.chkShowHatches);
            base.Name = "HatchLayerExtensionPropertyPage";
            base.Size = new Size(472, 304);
            base.Load += new EventHandler(this.HatchLayerExtensionPropertyPage_Load);
            this.chkShowHatches.Properties.EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.txtHatchInt.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnAddHatchClass;
        private SimpleButton btnDeleteAll;
        private SimpleButton btnDeleteHatchClass;
        private CheckEdit chkShowHatches;
        private HatchClassCtrl hatchClassCtrl1;
        private HatchDefinitionCtrl hatchDefinitionCtrl1;
        private Label label1;
        private Label label2;
        private Panel panel1;
        private Panel panel2;
        private TreeView treeView1;
        private TextEdit txtHatchInt;
    }
}