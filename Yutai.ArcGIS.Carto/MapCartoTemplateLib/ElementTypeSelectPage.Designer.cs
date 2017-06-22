using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    partial class ElementTypeSelectPage
    {
        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_2);
        }

       
        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.cboMapTemplateElementType = new ComboBox();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(18, 24);
            this.label1.Name = "label1";
            this.label1.Size = new Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "元素类型";
            this.cboMapTemplateElementType.FormattingEnabled = true;
            this.cboMapTemplateElementType.Items.AddRange(new object[] { "文本    ", "比例尺文本", "比例尺栏", "图例", "图片", "OLE对象", "指北针", "接图表", "自定义图例", "表格" });
            this.cboMapTemplateElementType.Location = new Point(77, 21);
            this.cboMapTemplateElementType.Name = "cboMapTemplateElementType";
            this.cboMapTemplateElementType.Size = new Size(121, 20);
            this.cboMapTemplateElementType.TabIndex = 1;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.cboMapTemplateElementType);
            base.Controls.Add(this.label1);
            base.Name = "ElementTypeSelectPage";
            base.Size = new Size(293, 320);
            base.Load += new EventHandler(this.ElementTypeSelectPage_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

       
        private ComboBox cboMapTemplateElementType;
        private Label label1;
        private MapCartoTemplateLib.MapTemplate mapTemplate_0;
        private MapCartoTemplateLib.MapTemplateElement mapTemplateElement_0;
    }
}