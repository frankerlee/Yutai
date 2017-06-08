using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Wrapper;

namespace Yutai.ArcGIS.Carto.UI
{
    public class frmGeographicTransformationConvert : Form
    {
        private bool bool_0 = false;
        private Button btnCancel;
        private Button btnEdit;
        private Button btnNew;
        private Button btnOK;
        private ComboBox cboTarget;
        private IBasicMap ibasicMap_0 = null;
        private IContainer icontainer_0 = null;
        private int[] int_0 = new int[] { 0x1076, 0x1202, 0x10e6 };
        private Label label1;
        private Label label2;
        private Label label3;
        private List<string> list_0 = new List<string>();
        private ListBox listBox1;
        private TextBox txtConvertMethod;

        public frmGeographicTransformationConvert()
        {
            this.InitializeComponent();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.txtConvertMethod.Tag != null)
            {
                frmGaphicTransformation transformation = new frmGaphicTransformation {
                    GeoTransformations = this.txtConvertMethod.Tag as IGeoTransformation
                };
                if (transformation.ShowDialog() == DialogResult.OK)
                {
                    this.txtConvertMethod.Text = transformation.GeoTransformations.Name;
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex > -1)
            {
                frmGaphicTransformation transformation = new frmGaphicTransformation();
                ISpatialReferenceFactory factory = new SpatialReferenceEnvironmentClass();
                ISpatialReference reference = null;
                if (this.cboTarget.SelectedIndex == 0)
                {
                    reference = factory.CreateGeographicCoordinateSystem(0x1076);
                }
                else if (this.cboTarget.SelectedIndex == 1)
                {
                    reference = factory.CreateGeographicCoordinateSystem(0x1202);
                }
                else
                {
                    reference = factory.CreateGeographicCoordinateSystem(0x10e6);
                }
                transformation.SourceSpatialReference = (this.listBox1.SelectedItem as ObjectWrap).Object as ISpatialReference;
                transformation.TargetSpatialReference = reference;
                if (transformation.ShowDialog() == DialogResult.OK)
                {
                    this.txtConvertMethod.Text = transformation.GeoTransformations.Name;
                    this.txtConvertMethod.Tag = transformation.GeoTransformations;
                    this.btnEdit.Enabled = true;
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtConvertMethod.Tag != null)
                {
                    esriTransformDirection direction;
                    IGeoTransformation transformation;
                    IGeoTransformationOperationSet geographicTransformations = (this.ibasicMap_0 as IMapGeographicTransformations).GeographicTransformations;
                    IGeographicCoordinateSystem pFromGCS = (this.listBox1.SelectedItem as ObjectWrap).Object as IGeographicCoordinateSystem;
                    IGeographicCoordinateSystem pToGCS = (this.cboTarget.SelectedItem as ObjectWrap).Object as IGeographicCoordinateSystem;
                    geographicTransformations.Get(pFromGCS, pToGCS, out direction, out transformation);
                    if (transformation != null)
                    {
                        geographicTransformations.Remove(direction, transformation);
                    }
                    geographicTransformations.Set(esriTransformDirection.esriTransformForward, this.txtConvertMethod.Tag as IGeoTransformation);
                    geographicTransformations.Set(esriTransformDirection.esriTransformReverse, this.txtConvertMethod.Tag as IGeoTransformation);
                }
            }
            catch (Exception exception)
            {
                exception.ToString();
            }
            base.DialogResult = DialogResult.OK;
        }

