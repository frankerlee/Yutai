using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
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
                double num = Convert.ToDouble(this.txtW.Text);
                double num2 = Convert.ToDouble(this.txtH.Text);
                if ((num == 0.0) && (num2 == 0.0))
                {
                    num = 1.0;
                }
                if (this.mapTemplateElement_0.Element is IRectangleElement)
                {
                    IEnvelope envelope = this.mapTemplateElement_0.Element.Geometry.Envelope;
                    if (num <= 0.0)
                    {
                        num = 1.0;
                    }
                    if (num2 <= 0.0)
                    {
                        num = 1.0;
                    }
                    envelope.XMax = envelope.XMin + num;
                    envelope.YMax = envelope.YMin + num2;
                    this.mapTemplateElement_0.Element.Geometry = envelope;
                }
                this.mapTemplateElement_0.ElementLocation.XOffset = Convert.ToDouble(this.txtOffsetX.Text);
                this.mapTemplateElement_0.ElementLocation.YOffset = Convert.ToDouble(this.txtOffsetY.Text);
                if (this.rdoLeftCenter.Checked)
                {
                    this.mapTemplateElement_0.ElementLocation.LocationType = LocationType.LeftCenter;
                }
                else if (this.rdoLeftLow.Checked)
                {
                    this.mapTemplateElement_0.ElementLocation.LocationType = LocationType.LeftLower;
                }
                else if (this.rdoLeftUpper.Checked)
                {
                    this.mapTemplateElement_0.ElementLocation.LocationType = LocationType.LeftUpper;
                }
                else if (this.rdoLowCenter.Checked)
                {
                    this.mapTemplateElement_0.ElementLocation.LocationType = LocationType.LowerCenter;
                }
                else if (this.rdoLowLeft.Checked)
                {
                    this.mapTemplateElement_0.ElementLocation.LocationType = LocationType.LowerLeft;
                }
                else if (this.rdoLowRight.Checked)
                {
                    this.mapTemplateElement_0.ElementLocation.LocationType = LocationType.LowerRight;
                }
                else if (this.rdoRightCenter.Checked)
                {
                    this.mapTemplateElement_0.ElementLocation.LocationType = LocationType.RightCenter;
                }
                else if (this.rdoRightLow.Checked)
                {
                    this.mapTemplateElement_0.ElementLocation.LocationType = LocationType.RightLower;
                }
                else if (this.rdoRightUpper.Checked)
                {
                    this.mapTemplateElement_0.ElementLocation.LocationType = LocationType.RightUpper;
                }
                else if (this.rdoUpperCenter.Checked)
                {
                    this.mapTemplateElement_0.ElementLocation.LocationType = LocationType.UpperrCenter;
                }
                else if (this.rdoUpperLeft.Checked)
                {
                    this.mapTemplateElement_0.ElementLocation.LocationType = LocationType.UpperLeft;
                }
                else if (this.rdoUpperRight.Checked)
                {
                    this.mapTemplateElement_0.ElementLocation.LocationType = LocationType.UpperRight;
                }
            }
        }

        public void Cancel()
        {
            this.bool_1 = false;
        }

 private void ElementPosition_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

 private void method_0()
        {
            this.bool_0 = false;
            switch (this.mapTemplateElement_0.ElementLocation.LocationType)
            {
                case LocationType.UpperLeft:
                    this.rdoUpperLeft.Checked = true;
                    break;

                case LocationType.UpperrCenter:
                    this.rdoUpperCenter.Checked = true;
                    break;

                case LocationType.UpperRight:
                    this.rdoUpperRight.Checked = true;
                    break;

                case LocationType.LeftUpper:
                    this.rdoLeftUpper.Checked = true;
                    break;

                case LocationType.RightUpper:
                    this.rdoRightUpper.Checked = true;
                    break;

                case LocationType.LeftCenter:
                    this.rdoLeftCenter.Checked = true;
                    break;

                case LocationType.RightCenter:
                    this.rdoRightCenter.Checked = true;
                    break;

                case LocationType.LeftLower:
                    this.rdoLeftLow.Checked = true;
                    break;

                case LocationType.RightLower:
                    this.rdoRightLow.Checked = true;
                    break;

                case LocationType.LowerLeft:
                    this.rdoLowLeft.Checked = true;
                    break;

                case LocationType.LowerCenter:
                    this.rdoLowCenter.Checked = true;
                    break;

                case LocationType.LowerRight:
                    this.rdoLowRight.Checked = true;
                    break;
            }
            this.txtOffsetX.Text = this.mapTemplateElement_0.ElementLocation.XOffset.ToString("0.##");
            this.txtOffsetY.Text = this.mapTemplateElement_0.ElementLocation.YOffset.ToString("0.##");
            if (this.mapTemplateElement_0.Element is IRectangleElement)
            {
                this.groupBox2.Visible = true;
                IEnvelope envelope = this.mapTemplateElement_0.Element.Geometry.Envelope;
                if (!envelope.IsEmpty)
                {
                    this.txtH.Text = envelope.Height.ToString();
                    this.txtW.Text = envelope.Width.ToString();
                }
            }
            this.bool_0 = true;
        }

        private void rdoLeftUpper_CheckedChanged(object sender, EventArgs e)
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

        private void rdoLowRight_CheckedChanged(object sender, EventArgs e)
        {
        }

        public void ResetControl()
        {
            this.bool_0 = false;
            this.method_0();
            this.bool_0 = true;
        }

        public void SetObjects(object object_0)
        {
            this.mapTemplateElement_0 = object_0 as MapCartoTemplateLib.MapTemplateElement;
        }

        private void txtH_TextChanged(object sender, EventArgs e)
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

        private void txtW_TextChanged(object sender, EventArgs e)
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

        public MapCartoTemplateLib.MapTemplateElement MapTemplateElement
        {
            set
            {
                this.mapTemplateElement_0 = value;
                this.method_0();
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

