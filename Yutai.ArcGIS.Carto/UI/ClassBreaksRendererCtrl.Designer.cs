using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.Shared;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class ClassBreaksRendererCtrl
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
            this.colorRampComboBox1 = new ColorRampComboBox();
            this.groupBox1 = new GroupBox();
            this.cboNormFields = new ComboBoxEdit();
            this.cboValueFields = new ComboBoxEdit();
            this.label2 = new Label();
            this.label1 = new Label();
            this.listView1 = new RenderInfoListView();
            this.columnHeader_2 = new ColumnHeader();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.label3 = new Label();
            this.Classifygroup = new GroupBox();
            this.cboClassifyNum = new ComboBoxEdit();
            this.cboClassifyMethod = new ComboBoxEdit();
            this.label4 = new Label();
            this.label5 = new Label();
            this.groupBox1.SuspendLayout();
            this.cboNormFields.Properties.BeginInit();
            this.cboValueFields.Properties.BeginInit();
            this.Classifygroup.SuspendLayout();
            this.cboClassifyNum.Properties.BeginInit();
            this.cboClassifyMethod.Properties.BeginInit();
            base.SuspendLayout();
            this.colorRampComboBox1.DrawMode = DrawMode.OwnerDrawVariable;
            this.colorRampComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.colorRampComboBox1.Location = new System.Drawing.Point(80, 104);
            this.colorRampComboBox1.Name = "colorRampComboBox1";
            this.colorRampComboBox1.Size = new Size(136, 22);
            this.colorRampComboBox1.TabIndex = 36;
            this.colorRampComboBox1.SelectedIndexChanged += new EventHandler(this.colorRampComboBox1_SelectedIndexChanged);
            this.groupBox1.Controls.Add(this.cboNormFields);
            this.groupBox1.Controls.Add(this.cboValueFields);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(192, 88);
            this.groupBox1.TabIndex = 41;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "字段";
            this.cboNormFields.EditValue = "";
            this.cboNormFields.Location = new System.Drawing.Point(80, 56);
            this.cboNormFields.Name = "cboNormFields";
            this.cboNormFields.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboNormFields.Size = new Size(104, 23);
            this.cboNormFields.TabIndex = 46;
            this.cboNormFields.SelectedIndexChanged += new EventHandler(this.cboNormFields_SelectedIndexChanged);
            this.cboValueFields.EditValue = "";
            this.cboValueFields.Location = new System.Drawing.Point(80, 24);
            this.cboValueFields.Name = "cboValueFields";
            this.cboValueFields.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboValueFields.Size = new Size(104, 23);
            this.cboValueFields.TabIndex = 45;
            this.cboValueFields.SelectedIndexChanged += new EventHandler(this.cboValueFields_SelectedIndexChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 56);
            this.label2.Name = "label2";
            this.label2.Size = new Size(66, 17);
            this.label2.TabIndex = 39;
            this.label2.Text = "正则化字段";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 24);
            this.label1.Name = "label1";
            this.label1.Size = new Size(42, 17);
            this.label1.TabIndex = 38;
            this.label1.Text = "值字段";
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_2, this.columnHeader_0, this.columnHeader_1 });
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(8, 136);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(320, 128);
            this.listView1.TabIndex = 42;
            this.listView1.View = View.Details;
            this.columnHeader_2.Text = "符号";
            this.columnHeader_0.Text = "范围";
            this.columnHeader_0.Width = 97;
            this.columnHeader_1.Text = "标注";
            this.columnHeader_1.Width = 150;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 104);
            this.label3.Name = "label3";
            this.label3.Size = new Size(54, 17);
            this.label3.TabIndex = 35;
            this.label3.Text = "颜色模型";
            this.Classifygroup.Controls.Add(this.cboClassifyNum);
            this.Classifygroup.Controls.Add(this.cboClassifyMethod);
            this.Classifygroup.Controls.Add(this.label4);
            this.Classifygroup.Controls.Add(this.label5);
            this.Classifygroup.Location = new System.Drawing.Point(208, 8);
            this.Classifygroup.Name = "Classifygroup";
            this.Classifygroup.Size = new Size(192, 88);
            this.Classifygroup.TabIndex = 43;
            this.Classifygroup.TabStop = false;
            this.Classifygroup.Text = "分类";
            this.cboClassifyNum.EditValue = "";
            this.cboClassifyNum.Location = new System.Drawing.Point(80, 56);
            this.cboClassifyNum.Name = "cboClassifyNum";
            this.cboClassifyNum.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboClassifyNum.Properties.Items.AddRange(new object[] { 
                "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", 
                "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32"
             });
            this.cboClassifyNum.Size = new Size(104, 23);
            this.cboClassifyNum.TabIndex = 49;
            this.cboClassifyNum.SelectedIndexChanged += new EventHandler(this.cboClassifyNum_SelectedIndexChanged);
            this.cboClassifyMethod.EditValue = "";
            this.cboClassifyMethod.Location = new System.Drawing.Point(80, 24);
            this.cboClassifyMethod.Name = "cboClassifyMethod";
            this.cboClassifyMethod.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboClassifyMethod.Properties.Items.AddRange(new object[] { "等间隔", "分位数", "自然间隔" });
            this.cboClassifyMethod.Size = new Size(104, 23);
            this.cboClassifyMethod.TabIndex = 48;
            this.cboClassifyMethod.SelectedIndexChanged += new EventHandler(this.cboClassifyMethod_SelectedIndexChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 56);
            this.label4.Name = "label4";
            this.label4.Size = new Size(17, 17);
            this.label4.TabIndex = 39;
            this.label4.Text = "类";
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 24);
            this.label5.Name = "label5";
            this.label5.Size = new Size(54, 17);
            this.label5.TabIndex = 38;
            this.label5.Text = "分类方法";
            base.Controls.Add(this.Classifygroup);
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.colorRampComboBox1);
            base.Controls.Add(this.label3);
            base.Name = "ClassBreaksRendererCtrl";
            base.Size = new Size(424, 280);
            base.Load += new EventHandler(this.ClassBreaksRendererCtrl_Load);
            this.groupBox1.ResumeLayout(false);
            this.cboNormFields.Properties.EndInit();
            this.cboValueFields.Properties.EndInit();
            this.Classifygroup.ResumeLayout(false);
            this.cboClassifyNum.Properties.EndInit();
            this.cboClassifyMethod.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private ComboBoxEdit cboClassifyMethod;
        private ComboBoxEdit cboClassifyNum;
        private ComboBoxEdit cboNormFields;
        private ComboBoxEdit cboValueFields;
        private GroupBox Classifygroup;
        private ColorRampComboBox colorRampComboBox1;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ColumnHeader columnHeader_2;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private RenderInfoListView listView1;
    }
}