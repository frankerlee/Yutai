using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    internal partial class LineFeaturePlaceSetCtrl : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private ILineLabelPosition m_LineLabelPosition = null;
        private IBasicOverposterLayerProperties4 m_OverLayerProperty = null;

        public event OnValueChangeEventHandler OnValueChange;

        public LineFeaturePlaceSetCtrl()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
                double num = 0.0;
                try
                {
                    num = double.Parse(this.txtOffset.Text);
                    this.m_LineLabelPosition.Offset = num;
                }
                catch
                {
                }
                this.m_LineLabelPosition.Horizontal = this.rdoLineLabelPosition.SelectedIndex == 0;
                this.m_LineLabelPosition.Parallel = this.rdoLineLabelPosition.SelectedIndex == 1;
                this.m_LineLabelPosition.ProduceCurvedLabels = this.rdoLineLabelPosition.SelectedIndex == 2;
                this.m_LineLabelPosition.Perpendicular = this.rdoLineLabelPosition.SelectedIndex == 3;
                this.m_LineLabelPosition.InLine = this.cboPlaceLinePos.SelectedIndex == 0;
                this.m_LineLabelPosition.AtStart = this.cboPlaceLinePos.SelectedIndex == 1;
                this.m_LineLabelPosition.AtEnd = this.cboPlaceLinePos.SelectedIndex == 2;
                if (this.cboCoorType.SelectedIndex == 0)
                {
                    this.m_LineLabelPosition.Right = this.chkBelow_Right.Checked;
                    this.m_LineLabelPosition.Below = false;
                    this.m_LineLabelPosition.Left = this.chkTop_Left.Checked;
                    this.m_LineLabelPosition.Above = false;
                }
                else
                {
                    this.m_LineLabelPosition.Right = false;
                    this.m_LineLabelPosition.Below = this.chkBelow_Right.Checked;
                    this.m_LineLabelPosition.Left = false;
                    this.m_LineLabelPosition.Above = this.chkTop_Left.Checked;
                }
                this.m_LineLabelPosition.OnTop = this.chkOnTop.Checked;
            }
        }

        public void Cancel()
        {
            this.m_IsPageDirty = false;
        }

        private void cboCoorType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboCoorType.SelectedIndex == 0)
            {
                this.chkBelow_Right.Text = "右";
                this.chkTop_Left.Text = "左";
                this.pictureEdit1.Visible = true;
                this.pictureEdit2.Visible = false;
            }
            else
            {
                this.chkBelow_Right.Text = "下";
                this.chkTop_Left.Text = "上";
                this.pictureEdit2.Visible = true;
                this.pictureEdit1.Visible = false;
            }
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void cboPlaceLinePos_SelectedIndexChanged(object sender, EventArgs e)
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

        private void chkBelow_Right_CheckedChanged(object sender, EventArgs e)
        {
            if (!(this.chkTop_Left.Checked || this.chkBelow_Right.Checked))
            {
                this.cboCoorType.Enabled = false;
            }
            else
            {
                this.cboCoorType.Enabled = true;
            }
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void chkOnTop_CheckedChanged(object sender, EventArgs e)
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

        private void chkTop_Left_CheckedChanged(object sender, EventArgs e)
        {
            if (!(this.chkTop_Left.Checked || this.chkBelow_Right.Checked))
            {
                this.cboCoorType.Enabled = false;
            }
            else
            {
                this.cboCoorType.Enabled = true;
            }
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

 private void LineFeaturePlaceSetCtrl_Load(object sender, EventArgs e)
        {
            if (this.m_OverLayerProperty != null)
            {
                this.m_LineLabelPosition = this.m_OverLayerProperty.LineLabelPosition;
                if (this.m_LineLabelPosition.Horizontal)
                {
                    this.rdoLineLabelPosition.SelectedIndex = 0;
                }
                else if (this.m_LineLabelPosition.Parallel)
                {
                    this.rdoLineLabelPosition.SelectedIndex = 1;
                }
                else if (this.m_LineLabelPosition.ProduceCurvedLabels)
                {
                    this.rdoLineLabelPosition.SelectedIndex = 2;
                }
                else
                {
                    this.rdoLineLabelPosition.SelectedIndex = 3;
                }
                this.txtOffset.Text = this.m_LineLabelPosition.Offset.ToString();
                if (this.m_LineLabelPosition.InLine)
                {
                    this.cboPlaceLinePos.SelectedIndex = 0;
                }
                else if (this.m_LineLabelPosition.AtStart)
                {
                    this.cboPlaceLinePos.SelectedIndex = 1;
                }
                else
                {
                    this.cboPlaceLinePos.SelectedIndex = 2;
                }
                this.chkBelow_Right.Checked = this.m_LineLabelPosition.Below ? this.m_LineLabelPosition.Below : this.m_LineLabelPosition.Right;
                this.chkTop_Left.Checked = this.m_LineLabelPosition.Above ? this.m_LineLabelPosition.Above : this.m_LineLabelPosition.Left;
                this.chkOnTop.Checked = this.m_LineLabelPosition.OnTop;
                this.cboCoorType_SelectedIndexChanged(this, e);
                if (!(this.chkTop_Left.Checked || this.chkBelow_Right.Checked))
                {
                    this.cboCoorType.Enabled = false;
                }
                else
                {
                    this.cboCoorType.Enabled = true;
                }
                this.m_CanDo = true;
            }
        }

        private void rdoLineLabelPosition_SelectedIndexChanged(object sender, EventArgs e)
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
            this.m_OverLayerProperty = @object as IBasicOverposterLayerProperties4;
        }

        private void txtOffset_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
                double num = 0.0;
                try
                {
                    num = double.Parse(this.txtOffset.Text);
                    this.txtOffset.ForeColor = Color.Black;
                }
                catch
                {
                    this.txtOffset.ForeColor = Color.Red;
                }
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.m_IsPageDirty;
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
                return "";
            }
            set
            {
            }
        }
    }
}

