using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Symbol;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;
using IPropertyPage = Yutai.ArcGIS.Common.BaseClasses.IPropertyPage;
using IPropertyPageEvents = Yutai.ArcGIS.Common.BaseClasses.IPropertyPageEvents;
using OnValueChangeEventHandler = Yutai.ArcGIS.Common.BaseClasses.OnValueChangeEventHandler;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    partial class LegendItemGeneralPropertyPage
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
            this.icontainer_0 = new Container();
            this.groupBox1 = new GroupBox();
            this.cboAreaPatches = new StyleComboBox(this.icontainer_0);
            this.cboLinePatches = new StyleComboBox(this.icontainer_0);
            this.txtDefaultPatchHeight = new TextEdit();
            this.txtDefaultPatchWidth = new TextEdit();
            this.label4 = new Label();
            this.label3 = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            this.chkOveralpDefaultPatchSize = new CheckEdit();
            this.chkOveralpDefaultPatch = new CheckEdit();
            this.btnDescriptionSymbol = new SimpleButton();
            this.chkShowDescription = new CheckEdit();
            this.btnTitleSymbol = new SimpleButton();
            this.chkShowTitle = new CheckEdit();
            this.btnLabelSymbol = new SimpleButton();
            this.btnLayerNameSymbol = new SimpleButton();
            this.chkShowLabel = new CheckEdit();
            this.chkShowLayerName = new CheckEdit();
            this.groupBox1.SuspendLayout();
            this.txtDefaultPatchHeight.Properties.BeginInit();
            this.txtDefaultPatchWidth.Properties.BeginInit();
            this.chkOveralpDefaultPatchSize.Properties.BeginInit();
            this.chkOveralpDefaultPatch.Properties.BeginInit();
            this.chkShowDescription.Properties.BeginInit();
            this.chkShowTitle.Properties.BeginInit();
            this.chkShowLabel.Properties.BeginInit();
            this.chkShowLayerName.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.cboAreaPatches);
            this.groupBox1.Controls.Add(this.cboLinePatches);
            this.groupBox1.Controls.Add(this.txtDefaultPatchHeight);
            this.groupBox1.Controls.Add(this.txtDefaultPatchWidth);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.chkOveralpDefaultPatchSize);
            this.groupBox1.Controls.Add(this.chkOveralpDefaultPatch);
            this.groupBox1.Controls.Add(this.btnDescriptionSymbol);
            this.groupBox1.Controls.Add(this.chkShowDescription);
            this.groupBox1.Controls.Add(this.btnTitleSymbol);
            this.groupBox1.Controls.Add(this.chkShowTitle);
            this.groupBox1.Controls.Add(this.btnLabelSymbol);
            this.groupBox1.Controls.Add(this.btnLayerNameSymbol);
            this.groupBox1.Controls.Add(this.chkShowLabel);
            this.groupBox1.Controls.Add(this.chkShowLayerName);
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(288, 240);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "外观";
            this.cboAreaPatches.DrawMode = DrawMode.OwnerDrawVariable;
            this.cboAreaPatches.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboAreaPatches.DropDownWidth = 160;
            this.cboAreaPatches.Font = new Font("宋体", 15f, FontStyle.Regular, GraphicsUnit.Point, 134);
            this.cboAreaPatches.Location = new Point(48, 200);
            this.cboAreaPatches.Name = "cboAreaPatches";
            this.cboAreaPatches.Size = new Size(64, 31);
            this.cboAreaPatches.TabIndex = 17;
            this.cboLinePatches.DrawMode = DrawMode.OwnerDrawVariable;
            this.cboLinePatches.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboLinePatches.DropDownWidth = 160;
            this.cboLinePatches.Font = new Font("宋体", 15f, FontStyle.Regular, GraphicsUnit.Point, 134);
            this.cboLinePatches.ItemHeight = 20;
            this.cboLinePatches.Location = new Point(48, 168);
            this.cboLinePatches.Name = "cboLinePatches";
            this.cboLinePatches.Size = new Size(64, 26);
            this.cboLinePatches.TabIndex = 16;
            this.cboLinePatches.SelectedIndexChanged += new EventHandler(this.cboLinePatches_SelectedIndexChanged);
            this.txtDefaultPatchHeight.EditValue = "";
            this.txtDefaultPatchHeight.Location = new Point(184, 202);
            this.txtDefaultPatchHeight.Name = "txtDefaultPatchHeight";
            this.txtDefaultPatchHeight.Size = new Size(64, 21);
            this.txtDefaultPatchHeight.TabIndex = 15;
            this.txtDefaultPatchWidth.EditValue = "";
            this.txtDefaultPatchWidth.Location = new Point(184, 170);
            this.txtDefaultPatchWidth.Name = "txtDefaultPatchWidth";
            this.txtDefaultPatchWidth.Size = new Size(64, 21);
            this.txtDefaultPatchWidth.TabIndex = 14;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(144, 210);
            this.label4.Name = "label4";
            this.label4.Size = new Size(29, 17);
            this.label4.TabIndex = 13;
            this.label4.Text = "高度";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(144, 178);
            this.label3.Name = "label3";
            this.label3.Size = new Size(29, 17);
            this.label3.TabIndex = 12;
            this.label3.Text = "宽度";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(16, 202);
            this.label2.Name = "label2";
            this.label2.Size = new Size(17, 17);
            this.label2.TabIndex = 11;
            this.label2.Text = "面";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 170);
            this.label1.Name = "label1";
            this.label1.Size = new Size(17, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "线";
            this.chkOveralpDefaultPatchSize.EditValue = false;
            this.chkOveralpDefaultPatchSize.Location = new Point(144, 138);
            this.chkOveralpDefaultPatchSize.Name = "chkOveralpDefaultPatchSize";
            this.chkOveralpDefaultPatchSize.Properties.Caption = "覆盖默认区块尺寸";
            this.chkOveralpDefaultPatchSize.Size = new Size(128, 19);
            this.chkOveralpDefaultPatchSize.TabIndex = 9;
            this.chkOveralpDefaultPatchSize.CheckedChanged += new EventHandler(this.chkOveralpDefaultPatchSize_CheckedChanged);
            this.chkOveralpDefaultPatch.EditValue = false;
            this.chkOveralpDefaultPatch.Location = new Point(8, 138);
            this.chkOveralpDefaultPatch.Name = "chkOveralpDefaultPatch";
            this.chkOveralpDefaultPatch.Properties.Caption = "覆盖默认区块";
            this.chkOveralpDefaultPatch.Size = new Size(104, 19);
            this.chkOveralpDefaultPatch.TabIndex = 8;
            this.chkOveralpDefaultPatch.CheckedChanged += new EventHandler(this.chkOveralpDefaultPatch_CheckedChanged);
            this.btnDescriptionSymbol.Location = new Point(144, 101);
            this.btnDescriptionSymbol.Name = "btnDescriptionSymbol";
            this.btnDescriptionSymbol.Size = new Size(96, 24);
            this.btnDescriptionSymbol.TabIndex = 7;
            this.btnDescriptionSymbol.Text = "说明符号...";
            this.btnDescriptionSymbol.Click += new EventHandler(this.btnDescriptionSymbol_Click);
            this.chkShowDescription.EditValue = false;
            this.chkShowDescription.Location = new Point(144, 74);
            this.chkShowDescription.Name = "chkShowDescription";
            this.chkShowDescription.Properties.Caption = "显示说明";
            this.chkShowDescription.Size = new Size(88, 19);
            this.chkShowDescription.TabIndex = 6;
            this.chkShowDescription.CheckedChanged += new EventHandler(this.chkShowDescription_CheckedChanged);
            this.btnTitleSymbol.Location = new Point(16, 101);
            this.btnTitleSymbol.Name = "btnTitleSymbol";
            this.btnTitleSymbol.Size = new Size(96, 24);
            this.btnTitleSymbol.TabIndex = 5;
            this.btnTitleSymbol.Text = "标题符号...";
            this.btnTitleSymbol.Click += new EventHandler(this.btnTitleSymbol_Click);
            this.chkShowTitle.EditValue = false;
            this.chkShowTitle.Location = new Point(8, 74);
            this.chkShowTitle.Name = "chkShowTitle";
            this.chkShowTitle.Properties.Caption = "显示标题";
            this.chkShowTitle.Size = new Size(88, 19);
            this.chkShowTitle.TabIndex = 4;
            this.chkShowTitle.CheckedChanged += new EventHandler(this.chkShowTitle_CheckedChanged);
            this.btnLabelSymbol.Location = new Point(144, 43);
            this.btnLabelSymbol.Name = "btnLabelSymbol";
            this.btnLabelSymbol.Size = new Size(96, 24);
            this.btnLabelSymbol.TabIndex = 3;
            this.btnLabelSymbol.Text = "标注符号...";
            this.btnLabelSymbol.Click += new EventHandler(this.btnLabelSymbol_Click);
            this.btnLayerNameSymbol.Location = new Point(16, 43);
            this.btnLayerNameSymbol.Name = "btnLayerNameSymbol";
            this.btnLayerNameSymbol.Size = new Size(96, 24);
            this.btnLayerNameSymbol.TabIndex = 2;
            this.btnLayerNameSymbol.Text = "图层名称符号...";
            this.btnLayerNameSymbol.Click += new EventHandler(this.btnLayerNameSymbol_Click);
            this.chkShowLabel.EditValue = false;
            this.chkShowLabel.Location = new Point(144, 21);
            this.chkShowLabel.Name = "chkShowLabel";
            this.chkShowLabel.Properties.Caption = "显示标注";
            this.chkShowLabel.Size = new Size(88, 19);
            this.chkShowLabel.TabIndex = 1;
            this.chkShowLabel.CheckedChanged += new EventHandler(this.chkShowLabel_CheckedChanged);
            this.chkShowLayerName.EditValue = false;
            this.chkShowLayerName.Location = new Point(8, 21);
            this.chkShowLayerName.Name = "chkShowLayerName";
            this.chkShowLayerName.Properties.Caption = "显示图层名";
            this.chkShowLayerName.Size = new Size(88, 19);
            this.chkShowLayerName.TabIndex = 0;
            this.chkShowLayerName.CheckedChanged += new EventHandler(this.chkShowLayerName_CheckedChanged);
            base.Controls.Add(this.groupBox1);
            base.Name = "LegendItemGeneralPropertyPage";
            base.Size = new Size(320, 288);
            base.Load += new EventHandler(this.LegendItemGeneralPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.txtDefaultPatchHeight.Properties.EndInit();
            this.txtDefaultPatchWidth.Properties.EndInit();
            this.chkOveralpDefaultPatchSize.Properties.EndInit();
            this.chkOveralpDefaultPatch.Properties.EndInit();
            this.chkShowDescription.Properties.EndInit();
            this.chkShowTitle.Properties.EndInit();
            this.chkShowLabel.Properties.EndInit();
            this.chkShowLayerName.Properties.EndInit();
            base.ResumeLayout(false);
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

       
        private SimpleButton btnDescriptionSymbol;
        private SimpleButton btnLabelSymbol;
        private SimpleButton btnLayerNameSymbol;
        private SimpleButton btnTitleSymbol;
        private StyleComboBox cboAreaPatches;
        private StyleComboBox cboLinePatches;
        private CheckEdit chkOveralpDefaultPatch;
        private CheckEdit chkOveralpDefaultPatchSize;
        private CheckEdit chkShowDescription;
        private CheckEdit chkShowLabel;
        private CheckEdit chkShowLayerName;
        private CheckEdit chkShowTitle;
        private GroupBox groupBox1;
        private IContainer icontainer_0;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextEdit txtDefaultPatchHeight;
        private TextEdit txtDefaultPatchWidth;
    }
}