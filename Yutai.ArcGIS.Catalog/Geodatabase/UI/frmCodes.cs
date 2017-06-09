using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal class frmCodes : Form
    {
        private bool bool_0 = false;
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private ICodedValueDomain icodedValueDomain_0 = null;
        private IContainer icontainer_0 = null;
        private Label label1;
        private Label label2;
        internal object m_Code = "";
        internal string m_CodeName = "";
        private TextEdit txtCode;
        private TextEdit txtName;

        public frmCodes()
        {
            this.Text = "新建";
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtCode.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入代码值！");
            }
            else if (this.txtName.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入代码名称！");
            }
            else
            {
                string str = this.txtCode.Text.Trim().ToLower();
                string name = this.txtName.Text.Trim().ToLower();
                if (str == this.m_Code.ToString().ToLower())
                {
                    if (name == this.m_CodeName.ToLower())
                    {
                        base.Close();
                    }
                    else
                    {
                        this.icodedValueDomain_0.DeleteCode(this.m_Code);
                        this.icodedValueDomain_0.AddCode(this.m_Code, name);
                        this.m_CodeName = name;
                        base.DialogResult = DialogResult.OK;
                    }
                }
                else
                {
                    object obj2 = this.method_0(this.txtCode.Text.Trim());
                    if (obj2 == null)
                    {
                        MessageBox.Show("代码值和域值类型不符!");
                    }
                    else
                    {
                        string str3 = obj2.ToString();
                        for (int i = 0; i < this.icodedValueDomain_0.CodeCount; i++)
                        {
                            if (this.icodedValueDomain_0.get_Value(i).ToString().ToLower() == str3)
                            {
                                MessageBox.Show("域值中已包含该代码值!");
                                return;
                            }
                        }
                        if (this.bool_0)
                        {
                            this.icodedValueDomain_0.DeleteCode(this.m_Code);
                        }
                        this.icodedValueDomain_0.AddCode(obj2, name);
                        this.m_CodeName = name;
                        this.m_Code = obj2;
                        base.DialogResult = DialogResult.OK;
                    }
                }
            }
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void frmCodes_Load(object sender, EventArgs e)
        {
            this.txtCode.Text = this.m_Code.ToString();
            this.txtName.Text = this.m_CodeName;
        }

        private void InitializeComponent()
        {
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.txtCode = new TextEdit();
            this.label1 = new Label();
            this.txtName = new TextEdit();
            this.label2 = new Label();
            this.txtCode.Properties.BeginInit();
            this.txtName.Properties.BeginInit();
            base.SuspendLayout();
            this.btnOK.Location = new Point(0x52, 0x3b);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x40, 0x18);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0x98, 0x3b);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x40, 0x18);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消";
            this.txtCode.EditValue = "";
            this.txtCode.Location = new Point(0x2f, 4);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new Size(0xa5, 0x15);
            this.txtCode.TabIndex = 9;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "代码";
            this.txtName.EditValue = "";
            this.txtName.Location = new Point(0x2f, 0x20);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(0xa5, 0x15);
            this.txtName.TabIndex = 11;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 0x25);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "描述";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0xec, 0x62);
            base.Controls.Add(this.txtName);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.txtCode);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmCodes";
            base.Load += new EventHandler(this.frmCodes_Load);
            this.txtCode.Properties.EndInit();
            this.txtName.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private object method_0(string string_0)
        {
            try
            {
                switch ((this.icodedValueDomain_0 as IDomain).FieldType)
                {
                    case esriFieldType.esriFieldTypeSmallInteger:
                        return short.Parse(string_0);

                    case esriFieldType.esriFieldTypeInteger:
                        return int.Parse(string_0);

                    case esriFieldType.esriFieldTypeSingle:
                        return float.Parse(string_0);

                    case esriFieldType.esriFieldTypeDouble:
                        return double.Parse(string_0);

                    case esriFieldType.esriFieldTypeString:
                        return string_0;
                }
            }
            catch
            {
            }
            return null;
        }

        internal void SetCode(object object_0, string string_0)
        {
            this.Text = "编辑";
            this.m_Code = object_0;
            this.m_CodeName = string_0;
            this.bool_0 = true;
        }

        internal ICodedValueDomain CodeValueDomain
        {
            set
            {
                this.icodedValueDomain_0 = value;
            }
        }

        internal bool IsEdit
        {
            set
            {
                this.bool_0 = value;
            }
        }
    }
}

