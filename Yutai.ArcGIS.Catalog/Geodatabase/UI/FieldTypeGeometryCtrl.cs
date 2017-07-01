using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Helpers;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal partial class FieldTypeGeometryCtrl : UserControl, IControlBaseInterface
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private bool bool_2 = false;
        private Container container_0 = null;
        private IWorkspace iworkspace_0 = null;
        private string string_0 = "SHAPE";

        public event FieldChangedHandler FieldChanged;

        public event ValueChangedHandler ValueChanged;

        public FieldTypeGeometryCtrl()
        {
            this.InitializeComponent();
        }

        private void cboAllowNull_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.ifieldEdit_0.IsNullable_2 = this.cboAllowNull.SelectedIndex == 1;
            }
        }

        private void FieldTypeGeometryCtrl_Load(object sender, EventArgs e)
        {
            this.method_3();
        }

        private void FieldTypeGeometryCtrl_VisibleChanged(object sender, EventArgs e)
        {
            if (base.Visible)
            {
                this.method_3();
            }
        }

        public void Init()
        {
        }

        private void method_0(IField ifield_0, FieldChangeType fieldChangeType_0)
        {
            if (this.FieldChanged != null)
            {
                this.FieldChanged(ifield_0, fieldChangeType_0);
            }
        }

        private void method_1(object sender, EventArgs e)
        {
        }

        private void method_2(object sender, EventArgs e)
        {
        }

        private void method_3()
        {
            this.bool_0 = false;
            if (ObjectClassShareData.m_IsShapeFile)
            {
                this.txtAlias.Enabled = false;
            }
            this.txtAlias.Text = this.ifieldEdit_0.AliasName;
            this.cboAllowNull.SelectedIndex = Convert.ToInt32(this.ifieldEdit_0.IsNullable);
            if (this.igeometryDefEdit_0 != null)
            {
                this.txtPointCount.Text = this.igeometryDefEdit_0.AvgNumPoints.ToString();
                this.txtGrid1.Text = "0";
                this.txtGrid2.Text = "0";
                this.txtGrid3.Text = "0";
                if (this.igeometryDefEdit_0.GridCount > 0)
                {
                    this.txtGrid1.Text = this.igeometryDefEdit_0.get_GridSize(0).ToString();
                }
                if (this.igeometryDefEdit_0.GridCount > 1)
                {
                    this.txtGrid2.Text = this.igeometryDefEdit_0.get_GridSize(1).ToString();
                }
                if (this.igeometryDefEdit_0.GridCount > 2)
                {
                    this.txtGrid2.Text = this.igeometryDefEdit_0.get_GridSize(2).ToString();
                }
                switch (this.igeometryDefEdit_0.GeometryType)
                {
                    case esriGeometryType.esriGeometryPoint:
                        this.txtGeometryType.Text = "多点";
                        break;

                    case esriGeometryType.esriGeometryMultipoint:
                        this.txtGeometryType.Text = "多点";
                        break;

                    case esriGeometryType.esriGeometryPolyline:
                        this.txtGeometryType.Text = "线";
                        break;

                    case esriGeometryType.esriGeometryPolygon:
                        this.txtGeometryType.Text = "面";
                        break;

                    case esriGeometryType.esriGeometryMultiPatch:
                        this.txtGeometryType.Text = "面片";
                        break;
                }
                this.txtPointCount.Properties.ReadOnly = this.bool_2;
                this.txtGrid1.Properties.ReadOnly = this.bool_2;
                this.txtGrid2.Properties.ReadOnly = this.bool_2;
                this.txtGrid3.Properties.ReadOnly = this.bool_2;
                this.cboAllowNull.Enabled = !this.bool_2;
                this.bool_0 = true;
            }
        }

        private void txtAlias_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.ifieldEdit_0.AliasName_2 = this.txtAlias.Text;
                if (this.ValueChanged != null)
                {
                    this.ValueChanged(this, e);
                }
                this.method_0(this.ifieldEdit_0, FieldChangeType.FCTAlias);
            }
        }

        private void txtGrid1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                if (CommonHelper.IsNmuber(this.txtGrid1.Text))
                {
                    this.txtGrid1.ForeColor = SystemColors.WindowText;
                    if (this.igeometryDefEdit_0.GridCount < 1)
                    {
                        this.igeometryDefEdit_0.GridCount_2 = 1;
                    }
                    this.igeometryDefEdit_0.set_GridSize(0, Convert.ToDouble(this.txtGrid1.Text));
                }
                else
                {
                    this.txtGrid1.ForeColor = Color.Red;
                }
            }
        }

        private void txtGrid2_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                if (CommonHelper.IsNmuber(this.txtGrid2.Text))
                {
                    this.txtGrid2.ForeColor = SystemColors.WindowText;
                    if (this.igeometryDefEdit_0.GridCount < 2)
                    {
                        this.igeometryDefEdit_0.GridCount_2 = 2;
                    }
                    this.igeometryDefEdit_0.set_GridSize(1, Convert.ToDouble(this.txtGrid2.Text));
                }
                else
                {
                    this.txtGrid2.ForeColor = Color.Red;
                }
            }
        }

        private void txtGrid3_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                if (CommonHelper.IsNmuber(this.txtGrid3.Text))
                {
                    this.txtGrid3.ForeColor = SystemColors.WindowText;
                    if (this.igeometryDefEdit_0.GridCount < 3)
                    {
                        this.igeometryDefEdit_0.GridCount_2 = 3;
                    }
                    this.igeometryDefEdit_0.set_GridSize(2, Convert.ToDouble(this.txtGrid3.Text));
                }
                else
                {
                    this.txtGrid3.ForeColor = Color.Red;
                }
            }
        }

        private void txtPointCount_EditValueChanged(object sender, EventArgs e)
        {
        }

        public IField Filed
        {
            set
            {
                this.ifieldEdit_0 = value as IFieldEdit;
                this.igeometryDefEdit_0 = this.ifieldEdit_0.GeometryDef as IGeometryDefEdit;
            }
        }

        public bool IsEdit
        {
            set { this.bool_2 = value; }
        }

        public string ShapfileName
        {
            set { this.string_0 = value; }
        }

        public IWorkspace Workspace
        {
            set { this.iworkspace_0 = value; }
        }
    }
}