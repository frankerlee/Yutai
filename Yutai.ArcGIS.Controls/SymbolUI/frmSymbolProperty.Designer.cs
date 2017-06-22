using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    partial class frmSymbolProperty
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

       
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSymbolProperty));
            this.btnOK = new SimpleButton();
            this.btnCancle = new SimpleButton();
            this.groupBox1 = new GroupBox();
            this.symbolItem1 = new SymbolItem();
            this.groupBox2 = new GroupBox();
            this.txtCategory = new TextEdit();
            this.txtName = new TextEdit();
            this.label2 = new Label();
            this.label1 = new Label();
            this.btnChangeSymbol = new SimpleButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.txtCategory.Properties.BeginInit();
            this.txtName.Properties.BeginInit();
            base.SuspendLayout();
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new Point(64, 232);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(48, 24);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.tnOK_Click);
            this.btnCancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancle.Location = new Point(120, 232);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new Size(48, 24);
            this.btnCancle.TabIndex = 7;
            this.btnCancle.Text = "取消";
            this.btnCancle.Click += new EventHandler(this.btnCancle_Click);
            this.groupBox1.Controls.Add(this.symbolItem1);
            this.groupBox1.Location = new Point(8, 104);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(168, 120);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "样式";
            this.symbolItem1.BackColor = SystemColors.ControlLight;
            this.symbolItem1.Location = new Point(12, 24);
            this.symbolItem1.Name = "symbolItem1";
            this.symbolItem1.Size = new Size(144, 88);
            this.symbolItem1.Symbol = null;
            this.symbolItem1.TabIndex = 5;
            this.groupBox2.Controls.Add(this.txtCategory);
            this.groupBox2.Controls.Add(this.txtName);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new Point(8, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(168, 88);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "符号名称";
            this.txtCategory.EditValue = "";
            this.txtCategory.Location = new Point(40, 56);
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.Size = new Size(112, 21);
            this.txtCategory.TabIndex = 12;
            this.txtName.EditValue = "";
            this.txtName.Location = new Point(40, 24);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(112, 21);
            this.txtName.TabIndex = 11;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 60);
            this.label2.Name = "label2";
            this.label2.Size = new Size(29, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "种类";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 28);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "名称";
            this.btnChangeSymbol.Location = new Point(8, 232);
            this.btnChangeSymbol.Name = "btnChangeSymbol";
            this.btnChangeSymbol.Size = new Size(48, 24);
            this.btnChangeSymbol.TabIndex = 10;
            this.btnChangeSymbol.Text = "属性...";
            this.btnChangeSymbol.Click += new EventHandler(this.btnChangeSymbol_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(186, 263);
            base.Controls.Add(this.btnChangeSymbol);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.btnCancle);
            base.Controls.Add(this.btnOK);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmSymbolProperty";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "符号属性";
            base.Load += new EventHandler(this.frmSymbolProperty_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.txtCategory.Properties.EndInit();
            this.txtName.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private SimpleButton btnCancle;
        private SimpleButton btnChangeSymbol;
        private SimpleButton btnOK;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label2;
        private IStyleGalleryItem m_pStyleGalleryItem;
        private enumSymbolType m_SymbolType;
        private SymbolItem symbolItem1;
        private TextEdit txtCategory;
        private TextEdit txtName;
    }
}