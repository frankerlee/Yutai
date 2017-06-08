using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    public class frmNewTemplate : Form
    {
        private Button btnFinish;
        private Button btnLast;
        private Button btnNext;
        private Button button4;
        private IContainer components = null;
        private Dictionary<IFeatureLayer, List<JLKEditTemplate>> m_list = new Dictionary<IFeatureLayer, List<JLKEditTemplate>>();
        private int m_step = 0;
        private Panel panel1;
        private Panel panel2;
        private SelectCreateTemplateLayer selectLayerCtrl = new SelectCreateTemplateLayer();
        private SelectTemplateCtrl selectTemplateCtrl = new SelectTemplateCtrl();

        public frmNewTemplate()
        {
            this.InitializeComponent();
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            if (this.m_step == 0)
            {
                this.selectLayerCtrl.Apply();
            }
            List<JLKEditTemplate> template = new List<JLKEditTemplate>();
            foreach (KeyValuePair<IFeatureLayer, List<JLKEditTemplateWrap>> pair in this.selectLayerCtrl.Templates)
            {
                foreach (JLKEditTemplateWrap wrap in pair.Value)
                {
                    if (wrap.IsUse)
                    {
                        template.Add(wrap.EditTemplate);
                    }
                }
            }
            if (template.Count > 0)
            {
                EditTemplateManager.AddMoreEditTemplate(template);
            }
            base.DialogResult = DialogResult.OK;
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (this.m_step == 1)
            {
                this.selectLayerCtrl.Visible = true;
                this.selectTemplateCtrl.Visible = false;
                this.btnLast.Enabled = false;
            }
            else
            {
                this.selectTemplateCtrl.LastStep();
                this.btnNext.Enabled = true;
            }
            this.m_step--;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (this.m_step == 0)
            {
                this.selectLayerCtrl.Apply();
                this.selectLayerCtrl.Visible = false;
                this.selectTemplateCtrl.Templates = this.selectLayerCtrl.Templates;
                this.selectTemplateCtrl.Visible = true;
                if (!this.selectTemplateCtrl.NextStep(false))
                {
                    this.btnNext.Enabled = false;
                }
                this.btnLast.Enabled = true;
            }
            else if (!this.selectTemplateCtrl.NextStep(true))
            {
                this.btnNext.Enabled = false;
            }
            this.m_step++;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmNewTemplate_Load(object sender, EventArgs e)
        {
            this.btnNext.Enabled = false;
            this.btnLast.Enabled = false;
            this.btnFinish.Enabled = false;
            this.selectLayerCtrl.OnValueChange += new OnValueChangeHandler(this.selectLayerCtrl_OnValueChange);
            this.selectLayerCtrl.Map = this.Map;
            this.selectTemplateCtrl.Dock = DockStyle.Fill;
            this.selectTemplateCtrl.Visible = false;
            this.panel1.Controls.Add(this.selectTemplateCtrl);
            this.selectLayerCtrl.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(this.selectLayerCtrl);
        }

        private void InitializeComponent()
        {
            this.panel2 = new Panel();
            this.button4 = new Button();
            this.btnFinish = new Button();
            this.btnNext = new Button();
            this.btnLast = new Button();
            this.panel1 = new Panel();
            this.panel2.SuspendLayout();
            base.SuspendLayout();
            this.panel2.Controls.Add(this.button4);
            this.panel2.Controls.Add(this.btnFinish);
            this.panel2.Controls.Add(this.btnNext);
            this.panel2.Controls.Add(this.btnLast);
            this.panel2.Dock = DockStyle.Bottom;
            this.panel2.Location = new Point(0, 0xde);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x1b2, 0x25);
            this.panel2.TabIndex = 1;
            this.button4.DialogResult = DialogResult.Cancel;
            this.button4.Location = new Point(0x146, 6);
            this.button4.Name = "button4";
            this.button4.Size = new Size(0x4b, 0x17);
            this.button4.TabIndex = 3;
            this.button4.Text = "取消";
            this.button4.UseVisualStyleBackColor = true;
            this.btnFinish.Location = new Point(0xe2, 6);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new Size(0x4b, 0x17);
            this.btnFinish.TabIndex = 2;
            this.btnFinish.Text = "完成";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += new EventHandler(this.btnFinish_Click);
            this.btnNext.Location = new Point(0x91, 6);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(0x4b, 0x17);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = "下一步";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.btnLast.Location = new Point(0x35, 6);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new Size(0x4b, 0x17);
            this.btnLast.TabIndex = 0;
            this.btnLast.Text = "上一步";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new EventHandler(this.btnLast_Click);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x1b2, 0xde);
            this.panel1.TabIndex = 2;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1b2, 0x103);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.panel2);
            base.Name = "frmNewTemplate";
            this.Text = "编辑模板创建向导";
            base.Load += new EventHandler(this.frmNewTemplate_Load);
            this.panel2.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void selectLayerCtrl_OnValueChange(bool b)
        {
            this.btnNext.Enabled = this.selectLayerCtrl.NexHasEnable;
            this.btnFinish.Enabled = this.selectLayerCtrl.CanApply;
        }

        public IMap Map { get; set; }

        public Dictionary<IFeatureLayer, List<JLKEditTemplate>> TemplateList
        {
            get
            {
                return this.m_list;
            }
        }
    }
}

