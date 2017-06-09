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
    internal class FieldTypeGeometryCtrl1 : UserControl, IControlBaseInterface
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private bool bool_2 = false;
        private SimpleButton btnSetSR;
        private ComboBoxEdit cboAllowNull;
        private ComboBoxEdit cboGeometryType;
        private ComboBoxEdit cboHasM;
        private ComboBoxEdit cboHasZ;
        private ComboBoxEdit cboIsDefaultShapeField;
        private Container container_0 = null;
        private IFieldEdit ifieldEdit_0;
        private IGeometryDefEdit igeometryDefEdit_0;
        private IWorkspace iworkspace_0 = null;
        private string string_0 = "SHAPE";
        private TextEdit textEdit1;
        private TextEdit textEdit10;
        private TextEdit textEdit12;
        private TextEdit textEdit13;
        private TextEdit textEdit14;
        private TextEdit textEdit15;
        private TextEdit textEdit16;
        private TextEdit textEdit3;
        private TextEdit textEdit4;
        private TextEdit textEdit6;
        private TextEdit textEdit8;
        private TextEdit txtAlias;
        private TextEdit txtGrid1;
        private TextEdit txtGrid2;
        private TextEdit txtGrid3;
        private TextEdit txtPointCount;
        private TextEdit txtSpatialReference;

        public event ValueChangedHandler ValueChanged;

        public FieldTypeGeometryCtrl1()
        {
            this.InitializeComponent();
        }

        private void btnSetSR_Click(object sender, EventArgs e)
        {
            frmSpatialReference reference = new frmSpatialReference {
                SpatialRefrence = this.igeometryDefEdit_0.SpatialReference
            };
            if (reference.ShowDialog() == DialogResult.OK)
            {
                this.igeometryDefEdit_0.SpatialReference_2 = reference.SpatialRefrence;
                this.txtSpatialReference.Text = this.igeometryDefEdit_0.SpatialReference.Name;
                this.ifieldEdit_0.GeometryDef_2 = this.igeometryDefEdit_0;
            }
        }

        private void cboAllowNull_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.ifieldEdit_0.IsNullable_2 = this.cboAllowNull.SelectedIndex == 1;
            }
        }

        private void cboGeometryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                switch (this.cboGeometryType.SelectedIndex)
                {
                    case 0:
                        this.igeometryDefEdit_0.GeometryType_2 = esriGeometryType.esriGeometryPoint;
                        break;

                    case 1:
                        this.igeometryDefEdit_0.GeometryType_2 = esriGeometryType.esriGeometryPolygon;
                        break;

                    case 2:
                        this.igeometryDefEdit_0.GeometryType_2 = esriGeometryType.esriGeometryMultipoint;
                        break;

                    case 3:
                        this.igeometryDefEdit_0.GeometryType_2 = esriGeometryType.esriGeometryMultiPatch;
                        break;

                    case 4:
                        this.igeometryDefEdit_0.GeometryType_2 = esriGeometryType.esriGeometryPolyline;
                        break;
                }
            }
        }

        private void cboHasM_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.igeometryDefEdit_0.HasM_2 = this.cboHasM.SelectedIndex == 1;
            }
        }

        private void cboHasZ_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void cboHasZ_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.igeometryDefEdit_0.HasZ_2 = this.cboHasZ.SelectedIndex == 1;
            }
        }

        private void cboIsDefaultShapeField_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected override void Dispose(bool bool_3)
        {
            if (bool_3 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_3);
        }

        private void FieldTypeGeometryCtrl1_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

        private void FieldTypeGeometryCtrl1_VisibleChanged(object sender, EventArgs e)
        {
            if (base.Visible)
            {
                this.method_0();
            }
        }

        public void Init()
        {
        }

        private void InitializeComponent()
        {
            this.textEdit1 = new TextEdit();
            this.txtAlias = new TextEdit();
            this.textEdit4 = new TextEdit();
            this.cboAllowNull = new ComboBoxEdit();
            this.cboGeometryType = new ComboBoxEdit();
            this.textEdit3 = new TextEdit();
            this.txtPointCount = new TextEdit();
            this.textEdit6 = new TextEdit();
            this.txtGrid1 = new TextEdit();
            this.textEdit8 = new TextEdit();
            this.txtGrid2 = new TextEdit();
            this.textEdit10 = new TextEdit();
            this.txtGrid3 = new TextEdit();
            this.textEdit12 = new TextEdit();
            this.cboHasM = new ComboBoxEdit();
            this.textEdit13 = new TextEdit();
            this.cboHasZ = new ComboBoxEdit();
            this.textEdit14 = new TextEdit();
            this.textEdit15 = new TextEdit();
            this.cboIsDefaultShapeField = new ComboBoxEdit();
            this.textEdit16 = new TextEdit();
            this.txtSpatialReference = new TextEdit();
            this.btnSetSR = new SimpleButton();
            this.textEdit1.Properties.BeginInit();
            this.txtAlias.Properties.BeginInit();
            this.textEdit4.Properties.BeginInit();
            this.cboAllowNull.Properties.BeginInit();
            this.cboGeometryType.Properties.BeginInit();
            this.textEdit3.Properties.BeginInit();
            this.txtPointCount.Properties.BeginInit();
            this.textEdit6.Properties.BeginInit();
            this.txtGrid1.Properties.BeginInit();
            this.textEdit8.Properties.BeginInit();
            this.txtGrid2.Properties.BeginInit();
            this.textEdit10.Properties.BeginInit();
            this.txtGrid3.Properties.BeginInit();
            this.textEdit12.Properties.BeginInit();
            this.cboHasM.Properties.BeginInit();
            this.textEdit13.Properties.BeginInit();
            this.cboHasZ.Properties.BeginInit();
            this.textEdit14.Properties.BeginInit();
            this.textEdit15.Properties.BeginInit();
            this.cboIsDefaultShapeField.Properties.BeginInit();
            this.textEdit16.Properties.BeginInit();
            this.txtSpatialReference.Properties.BeginInit();
            base.SuspendLayout();
            this.textEdit1.EditValue = "别名";
            this.textEdit1.Location = new System.Drawing.Point(8, 8);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.AllowFocused = false;
            this.textEdit1.Properties.BorderStyle = BorderStyles.Simple;
            this.textEdit1.Properties.ReadOnly = true;
            this.textEdit1.Size = new Size(0x58, 0x15);
            this.textEdit1.TabIndex = 0;
            this.txtAlias.Location = new System.Drawing.Point(0x60, 8);
            this.txtAlias.Name = "txtAlias";
            this.txtAlias.Properties.BorderStyle = BorderStyles.Simple;
            this.txtAlias.Size = new Size(0x70, 0x15);
            this.txtAlias.TabIndex = 1;
            this.txtAlias.EditValueChanged += new EventHandler(this.txtAlias_EditValueChanged);
            this.textEdit4.EditValue = "允许空值";
            this.textEdit4.Location = new System.Drawing.Point(8, 0x1b);
            this.textEdit4.Name = "textEdit4";
            this.textEdit4.Properties.BorderStyle = BorderStyles.Simple;
            this.textEdit4.Properties.ReadOnly = true;
            this.textEdit4.Size = new Size(0x58, 0x15);
            this.textEdit4.TabIndex = 2;
            this.cboAllowNull.EditValue = "是";
            this.cboAllowNull.Location = new System.Drawing.Point(0x60, 0x1b);
            this.cboAllowNull.Name = "cboAllowNull";
            this.cboAllowNull.Properties.BorderStyle = BorderStyles.Simple;
            this.cboAllowNull.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboAllowNull.Properties.Items.AddRange(new object[] { "否", "是" });
            this.cboAllowNull.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            this.cboAllowNull.Size = new Size(0x70, 0x15);
            this.cboAllowNull.TabIndex = 4;
            this.cboAllowNull.SelectedIndexChanged += new EventHandler(this.cboAllowNull_SelectedIndexChanged);
            this.cboGeometryType.EditValue = "多边形";
            this.cboGeometryType.Location = new System.Drawing.Point(0x60, 0x2e);
            this.cboGeometryType.Name = "cboGeometryType";
            this.cboGeometryType.Properties.BorderStyle = BorderStyles.Simple;
            this.cboGeometryType.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboGeometryType.Properties.Items.AddRange(new object[] { "点", "多边形", "多点", "多面", "线" });
            this.cboGeometryType.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            this.cboGeometryType.Size = new Size(0x70, 0x15);
            this.cboGeometryType.TabIndex = 6;
            this.cboGeometryType.SelectedIndexChanged += new EventHandler(this.cboGeometryType_SelectedIndexChanged);
            this.textEdit3.EditValue = "几何类型";
            this.textEdit3.Location = new System.Drawing.Point(8, 0x2e);
            this.textEdit3.Name = "textEdit3";
            this.textEdit3.Properties.BorderStyle = BorderStyles.Simple;
            this.textEdit3.Properties.ReadOnly = true;
            this.textEdit3.Size = new Size(0x58, 0x15);
            this.textEdit3.TabIndex = 5;
            this.txtPointCount.EditValue = "0";
            this.txtPointCount.Location = new System.Drawing.Point(0x60, 0x41);
            this.txtPointCount.Name = "txtPointCount";
            this.txtPointCount.Properties.BorderStyle = BorderStyles.Simple;
            this.txtPointCount.Size = new Size(0x70, 0x15);
            this.txtPointCount.TabIndex = 8;
            this.txtPointCount.EditValueChanged += new EventHandler(this.txtPointCount_EditValueChanged);
            this.textEdit6.EditValue = "平均点数";
            this.textEdit6.Location = new System.Drawing.Point(8, 0x41);
            this.textEdit6.Name = "textEdit6";
            this.textEdit6.Properties.BorderStyle = BorderStyles.Simple;
            this.textEdit6.Properties.ReadOnly = true;
            this.textEdit6.Size = new Size(0x58, 0x15);
            this.textEdit6.TabIndex = 7;
            this.txtGrid1.EditValue = "1000";
            this.txtGrid1.Location = new System.Drawing.Point(0x60, 0x54);
            this.txtGrid1.Name = "txtGrid1";
            this.txtGrid1.Properties.BorderStyle = BorderStyles.Simple;
            this.txtGrid1.Size = new Size(0x70, 0x15);
            this.txtGrid1.TabIndex = 10;
            this.txtGrid1.EditValueChanged += new EventHandler(this.txtGrid1_EditValueChanged);
            this.textEdit8.EditValue = "Grid 1";
            this.textEdit8.Location = new System.Drawing.Point(8, 0x54);
            this.textEdit8.Name = "textEdit8";
            this.textEdit8.Properties.BorderStyle = BorderStyles.Simple;
            this.textEdit8.Properties.ReadOnly = true;
            this.textEdit8.Size = new Size(0x58, 0x15);
            this.textEdit8.TabIndex = 9;
            this.txtGrid2.EditValue = "0";
            this.txtGrid2.Location = new System.Drawing.Point(0x60, 0x67);
            this.txtGrid2.Name = "txtGrid2";
            this.txtGrid2.Properties.BorderStyle = BorderStyles.Simple;
            this.txtGrid2.Size = new Size(0x70, 0x15);
            this.txtGrid2.TabIndex = 12;
            this.txtGrid2.EditValueChanged += new EventHandler(this.txtGrid2_EditValueChanged);
            this.textEdit10.EditValue = "Grid 2";
            this.textEdit10.Location = new System.Drawing.Point(8, 0x67);
            this.textEdit10.Name = "textEdit10";
            this.textEdit10.Properties.BorderStyle = BorderStyles.Simple;
            this.textEdit10.Properties.ReadOnly = true;
            this.textEdit10.Size = new Size(0x58, 0x15);
            this.textEdit10.TabIndex = 11;
            this.txtGrid3.EditValue = "0";
            this.txtGrid3.Location = new System.Drawing.Point(0x60, 0x7a);
            this.txtGrid3.Name = "txtGrid3";
            this.txtGrid3.Properties.BorderStyle = BorderStyles.Simple;
            this.txtGrid3.Size = new Size(0x70, 0x15);
            this.txtGrid3.TabIndex = 14;
            this.txtGrid3.EditValueChanged += new EventHandler(this.txtGrid3_EditValueChanged);
            this.textEdit12.EditValue = "Grid 3";
            this.textEdit12.Location = new System.Drawing.Point(8, 0x7a);
            this.textEdit12.Name = "textEdit12";
            this.textEdit12.Properties.BorderStyle = BorderStyles.Simple;
            this.textEdit12.Properties.ReadOnly = true;
            this.textEdit12.Size = new Size(0x58, 0x15);
            this.textEdit12.TabIndex = 13;
            this.cboHasM.EditValue = "否";
            this.cboHasM.Location = new System.Drawing.Point(0x60, 160);
            this.cboHasM.Name = "cboHasM";
            this.cboHasM.Properties.BorderStyle = BorderStyles.Simple;
            this.cboHasM.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboHasM.Properties.Items.AddRange(new object[] { "否", "是" });
            this.cboHasM.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            this.cboHasM.Size = new Size(0x70, 0x15);
            this.cboHasM.TabIndex = 0x12;
            this.cboHasM.SelectedIndexChanged += new EventHandler(this.cboHasM_SelectedIndexChanged);
            this.textEdit13.EditValue = "包含M值";
            this.textEdit13.Location = new System.Drawing.Point(8, 160);
            this.textEdit13.Name = "textEdit13";
            this.textEdit13.Properties.BorderStyle = BorderStyles.Simple;
            this.textEdit13.Properties.ReadOnly = true;
            this.textEdit13.Size = new Size(0x58, 0x15);
            this.textEdit13.TabIndex = 0x11;
            this.cboHasZ.EditValue = "否";
            this.cboHasZ.Location = new System.Drawing.Point(0x60, 0x8d);
            this.cboHasZ.Name = "cboHasZ";
            this.cboHasZ.Properties.BorderStyle = BorderStyles.Simple;
            this.cboHasZ.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboHasZ.Properties.Items.AddRange(new object[] { "否", "是" });
            this.cboHasZ.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            this.cboHasZ.Size = new Size(0x70, 0x15);
            this.cboHasZ.TabIndex = 0x10;
            this.cboHasZ.EditValueChanged += new EventHandler(this.cboHasZ_EditValueChanged);
            this.cboHasZ.SelectedIndexChanged += new EventHandler(this.cboHasZ_SelectedIndexChanged);
            this.textEdit14.EditValue = "包含Z值";
            this.textEdit14.Location = new System.Drawing.Point(8, 0x8d);
            this.textEdit14.Name = "textEdit14";
            this.textEdit14.Properties.AllowFocused = false;
            this.textEdit14.Properties.BorderStyle = BorderStyles.Simple;
            this.textEdit14.Properties.ReadOnly = true;
            this.textEdit14.Size = new Size(0x58, 0x15);
            this.textEdit14.TabIndex = 15;
            this.textEdit15.EditValue = "空间参考";
            this.textEdit15.Location = new System.Drawing.Point(8, 0xc6);
            this.textEdit15.Name = "textEdit15";
            this.textEdit15.Properties.BorderStyle = BorderStyles.Simple;
            this.textEdit15.Properties.ReadOnly = true;
            this.textEdit15.Size = new Size(0x58, 0x15);
            this.textEdit15.TabIndex = 0x15;
            this.cboIsDefaultShapeField.EditValue = "是";
            this.cboIsDefaultShapeField.Location = new System.Drawing.Point(0x60, 0xb3);
            this.cboIsDefaultShapeField.Name = "cboIsDefaultShapeField";
            this.cboIsDefaultShapeField.Properties.BorderStyle = BorderStyles.Simple;
            this.cboIsDefaultShapeField.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboIsDefaultShapeField.Properties.Items.AddRange(new object[] { "否", "是" });
            this.cboIsDefaultShapeField.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            this.cboIsDefaultShapeField.Size = new Size(0x70, 0x15);
            this.cboIsDefaultShapeField.TabIndex = 20;
            this.cboIsDefaultShapeField.SelectedIndexChanged += new EventHandler(this.cboIsDefaultShapeField_SelectedIndexChanged);
            this.textEdit16.EditValue = "默认Shape字段";
            this.textEdit16.Location = new System.Drawing.Point(8, 0xb3);
            this.textEdit16.Name = "textEdit16";
            this.textEdit16.Properties.BorderStyle = BorderStyles.Simple;
            this.textEdit16.Properties.ReadOnly = true;
            this.textEdit16.Size = new Size(0x58, 0x15);
            this.textEdit16.TabIndex = 0x13;
            this.txtSpatialReference.EditValue = "Unknown";
            this.txtSpatialReference.Location = new System.Drawing.Point(0x60, 0xc6);
            this.txtSpatialReference.Name = "txtSpatialReference";
            this.txtSpatialReference.Properties.BorderStyle = BorderStyles.Simple;
            this.txtSpatialReference.Properties.ReadOnly = true;
            this.txtSpatialReference.Size = new Size(0x70, 0x15);
            this.txtSpatialReference.TabIndex = 0x16;
            this.btnSetSR.Location = new System.Drawing.Point(0xd0, 0xc7);
            this.btnSetSR.Name = "btnSetSR";
            this.btnSetSR.Size = new Size(0x18, 0x13);
            this.btnSetSR.TabIndex = 0x17;
            this.btnSetSR.Text = "...";
            this.btnSetSR.Click += new EventHandler(this.btnSetSR_Click);
            this.BackColor = SystemColors.Control;
            base.Controls.Add(this.btnSetSR);
            base.Controls.Add(this.txtSpatialReference);
            base.Controls.Add(this.textEdit15);
            base.Controls.Add(this.cboIsDefaultShapeField);
            base.Controls.Add(this.textEdit16);
            base.Controls.Add(this.cboHasM);
            base.Controls.Add(this.textEdit13);
            base.Controls.Add(this.cboHasZ);
            base.Controls.Add(this.textEdit14);
            base.Controls.Add(this.txtGrid3);
            base.Controls.Add(this.textEdit12);
            base.Controls.Add(this.txtGrid2);
            base.Controls.Add(this.textEdit10);
            base.Controls.Add(this.txtGrid1);
            base.Controls.Add(this.textEdit8);
            base.Controls.Add(this.txtPointCount);
            base.Controls.Add(this.textEdit6);
            base.Controls.Add(this.cboGeometryType);
            base.Controls.Add(this.textEdit3);
            base.Controls.Add(this.cboAllowNull);
            base.Controls.Add(this.textEdit4);
            base.Controls.Add(this.txtAlias);
            base.Controls.Add(this.textEdit1);
            base.Name = "FieldTypeGeometryCtrl1";
            base.Size = new Size(240, 0xe8);
            base.VisibleChanged += new EventHandler(this.FieldTypeGeometryCtrl1_VisibleChanged);
            base.Load += new EventHandler(this.FieldTypeGeometryCtrl1_Load);
            this.textEdit1.Properties.EndInit();
            this.txtAlias.Properties.EndInit();
            this.textEdit4.Properties.EndInit();
            this.cboAllowNull.Properties.EndInit();
            this.cboGeometryType.Properties.EndInit();
            this.textEdit3.Properties.EndInit();
            this.txtPointCount.Properties.EndInit();
            this.textEdit6.Properties.EndInit();
            this.txtGrid1.Properties.EndInit();
            this.textEdit8.Properties.EndInit();
            this.txtGrid2.Properties.EndInit();
            this.textEdit10.Properties.EndInit();
            this.txtGrid3.Properties.EndInit();
            this.textEdit12.Properties.EndInit();
            this.cboHasM.Properties.EndInit();
            this.textEdit13.Properties.EndInit();
            this.cboHasZ.Properties.EndInit();
            this.textEdit14.Properties.EndInit();
            this.textEdit15.Properties.EndInit();
            this.cboIsDefaultShapeField.Properties.EndInit();
            this.textEdit16.Properties.EndInit();
            this.txtSpatialReference.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void method_0()
        {
            this.bool_0 = false;
            this.txtAlias.Text = this.ifieldEdit_0.AliasName;
            this.cboAllowNull.SelectedIndex = Convert.ToInt32(this.ifieldEdit_0.IsNullable);
            if (this.igeometryDefEdit_0 != null)
            {
                switch (this.igeometryDefEdit_0.GeometryType)
                {
                    case esriGeometryType.esriGeometryPoint:
                        this.cboGeometryType.SelectedIndex = 0;
                        break;

                    case esriGeometryType.esriGeometryMultipoint:
                        this.cboGeometryType.SelectedIndex = 2;
                        break;

                    case esriGeometryType.esriGeometryPolyline:
                        this.cboGeometryType.SelectedIndex = 4;
                        break;

                    case esriGeometryType.esriGeometryPolygon:
                        this.cboGeometryType.SelectedIndex = 1;
                        break;

                    case esriGeometryType.esriGeometryMultiPatch:
                        this.cboGeometryType.SelectedIndex = 3;
                        break;
                }
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
                this.cboHasZ.SelectedIndex = Convert.ToInt32(this.igeometryDefEdit_0.HasZ);
                this.cboHasM.SelectedIndex = Convert.ToInt32(this.igeometryDefEdit_0.HasM);
                this.txtSpatialReference.Text = this.igeometryDefEdit_0.SpatialReference.Name;
                if (this.string_0 == this.ifieldEdit_0.Name)
                {
                    this.cboIsDefaultShapeField.SelectedIndex = 1;
                }
                else
                {
                    this.cboIsDefaultShapeField.SelectedIndex = 0;
                }
                this.txtPointCount.Properties.ReadOnly = this.bool_2;
                this.txtGrid1.Properties.ReadOnly = this.bool_2;
                this.txtGrid2.Properties.ReadOnly = this.bool_2;
                this.txtGrid3.Properties.ReadOnly = this.bool_2;
                this.cboAllowNull.Enabled = !this.bool_2;
                this.cboGeometryType.Enabled = !this.bool_2;
                this.cboHasZ.Enabled = !this.bool_2;
                this.cboHasM.Enabled = !this.bool_2;
                this.cboIsDefaultShapeField.Enabled = !this.bool_2;
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
            set
            {
                this.bool_2 = value;
            }
        }

        public string ShapfileName
        {
            set
            {
                this.string_0 = value;
            }
        }

        public IWorkspace Workspace
        {
            set
            {
                this.iworkspace_0 = value;
            }
        }
    }
}

