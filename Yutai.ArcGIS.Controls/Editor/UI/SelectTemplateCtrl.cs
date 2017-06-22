using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    public partial class SelectTemplateCtrl : UserControl
    {
        private List<List<YTEditTemplateWrap>> EditTemplates = new List<List<YTEditTemplateWrap>>();
        private List<IFeatureLayer> Layers = new List<IFeatureLayer>();
        internal int Step = 0;
        internal int StepCount = 0;

        public SelectTemplateCtrl()
        {
            this.InitializeComponent();
        }

 internal void InitControl()
        {
            this.listView1.Items.Clear();
            string[] items = new string[2];
            foreach (YTEditTemplateWrap wrap in this.EditTemplates[this.Step])
            {
                items[0] = wrap.EditTemplate.Name;
                items[1] = wrap.EditTemplate.Name;
                ListViewItem item = new ListViewItem(items) {
                    Tag = wrap,
                    Checked = wrap.IsUse
                };
                if (!this.imageList1.Images.ContainsKey(wrap.EditTemplate.ImageKey) && (wrap.EditTemplate.Bitmap != null))
                {
                    this.imageList1.Images.Add((string) wrap.EditTemplate.ImageKey, (Image)wrap.EditTemplate.Bitmap);
                }
                item.ImageKey = wrap.EditTemplate.ImageKey;
                this.listView1.Items.Add(item);
            }
        }

 public bool LastStep()
        {
            this.Step--;
            this.InitControl();
            return (this.Step > 0);
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            (e.Item.Tag as YTEditTemplateWrap).IsUse = e.Item.Checked;
        }

        public bool NextStep(bool IsAdd)
        {
            if (IsAdd)
            {
                this.Step++;
            }
            this.InitControl();
            return (this.Step < (this.StepCount - 1));
        }

        private void SelectTemplateCtrl_Load(object sender, EventArgs e)
        {
            foreach (KeyValuePair<IFeatureLayer, List<YTEditTemplateWrap>> pair in this.Templates)
            {
                if (pair.Key is IFDOGraphicsLayer)
                {
                    this.Layers.Add(pair.Key);
                    this.EditTemplates.Add(pair.Value);
                }
                else if (pair.Value.Count > 1)
                {
                    this.Layers.Add(pair.Key);
                    this.EditTemplates.Add(pair.Value);
                }
            }
            this.StepCount = this.Layers.Count;
        }

        internal Dictionary<IFeatureLayer, List<YTEditTemplateWrap>> Templates { get; set; }
    }
}

