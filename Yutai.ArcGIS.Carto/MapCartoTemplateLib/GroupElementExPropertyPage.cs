using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public partial class GroupElementExPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private IContainer icontainer_0 = null;

        public event OnValueChangeEventHandler OnValueChange;

        public GroupElementExPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                if (this.rdoFiexdSize.Checked)
                {
                    (this.mapTemplateElement_0 as MapTemplateGroupElement).SizeStyle = SizeStyle.Fixed;
                }
                else if (this.rdoSameAsWidth.Checked)
                {
                    (this.mapTemplateElement_0 as MapTemplateGroupElement).SizeStyle = SizeStyle.SameAsInsideWidth;
                }
                else if (this.rdoWidthScale.Checked)
                {
                    (this.mapTemplateElement_0 as MapTemplateGroupElement).SizeStyle = SizeStyle.InsideWidthScale;
                    try
                    {
                        (this.mapTemplateElement_0 as MapTemplateGroupElement).SizeScale = Convert.ToDouble(this.txtScale.Text);
                    }
                    catch
                    {
                        MessageBox.Show("数据输入错误!");
                        return;
                    }
                }
                this.bool_1 = false;
            }
        }

        public void Cancel()
        {
            this.bool_1 = false;
        }

 private void GroupElementExPropertyPage_Load(object sender, EventArgs e)
        {
            this.method_1();
        }

 private void method_0()
        {
            this.bool_1 = true;
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
            }
        }

        private void method_1()
        {
            this.bool_0 = false;
            if (this.mapTemplateElement_0 != null)
            {
                SizeStyle sizeStyle = (this.mapTemplateElement_0 as MapTemplateGroupElement).SizeStyle;
                double sizeScale = (this.mapTemplateElement_0 as MapTemplateGroupElement).SizeScale;
                if (sizeStyle == SizeStyle.Fixed)
                {
                    this.rdoFiexdSize.Checked = true;
                    this.txtScale.Enabled = false;
                }
                else if (sizeStyle == SizeStyle.InsideWidthScale)
                {
                    this.rdoWidthScale.Checked = true;
                    this.txtScale.Enabled = true;
                }
                else
                {
                    this.rdoSameAsWidth.Checked = true;
                    this.txtScale.Enabled = false;
                }
                this.txtScale.Text = sizeScale.ToString();
            }
            this.bool_0 = true;
        }

        private void rdoWidthScale_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoWidthScale.Checked)
            {
                this.txtScale.Enabled = true;
            }
            else
            {
                this.txtScale.Enabled = false;
            }
            if (this.bool_0)
            {
                this.method_0();
            }
        }

        public void ResetControl()
        {
            this.bool_0 = false;
            this.method_1();
            this.bool_0 = true;
        }

        public void SetObjects(object object_0)
        {
            this.mapTemplateElement_0 = object_0 as MapCartoTemplateLib.MapTemplateElement;
        }

        private void txtScale_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.method_0();
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

        public MapCartoTemplateLib.MapTemplateElement MapTemplateElement
        {
            set
            {
                this.mapTemplateElement_0 = value;
                this.method_1();
            }
        }

        public string Title
        {
            get
            {
                return "组合元素缩放";
            }
            set
            {
            }
        }
    }
}

