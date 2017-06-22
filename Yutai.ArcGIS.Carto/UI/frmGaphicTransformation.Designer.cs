using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.ControlExtend;
using Yutai.ArcGIS.Common.Wrapper;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class frmGaphicTransformation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGaphicTransformation));
            this.label1 = new Label();
            this.txtName = new TextBox();
            this.txtSourGCS = new TextBox();
            this.label2 = new Label();
            this.label3 = new Label();
            this.cboTargetGCS = new ComboBox();
            this.groupBox1 = new GroupBox();
            this.paramlistView = new EditListView();
            this.lvcolumnHeader_0 = new LVColumnHeader();
            this.lvcolumnHeader_1 = new LVColumnHeader();
            this.label5 = new Label();
            this.cboHCSTransformMethod = new ComboBox();
            this.label4 = new Label();
            this.btnOK = new Button();
            this.button2 = new Button();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称";
            this.txtName.Location = new System.Drawing.Point(95, 14);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(145, 21);
            this.txtName.TabIndex = 1;
            this.txtSourGCS.Enabled = false;
            this.txtSourGCS.Location = new System.Drawing.Point(95, 41);
            this.txtSourGCS.Name = "txtSourGCS";
            this.txtSourGCS.Size = new Size(145, 21);
            this.txtSourGCS.TabIndex = 3;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 44);
            this.label2.Name = "label2";
            this.label2.Size = new Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "源数据框架";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 74);
            this.label3.Name = "label3";
            this.label3.Size = new Size(77, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "目的数据框架";
            this.cboTargetGCS.FormattingEnabled = true;
            this.cboTargetGCS.Items.AddRange(new object[] { "北京54", "西安80", "WGS_84" });
            this.cboTargetGCS.Location = new System.Drawing.Point(95, 74);
            this.cboTargetGCS.Name = "cboTargetGCS";
            this.cboTargetGCS.Size = new Size(155, 20);
            this.cboTargetGCS.TabIndex = 5;
            this.cboTargetGCS.SelectedIndexChanged += new EventHandler(this.cboTargetGCS_SelectedIndexChanged);
            this.groupBox1.Controls.Add(this.paramlistView);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cboHCSTransformMethod);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(2, 106);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(277, 216);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "转换方法";
            this.paramlistView.Columns.AddRange(new ColumnHeader[] { this.lvcolumnHeader_0, this.lvcolumnHeader_1 });
            this.paramlistView.ComboBoxBgColor = Color.LightBlue;
            this.paramlistView.ComboBoxFont = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
            this.paramlistView.EditBgColor = Color.LightBlue;
            this.paramlistView.EditFont = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
            this.paramlistView.FullRowSelect = true;
            this.paramlistView.GridLines = true;
            this.paramlistView.Location = new System.Drawing.Point(10, 77);
            this.paramlistView.LockRowCount = 0;
            this.paramlistView.Name = "paramlistView";
            this.paramlistView.Size = new Size(246, 124);
            this.paramlistView.TabIndex = 9;
            this.paramlistView.UseCompatibleStateImageBehavior = false;
            this.paramlistView.View = View.Details;
            this.lvcolumnHeader_0.ColumnStyle = ListViewColumnStyle.ReadOnly;
            this.lvcolumnHeader_0.Text = "名称";
            this.lvcolumnHeader_0.Width = 108;
            this.lvcolumnHeader_1.ColumnStyle = ListViewColumnStyle.EditBox;
            this.lvcolumnHeader_1.Text = "值";
            this.lvcolumnHeader_1.Width = 122;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 55);
            this.label5.Name = "label5";
            this.label5.Size = new Size(29, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "参数";
            this.cboHCSTransformMethod.FormattingEnabled = true;
            this.cboHCSTransformMethod.Location = new System.Drawing.Point(68, 25);
            this.cboHCSTransformMethod.Name = "cboHCSTransformMethod";
            this.cboHCSTransformMethod.Size = new Size(155, 20);
            this.cboHCSTransformMethod.TabIndex = 7;
            this.cboHCSTransformMethod.SelectedIndexChanged += new EventHandler(this.cboHCSTransformMethod_SelectedIndexChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 28);
            this.label4.Name = "label4";
            this.label4.Size = new Size(29, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "名称";
            this.btnOK.Location = new System.Drawing.Point(134, 328);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(51, 23);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(201, 329);
            this.button2.Name = "button2";
            this.button2.Size = new Size(52, 20);
            this.button2.TabIndex = 8;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(300, 360);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.cboTargetGCS);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.txtSourGCS);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.txtName);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmGaphicTransformation";
            this.Text = "新建地理坐标转换";
            base.Load += new EventHandler(this.frmGaphicTransformation_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private Button btnOK;
        private Button button2;
        private ComboBox cboHCSTransformMethod;
        private ComboBox cboTargetGCS;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private LVColumnHeader lvcolumnHeader_0;
        private LVColumnHeader lvcolumnHeader_1;
        private EditListView paramlistView;
        private TextBox txtName;
        private TextBox txtSourGCS;
    }
}