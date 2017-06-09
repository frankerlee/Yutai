﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.Editor;
using Yutai.ArcGIS.Common.Wrapper;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    public class SelectCreateTemplateLayer : UserControl
    {
        private Button btnClearAll;
        private Button btnSelectAll;
        private Button button1;
        internal bool CanApply = false;
        private CheckedListBox checkedListBox1;
        private IContainer components = null;
        private Label label1;
        private bool m_CanDo = true;
        internal bool NexHasEnable = false;

        internal event OnValueChangeHandler OnValueChange;

        public SelectCreateTemplateLayer()
        {
            this.InitializeComponent();
        }

        internal void Apply()
        {
            if (this.Templates == null)
            {
                this.Templates = new Dictionary<IFeatureLayer, List<YTEditTemplateWrap>>();
            }
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                bool itemChecked = this.checkedListBox1.GetItemChecked(i);
                LayerObject obj2 = this.checkedListBox1.Items[i] as LayerObject;
                if (itemChecked)
                {
                    if (!this.Templates.ContainsKey(obj2.Layer as IFeatureLayer))
                    {
                        List<YTEditTemplate> list = YTEditTemplateFactory.Create(obj2.Layer as IFeatureLayer);
                        List<YTEditTemplateWrap> list2 = new List<YTEditTemplateWrap>();
                        foreach (YTEditTemplate template in list)
                        {
                            list2.Add(new YTEditTemplateWrap(template));
                        }
                        this.Templates.Add(obj2.Layer as IFeatureLayer, list2);
                    }
                }
                else if (this.Templates.ContainsKey(obj2.Layer as IFeatureLayer))
                {
                    this.Templates.Remove(obj2.Layer as IFeatureLayer);
                }
            }
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                if (this.checkedListBox1.GetItemChecked(i))
                {
                    this.checkedListBox1.SetItemChecked(i, false);
                }
            }
            this.CanApply = false;
            this.NexHasEnable = false;
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                if (!this.checkedListBox1.GetItemChecked(i))
                {
                    this.checkedListBox1.SetItemChecked(i, true);
                }
            }
            this.CanApply = true;
            this.NexHasEnable = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                LayerObject obj2 = this.checkedListBox1.Items[i] as LayerObject;
                this.checkedListBox1.SetItemChecked(i, obj2.Layer.Visible);
            }
            this.CanApply = this.checkedListBox1.CheckedIndices.Count > 0;
            this.NexHasEnable = this.checkedListBox1.CheckedIndices.Count > 0;
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (this.m_CanDo)
            {
                int num;
                LayerObject obj2;
                this.CanApply = false;
                this.NexHasEnable = false;
                if (e.NewValue != CheckState.Checked)
                {
                    for (num = 0; num < this.checkedListBox1.Items.Count; num++)
                    {
                        if ((num != e.Index) && this.checkedListBox1.GetItemChecked(num))
                        {
                            this.CanApply = true;
                            break;
                        }
                    }
                }
                else
                {
                    this.CanApply = true;
                    obj2 = this.checkedListBox1.Items[e.Index] as LayerObject;
                    if (obj2.Layer is IFDOGraphicsLayer)
                    {
                        this.NexHasEnable = true;
                    }
                    else if ((obj2.Layer is IGeoFeatureLayer) && ((obj2.Layer as IGeoFeatureLayer).Renderer is IUniqueValueRenderer))
                    {
                        this.NexHasEnable = true;
                    }
                }
                if (!this.NexHasEnable)
                {
                    for (num = 0; num < this.checkedListBox1.Items.Count; num++)
                    {
                        if (num != e.Index)
                        {
                            obj2 = this.checkedListBox1.Items[num] as LayerObject;
                            if (obj2.Layer is IFDOGraphicsLayer)
                            {
                                this.NexHasEnable = true;
                            }
                            else if ((obj2.Layer is IGeoFeatureLayer) && ((obj2.Layer as IGeoFeatureLayer).Renderer is IUniqueValueRenderer))
                            {
                                this.NexHasEnable = true;
                            }
                        }
                        if (this.NexHasEnable)
                        {
                            break;
                        }
                    }
                }
                if (this.OnValueChange != null)
                {
                    this.OnValueChange(this.CanApply);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.checkedListBox1 = new CheckedListBox();
            this.btnClearAll = new Button();
            this.btnSelectAll = new Button();
            this.label1 = new Label();
            this.button1 = new Button();
            base.SuspendLayout();
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new Point(14, 30);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new Size(0x107, 0xa4);
            this.checkedListBox1.TabIndex = 8;
            this.checkedListBox1.ItemCheck += new ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            this.btnClearAll.Location = new Point(0x12b, 0x58);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new Size(0x66, 0x17);
            this.btnClearAll.TabIndex = 7;
            this.btnClearAll.Text = "全部清除";
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnClearAll.Click += new EventHandler(this.btnClearAll_Click);
            this.btnSelectAll.Location = new Point(0x12b, 0x3b);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new Size(0x66, 0x17);
            this.btnSelectAll.TabIndex = 6;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new EventHandler(this.btnSelectAll_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 4);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x7d, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "选择要创建模板的图层";
            this.button1.Location = new Point(0x12b, 30);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x66, 0x17);
            this.button1.TabIndex = 10;
            this.button1.Text = "选择可见图层";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.button1);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.checkedListBox1);
            base.Controls.Add(this.btnClearAll);
            base.Controls.Add(this.btnSelectAll);
            base.Name = "SelectCreateTemplateLayer";
            base.Size = new Size(0x19f, 0xd0);
            base.Load += new EventHandler(this.SelectCreateTemplateLayer_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void SelectCreateTemplateLayer_Load(object sender, EventArgs e)
        {
            UID uid = new UIDClass {
                Value = "{40A9E885-5533-11d0-98BE-00805F7CED21}"
            };
            IEnumLayer layer = this.Map.get_Layers(uid, true);
            layer.Reset();
            for (ILayer layer2 = layer.Next(); layer2 != null; layer2 = layer.Next())
            {
                if ((layer2 is IFeatureLayer) && Yutai.ArcGIS.Common.Editor.Editor.LayerCanEdit(layer2 as IFeatureLayer))
                {
                    this.checkedListBox1.Items.Add(new LayerObject(layer2), false);
                }
            }
        }

        internal IMap Map { get; set; }

        internal Dictionary<IFeatureLayer, List<YTEditTemplateWrap>> Templates { get; set; }
    }
}
