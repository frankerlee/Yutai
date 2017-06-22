using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    [ToolboxItem(false)]
    public partial class TFInfoPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private IContainer icontainer_0 = null;

        public event OnValueChangeEventHandler OnValueChange;

        public TFInfoPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                try
                {
                    CartoTemplateClass.InOutDist = double.Parse(this.txtInOutDis.Text);
                }
                catch
                {
                }
                try
                {
                    CartoTemplateClass.OutBorderWidth = double.Parse(this.txtOutBorderWidth.Text);
                }
                catch
                {
                }
                try
                {
                    CartoTemplateClass.XInterval = double.Parse(this.txtXInterval.Text);
                }
                catch
                {
                }
                try
                {
                    CartoTemplateClass.YInterval = double.Parse(this.txtYInterval.Text);
                }
                catch
                {
                }
                try
                {
                    CartoTemplateClass.TitleDist = double.Parse(this.txtTitleSpace.Text);
                }
                catch
                {
                }
                try
                {
                    CartoTemplateClass.StartCoodinateMultiple = double.Parse(this.txtStartMultiple.Text);
                }
                catch
                {
                }
                CartoTemplateClass.TuKuoInfo = this.SaveToXml();
            }
        }

        public void Cancel()
        {
            this.bool_1 = false;
        }

 public void Init()
        {
            if (CartoTemplateClass.TuKuoInfo.Length != 0)
            {
                try
                {
                    XmlDocument document = new XmlDocument();
                    document.LoadXml(CartoTemplateClass.TuKuoInfo);
                    XmlNode node = document.ChildNodes[0];
                    for (int i = 0; i < node.ChildNodes.Count; i++)
                    {
                        XmlNode node2 = node.ChildNodes[i];
                        string str = node2.Attributes["name"].Value;
                        string str2 = node2.Attributes["value"].Value;
                        switch (str)
                        {
                            case "InOutDist":
                                this.txtInOutDis.Text = str2;
                                break;

                            case "TitleDist":
                                this.txtTitleSpace.Text = str2;
                                break;

                            case "XInterval":
                                this.txtXInterval.Text = str2;
                                break;

                            case "YInterval":
                                this.txtYInterval.Text = str2;
                                break;

                            case "OutBorderWidth":
                                this.txtOutBorderWidth.Text = str2;
                                break;

                            case "StartCoodinateMultiple":
                                this.txtStartMultiple.Text = str2;
                                break;
                        }
                    }
                }
                catch
                {
                }
            }
        }

 private void method_0(StringBuilder stringBuilder_0, string string_0, string string_1)
        {
            stringBuilder_0.Append("<attribute name=\"");
            stringBuilder_0.Append(string_0);
            stringBuilder_0.Append("\" value=\"");
            stringBuilder_0.Append(string_1 + "\"/>");
        }

        public void ResetControl()
        {
            this.bool_0 = false;
            this.Init();
            this.bool_0 = true;
        }

        public string SaveToXml()
        {
            StringBuilder builder = new StringBuilder("<TFElement>");
            this.method_0(builder, "InOutDist", this.txtInOutDis.Text);
            this.method_0(builder, "TitleDist", this.txtTitleSpace.Text);
            this.method_0(builder, "XInterval", this.txtXInterval.Text);
            this.method_0(builder, "YInterval", this.txtYInterval.Text);
            this.method_0(builder, "OutBorderWidth", this.txtOutBorderWidth.Text);
            this.method_0(builder, "StartCoodinateMultiple", this.txtStartMultiple.Text);
            builder.Append("</TFElement>");
            return builder.ToString();
        }

        public void SetObjects(object object_0)
        {
        }

        private void TFInfoPage_Load(object sender, EventArgs e)
        {
            this.Init();
            this.bool_0 = true;
        }

        private void txtInOutDis_TextChanged(object sender, EventArgs e)
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
                return "图幅";
            }
            set
            {
            }
        }
    }
}

