using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class LegendSetupUserControl : UserControl
    {
        private bool bool_0 = false;
        private StyleComboBox cboAreaPatches;
        private StyleComboBox cboLinePatches;
        private GroupBox groupBox1;
        private IArray iarray_0;
        private IContainer icontainer_0;
        private ILegend ilegend_0 = null;
        private ILegendItem ilegendItem_0 = null;
        private IStyleGallery istyleGallery_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private ListBoxControl listLegendLayers;
        private TextEdit txtHeight;
        private TextEdit txtWidth;
        private IStyleGalleryItem 定制 = null;
        private IStyleGalleryItem 定制_1 = null;

        public LegendSetupUserControl()
        {
            this.InitializeComponent();
        }

        private void cboAreaPatches_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0 && (this.ilegendItem_0 != null))
            {
                this.ilegendItem_0.LegendClassFormat.AreaPatch = this.cboAreaPatches.GetSelectStyleGalleryItem().Item as IAreaPatch;
            }
        }

        private void cboLinePatches_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0 && (this.ilegendItem_0 != null))
            {
                this.ilegendItem_0.LegendClassFormat.LinePatch = this.cboLinePatches.GetSelectStyleGalleryItem().Item as ILinePatch;
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

        private void InitializeComponent()
        {
            this.icontainer_0 = new Container();
            this.groupBox1 = new GroupBox();
            this.cboAreaPatches = new StyleComboBox(this.icontainer_0);
            this.cboLinePatches = new StyleComboBox(this.icontainer_0);
            this.label6 = new Label();
            this.label7 = new Label();
            this.label5 = new Label();
            this.label4 = new Label();
            this.txtHeight = new TextEdit();
            this.txtWidth = new TextEdit();
            this.label3 = new Label();
            this.label1 = new Label();
            this.listLegendLayers = new ListBoxControl();
            this.label2 = new Label();
            this.groupBox1.SuspendLayout();
            this.txtHeight.Properties.BeginInit();
            this.txtWidth.Properties.BeginInit();
            ((ISupportInitialize) this.listLegendLayers).BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.cboAreaPatches);
            this.groupBox1.Controls.Add(this.cboLinePatches);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtHeight);
            this.groupBox1.Controls.Add(this.txtWidth);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(0xa8, 0x20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xb8, 0xa8);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "区块";
            this.cboAreaPatches.DrawMode = DrawMode.OwnerDrawVariable;
            this.cboAreaPatches.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboAreaPatches.DropDownWidth = 160;
            this.cboAreaPatches.Font = new Font("宋体", 15f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.cboAreaPatches.Location = new System.Drawing.Point(0x38, 0x80);
            this.cboAreaPatches.Name = "cboAreaPatches";
            this.cboAreaPatches.Size = new Size(0x48, 0x1f);
            this.cboAreaPatches.TabIndex = 9;
            this.cboAreaPatches.SelectedIndexChanged += new EventHandler(this.cboAreaPatches_SelectedIndexChanged);
            this.cboLinePatches.DrawMode = DrawMode.OwnerDrawVariable;
            this.cboLinePatches.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboLinePatches.DropDownWidth = 160;
            this.cboLinePatches.Font = new Font("宋体", 15f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.cboLinePatches.Location = new System.Drawing.Point(0x38, 0x58);
            this.cboLinePatches.Name = "cboLinePatches";
            this.cboLinePatches.Size = new Size(0x48, 0x1f);
            this.cboLinePatches.TabIndex = 8;
            this.cboLinePatches.SelectedIndexChanged += new EventHandler(this.cboLinePatches_SelectedIndexChanged);
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(0x10, 0x88);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x17, 0x11);
            this.label6.TabIndex = 7;
            this.label6.Text = "面:";
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(0x10, 0x60);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x17, 0x11);
            this.label7.TabIndex = 6;
            this.label7.Text = "线:";
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0x90, 0x30);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x11, 0x11);
            this.label5.TabIndex = 5;
            this.label5.Text = "点";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0x90, 0x10);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x11, 0x11);
            this.label4.TabIndex = 4;
            this.label4.Text = "点";
            this.txtHeight.EditValue = "";
            this.txtHeight.Location = new System.Drawing.Point(0x38, 0x30);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new Size(80, 0x17);
            this.txtHeight.TabIndex = 3;
            this.txtHeight.EditValueChanged += new EventHandler(this.txtHeight_EditValueChanged);
            this.txtHeight.TextChanged += new EventHandler(this.txtHeight_TextChanged);
            this.txtWidth.EditValue = "";
            this.txtWidth.Location = new System.Drawing.Point(0x38, 0x10);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new Size(80, 0x17);
            this.txtWidth.TabIndex = 2;
            this.txtWidth.EditValueChanged += new EventHandler(this.txtWidth_EditValueChanged);
            this.txtWidth.TextChanged += new EventHandler(this.txtWidth_TextChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 0x38);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x23, 0x11);
            this.label3.TabIndex = 1;
            this.label3.Text = "高度:";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 0x18);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "宽度:";
            this.listLegendLayers.ItemHeight = 15;
            this.listLegendLayers.Location = new System.Drawing.Point(8, 40);
            this.listLegendLayers.Name = "listLegendLayers";
            this.listLegendLayers.Size = new Size(0x90, 160);
            this.listLegendLayers.TabIndex = 8;
            this.listLegendLayers.SelectedIndexChanged += new EventHandler(this.listLegendLayers_SelectedIndexChanged);
            this.label2.Location = new System.Drawing.Point(8, 0x10);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x58, 0x10);
            this.label2.TabIndex = 7;
            this.label2.Text = "图例项";
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.listLegendLayers);
            base.Controls.Add(this.label2);
            base.Name = "LegendSetupUserControl";
            base.Size = new Size(400, 0x110);
            base.Load += new EventHandler(this.LegendSetupUserControl_Load);
            this.groupBox1.ResumeLayout(false);
            this.txtHeight.Properties.EndInit();
            this.txtWidth.Properties.EndInit();
            ((ISupportInitialize) this.listLegendLayers).EndInit();
            base.ResumeLayout(false);
        }

        private void LegendSetupUserControl_Load(object sender, EventArgs e)
        {
            if (this.istyleGallery_0 != null)
            {
                IStyleGalleryItem item = null;
                IEnumStyleGalleryItem item2 = this.istyleGallery_0.get_Items("Line Patches", "", "");
                item2.Reset();
                for (item = item2.Next(); item != null; item = item2.Next())
                {
                    this.cboLinePatches.Add(item);
                }
                if (this.cboLinePatches.Items.Count > 0)
                {
                    this.cboLinePatches.SelectedIndex = 0;
                }
                item2 = this.istyleGallery_0.get_Items("Area Patches", "", "");
                item2.Reset();
                for (item = item2.Next(); item != null; item = item2.Next())
                {
                    this.cboAreaPatches.Add(item);
                }
                if (this.cboAreaPatches.Items.Count > 0)
                {
                    this.cboAreaPatches.SelectedIndex = 0;
                }
            }
            this.method_0();
            this.bool_0 = true;
        }

        private void listLegendLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.bool_0 = false;
            this.ilegendItem_0 = (this.listLegendLayers.Items[this.listLegendLayers.SelectedIndex] as LegendItemWrap).LegendItem;
            if (this.ilegendItem_0 != null)
            {
                ILegendClassFormat legendClassFormat = this.ilegendItem_0.LegendClassFormat;
                if (legendClassFormat.AreaPatch == null)
                {
                    ILegendFormat format = this.ilegend_0.Format;
                    legendClassFormat.AreaPatch = format.DefaultAreaPatch;
                    legendClassFormat.LinePatch = format.DefaultLinePatch;
                    legendClassFormat.PatchWidth = format.DefaultPatchWidth;
                    legendClassFormat.PatchHeight = format.DefaultPatchHeight;
                }
                if (this.定制 == null)
                {
                    this.定制 = new ServerStyleGalleryItemClass();
                    this.定制.Name = "定制";
                    this.定制.Item = legendClassFormat.LinePatch;
                }
                if (this.定制_1 == null)
                {
                    this.定制_1 = new ServerStyleGalleryItemClass();
                    this.定制_1.Name = "定制";
                    this.定制_1.Item = legendClassFormat.AreaPatch;
                }
                this.cboLinePatches.SelectStyleGalleryItem(this.定制);
                this.cboAreaPatches.SelectStyleGalleryItem(this.定制_1);
                this.txtWidth.Text = legendClassFormat.PatchWidth.ToString("#.##");
                this.txtHeight.Text = legendClassFormat.PatchHeight.ToString("#.##");
                this.bool_0 = true;
            }
        }

        private void method_0()
        {
            this.listLegendLayers.Items.Clear();
            for (int i = 0; i < this.ilegend_0.ItemCount; i++)
            {
                ILegendItem item = this.ilegend_0.get_Item(i);
                if (!(item.Layer is IFeatureLayer) || (((item.Layer as IFeatureLayer).FeatureClass.ShapeType != esriGeometryType.esriGeometryPoint) && ((item.Layer as IFeatureLayer).FeatureClass.ShapeType != esriGeometryType.esriGeometryMultipoint)))
                {
                    this.listLegendLayers.Items.Add(new LegendItemWrap(item));
                }
            }
            if (this.listLegendLayers.Items.Count > 0)
            {
                this.listLegendLayers.SelectedIndex = 0;
            }
        }

        private void txtHeight_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    this.txtHeight.ForeColor = Color.Red;
                    double num = Convert.ToDouble(this.txtHeight.Text);
                    if (this.ilegendItem_0 != null)
                    {
                        this.ilegendItem_0.LegendClassFormat.PatchHeight = num;
                    }
                }
                catch
                {
                    this.txtHeight.ForeColor = Color.Red;
                }
            }
        }

        private void txtHeight_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtWidth_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    this.txtWidth.ForeColor = Color.Black;
                    double num = Convert.ToDouble(this.txtWidth.Text);
                    if (this.ilegendItem_0 != null)
                    {
                        this.ilegendItem_0.LegendClassFormat.PatchWidth = num;
                    }
                }
                catch
                {
                    this.txtWidth.ForeColor = Color.Red;
                }
            }
        }

        private void txtWidth_TextChanged(object sender, EventArgs e)
        {
        }

        public ILegend Legend
        {
            set
            {
                this.ilegend_0 = value;
            }
        }

        public IStyleGallery StyleGallery
        {
            set
            {
                if (this.istyleGallery_0 != value)
                {
                    this.istyleGallery_0 = value;
                }
            }
        }
    }
}

