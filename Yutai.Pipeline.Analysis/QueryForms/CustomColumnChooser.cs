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
	public partial class CustomColumnChooser : Form
	{





















		private FontData mFontData = new FontData();


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
