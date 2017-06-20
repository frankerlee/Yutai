using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class frmNewLegendItem : Form
    {
        private bool bool_0 = false;
        private Button btnOK;
        private NewSymbolButton btnStyle;
        private Button button2;
        private GroupBox groupBox1;
        private IContainer icontainer_0 = null;
        private IFillSymbol ifillSymbol_0 = new SimpleFillSymbolClass();
        private ILineSymbol ilineSymbol_0 = new SimpleLineSymbolClass();
        private IMarkerSymbol imarkerSymbol_0 = new SimpleMarkerSymbolClass();
        private IStyleGallery istyleGallery_0 = null;
        private YTLegendItem jlklenendItem_0 = null;
        private Label labPoint;
        private RadioButton rdoFillSymbol;
        private RadioButton rdoLineSymbol;
        private RadioButton rdoPointSymbol;
        private TextBox txtLegendItemName;

        public frmNewLegendItem()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.jlklenendItem_0 = new YTLegendItem(this.btnStyle.Style as ISymbol, this.txtLegendItemName.Text);
        }

        private void btnStyle_Click(object sender, EventArgs e)
        {
            frmSymbolSelector selector = new frmSymbolSelector();
            if ((this.rdoPointSymbol.Checked || this.rdoLineSymbol.Checked) || !this.rdoFillSymbol.Checked)
            {
            }
            selector.SetStyleGallery(this.istyleGallery_0);
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

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void frmNewLegendItem_Load(object sender, EventArgs e)
        {
            this.bool_0 = true;
            if (this.rdoPointSymbol.Checked)
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
            this.groupBox1.SuspendLayout();
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
            this.btnOK.Location = new Point(0x3b, 0xbf);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(70, 0x17);
            this.btnOK.TabIndex = 70;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.button2.DialogResult = DialogResult.Cancel;
            this.button2.Location = new Point(0x9d, 0xbf);
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
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0xf8, 0xe9);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.txtLegendItemName);
            base.Controls.Add(this.labPoint);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmNewLegendItem";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "新增图例项";
            base.Load += new EventHandler(this.frmNewLegendItem_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
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

        internal YTLegendItem YTLegendItem
        {
            get
            {
                return this.jlklenendItem_0;
            }
            set
            {
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

