namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.Geometry;
    using JLK.Editors;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;

    internal class VCSCoordinateInfoPage : UserControl
    {
        private bool bool_0 = true;
        private SimpleButton btnClear;
        private SimpleButton btnImport;
        private SimpleButton btnModify;
        private SimpleButton btnNew;
        private SimpleButton btnSaveAs;
        private SimpleButton btnSelect;
        private IContainer icontainer_0 = null;
        private ISpatialReferenceFactory ispatialReferenceFactory_0 = new SpatialReferenceEnvironmentClass();
        private IVerticalCoordinateSystem iverticalCoordinateSystem_0;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private MemoEdit textBoxDetail;
        private TextEdit textBoxName;

        public event SpatialReferenceChangedHandler SpatialReferenceChanged;

        public VCSCoordinateInfoPage()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void InitializeComponent()
        {
            this.label8 = new Label();
            this.btnImport = new SimpleButton();
            this.label7 = new Label();
            this.btnSaveAs = new SimpleButton();
            this.label6 = new Label();
            this.label5 = new Label();
            this.label4 = new Label();
            this.label3 = new Label();
            this.btnModify = new SimpleButton();
            this.btnClear = new SimpleButton();
            this.btnNew = new SimpleButton();
            this.btnSelect = new SimpleButton();
            this.textBoxName = new TextEdit();
            this.textBoxDetail = new MemoEdit();
            this.label2 = new Label();
            this.label1 = new Label();
            this.textBoxName.Properties.BeginInit();
            this.textBoxDetail.Properties.BeginInit();
            base.SuspendLayout();
            this.label8.Location = new System.Drawing.Point(0x4d, 0x11c);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0xd0, 0x20);
            this.label8.TabIndex = 0x27;
            this.label8.Text = "从已存在的数据中导入坐标系统和Z域值";
            this.label8.Visible = false;
            this.btnImport.Location = new System.Drawing.Point(13, 0x11c);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new Size(0x38, 0x18);
            this.btnImport.TabIndex = 0x26;
            this.btnImport.Text = "导入";
            this.btnImport.Visible = false;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(0x4d, 0x1a2);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x71, 12);
            this.label7.TabIndex = 0x25;
            this.label7.Text = "保存坐标系统到文件";
            this.label7.Visible = false;
            this.btnSaveAs.Location = new System.Drawing.Point(13, 0x19c);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new Size(0x38, 0x18);
            this.btnSaveAs.TabIndex = 0x24;
            this.btnSaveAs.Text = "另存为...";
            this.btnSaveAs.Visible = false;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(0x4d, 0x182);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x89, 12);
            this.label6.TabIndex = 0x23;
            this.label6.Text = "设置垂直坐标系统到未知";
            this.label6.Visible = false;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0x4d, 0x161);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0xa1, 12);
            this.label5.TabIndex = 0x22;
            this.label5.Text = "编辑当前选择的坐标系统属性";
            this.label5.Visible = false;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0x4d, 0x141);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x65, 12);
            this.label4.TabIndex = 0x21;
            this.label4.Text = "新建一个坐标系统";
            this.label4.Visible = false;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0x4d, 0x101);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x95, 12);
            this.label3.TabIndex = 0x20;
            this.label3.Text = "选择一个预定义的坐标系统";
            this.label3.Visible = false;
            this.btnModify.Location = new System.Drawing.Point(13, 0x15c);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new Size(0x38, 0x18);
            this.btnModify.TabIndex = 0x1f;
            this.btnModify.Text = "修改...";
            this.btnModify.Visible = false;
            this.btnClear.Location = new System.Drawing.Point(13, 380);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new Size(0x38, 0x18);
            this.btnClear.TabIndex = 30;
            this.btnClear.Text = "清空";
            this.btnClear.Visible = false;
            this.btnNew.Location = new System.Drawing.Point(13, 0x13c);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new Size(0x38, 0x18);
            this.btnNew.TabIndex = 0x1d;
            this.btnNew.Text = "新建...";
            this.btnNew.Visible = false;
            this.btnSelect.Location = new System.Drawing.Point(13, 0xfc);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new Size(0x38, 0x18);
            this.btnSelect.TabIndex = 0x1c;
            this.btnSelect.Text = "选择";
            this.btnSelect.Visible = false;
            this.textBoxName.EditValue = "";
            this.textBoxName.Location = new System.Drawing.Point(0x35, 12);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Properties.ReadOnly = true;
            this.textBoxName.Size = new Size(0xd8, 0x15);
            this.textBoxName.TabIndex = 0x1b;
            this.textBoxDetail.EditValue = "";
            this.textBoxDetail.Location = new System.Drawing.Point(13, 60);
            this.textBoxDetail.Name = "textBoxDetail";
            this.textBoxDetail.Properties.Appearance.BackColor = SystemColors.InactiveBorder;
            this.textBoxDetail.Properties.Appearance.Options.UseBackColor = true;
            this.textBoxDetail.Properties.ReadOnly = true;
            this.textBoxDetail.Size = new Size(0x108, 0xb8);
            this.textBoxDetail.TabIndex = 0x1a;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 0x2c);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 0x19;
            this.label2.Text = "详细信息";
            this.label1.AllowDrop = true;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 14);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 12);
            this.label1.TabIndex = 0x18;
            this.label1.Text = "名字:";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.label8);
            base.Controls.Add(this.btnImport);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.btnSaveAs);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.btnModify);
            base.Controls.Add(this.btnClear);
            base.Controls.Add(this.btnNew);
            base.Controls.Add(this.btnSelect);
            base.Controls.Add(this.textBoxName);
            base.Controls.Add(this.textBoxDetail);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "VCSCoordinateInfoPage";
            base.Size = new Size(0x157, 0x1bc);
            base.Load += new EventHandler(this.VCSCoordinateInfoPage_Load);
            this.textBoxName.Properties.EndInit();
            this.textBoxDetail.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private string method_0(ISpatialReferenceInfo ispatialReferenceInfo_0)
        {
            IGeographicCoordinateSystem geographicCoordinateSystem;
            string str2;
            this.textBoxName.Text = ispatialReferenceInfo_0.Name;
            if (ispatialReferenceInfo_0 is IVerticalCoordinateSystem)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("线性单位:");
                builder.Append((ispatialReferenceInfo_0 as IVerticalCoordinateSystem).CoordinateUnit.Name);
                builder.Append("\r\n");
                if ((ispatialReferenceInfo_0 as IVerticalCoordinateSystem).PositiveDirection == -1)
                {
                    builder.Append("方向:正\r\n");
                }
                else
                {
                    builder.Append("方向:负\r\n");
                }
                builder.Append("垂直偏移:");
                builder.Append((ispatialReferenceInfo_0 as IVerticalCoordinateSystem).VerticalShift.ToString());
                builder.Append("\r\n");
                builder.Append("垂直坐标框架:");
                builder.Append(((ispatialReferenceInfo_0 as IVerticalCoordinateSystem).Datum as ISpatialReferenceInfo).Name);
                return builder.ToString();
            }
            if (ispatialReferenceInfo_0 is IGeographicCoordinateSystem)
            {
                geographicCoordinateSystem = (IGeographicCoordinateSystem) ispatialReferenceInfo_0;
                str2 = ("别名: " + geographicCoordinateSystem.Alias + "\r\n") + "缩略名: " + geographicCoordinateSystem.Abbreviation + "\r\n";
                string[] strArray = new string[] { 
                    str2, "说明: ", geographicCoordinateSystem.Remarks, "\r\n角度单位: ", geographicCoordinateSystem.CoordinateUnit.Name, " (", geographicCoordinateSystem.CoordinateUnit.RadiansPerUnit.ToString(), ")\r\n本初子午线: ", geographicCoordinateSystem.PrimeMeridian.Name, " (", geographicCoordinateSystem.PrimeMeridian.Longitude.ToString(), ")\r\n数据: ", geographicCoordinateSystem.Datum.Name, "\r\n  椭球体: ", geographicCoordinateSystem.Datum.Spheroid.Name, "\r\n    长半轴: ", 
                    geographicCoordinateSystem.Datum.Spheroid.SemiMajorAxis.ToString(), "\r\n    短半轴: ", geographicCoordinateSystem.Datum.Spheroid.SemiMinorAxis.ToString(), "\r\n    扁率倒数: ", (1.0 / geographicCoordinateSystem.Datum.Spheroid.Flattening).ToString()
                 };
                return string.Concat(strArray);
            }
            if (!(ispatialReferenceInfo_0 is IProjectedCoordinateSystem))
            {
                return "";
            }
            IProjectedCoordinateSystem system2 = (IProjectedCoordinateSystem) ispatialReferenceInfo_0;
            geographicCoordinateSystem = system2.GeographicCoordinateSystem;
            IProjection projection = system2.Projection;
            IParameter[] parameters = new IParameter[0x19];
            ((IProjectedCoordinateSystem4GEN) system2).GetParameters(ref parameters);
            string str3 = "  ";
            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i] == null)
                {
                    break;
                }
                str3 = str3 + parameters[i].Name + ": " + parameters[i].Value.ToString() + "\r\n ";
            }
            str2 = (((("别名: " + system2.Alias + "\r\n") + "缩略名: " + system2.Abbreviation + "\r\n") + "说明: " + system2.Remarks + "\r\n") + "投影: " + system2.Projection.Name + "\r\n") + "参数:\r\n" + str3;
            str2 = ((((str2 + "线性单位: " + system2.CoordinateUnit.Name + " (" + system2.CoordinateUnit.MetersPerUnit.ToString() + ")\r\n") + "地理坐标系:\r\n") + "  名称: " + geographicCoordinateSystem.Name + "\r\n") + "  缩略名: " + geographicCoordinateSystem.Abbreviation + "\r\n") + "  说明: " + geographicCoordinateSystem.Remarks + "\r\n";
            str2 = str2 + "  角度单位: " + geographicCoordinateSystem.CoordinateUnit.Name + " (" + geographicCoordinateSystem.CoordinateUnit.RadiansPerUnit.ToString() + ")\r\n";
            double num = 1.0 / geographicCoordinateSystem.Datum.Spheroid.Flattening;
            return ((((((str2 + "  本初子午线: " + geographicCoordinateSystem.PrimeMeridian.Name + " (" + geographicCoordinateSystem.PrimeMeridian.Longitude.ToString() + ")\r\n") + "  数据: " + geographicCoordinateSystem.Datum.Name + "\r\n") + "    椭球体: " + geographicCoordinateSystem.Datum.Spheroid.Name + "\r\n") + "    长半轴: " + geographicCoordinateSystem.Datum.Spheroid.SemiMajorAxis.ToString() + "\r\n") + "    短半轴: " + geographicCoordinateSystem.Datum.Spheroid.SemiMinorAxis.ToString() + "\r\n") + "    扁率倒数: " + num.ToString());
        }

        private void VCSCoordinateInfoPage_Load(object sender, EventArgs e)
        {
            if (!this.bool_0)
            {
                this.btnClear.Enabled = false;
                this.btnImport.Enabled = false;
                this.btnModify.Enabled = false;
                this.btnNew.Enabled = false;
                this.btnSaveAs.Enabled = false;
                this.btnSelect.Enabled = false;
                this.textBoxName.Properties.ReadOnly = false;
            }
            if (this.iverticalCoordinateSystem_0 != null)
            {
                this.textBoxName.Text = this.iverticalCoordinateSystem_0.Name;
                this.textBoxDetail.Text = this.method_0(this.iverticalCoordinateSystem_0);
            }
        }

        public bool IsEdit
        {
            set
            {
                this.bool_0 = value;
            }
        }

        public ISpatialReferenceInfo SpatialReference
        {
            get
            {
                return this.iverticalCoordinateSystem_0;
            }
            set
            {
                this.iverticalCoordinateSystem_0 = value as IVerticalCoordinateSystem;
            }
        }

        internal delegate void SpatialReferenceChangedHandler(object object_0);
    }
}

