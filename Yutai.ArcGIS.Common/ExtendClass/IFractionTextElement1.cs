using Yutai.ArcGIS.Common.SymbolEx;

namespace Yutai.ArcGIS.Common.ExtendClass
{
	public interface IFractionTextElement1
	{
		string DenominatorText
		{
			get;
			set;
		}

		IFractionTextSymbol FractionTextSymbol
		{
			get;
			set;
		}

		string NumeratorText
		{
			get;
			set;
		}
	}
}