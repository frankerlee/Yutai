namespace Yutai.Catalog.UI
{
    using ESRI.ArcGIS.Geodatabase;
    
   
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class SetImportRecordCtrl : UserControl
    {
        private Button btnQueryDialog;
        private IContainer icontainer_0 = null;
    
        private TextBox  memoEdit;
        private RadioButton radioButton1;
        private RadioButton radioButton2;

        public SetImportRecordCtrl()
        {
            this.InitializeComponent();
        }

        private void btnQueryDialog_Click(object sender, EventArgs e)
        {
            //frmAttributeQueryBuilder builder = new frmAttributeQueryBuilder
            //{
            //    Table = this.Table,
            //    WhereCaluse = this.memoEdit.Text
            //};
            //if (builder.ShowDialog() == DialogResult.OK)
            //{
            //    this.memoEdit.Text = builder.WhereCaluse;
            //}
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void InitializeComponent()
        {
            this.radioButton1 = new RadioButton();
            this.radioButton2 = new RadioButton();
            this.btnQueryDialog = new Button();
            this.memoEdit = new TextBox();
           
            base.SuspendLayout();
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new Point(0x22, 0x1c);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new Size(0x5f, 0x10);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "导入所有记录";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new EventHandler(this.radioButton1_CheckedChanged);
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new Point(0x22, 0x3b);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new Size(0x9b, 0x10);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "导入满足查询条件的记录";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.btnQueryDialog.Enabled = false;
            this.btnQueryDialog.Location = new Point(0x4a, 0x5c);
            this.btnQueryDialog.Name = "btnQueryDialog";
            this.btnQueryDialog.Size = new Size(0x58, 0x18);
            this.btnQueryDialog.TabIndex = 4;
            this.btnQueryDialog.Text = "查询生成器";
            this.btnQueryDialog.Click += new EventHandler(this.btnQueryDialog_Click);
            this.memoEdit.Text="";
            this.memoEdit.Location = new Point(0x22, 0x81);
            this.memoEdit.Name = "memoEdit";
            
            this.memoEdit.Size = new Size(0x120, 0x88);
            this.memoEdit.TabIndex = 3;
            this.memoEdit.Visible = false;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.btnQueryDialog);
            base.Controls.Add(this.memoEdit);
            base.Controls.Add(this.radioButton2);
            base.Controls.Add(this.radioButton1);
            base.Name = "SetImportRecordCtrl";
            base.Size = new Size(0x198, 0x11e);
           
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.btnQueryDialog.Enabled = this.radioButton2.Checked;
            this.memoEdit.Visible = this.radioButton2.Checked;
        }

        public ITable Table
        {
            get; set;
        }

        public string Where
        {
            get
            {
                if (this.radioButton1.Checked)
                {
                    return "";
                }
                return this.memoEdit.Text;
            }
        }
    }
}

