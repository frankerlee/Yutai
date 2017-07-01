using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public partial class ElementTypeSelectPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private IContainer icontainer_0 = null;

        public event OnValueChangeEventHandler OnValueChange;

        public ElementTypeSelectPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.rdoConstantTextElement.Checked)
            {
                ElementWizardHelp.ElementType = ElementType.ConstantText;
            }
            else if (this.rdoSingleTextElement.Checked)
            {
                ElementWizardHelp.ElementType = ElementType.SingleText;
            }
            else if (this.rdoMultiTextElement.Checked)
            {
                ElementWizardHelp.ElementType = ElementType.MultiText;
            }
            else if (this.rdoJoinTableElement.Checked)
            {
                ElementWizardHelp.ElementType = ElementType.JoinTable;
            }
            else if (this.rdoScaleText.Checked)
            {
                ElementWizardHelp.ElementType = ElementType.ScaleText;
            }
            else if (this.rdoCustomLegend.Checked)
            {
                ElementWizardHelp.ElementType = ElementType.CustomLegend;
            }
            else if (this.rdoLegend.Checked)
            {
                ElementWizardHelp.ElementType = ElementType.Legend;
            }
            else if (this.rdoPicture.Checked)
            {
                ElementWizardHelp.ElementType = ElementType.Picture;
            }
            else if (this.rdoOle.Checked)
            {
                ElementWizardHelp.ElementType = ElementType.OLE;
            }
            else if (this.rdoScaleBar.Checked)
            {
                ElementWizardHelp.ElementType = ElementType.ScaleBar;
            }
            else if (this.rdoNorth.Checked)
            {
                ElementWizardHelp.ElementType = ElementType.North;
            }
        }

        public void Cancel()
        {
            this.bool_1 = false;
        }

        private void ElementTypeSelectPage_Load(object sender, EventArgs e)
        {
            if (ElementWizardHelp.ElementType == ElementType.ConstantText)
            {
                this.rdoConstantTextElement.Checked = true;
            }
            else if (ElementWizardHelp.ElementType == ElementType.SingleText)
            {
                this.rdoSingleTextElement.Checked = true;
            }
            else if (ElementWizardHelp.ElementType == ElementType.MultiText)
            {
                this.rdoMultiTextElement.Checked = true;
            }
            else if (ElementWizardHelp.ElementType == ElementType.JoinTable)
            {
                this.rdoJoinTableElement.Checked = true;
            }
            else if (ElementWizardHelp.ElementType == ElementType.ScaleText)
            {
                this.rdoScaleText.Checked = true;
            }
            else if (ElementWizardHelp.ElementType == ElementType.CustomLegend)
            {
                this.rdoCustomLegend.Checked = true;
            }
            else if (ElementWizardHelp.ElementType == ElementType.Legend)
            {
                this.rdoLegend.Checked = true;
            }
            else if (ElementWizardHelp.ElementType == ElementType.Picture)
            {
                this.rdoPicture.Checked = true;
            }
            else if (ElementWizardHelp.ElementType == ElementType.OLE)
            {
                this.rdoOle.Checked = true;
            }
            else if (ElementWizardHelp.ElementType == ElementType.ScaleBar)
            {
                this.rdoScaleBar.Checked = true;
            }
            else if (ElementWizardHelp.ElementType == ElementType.North)
            {
                this.rdoNorth.Checked = true;
            }
            else if (ElementWizardHelp.ElementType == ElementType.DataGraphicElement)
            {
                this.rdoDataGraphicElement.Checked = true;
            }
            if (!ElementWizardHelp.HasElementSelect)
            {
                this.groupBox1.Enabled = false;
            }
            this.bool_0 = true;
        }

        private void rdoNorth_CheckedChanged(object sender, EventArgs e)
        {
        }

        public void ResetControl()
        {
        }

        public void SetObjects(object object_0)
        {
        }

        public bool IsPageDirty
        {
            get { return this.bool_1; }
        }

        int IPropertyPage.Height
        {
            get { return base.Height; }
        }

        int IPropertyPage.Width
        {
            get { return base.Width; }
        }

        public string Title
        {
            get { return "类型"; }
            set { }
        }
    }
}