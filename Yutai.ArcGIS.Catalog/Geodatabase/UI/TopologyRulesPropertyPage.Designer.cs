using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class TopologyRulesPropertyPage
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
            this.listView1 = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.columnHeader_2 = new ColumnHeader();
            this.btnDescription = new SimpleButton();
            this.btnAddRule = new SimpleButton();
            this.btnDeleteRule = new SimpleButton();
            this.btnDeleteAll = new SimpleButton();
            this.btnAddRule1 = new SimpleButton();
            base.SuspendLayout();
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1, this.columnHeader_2 });
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new Point(8, 8);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(296, 208);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.columnHeader_0.Text = "要素类";
            this.columnHeader_0.Width = 76;
            this.columnHeader_1.Text = "规则";
            this.columnHeader_1.Width = 135;
            this.columnHeader_2.Text = "要素类";
            this.columnHeader_2.Width = 75;
            this.btnDescription.Location = new Point(312, 8);
            this.btnDescription.Name = "btnDescription";
            this.btnDescription.Size = new Size(96, 32);
            this.btnDescription.TabIndex = 1;
            this.btnDescription.Text = "描述";
            this.btnDescription.Click += new EventHandler(this.btnDescription_Click);
            this.btnAddRule.Location = new Point(312, 48);
            this.btnAddRule.Name = "btnAddRule";
            this.btnAddRule.Size = new Size(96, 32);
            this.btnAddRule.TabIndex = 2;
            this.btnAddRule.Text = "按类添加规则";
            this.btnAddRule.Click += new EventHandler(this.btnAddRule_Click);
            this.btnDeleteRule.Enabled = false;
            this.btnDeleteRule.Location = new Point(312, 125);
            this.btnDeleteRule.Name = "btnDeleteRule";
            this.btnDeleteRule.Size = new Size(96, 32);
            this.btnDeleteRule.TabIndex = 3;
            this.btnDeleteRule.Text = "删除";
            this.btnDeleteRule.Click += new EventHandler(this.btnDeleteRule_Click);
            this.btnDeleteAll.Location = new Point(312, 165);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new Size(96, 32);
            this.btnDeleteAll.TabIndex = 4;
            this.btnDeleteAll.Text = "全部删除";
            this.btnDeleteAll.Click += new EventHandler(this.btnDeleteAll_Click);
            this.btnAddRule1.Location = new Point(310, 86);
            this.btnAddRule1.Name = "btnAddRule1";
            this.btnAddRule1.Size = new Size(98, 32);
            this.btnAddRule1.TabIndex = 5;
            this.btnAddRule1.Text = "添加规则";
            this.btnAddRule1.Click += new EventHandler(this.btnAddRule1_Click);
            base.Controls.Add(this.btnAddRule1);
            base.Controls.Add(this.btnDeleteAll);
            base.Controls.Add(this.btnDeleteRule);
            base.Controls.Add(this.btnAddRule);
            base.Controls.Add(this.btnDescription);
            base.Controls.Add(this.listView1);
            base.Name = "TopologyRulesPropertyPage";
            base.Size = new Size(411, 272);
            base.Load += new EventHandler(this.TopologyRulesPropertyPage_Load);
            base.ResumeLayout(false);
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

       
        private SimpleButton btnAddRule;
        private SimpleButton btnAddRule1;
        private SimpleButton btnDeleteAll;
        private SimpleButton btnDeleteRule;
        private SimpleButton btnDescription;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ColumnHeader columnHeader_2;
        private ListView listView1;
    }
}