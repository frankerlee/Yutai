using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Query.UI;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class FillLabelSetCtrl
    {
        protected override void Dispose(bool bool_3)
        {
            if (bool_3 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_3);
        }

       
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FillLabelSetCtrl));
            this.btnSQL = new SimpleButton();
            this.btnScaleSet = new SimpleButton();
            this.groupBox2 = new GroupBox();
            this.chkPlaceOnlyInsidePolygon = new CheckEdit();
            this.pictureEdit3 = new PictureEdit();
            this.pictureEdit2 = new PictureEdit();
            this.pictureEdit1 = new PictureEdit();
            this.rdoPolygonPlacementMethod = new RadioGroup();
            this.btnLabelExpression = new GroupBox();
            this.btnEditSymbol = new SimpleButton();
            this.symbolItem = new SymbolItem();
            this.groupBox1 = new GroupBox();
            this.cboFields = new ComboBoxEdit();
            this.btnExpress = new SimpleButton();
            this.label1 = new Label();
            this.groupBox2.SuspendLayout();
            this.chkPlaceOnlyInsidePolygon.Properties.BeginInit();
            this.pictureEdit3.Properties.BeginInit();
            this.pictureEdit2.Properties.BeginInit();
            this.pictureEdit1.Properties.BeginInit();
            this.rdoPolygonPlacementMethod.Properties.BeginInit();
            this.btnLabelExpression.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.cboFields.Properties.BeginInit();
            base.SuspendLayout();
            this.btnSQL.Location = new Point(88, 264);
            this.btnSQL.Name = "btnSQL";
            this.btnSQL.Size = new Size(56, 24);
            this.btnSQL.TabIndex = 23;
            this.btnSQL.Text = "SQL查询";
            this.btnSQL.Click += new EventHandler(this.btnSQL_Click);
            this.btnScaleSet.Location = new Point(8, 264);
            this.btnScaleSet.Name = "btnScaleSet";
            this.btnScaleSet.Size = new Size(64, 24);
            this.btnScaleSet.TabIndex = 22;
            this.btnScaleSet.Text = "比例范围";
            this.btnScaleSet.Click += new EventHandler(this.btnScaleSet_Click);
            this.groupBox2.Controls.Add(this.chkPlaceOnlyInsidePolygon);
            this.groupBox2.Controls.Add(this.pictureEdit3);
            this.groupBox2.Controls.Add(this.pictureEdit2);
            this.groupBox2.Controls.Add(this.pictureEdit1);
            this.groupBox2.Controls.Add(this.rdoPolygonPlacementMethod);
            this.groupBox2.Location = new Point(4, 138);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(360, 118);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "放值属性";
            this.chkPlaceOnlyInsidePolygon.Location = new Point(16, 96);
            this.chkPlaceOnlyInsidePolygon.Name = "chkPlaceOnlyInsidePolygon";
            this.chkPlaceOnlyInsidePolygon.Properties.Caption = "只将标注放在多边形内部";
            this.chkPlaceOnlyInsidePolygon.Size = new Size(168, 19);
            this.chkPlaceOnlyInsidePolygon.TabIndex = 18;
            this.chkPlaceOnlyInsidePolygon.CheckedChanged += new EventHandler(this.chkPlaceOnlyInsidePolygon_CheckedChanged);
            this.pictureEdit3.EditValue = resources.GetObject("pictureEdit3.EditValue");
            this.pictureEdit3.Location = new Point(184, 16);
            this.pictureEdit3.Name = "pictureEdit3";
            this.pictureEdit3.Properties.BorderStyle = BorderStyles.NoBorder;
            this.pictureEdit3.Properties.SizeMode = PictureSizeMode.Clip;
            this.pictureEdit3.Size = new Size(74, 81);
            this.pictureEdit3.TabIndex = 17;
            this.pictureEdit3.Visible = false;
            this.pictureEdit2.EditValue = resources.GetObject("pictureEdit2.EditValue");
            this.pictureEdit2.Location = new Point(184, 16);
            this.pictureEdit2.Name = "pictureEdit2";
            this.pictureEdit2.Properties.BorderStyle = BorderStyles.NoBorder;
            this.pictureEdit2.Properties.SizeMode = PictureSizeMode.Clip;
            this.pictureEdit2.Size = new Size(74, 81);
            this.pictureEdit2.TabIndex = 16;
            this.pictureEdit2.Visible = false;
            this.pictureEdit1.EditValue = resources.GetObject("pictureEdit1.EditValue");
            this.pictureEdit1.Location = new Point(184, 16);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.pictureEdit1.Properties.SizeMode = PictureSizeMode.Clip;
            this.pictureEdit1.Size = new Size(74, 81);
            this.pictureEdit1.TabIndex = 15;
            this.pictureEdit1.Visible = false;
            this.rdoPolygonPlacementMethod.Location = new Point(16, 16);
            this.rdoPolygonPlacementMethod.Name = "rdoPolygonPlacementMethod";
            this.rdoPolygonPlacementMethod.Properties.Appearance.BackColor = SystemColors.Control;
            this.rdoPolygonPlacementMethod.Properties.Appearance.Options.UseBackColor = true;
            this.rdoPolygonPlacementMethod.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoPolygonPlacementMethod.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "总是水平"), new RadioGroupItem(null, "总是垂直"), new RadioGroupItem(null, "尽量先水平，然后在垂直") });
            this.rdoPolygonPlacementMethod.Size = new Size(168, 80);
            this.rdoPolygonPlacementMethod.TabIndex = 14;
            this.rdoPolygonPlacementMethod.SelectedIndexChanged += new EventHandler(this.rdoPolygonPlacementMethod_SelectedIndexChanged);
            this.btnLabelExpression.Controls.Add(this.btnEditSymbol);
            this.btnLabelExpression.Controls.Add(this.symbolItem);
            this.btnLabelExpression.Location = new Point(4, 66);
            this.btnLabelExpression.Name = "btnLabelExpression";
            this.btnLabelExpression.Size = new Size(360, 70);
            this.btnLabelExpression.TabIndex = 20;
            this.btnLabelExpression.TabStop = false;
            this.btnLabelExpression.Text = "文本符号";
            this.btnEditSymbol.Location = new Point(232, 24);
            this.btnEditSymbol.Name = "btnEditSymbol";
            this.btnEditSymbol.Size = new Size(64, 24);
            this.btnEditSymbol.TabIndex = 1;
            this.btnEditSymbol.Text = "符号...";
            this.btnEditSymbol.Click += new EventHandler(this.btnEditSymbol_Click);
            this.symbolItem.BackColor = SystemColors.ControlLight;
            this.symbolItem.Location = new Point(16, 24);
            this.symbolItem.Name = "symbolItem";
            this.symbolItem.Size = new Size(168, 40);
            this.symbolItem.Symbol = null;
            this.symbolItem.TabIndex = 0;
            this.groupBox1.Controls.Add(this.cboFields);
            this.groupBox1.Controls.Add(this.btnExpress);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(4, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(360, 64);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "标注表达式";
            this.cboFields.EditValue = "";
            this.cboFields.Location = new Point(80, 24);
            this.cboFields.Name = "cboFields";
            this.cboFields.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFields.Size = new Size(136, 23);
            this.cboFields.TabIndex = 5;
            this.cboFields.SelectedIndexChanged += new EventHandler(this.cboFields_SelectedIndexChanged);
            this.btnExpress.Location = new Point(240, 24);
            this.btnExpress.Name = "btnExpress";
            this.btnExpress.Size = new Size(64, 24);
            this.btnExpress.TabIndex = 4;
            this.btnExpress.Text = "表达式...";
            this.btnExpress.Click += new EventHandler(this.btnExpress_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 26);
            this.label1.Name = "label1";
            this.label1.Size = new Size(54, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "标注字段";
            base.Controls.Add(this.btnSQL);
            base.Controls.Add(this.btnScaleSet);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.btnLabelExpression);
            base.Controls.Add(this.groupBox1);
            base.Name = "FillLabelSetCtrl";
            base.Size = new Size(384, 296);
            base.Load += new EventHandler(this.FillLabelSetCtrl_Load);
            this.groupBox2.ResumeLayout(false);
            this.chkPlaceOnlyInsidePolygon.Properties.EndInit();
            this.pictureEdit3.Properties.EndInit();
            this.pictureEdit2.Properties.EndInit();
            this.pictureEdit1.Properties.EndInit();
            this.rdoPolygonPlacementMethod.Properties.EndInit();
            this.btnLabelExpression.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.cboFields.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnEditSymbol;
        private SimpleButton btnExpress;
        private GroupBox btnLabelExpression;
        private SimpleButton btnScaleSet;
        private SimpleButton btnSQL;
        private ComboBoxEdit cboFields;
        private CheckEdit chkPlaceOnlyInsidePolygon;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private PictureEdit pictureEdit1;
        private PictureEdit pictureEdit2;
        private PictureEdit pictureEdit3;
        private RadioGroup rdoPolygonPlacementMethod;
        private SymbolItem symbolItem;
    }
}