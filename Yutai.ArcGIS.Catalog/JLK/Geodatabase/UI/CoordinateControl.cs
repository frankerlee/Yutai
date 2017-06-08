namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.Geodatabase;
    using ESRI.ArcGIS.Geometry;
    using JLK.Catalog;
    using JLK.Catalog.UI;
    using JLK.Editors;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;

    internal class CoordinateControl : UserControl
    {
        private bool bool_0 = true;
        private bool bool_1 = false;
        private bool bool_2 = false;
        private SimpleButton btnClear;
        private SimpleButton btnImport;
        private SimpleButton btnModify;
        private SimpleButton btnNew;
        private SimpleButton btnSaveAs;
        private SimpleButton btnSelect;
        private SimpleButton btnSet;
        private Container container_0 = null;
        private ContextMenu contextMenu_0;
        private ISpatialReference ispatialReference_0;
        private ISpatialReferenceFactory ispatialReferenceFactory_0 = new SpatialReferenceEnvironmentClass();
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private MenuItem menuItem_0;
        private MenuItem menuItem_1;
        private MemoEdit textBoxDetail;
        private TextEdit textBoxName;

        public event SpatialReferenceChangedHandler SpatialReferenceChanged;

        public event ValueChangedHandler ValueChanged;

        public CoordinateControl()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            this.bool_1 = false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.ispatialReference_0 = new UnknownCoordinateSystemClass();
            this.method_1(this.ispatialReference_0);
            this.btnClear.Enabled = false;
            this.btnModify.Enabled = false;
            this.btnSaveAs.Enabled = false;
            this.method_0();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            frmOpenFile file = new frmOpenFile();
            file.AddFilter(new MyGxFilterDatasets(), true);
            file.AllowMultiSelect = false;
            if (file.ShowDialog() == DialogResult.OK)
            {
                IGxDataset dataset = file.Items.get_Element(0) as IGxDataset;
                if (dataset != null)
                {
                    IGeoDataset dataset2 = dataset.Dataset as IGeoDataset;
                    if (dataset2 != null)
                    {
                        this.ispatialReference_0 = dataset2.SpatialReference;
                        IControlPrecision2 precision = this.ispatialReference_0 as IControlPrecision2;
                        if (this.bool_2 != precision.IsHighPrecision)
                        {
                            if (precision.IsHighPrecision)
                            {
                                precision.IsHighPrecision = this.bool_2;
                                (this.ispatialReference_0 as ISpatialReferenceResolution).ConstructFromHorizon();
                            }
                            else
                            {
                                precision.IsHighPrecision = this.bool_2;
                            }
                        }
                        this.method_1(this.ispatialReference_0);
                        if (this.spatialReferenceChangedHandler_0 != null)
                        {
                            this.spatialReferenceChangedHandler_0(this.ispatialReference_0);
                        }
                        this.method_0();
                    }
                }
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            frmSpatialRefrence refrence = new frmSpatialRefrence {
                SpatialRefrence = this.ispatialReference_0
            };
            if (refrence.ShowDialog() == DialogResult.OK)
            {
                this.ispatialReference_0 = refrence.SpatialRefrence;
                this.method_3();
                this.method_1(this.ispatialReference_0);
                this.method_0();
            }
            refrence.Dispose();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            System.Drawing.Point pos = new System.Drawing.Point(this.btnNew.Location.X, this.btnNew.Location.Y + this.btnNew.Height);
            this.contextMenu_0.Show(this, pos);
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog {
                Filter = "空间参考文件 (*.prj)|*.prj",
                OverwritePrompt = true
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = dialog.FileName;
                ESRI.ArcGIS.esriSystem.IPersistStream stream = (ESRI.ArcGIS.esriSystem.IPersistStream) this.ispatialReference_0;
                IXMLStream stream2 = new XMLStreamClass();
                stream.Save((ESRI.ArcGIS.esriSystem.IStream) stream2, 1);
                stream2.SaveToFile(fileName);
                string str2 = stream2.SaveToString();
                int index = str2.IndexOf("[");
                str2 = str2.Substring(index - 6);
                index = str2.LastIndexOf("]");
                str2 = str2.Substring(0, index + 1);
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                using (System.IO.FileStream stream3 = File.Create(fileName))
                {
                    this.method_2(stream3, str2);
                }
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                Filter = "空间参考文件 (*.prj)|*.prj"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = dialog.FileName;
                this.ispatialReference_0 = ((ISpatialReferenceFactory2) this.ispatialReferenceFactory_0).CreateESRISpatialReferenceFromPRJFile(fileName);
                IControlPrecision2 precision = this.ispatialReference_0 as IControlPrecision2;
                if (this.bool_2 != precision.IsHighPrecision)
                {
                    if (precision.IsHighPrecision)
                    {
                        (this.ispatialReference_0 as ISpatialReferenceResolution).ConstructFromHorizon();
                        precision.IsHighPrecision = this.bool_2;
                    }
                    else
                    {
                        precision.IsHighPrecision = this.bool_2;
                    }
                }
                this.method_1(this.ispatialReference_0);
                this.method_0();
            }
            dialog.Dispose();
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            FrmFastSelSpatial spatial = new FrmFastSelSpatial();
            if (spatial.ShowDialog() == DialogResult.OK)
            {
                this.ispatialReference_0 = spatial.SpatialRefrence;
                this.method_1(this.ispatialReference_0);
                if (this.spatialReferenceChangedHandler_0 != null)
                {
                    this.spatialReferenceChangedHandler_0(this.ispatialReference_0);
                }
            }
        }

        private void CoordinateControl_Load(object sender, EventArgs e)
        {
            if (!this.bool_0)
            {
                this.btnSet.Enabled = false;
                this.btnClear.Enabled = false;
                this.btnImport.Enabled = false;
                this.btnModify.Enabled = false;
                this.btnNew.Enabled = false;
                this.btnSelect.Enabled = false;
                this.textBoxName.Properties.ReadOnly = false;
            }
            if (this.ispatialReference_0 == null)
            {
                this.ispatialReference_0 = new UnknownCoordinateSystemClass();
            }
            this.method_1(this.ispatialReference_0);
        }

        protected override void Dispose(bool bool_3)
        {
            if (bool_3 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_3);
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.label2 = new Label();
            this.contextMenu_0 = new ContextMenu();
            this.menuItem_0 = new MenuItem();
            this.menuItem_1 = new MenuItem();
            this.textBoxDetail = new MemoEdit();
            this.textBoxName = new TextEdit();
            this.btnSelect = new SimpleButton();
            this.btnNew = new SimpleButton();
            this.btnClear = new SimpleButton();
            this.btnModify = new SimpleButton();
            this.label3 = new Label();
            this.label4 = new Label();
            this.label5 = new Label();
            this.label6 = new Label();
            this.label7 = new Label();
            this.btnSaveAs = new SimpleButton();
            this.label8 = new Label();
            this.btnImport = new SimpleButton();
            this.btnSet = new SimpleButton();
            this.label9 = new Label();
            this.textBoxDetail.Properties.BeginInit();
            this.textBoxName.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AllowDrop = true;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "名字:";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "详细信息";
            this.contextMenu_0.MenuItems.AddRange(new MenuItem[] { this.menuItem_0, this.menuItem_1 });
            this.menuItem_0.Index = 0;
            this.menuItem_0.Text = "地理坐标系";
            this.menuItem_0.Click += new EventHandler(this.menuItem_0_Click);
            this.menuItem_1.Index = 1;
            this.menuItem_1.Text = "投影坐标系";
            this.menuItem_1.Click += new EventHandler(this.menuItem_1_Click);
            this.textBoxDetail.EditValue = "";
            this.textBoxDetail.Location = new System.Drawing.Point(10, 0x38);
            this.textBoxDetail.Name = "textBoxDetail";
            this.textBoxDetail.Properties.Appearance.BackColor = SystemColors.InactiveBorder;
            this.textBoxDetail.Properties.Appearance.Options.UseBackColor = true;
            this.textBoxDetail.Properties.ReadOnly = true;
            this.textBoxDetail.Size = new Size(0x108, 0xa5);
            this.textBoxDetail.TabIndex = 10;
            this.textBoxName.EditValue = "";
            this.textBoxName.Location = new System.Drawing.Point(0x38, 8);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Properties.ReadOnly = true;
            this.textBoxName.Size = new Size(0xd8, 0x15);
            this.textBoxName.TabIndex = 11;
            this.textBoxName.EditValueChanged += new EventHandler(this.textBoxName_EditValueChanged);
            this.btnSelect.Location = new System.Drawing.Point(5, 0x101);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new Size(0x45, 0x18);
            this.btnSelect.TabIndex = 12;
            this.btnSelect.Text = "选择";
            this.btnSelect.Click += new EventHandler(this.btnSelect_Click);
            this.btnNew.Location = new System.Drawing.Point(5, 0x141);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new Size(0x45, 0x18);
            this.btnNew.TabIndex = 13;
            this.btnNew.Text = "新建...";
            this.btnNew.Click += new EventHandler(this.btnNew_Click);
            this.btnClear.Location = new System.Drawing.Point(5, 0x181);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new Size(0x45, 0x18);
            this.btnClear.TabIndex = 14;
            this.btnClear.Text = "清空";
            this.btnClear.Click += new EventHandler(this.btnClear_Click);
            this.btnModify.Location = new System.Drawing.Point(5, 0x161);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new Size(0x45, 0x18);
            this.btnModify.TabIndex = 15;
            this.btnModify.Text = "修改...";
            this.btnModify.Click += new EventHandler(this.btnModify_Click);
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(80, 0x107);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x95, 12);
            this.label3.TabIndex = 0x10;
            this.label3.Text = "选择一个预定义的坐标系统";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(80, 0x147);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x65, 12);
            this.label4.TabIndex = 0x11;
            this.label4.Text = "新建一个坐标系统";
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(80, 0x167);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0xa1, 12);
            this.label5.TabIndex = 0x12;
            this.label5.Text = "编辑当前选择的坐标系统属性";
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(80, 0x188);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x71, 12);
            this.label6.TabIndex = 0x13;
            this.label6.Text = "设置坐标系统到未知";
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(80, 0x1a8);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x71, 12);
            this.label7.TabIndex = 0x15;
            this.label7.Text = "保存坐标系统到文件";
            this.btnSaveAs.Location = new System.Drawing.Point(5, 0x1a1);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new Size(0x45, 0x18);
            this.btnSaveAs.TabIndex = 20;
            this.btnSaveAs.Text = "另存为...";
            this.btnSaveAs.Click += new EventHandler(this.btnSaveAs_Click);
            this.label8.Location = new System.Drawing.Point(80, 290);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0xd0, 0x20);
            this.label8.TabIndex = 0x17;
            this.label8.Text = "从已存在的数据中导入坐标系统和X/Y,Z和M域值";
            this.btnImport.Location = new System.Drawing.Point(5, 0x121);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new Size(0x45, 0x18);
            this.btnImport.TabIndex = 0x16;
            this.btnImport.Text = "导入";
            this.btnImport.Click += new EventHandler(this.btnImport_Click);
            this.btnSet.Location = new System.Drawing.Point(5, 0xe3);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new Size(0x45, 0x18);
            this.btnSet.TabIndex = 0x18;
            this.btnSet.Text = "快速选择...";
            this.btnSet.Click += new EventHandler(this.btnSet_Click);
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(80, 0xe9);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x65, 12);
            this.label9.TabIndex = 0x19;
            this.label9.Text = "快速设置坐标系统";
            base.Controls.Add(this.label9);
            base.Controls.Add(this.btnSet);
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
            base.Name = "CoordinateControl";
            base.Size = new Size(0x128, 0x1c0);
            base.Load += new EventHandler(this.CoordinateControl_Load);
            this.textBoxDetail.Properties.EndInit();
            this.textBoxName.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void menuItem_0_Click(object sender, EventArgs e)
        {
            frmSpatialRefrence refrence = new frmSpatialRefrence {
                SpatialRefrenceType = frmSpatialRefrence.enumSpatialRefrenceType.enumGeographicCoord
            };
            if (refrence.ShowDialog() == DialogResult.OK)
            {
                this.ispatialReference_0 = refrence.SpatialRefrence;
                this.method_3();
                this.method_1(this.ispatialReference_0);
                this.method_0();
            }
        }

        private void menuItem_1_Click(object sender, EventArgs e)
        {
            frmSpatialRefrence refrence = new frmSpatialRefrence {
                SpatialRefrenceType = frmSpatialRefrence.enumSpatialRefrenceType.enumProjectCoord
            };
            if (refrence.ShowDialog() == DialogResult.OK)
            {
                this.ispatialReference_0 = refrence.SpatialRefrence;
                this.method_3();
                this.method_1(this.ispatialReference_0);
                this.method_0();
            }
        }

        private void method_0()
        {
            this.bool_1 = true;
            if (this.valueChangedHandler_0 != null)
            {
                this.valueChangedHandler_0(this, new EventArgs());
            }
        }

        private void method_1(ISpatialReference ispatialReference_1)
        {
            IGeographicCoordinateSystem geographicCoordinateSystem;
            string str;
            this.textBoxName.Text = ispatialReference_1.Name;
            if (ispatialReference_1 is IGeographicCoordinateSystem)
            {
                geographicCoordinateSystem = (IGeographicCoordinateSystem) ispatialReference_1;
                str = ("别名: " + geographicCoordinateSystem.Alias + "\r\n") + "缩略名: " + geographicCoordinateSystem.Abbreviation + "\r\n";
                string[] strArray = new string[0x15];
                strArray[0] = str;
                strArray[1] = "说明: ";
                strArray[2] = geographicCoordinateSystem.Remarks;
                strArray[3] = "\r\n角度单位: ";
                strArray[4] = geographicCoordinateSystem.CoordinateUnit.Name;
                strArray[5] = " (";
                strArray[6] = geographicCoordinateSystem.CoordinateUnit.RadiansPerUnit.ToString();
                strArray[7] = ")\r\n本初子午线: ";
                strArray[8] = geographicCoordinateSystem.PrimeMeridian.Name;
                strArray[9] = " (";
                strArray[10] = geographicCoordinateSystem.PrimeMeridian.Longitude.ToString();
                strArray[11] = ")\r\n数据: ";
                strArray[12] = geographicCoordinateSystem.Datum.Name;
                strArray[13] = "\r\n  椭球体: ";
                strArray[14] = geographicCoordinateSystem.Datum.Spheroid.Name;
                strArray[15] = "\r\n    长半轴: ";
                strArray[0x10] = geographicCoordinateSystem.Datum.Spheroid.SemiMajorAxis.ToString();
                strArray[0x11] = "\r\n    短半轴: ";
                strArray[0x12] = geographicCoordinateSystem.Datum.Spheroid.SemiMinorAxis.ToString();
                strArray[0x13] = "\r\n    扁率倒数: ";
                double num = 1.0 / geographicCoordinateSystem.Datum.Spheroid.Flattening;
                strArray[20] = num.ToString();
                str = string.Concat(strArray);
                this.textBoxDetail.Text = str;
                if (this.bool_0)
                {
                    this.btnModify.Enabled = true;
                    this.btnClear.Enabled = true;
                    this.btnSaveAs.Enabled = true;
                }
            }
            else if (!(ispatialReference_1 is IProjectedCoordinateSystem))
            {
                if (ispatialReference_1 is IUnknownCoordinateSystem)
                {
                    str = "未知坐标系统";
                    this.textBoxDetail.Text = str;
                    if (this.ispatialReference_0 is IUnknownCoordinateSystem)
                    {
                        this.btnModify.Enabled = false;
                        this.btnClear.Enabled = false;
                        this.btnSaveAs.Enabled = false;
                    }
                    else if (this.bool_0)
                    {
                        this.btnModify.Enabled = true;
                        this.btnClear.Enabled = true;
                        this.btnSaveAs.Enabled = true;
                    }
                }
                else
                {
                    this.btnClear.Enabled = false;
                    this.btnModify.Enabled = false;
                    this.textBoxDetail.Text = "";
                }
            }
            else
            {
                IProjectedCoordinateSystem system2 = (IProjectedCoordinateSystem) ispatialReference_1;
                geographicCoordinateSystem = system2.GeographicCoordinateSystem;
                IProjection projection = system2.Projection;
                IParameter[] parameters = new IParameter[0x19];
                ((IProjectedCoordinateSystem4GEN) system2).GetParameters(ref parameters);
                string str2 = "  ";
                for (int i = 0; i < parameters.Length; i++)
                {
                    if (parameters[i] == null)
                    {
                        break;
                    }
                    str2 = str2 + parameters[i].Name + ": " + parameters[i].Value.ToString() + "\r\n ";
                }
                str = (((("别名: " + system2.Alias + "\r\n") + "缩略名: " + system2.Abbreviation + "\r\n") + "说明: " + system2.Remarks + "\r\n") + "投影: " + system2.Projection.Name + "\r\n") + "参数:\r\n" + str2;
                str = ((((str + "线性单位: " + system2.CoordinateUnit.Name + " (" + system2.CoordinateUnit.MetersPerUnit.ToString() + ")\r\n") + "地理坐标系:\r\n") + "  名称: " + geographicCoordinateSystem.Name + "\r\n") + "  缩略名: " + geographicCoordinateSystem.Abbreviation + "\r\n") + "  说明: " + geographicCoordinateSystem.Remarks + "\r\n";
                str = str + "  角度单位: " + geographicCoordinateSystem.CoordinateUnit.Name + " (" + geographicCoordinateSystem.CoordinateUnit.RadiansPerUnit.ToString() + ")\r\n";
                str = (((((str + "  本初子午线: " + geographicCoordinateSystem.PrimeMeridian.Name + " (" + geographicCoordinateSystem.PrimeMeridian.Longitude.ToString() + ")\r\n") + "  数据: " + geographicCoordinateSystem.Datum.Name + "\r\n") + "    椭球体: " + geographicCoordinateSystem.Datum.Spheroid.Name + "\r\n") + "    长半轴: " + geographicCoordinateSystem.Datum.Spheroid.SemiMajorAxis.ToString() + "\r\n") + "    短半轴: " + geographicCoordinateSystem.Datum.Spheroid.SemiMinorAxis.ToString() + "\r\n") + "    扁率倒数: " + ((1.0 / geographicCoordinateSystem.Datum.Spheroid.Flattening)).ToString();
                this.textBoxDetail.Text = str;
                if (this.bool_0)
                {
                    this.btnModify.Enabled = true;
                    this.btnClear.Enabled = true;
                    this.btnSaveAs.Enabled = true;
                }
            }
        }

        private void method_2(System.IO.FileStream fileStream_0, string string_0)
        {
            byte[] bytes = new UTF8Encoding(true).GetBytes(string_0);
            fileStream_0.Write(bytes, 0, bytes.Length);
        }

        private void method_3()
        {
        }

        private void textBoxName_EditValueChanged(object sender, EventArgs e)
        {
            this.method_0();
        }

        public bool IsDirty
        {
            get
            {
                return this.bool_1;
            }
        }

        public bool IsEdit
        {
            set
            {
                this.bool_0 = value;
            }
        }

        public ISpatialReference SpatialRefrence
        {
            get
            {
                return this.ispatialReference_0;
            }
            set
            {
                this.ispatialReference_0 = value;
                IControlPrecision2 precision = this.ispatialReference_0 as IControlPrecision2;
                this.bool_2 = precision.IsHighPrecision;
            }
        }

        public delegate void SpatialReferenceChangedHandler(object object_0);
    }
}

