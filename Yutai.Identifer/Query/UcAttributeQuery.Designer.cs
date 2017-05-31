using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.Plugins.Identifer.Query
{
    partial class UcAttributeQuery
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            this.panel2 = new Panel();
            this.btnClear = new Button();
            this.panel1 = new Panel();
            this.cboSelectType = new ComboBox();
            this.chkShowSelectbaleLayer = new CheckBox();
            this.label3 = new Label();
            this.comboBoxLayer = new ComboBox();
            this.label1 = new Label();
            this.btnClose = new Button();
            this.btnApply = new Button();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.panel2.Dock = DockStyle.Top;
            this.panel2.Location = new Point(0, 88);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(376, 360);
            this.panel2.TabIndex = 57;
            this.btnClear.Location = new Point(8, 456);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new Size(56, 24);
            this.btnClear.TabIndex = 56;
            this.btnClear.Text = "清除";
            this.btnClear.Click += new EventHandler(this.btnClear_Click);
            this.panel1.Controls.Add(this.cboSelectType);
            this.panel1.Controls.Add(this.chkShowSelectbaleLayer);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.comboBoxLayer);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(376, 88);
            this.panel1.TabIndex = 55;
            this.cboSelectType.Text = "创建一个新的选择集";
            this.cboSelectType.Location = new Point(56, 56);
            this.cboSelectType.Name = "cboSelectType";

            cboSelectType.Items.AddRange(new object[] { "创建一个新的选择集", "添加到现有选择集", "从现有选择集中删除", "从现有选择集中选择" });
           
            this.cboSelectType.Size = new Size(296, 23);
            this.cboSelectType.TabIndex = 51;
            this.cboSelectType.SelectedIndexChanged += new EventHandler(this.cboSelectType_SelectedIndexChanged);
            this.chkShowSelectbaleLayer.Location = new Point(56, 32);
            this.chkShowSelectbaleLayer.Name = "chkShowSelectbaleLayer";
            this.chkShowSelectbaleLayer.Text = "只显示可选择图层";
            this.chkShowSelectbaleLayer.Size = new Size(168, 19);
            this.chkShowSelectbaleLayer.TabIndex = 50;
            this.chkShowSelectbaleLayer.CheckedChanged += new EventHandler(this.chkShowSelectbaleLayer_CheckedChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 56);
            this.label3.Name = "label3";
            this.label3.Size = new Size(35, 17);
            this.label3.TabIndex = 49;
            this.label3.Text = "方法:";
            this.comboBoxLayer.Text = "";
            this.comboBoxLayer.Location = new Point(56, 7);
            this.comboBoxLayer.Name = "comboBoxLayer";
            
            this.comboBoxLayer.Size = new Size(288, 23);
            this.comboBoxLayer.TabIndex = 48;
            this.comboBoxLayer.SelectedIndexChanged += new EventHandler(this.comboBoxLayer_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(35, 17);
            this.label1.TabIndex = 47;
            this.label1.Text = "图层:";
            this.btnClose.DialogResult = DialogResult.Cancel;
            this.btnClose.Location = new Point(296, 456);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(56, 24);
            this.btnClose.TabIndex = 54;
            this.btnClose.Text = "关闭";
            this.btnApply.Location = new Point(232, 456);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new Size(56, 24);
            this.btnApply.TabIndex = 53;
            this.btnApply.Text = "应用";
            this.btnApply.Click += new EventHandler(this.btnApply_Click);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.btnClear);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.btnClose);
            base.Controls.Add(this.btnApply);
            base.Name = "AttributeQueryControl";
            base.Size = new Size(376, 488);
            base.Load += new EventHandler(this.AttributeQueryControl_Load);
            this.panel1.ResumeLayout(false);
        
            base.ResumeLayout(false);
        }

        #endregion

        private UcAttributeQueryBuilder attributeQueryBuliderControl = new UcAttributeQueryBuilder();

        private Panel panel2;

        private Button btnClear;

        private Panel panel1;

        private ComboBox cboSelectType;

        private CheckBox chkShowSelectbaleLayer;

        private Label label3;

        private ComboBox comboBoxLayer;

        private Label label1;

        private Button btnClose;

        private Button btnApply;

    }
}
