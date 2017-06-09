using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public class frmNewLegendItem : Form
    {
        private bool bool_0 = false;
        private bool bool_1 = true;
        private Button btnClear;
        private Button btnOK;
        private NewSymbolButton btnStyle;
        private Button button2;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private IContainer icontainer_0 = null;
        private IFillSymbol ifillSymbol_0 = new SimpleFillSymbolClass();
        private IFillSymbol ifillSymbol_1 = new SimpleFillSymbolClass();
        private ILineSymbol ilineSymbol_0 = new SimpleLineSymbolClass();
        private IMarkerSymbol imarkerSymbol_0 = new SimpleMarkerSymbolClass();
        private IStyleGallery istyleGallery_0 = null;
        private MapCartoTemplateLib.JLKLenendItem jlklenendItem_0 = null;
        private Label labPoint;
        private NewSymbolButton newSymbolButton1;
        private RadioButton rdoFillSymbol;
        private RadioButton rdoLineSymbol;
        private RadioButton rdoPointSymbol;
        private TextBox txtLegendItemName;

        public frmNewLegendItem()
        {
            this.InitializeComponent();
            (this.ifillSymbol_1 as ISimpleFillSymbol).Style = esriSimpleFillStyle.esriSFSNull;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.newSymbolButton1.Style = null;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.jlklenendItem_0 == null)
            {
                this.jlklenendItem_0 = new MapCartoTemplateLib.JLKLenendItem(this.btnStyle.Style as ISymbol, this.txtLegendItemName.Text, this.newSymbolButton1.Style as ISymbol);
            }
            else
            {
                this.jlklenendItem_0.Description = this.txtLegendItemName.Text;
                this.jlklenendItem_0.BackSymbol = this.newSymbolButton1.Style as ISymbol;
                this.jlklenendItem_0.Symbol = this.btnStyle.Style as ISymbol;
            }
        }

        private void btnStyle_Click(object sender, EventArgs e)
        {
            frmSymbolSelector selector = new frmSymbolSelector();
            selector.SetStyleGallery(ApplicationBase.StyleGallery);
            selector.SetSymbol(this.btnStyle.Style);
            if (selector.ShowDialog() == DialogResult.OK)
            {
                this.btnStyle.Style = selector.GetSymbol();
                if (this.rdoPointSymbol.Checked)
                {
                    this.imarkerSymbol_0 = this.btnStyle.Style as IMarkerSymbol;
                }
                else if (this.rdoLineSymbol.Checked)
                {
                    this.ilineSymbol_0 = this.btnStyle.Style as ILineSymbol;
                }
                else
                {
                    this.ifillSymbol_0 = this.btnStyle.Style as IFillSymbol;
                }
            }
        }

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_2);
        }

        private void frmNewLegendItem_Load(object sender, EventArgs e)
        {
            this.bool_0 = true;
            if (this.jlklenendItem_0 != null)
            {
                if (this.jlklenendItem_0.Symbol is IMarkerSymbol)
                {
                    this.btnStyle.Style = this.jlklenendItem_0.Symbol as IMarkerSymbol;
                    this.rdoPointSymbol.Checked = true;
                }
                else if (this.jlklenendItem_0.Symbol is ILineSymbol)
                {
                    this.btnStyle.Style = this.jlklenendItem_0.Symbol as ILineSymbol;
                    this.rdoLineSymbol.Checked = true;
                }
                else if (this.jlklenendItem_0.Symbol is IFillSymbol)
                {
                    this.btnStyle.Style = this.jlklenendItem_0.Symbol as IFillSymbol;
                    this.rdoFillSymbol.Checked = true;
                }
                this.newSymbolButton1.Style = this.jlklenendItem_0.BackSymbol;
                this.txtLegendItemName.Text = this.jlklenendItem_0.Description;
            }
            else if (this.rdoPointSymbol.Checked)
            {
                this.btnStyle.Style = this.imarkerSymbol_0;
            }
            else if (this.rdoLineSymbol.Checked)
            {
                this.btnStyle.Style = this.ilineSymbol_0;
            }
            else
            {
                this.btnStyle.Style = this.ifillSymbol_0;
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewLegendItem));
            this.txtLegendItemName = new TextBox();
            this.labPoint = new Label();
            this.btnOK = new Button();
            this.button2 = new Button();
            this.groupBox1 = new GroupBox();
            this.btnStyle = new NewSymbolButton();
            this.rdoFillSymbol = new RadioButton();
            this.rdoLineSymbol = new RadioButton();
            this.rdoPointSymbol = new RadioButton();
            this.groupBox2 = new GroupBox();
            this.newSymbolButton1 = new NewSymbolButton();
            this.btnClear = new Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            base.SuspendLayout();
            this.txtLegendItemName.Location = new Point(0x51, 12);
            this.txtLegendItemName.Name = "txtLegendItemName";
            this.txtLegendItemName.Size = new Size(0x92, 0x15);
            this.txtLegendItemName.TabIndex = 0x26;
            this.txtLegendItemName.TextChanged += new EventHandler(this.txtLegendItemName_TextChanged);
            this.labPoint.AutoSize = true;
            this.labPoint.Location = new Point(4, 15);
            this.labPoint.Name = "labPoint";
            this.labPoint.Size = new Size(0x47, 12);
            this.labPoint.TabIndex = 0x27;
            this.labPoint.Text = "图例项描述:";
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0x2e, 0x11c);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(70, 0x17);
            this.btnOK.TabIndex = 70;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.button2.DialogResult = DialogResult.Cancel;
            this.button2.Location = new Point(0x90, 0x11c);
            this.button2.Name = "button2";
            this.button2.Size = new Size(70, 0x17);
            this.button2.TabIndex = 0x45;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.groupBox1.Controls.Add(this.btnStyle);
            this.groupBox1.Controls.Add(this.rdoFillSymbol);
            this.groupBox1.Controls.Add(this.rdoLineSymbol);
            this.groupBox1.Controls.Add(this.rdoPointSymbol);
            this.groupBox1.Location = new Point(6, 0x36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xdd, 0x7a);
            this.groupBox1.TabIndex = 0x48;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "符号";
            this.btnStyle.Location = new Point(0x2a, 0x34);
            this.btnStyle.Name = "btnStyle";
            this.btnStyle.Size = new Size(0x6a, 50);
            this.btnStyle.Style = null;
            this.btnStyle.TabIndex = 13;
            this.btnStyle.Click += new EventHandler(this.btnStyle_Click);
            this.rdoFillSymbol.AutoSize = true;
            this.rdoFillSymbol.Checked = true;
            this.rdoFillSymbol.Location = new Point(0x9a, 20);
            this.rdoFillSymbol.Name = "rdoFillSymbol";
            this.rdoFillSymbol.Size = new Size(0x3b, 0x10);
            this.rdoFillSymbol.TabIndex = 11;
            this.rdoFillSymbol.TabStop = true;
            this.rdoFillSymbol.Text = "面符号";
            this.rdoFillSymbol.UseVisualStyleBackColor = true;
            this.rdoFillSymbol.CheckedChanged += new EventHandler(this.rdoFillSymbol_CheckedChanged);
            this.rdoLineSymbol.AutoSize = true;
            this.rdoLineSymbol.Location = new Point(0x59, 20);
            this.rdoLineSymbol.Name = "rdoLineSymbol";
            this.rdoLineSymbol.Size = new Size(0x3b, 0x10);
            this.rdoLineSymbol.TabIndex = 10;
            this.rdoLineSymbol.Text = "线符号";
            this.rdoLineSymbol.UseVisualStyleBackColor = true;
            this.rdoLineSymbol.CheckedChanged += new EventHandler(this.rdoLineSymbol_CheckedChanged);
            this.rdoPointSymbol.AutoSize = true;
            this.rdoPointSymbol.Location = new Point(0x10, 20);
            this.rdoPointSymbol.Name = "rdoPointSymbol";
            this.rdoPointSymbol.Size = new Size(0x3b, 0x10);
            this.rdoPointSymbol.TabIndex = 9;
            this.rdoPointSymbol.Text = "点符号";
            this.rdoPointSymbol.UseVisualStyleBackColor = true;
            this.rdoPointSymbol.CheckedChanged += new EventHandler(this.rdoPointSymbol_CheckedChanged);
            this.groupBox2.Controls.Add(this.btnClear);
            this.groupBox2.Controls.Add(this.newSymbolButton1);
            this.groupBox2.Location = new Point(6, 0xb6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0xdd, 0x54);
            this.groupBox2.TabIndex = 0x49;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "背景符号";
            this.newSymbolButton1.Location = new Point(0x10, 20);
            this.newSymbolButton1.Name = "newSymbolButton1";
            this.newSymbolButton1.Size = new Size(0x6a, 50);
            this.newSymbolButton1.Style = null;
            this.newSymbolButton1.TabIndex = 13;
            this.newSymbolButton1.Click += new EventHandler(this.newSymbolButton1_Click);
            this.btnClear.DialogResult = DialogResult.OK;
            this.btnClear.Location = new Point(0x9d, 20);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new Size(0x33, 0x17);
            this.btnClear.TabIndex = 0x47;
            this.btnClear.Text = "清除";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new EventHandler(this.btnClear_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x11b, 0x152);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.txtLegendItemName);
            base.Controls.Add(this.labPoint);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon)resources.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmNewLegendItem";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "新增图例项";
            base.Load += new EventHandler(this.frmNewLegendItem_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void newSymbolButton1_Click(object sender, EventArgs e)
        {
            frmSymbolSelector selector = new frmSymbolSelector();
            selector.SetStyleGallery(ApplicationBase.StyleGallery);
            selector.SetSymbol(this.ifillSymbol_1);
            if (selector.ShowDialog() == DialogResult.OK)
            {
                this.newSymbolButton1.Style = selector.GetSymbol();
                this.ifillSymbol_1 = this.btnStyle.Style as IFillSymbol;
            }
        }

        private void rdoFillSymbol_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.btnStyle.Style = this.ifillSymbol_0;
            }
        }

        private void rdoLineSymbol_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.btnOK.Enabled = true;
                this.btnStyle.Style = this.ilineSymbol_0;
            }
        }

        private void rdoPointSymbol_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.btnStyle.Style = this.imarkerSymbol_0;
            }
        }

        private void txtLegendItemName_TextChanged(object sender, EventArgs e)
        {
        }

        internal MapCartoTemplateLib.JLKLenendItem JLKLenendItem
        {
            get
            {
                return this.jlklenendItem_0;
            }
            set
            {
                this.bool_1 = false;
                this.jlklenendItem_0 = value;
            }
        }

        public IStyleGallery StyleGallery
        {
            set
            {
                this.istyleGallery_0 = value;
            }
        }
    }
}

