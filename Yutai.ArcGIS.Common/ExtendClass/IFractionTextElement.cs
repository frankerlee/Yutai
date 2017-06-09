using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Common.ExtendClass
{
	public interface IFractionTextElement
	{
		string DenominatorText
		{
			get;
			set;
		}

		ITextSymbol DenominatorTextSymbol
		{
			get;
			set;
		}

		ILineSymbol LineSymbol
		{
			get;
			set;
		}

		string NumeratorText
		{
			get;
			set;
		}

		ITextSymbol NumeratorTextSymbol
		{
			get;
			set;
		}

		bool ResetGeometry
		{
			set;
		}
	}
}