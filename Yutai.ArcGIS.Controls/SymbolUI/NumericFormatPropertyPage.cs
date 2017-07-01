using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.esriSystem;
using Yutai.Plugins.Interfaces;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    public partial class NumericFormatPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private INumericFormat m_pNumericFormat = null;
        private string m_Title = "数字";

        public event OnValueChangeEventHandler OnValueChange;

        public NumericFormatPropertyPage(IAppContext context)
        {
            this.InitializeComponent();
            _context = context;
        }

        public NumericFormatPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
                if (this.rdoRoundingOption.SelectedIndex == 0)
                {
                    this.m_pNumericFormat.RoundingOption = esriRoundingOptionEnum.esriRoundNumberOfDecimals;
                }
                else
                {
                    this.m_pNumericFormat.RoundingOption = esriRoundingOptionEnum.esriRoundNumberOfSignificantDigits;
                }
                this.m_pNumericFormat.RoundingValue = (int) this.txtRoundingValue.Value;
                if (this.rdoAlignmentOption.SelectedIndex == 0)
                {
                    this.m_pNumericFormat.AlignmentOption = esriNumericAlignmentEnum.esriAlignLeft;
                }
                else
                {
                    this.m_pNumericFormat.AlignmentOption = esriNumericAlignmentEnum.esriAlignRight;
                    this.m_pNumericFormat.AlignmentWidth = (int) this.txtAlignmentWidth.Value;
                }
                this.m_pNumericFormat.ShowPlusSign = this.chkShowPlusSign.Checked;
                this.m_pNumericFormat.UseSeparator = this.chkUseSeparator.Checked;
                this.m_pNumericFormat.ZeroPad = this.chkZeroPad.Checked;
            }
        }

        public void Cancel()
        {
        }

        private void chkShowPlusSign_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void chkUseSeparator_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void chkZeroPad_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        public void Hide()
        {
        }

        private void NumericFormatPropertyPage_Load(object sender, EventArgs e)
        {
            if (this.m_pNumericFormat != null)
            {
                if (this.m_pNumericFormat.RoundingOption == esriRoundingOptionEnum.esriRoundNumberOfDecimals)
                {
                    this.rdoRoundingOption.SelectedIndex = 0;
                }
                else
                {
                    this.rdoRoundingOption.SelectedIndex = 1;
                }
                this.txtRoundingValue.Value = this.m_pNumericFormat.RoundingValue;
                if (this.m_pNumericFormat.AlignmentOption == esriNumericAlignmentEnum.esriAlignLeft)
                {
                    this.rdoAlignmentOption.SelectedIndex = 0;
                    this.txtAlignmentWidth.Enabled = false;
                }
                else
                {
                    this.rdoAlignmentOption.SelectedIndex = 1;
                    this.txtAlignmentWidth.Enabled = true;
                }
                this.txtAlignmentWidth.Value = this.m_pNumericFormat.AlignmentWidth;
                this.chkShowPlusSign.Checked = this.m_pNumericFormat.ShowPlusSign;
                this.chkUseSeparator.Checked = this.m_pNumericFormat.UseSeparator;
                this.chkZeroPad.Checked = this.m_pNumericFormat.ZeroPad;
            }
            this.m_CanDo = true;
        }

        private void rdoAlignmentOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtAlignmentWidth.Enabled = this.rdoAlignmentOption.SelectedIndex == 1;
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void rdoRoundingOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        public void SetObjects(object @object)
        {
            this.m_pNumericFormat = @object as INumericFormat;
        }

        private void txtAlignmentWidth_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void txtRoundingValue_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        public int Height
        {
            get { return 0; }
        }

        public bool IsPageDirty
        {
            get { return this.m_IsPageDirty; }
        }

        public string Title
        {
            get { return this.m_Title; }
            set { }
        }

        public int Width
        {
            get { return 0; }
        }
    }
}