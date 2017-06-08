using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class TinSimpleRenderCtrl : UserControl, IUserControl
    {
        private bool bool_0 = false;
        private StyleButton btnStyle;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private IContainer icontainer_0 = null;
        private IStyleGallery istyleGallery_0 = null;
        private ITinLayer itinLayer_0 = null;
        private ITinSingleSymbolRenderer itinSingleSymbolRenderer_0;
        private Label label2;
        private Label label3;
        private MemoEdit txtDescription;
        private TextEdit txtLabel;

        public TinSimpleRenderCtrl()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        private void btnStyle_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.istyleGallery_0);
                    selector.SetSymbol(this.btnStyle.Style);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        this.btnStyle.Style = selector.GetSymbol();
                        this.itinSingleSymbolRenderer_0.Symbol = this.btnStyle.Style as ISymbol;
                    }
                }
            }
            catch
            {
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
            this.groupBox2 = new GroupBox();
            this.label3 = new Label();
            this.txtDescription = new MemoEdit();
            this.label2 = new Label();
            this.txtLabel = new TextEdit();
            this.groupBox1 = new GroupBox();
            this.btnStyle = new StyleButton();
            this.groupBox2.SuspendLayout();
            this.txtDescription.Properties.BeginInit();
            this.txtLabel.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtDescription);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtLabel);
            this.groupBox2.Location = new Point(13, 0x70);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(360, 0x98);
            this.groupBox2.TabIndex = 0x37;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "图例";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 0x40);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x1d, 12);
            this.label3.TabIndex = 0x37;
            this.label3.Text = "说明";
            this.txtDescription.EditValue = "";
            this.txtDescription.Location = new Point(0x30, 0x38);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new Size(0x110, 0x58);
            this.txtDescription.TabIndex = 0x36;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 0x18);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 12);
            this.label2.TabIndex = 0x35;
            this.label2.Text = "标注";
            this.txtLabel.EditValue = "";
            this.txtLabel.Location = new Point(0x30, 0x18);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.Size = new Size(0x110, 0x15);
            this.txtLabel.TabIndex = 0x34;
            this.groupBox1.Controls.Add(this.btnStyle);
            this.groupBox1.Location = new Point(13, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(360, 0x60);
            this.groupBox1.TabIndex = 0x36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "符号";
            this.btnStyle.Location = new Point(0x40, 0x10);
            this.btnStyle.Name = "btnStyle";
            this.btnStyle.Size = new Size(0x88, 0x40);
            this.btnStyle.Style = null;
            this.btnStyle.TabIndex = 0x2a;
            this.btnStyle.Click += new EventHandler(this.btnStyle_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "TinSimpleRenderCtrl";
            base.Size = new Size(0x1b5, 0x10d);
            base.Load += new EventHandler(this.TinSimpleRenderCtrl_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.txtDescription.Properties.EndInit();
            this.txtLabel.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void method_0()
        {
            this.txtDescription.Text = this.itinSingleSymbolRenderer_0.Description;
            this.txtLabel.Text = this.itinSingleSymbolRenderer_0.Label;
            this.btnStyle.Style = this.itinSingleSymbolRenderer_0.Symbol;
        }

        private void TinSimpleRenderCtrl_Load(object sender, EventArgs e)
        {
            this.method_0();
            this.bool_0 = true;
        }

        public ILayer CurrentLayer
        {
            set
            {
                this.itinLayer_0 = value as ITinLayer;
            }
        }

        bool IUserControl.Visible
        {
            get
            {
                return base.Visible;
            }
            set
            {
                base.Visible = value;
            }
        }

        public IStyleGallery StyleGallery
        {
            set
            {
                this.istyleGallery_0 = value;
            }
        }

        public ITinRenderer TinRenderer
        {
            set
            {
                this.itinSingleSymbolRenderer_0 = value as ITinSingleSymbolRenderer;
                if (this.bool_0)
                {
                    this.bool_0 = false;
                    this.method_0();
                    this.bool_0 = true;
                }
            }
        }
    }
}

