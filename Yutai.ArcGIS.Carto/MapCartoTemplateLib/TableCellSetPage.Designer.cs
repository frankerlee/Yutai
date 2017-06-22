using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    partial class TableCellSetPage
    {
        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_1);
        }

       
        private void InitializeComponent()
        {
            this.listView1 = new ListView();
            this.btnSetCell = new Button();
            base.SuspendLayout();
            this.listView1.Location = new Point(13, 46);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(296, 181);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.btnSetCell.Location = new Point(122, 251);
            this.btnSetCell.Name = "btnSetCell";
            this.btnSetCell.Size = new Size(86, 23);
            this.btnSetCell.TabIndex = 1;
            this.btnSetCell.Text = "设置单元格";
            this.btnSetCell.UseVisualStyleBackColor = true;
            this.btnSetCell.Click += new EventHandler(this.btnSetCell_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.btnSetCell);
            base.Controls.Add(this.listView1);
            base.Name = "TableCellSetPage";
            base.Size = new Size(327, 301);
            base.Load += new EventHandler(this.TableCellSetPage_Load);
            base.VisibleChanged += new EventHandler(this.TableCellSetPage_VisibleChanged);
            base.ResumeLayout(false);
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

       
        private Button btnSetCell;
        private ListView listView1;
        private MapTemplateElement mapTemplateElement_0;
    }
}