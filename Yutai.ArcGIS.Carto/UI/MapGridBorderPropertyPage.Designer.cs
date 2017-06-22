using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class MapGridBorderPropertyPage
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
            this.groupBox2 = new GroupBox();
            this.btnOutlineSymbol = new StyleButton();
            this.chkOutline = new CheckEdit();
            this.groupBoxGridBorder = new GroupBox();
            this.btnSimpleBorderLine = new StyleButton();
            this.chkUseSimpleBorder = new CheckEdit();
            this.groupBoxGridProperty = new GroupBox();
            this.rdoGenerateGraphicsType = new RadioGroup();
            this.groupBoxGraticuleBorder = new GroupBox();
            this.btnCalibratedMapBorder = new SimpleButton();
            this.btnSimpleBorderLineSymbol = new StyleButton();
            this.rdoGraticuleBorderType = new RadioGroup();
            this.groupBox2.SuspendLayout();
            this.chkOutline.Properties.BeginInit();
            this.groupBoxGridBorder.SuspendLayout();
            this.chkUseSimpleBorder.Properties.BeginInit();
            this.groupBoxGridProperty.SuspendLayout();
            this.rdoGenerateGraphicsType.Properties.BeginInit();
            this.groupBoxGraticuleBorder.SuspendLayout();
            this.rdoGraticuleBorderType.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox2.Controls.Add(this.btnOutlineSymbol);
            this.groupBox2.Controls.Add(this.chkOutline);
            this.groupBox2.Location = new Point(8, 133);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(272, 51);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "图廓线";
            this.btnOutlineSymbol.Enabled = false;
            this.btnOutlineSymbol.Location = new Point(168, 16);
            this.btnOutlineSymbol.Name = "btnOutlineSymbol";
            this.btnOutlineSymbol.Size = new Size(88, 24);
            this.btnOutlineSymbol.Style = null;
            this.btnOutlineSymbol.TabIndex = 1;
            this.btnOutlineSymbol.Click += new EventHandler(this.btnOutlineSymbol_Click);
            this.chkOutline.Location = new Point(8, 16);
            this.chkOutline.Name = "chkOutline";
            this.chkOutline.Properties.Caption = "在格网外放置边框";
            this.chkOutline.Size = new Size(128, 19);
            this.chkOutline.TabIndex = 0;
            this.chkOutline.CheckedChanged += new EventHandler(this.chkOutline_CheckedChanged);
            this.groupBoxGridBorder.Controls.Add(this.btnSimpleBorderLine);
            this.groupBoxGridBorder.Controls.Add(this.chkUseSimpleBorder);
            this.groupBoxGridBorder.Location = new Point(8, 8);
            this.groupBoxGridBorder.Name = "groupBoxGridBorder";
            this.groupBoxGridBorder.Size = new Size(272, 120);
            this.groupBoxGridBorder.TabIndex = 2;
            this.groupBoxGridBorder.TabStop = false;
            this.groupBoxGridBorder.Text = "格网边框";
            this.btnSimpleBorderLine.Location = new Point(48, 56);
            this.btnSimpleBorderLine.Name = "btnSimpleBorderLine";
            this.btnSimpleBorderLine.Size = new Size(112, 32);
            this.btnSimpleBorderLine.Style = null;
            this.btnSimpleBorderLine.TabIndex = 1;
            this.btnSimpleBorderLine.Click += new EventHandler(this.btnSimpleBorderLine_Click);
            this.chkUseSimpleBorder.Location = new Point(24, 24);
            this.chkUseSimpleBorder.Name = "chkUseSimpleBorder";
            this.chkUseSimpleBorder.Properties.Caption = "在格网和轴标注间放置边框";
            this.chkUseSimpleBorder.Size = new Size(176, 19);
            this.chkUseSimpleBorder.TabIndex = 0;
            this.chkUseSimpleBorder.CheckedChanged += new EventHandler(this.chkUseSimpleBorder_CheckedChanged);
            this.groupBoxGridProperty.Controls.Add(this.rdoGenerateGraphicsType);
            this.groupBoxGridProperty.Location = new Point(8, 192);
            this.groupBoxGridProperty.Name = "groupBoxGridProperty";
            this.groupBoxGridProperty.Size = new Size(272, 64);
            this.groupBoxGridProperty.TabIndex = 4;
            this.groupBoxGridProperty.TabStop = false;
            this.groupBoxGridProperty.Text = "格网属性";
            this.rdoGenerateGraphicsType.Location = new Point(16, 16);
            this.rdoGenerateGraphicsType.Name = "rdoGenerateGraphicsType";
            this.rdoGenerateGraphicsType.Properties.Appearance.BackColor = SystemColors.Control;
            this.rdoGenerateGraphicsType.Properties.Appearance.Options.UseBackColor = true;
            this.rdoGenerateGraphicsType.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoGenerateGraphicsType.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "作为可以编辑的静态图形存储"), new RadioGroupItem(null, "作为与数据框同步更新的固定格网存储") });
            this.rdoGenerateGraphicsType.Size = new Size(248, 40);
            this.rdoGenerateGraphicsType.SelectedIndex = 1;
            this.rdoGenerateGraphicsType.TabIndex = 0;
            this.groupBoxGraticuleBorder.Controls.Add(this.btnCalibratedMapBorder);
            this.groupBoxGraticuleBorder.Controls.Add(this.btnSimpleBorderLineSymbol);
            this.groupBoxGraticuleBorder.Controls.Add(this.rdoGraticuleBorderType);
            this.groupBoxGraticuleBorder.Location = new Point(8, 8);
            this.groupBoxGraticuleBorder.Name = "groupBoxGraticuleBorder";
            this.groupBoxGraticuleBorder.Size = new Size(272, 120);
            this.groupBoxGraticuleBorder.TabIndex = 5;
            this.groupBoxGraticuleBorder.TabStop = false;
            this.groupBoxGraticuleBorder.Text = "经纬网边框";
            this.btnCalibratedMapBorder.Location = new Point(72, 86);
            this.btnCalibratedMapBorder.Name = "btnCalibratedMapBorder";
            this.btnCalibratedMapBorder.Size = new Size(80, 22);
            this.btnCalibratedMapBorder.TabIndex = 4;
            this.btnCalibratedMapBorder.Text = "属性...";
            this.btnCalibratedMapBorder.Click += new EventHandler(this.btnCalibratedMapBorder_Click);
            this.btnSimpleBorderLineSymbol.Location = new Point(64, 41);
            this.btnSimpleBorderLineSymbol.Name = "btnSimpleBorderLineSymbol";
            this.btnSimpleBorderLineSymbol.Size = new Size(104, 22);
            this.btnSimpleBorderLineSymbol.Style = null;
            this.btnSimpleBorderLineSymbol.TabIndex = 3;
            this.btnSimpleBorderLineSymbol.Click += new EventHandler(this.btnSimpleBorderLineSymbol_Click);
            this.rdoGraticuleBorderType.Location = new Point(8, 12);
            this.rdoGraticuleBorderType.Name = "rdoGraticuleBorderType";
            this.rdoGraticuleBorderType.Properties.Appearance.BackColor = SystemColors.Control;
            this.rdoGraticuleBorderType.Properties.Appearance.Options.UseBackColor = true;
            this.rdoGraticuleBorderType.Properties.Appearance.Options.UseTextOptions = true;
            this.rdoGraticuleBorderType.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Near;
            this.rdoGraticuleBorderType.Properties.Appearance.TextOptions.VAlignment = VertAlignment.Top;
            this.rdoGraticuleBorderType.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoGraticuleBorderType.Properties.GlyphAlignment = HorzAlignment.Default;
            this.rdoGraticuleBorderType.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "在经纬网边界放置简单边框"), new RadioGroupItem(null, "在经纬网边界放置校准边框") });
            this.rdoGraticuleBorderType.Size = new Size(192, 84);
            this.rdoGraticuleBorderType.TabIndex = 2;
            this.rdoGraticuleBorderType.SelectedIndexChanged += new EventHandler(this.rdoGraticuleBorderType_SelectedIndexChanged);
            base.Controls.Add(this.groupBoxGraticuleBorder);
            base.Controls.Add(this.groupBoxGridProperty);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBoxGridBorder);
            base.Name = "MapGridBorderPropertyPage";
            base.Size = new Size(296, 272);
            base.Load += new EventHandler(this.MapGridBorderPropertyPage_Load);
            this.groupBox2.ResumeLayout(false);
            this.chkOutline.Properties.EndInit();
            this.groupBoxGridBorder.ResumeLayout(false);
            this.chkUseSimpleBorder.Properties.EndInit();
            this.groupBoxGridProperty.ResumeLayout(false);
            this.rdoGenerateGraphicsType.Properties.EndInit();
            this.groupBoxGraticuleBorder.ResumeLayout(false);
            this.rdoGraticuleBorderType.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnCalibratedMapBorder;
        private StyleButton btnOutlineSymbol;
        private StyleButton btnSimpleBorderLine;
        private StyleButton btnSimpleBorderLineSymbol;
        private CheckEdit chkOutline;
        private CheckEdit chkUseSimpleBorder;
        private GroupBox groupBox2;
        private GroupBox groupBoxGraticuleBorder;
        private GroupBox groupBoxGridBorder;
        private GroupBox groupBoxGridProperty;
        private RadioGroup rdoGenerateGraphicsType;
        private RadioGroup rdoGraticuleBorderType;
    }
}