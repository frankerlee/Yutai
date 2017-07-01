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
    public partial class MapTemplateGeneralPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private IContainer icontainer_0 = null;
        private IFillSymbol ifillSymbol_0 = new SimpleFillSymbolClass();
        private ILineSymbol ilineSymbol_0 = new SimpleLineSymbolClass();
        private MapTemplate mapTemplate_0 = null;

        public event OnValueChangeEventHandler OnValueChange;

        public MapTemplateGeneralPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                this.mapTemplate_0.Width = double.Parse(this.txtWidth.Text);
                this.mapTemplate_0.Height = double.Parse(this.txtHeight.Text);
                this.mapTemplate_0.Scale = double.Parse(this.txtScale.Text);
                this.mapTemplate_0.StartX = double.Parse(this.txtStartX.Text);
                this.mapTemplate_0.StartY = double.Parse(this.txtStartY.Text);
                this.mapTemplate_0.XInterval = double.Parse(this.txtXInterval.Text);
                this.mapTemplate_0.YInterval = double.Parse(this.txtYInterval.Text);
                this.mapTemplate_0.Name = this.txtName.Text;
                if ((this.txtLengendInfo.Text.Length > 0) && File.Exists(this.txtLengendInfo.Text))
                {
                    XmlDocument document = new XmlDocument();
                    document.Load(this.txtLengendInfo.Text);
                    this.mapTemplate_0.LegendInfo = document.InnerXml;
                }
                this.mapTemplate_0.InOutSpace = double.Parse(this.textBox2.Text);
                this.mapTemplate_0.BorderSymbol = this.styleButton1.Style as ISymbol;
                this.mapTemplate_0.OutBorderWidth = double.Parse(this.txtOutBorderWidth.Text);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtLengendInfo.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "*.xml|*.xml"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txtLengendInfo.Text = dialog.FileName;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
                this.bool_1 = true;
            }
        }

        public bool CanApply()
        {
            try
            {
                if (double.Parse(this.txtWidth.Text) <= 0.0)
                {
                    return false;
                }
                if (double.Parse(this.txtHeight.Text) <= 0.0)
                {
                    return false;
                }
                if (double.Parse(this.txtScale.Text) <= 0.0)
                {
                    return false;
                }
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

        private void MapTemplateGeneralPage_Load(object sender, EventArgs e)
        {
            this.styleButton1.Style = this.ilineSymbol_0;
            if (this.mapTemplate_0 != null)
            {
                this.txtWidth.Text = this.mapTemplate_0.Width.ToString();
                this.txtHeight.Text = this.mapTemplate_0.Height.ToString();
                this.txtScale.Text = this.mapTemplate_0.Scale.ToString();
                this.txtStartX.Text = this.mapTemplate_0.StartX.ToString();
                this.txtStartY.Text = this.mapTemplate_0.StartY.ToString();
                this.txtXInterval.Text = this.mapTemplate_0.XInterval.ToString();
                this.txtYInterval.Text = this.mapTemplate_0.YInterval.ToString();
                this.txtName.Text = this.mapTemplate_0.Name;
                this.textBox2.Text = this.mapTemplate_0.InOutSpace.ToString();
                this.txtOutBorderWidth.Text = this.mapTemplate_0.OutBorderWidth.ToString();
                this.styleButton1.Style = this.mapTemplate_0.BorderSymbol;
                if (this.mapTemplate_0.BorderSymbol is ILineSymbol)
                {
                    this.rdoLine.Checked = true;
                    this.ilineSymbol_0 = this.mapTemplate_0.BorderSymbol as ILineSymbol;
                }
                if (this.mapTemplate_0.BorderSymbol is IFillSymbol)
                {
                    this.ifillSymbol_0 = this.mapTemplate_0.BorderSymbol as IFillSymbol;
                    this.rdoFill.Checked = true;
                }
            }
            this.bool_0 = true;
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

        private void txtHeight_TextChanged(object sender, EventArgs e)
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

        private void txtScale_TextChanged(object sender, EventArgs e)
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

        public MapTemplate MapTemplate
        {
            get { return this.mapTemplate_0; }
            set { this.mapTemplate_0 = value; }
        }

        public string Title
        {
            get { return "常规"; }
            set { }
        }

        public bool UseMapGrid
        {
            get { return this.chkMapGrid.Checked; }
            set { this.chkMapGrid.Visible = false; }
        }
    }
}