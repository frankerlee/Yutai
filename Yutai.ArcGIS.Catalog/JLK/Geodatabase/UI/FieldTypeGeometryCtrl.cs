namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.Geodatabase;
    using ESRI.ArcGIS.Geometry;
    using JLK.Editors;
    using JLK.Editors.Controls;
    using JLK.Utility.BaseClass;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    internal class FieldTypeGeometryCtrl : UserControl, IControlBaseInterface
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private bool bool_2 = false;
        private ComboBoxEdit cboAllowNull;
        private Container container_0 = null;
        private IFieldEdit ifieldEdit_0;
        private IGeometryDefEdit igeometryDefEdit_0;
        private IWorkspace iworkspace_0 = null;
        private string string_0 = "SHAPE";
        private TextEdit textEdit1;
        private TextEdit textEdit10;
        private TextEdit textEdit12;
        private TextEdit textEdit3;
        private TextEdit textEdit4;
        private TextEdit textEdit6;
        private TextEdit textEdit8;
        private TextEdit txtAlias;
        private TextEdit txtGeometryType;
        private TextEdit txtGrid1;
        private TextEdit txtGrid2;
        private TextEdit txtGrid3;
        private TextEdit txtPointCount;

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
                this.ifieldEdit_0.IsNullable = this.cboAllowNull.SelectedIndex == 1;
            }
        }

        protected override void Dispose(bool bool_3)
        {
            if (bool_3 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_3);
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

        private void InitializeComponent()
        {
            this.textEdit1 = new TextEdit();
            this.txtAlias = new TextEdit();
            this.textEdit4 = new TextEdit();
            this.cboAllowNull = new ComboBoxEdit();
            this.textEdit3 = new TextEdit();
            this.txtPointCount = new TextEdit();
            this.textEdit6 = new TextEdit();
            this.txtGrid1 = new TextEdit();
            this.textEdit8 = new TextEdit();
            this.txtGrid2 = new TextEdit();
            this.textEdit10 = new TextEdit();
            this.txtGrid3 = new TextEdit();
            this.textEdit12 = new TextEdit();
            this.txtGeometryType = new TextEdit();
            this.textEdit1.Properties.BeginInit();
            this.txtAlias.Properties.BeginInit();
            this.textEdit4.Properties.BeginInit();
            this.cboAllowNull.Properties.BeginInit();
            this.textEdit3.Properties.BeginInit();
            this.txtPointCount.Properties.BeginInit();
            this.textEdit6.Properties.BeginInit();
            this.txtGrid1.Properties.BeginInit();
            this.textEdit8.Properties.BeginInit();
            this.txtGrid2.Properties.BeginInit();
            this.textEdit10.Properties.BeginInit();
            this.txtGrid3.Properties.BeginInit();
            this.textEdit12.Properties.BeginInit();
            this.txtGeometryType.Properties.BeginInit();
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
            this.txtGeometryType.EditValue = "";
            this.txtGeometryType.Enabled = false;
            this.txtGeometryType.Location = new System.Drawing.Point(0x60, 0x2e);
            this.txtGeometryType.Name = "txtGeometryType";
            this.txtGeometryType.Properties.BorderStyle = BorderStyles.Simple;
            this.txtGeometryType.Properties.ReadOnly = true;
            this.txtGeometryType.Size = new Size(0x70, 0x15);
            this.txtGeometryType.TabIndex = 0x16;
            this.BackColor = SystemColors.Control;
            base.Controls.Add(this.txtGeometryType);
            base.Controls.Add(this.txtGrid3);
            base.Controls.Add(this.textEdit12);
            base.Controls.Add(this.txtGrid2);
            base.Controls.Add(this.textEdit10);
            base.Controls.Add(this.txtGrid1);
            base.Controls.Add(this.textEdit8);
            base.Controls.Add(this.txtPointCount);
            base.Controls.Add(this.textEdit6);
            base.Controls.Add(this.textEdit3);
            base.Controls.Add(this.cboAllowNull);
            base.Controls.Add(this.textEdit4);
            base.Controls.Add(this.txtAlias);
            base.Controls.Add(this.textEdit1);
            base.Name = "FieldTypeGeometryCtrl";
            base.Size = new Size(240, 0xa6);
            base.VisibleChanged += new EventHandler(this.FieldTypeGeometryCtrl_VisibleChanged);
            base.Load += new EventHandler(this.FieldTypeGeometryCtrl_Load);
            this.textEdit1.Properties.EndInit();
            this.txtAlias.Properties.EndInit();
            this.textEdit4.Properties.EndInit();
            this.cboAllowNull.Properties.EndInit();
            this.textEdit3.Properties.EndInit();
            this.txtPointCount.Properties.EndInit();
            this.textEdit6.Properties.EndInit();
            this.txtGrid1.Properties.EndInit();
            this.textEdit8.Properties.EndInit();
            this.txtGrid2.Properties.EndInit();
            this.textEdit10.Properties.EndInit();
            this.txtGrid3.Properties.EndInit();
            this.textEdit12.Properties.EndInit();
            this.txtGeometryType.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void method_0(IField ifield_0, FieldChangeType fieldChangeType_0)
        {
            if (this.fieldChangedHandler_0 != null)
            {
                this.fieldChangedHandler_0(ifield_0, fieldChangeType_0);
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
                this.ifieldEdit_0.AliasName = this.txtAlias.Text;
                if (this.valueChangedHandler_0 != null)
                {
                    this.valueChangedHandler_0(this, e);
                }
                this.method_0(this.ifieldEdit_0, FieldChangeType.FCTAlias);
            }
        }

        private void txtGrid1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                if (Common.IsNmuber(this.txtGrid1.Text))
                {
                    this.txtGrid1.ForeColor = SystemColors.WindowText;
                    if (this.igeometryDefEdit_0.GridCount < 1)
                    {
                        this.igeometryDefEdit_0.GridCount = 1;
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
                if (Common.IsNmuber(this.txtGrid2.Text))
                {
                    this.txtGrid2.ForeColor = SystemColors.WindowText;
                    if (this.igeometryDefEdit_0.GridCount < 2)
                    {
                        this.igeometryDefEdit_0.GridCount = 2;
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
                if (Common.IsNmuber(this.txtGrid3.Text))
                {
                    this.txtGrid3.ForeColor = SystemColors.WindowText;
                    if (this.igeometryDefEdit_0.GridCount < 3)
                    {
                        this.igeometryDefEdit_0.GridCount = 3;
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

