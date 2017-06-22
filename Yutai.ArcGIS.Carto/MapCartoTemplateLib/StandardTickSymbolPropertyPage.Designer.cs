using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;
using IPropertyPage = Yutai.ArcGIS.Common.BaseClasses.IPropertyPage;
using IPropertyPageEvents = Yutai.ArcGIS.Common.BaseClasses.IPropertyPageEvents;
using OnValueChangeEventHandler = Yutai.ArcGIS.Common.BaseClasses.OnValueChangeEventHandler;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    partial class StandardTickSymbolPropertyPage
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
            this.groupBox2 = new GroupBox();
            this.label4 = new Label();
            this.label1 = new Label();
            this.cboFontSize = new ComboBox();
            this.cboFontName = new ComboBox();
            this.groupBox1 = new GroupBox();
            this.btnStyle = new StyleButton();
            this.rdoTick = new RadioButton();
            this.rdoLine = new RadioButton();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cboFontSize);
            this.groupBox2.Controls.Add(this.cboFontName);
            this.groupBox2.Location = new Point(8, 109);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(264, 113);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "标注样式";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(17, 41);
            this.label4.Name = "label4";
            this.label4.Size = new Size(35, 12);
            this.label4.TabIndex = 34;
            this.label4.Text = "大小:";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(17, 17);
            this.label1.Name = "label1";
            this.label1.Size = new Size(35, 12);
            this.label1.TabIndex = 33;
            this.label1.Text = "字体:";
            this.cboFontSize.Text = "5";
            this.cboFontSize.Location = new Point(57, 41);
            this.cboFontSize.Name = "cboFontSize";
            this.cboFontSize.Items.AddRange(new object[] { 
                "5", "6", "7", "8", "9", "10", "11", "12", "14", "16", "18", "20", "22", "24", "26", "28", 
                "34", "36", "48", "72"
             });
            this.cboFontSize.Size = new Size(64, 21);
            this.cboFontSize.TabIndex = 32;
            this.cboFontSize.SelectedIndexChanged += new EventHandler(this.cboFontSize_SelectedIndexChanged);
            this.cboFontName.Location = new Point(57, 17);
            this.cboFontName.Name = "cboFontName";
            this.cboFontName.Size = new Size(184, 20);
            this.cboFontName.TabIndex = 31;
            this.cboFontName.SelectedIndexChanged += new EventHandler(this.cboFontName_SelectedIndexChanged);
            this.groupBox1.Controls.Add(this.btnStyle);
            this.groupBox1.Controls.Add(this.rdoTick);
            this.groupBox1.Controls.Add(this.rdoLine);
            this.groupBox1.Location = new Point(8, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(264, 84);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "显示属性";
            this.btnStyle.Location = new Point(64, 43);
            this.btnStyle.Name = "btnStyle";
            this.btnStyle.Size = new Size(80, 32);
            this.btnStyle.Style = null;
            this.btnStyle.TabIndex = 2;
            this.btnStyle.Click += new EventHandler(this.btnStyle_Click);
            this.rdoTick.AutoSize = true;
            this.rdoTick.Checked = true;
            this.rdoTick.Location = new Point(129, 21);
            this.rdoTick.Name = "rdoTick";
            this.rdoTick.Size = new Size(47, 16);
            this.rdoTick.TabIndex = 1;
            this.rdoTick.TabStop = true;
            this.rdoTick.Text = "十字";
            this.rdoTick.UseVisualStyleBackColor = true;
            this.rdoTick.CheckedChanged += new EventHandler(this.rdoTick_CheckedChanged);
            this.rdoLine.AutoSize = true;
            this.rdoLine.Location = new Point(19, 21);
            this.rdoLine.Name = "rdoLine";
            this.rdoLine.Size = new Size(59, 16);
            this.rdoLine.TabIndex = 0;
            this.rdoLine.Text = "网格线";
            this.rdoLine.UseVisualStyleBackColor = true;
            this.rdoLine.CheckedChanged += new EventHandler(this.rdoLine_CheckedChanged);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "StandardTickSymbolPropertyPage";
            base.Size = new Size(280, 240);
            base.Load += new EventHandler(this.StandardTickSymbolPropertyPage_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
        }

       
        private StyleButton btnStyle;
        private ComboBox cboFontName;
        private ComboBox cboFontSize;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label4;
        private RadioButton rdoLine;
        private RadioButton rdoTick;
    }
}