using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;
using IPropertyPage = Yutai.ArcGIS.Common.BaseClasses.IPropertyPage;
using IPropertyPageEvents = Yutai.ArcGIS.Common.BaseClasses.IPropertyPageEvents;
using OnValueChangeEventHandler = Yutai.ArcGIS.Common.BaseClasses.OnValueChangeEventHandler;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public partial class StarndFenFuMapTemplatePage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private IContainer icontainer_0 = null;
        private IFillSymbol ifillSymbol_0 = new SimpleFillSymbolClass();
        private ILineSymbol ilineSymbol_0 = new SimpleLineSymbolClass();
        private MapTemplate mapTemplate_0 = null;

        public event OnValueChangeEventHandler OnValueChange;

        public StarndFenFuMapTemplatePage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            this.mapTemplate_0.StartX = double.Parse(this.txtStartX.Text);
            this.mapTemplate_0.StartY = double.Parse(this.txtStartY.Text);
            this.mapTemplate_0.XInterval = double.Parse(this.txtXInterval.Text);
            this.mapTemplate_0.YInterval = double.Parse(this.txtYInterval.Text);
            this.mapTemplate_0.Name = this.txtName.Text;
            if ((this.txtLegendInfo.Text.Length > 0) && File.Exists(this.txtLegendInfo.Text))
            {
                XmlDocument document = new XmlDocument();
                document.Load(this.txtLegendInfo.Text);
                this.mapTemplate_0.LegendInfo = document.InnerXml;
            }
            this.mapTemplate_0.InOutSpace = double.Parse(this.textBox2.Text);
            this.mapTemplate_0.BorderSymbol = this.styleButton1.Style as ISymbol;
            this.mapTemplate_0.OutBorderWidth = double.Parse(this.txtOutBorderWidth.Text);
            this.mapTemplate_0.SpheroidType = (this.cboDataum.SelectedIndex == 0) ? SpheroidType.Beijing54 : SpheroidType.Xian1980;
            if (this.rdo3.Checked)
            {
                this.mapTemplate_0.StripType = StripType.STThreeDeg;
            }
            else
            {
                this.mapTemplate_0.StripType = StripType.STSixDeg;
            }
        }

        public bool CanApply()
        {
            try
            {
                if (double.Parse(this.txtXInterval.Text) <= 0.0)
                {
                    return false;
                }
                if (double.Parse(this.txtYInterval.Text) <= 0.0)
                {
                    return false;
                }
                if (double.Parse(this.txtOutBorderWidth.Text) < 0.0)
                {
                    return false;
                }
                if (double.Parse(this.textBox2.Text) < 0.0)
                {
                    return false;
                }
                return true;
            }
            catch
            {
            }
            return false;
        }

        public void Cancel()
        {
        }

        private void cboDataum_SelectedIndexChanged(object sender, EventArgs e)
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

 private void rdo3_CheckedChanged(object sender, EventArgs e)
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

        private void rdo6_CheckedChanged(object sender, EventArgs e)
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

        private void rdoFill_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0 && this.rdoFill.Checked)
            {
                if (this.styleButton1.Style is ILineSymbol)
                {
                    this.ilineSymbol_0 = this.styleButton1.Style as ILineSymbol;
                }
                this.styleButton1.Style = this.ifillSymbol_0;
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void rdoLine_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0 && this.rdoLine.Checked)
            {
                if (this.styleButton1.Style is IFillSymbol)
                {
                    this.ifillSymbol_0 = this.styleButton1.Style as IFillSymbol;
                }
                this.styleButton1.Style = this.ilineSymbol_0;
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

        public void SetObjects(object object_0)
        {
        }

        private void StarndFenFuMapTemplatePage_Load(object sender, EventArgs e)
        {
            this.styleButton1.Style = this.ilineSymbol_0;
            if (this.mapTemplate_0 != null)
            {
                this.txtStartX.Text = this.mapTemplate_0.StartX.ToString();
                this.txtStartY.Text = this.mapTemplate_0.StartY.ToString();
                this.txtXInterval.Text = this.mapTemplate_0.XInterval.ToString();
                this.txtYInterval.Text = this.mapTemplate_0.YInterval.ToString();
                this.txtName.Text = this.mapTemplate_0.Name;
                this.textBox2.Text = this.mapTemplate_0.InOutSpace.ToString();
                this.txtOutBorderWidth.Text = this.mapTemplate_0.OutBorderWidth.ToString();
                this.styleButton1.Style = this.mapTemplate_0.BorderSymbol;
                this.cboDataum.SelectedIndex = (this.mapTemplate_0.SpheroidType == SpheroidType.Beijing54) ? 0 : 1;
                if (this.mapTemplate_0.StripType == StripType.STThreeDeg)
                {
                    this.rdo3.Checked = true;
                }
                else
                {
                    this.rdo6.Checked = true;
                }
                if (this.mapTemplate_0.BorderSymbol is ILineSymbol)
                {
                    this.rdoLine.Checked = true;
                    this.ilineSymbol_0 = this.mapTemplate_0.BorderSymbol as ILineSymbol;
                }
                if (this.mapTemplate_0.BorderSymbol is IFillSymbol)
                {
                    this.rdoFill.Checked = true;
                    this.ifillSymbol_0 = this.mapTemplate_0.BorderSymbol as IFillSymbol;
                }
            }
            this.bool_0 = true;
        }

        private void styleButton1_Click(object sender, EventArgs e)
        {
            frmSymbolSelector selector = new frmSymbolSelector();
            if (selector != null)
            {
                selector.SetSymbol(this.styleButton1.Style);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    this.styleButton1.Style = selector.GetSymbol();
                    if (this.rdoLine.Checked)
                    {
                        this.ilineSymbol_0 = this.styleButton1.Style as ILineSymbol;
                    }
                    else
                    {
                        this.ifillSymbol_0 = this.styleButton1.Style as IFillSymbol;
                    }
                    this.bool_1 = true;
                    if (this.OnValueChange != null)
                    {
                        this.OnValueChange();
                    }
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
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

        private void txtName_TextChanged(object sender, EventArgs e)
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

        private void txtOutBorderWidth_TextChanged(object sender, EventArgs e)
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

        private void txtStartX_TextChanged(object sender, EventArgs e)
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

        private void txtStartY_TextChanged(object sender, EventArgs e)
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

        private void txtXInterval_TextChanged(object sender, EventArgs e)
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

        private void txtYInterval_TextChanged(object sender, EventArgs e)
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

        private void txtYOffset_TextChanged(object sender, EventArgs e)
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

        public MapTemplate MapTemplate
        {
            get
            {
                return this.mapTemplate_0;
            }
            set
            {
                this.mapTemplate_0 = value;
            }
        }

        public string Title
        {
            get
            {
                return "常规";
            }
            set
            {
            }
        }
    }
}

