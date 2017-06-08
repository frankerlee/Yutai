using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Controls.SymbolLib
{
    internal class GeometricCutCurverPage : GeometricEffectBaseControl
    {
        private Button btnAddGemoetricEffic;
        private Button btnChangeEffic;
        private IContainer components = null;
        private ContextMenuStrip contextMenuStrip1;
        private ImageList imageList1;
        private Label label1;
        private Label label2;
        private Label label3;
        private BasicSymbolLayerBaseControl m_pControl = null;
        private Panel panel1;
        private Panel panel2;
        private TextBox txtEnd;
        private TextBox txtStart;
        private ToolStripMenuItem 删除ToolStripMenuItem;
        private ToolStripMenuItem 修改ToolStripMenuItem;

        public GeometricCutCurverPage(BasicSymbolLayerBaseControl pControl)
        {
            this.InitializeComponent();
            this.m_pControl = pControl;
            base.m_pGeometricEffect = new GeometricEffectCutClass();
        }

        public override bool Apply()
        {
            double val = 0.0;
            double num2 = 0.0;
            try
            {
                val = double.Parse(this.txtStart.Text);
            }
            catch
            {
                MessageBox.Show("请出入数字型数据！");
                return false;
            }
            try
            {
                num2 = double.Parse(this.txtEnd.Text);
            }
            catch
            {
                MessageBox.Show("请出入数字型数据！");
                return false;
            }
            (base.m_pGeometricEffect as IGraphicAttributes).set_Value((int) this.txtStart.Tag, val);
            (base.m_pGeometricEffect as IGraphicAttributes).set_Value((int) this.txtEnd.Tag, num2);
            return true;
        }

        private void btnAddGemoetricEffic_Click(object sender, EventArgs e)
        {
            frmGeometricEffectList list = new frmGeometricEffectList {
                BasicSymbolLayerBaseControl = this.m_pControl
            };
            if ((list.ShowDialog() == DialogResult.OK) && (this.m_pControl != null))
            {
                this.m_pControl.AddControl(this, list.SelectControl);
            }
        }

        private void btnChangeEffic_Click(object sender, EventArgs e)
        {
            this.contextMenuStrip1.Show(this, this.btnChangeEffic.Right, this.btnChangeEffic.Bottom);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private Control FindControl(int id)
        {
            for (int i = 0; i < base.Controls.Count; i++)
            {
                if ((!(base.Controls[i] is Label) && (base.Controls[i].Tag != null)) && ((base.Controls[i].Tag is int) && (id == ((int) base.Controls[i].Tag))))
                {
                    return base.Controls[i];
                }
            }
            return null;
        }

        private void GeometricCutCurverPage_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < (base.m_pGeometricEffect as IGraphicAttributes).GraphicAttributeCount; i++)
            {
                int attrId = (base.m_pGeometricEffect as IGraphicAttributes).get_ID(i);
                string str = (base.m_pGeometricEffect as IGraphicAttributes).get_Name(attrId);
                if (attrId == 0)
                {
                    this.txtStart.Text = (base.m_pGeometricEffect as IGraphicAttributes).get_Value(attrId).ToString();
                    this.txtStart.Tag = attrId;
                }
                else
                {
                    this.txtEnd.Text = (base.m_pGeometricEffect as IGraphicAttributes).get_Value(attrId).ToString();
                    this.txtEnd.Tag = attrId;
                }
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeometricCutCurverPage));
            this.label1 = new Label();
            this.panel1 = new Panel();
            this.txtEnd = new TextBox();
            this.label3 = new Label();
            this.txtStart = new TextBox();
            this.label2 = new Label();
            this.btnChangeEffic = new Button();
            this.imageList1 = new ImageList(this.components);
            this.panel2 = new Panel();
            this.btnAddGemoetricEffic = new Button();
            this.contextMenuStrip1 = new ContextMenuStrip(this.components);
            this.修改ToolStripMenuItem = new ToolStripMenuItem();
            this.删除ToolStripMenuItem = new ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.ForeColor = SystemColors.ActiveCaptionText;
            this.label1.Location = new Point(3, 2);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "裁剪线段";
            this.panel1.BackColor = SystemColors.Window;
            this.panel1.Controls.Add(this.txtEnd);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtStart);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 0x20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0xd4, 0x3d);
            this.panel1.TabIndex = 1;
            this.txtEnd.Location = new Point(0x40, 0x23);
            this.txtEnd.Name = "txtEnd";
            this.txtEnd.Size = new Size(0x80, 0x15);
            this.txtEnd.TabIndex = 4;
            this.txtEnd.Tag = "1";
            this.txtEnd.Text = "0";
            this.txtEnd.Leave += new EventHandler(this.txtStart_Leave);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(12, 0x26);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x23, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "终点:";
            this.txtStart.Location = new Point(0x40, 7);
            this.txtStart.Name = "txtStart";
            this.txtStart.Size = new Size(0x80, 0x15);
            this.txtStart.TabIndex = 2;
            this.txtStart.Tag = "0";
            this.txtStart.Text = "0";
            this.txtStart.Leave += new EventHandler(this.txtStart_Leave);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 10);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x23, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "起点:";
            this.btnChangeEffic.ImageIndex = 0;
            this.btnChangeEffic.ImageList = this.imageList1;
            this.btnChangeEffic.Location = new Point(0xc1, -1);
            this.btnChangeEffic.Name = "btnChangeEffic";
            this.btnChangeEffic.RightToLeft = RightToLeft.Yes;
            this.btnChangeEffic.Size = new Size(0x10, 0x10);
            this.btnChangeEffic.TabIndex = 2;
            this.btnChangeEffic.UseVisualStyleBackColor = true;
            this.btnChangeEffic.Click += new EventHandler(this.btnChangeEffic_Click);
            this.imageList1.ImageStream = (ImageListStreamer) resources.GetObject("imageList1.ImageStream");
            this.imageList1.TransparentColor = Color.White;
            this.imageList1.Images.SetKeyName(0, "Bitmap4.bmp");
            this.imageList1.Images.SetKeyName(1, "Bitmap5.bmp");
            this.panel2.BackColor = SystemColors.ControlDark;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btnChangeEffic);
            this.panel2.Location = new Point(3, 0x10);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0xd1, 0x10);
            this.panel2.TabIndex = 3;
            this.btnAddGemoetricEffic.ImageIndex = 1;
            this.btnAddGemoetricEffic.ImageList = this.imageList1;
            this.btnAddGemoetricEffic.Location = new Point(0xc4, 0);
            this.btnAddGemoetricEffic.Name = "btnAddGemoetricEffic";
            this.btnAddGemoetricEffic.RightToLeft = RightToLeft.Yes;
            this.btnAddGemoetricEffic.Size = new Size(0x10, 0x10);
            this.btnAddGemoetricEffic.TabIndex = 4;
            this.btnAddGemoetricEffic.UseVisualStyleBackColor = true;
            this.btnAddGemoetricEffic.Click += new EventHandler(this.btnAddGemoetricEffic_Click);
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.修改ToolStripMenuItem, this.删除ToolStripMenuItem });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new Size(0x63, 0x30);
            this.修改ToolStripMenuItem.Name = "修改ToolStripMenuItem";
            this.修改ToolStripMenuItem.Size = new Size(0x62, 0x16);
            this.修改ToolStripMenuItem.Text = "修改";
            this.修改ToolStripMenuItem.Click += new EventHandler(this.修改ToolStripMenuItem_Click);
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new Size(0x62, 0x16);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new EventHandler(this.删除ToolStripMenuItem_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.Control;
            base.Controls.Add(this.btnAddGemoetricEffic);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Name = "GeometricCutCurverPage";
            base.Size = new Size(0xd4, 0x5d);
            base.Load += new EventHandler(this.GeometricCutCurverPage_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void txtStart_Leave(object sender, EventArgs e)
        {
            double val = 0.0;
            try
            {
                val = double.Parse((sender as TextBox).Text);
            }
            catch
            {
                return;
            }
            (base.m_pGeometricEffect as IGraphicAttributes).set_Value((int) (sender as TextBox).Tag, val);
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.m_pControl != null)
            {
                this.m_pControl.RemoveControl(this);
            }
        }

        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGeometricEffectList list = new frmGeometricEffectList {
                BasicSymbolLayerBaseControl = this.m_pControl
            };
            if ((list.ShowDialog() == DialogResult.OK) && (this.m_pControl != null))
            {
                this.m_pControl.ReplaceControl(this, list.SelectControl);
            }
        }
    }
}

