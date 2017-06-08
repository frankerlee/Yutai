using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Carto.UI
{
    public class TinLayerRenderPropertyPage : UserControl, ILayerAndStandaloneTablePropertyPage
    {
        private bool bool_0 = false;
        private SimpleButton btnAdd;
        private SimpleButton btnDelete;
        private CheckedListBox checkedListBox1;
        private IBasicMap ibasicMap_0 = null;
        private IContainer icontainer_0 = null;
        private ITinLayer itinLayer_0 = null;
        private IUserControl iuserControl_0 = null;
        private Label label1;
        private List<ITinRenderer> list_0 = new List<ITinRenderer>();
        private Panel panel;
        private TinColorRampRenderPropertyPage tinColorRampRenderPropertyPage_0;
        private TinSimpleRenderCtrl tinSimpleRenderCtrl_0;
        private TinUniqueRenderPropertyPage tinUniqueRenderPropertyPage_0;

        public TinLayerRenderPropertyPage()
        {
            this.InitializeComponent();
            this.tinSimpleRenderCtrl_0 = new TinSimpleRenderCtrl();
            this.tinColorRampRenderPropertyPage_0 = new TinColorRampRenderPropertyPage();
            this.tinUniqueRenderPropertyPage_0 = new TinUniqueRenderPropertyPage();
            this.tinSimpleRenderCtrl_0.Visible = false;
            this.tinSimpleRenderCtrl_0.Dock = DockStyle.Fill;
            this.tinColorRampRenderPropertyPage_0.Visible = false;
            this.tinColorRampRenderPropertyPage_0.Dock = DockStyle.Fill;
            this.tinUniqueRenderPropertyPage_0.Visible = false;
            this.tinUniqueRenderPropertyPage_0.Dock = DockStyle.Fill;
            this.panel.Controls.Add(this.tinSimpleRenderCtrl_0);
            this.panel.Controls.Add(this.tinColorRampRenderPropertyPage_0);
            this.panel.Controls.Add(this.tinUniqueRenderPropertyPage_0);
        }

        public bool Apply()
        {
            int num;
            if (this.list_0.Count > 0)
            {
                num = 0;
                while (num < this.list_0.Count)
                {
                    try
                    {
                        this.itinLayer_0.DeleteRenderer(this.list_0[num]);
                    }
                    catch (Exception exception)
                    {
                        exception.ToString();
                    }
                    num++;
                }
            }
            this.list_0.Clear();
            for (num = 0; num < this.checkedListBox1.Items.Count; num++)
            {
                TinRenderWrap wrap = this.checkedListBox1.Items[num] as TinRenderWrap;
                int index = this.checkedListBox1.CheckedIndices.IndexOf(num);
                wrap.Update(index != -1);
                if (wrap.IsNew)
                {
                    this.itinLayer_0.AddRenderer(wrap.TinRender);
                }
            }
            return true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddTinRender render = new frmAddTinRender {
                TinLayer = this.itinLayer_0
            };
            render.OnAddTinRender += new frmAddTinRender.OnAddTinRenderHander(this.method_0);
            render.ShowDialog();
            render.OnAddTinRender -= new frmAddTinRender.OnAddTinRenderHander(this.method_0);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            for (int i = this.checkedListBox1.SelectedIndices.Count - 1; i >= 0; i--)
            {
                TinRenderWrap wrap = this.checkedListBox1.SelectedItems[i] as TinRenderWrap;
                if (!wrap.IsNew)
                {
                    this.list_0.Add(wrap.OldTinRender);
                }
                this.checkedListBox1.Items.Remove(this.checkedListBox1.SelectedItems[i]);
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnDelete.Enabled = this.checkedListBox1.SelectedIndices.Count > 0;
            int selectedIndex = this.checkedListBox1.SelectedIndex;
            if (this.iuserControl_0 != null)
            {
                this.iuserControl_0.Visible = false;
            }
            if (selectedIndex != -1)
            {
                ITinRenderer tinRender = (this.checkedListBox1.SelectedItem as TinRenderWrap).TinRender;
                if (tinRender is ITinColorRampRenderer)
                {
                    this.iuserControl_0 = this.tinColorRampRenderPropertyPage_0;
                    this.tinColorRampRenderPropertyPage_0.TinRenderer = tinRender;
                    this.tinColorRampRenderPropertyPage_0.CurrentLayer = this.itinLayer_0;
                    this.tinColorRampRenderPropertyPage_0.Visible = true;
                }
                else if (tinRender is ITinSingleSymbolRenderer)
                {
                    this.iuserControl_0 = this.tinSimpleRenderCtrl_0;
                    this.tinSimpleRenderCtrl_0.CurrentLayer = this.itinLayer_0;
                    this.tinSimpleRenderCtrl_0.TinRenderer = tinRender;
                    this.tinSimpleRenderCtrl_0.Visible = true;
                }
                else
                {
                    this.iuserControl_0 = this.tinUniqueRenderPropertyPage_0;
                    this.tinUniqueRenderPropertyPage_0.CurrentLayer = this.itinLayer_0;
                    this.tinUniqueRenderPropertyPage_0.TinRenderer = tinRender;
                    this.tinUniqueRenderPropertyPage_0.Visible = true;
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

        private void InitializeComponent()
        {
            this.checkedListBox1 = new CheckedListBox();
            this.label1 = new Label();
            this.btnAdd = new SimpleButton();
            this.btnDelete = new SimpleButton();
            this.panel = new Panel();
            base.SuspendLayout();
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new Point(14, 0x18);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new Size(0x6b, 0x84);
            this.checkedListBox1.TabIndex = 0;
            this.checkedListBox1.SelectedIndexChanged += new EventHandler(this.checkedListBox1_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(14, 6);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "显示";
            this.btnAdd.Location = new Point(0x1b, 0xac);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(0x4b, 0x17);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "添加";
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.btnDelete.Location = new Point(0x1b, 0xc9);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(0x4b, 0x17);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.panel.Dock = DockStyle.Right;
            this.panel.Location = new Point(0x7f, 0);
            this.panel.Name = "panel";
            this.panel.Size = new Size(0x191, 0x10c);
            this.panel.TabIndex = 4;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.panel);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.btnAdd);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.checkedListBox1);
            base.Name = "TinLayerRenderPropertyPage";
            base.Size = new Size(0x210, 0x10c);
            base.Load += new EventHandler(this.TinLayerRenderPropertyPage_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void method_0(ITinRenderer itinRenderer_0)
        {
            this.checkedListBox1.Items.Add(new TinRenderWrap(itinRenderer_0, true), itinRenderer_0.Visible);
        }

        private void TinLayerRenderPropertyPage_Load(object sender, EventArgs e)
        {
            this.btnDelete.Enabled = false;
            if (this.itinLayer_0 != null)
            {
                for (int i = 0; i < this.itinLayer_0.RendererCount; i++)
                {
                    ITinRenderer renderer = this.itinLayer_0.GetRenderer(i);
                    this.checkedListBox1.Items.Add(new TinRenderWrap(renderer, false), renderer.Visible);
                }
            }
        }

        public IBasicMap FocusMap
        {
            set
            {
                this.ibasicMap_0 = value;
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_0;
            }
        }

        public object SelectItem
        {
            set
            {
                this.itinLayer_0 = value as ITinLayer;
            }
        }

        internal class TinRenderWrap
        {
            private bool bool_0 = false;
            private ITinRenderer itinRenderer_0 = null;
            private ITinRenderer itinRenderer_1 = null;

            internal TinRenderWrap(ITinRenderer itinRenderer_2, bool bool_1)
            {
                this.itinRenderer_1 = itinRenderer_2;
                IObjectCopy copy = new ObjectCopyClass();
                this.itinRenderer_0 = copy.Copy(this.itinRenderer_1) as ITinRenderer;
                this.bool_0 = bool_1;
            }

            public override string ToString()
            {
                return this.itinRenderer_0.Name;
            }

            internal void Update(bool bool_1)
            {
                IObjectCopy copy = new ObjectCopyClass();
                object pOverwriteObject = this.itinRenderer_1;
                copy.Overwrite(this.itinRenderer_0, ref pOverwriteObject);
                this.itinRenderer_1.Visible = bool_1;
            }

            public bool IsNew
            {
                get
                {
                    return this.bool_0;
                }
            }

            internal ITinRenderer OldTinRender
            {
                get
                {
                    return this.itinRenderer_1;
                }
            }

            internal ITinRenderer TinRender
            {
                get
                {
                    return this.itinRenderer_0;
                }
            }
        }
    }
}

