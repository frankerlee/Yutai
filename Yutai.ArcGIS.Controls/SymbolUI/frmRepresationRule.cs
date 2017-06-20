using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Controls.SymbolLib;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    internal class frmRepresationRule : Form
    {
        private Button btnAdd;
        private Button btnCnacel;
        private IContainer components = null;
        internal static IStyleGallery m_pSG = null;
        private RepresationRuleCtrl m_RepresationRuleCtrl = new RepresationRuleCtrl();

        public frmRepresationRule()
        {
            this.InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.OK;
            base.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmRepresationRule_Load(object sender, EventArgs e)
        {
            base.Controls.Add(this.m_RepresationRuleCtrl);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRepresationRule));
            this.btnAdd = new Button();
            this.btnCnacel = new Button();
            base.SuspendLayout();
            this.btnAdd.Location = new Point(0xa9, 0x173);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(0x27, 0x19);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "确定";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.btnCnacel.Location = new Point(0xf9, 0x173);
            this.btnCnacel.Name = "btnCnacel";
            this.btnCnacel.Size = new Size(0x27, 0x19);
            this.btnCnacel.TabIndex = 3;
            this.btnCnacel.Text = "取消";
            this.btnCnacel.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x124, 0x198);
            base.Controls.Add(this.btnCnacel);
            base.Controls.Add(this.btnAdd);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmRepresationRule";
            this.Text = "Representation Rule";
            base.Load += new EventHandler(this.frmRepresationRule_Load);
            base.ResumeLayout(false);
        }

        public IRepresentationRuleItem RepresentationRuleItem
        {
            get
            {
                return this.m_RepresationRuleCtrl.RepresentationRuleItem;
            }
            set
            {
                this.m_RepresationRuleCtrl.RepresentationRuleItem = value;
            }
        }
    }
}

