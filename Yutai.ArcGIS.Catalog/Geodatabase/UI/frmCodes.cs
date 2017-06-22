using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal partial class frmCodes : Form
    {
        private bool bool_0 = false;
        private ICodedValueDomain icodedValueDomain_0 = null;
        private IContainer icontainer_0 = null;
        internal object m_Code = "";
        internal string m_CodeName = "";

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

 private void frmCodes_Load(object sender, EventArgs e)
        {
            this.txtCode.Text = this.m_Code.ToString();
            this.txtName.Text = this.m_CodeName;
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

