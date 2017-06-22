using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Wrapper;
using ItemCheckEventArgs = System.Windows.Forms.ItemCheckEventArgs;
using ItemCheckEventHandler = System.Windows.Forms.ItemCheckEventHandler;

namespace Yutai.ArcGIS.Common.Query.UI
{
    partial class SelectByLocationCtrl
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
            this.label1 = new Label();
            this.cboOperationType = new ComboBoxEdit();
            this.checkedListBoxLayer = new CheckedListBox();
            this.label2 = new Label();
            this.chkUsetSelectedLayer = new CheckEdit();
            this.cboSpatialRelation = new ComboBoxEdit();
            this.label3 = new Label();
            this.cboSourceLayer = new ComboBoxEdit();
            this.chkUseSelectFeature = new CheckEdit();
            this.chkUseBuffer = new CheckEdit();
            this.txtRadius = new TextEdit();
            this.cboUnit = new ComboBoxEdit();
            this.btnApply = new SimpleButton();
            this.panel1 = new Panel();
            this.lblStatu = new Label();
            this.cboOperationType.Properties.BeginInit();
            this.chkUsetSelectedLayer.Properties.BeginInit();
            this.cboSpatialRelation.Properties.BeginInit();
            this.cboSourceLayer.Properties.BeginInit();
            this.chkUseSelectFeature.Properties.BeginInit();
            this.chkUseBuffer.Properties.BeginInit();
            this.txtRadius.Properties.BeginInit();
            this.cboUnit.Properties.BeginInit();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(209, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "根据要素的空间关系从图层中选择要素";
            this.cboOperationType.EditValue = "选择要素";
            this.cboOperationType.Location = new System.Drawing.Point(8, 168);
            this.cboOperationType.Name = "cboOperationType";
            this.cboOperationType.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboOperationType.Properties.Items.AddRange(new object[] { "选择要素", "选择要素,并添加到选择集中", "的选择集中删除", "的选择集中选择" });
            this.cboOperationType.Size = new Size(248, 21);
            this.cboOperationType.TabIndex = 1;
            this.checkedListBoxLayer.Location = new System.Drawing.Point(8, 56);
            this.checkedListBoxLayer.Name = "checkedListBoxLayer";
            this.checkedListBoxLayer.Size = new Size(248, 100);
            this.checkedListBoxLayer.TabIndex = 2;
            this.checkedListBoxLayer.ItemCheck += new ItemCheckEventHandler(this.checkedListBoxLayer_ItemCheck);
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 32);
            this.label2.Name = "label2";
            this.label2.Size = new Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "从下列图层";
            this.chkUsetSelectedLayer.Location = new System.Drawing.Point(96, 32);
            this.chkUsetSelectedLayer.Name = "chkUsetSelectedLayer";
            this.chkUsetSelectedLayer.Properties.Caption = "只显示可选图层";
            this.chkUsetSelectedLayer.Size = new Size(112, 19);
            this.chkUsetSelectedLayer.TabIndex = 4;
            this.chkUsetSelectedLayer.Visible = false;
            this.chkUsetSelectedLayer.CheckedChanged += new EventHandler(this.chkUsetSelectedLayer_CheckedChanged);
            this.cboSpatialRelation.EditValue = "相交";
            this.cboSpatialRelation.Location = new System.Drawing.Point(8, 248);
            this.cboSpatialRelation.Name = "cboSpatialRelation";
            this.cboSpatialRelation.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboSpatialRelation.Properties.Items.AddRange(new object[] { "相交", "包围矩形相交", "相接", "重叠", "被包含", "包含" });
            this.cboSpatialRelation.Size = new Size(248, 21);
            this.cboSpatialRelation.TabIndex = 5;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 200);
            this.label3.Name = "label3";
            this.label3.Size = new Size(89, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "与图层中的要素";
            this.cboSourceLayer.EditValue = "";
            this.cboSourceLayer.Location = new System.Drawing.Point(8, 216);
            this.cboSourceLayer.Name = "cboSourceLayer";
            this.cboSourceLayer.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboSourceLayer.Size = new Size(144, 21);
            this.cboSourceLayer.TabIndex = 7;
            this.cboSourceLayer.SelectedIndexChanged += new EventHandler(this.cboSourceLayer_SelectedIndexChanged);
            this.chkUseSelectFeature.Location = new System.Drawing.Point(160, 216);
            this.chkUseSelectFeature.Name = "chkUseSelectFeature";
            this.chkUseSelectFeature.Properties.Caption = "使用选中的要素";
            this.chkUseSelectFeature.Size = new Size(104, 19);
            this.chkUseSelectFeature.TabIndex = 8;
            this.chkUseBuffer.Location = new System.Drawing.Point(8, 280);
            this.chkUseBuffer.Name = "chkUseBuffer";
            this.chkUseBuffer.Properties.Caption = "对要素进行缓冲区操作";
            this.chkUseBuffer.Size = new Size(168, 19);
            this.chkUseBuffer.TabIndex = 9;
            this.chkUseBuffer.CheckedChanged += new EventHandler(this.chkUseBuffer_CheckedChanged);
            this.txtRadius.EditValue = "0";
            this.txtRadius.Location = new System.Drawing.Point(8, 304);
            this.txtRadius.Name = "txtRadius";
            this.txtRadius.Size = new Size(88, 21);
            this.txtRadius.TabIndex = 10;
            this.cboUnit.EditValue = "";
            this.cboUnit.Location = new System.Drawing.Point(104, 304);
            this.cboUnit.Name = "cboUnit";
            this.cboUnit.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboUnit.Size = new Size(96, 21);
            this.cboUnit.TabIndex = 11;
            this.cboUnit.Visible = false;
            this.btnApply.Location = new System.Drawing.Point(144, 336);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new Size(64, 24);
            this.btnApply.TabIndex = 12;
            this.btnApply.Text = "应用";
            this.btnApply.Click += new EventHandler(this.btnApply_Click);
            this.panel1.Controls.Add(this.lblStatu);
            this.panel1.Location = new System.Drawing.Point(40, 160);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(152, 64);
            this.panel1.TabIndex = 14;
            this.panel1.Visible = false;
            this.lblStatu.AutoSize = true;
            this.lblStatu.Location = new System.Drawing.Point(8, 8);
            this.lblStatu.Name = "lblStatu";
            this.lblStatu.Size = new Size(107, 12);
            this.lblStatu.TabIndex = 14;
            this.lblStatu.Text = "正在查找请稍候...";
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.btnApply);
            base.Controls.Add(this.cboUnit);
            base.Controls.Add(this.txtRadius);
            base.Controls.Add(this.chkUseBuffer);
            base.Controls.Add(this.chkUseSelectFeature);
            base.Controls.Add(this.cboSourceLayer);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.cboSpatialRelation);
            base.Controls.Add(this.chkUsetSelectedLayer);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.checkedListBoxLayer);
            base.Controls.Add(this.cboOperationType);
            base.Controls.Add(this.label1);
            base.Name = "SelectByLocationCtrl";
            base.Size = new Size(280, 384);
            base.Load += new EventHandler(this.SelectByLocationCtrl_Load);
            this.cboOperationType.Properties.EndInit();
            this.chkUsetSelectedLayer.Properties.EndInit();
            this.cboSpatialRelation.Properties.EndInit();
            this.cboSourceLayer.Properties.EndInit();
            this.chkUseSelectFeature.Properties.EndInit();
            this.chkUseBuffer.Properties.EndInit();
            this.txtRadius.Properties.EndInit();
            this.cboUnit.Properties.EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private SimpleButton btnApply;
        private ComboBoxEdit cboOperationType;
        private ComboBoxEdit cboSourceLayer;
        private ComboBoxEdit cboSpatialRelation;
        private ComboBoxEdit cboUnit;
        private CheckedListBox checkedListBoxLayer;
        private CheckEdit chkUseBuffer;
        private CheckEdit chkUseSelectFeature;
        private CheckEdit chkUsetSelectedLayer;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label lblStatu;
        private Panel panel1;
        private TextEdit txtRadius;
    }
}