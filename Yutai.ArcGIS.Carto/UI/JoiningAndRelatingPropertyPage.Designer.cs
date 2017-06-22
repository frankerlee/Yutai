using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class JoiningAndRelatingPropertyPage
    {
        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_1);
        }

       
 private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.label1 = new Label();
            this.groupBox2 = new GroupBox();
            this.label2 = new Label();
            this.JoiningDataList = new ListBoxControl();
            this.btnAddJoining = new SimpleButton();
            this.btnRemoveJoining = new SimpleButton();
            this.btnRemoveAllJoining = new SimpleButton();
            this.btnRemoveAllRelating = new SimpleButton();
            this.btnRemoveRelating = new SimpleButton();
            this.btnAddRelating = new SimpleButton();
            this.RelatingDataList = new ListBoxControl();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((ISupportInitialize) this.JoiningDataList).BeginInit();
            ((ISupportInitialize) this.RelatingDataList).BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.btnRemoveAllJoining);
            this.groupBox1.Controls.Add(this.btnRemoveJoining);
            this.groupBox1.Controls.Add(this.btnAddJoining);
            this.groupBox1.Controls.Add(this.JoiningDataList);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(16, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(216, 200);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "连接";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 24);
            this.label1.Name = "label1";
            this.label1.Size = new Size(196, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "显示添加到当前表/图层中的属性表";
            this.groupBox2.Controls.Add(this.btnRemoveAllRelating);
            this.groupBox2.Controls.Add(this.btnRemoveRelating);
            this.groupBox2.Controls.Add(this.btnAddRelating);
            this.groupBox2.Controls.Add(this.RelatingDataList);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new Point(240, 16);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(216, 200);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "关联";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 24);
            this.label2.Name = "label2";
            this.label2.Size = new Size(196, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "显示添加到当前表/图层中的属性表";
            this.JoiningDataList.ItemHeight = 17;
            this.JoiningDataList.Location = new Point(8, 48);
            this.JoiningDataList.Name = "JoiningDataList";
            this.JoiningDataList.Size = new Size(128, 136);
            this.JoiningDataList.TabIndex = 1;
            this.JoiningDataList.SelectedIndexChanged += new EventHandler(this.JoiningDataList_SelectedIndexChanged);
            this.btnAddJoining.Location = new Point(144, 48);
            this.btnAddJoining.Name = "btnAddJoining";
            this.btnAddJoining.Size = new Size(64, 24);
            this.btnAddJoining.TabIndex = 2;
            this.btnAddJoining.Text = "添加";
            this.btnAddJoining.Click += new EventHandler(this.btnAddJoining_Click);
            this.btnRemoveJoining.Enabled = false;
            this.btnRemoveJoining.Location = new Point(144, 80);
            this.btnRemoveJoining.Name = "btnRemoveJoining";
            this.btnRemoveJoining.Size = new Size(64, 24);
            this.btnRemoveJoining.TabIndex = 3;
            this.btnRemoveJoining.Text = "移除";
            this.btnRemoveJoining.Click += new EventHandler(this.btnRemoveJoining_Click);
            this.btnRemoveAllJoining.Enabled = false;
            this.btnRemoveAllJoining.Location = new Point(144, 112);
            this.btnRemoveAllJoining.Name = "btnRemoveAllJoining";
            this.btnRemoveAllJoining.Size = new Size(64, 24);
            this.btnRemoveAllJoining.TabIndex = 4;
            this.btnRemoveAllJoining.Text = "移除所有";
            this.btnRemoveAllJoining.Click += new EventHandler(this.btnRemoveAllJoining_Click);
            this.btnRemoveAllRelating.Enabled = false;
            this.btnRemoveAllRelating.Location = new Point(144, 112);
            this.btnRemoveAllRelating.Name = "btnRemoveAllRelating";
            this.btnRemoveAllRelating.Size = new Size(64, 24);
            this.btnRemoveAllRelating.TabIndex = 8;
            this.btnRemoveAllRelating.Text = "移除所有";
            this.btnRemoveAllRelating.Click += new EventHandler(this.btnRemoveAllRelating_Click);
            this.btnRemoveRelating.Enabled = false;
            this.btnRemoveRelating.Location = new Point(144, 80);
            this.btnRemoveRelating.Name = "btnRemoveRelating";
            this.btnRemoveRelating.Size = new Size(64, 24);
            this.btnRemoveRelating.TabIndex = 7;
            this.btnRemoveRelating.Text = "移除";
            this.btnRemoveRelating.Click += new EventHandler(this.btnRemoveRelating_Click);
            this.btnAddRelating.Location = new Point(144, 48);
            this.btnAddRelating.Name = "btnAddRelating";
            this.btnAddRelating.Size = new Size(64, 24);
            this.btnAddRelating.TabIndex = 6;
            this.btnAddRelating.Text = "添加";
            this.btnAddRelating.Click += new EventHandler(this.btnAddRelating_Click);
            this.RelatingDataList.ItemHeight = 17;
            this.RelatingDataList.Location = new Point(8, 48);
            this.RelatingDataList.Name = "RelatingDataList";
            this.RelatingDataList.Size = new Size(128, 136);
            this.RelatingDataList.TabIndex = 5;
            this.RelatingDataList.SelectedIndexChanged += new EventHandler(this.RelatingDataList_SelectedIndexChanged);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "JoiningAndRelatingPropertyPage";
            base.Size = new Size(480, 304);
            base.Load += new EventHandler(this.JoiningAndRelatingPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((ISupportInitialize) this.JoiningDataList).EndInit();
            ((ISupportInitialize) this.RelatingDataList).EndInit();
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnAddJoining;
        private SimpleButton btnAddRelating;
        private SimpleButton btnRemoveAllJoining;
        private SimpleButton btnRemoveAllRelating;
        private SimpleButton btnRemoveJoining;
        private SimpleButton btnRemoveRelating;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private ListBoxControl JoiningDataList;
        private Label label1;
        private Label label2;
        private ListBoxControl RelatingDataList;
    }
}