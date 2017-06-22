using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class MapGeneralInfoCtrl
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
            this.label1 = new Label();
            this.txtName = new TextEdit();
            this.label2 = new Label();
            this.txtDes = new MemoEdit();
            this.groupBox1 = new GroupBox();
            this.cboDisplayUnit = new ComboBoxEdit();
            this.label4 = new Label();
            this.cboMapUnit = new ComboBoxEdit();
            this.label3 = new Label();
            this.txtRefrenceScale = new TextEdit();
            this.label5 = new Label();
            this.txtRotate = new TextEdit();
            this.label6 = new Label();
            this.label7 = new Label();
            this.cboLabelEngine = new ComboBoxEdit();
            this.txtName.Properties.BeginInit();
            this.txtDes.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            this.cboDisplayUnit.Properties.BeginInit();
            this.cboMapUnit.Properties.BeginInit();
            this.txtRefrenceScale.Properties.BeginInit();
            this.txtRotate.Properties.BeginInit();
            this.cboLabelEngine.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(35, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称:";
            this.txtName.EditValue = "";
            this.txtName.Location = new System.Drawing.Point(56, 8);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(208, 23);
            this.txtName.TabIndex = 1;
            this.txtName.EditValueChanged += new EventHandler(this.txtName_EditValueChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 40);
            this.label2.Name = "label2";
            this.label2.Size = new Size(29, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "描述";
            this.txtDes.EditValue = "";
            this.txtDes.Location = new System.Drawing.Point(16, 64);
            this.txtDes.Name = "txtDes";
            this.txtDes.Size = new Size(248, 120);
            this.txtDes.TabIndex = 3;
            this.txtDes.EditValueChanged += new EventHandler(this.txtDes_EditValueChanged);
            this.groupBox1.Controls.Add(this.cboDisplayUnit);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cboMapUnit);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(16, 192);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(248, 80);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "单位";
            this.cboDisplayUnit.EditValue = "未知单位";
            this.cboDisplayUnit.Location = new System.Drawing.Point(64, 48);
            this.cboDisplayUnit.Name = "cboDisplayUnit";
            this.cboDisplayUnit.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboDisplayUnit.Properties.Items.AddRange(new object[] { "未知单位", "英寸", "点", "英尺", "码", "英里", "海里", "毫米", "厘米", "米", "公里", "度", "分米" });
            this.cboDisplayUnit.Size = new Size(168, 23);
            this.cboDisplayUnit.TabIndex = 4;
            this.cboDisplayUnit.SelectedIndexChanged += new EventHandler(this.cboDisplayUnit_SelectedIndexChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 50);
            this.label4.Name = "label4";
            this.label4.Size = new Size(35, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "显示:";
            this.cboMapUnit.EditValue = "未知单位";
            this.cboMapUnit.Location = new System.Drawing.Point(64, 20);
            this.cboMapUnit.Name = "cboMapUnit";
            this.cboMapUnit.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboMapUnit.Properties.Items.AddRange(new object[] { "未知单位", "英寸", "点", "英尺", "码", "英里", "海里", "毫米", "厘米", "米", "公里", "度", "分米" });
            this.cboMapUnit.Size = new Size(168, 23);
            this.cboMapUnit.TabIndex = 2;
            this.cboMapUnit.SelectedIndexChanged += new EventHandler(this.cboMapUnit_SelectedIndexChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 24);
            this.label3.Name = "label3";
            this.label3.Size = new Size(35, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "地图:";
            this.txtRefrenceScale.EditValue = "";
            this.txtRefrenceScale.Location = new System.Drawing.Point(80, 280);
            this.txtRefrenceScale.Name = "txtRefrenceScale";
            this.txtRefrenceScale.Size = new Size(184, 23);
            this.txtRefrenceScale.TabIndex = 6;
            this.txtRefrenceScale.EditValueChanged += new EventHandler(this.txtRefrenceScale_EditValueChanged);
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 280);
            this.label5.Name = "label5";
            this.label5.Size = new Size(60, 17);
            this.label5.TabIndex = 5;
            this.label5.Text = "参考比例:";
            this.txtRotate.EditValue = "";
            this.txtRotate.Location = new System.Drawing.Point(80, 304);
            this.txtRotate.Name = "txtRotate";
            this.txtRotate.Size = new Size(184, 23);
            this.txtRotate.TabIndex = 8;
            this.txtRotate.EditValueChanged += new EventHandler(this.txtRotate_EditValueChanged);
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 304);
            this.label6.Name = "label6";
            this.label6.Size = new Size(35, 17);
            this.label6.TabIndex = 7;
            this.label6.Text = "旋转:";
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 336);
            this.label7.Name = "label7";
            this.label7.Size = new Size(60, 17);
            this.label7.TabIndex = 9;
            this.label7.Text = "标注引擎:";
            this.cboLabelEngine.EditValue = "ESRI标准标注引擎";
            this.cboLabelEngine.Location = new System.Drawing.Point(80, 336);
            this.cboLabelEngine.Name = "cboLabelEngine";
            this.cboLabelEngine.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboLabelEngine.Properties.Items.AddRange(new object[] { "ESRI标准标注引擎" });
            this.cboLabelEngine.Size = new Size(184, 23);
            this.cboLabelEngine.TabIndex = 10;
            base.Controls.Add(this.cboLabelEngine);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.txtRotate);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.txtRefrenceScale);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.txtDes);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.txtName);
            base.Controls.Add(this.label1);
            base.Name = "MapGeneralInfoCtrl";
            base.Size = new Size(288, 368);
            base.Load += new EventHandler(this.MapGeneralInfoCtrl_Load);
            this.txtName.Properties.EndInit();
            this.txtDes.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.cboDisplayUnit.Properties.EndInit();
            this.cboMapUnit.Properties.EndInit();
            this.txtRefrenceScale.Properties.EndInit();
            this.txtRotate.Properties.EndInit();
            this.cboLabelEngine.Properties.EndInit();
            base.ResumeLayout(false);
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

       
        private ComboBoxEdit cboDisplayUnit;
        private ComboBoxEdit cboLabelEngine;
        private ComboBoxEdit cboMapUnit;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private MemoEdit txtDes;
        private TextEdit txtName;
        private TextEdit txtRefrenceScale;
        private TextEdit txtRotate;
    }
}