using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.ControlExtend;
using Yutai.ArcGIS.Common.Symbol;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;
using IPropertyPage = Yutai.ArcGIS.Common.BaseClasses.IPropertyPage;
using IPropertyPageEvents = Yutai.ArcGIS.Common.BaseClasses.IPropertyPageEvents;
using OnValueChangeEventHandler = Yutai.ArcGIS.Common.BaseClasses.OnValueChangeEventHandler;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public partial class LegendPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private IAreaPatch iareaPatch_0 = null;
        private ILegend ilegend_0 = null;
        private ILinePatch ilinePatch_0 = null;
        private IMapSurroundFrame imapSurroundFrame_0 = null;
        private IStyleGallery istyleGallery_0 = ApplicationBase.StyleGallery;
        private ITextSymbol itextSymbol_0 = null;
        internal static short m_ApplyCount;
        internal static short m_InitCount;
        private string string_0 = "图例";

        public event OnValueChangeEventHandler OnValueChange;

        static LegendPropertyPage()
        {
            old_acctor_mc();
        }

        public LegendPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                ILegendFormat format = this.ilegend_0.Format;
                format.TitlePosition = (esriRectanglePosition) (this.cboTitlePosition.SelectedIndex + 1);
                format.TitleSymbol = this.itextSymbol_0;
                format.DefaultAreaPatch = this.cboAreaPatches.GetSelectStyleGalleryItem().Item as IAreaPatch;
                format.DefaultLinePatch = this.cboLinePatches.GetSelectStyleGalleryItem().Item as ILinePatch;
                format.DefaultPatchWidth = double.Parse(this.txtWidth.Text);
                format.DefaultPatchHeight = double.Parse(this.txtHeight.Text);
                (this.ilegend_0 as IReadingDirection).RightToLeft = this.chkRightToLeft.Checked;
                format.ShowTitle = this.chkShow.Checked;
                this.ilegend_0.Title = this.txtTitle.Text;
                for (int i = 0; i < this.listView1.Items.Count; i++)
                {
                    this.method_4(i, this.listView1.Items[i].SubItems[1].Text, format);
                }
            }
        }

        private void btnTitleSymbol_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(ApplicationBase.StyleGallery);
                    selector.SetSymbol(this.itextSymbol_0);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        this.itextSymbol_0 = selector.GetSymbol() as ITextSymbol;
                        this.method_1();
                    }
                }
            }
            catch
            {
            }
        }

        public void Cancel()
        {
        }

        private void cboAreaPatches_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    this.method_1();
                }
                catch
                {
                }
            }
        }

        private void cboLinePatches_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    this.method_1();
                }
                catch
                {
                }
            }
        }

        private void cboTitlePosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.method_1();
            }
        }

        private void chkRightToLeft_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.method_1();
            }
        }

        private void chkRightToLeft_Click(object sender, EventArgs e)
        {
        }

        private void chkShow_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.txtTitle.Enabled = this.chkShow.Checked;
                this.method_1();
            }
        }

        private void chkShow_Click(object sender, EventArgs e)
        {
        }

 private void LegendPropertyPage_Load(object sender, EventArgs e)
        {
            if (this.istyleGallery_0 != null)
            {
                IStyleGalleryItem item = null;
                IEnumStyleGalleryItem item2 = this.istyleGallery_0.get_Items("Line Patches", "", "");
                item2.Reset();
                for (item = item2.Next(); item != null; item = item2.Next())
                {
                    this.cboLinePatches.Add(item);
                }
                if (this.cboLinePatches.Items.Count > 0)
                {
                    this.cboLinePatches.SelectedIndex = 0;
                }
                item2 = this.istyleGallery_0.get_Items("Area Patches", "", "");
                item2.Reset();
                for (item = item2.Next(); item != null; item = item2.Next())
                {
                    this.cboAreaPatches.Add(item);
                }
                if (this.cboAreaPatches.Items.Count > 0)
                {
                    this.cboAreaPatches.SelectedIndex = 0;
                }
            }
            this.method_0();
            this.bool_0 = true;
            this.lvcolumnHeader_0.ColumnStyle = ListViewColumnStyle.ReadOnly;
            this.lvcolumnHeader_1.ColumnStyle = ListViewColumnStyle.EditBox;
        }

        private void method_0()
        {
            this.txtTitle.Text = this.ilegend_0.Title;
            ILegendFormat format = this.ilegend_0.Format;
            this.chkShow.Checked = format.ShowTitle;
            this.txtTitle.Enabled = format.ShowTitle;
            this.cboTitlePosition.SelectedIndex = ((int) format.TitlePosition) - 1;
            this.txtWidth.Text = format.DefaultPatchWidth.ToString("#.##");
            this.txtHeight.Text = format.DefaultPatchHeight.ToString("#.##");
            this.listView1.Items[0].SubItems[1].Text = format.HeadingGap.ToString("#.##");
            this.listView1.Items[1].SubItems[1].Text = format.VerticalItemGap.ToString("#.##");
            this.listView1.Items[2].SubItems[1].Text = format.HorizontalItemGap.ToString("#.##");
            this.listView1.Items[3].SubItems[1].Text = format.LayerNameGap.ToString("#.##");
            this.listView1.Items[4].SubItems[1].Text = format.GroupGap.ToString("#.##");
            this.listView1.Items[5].SubItems[1].Text = format.TitleGap.ToString("#.##");
            this.listView1.Items[6].SubItems[1].Text = format.TextGap.ToString("#.##");
            this.listView1.Items[7].SubItems[1].Text = format.VerticalPatchGap.ToString("#.##");
            this.listView1.Items[8].SubItems[1].Text = format.HorizontalPatchGap.ToString("#.##");
            IStyleGalleryItem oO = new MyStyleGalleryItem {
                Name = "<定制>"
            };
            this.ilinePatch_0 = format.DefaultLinePatch;
            oO.Item = this.ilinePatch_0;
            this.cboLinePatches.SelectStyleGalleryItem(oO);
            oO = new MyStyleGalleryItem {
                Name = "<定制>"
            };
            this.iareaPatch_0 = format.DefaultAreaPatch;
            oO.Item = this.iareaPatch_0;
            this.cboAreaPatches.SelectStyleGalleryItem(oO);
            this.chkRightToLeft.Checked = (this.ilegend_0 as IReadingDirection).RightToLeft;
        }

        private void method_1()
        {
            this.bool_1 = true;
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
            }
        }

        private void method_2(object sender, ValueChangedEventArgs e)
        {
            try
            {
                double.Parse(e.NewValue.ToString());
                this.method_1();
            }
            catch
            {
            }
        }

        private void method_3(int int_0, double double_0)
        {
            ILegendFormat format = this.ilegend_0.Format;
            switch (int_0)
            {
                case 0:
                    format.HeadingGap = double_0;
                    break;

                case 1:
                    format.VerticalItemGap = double_0;
                    break;

                case 2:
                    format.HorizontalItemGap = double_0;
                    break;

                case 3:
                    format.LayerNameGap = double_0;
                    break;

                case 4:
                    format.GroupGap = double_0;
                    break;

                case 5:
                    format.TitleGap = double_0;
                    break;

                case 6:
                    format.TextGap = double_0;
                    break;

                case 7:
                    format.VerticalPatchGap = double_0;
                    break;

                case 8:
                    format.HorizontalPatchGap = double_0;
                    break;
            }
        }

        private void method_4(int int_0, string string_1, ILegendFormat ilegendFormat_0)
        {
            try
            {
                double num = double.Parse(string_1);
                switch (int_0)
                {
                    case 0:
                        ilegendFormat_0.HeadingGap = num;
                        return;

                    case 1:
                        ilegendFormat_0.VerticalItemGap = num;
                        return;

                    case 2:
                        ilegendFormat_0.HorizontalItemGap = num;
                        return;

                    case 3:
                        ilegendFormat_0.LayerNameGap = num;
                        return;

                    case 4:
                        ilegendFormat_0.GroupGap = num;
                        return;

                    case 5:
                        ilegendFormat_0.TitleGap = num;
                        return;

                    case 6:
                        ilegendFormat_0.TextGap = num;
                        return;

                    case 7:
                        ilegendFormat_0.VerticalPatchGap = num;
                        return;

                    case 8:
                        ilegendFormat_0.HorizontalPatchGap = num;
                        return;
                }
            }
            catch
            {
            }
        }

        private static void old_acctor_mc()
        {
            m_InitCount = 0;
            m_ApplyCount = 0;
        }

        public void ResetControl()
        {
            this.bool_0 = false;
            this.method_0();
            this.bool_0 = true;
        }

        public void SetObjects(object object_0)
        {
            this.imapSurroundFrame_0 = object_0 as IMapSurroundFrame;
            this.ilegend_0 = this.imapSurroundFrame_0.MapSurround as ILegend;
            ILegendFormat format = this.ilegend_0.Format;
            this.itextSymbol_0 = format.TitleSymbol;
        }

        private void txtHeight_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    this.method_1();
                }
                catch
                {
                }
            }
        }

        private void txtTitle_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.method_1();
            }
        }

        private void txtWidth_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    this.method_1();
                }
                catch
                {
                }
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_1;
            }
        }

        int IPropertyPage.Height
        {
            get
            {
                return base.Height;
            }
        }

        int IPropertyPage.Width
        {
            get
            {
                return base.Width;
            }
        }

        public string Title
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }
    }
}

