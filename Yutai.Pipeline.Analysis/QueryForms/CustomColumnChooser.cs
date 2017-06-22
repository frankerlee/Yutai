using Infragistics.Win;
using Infragistics.Win.UltraWinDataSource;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.Pipeline.Analysis.QueryForms
{
	public class CustomColumnChooser : Form
	{
		private IContainer components = null;

		private UltraGridColumnChooser ultraGridColumnChooser1;

		private Label label1;

		private GroupBox groupBox1;

		private Label label2;

		private TextBox TopBox;

		private Button button1;

		private Button button2;

		private GroupBox groupBox2;

		private ComboBox FontSizeBox;

		private UltraFontNameEditor ultraFontNameEditor1;

		private CheckBox UnderlineStyle;

		private CheckBox ItaStyle;

		private CheckBox BlackStyle;

		private ImageList IconList;

		private CheckBox FitWidthBox;

		private CheckBox HaveHead;

		private CheckBox ShowGeoInfo;

		private UltraGridBase grid;

		private UltraCombo ultraComboBandSelector;

		private UltraColorPicker ultraColorPicker1;

		private FontData mFontData = new FontData();

		private Color mFontColor;

		public UltraGridBase Grid
		{
			get
			{
				return this.grid;
			}
			set
			{
				if (value != this.grid)
				{
					this.grid = value;
					this.ultraGridColumnChooser1.SourceGrid=(this.grid);
					this.InitializeBandsCombo(this.grid);
					if (this.ultraComboBandSelector.Rows.Count > 0)
					{
						this.ultraComboBandSelector.SelectedRow=(this.ultraComboBandSelector.Rows[0]);
					}
				}
			}
		}

		public UltraGridBand CurrentBand
		{
			get
			{
				return this.ColumnChooserControl.CurrentBand;
			}
			set
			{
				if (value != null && (this.Grid == null || this.Grid != value.Layout.Grid))
				{
					throw new ArgumentException();
				}
				this.ultraComboBandSelector.Value=(value);
			}
		}

		public UltraGridColumnChooser ColumnChooserControl
		{
			get
			{
				return this.ultraGridColumnChooser1;
			}
		}

		public string TopBand
		{
			get
			{
				return this.TopBox.Text;
			}
			set
			{
				this.TopBox.Text = value;
			}
		}

		public FontData mFont
		{
			get
			{
				return this.mFontData;
			}
			set
			{
				this.mFontData = value;
			}
		}

		public Color mColor
		{
			get
			{
				return this.mFontColor;
			}
			set
			{
				this.mFontColor = value;
			}
		}

		public bool bFitWidth
		{
			get
			{
				return this.FitWidthBox.Checked;
			}
			set
			{
				this.FitWidthBox.Checked = value;
			}
		}

		public bool HaveTopBand
		{
			get
			{
				return this.HaveHead.Checked;
			}
			set
			{
				this.HaveHead.Checked = value;
			}
		}

		public bool ShowGeo
		{
			get
			{
				return this.ShowGeoInfo.Checked;
			}
			set
			{
				this.ShowGeoInfo.Checked = value;
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.components = new Container();
			Infragistics.Win.Appearance appearance = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomColumnChooser));
			this.ultraComboBandSelector = new UltraCombo();
			this.ultraGridColumnChooser1 = new UltraGridColumnChooser();
			this.label1 = new Label();
			this.groupBox1 = new GroupBox();
			this.label2 = new Label();
			this.TopBox = new TextBox();
			this.button1 = new Button();
			this.button2 = new Button();
			this.groupBox2 = new GroupBox();
			this.HaveHead = new CheckBox();
			this.UnderlineStyle = new CheckBox();
			this.IconList = new ImageList(this.components);
			this.ItaStyle = new CheckBox();
			this.BlackStyle = new CheckBox();
			this.ultraColorPicker1 = new UltraColorPicker();
			this.FontSizeBox = new ComboBox();
			this.ultraFontNameEditor1 = new UltraFontNameEditor();
			this.FitWidthBox = new CheckBox();
			this.ShowGeoInfo = new CheckBox();
			((ISupportInitialize) this.ultraComboBandSelector).BeginInit();
			((ISupportInitialize) this.ultraGridColumnChooser1).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((ISupportInitialize)this.ultraColorPicker1).BeginInit();
			((ISupportInitialize)this.ultraFontNameEditor1).BeginInit();
			base.SuspendLayout();
			this.ultraComboBandSelector.CheckedListSettings.CheckStateMember=("");
			appearance.BackColor = SystemColors.Window;
			appearance.BorderColor = SystemColors.InactiveCaption;
			this.ultraComboBandSelector.DisplayLayout.Appearance=(appearance);
			this.ultraComboBandSelector.DisplayLayout.CaptionVisible=(DefaultableBoolean.False);
			appearance2.BackColor = SystemColors.ActiveBorder;
			appearance2.BackColor2 = SystemColors.ControlDark;
			appearance2.BackGradientStyle = GradientStyle.Vertical;
			appearance2.BorderColor = SystemColors.Window;
			this.ultraComboBandSelector.DisplayLayout.GroupByBox.Appearance=(appearance2);
			appearance3.ForeColor = SystemColors.GrayText;
			this.ultraComboBandSelector.DisplayLayout.GroupByBox.BandLabelAppearance=(appearance3);
			this.ultraComboBandSelector.DisplayLayout.GroupByBox.BorderStyle=(UIElementBorderStyle.Solid);
			appearance4.BackColor = SystemColors.ControlLightLight;
			appearance4.BackColor2 = SystemColors.Control;
			appearance4.BackGradientStyle = GradientStyle.Horizontal;
			appearance4.ForeColor = SystemColors.GrayText;
			this.ultraComboBandSelector.DisplayLayout.GroupByBox.PromptAppearance=(appearance4);
			this.ultraComboBandSelector.DisplayLayout.MaxColScrollRegions=(1);
			this.ultraComboBandSelector.DisplayLayout.MaxRowScrollRegions=(1);
			appearance5.BackColor = SystemColors.Window;
			appearance5.ForeColor = SystemColors.ControlText;
			this.ultraComboBandSelector.DisplayLayout.Override.ActiveCellAppearance=(appearance5);
			appearance6.BackColor = SystemColors.Highlight;
			appearance6.ForeColor = SystemColors.HighlightText;
			this.ultraComboBandSelector.DisplayLayout.Override.ActiveRowAppearance=(appearance6);
			this.ultraComboBandSelector.DisplayLayout.Override.BorderStyleCell=(UIElementBorderStyle.Dotted);
			this.ultraComboBandSelector.DisplayLayout.Override.BorderStyleRow=(UIElementBorderStyle.Dotted);
			appearance7.BackColor = SystemColors.Window;
			this.ultraComboBandSelector.DisplayLayout.Override.CardAreaAppearance=(appearance7);
			appearance8.BorderColor = Color.Silver;
			appearance8.TextTrimming = TextTrimming.EllipsisCharacter;
			this.ultraComboBandSelector.DisplayLayout.Override.CellAppearance=(appearance8);
			this.ultraComboBandSelector.DisplayLayout.Override.CellClickAction= CellClickAction.Default;
			this.ultraComboBandSelector.DisplayLayout.Override.CellPadding=(0);
			appearance9.BackColor = SystemColors.Control;
			appearance9.BackColor2 = SystemColors.ControlDark;
			appearance9.BackGradientAlignment = GradientAlignment.Client;
			appearance9.BackGradientStyle = GradientStyle.Horizontal;
			appearance9.BorderColor = SystemColors.Window;
			this.ultraComboBandSelector.DisplayLayout.Override.GroupByRowAppearance=(appearance9);
			appearance10.TextHAlignAsString = "Left";
			this.ultraComboBandSelector.DisplayLayout.Override.HeaderAppearance=(appearance10);
			this.ultraComboBandSelector.DisplayLayout.Override.HeaderClickAction= HeaderClickAction.Default;
			this.ultraComboBandSelector.DisplayLayout.Override.HeaderStyle=(HeaderStyle.WindowsXPCommand);
			appearance11.BackColor = SystemColors.Window;
			appearance11.BorderColor = Color.Silver;
			this.ultraComboBandSelector.DisplayLayout.Override.RowAppearance=(appearance11);
			this.ultraComboBandSelector.DisplayLayout.Override.RowSelectors=(DefaultableBoolean.False);
			appearance12.BackColor = SystemColors.ControlLight;
			this.ultraComboBandSelector.DisplayLayout.Override.TemplateAddRowAppearance=(appearance12);
			this.ultraComboBandSelector.DisplayLayout.ScrollBounds=(0);
			this.ultraComboBandSelector.DisplayLayout.ScrollStyle=(ScrollStyle) (1);
			this.ultraComboBandSelector.DisplayLayout.ViewStyleBand=(ViewStyleBand) (2);
			this.ultraComboBandSelector.DropDownStyle=(UltraComboStyle) (1);
			this.ultraComboBandSelector.Location = new System.Drawing.Point(57, 1);
			this.ultraComboBandSelector.Name = "ultraComboBandSelector";
			this.ultraComboBandSelector.Size = new Size(123, 22);
			this.ultraComboBandSelector.TabIndex = 3;
			this.ultraComboBandSelector.RowSelected+=(new RowSelectedEventHandler(this.ultraComboBandSelector_RowSelected));
			this.ultraGridColumnChooser1.DisplayLayout.AutoFitStyle=(AutoFitStyle) (1);
			this.ultraGridColumnChooser1.DisplayLayout.CaptionVisible=(DefaultableBoolean.False);
			this.ultraGridColumnChooser1.DisplayLayout.MaxColScrollRegions=(1);
			this.ultraGridColumnChooser1.DisplayLayout.MaxRowScrollRegions=(1);
			this.ultraGridColumnChooser1.DisplayLayout.Override.AllowColSizing=(AllowColSizing) (1);
			this.ultraGridColumnChooser1.DisplayLayout.Override.AllowRowLayoutCellSizing=(RowLayoutSizing) (1);
			this.ultraGridColumnChooser1.DisplayLayout.Override.AllowRowLayoutLabelSizing=(RowLayoutSizing) (1);
			this.ultraGridColumnChooser1.DisplayLayout.Override.CellClickAction=(CellClickAction) (2);
			this.ultraGridColumnChooser1.DisplayLayout.Override.CellPadding=(2);
			this.ultraGridColumnChooser1.DisplayLayout.Override.ExpansionIndicator=(ShowExpansionIndicator) (2);
			this.ultraGridColumnChooser1.DisplayLayout.Override.HeaderClickAction=(HeaderClickAction) (1);
			this.ultraGridColumnChooser1.DisplayLayout.Override.RowSelectors=(DefaultableBoolean.False);
			this.ultraGridColumnChooser1.DisplayLayout.Override.RowSizing=(RowSizing) (4);
			this.ultraGridColumnChooser1.DisplayLayout.Override.SelectTypeCell=(SelectType) (1);
			this.ultraGridColumnChooser1.DisplayLayout.Override.SelectTypeCol=(SelectType) (1);
			this.ultraGridColumnChooser1.DisplayLayout.Override.SelectTypeRow=(SelectType) (1);
			this.ultraGridColumnChooser1.DisplayLayout.RowConnectorStyle=(RowConnectorStyle) (1);
			this.ultraGridColumnChooser1.DisplayLayout.ScrollBounds=(0);
			this.ultraGridColumnChooser1.DisplayLayout.ScrollStyle=(ScrollStyle) (1);
			this.ultraGridColumnChooser1.Location = new System.Drawing.Point(7, 16);
			this.ultraGridColumnChooser1.MultipleBandSupport=(MultipleBandSupport) (2);
			this.ultraGridColumnChooser1.Name = "ultraGridColumnChooser1";
			this.ultraGridColumnChooser1.Size = new Size(170, 212);
			this.ultraGridColumnChooser1.Style=(ColumnChooserStyle) (1);
			this.ultraGridColumnChooser1.StyleLibraryName=("");
			this.ultraGridColumnChooser1.StyleSetName=("");
			this.ultraGridColumnChooser1.TabIndex = 4;
			this.ultraGridColumnChooser1.Text = "ultraGridColumnChooser2";
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(5, 9);
			this.label1.Name = "label1";
			this.label1.Size = new Size(47, 12);
			this.label1.TabIndex = 5;
			this.label1.Text = "表选择:";
			this.groupBox1.Controls.Add(this.ultraGridColumnChooser1);
			this.groupBox1.Location = new System.Drawing.Point(4, 27);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(183, 237);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "字段列表";
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(15, 23);
			this.label2.Name = "label2";
			this.label2.Size = new Size(35, 12);
			this.label2.TabIndex = 7;
			this.label2.Text = "表头:";
			this.TopBox.Location = new System.Drawing.Point(56, 18);
			this.TopBox.Name = "TopBox";
			this.TopBox.Size = new Size(215, 21);
			this.TopBox.TabIndex = 8;
			this.button1.DialogResult = DialogResult.OK;
			this.button1.Location = new System.Drawing.Point(320, 241);
			this.button1.Name = "button1";
			this.button1.Size = new Size(75, 23);
			this.button1.TabIndex = 11;
			this.button1.Text = "确定";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.button2.DialogResult = DialogResult.Cancel;
			this.button2.Location = new System.Drawing.Point(401, 241);
			this.button2.Name = "button2";
			this.button2.Size = new Size(75, 23);
			this.button2.TabIndex = 12;
			this.button2.Text = "关闭";
			this.button2.UseVisualStyleBackColor = true;
			this.groupBox2.Controls.Add(this.HaveHead);
			this.groupBox2.Controls.Add(this.UnderlineStyle);
			this.groupBox2.Controls.Add(this.ItaStyle);
			this.groupBox2.Controls.Add(this.BlackStyle);
			this.groupBox2.Controls.Add(this.ultraColorPicker1);
			this.groupBox2.Controls.Add(this.FontSizeBox);
			this.groupBox2.Controls.Add(this.ultraFontNameEditor1);
			this.groupBox2.Controls.Add(this.TopBox);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Location = new System.Drawing.Point(193, 9);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(286, 101);
			this.groupBox2.TabIndex = 13;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "表头";
			this.HaveHead.AutoSize = true;
			this.HaveHead.Location = new System.Drawing.Point(6, 0);
			this.HaveHead.Name = "HaveHead";
			this.HaveHead.Size = new Size(48, 16);
			this.HaveHead.TabIndex = 15;
			this.HaveHead.Text = "表头";
			this.HaveHead.UseVisualStyleBackColor = true;
			this.UnderlineStyle.Appearance = System.Windows.Forms.Appearance.Button;
			this.UnderlineStyle.AutoSize = true;
			this.UnderlineStyle.ImageIndex = 2;
			this.UnderlineStyle.ImageList = this.IconList;
			this.UnderlineStyle.Location = new System.Drawing.Point(128, 73);
			this.UnderlineStyle.Name = "UnderlineStyle";
			this.UnderlineStyle.Size = new Size(22, 22);
			this.UnderlineStyle.TabIndex = 14;
			this.UnderlineStyle.UseVisualStyleBackColor = true;
			this.IconList.ImageStream = (ImageListStreamer)resources.GetObject("IconList.ImageStream");
			this.IconList.TransparentColor = Color.Transparent;
			this.IconList.Images.SetKeyName(0, "btnPicSetting.Image.png");
			this.IconList.Images.SetKeyName(1, "");
			this.IconList.Images.SetKeyName(2, "");
			this.ItaStyle.Appearance = System.Windows.Forms.Appearance.Button;
			this.ItaStyle.AutoSize = true;
			this.ItaStyle.ImageIndex = 1;
			this.ItaStyle.ImageList = this.IconList;
			this.ItaStyle.Location = new System.Drawing.Point(101, 73);
			this.ItaStyle.Name = "ItaStyle";
			this.ItaStyle.Size = new Size(22, 22);
			this.ItaStyle.TabIndex = 13;
			this.ItaStyle.UseVisualStyleBackColor = true;
			this.BlackStyle.Appearance = System.Windows.Forms.Appearance.Button;
			this.BlackStyle.AutoSize = true;
			this.BlackStyle.ImageIndex = 0;
			this.BlackStyle.ImageList = this.IconList;
			this.BlackStyle.Location = new System.Drawing.Point(76, 73);
			this.BlackStyle.Name = "BlackStyle";
			this.BlackStyle.Size = new Size(22, 22);
			this.BlackStyle.TabIndex = 12;
			this.BlackStyle.UseVisualStyleBackColor = true;
			this.ultraColorPicker1.Color = Color.Black;
			this.ultraColorPicker1.Location = new System.Drawing.Point(26, 75);
			this.ultraColorPicker1.Name = "ultraColorPicker1";
			this.ultraColorPicker1.Size = new Size(44, 21);
			this.ultraColorPicker1.TabIndex = 11;
			this.ultraColorPicker1.Text = "Black";
			this.FontSizeBox.FormattingEnabled = true;
			this.FontSizeBox.Items.AddRange(new object[]
			{
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"14",
				"16",
				"18",
				"20",
				"22",
				"24",
				"26",
				"28",
				"36",
				"48",
				"72"
			});
			this.FontSizeBox.Location = new System.Drawing.Point(99, 50);
			this.FontSizeBox.Name = "FontSizeBox";
			this.FontSizeBox.Size = new Size(57, 20);
			this.FontSizeBox.TabIndex = 10;
			this.FontSizeBox.Text = "24";
			this.ultraFontNameEditor1.Location = new System.Drawing.Point(25, 50);
			this.ultraFontNameEditor1.Name = "ultraFontNameEditor1";
			this.ultraFontNameEditor1.Size = new Size(70, 21);
			this.ultraFontNameEditor1.TabIndex = 9;
			this.ultraFontNameEditor1.Text = "宋体";
			this.FitWidthBox.AutoSize = true;
			this.FitWidthBox.Checked = true;
			this.FitWidthBox.CheckState = CheckState.Checked;
			this.FitWidthBox.Location = new System.Drawing.Point(196, 250);
			this.FitWidthBox.Name = "FitWidthBox";
			this.FitWidthBox.Size = new Size(96, 16);
			this.FitWidthBox.TabIndex = 14;
			this.FitWidthBox.Text = "宽度适合纸张";
			this.FitWidthBox.UseVisualStyleBackColor = true;
			this.ShowGeoInfo.AutoSize = true;
			this.ShowGeoInfo.Checked = true;
			this.ShowGeoInfo.CheckState = CheckState.Checked;
			this.ShowGeoInfo.Location = new System.Drawing.Point(197, 229);
			this.ShowGeoInfo.Name = "ShowGeoInfo";
			this.ShowGeoInfo.Size = new Size(96, 16);
			this.ShowGeoInfo.TabIndex = 15;
			this.ShowGeoInfo.Text = "显示几何信息";
			this.ShowGeoInfo.UseVisualStyleBackColor = true;
			this.AutoScaleBaseSize = new Size(6, 14);
			base.ClientSize = new Size(485, 271);
			base.Controls.Add(this.ShowGeoInfo);
			base.Controls.Add(this.FitWidthBox);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.ultraComboBandSelector);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CustomColumnChooser";
			base.ShowInTaskbar = false;
			this.Text = "列选择设置";
			base.Load += new EventHandler(this.CustomColumnChooser_Load);
			((ISupportInitialize) this.ultraComboBandSelector).EndInit();
			((ISupportInitialize) this.ultraGridColumnChooser1).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((ISupportInitialize)this.ultraColorPicker1).EndInit();
			((ISupportInitialize)this.ultraFontNameEditor1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public CustomColumnChooser()
		{
			this.InitializeComponent();
		}

		private void ultraButtonNewColumn_Click(object sender, EventArgs e)
		{
		}

		private void ultraButtonDeleteColumn_Click(object sender, EventArgs e)
		{
		}

		private void InitializeBandsCombo(UltraGridBase grid)
		{
			this.ultraComboBandSelector.SetDataBinding(null, null);
			if (grid != null)
			{
				UltraDataSource ultraDataSource = new UltraDataSource();
				ultraDataSource.Band.Columns.Add("Band", typeof(UltraGridBand));
				ultraDataSource.Band.Columns.Add("DisplayText", typeof(string));
				BandEnumerator enumerator = grid.DisplayLayout.Bands.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						UltraGridBand current = enumerator.Current;
						if (!this.IsBandExcluded(current))
						{
							ultraDataSource.Rows.Add(new object[]
							{
								current,
								current.Header.Caption
							});
						}
					}
				}
				finally
				{
					IDisposable disposable = enumerator as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
				this.ultraComboBandSelector.DisplayMember=("DisplayText");
				this.ultraComboBandSelector.ValueMember=("Band");
				this.ultraComboBandSelector.SetDataBinding(ultraDataSource, null);
				this.ultraComboBandSelector.DisplayLayout.Bands[0].Columns["Band"].Hidden=(true);
				this.ultraComboBandSelector.DisplayLayout.Bands[0].ColHeadersVisible=(false);
				this.ultraComboBandSelector.DropDownWidth=(0);
				this.ultraComboBandSelector.DisplayLayout.Override.HotTrackRowAppearance.BackColor = Color.LightYellow;
				this.ultraComboBandSelector.DisplayLayout.AutoFitStyle=(AutoFitStyle) (1);
				this.ultraComboBandSelector.DisplayLayout.BorderStyle=(UIElementBorderStyle.Solid);
				this.ultraComboBandSelector.DisplayLayout.Appearance.BorderColor = SystemColors.Highlight;
			}
		}

		private bool IsBandExcluded(UltraGridBand band)
		{
			bool result;
			while (band != null)
			{
				if (1 == (int) band.ExcludeFromColumnChooser)
				{
					result = true;
					return result;
				}
				band = band.ParentBand;
			}
			result = false;
			return result;
		}

		private void ultraComboBandSelector_RowSelected(object sender, RowSelectedEventArgs e)
		{
			if (this.Grid != null && !this.Grid.IsDisposed)
			{
				UltraGridBand ultraGridBand = this.ultraComboBandSelector.Value as UltraGridBand;
				if (ultraGridBand == null)
				{
					ultraGridBand = this.Grid.DisplayLayout.Bands[0];
					this.ultraComboBandSelector.Value=(ultraGridBand);
				}
				this.ultraGridColumnChooser1.CurrentBand=(ultraGridBand);
			}
		}

		private void CustomColumnChooser_Load(object sender, EventArgs e)
		{
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (this.BlackStyle.Checked)
			{
				this.mFontData.Bold = DefaultableBoolean.True;
			}
			else
			{
				this.mFontData.Bold = DefaultableBoolean.False;
			}
			if (this.ItaStyle.Checked)
			{
				this.mFontData.Italic = DefaultableBoolean.True;
			}
			else
			{
				this.mFontData.Italic = DefaultableBoolean.False;
			}
			if (this.UnderlineStyle.Checked)
			{
				this.mFontData.Underline = DefaultableBoolean.True;
			}
			else
			{
				this.mFontData.Underline = DefaultableBoolean.False;
			}
			this.mFontData.SizeInPoints = Convert.ToSingle(this.FontSizeBox.Text);
			this.mFontData.Name = this.ultraFontNameEditor1.SelectedItem.DisplayText;
			this.mFontColor = this.ultraColorPicker1.Color;
		}
	}
}
