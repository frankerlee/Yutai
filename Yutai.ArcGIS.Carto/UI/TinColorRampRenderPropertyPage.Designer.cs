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
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.Shared;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class TinColorRampRenderPropertyPage
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
            this.icontainer_0 = new Container();
            this.colorRampComboBox1 = new StyleComboBox(this.icontainer_0);
            this.groupBox1 = new GroupBox();
            this.lblLabelInfo = new Label();
            this.label3 = new Label();
            this.Classifygroup = new GroupBox();
            this.cboClassifyNum = new ComboBoxEdit();
            this.cboClassifyMethod = new ComboBoxEdit();
            this.label4 = new Label();
            this.label5 = new Label();
            this.listView1 = new RenderInfoListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.columnHeader_2 = new ColumnHeader();
            this.groupBox1.SuspendLayout();
            this.Classifygroup.SuspendLayout();
            this.cboClassifyNum.Properties.BeginInit();
            this.cboClassifyMethod.Properties.BeginInit();
            base.SuspendLayout();
            this.colorRampComboBox1.DrawMode = DrawMode.OwnerDrawVariable;
            this.colorRampComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.colorRampComboBox1.DropDownWidth = 160;
            this.colorRampComboBox1.Location = new Point(62, 50);
            this.colorRampComboBox1.Name = "colorRampComboBox1";
            this.colorRampComboBox1.Size = new Size(135, 22);
            this.colorRampComboBox1.TabIndex = 14;
            this.colorRampComboBox1.SelectedIndexChanged += new EventHandler(this.colorRampComboBox1_SelectedIndexChanged);
            this.groupBox1.Controls.Add(this.lblLabelInfo);
            this.groupBox1.Location = new Point(3, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(194, 43);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "值字段";
            this.lblLabelInfo.AutoSize = true;
            this.lblLabelInfo.Location = new Point(31, 20);
            this.lblLabelInfo.Name = "lblLabelInfo";
            this.lblLabelInfo.Size = new Size(0, 12);
            this.lblLabelInfo.TabIndex = 0;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(3, 54);
            this.label3.Name = "label3";
            this.label3.Size = new Size(53, 12);
            this.label3.TabIndex = 36;
            this.label3.Text = "颜色模型";
            this.Classifygroup.Controls.Add(this.cboClassifyNum);
            this.Classifygroup.Controls.Add(this.cboClassifyMethod);
            this.Classifygroup.Controls.Add(this.label4);
            this.Classifygroup.Controls.Add(this.label5);
            this.Classifygroup.Location = new Point(203, 1);
            this.Classifygroup.Name = "Classifygroup";
            this.Classifygroup.Size = new Size(192, 74);
            this.Classifygroup.TabIndex = 45;
            this.Classifygroup.TabStop = false;
            this.Classifygroup.Text = "分类";
            this.cboClassifyNum.EditValue = "";
            this.cboClassifyNum.Location = new Point(80, 43);
            this.cboClassifyNum.Name = "cboClassifyNum";
            this.cboClassifyNum.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboClassifyNum.Properties.Items.AddRange(new object[] { 
                "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", 
                "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32"
             });
            this.cboClassifyNum.Size = new Size(104, 21);
            this.cboClassifyNum.TabIndex = 49;
            this.cboClassifyNum.SelectedIndexChanged += new EventHandler(this.cboClassifyNum_SelectedIndexChanged);
            this.cboClassifyMethod.EditValue = "";
            this.cboClassifyMethod.Location = new Point(80, 18);
            this.cboClassifyMethod.Name = "cboClassifyMethod";
            this.cboClassifyMethod.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboClassifyMethod.Properties.Items.AddRange(new object[] { "手动", "等间隔" });
            this.cboClassifyMethod.Size = new Size(104, 21);
            this.cboClassifyMethod.TabIndex = 48;
            this.cboClassifyMethod.SelectedIndexChanged += new EventHandler(this.cboClassifyMethod_SelectedIndexChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(8, 43);
            this.label4.Name = "label4";
            this.label4.Size = new Size(17, 12);
            this.label4.TabIndex = 39;
            this.label4.Text = "类";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(8, 18);
            this.label5.Name = "label5";
            this.label5.Size = new Size(53, 12);
            this.label5.TabIndex = 38;
            this.label5.Text = "分类方法";
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1, this.columnHeader_2 });
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new Point(8, 88);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(379, 128);
            this.listView1.TabIndex = 44;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.columnHeader_0.Text = "符号";
            this.columnHeader_0.Width = 76;
            this.columnHeader_1.Text = "范围";
            this.columnHeader_1.Width = 125;
            this.columnHeader_2.Text = "标注";
            this.columnHeader_2.Width = 150;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.Classifygroup);
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.colorRampComboBox1);
            base.Controls.Add(this.groupBox1);
            base.Name = "TinColorRampRenderPropertyPage";
            base.Size = new Size(401, 251);
            base.Load += new EventHandler(this.TinColorRampRenderPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.Classifygroup.ResumeLayout(false);
            this.Classifygroup.PerformLayout();
            this.cboClassifyNum.Properties.EndInit();
            this.cboClassifyMethod.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private ComboBoxEdit cboClassifyMethod;
        private ComboBoxEdit cboClassifyNum;
        private GroupBox Classifygroup;
        private StyleComboBox colorRampComboBox1;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ColumnHeader columnHeader_2;
        private GroupBox groupBox1;
        private ITinColorRampRenderer itinColorRampRenderer_0;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label lblLabelInfo;
        private RenderInfoListView listView1;
    }
}