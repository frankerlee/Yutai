namespace Yutai.ArcGIS.Common.ExtendClass
{
	public interface ISeriesProperties
	{
		/// <summary>
		/// Default color for all points
		/// </summary>
		int Color
		{
			get;
			set;
		}

		/// <summary>
		/// Draw points with different preset Colors
		/// </summary>
		bool ColorEach
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the Datasource Color Field
		/// </summary>
		string ColorMember
		{
			get;
			set;
		}

		object DataSource
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the Datasource Label Field
		/// </summary>
		string LabelMember
		{
			get;
			set;
		}

		/// <summary>
		/// Sets the Format to display point values as percentage
		/// </summary>
		string PercentFormat
		{
			get;
			set;
		}

		/// <summary>
		/// Displays this Series Title in Legend
		/// </summary>
		bool ShowInLegend
		{
			get;
			set;
		}

		/// <summary>
		/// Series description to show in Legend and editors
		/// </summary>
		string Title
		{
			get;
			set;
		}
	}
}