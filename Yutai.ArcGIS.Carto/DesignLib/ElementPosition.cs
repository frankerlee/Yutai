using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public partial class ElementPosition : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private IContainer icontainer_0 = null;

        public event OnValueChangeEventHandler OnValueChange;

        public ElementPosition()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                ElementWizardHelp.Position = this.SaveToXml();
            }
        }

        public void Cancel()
        {
            this.bool_1 = false;
        }

 private void ElementPosition_Load(object sender, EventArgs e)
        {
            if (ElementWizardHelp.Position.Length > 0)
            {
                int num = 0;
                try
                {
                    XmlDocument document = new XmlDocument();
                    document.LoadXml(ElementWizardHelp.Position);
                    XmlNode node = document.ChildNodes[0];
                    for (int i = 0; i < node.ChildNodes.Count; i++)
                    {
                        XmlNode node2 = node.ChildNodes[i];
                        string str = node2.Attributes["name"].Value;
                        string s = node2.Attributes["value"].Value;
                        switch (str)
                        {
                            case "position":
                                try
                                {
                                    num = int.Parse(s);
                                }
                                catch
                                {
                                }
                                break;

                            case "xoffset":
                                this.txtOffsetX.Text = s;
                                break;

                            case "yoffset":
                                this.txtOffsetY.Text = s;
                                break;
                        }
                    }
                }
                catch
                {
                }
                switch (num)
                {
                    case 0:
                        this.rdoUpperLeft.Checked = true;
                        break;

                    case 1:
                        this.rdoUM.Checked = true;
                        break;

                    case 2:
                        this.rdoUpperRight.Checked = true;
                        break;

                    case 3:
                        this.rdoLU.Checked = true;
                        break;

                    case 4:
                        this.rdoRU.Checked = true;
                        break;

                    case 5:
                        this.rdoLeftM.Checked = true;
                        break;

                    case 6:
                        this.rdoRM.Checked = true;
                        break;

                    case 7:
                        this.rdoLL.Checked = true;
                        break;

                    case 8:
                        this.rdoRL.Checked = true;
                        break;

                    case 9:
                        this.rdoBottonLeft.Checked = true;
                        break;

                    case 10:
                        this.rdoLowM.Checked = true;
                        break;

                    case 11:
                        this.rdoBottomRight.Checked = true;
                        break;
                }
            }
            else
            {
                ElementWizardHelp.Position = this.SaveToXml();
            }
            this.bool_0 = true;
        }

 private void method_0(StringBuilder stringBuilder_0, string string_0, string string_1)
        {
            stringBuilder_0.Append("<attribute name=\"");
            stringBuilder_0.Append(string_0);
            stringBuilder_0.Append("\" value=\"");
            stringBuilder_0.Append(string_1 + "\"/>");
        }

        private void rdoLU_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        public void ResetControl()
        {
        }

        public string SaveToXml()
        {
            StringBuilder builder = new StringBuilder("<ElementPosition>");
            int num = 0;
            if (this.rdoUpperLeft.Checked)
            {
                num = 0;
            }
            else if (this.rdoUM.Checked)
            {
                num = 1;
            }
            else if (this.rdoUpperRight.Checked)
            {
                num = 2;
            }
            else if (this.rdoLU.Checked)
            {
                num = 3;
            }
            else if (this.rdoRU.Checked)
            {
                num = 4;
            }
            else if (this.rdoLeftM.Checked)
            {
                num = 5;
            }
            else if (this.rdoRM.Checked)
            {
                num = 6;
            }
            else if (this.rdoLL.Checked)
            {
                num = 7;
            }
            else if (this.rdoRL.Checked)
            {
                num = 8;
            }
            else if (this.rdoBottonLeft.Checked)
            {
                num = 9;
            }
            else if (this.rdoLowM.Checked)
            {
                num = 10;
            }
            else if (this.rdoBottomRight.Checked)
            {
                num = 11;
            }
            this.method_0(builder, "position", num.ToString());
            this.method_0(builder, "xoffset", this.txtOffsetX.Text);
            this.method_0(builder, "yoffset", this.txtOffsetY.Text);
            builder.Append("</ElementPosition>");
            return builder.ToString();
        }

        public void SetObjects(object object_0)
        {
        }

        private void txtOffsetY_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_1;
            }
        }

        int IPropertyPage.Height
        {
            get
            {
                return base.Height;
            }
        }

        int IPropertyPage.Width
        {
            get
            {
                return base.Width;
            }
        }

        public string Title
        {
            get
            {
                return "位置";
            }
            set
            {
            }
        }
    }
}

