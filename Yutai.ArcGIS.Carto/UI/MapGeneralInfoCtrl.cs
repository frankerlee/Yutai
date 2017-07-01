using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class MapGeneralInfoCtrl : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Container container_0 = null;
        private IBasicMap ibasicMap_0 = null;
        private string string_0 = "常规";

        public event OnValueChangeEventHandler OnValueChange;

        public MapGeneralInfoCtrl()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                this.ibasicMap_0.Name = this.txtName.Text;
                if (this.ibasicMap_0 is IMap)
                {
                    string[] strArray = this.txtRefrenceScale.Text.Split(new char[] {':'});
                    try
                    {
                        (this.ibasicMap_0 as IMap).ReferenceScale = double.Parse(strArray[strArray.Length - 1]);
                    }
                    catch
                    {
                    }
                    try
                    {
                        (this.ibasicMap_0 as IActiveView).ScreenDisplay.DisplayTransformation.Rotation =
                            double.Parse(this.txtRotate.Text);
                    }
                    catch
                    {
                    }
                    (this.ibasicMap_0 as IMap).MapUnits = (esriUnits) this.cboMapUnit.SelectedIndex;
                    (this.ibasicMap_0 as IMap).DistanceUnits = (esriUnits) this.cboDisplayUnit.SelectedIndex;
                }
                this.ibasicMap_0.Description = this.txtDes.Text;
            }
        }

        public void Cancel()
        {
        }

        private void cboDisplayUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.method_1();
        }

        private void cboMapUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboMapUnit.SelectedIndex == 0)
            {
                this.cboDisplayUnit.Enabled = false;
            }
            else
            {
                this.cboDisplayUnit.Enabled = true;
            }
            this.method_1();
        }

        private void MapGeneralInfoCtrl_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

        private void method_0()
        {
            this.txtName.Text = this.ibasicMap_0.Name;
            if (this.ibasicMap_0 is IMap)
            {
                try
                {
                    this.txtRefrenceScale.Text = (this.ibasicMap_0 as IMap).ReferenceScale.ToString();
                }
                catch
                {
                }
                if (this.bool_0)
                {
                    this.cboMapUnit.Enabled = true;
                }
                else
                {
                    this.cboMapUnit.Enabled = false;
                }
                this.cboMapUnit.SelectedIndex = (int) (this.ibasicMap_0 as IMap).MapUnits;
                this.cboDisplayUnit.Enabled = this.cboMapUnit.SelectedIndex > 0;
                this.cboDisplayUnit.SelectedIndex = (int) (this.ibasicMap_0 as IMap).DistanceUnits;
                this.txtRotate.Text =
                    (this.ibasicMap_0 as IActiveView).ScreenDisplay.DisplayTransformation.Rotation.ToString();
            }
            else
            {
                this.txtRefrenceScale.Visible = false;
                this.cboMapUnit.Visible = false;
                this.cboDisplayUnit.Visible = false;
                this.txtRotate.Visible = false;
                this.groupBox1.Visible = false;
                this.label7.Visible = false;
                this.label6.Visible = false;
                this.label5.Visible = false;
                this.cboLabelEngine.Visible = false;
            }
            this.txtDes.Text = this.ibasicMap_0.Description;
        }

        private void method_1()
        {
            this.bool_1 = true;
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
            }
        }

        public void ResetControl()
        {
        }

        public void SetObjects(object object_0)
        {
            if (object_0 is IBasicMap)
            {
                this.ibasicMap_0 = object_0 as IBasicMap;
            }
            else
            {
                if (!(object_0 is IMapFrame))
                {
                    return;
                }
                this.ibasicMap_0 = (object_0 as IMapFrame).Map as IBasicMap;
            }
            if (this.ibasicMap_0.SpatialReference is IUnknownCoordinateSystem)
            {
                this.bool_0 = true;
            }
            else
            {
                this.bool_0 = false;
            }
        }

        private void txtDes_EditValueChanged(object sender, EventArgs e)
        {
            this.method_1();
        }

        private void txtName_EditValueChanged(object sender, EventArgs e)
        {
            this.method_1();
        }

        private void txtRefrenceScale_EditValueChanged(object sender, EventArgs e)
        {
            this.method_1();
        }

        private void txtRotate_EditValueChanged(object sender, EventArgs e)
        {
            this.method_1();
        }

        public IBasicMap FocusMap
        {
            set
            {
                this.ibasicMap_0 = value;
                if (this.ibasicMap_0.SpatialReference == null)
                {
                    this.bool_0 = true;
                }
                else if (this.ibasicMap_0.SpatialReference is IUnknownCoordinateSystem)
                {
                    this.bool_0 = true;
                }
                else
                {
                    this.bool_0 = false;
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

        public string Title
        {
            get { return this.string_0; }
            set { this.string_0 = value; }
        }
    }
}