        private void cboTarget_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.listBox1_SelectedIndexChanged(this, e);
            }
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void frmGeographicTransformationConvert_Load(object sender, EventArgs e)
        {
            int num;
            this.btnEdit.Enabled = false;
            this.btnNew.Enabled = false;
            this.cboTarget.Items.Clear();
            ISpatialReferenceFactory factory = new SpatialReferenceEnvironmentClass();
            ISpatialReference reference = null;
            for (num = 0; num < this.int_0.Length; num++)
            {
                reference = factory.CreateGeographicCoordinateSystem(this.int_0[num]);
                this.cboTarget.Items.Add(new ObjectWrap(reference));
            }
            this.cboTarget.SelectedIndex = 0;
            if (this.ibasicMap_0.SpatialReference is IProjectedCoordinateSystem)
            {
                this.list_0.Add((this.ibasicMap_0.SpatialReference as IProjectedCoordinateSystem).GeographicCoordinateSystem.Name);
                this.listBox1.Items.Add(new ObjectWrap((this.ibasicMap_0.SpatialReference as IProjectedCoordinateSystem).GeographicCoordinateSystem));
            }
            else if (this.ibasicMap_0.SpatialReference is IGeographicCoordinateSystem)
            {
                this.list_0.Add(this.ibasicMap_0.SpatialReference.Name);
                this.listBox1.Items.Add(new ObjectWrap(this.ibasicMap_0.SpatialReference));
            }
            for (num = 0; num < this.ibasicMap_0.LayerCount; num++)
            {
                ILayer layer = this.ibasicMap_0.get_Layer(num);
                if (layer is IGroupLayer)
                {
                    for (int i = 0; i < (layer as ICompositeLayer).Count; i++)
                    {
                        this.method_0((layer as ICompositeLayer).get_Layer(i));
                    }
                }
                else if (layer is IGeoDataset)
                {
                    string name;
                    IGeoDataset dataset = layer as IGeoDataset;
                    if (dataset.SpatialReference is IProjectedCoordinateSystem)
                    {
                        name = (this.ibasicMap_0.SpatialReference as IProjectedCoordinateSystem).GeographicCoordinateSystem.Name;
                        if (this.list_0.IndexOf(name) == -1)
                        {
                            this.list_0.Add((this.ibasicMap_0.SpatialReference as IProjectedCoordinateSystem).GeographicCoordinateSystem.Name);
                            this.listBox1.Items.Add(new ObjectWrap((dataset.SpatialReference as IProjectedCoordinateSystem).GeographicCoordinateSystem));
                        }
                    }
                    else if (dataset.SpatialReference is IGeographicCoordinateSystem)
                    {
                        name = this.ibasicMap_0.SpatialReference.Name;
                        if (this.list_0.IndexOf(name) == -1)
                        {
                            this.list_0.Add(name);
                            this.listBox1.Items.Add(new ObjectWrap(dataset.SpatialReference));
                        }
                    }
                }
            }
            this.bool_0 = true;
            if (this.listBox1.Items.Count > 0)
            {
                this.listBox1.SelectedIndex = 0;
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGeographicTransformationConvert));
            this.label1 = new Label();
            this.listBox1 = new ListBox();
            this.label2 = new Label();
            this.cboTarget = new ComboBox();
            this.label3 = new Label();
            this.btnOK = new Button();
            this.btnCancel = new Button();
            this.btnNew = new Button();
            this.txtConvertMethod = new TextBox();
            this.btnEdit = new Button();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 7);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "源地理坐标";
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(4, 0x16);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new Size(0xe5, 0x40);
            this.listBox1.TabIndex = 1;
            this.listBox1.SelectedIndexChanged += new EventHandler(this.listBox1_SelectedIndexChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 0x62);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x4d, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "目的地理坐标";
            this.cboTarget.FormattingEnabled = true;
            this.cboTarget.Items.AddRange(new object[] { "北京54", "西安80", "WGS84" });
            this.cboTarget.Location = new System.Drawing.Point(4, 0x7a);
            this.cboTarget.Name = "cboTarget";
            this.cboTarget.Size = new Size(0xd9, 20);
            this.cboTarget.TabIndex = 3;
            this.cboTarget.SelectedIndexChanged += new EventHandler(this.cboTarget_SelectedIndexChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 0x9c);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x35, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "转换方法";
            this.btnOK.FlatStyle = FlatStyle.Popup;
            this.btnOK.Location = new System.Drawing.Point(0xef, 0x16);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x38, 0x18);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.FlatStyle = FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(0xef, 0x3b);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x38, 0x18);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnNew.FlatStyle = FlatStyle.Popup;
            this.btnNew.Location = new System.Drawing.Point(0x6c, 0xc6);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new Size(0x38, 0x18);
            this.btnNew.TabIndex = 8;
            this.btnNew.Text = "新建";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new EventHandler(this.btnNew_Click);
            this.txtConvertMethod.Location = new System.Drawing.Point(4, 0xab);
            this.txtConvertMethod.Name = "txtConvertMethod";
            this.txtConvertMethod.ReadOnly = true;
            this.txtConvertMethod.Size = new Size(0xd7, 0x15);
            this.txtConvertMethod.TabIndex = 9;
            this.btnEdit.FlatStyle = FlatStyle.Popup;
            this.btnEdit.Location = new System.Drawing.Point(170, 0xc6);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new Size(0x38, 0x18);
            this.btnEdit.TabIndex = 10;
            this.btnEdit.Text = "编辑";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new EventHandler(this.btnEdit_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x133, 0xec);
            base.Controls.Add(this.btnEdit);
            base.Controls.Add(this.txtConvertMethod);
            base.Controls.Add(this.btnNew);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.cboTarget);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.listBox1);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = ((System.Drawing.Icon)resources.GetObject("$Icon"));
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmGeographicTransformationConvert";
            this.Text = "地理坐标转换";
            base.Load += new EventHandler(this.frmGeographicTransformationConvert_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnNew.Enabled = this.listBox1.SelectedIndex != -1;
            if ((this.bool_0 && (this.listBox1.SelectedIndex != -1)) && (this.cboTarget.SelectedIndex != -1))
            {
                IGeographicCoordinateSystem pFromGCS = (this.listBox1.SelectedItem as ObjectWrap).Object as IGeographicCoordinateSystem;
                IGeographicCoordinateSystem pToGCS = (this.cboTarget.SelectedItem as ObjectWrap).Object as IGeographicCoordinateSystem;
                IGeoTransformationOperationSet geographicTransformations = (this.ibasicMap_0 as IMapGeographicTransformations).GeographicTransformations;
                if (geographicTransformations != null)
                {
                    esriTransformDirection direction;
                    IGeoTransformation transformation;
                    geographicTransformations.Get(pFromGCS, pToGCS, out direction, out transformation);
                    if (transformation != null)
                    {
                        this.txtConvertMethod.Text = transformation.Name;
                        this.txtConvertMethod.Tag = transformation;
                    }
                    else
                    {
                        this.txtConvertMethod.Text = "";
                        this.txtConvertMethod.Tag = transformation;
                    }
                }
            }
        }

        private void method_0(ILayer ilayer_0)
        {
            if (ilayer_0 is ICompositeLayer)
            {
                for (int i = 0; i < (ilayer_0 as ICompositeLayer).Count; i++)
                {
                    this.method_0((ilayer_0 as ICompositeLayer).get_Layer(i));
                }
            }
            else if (ilayer_0 is IGeoDataset)
            {
                IGeoDataset dataset = ilayer_0 as IGeoDataset;
                if (dataset.SpatialReference != null)
                {
                    string name;
                    if (dataset.SpatialReference is IProjectedCoordinateSystem)
                    {
                        name = (dataset.SpatialReference as IProjectedCoordinateSystem).GeographicCoordinateSystem.Name;
                        if (this.list_0.IndexOf(name) == -1)
                        {
                            this.list_0.Add(name);
                            this.listBox1.Items.Add(new ObjectWrap((dataset.SpatialReference as IProjectedCoordinateSystem).GeographicCoordinateSystem));
                        }
                    }
                    else if (dataset.SpatialReference is IGeographicCoordinateSystem)
                    {
                        name = this.ibasicMap_0.SpatialReference.Name;
                        if (this.list_0.IndexOf(name) == -1)
                        {
                            this.list_0.Add(name);
                            this.listBox1.Items.Add(new ObjectWrap(dataset.SpatialReference));
                        }
                        this.listBox1.Items.Add(new ObjectWrap(dataset.SpatialReference));
                    }
                }
            }
        }

        public IBasicMap Map
        {
            set
            {
                this.ibasicMap_0 = value;
            }
        }
    }
}

