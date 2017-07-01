using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    internal partial class PointFeatureLabelCtrl : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private IBasicOverposterLayerProperties4 m_OverLayerProperty = null;
        private IPointPlacementPriorities m_Priorities = null;

        public event OnValueChangeEventHandler OnValueChange;

        public PointFeatureLabelCtrl()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
                ImageComboBoxItem item = this.imageComboBoxEdit1.Properties.Items[this.imageComboBoxEdit1.SelectedIndex];
                this.m_OverLayerProperty.PointPlacementMethod =
                    (esriOverposterPointPlacementMethod) this.rdoPointPlacementMethod.SelectedIndex;
                switch (this.rdoPointPlacementMethod.SelectedIndex)
                {
                    case 0:
                        this.SetPlacementPriorities(item.Value.ToString(), this.m_Priorities);
                        this.m_OverLayerProperty.PointPlacementPriorities = this.m_Priorities;
                        break;

                    case 1:
                    {
                        double[] numArray = new double[this.listPointPlacementAngles.Items.Count];
                        for (int i = 0; i < this.listPointPlacementAngles.Items.Count; i++)
                        {
                            numArray[i] = (double) this.listPointPlacementAngles.Items[i];
                        }
                        this.m_OverLayerProperty.PointPlacementAngles = numArray;
                        break;
                    }
                    case 3:
                        if (this.cboFields.SelectedIndex != -1)
                        {
                            this.m_OverLayerProperty.RotationField = this.cboFields.Text;
                        }
                        this.m_OverLayerProperty.PerpendicularToAngle = this.chkPerpendicularToAngle.Checked;
                        if (this.rdoRotationType.SelectedIndex == 0)
                        {
                            this.m_OverLayerProperty.RotationType = esriLabelRotationType.esriRotateLabelGeographic;
                        }
                        else
                        {
                            this.m_OverLayerProperty.RotationType = esriLabelRotationType.esriRotateLabelArithmetic;
                        }
                        break;
                }
            }
        }

        private void btnAddAngle_Click(object sender, EventArgs e)
        {
            try
            {
                double item = double.Parse(this.txtAngle.Text);
                this.listPointPlacementAngles.Items.Add(item);
                if (this.m_CanDo)
                {
                    this.m_IsPageDirty = true;
                    if (this.OnValueChange != null)
                    {
                        this.OnValueChange();
                    }
                }
            }
            catch
            {
            }
        }

        private void btnDeleteLayer_Click(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
                int selectedIndex = this.listPointPlacementAngles.SelectedIndex;
                if (selectedIndex != -1)
                {
                    this.listPointPlacementAngles.Items.RemoveAt(selectedIndex);
                }
            }
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
                int selectedIndex = this.listPointPlacementAngles.SelectedIndex;
                if ((selectedIndex > -1) && (selectedIndex < (this.listPointPlacementAngles.Items.Count - 1)))
                {
                    object item = this.listPointPlacementAngles.Items[selectedIndex];
                    this.listPointPlacementAngles.Items.RemoveAt(selectedIndex);
                    if (this.listPointPlacementAngles.Items.Count == selectedIndex)
                    {
                        this.listPointPlacementAngles.Items.Add(item);
                    }
                    else
                    {
                        this.listPointPlacementAngles.Items.Insert(selectedIndex + 1, item);
                    }
                }
            }
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
                int selectedIndex = this.listPointPlacementAngles.SelectedIndex;
                if (selectedIndex > 0)
                {
                    object item = this.listPointPlacementAngles.Items[selectedIndex];
                    this.listPointPlacementAngles.Items.RemoveAt(selectedIndex);
                    this.listPointPlacementAngles.Items.Insert(selectedIndex - 1, item);
                }
            }
        }

        public void Cancel()
        {
        }

        private void chkPerpendicularToAngle_CheckedChanged(object sender, EventArgs e)
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

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
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

        private int GetNumber(char c)
        {
            switch (c)
            {
                case '0':
                    return 0;

                case '1':
                    return 1;

                case '2':
                    return 2;

                case '3':
                    return 3;

                case '4':
                    return 4;

                case '5':
                    return 5;

                case '6':
                    return 6;

                case '7':
                    return 7;

                case '8':
                    return 8;

                case '9':
                    return 9;
            }
            return 0;
        }

        private string GetPlacementPrioritiesStr(IPointPlacementPriorities Priorities)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.GetValue(Priorities.AboveLeft));
            builder.Append(this.GetValue(Priorities.AboveCenter));
            builder.Append(this.GetValue(Priorities.AboveRight));
            builder.Append(this.GetValue(Priorities.CenterLeft));
            builder.Append(this.GetValue(0));
            builder.Append(this.GetValue(Priorities.CenterRight));
            builder.Append(this.GetValue(Priorities.BelowLeft));
            builder.Append(this.GetValue(Priorities.BelowCenter));
            builder.Append(this.GetValue(Priorities.BelowRight));
            return builder.ToString();
        }

        private int GetValue(int i)
        {
            if (i > 3)
            {
                return 3;
            }
            return i;
        }

        public void Hide()
        {
        }

        private void imageComboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
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

        private void listPointPlacementAngles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listPointPlacementAngles.SelectedIndex != -1)
            {
                if (this.listPointPlacementAngles.Items.Count == 1)
                {
                    this.btnMoveUp.Enabled = false;
                    this.btnMoveDown.Enabled = false;
                }
                else if (this.listPointPlacementAngles.SelectedIndex == 0)
                {
                    this.btnMoveUp.Enabled = false;
                    this.btnMoveDown.Enabled = true;
                }
                else if (this.listPointPlacementAngles.SelectedIndex == (this.listPointPlacementAngles.Items.Count - 1))
                {
                    this.btnMoveUp.Enabled = true;
                    this.btnMoveDown.Enabled = false;
                }
                else
                {
                    this.btnMoveUp.Enabled = true;
                    this.btnMoveDown.Enabled = true;
                }
                this.btnDeleteLayer.Enabled = true;
            }
            else
            {
                this.btnMoveUp.Enabled = false;
                this.btnMoveDown.Enabled = false;
                this.btnDeleteLayer.Enabled = false;
            }
        }

        private void PointFeatureLabelCtrl_Load(object sender, EventArgs e)
        {
            if (this.m_OverLayerProperty != null)
            {
                int num;
                this.rdoPointPlacementMethod.SelectedIndex = (int) this.m_OverLayerProperty.PointPlacementMethod;
                if (this.m_OverLayerProperty.RotationType == esriLabelRotationType.esriRotateLabelGeographic)
                {
                    this.rdoRotationType.SelectedIndex = 0;
                }
                else
                {
                    this.rdoRotationType.SelectedIndex = 1;
                }
                this.chkPerpendicularToAngle.Checked = this.m_OverLayerProperty.PerpendicularToAngle;
                this.m_Priorities = this.m_OverLayerProperty.PointPlacementPriorities;
                string placementPrioritiesStr = this.GetPlacementPrioritiesStr(this.m_Priorities);
                for (num = 0; num < this.imageComboBoxEdit1.Properties.Items.Count; num++)
                {
                    ImageComboBoxItem item = this.imageComboBoxEdit1.Properties.Items[num];
                    if (item.Value.ToString() == placementPrioritiesStr)
                    {
                        this.imageComboBoxEdit1.SelectedIndex = num;
                        break;
                    }
                }
                double[] pointPlacementAngles = (double[]) this.m_OverLayerProperty.PointPlacementAngles;
                for (num = 0; num < pointPlacementAngles.Length; num++)
                {
                    this.listPointPlacementAngles.Items.Add(pointPlacementAngles[num]);
                }
                this.rdoPointPlacementMethod_SelectedIndexChanged(this, e);
                this.m_CanDo = true;
            }
        }

        private void rdoPointPlacementMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.rdoPointPlacementMethod.SelectedIndex)
            {
                case 0:
                    this.panel1.Visible = true;
                    this.panel2.Visible = false;
                    this.panel3.Visible = false;
                    break;

                case 1:
                    this.panel1.Visible = false;
                    this.panel2.Visible = true;
                    this.panel3.Visible = false;
                    break;

                case 2:
                    this.panel1.Visible = false;
                    this.panel2.Visible = false;
                    this.panel3.Visible = false;
                    break;

                case 3:
                    this.panel1.Visible = false;
                    this.panel2.Visible = false;
                    this.panel3.Visible = true;
                    break;
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

        private void rdoRotationType_SelectedIndexChanged(object sender, EventArgs e)
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

        private void SetPlacementPriorities(string s, IPointPlacementPriorities Priorities)
        {
            Priorities.AboveLeft = this.GetNumber(s[0]);
            Priorities.AboveCenter = this.GetNumber(s[1]);
            Priorities.AboveRight = this.GetNumber(s[2]);
            Priorities.CenterLeft = this.GetNumber(s[3]);
            Priorities.CenterRight = this.GetNumber(s[5]);
            Priorities.BelowLeft = this.GetNumber(s[6]);
            Priorities.BelowCenter = this.GetNumber(s[7]);
            Priorities.BelowRight = this.GetNumber(s[8]);
        }

        public bool IsPageDirty
        {
            get { return this.m_IsPageDirty; }
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
            get { return ""; }
            set { }
        }
    }
}