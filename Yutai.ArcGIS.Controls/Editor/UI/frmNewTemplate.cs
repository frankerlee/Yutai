using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using Yutai.ArcGIS.Common.Editor;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    public partial class frmNewTemplate : Form
    {
        private Dictionary<IFeatureLayer, List<YTEditTemplate>> m_list = new Dictionary<IFeatureLayer, List<YTEditTemplate>>();
        private int m_step = 0;
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
            List<YTEditTemplate> template = new List<YTEditTemplate>();
            foreach (KeyValuePair<IFeatureLayer, List<YTEditTemplateWrap>> pair in this.selectLayerCtrl.Templates)
            {
                foreach (YTEditTemplateWrap wrap in pair.Value)
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

 private void selectLayerCtrl_OnValueChange(bool b)
        {
            this.btnNext.Enabled = this.selectLayerCtrl.NexHasEnable;
            this.btnFinish.Enabled = this.selectLayerCtrl.CanApply;
        }

        public IMap Map { get; set; }

        public Dictionary<IFeatureLayer, List<YTEditTemplate>> TemplateList
        {
            get
            {
                return this.m_list;
            }
        }
    }
}

