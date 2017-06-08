using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.ControlExtend;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class LegendPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private SimpleButton btnTitleSymbol;
        private StyleComboBox cboAreaPatches;
        private StyleComboBox cboLinePatches;
        private ComboBoxEdit cboTitlePosition;
        private CheckEdit chkRightToLeft;
        private CheckEdit chkShow;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private IAreaPatch iareaPatch_0 = null;
        private IContainer icontainer_0;
        private ILegend ilegend_0 = null;
        private ILinePatch ilinePatch_0 = null;
        private IMapSurroundFrame imapSurroundFrame_0 = null;
        private IStyleGallery istyleGallery_0 = ApplicationBase.StyleGallery;
        private ITextSymbol itextSymbol_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private EditListView listView1;
        private LVColumnHeader lvcolumnHeader_0;
        private LVColumnHeader lvcolumnHeader_1;
        internal static short m_ApplyCount;
        internal static short m_InitCount;
        private string string_0 = "图例";
        private TextEdit txtHeight;
        private MemoEdit txtTitle;
        private TextEdit txtWidth;

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
                    selector.SetStyleGallery(this.istyleGallery_0);
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

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_2);
        }

        private void InitializeComponent()
        {
            this.icontainer_0 = new Container();
            ListViewItem item = new ListViewItem(new string[] { "标题和项", "8" }, -1);
            ListViewItem item2 = new ListViewItem(new string[] { "项", "5" }, -1);
            ListViewItem item3 = new ListViewItem(new string[] { "列", "5" }, -1);
            ListViewItem item4 = new ListViewItem(new string[] { "图层名称和组", "5" }, -1);
            ListViewItem item5 = new ListViewItem(new string[] { "组", "5" }, -1);
            ListViewItem item6 = new ListViewItem(new string[] { "标题和类", "5" }, -1);
            ListViewItem item7 = new ListViewItem(new string[] { "标注和描述", "5" }, -1);
            ListViewItem item8 = new ListViewItem(new string[] { "区块(垂直)", "5" }, -1);
            ListViewItem item9 = new ListViewItem(new string[] { "区块和标注", "5" }, -1);
            this.groupBox1 = new GroupBox();
            this.cboTitlePosition = new ComboBoxEdit();
            this.btnTitleSymbol = new SimpleButton();
            this.label1 = new Label();
            this.chkShow = new CheckEdit();
            this.txtTitle = new MemoEdit();
            this.groupBox2 = new GroupBox();
            this.cboAreaPatches = new StyleComboBox(this.icontainer_0);
            this.cboLinePatches = new StyleComboBox(this.icontainer_0);
            this.label6 = new Label();
            this.label7 = new Label();
            this.label5 = new Label();
            this.label4 = new Label();
            this.txtHeight = new TextEdit();
            this.txtWidth = new TextEdit();
            this.label3 = new Label();
            this.label2 = new Label();
            this.groupBox3 = new GroupBox();
            this.listView1 = new EditListView();
            this.lvcolumnHeader_0 = new LVColumnHeader();
            this.lvcolumnHeader_1 = new LVColumnHeader();
            this.chkRightToLeft = new CheckEdit();
            this.groupBox1.SuspendLayout();
            this.cboTitlePosition.Properties.BeginInit();
            this.chkShow.Properties.BeginInit();
            this.txtTitle.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.txtHeight.Properties.BeginInit();
            this.txtWidth.Properties.BeginInit();
            this.groupBox3.SuspendLayout();
            this.chkRightToLeft.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.cboTitlePosition);
            this.groupBox1.Controls.Add(this.btnTitleSymbol);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.chkShow);
            this.groupBox1.Controls.Add(this.txtTitle);
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x170, 0x88);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "标题";
            this.cboTitlePosition.EditValue = "上";
            this.cboTitlePosition.Location = new Point(0xa1, 0x67);
            this.cboTitlePosition.Name = "cboTitlePosition";
            this.cboTitlePosition.Properties.Items.AddRange(new object[] { "上", "下", "左", "右" });
            this.cboTitlePosition.Size = new Size(0x40, 0x17);
            this.cboTitlePosition.TabIndex = 5;
            this.cboTitlePosition.SelectedIndexChanged += new EventHandler(this.cboTitlePosition_SelectedIndexChanged);
            this.btnTitleSymbol.Location = new Point(0x130, 0x68);
            this.btnTitleSymbol.Name = "btnTitleSymbol";
            this.btnTitleSymbol.Size = new Size(0x30, 0x18);
            this.btnTitleSymbol.TabIndex = 4;
            this.btnTitleSymbol.Text = "符号";
            this.btnTitleSymbol.Click += new EventHandler(this.btnTitleSymbol_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x80, 0x6c);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 0x11);
            this.label1.TabIndex = 2;
            this.label1.Text = "位置";
            this.chkShow.Location = new Point(8, 0x68);
            this.chkShow.Name = "chkShow";
            this.chkShow.Properties.Caption = "显示";
            this.chkShow.Size = new Size(0x30, 0x13);
            this.chkShow.TabIndex = 1;
            this.chkShow.Click += new EventHandler(this.chkShow_Click);
            this.chkShow.CheckedChanged += new EventHandler(this.chkShow_CheckedChanged);
            this.txtTitle.EditValue = "memoEdit1";
            this.txtTitle.Location = new Point(8, 0x10);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new Size(0x158, 80);
            this.txtTitle.TabIndex = 0;
            this.txtTitle.EditValueChanged += new EventHandler(this.txtTitle_EditValueChanged);
            this.groupBox2.Controls.Add(this.cboAreaPatches);
            this.groupBox2.Controls.Add(this.cboLinePatches);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtHeight);
            this.groupBox2.Controls.Add(this.txtWidth);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new Point(8, 0x98);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(120, 0x90);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "区块";
            this.cboAreaPatches.DrawMode = DrawMode.OwnerDrawVariable;
            this.cboAreaPatches.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboAreaPatches.DropDownWidth = 0x40;
            this.cboAreaPatches.Font = new Font("宋体", 15f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.cboAreaPatches.ItemHeight = 20;
            this.cboAreaPatches.Location = new Point(40, 0x68);
            this.cboAreaPatches.Name = "cboAreaPatches";
            this.cboAreaPatches.Size = new Size(0x40, 0x1a);
            this.cboAreaPatches.TabIndex = 9;
            this.cboAreaPatches.SelectedIndexChanged += new EventHandler(this.cboAreaPatches_SelectedIndexChanged);
            this.cboLinePatches.DrawMode = DrawMode.OwnerDrawVariable;
            this.cboLinePatches.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboLinePatches.DropDownWidth = 0x40;
            this.cboLinePatches.Font = new Font("宋体", 14.25f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.cboLinePatches.ItemHeight = 20;
            this.cboLinePatches.Location = new Point(40, 0x48);
            this.cboLinePatches.Name = "cboLinePatches";
            this.cboLinePatches.Size = new Size(0x40, 0x1a);
            this.cboLinePatches.TabIndex = 8;
            this.cboLinePatches.SelectedIndexChanged += new EventHandler(this.cboLinePatches_SelectedIndexChanged);
            this.label6.AutoSize = true;
            this.label6.Location = new Point(8, 0x70);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x17, 0x11);
            this.label6.TabIndex = 7;
            this.label6.Text = "面:";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(8, 80);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x17, 0x11);
            this.label7.TabIndex = 6;
            this.label7.Text = "线:";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x5b, 0x2d);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x11, 0x11);
            this.label5.TabIndex = 5;
            this.label5.Text = "点";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x5b, 0x13);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x11, 0x11);
            this.label4.TabIndex = 4;
            this.label4.Text = "点";
            this.txtHeight.EditValue = "";
            this.txtHeight.Location = new Point(40, 0x2a);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new Size(0x30, 0x17);
            this.txtHeight.TabIndex = 3;
            this.txtHeight.EditValueChanged += new EventHandler(this.txtHeight_EditValueChanged);
            this.txtWidth.EditValue = "";
            this.txtWidth.Location = new Point(40, 0x10);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new Size(0x30, 0x17);
            this.txtWidth.TabIndex = 2;
            this.txtWidth.EditValueChanged += new EventHandler(this.txtWidth_EditValueChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 0x2f);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x23, 0x11);
            this.label3.TabIndex = 1;
            this.label3.Text = "高度:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 20);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x23, 0x11);
            this.label2.TabIndex = 0;
            this.label2.Text = "宽度:";
            this.groupBox3.Controls.Add(this.listView1);
            this.groupBox3.Location = new Point(0x88, 0x98);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(240, 0x90);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "间隔";
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.lvcolumnHeader_0, this.lvcolumnHeader_1 });
            this.listView1.ComboBoxBgColor = Color.LightBlue;
            this.listView1.ComboBoxFont = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.listView1.EditBgColor = Color.LightBlue;
            this.listView1.EditFont = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = ColumnHeaderStyle.None;
            this.listView1.Items.AddRange(new ListViewItem[] { item, item2, item3, item4, item5, item6, item7, item8, item9 });
            this.listView1.Location = new Point(0x10, 0x18);
            this.listView1.LockRowCount = 0;
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0xd0, 0x68);
            this.listView1.TabIndex = 0;
            this.listView1.View = View.Details;
            this.listView1.ValueChanged += new ValueChangedHandler(this.method_2);
            this.lvcolumnHeader_0.ColumnStyle = ListViewColumnStyle.ReadOnly;
            this.lvcolumnHeader_0.Width = 0x58;
            this.lvcolumnHeader_1.ColumnStyle = ListViewColumnStyle.ReadOnly;
            this.lvcolumnHeader_1.Width = 0x5d;
            this.chkRightToLeft.Location = new Point(0x10, 0x130);
            this.chkRightToLeft.Name = "chkRightToLeft";
            this.chkRightToLeft.Properties.Caption = "从右到左读取";
            this.chkRightToLeft.Size = new Size(0x70, 0x13);
            this.chkRightToLeft.TabIndex = 12;
            this.chkRightToLeft.Click += new EventHandler(this.chkRightToLeft_Click);
            this.chkRightToLeft.CheckedChanged += new EventHandler(this.chkRightToLeft_CheckedChanged);
            base.Controls.Add(this.chkRightToLeft);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "LegendPropertyPage";
            base.Size = new Size(0x188, 0x158);
            base.Load += new EventHandler(this.LegendPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.cboTitlePosition.Properties.EndInit();
            this.chkShow.Properties.EndInit();
            this.txtTitle.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.txtHeight.Properties.EndInit();
            this.txtWidth.Properties.EndInit();
            this.groupBox3.ResumeLayout(false);
            this.chkRightToLeft.Properties.EndInit();
            base.ResumeLayout(false);
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
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

